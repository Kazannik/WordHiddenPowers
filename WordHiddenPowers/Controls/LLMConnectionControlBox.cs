using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using Net = LLMConnectorLibrary.Utils.Net;

namespace WordHiddenPowers.Controls
{
	public partial class LLMConnectionControlBox : UserControl
	{
		public bool IsPing { get; private set; } = false;

		public int PingTimeout { get; set; } = Net.DEFAULT_TIMEOUT;

		public bool IsConnected { get; private set; } = false;

		public int ConnectingTimeout { get; set; } = Net.DEFAULT_TIMEOUT;

		public bool IsError { get; private set; } = false;

		public Uri Uri
		{
			get
			{
				if (Uri.TryCreate(Address, UriKind.Absolute, out Uri uri))
				{
					return uri;
				}
				else
				{
					return null;
				}
			}
		}
		
		public string Address
		{
			get => serverAddressTextBox.Text;
			set => serverAddressTextBox.Text = value;
		}

		public string Text
		{
			get => serverAddressLabel.Text;
			set => serverAddressLabel.Text = value;
		}

		private StateEnum state = StateEnum.OK;

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> StateChanged;
		protected virtual void OnStateChanged(ConnectionEventArgs e) => StateChanged?.Invoke(this, e);

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> PingChanged;
		protected virtual void OnPingChanged(ConnectionEventArgs e) => PingChanged?.Invoke(this, e);

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> Connecting;
		protected virtual void OnConnecting(ConnectionEventArgs e) => Connecting?.Invoke(this, e);

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ConnectionEventArgs> Connected;
		protected virtual void OnConnected(ConnectionEventArgs e) => Connected?.Invoke(this, e);

		public LLMConnectionControlBox()
		{
			InitializeComponent();
		}

		protected override void OnResize(EventArgs e)
		{
			Resize();
			base.OnResize(e);
		}

		private void AddressLabel_Resize(object sender, EventArgs e)
		{
			Resize();
		}

		private void Resize()
		{
			serverAddressLabel.Location = new Point(0, 2);
			serverAddressTextBox.Location = new Point(serverAddressLabel.Width + 4, 0);
			serverAddressTextBox.Width = Width - serverAddressLabel.Width - 4;
			this.Height = serverAddressTextBox.Height * 2 + 4;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + serverAddressTextBox.Height, e.ClipRectangle.Width, serverAddressTextBox.Height);
			Rectangle iconRect = new Rectangle(rectangle.X + rectangle.Width - 32, rectangle.Y + 2, 16, 16);

			graphics.Clear(BackColor);
			string message = string.Empty;

			if (state == StateEnum.OK)
			{

			}
			else if (state == (StateEnum.Ping | StateEnum.OK))
			{
				message = "Ping";
			}
			else if (state == (StateEnum.Ping | StateEnum.ERROR))
			{
				message = "Incorrected address";
				graphics.DrawImage(WordHiddenPowers.Properties.Resources.UncorrectedAddress, iconRect);
			}
			else if (state == StateEnum.Connecting)
			{
				message = "Connecting...";
				graphics.DrawImage(WordHiddenPowers.Properties.Resources.Connecting, iconRect);
			}
			else if (state == (StateEnum.Connected | StateEnum.OK))
			{
				message = "Ok!";
				graphics.DrawImage(WordHiddenPowers.Properties.Resources.ConnectionOk, iconRect);
			}
			else if (state == (StateEnum.Connected | StateEnum.ERROR))
			{
				message = "Connection error";
				graphics.DrawImage(WordHiddenPowers.Properties.Resources.ConnectionError, iconRect);
			}
			else
			{
				message = "Error!";
				graphics.DrawImage(WordHiddenPowers.Properties.Resources.UncorrectedAddress, iconRect);
			}

			if (!string.IsNullOrEmpty(message))
			{
				Size messageSize = graphics.MeasureString(message, Font).ToSize();
				Rectangle messageRect = new Rectangle(rectangle.X + rectangle.Width - 40 - messageSize.Width, rectangle.Y + 2, messageSize.Width + 4, messageSize.Height);
				graphics.DrawString(message, Font, new SolidBrush(ForeColor), messageRect);
			}
			base.OnPaint(e);
		}

		public void ConnectionCheck()
		{
			textChangedTimer.Start();
		}

		private void ConnectionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			ConnectingArgument arg = (ConnectingArgument)e.Argument;

			worker.ReportProgress(0, new State { Uri = arg.Uri, ConnectionState = StateEnum.Checked | StateEnum.OK });

			bool isPing = Net.CheckHostByPing(arg.Uri.Host, arg.PingTimeout);
			if (isPing)
			{
				worker.ReportProgress(0, new State { Uri = arg.Uri, ConnectionState = StateEnum.Ping | StateEnum.OK });
			}
			else
			{
				e.Result = new State { Uri = arg.Uri, ConnectionState = StateEnum.Ping | StateEnum.ERROR };
				return;
			}

			worker.ReportProgress(0, new State { Uri = arg.Uri, ConnectionState = StateEnum.Connecting | StateEnum.OK });

			bool isCheck = Net.CheckHostByHttp(arg.Uri, arg.ConnectingTimeout);

			if (isCheck)
			{
				e.Result = new State { Uri = arg.Uri, ConnectionState = StateEnum.Connected | StateEnum.OK };
			}
			else
			{
				e.Result = new State { Uri = arg.Uri, ConnectionState = StateEnum.Connected | StateEnum.ERROR };
			}
		}

		private void ConnectionBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			OnEvent((State)e.UserState);
		}

		private void ConnectionBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled == true)
			{
			}
			else if (e.Error != null)
			{
			}
			else
			{
				OnEvent((State)e.Result);
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

		private void ServerAddressTextBox_TextChanged(object sender, EventArgs e)
		{
			ConnectionCheck();
		}

		private string oldServerAddress;
		private void TextChangedTimer_Tick(object sender, EventArgs e)
		{
			Timer timer = sender as Timer;

			if (Address != oldServerAddress)
			{
				oldServerAddress = Address;
			}
			else if (Uri.TryCreate(oldServerAddress, UriKind.Absolute, out Uri uri))
			{
				if (!connectionBackgroundWorker.IsBusy)
				{
					timer.Stop();
					connectionBackgroundWorker.RunWorkerAsync(new ConnectingArgument()
					{
						Uri = uri,
						ConnectingTimeout = ConnectingTimeout,
						PingTimeout = PingTimeout
					});
				}
			}
		}

		private struct ConnectingArgument
		{
			public Uri Uri;
			public int PingTimeout;
			public int ConnectingTimeout;
		}

		private struct State
		{
			public Uri Uri;

			public StateEnum ConnectionState;
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

		public class ConnectionEventArgs : EventArgs
		{
			public ConnectionEventArgs(Uri uri, StateEnum state)
			{
				Uri = uri;
				State = state;
			}

			public Uri Uri { get; }

			public StateEnum State { get; }
		}		
	}
}
