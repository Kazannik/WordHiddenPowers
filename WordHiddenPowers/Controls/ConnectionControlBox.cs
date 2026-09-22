using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using Thread = System.Threading.Thread;
using Timer = System.Windows.Forms.Timer;


namespace WordHiddenPowers.Controls
{

	[ToolboxBitmap(typeof(TextBox))]
	[ComVisible(false)]
	public partial class ConnectionControlBox : Control
	{
		public const int MAX_PING_TIMEOUT = 1000;
		public static readonly TimeSpan MAX_TIMEOUT = new(days: 0, hours: 23, minutes: 59, seconds: 59, milliseconds: 999);

		private string _address;
		private int _pingTimeout;
		private TimeSpan _timeout;
		
		private readonly HttpClient client;

		private readonly BufferedGraphicsContext context = BufferedGraphicsManager.Current;

		public string Address
		{
			get => _address;
			private set
			{
				if (value != _address)
				{
					_address = value;
					DoAddressChanged();
				}
			}
		}

		public int PingTimeout
		{
			get => _pingTimeout;
			set
			{
				if (value != _pingTimeout)
				{
					_pingTimeout = value;
					DoPingTimeoutChanged();
				}
			}
		}

		public TimeSpan Timeout
		{
			get => _timeout;
			set
			{
				if (value != _timeout)
				{
					_timeout = value;
					DoTimeoutChanged();
				}
			}
		}

		/// <summary>
		/// Признак корректности ввода адреса.
		/// </summary>
		public bool IsCorrect { get; private set; } = false;

		/// <summary>
		/// Признак прохождения сигнала Ping.
		/// </summary>
		public bool IsPing { get; private set; } = false;

		/// <summary>
		/// Признак корректронсти соединения.
		/// </summary>
		public bool IsConnected { get; private set; } = false;

		/// <summary>
		/// Признак внутренней ошибки.
		/// </summary>
		public bool IsError { get; private set; } = false;

		public Uri Uri
		{
			get => Uri.TryCreate(Address, UriKind.Absolute, out Uri uri) ? uri : default;
			set
			{
				Address = value != null ? value.OriginalString : string.Empty;
			}
		}

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public string Caption
		{
			get => addressLabel.Text;
			set => addressLabel.Text = value;
		}

		[System.ComponentModel.Browsable(false)]
		public override bool Focused =>
			base.Focused ||
			addressLabel.Focused ||
			addressTextBox.Focused ||
			moreButton.Focused;
								

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler AddressChanged;
		protected virtual void OnAddressChanged(EventArgs e) => AddressChanged?.Invoke(this, e);

		private void DoAddressChanged()
		{			
			addressTextBox.Text = Address;
			OnAddressChanged(new EventArgs());
		}
		
		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler PingTimeoutChanged;
		protected virtual void OnPingTimeoutChanged(EventArgs e) => PingTimeoutChanged?.Invoke(this, e);
		private void DoPingTimeoutChanged()
		{
			OnPingTimeoutChanged(new EventArgs());
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler TimeoutChanged;
		protected virtual void OnTimeoutChanged(EventArgs e) => TimeoutChanged?.Invoke(this, e);
		private void DoTimeoutChanged()
		{
			OnTimeoutChanged(new EventArgs());
		}

		private StateEnum state = StateEnum.OK;

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> StateChanged;
		protected virtual void OnStateChanged(ConnectionEventArgs e) => StateChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> PingChanged;
		protected virtual void OnPingChanged(ConnectionEventArgs e) => PingChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> Connecting;
		protected virtual void OnConnecting(ConnectionEventArgs e) => Connecting?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> Connected;
		protected virtual void OnConnected(ConnectionEventArgs e) => Connected?.Invoke(this, e);
		
		private ConnectionControlBox()
		{
			client = null;
			InitializeComponent();
		}
				
		public ConnectionControlBox(HttpClient client) : this()
		{
			this.client = client;
		}

		public ConnectionControlBox(HttpClient client, Uri uri) : this(client: client)
		{
			this.Uri = uri;
		}

		private void DoShowDialog()
		{
			TimeoutDialog dialog = new(pingTimeout: PingTimeout, timeout: Timeout, maxPingTimeout: MAX_PING_TIMEOUT, maxTimeout: MAX_TIMEOUT);
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				PingTimeout = dialog.PingTimeout;
				Timeout = dialog.Timeout;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			DoResize();
		}
		
		protected override void OnResize(EventArgs e)
		{
			DoResize();
			base.OnResize(e);
		}
		
		protected override void OnEnter(EventArgs e)
		{
			addressTextBox.Focus();
			base.OnEnter(e);
		}
				
		private void Component_Enter(object sender, EventArgs e)
		{
			base.OnEnter(e);
		}

		private void Component_Leave(object sender, EventArgs e)
		{
			base.OnLeave(e);
		}

		private void DoResize()
		{
			context.MaximumBuffer = new Size(ClientSize.Width + 1, ClientSize.Height + 1);

			this.SuspendLayout();
			addressLabel.Location = new Point(0, 2);
			addressTextBox.Location = new Point(addressLabel.Width + 4, 0);
			addressTextBox.Width = Width - addressLabel.Width - 4;
			Height = addressTextBox.Height * 2 + 6;
			moreButton.Location = new Point(Width - moreButton.Width, Height - moreButton.Height - 2);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rect = new(e.ClipRectangle.X, e.ClipRectangle.Y + addressTextBox.Height + 3, e.ClipRectangle.Width, addressTextBox.Height + 2);
			Rectangle iconRect = new(new Point(Width - 40, Height - 20), new Size(16, 16));
			try
			{
				BufferedGraphics grafx = context.Allocate(e.Graphics, rect);

				grafx.Graphics.FillRectangle(new SolidBrush(BackColor), rect);

				string message = string.Empty;

				if (DesignMode)
				{
					message = "Test connecting";
					grafx.Graphics.DrawImage(Properties.Resources.ConnectionOk, iconRect);
				}
				else if (!IsCorrect)
				{
					message = "Invalid host";
					grafx.Graphics.DrawImage(Properties.Resources.UncorrectedAddress, iconRect);
				}
				else if (state == StateEnum.OK)
				{

				}
				else if (state == (StateEnum.Ping | StateEnum.OK))
				{
					message = "Ping";
				}
				else if (state == (StateEnum.Ping | StateEnum.ERROR))
				{
					message = "Unable to ping the host";
					grafx.Graphics.DrawImage(Properties.Resources.UncorrectedAddress, iconRect);
				}
				else if (state == StateEnum.Connecting)
				{
					message = "Connecting...";
					grafx.Graphics.DrawImage(Properties.Resources.Connecting, iconRect);
				}
				else if (state == (StateEnum.Connected | StateEnum.OK))
				{
					message = "Ok!";
					grafx.Graphics.DrawImage(Properties.Resources.ConnectionOk, iconRect);
				}
				else if (state == (StateEnum.Connected | StateEnum.ERROR))
				{
					message = "Connection error";
					grafx.Graphics.DrawImage(Properties.Resources.ConnectionError, iconRect);
				}
				else
				{
					message = "Error!";
					grafx.Graphics.DrawImage(Properties.Resources.UncorrectedAddress, iconRect);
				}

				if (!string.IsNullOrEmpty(message))
				{
					Size messageSize = grafx.Graphics.MeasureString(message, Font).ToSize();
					Rectangle messageRect = new(rect.X + iconRect.Left - SystemInformation.BorderSize.Width * 4 - messageSize.Width, rect.Y + 5, messageSize.Width + 4, messageSize.Height);
					grafx.Graphics.DrawString(message, Font, new SolidBrush(ForeColor), messageRect);
				}
				grafx.Render(e.Graphics);
			}
			catch (Exception) { }			
			base.OnPaint(e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				DoCheckConnected();
			}
			base.OnMouseDoubleClick(e);
		}

		public void CheckConnection() => DoCheckConnection();

		private void DoCheckConnection() => addressChangedTimer.Start();

		private async void DoCheckConnected()
		{
			OnEvent(new State(uri: Uri, StateEnum.Checked | StateEnum.OK));

			if (PingTimeout > 0)
			{
				Thread.Sleep(20);
				bool ping = await PingAsync(GetBaseAddress(Uri).Host, PingTimeout);
				if (ping)
				{
					OnEvent(new State(uri: Uri, StateEnum.Ping | StateEnum.OK));
				}
				else
				{
					OnEvent(new State(uri: Uri, StateEnum.Ping | StateEnum.ERROR));
					return;
				}
			}
			
			Thread.Sleep(20);
			OnEvent(new State(uri: Uri, StateEnum.Connecting | StateEnum.OK));

			if (client != null) 
			{
				int check = await CheckHostAsync(client, Uri);
				OnEvent(new State(uri: Uri, StateEnum.Connected | (check > 0 ? StateEnum.OK : StateEnum.ERROR)));		
			}
			else
			{
				OnEvent(new State(uri: Uri, StateEnum.Connecting | StateEnum.ERROR));
			}
		}

		private void OnEvent(State state)
		{
			this.state = state.ConnectionState;

			IsError = this.state.HasFlag(StateEnum.ERROR);

			switch (this.state)
			{
				case StateEnum.Checked | StateEnum.OK:
					IsPing = false;
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				case StateEnum.Ping | StateEnum.OK:
					IsPing = true;
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					OnPingChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				case StateEnum.Ping | StateEnum.ERROR:
					IsPing = false;
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					OnPingChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				case StateEnum.Connecting | StateEnum.OK:
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					OnConnecting(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				case StateEnum.Connected | StateEnum.OK:
					IsConnected = true;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					OnConnected(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				case StateEnum.Connected | StateEnum.ERROR:
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					OnConnected(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
				default:
					IsError = true;
					IsPing = false;
					IsConnected = false;
					OnStateChanged(new ConnectionEventArgs(uri: state.Uri, state: this.state));
					break;
			}
			Invalidate();
		}

		private void AddressTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			Address = textBox.Text;
		}

		private string oldAddress = string.Empty;
		
		private void AddressChangedTimer_Tick(object sender, EventArgs e)
		{
			Timer timer = sender as Timer;

			if (Address != oldAddress)
			{
				oldAddress = Address;
			}
			else
			{
				if (Uri.TryCreate(Address, UriKind.Absolute, out _))
				{
					timer.Stop();
					IsCorrect = true;
					DoCheckConnected();
				}
				else
				{
					timer.Stop();
					IsCorrect = false;
				}
			}
		}
				
		private static Uri GetBaseAddress(Uri endpoint)
		{
			string absoluteUri = endpoint.AbsoluteUri;
			string absolutePath = endpoint.AbsolutePath;
			return new Uri(absoluteUri[..^absolutePath.Length]);
		}

		private async Task<bool> PingAsync(string host, int timeout)
		{
			Ping ping = new();
			try
			{
				PingReply reply = await ping
					.SendPingAsync(hostNameOrAddress: host, timeout: timeout)
					.ConfigureAwait(false);
				return reply.Status == IPStatus.Success;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private async Task<int> CheckHostAsync(HttpClient client, Uri requestUri)
		{
			try
			{
				using HttpResponseMessage response = await client.GetAsync(requestUri: requestUri)
					.ConfigureAwait(false);
				return (int)response.StatusCode;
			}
			catch (Exception)
			{
				return 0;
			}
		}

		private readonly struct State(Uri uri, StateEnum connectionState)
		{
			public readonly Uri Uri = uri;
			public readonly StateEnum ConnectionState = connectionState;
		}

		[Flags]
		public enum StateEnum : short
		{
			OK = 0,
			ERROR = 1,
			Checked = 2,
			Ping = 4,
			Connecting = 8,
			Connected = 16
		}

		public class ConnectionEventArgs(Uri uri, StateEnum state) : EventArgs
		{
			public Uri Uri { get; } = uri;

			public StateEnum State { get; set; } = state;

		}
				
		private void MoreButton_Click(object sender, EventArgs e)
		{
			Parent?.Invalidate();
			DoShowDialog();
		}
	}
}
