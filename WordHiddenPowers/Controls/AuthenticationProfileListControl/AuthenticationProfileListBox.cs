using LLMConnectorLibrary.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem;

namespace WordHiddenPowers.Controls.AuthenticationProfileListControl
{
	public class AuthenticationProfileListBox : UserControl, IEnumerable<IAuthenticationProfile>
	{
		internal const int TOP_POSITION = 0;
		internal const int STEP_POSITION = 0;

		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public AuthenticationProfileListBox(IContainer components)
		{
			this.components = components;
			InitializeComponent();
		}

		public AuthenticationProfileListBox()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			SuspendLayout();
			// 
			// AuthenticationProfileListBox
			// 
			AutoScroll = true;
			BackColor = SystemColors.Window;
			Name = "AuthenticationProfileListBox";
			Size = new Size(333, 150);
			ResumeLayout(false);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			ItemsResize();
		}

		protected override void OnResize(EventArgs e)
		{
			ItemsResize();
			base.OnResize(e);
		}

		public IAuthenticationProfile this[int index] => ((AuthenticationProfileListItem)Controls[index: index]).Profile;

		private int GetPosition()
		{
			if (Controls.Count == 0)
				return TOP_POSITION;
			else
			{
				Control item = Controls[Controls.Count - 1];
				return item.Location.Y + item.Height + STEP_POSITION;
			}
		}

		public void Add() => AddItem(new AuthenticationProfileListItem(parent: this));

		public void Add(IAuthenticationProfile profile) => AddItem(new AuthenticationProfileListItem(parent: this, profile));

		public void AddRange(IEnumerable<IAuthenticationProfile> profiles)
		{
			foreach (IAuthenticationProfile profile in profiles)
			{
				Add(profile);
			}
		}

		private void AddItem(AuthenticationProfileListItem item)
		{
			item.Location = new Point(0, GetPosition());

			item.StateChanged += new EventHandler<ItemConnectionEventArgs>(Item_StateChanged);
			item.PingChanged += new EventHandler<ItemConnectionEventArgs>(Item_PingChanged);
			item.Connecting += new EventHandler<ItemConnectionEventArgs>(Item_Connecting);
			item.Connected += new EventHandler<ItemConnectionEventArgs>(Item_Connected);
			item.ProfileChanged += new EventHandler<ItemEventArgs>(Item_ProfileChanged);
			item.ProfileCaptionChanged += new EventHandler<ItemEventArgs>(Item_ProfileNameChanged);

			Controls.Add(item);
			item.BackColor = item.Index % 2 == 0 ? SystemColors.Window : SystemColors.ControlLight;
		}

		public void RemoveAt(int index)
		{
			AuthenticationProfileListItem item = Controls[index] as AuthenticationProfileListItem;

			item.StateChanged -= Item_StateChanged;
			item.PingChanged -= Item_PingChanged;
			item.Connecting -= Item_Connecting;
			item.Connected -= Item_Connected;
			item.ProfileChanged -= Item_ProfileChanged;

			Controls.RemoveAt(index);
			ItemsResize();
			OnItemConnected(new ItemConnectionEventArgs(item, ConnectionControlBox.StateEnum.Connected | ConnectionControlBox.StateEnum.ERROR));
			CheckConnection();
		}

		public void CheckConnection()
		{
			foreach (AuthenticationProfileListItem item in from AuthenticationProfileListItem item
														   in Controls
														   where !item.IsDouble
														   select item)
			{
				item.CheckConnection();
			}
		}

		public IEnumerable<IAuthenticationProfile> GetProfiles()
		{
			return from AuthenticationProfileListItem item
				   in Controls
				   where !item.IsDouble
				   select item.Profile;
		}

		internal void ItemsResize()
		{
			for (int i = 0; i < Controls.Count; i++)
			{
				AuthenticationProfileListItem item = Controls[i] as AuthenticationProfileListItem;
				item.Location = new Point(0, item.Top - VerticalScroll.Value);
				item.ResizeComponents();
				item.BackColor = i % 2 == 0 ? SystemColors.Window : SystemColors.ControlLight;
			}
		}

		public IEnumerator<IAuthenticationProfile> GetEnumerator() => new AuthenticationProfileEnum(Controls);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ItemProfileNameChanged;
		protected virtual void OnItemProfileNameChanged(ItemEventArgs e) => ItemProfileNameChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ItemProfileChanged;
		protected virtual void OnItemProfileChanged(ItemEventArgs e) => ItemProfileChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> ItemStateChanged;
		protected virtual void OnItemStateChanged(ItemConnectionEventArgs e) => ItemStateChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> ItemPingChanged;
		protected virtual void OnItemPingChanged(ItemConnectionEventArgs e) => ItemPingChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> ItemConnecting;
		protected virtual void OnItemConnecting(ItemConnectionEventArgs e) => ItemConnecting?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> ItemConnected;
		protected virtual void OnItemConnected(ItemConnectionEventArgs e) => ItemConnected?.Invoke(this, e);

		private void Item_StateChanged(object sender, ItemConnectionEventArgs e) => OnItemStateChanged(e);

		private void Item_ProfileChanged(object sender, ItemEventArgs e) => OnItemProfileChanged(e);

		private void Item_ProfileNameChanged(object sender, ItemEventArgs e) => OnItemProfileNameChanged(e);

		private void Item_PingChanged(object sender, ItemConnectionEventArgs e) => OnItemPingChanged(e);

		private void Item_Connecting(object sender, ItemConnectionEventArgs e) => OnItemConnecting(e);

		private void Item_Connected(object sender, ItemConnectionEventArgs e) => OnItemConnected(e);

		public class AuthenticationProfileEnum(Control.ControlCollection controls) : IEnumerator<IAuthenticationProfile>, IEnumerator
		{
			private readonly ControlCollection controls = controls;

			int position = -1;

			public bool MoveNext()
			{
				position++;
				return position < controls.Count;
			}

			public void Reset()
			{
				position = -1;
			}

			public void Dispose()
			{
				//throw new NotImplementedException();
			}

			object IEnumerator.Current => Current;

			IAuthenticationProfile IEnumerator<IAuthenticationProfile>.Current => Current;

			public IAuthenticationProfile Current
			{
				get
				{
					try
					{
						return ((AuthenticationProfileListItem)controls[position]).Profile;
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidOperationException();
					}
				}
			}
		}
	}
}
