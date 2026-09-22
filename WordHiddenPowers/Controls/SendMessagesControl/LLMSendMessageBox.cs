using System;
using System.ComponentModel;
using System.Windows.Forms;
using WordHiddenPowers.Documents;
using WordHiddenPowers.EventsBus;

namespace WordHiddenPowers.Controls.SendMessagesControl
{
	public partial class LLMSendMessageBox : UserControl
	{
		public Document Document { get; set; }

		private const int BUTTON_LARGE_WIDTH = 130;
		private const string SEND_BUTTON_TEXT = "Отправить";
		private const string REPEAT_BUTTON_TEXT = "Повторить";
		private const string UNDO_BUTTON_TEXT = "Отменить";
		private const string REDO_BUTTON_TEXT = "Вернуть";

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertHereMessage;
		protected virtual void OnClickInsertHereMessage(EventArgs e) => ClickInsertHereMessage?.Invoke(this, e);
		private void InsertHereButton_Click(object sender, EventArgs e) => OnClickInsertHereMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickReplaceSelectionMessage;
		protected virtual void OnClickReplaceSelectionMessage(EventArgs e) => ClickReplaceSelectionMessage?.Invoke(this, e);
		private void ReplaceSelectionButton_Click(object sender, EventArgs e) => OnClickReplaceSelectionMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertNextMessage;
		protected virtual void OnClickInsertNextMessage(EventArgs e) => ClickInsertNextMessage?.Invoke(this, e);
		private void InsertNextButton_Click(object sender, EventArgs e) => OnClickInsertNextMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertPreviousMessage;
		protected virtual void OnClickInsertPreviousMessage(EventArgs e) => ClickInsertPreviousMessage?.Invoke(this, e);
		private void InsertPreviousButton_Click(object sender, EventArgs e) => OnClickInsertPreviousMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertCenterMessage;
		protected virtual void OnClickInsertCenterMessage(EventArgs e) => ClickInsertCenterMessage?.Invoke(this, e);
		private void InsertCenterButton_Click(object sender, EventArgs e) => OnClickInsertCenterMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickRepeatMessage;
		protected virtual void OnClickRepeatMessage(EventArgs e) => ClickRepeatMessage?.Invoke(this, e);
		private void RepeatButton_Click(object sender, EventArgs e) => OnClickRepeatMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickRedoMessage;
		protected virtual void OnClickRedoMessage(EventArgs e) => ClickRedoMessage?.Invoke(this, e);
		private void RedoButton_Click(object sender, EventArgs e) => OnClickRedoMessage(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickUndoMessage;
		protected virtual void OnClickUndoMessage(EventArgs e) => ClickUndoMessage?.Invoke(this, e);
		private void UndoButton_Click(object sender, EventArgs e) => OnClickUndoMessage(e);


		public LLMSendMessageBox()
		{
			InitializeComponent();

			GlobalsEventsBus.DocumentChatMessageModeChanged += new EventHandler<EventsBus.EventArgs.DocumentChatMessageModeEventArgs>(GlobalsEventsBus_DocumentChatMessageModeChanged);
			GlobalsEventsBus.AccessToSendingUserMessageStateChanged += new EventHandler<EventsBus.EventArgs.AccessToSendingUserMessageEventArgs>(GlobalsEventsBus_AccessToSendingUserMessageStateChanged);		

			StateComponent();

			this.insertHereButton.Text = SEND_BUTTON_TEXT;
			this.replaceSelectionButton.Text = SEND_BUTTON_TEXT;
			this.insertNextButton.Text = SEND_BUTTON_TEXT;
			this.insertPreviousButton.Text = SEND_BUTTON_TEXT;
			this.insertBetweenButton.Text = SEND_BUTTON_TEXT;
			this.repeatButton.Text = REPEAT_BUTTON_TEXT;
			this.undoButton.Text = UNDO_BUTTON_TEXT;
			this.redoButton.Text = REDO_BUTTON_TEXT;
		}

		private void GlobalsEventsBus_AccessToSendingUserMessageStateChanged(object sender, EventsBus.EventArgs.AccessToSendingUserMessageEventArgs e)
		{
			AccessUserMessage = e.IsAccess;
		}
				
		private void GlobalsEventsBus_DocumentChatMessageModeChanged(object sender, EventsBus.EventArgs.DocumentChatMessageModeEventArgs e)
		{
			if (Document?.Hwnd != e.Document.Hwnd) return;

			ChatMessageMode = e.MessageMode;
		}

		protected override void OnResize(EventArgs e)
		{
			StateComponent();
			base.OnResize(e);
		}

		private Document.ChatMessageModeEnum chatMessageMode = Document.ChatMessageModeEnum.Nothing;

		public Document.ChatMessageModeEnum ChatMessageMode
		{
			get => chatMessageMode;
			set
			{
				if (chatMessageMode != value)
				{
					chatMessageMode = value;
					StateComponent();
				}
			}
		}

		private bool accessUserMessage = false;

		public bool AccessUserMessage
		{
			get => accessUserMessage;
			set
			{
				if (accessUserMessage != value)
				{
					accessUserMessage = value;
					StateComponent();
				}
			}
		}

		private void StateComponent()
		{
			if (this.toolStripBar.InvokeRequired)
			{
				this.toolStripBar.Invoke(new Action(StateComponent));
			}
			else
			{
				if (Height != toolStripBar.Height)
					Height = toolStripBar.Height;

				insertHereButton.Visible =
					replaceSelectionButton.Visible =
					insertNextButton.Visible =
					insertPreviousButton.Visible =
					insertBetweenButton.Visible =
					repeatButton.Visible =
					redoButton.Visible =
					undoButton.Visible = false;

				SetButtonsEnabled();

				if (chatMessageMode == Document.ChatMessageModeEnum.Nothing)
				{
					insertHereButton.Visible = true;
					return;
				}

				insertHereButton.Visible = chatMessageMode == Document.ChatMessageModeEnum.InsertHere;
				replaceSelectionButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.ReplaceSelection);
				insertNextButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Next);
				insertPreviousButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Previous);
				insertBetweenButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Between);

				repeatButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Repeat);
				repeatButton.AutoSize = false;
				repeatButton.Width = BUTTON_LARGE_WIDTH;

				undoButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Undo);

				redoButton.Visible = chatMessageMode.HasFlag(Document.ChatMessageModeEnum.Redo);
				redoButton.AutoSize = false;
				redoButton.Width = BUTTON_LARGE_WIDTH;
			}
		}

		public void SetButtonsEnabled() => SetButtonsEnabled(AccessUserMessage);

		public void SetButtonsEnabled(bool enabled)
		{
			if (this.toolStripBar.InvokeRequired)
			{
				this.toolStripBar.Invoke(new Action(() => SetButtonsEnabled(enabled)));
			}
			else
			{
				insertHereButton.Enabled =
				replaceSelectionButton.Enabled =
				insertNextButton.Enabled =
				insertPreviousButton.Enabled =
				insertBetweenButton.Enabled =
				repeatButton.Enabled =
				undoButton.Enabled =
				redoButton.Enabled = enabled;
			}
		}
		
		public void SetUndoButtonsEnabled(bool enabled)
		{
			if (this.toolStripBar.InvokeRequired)
			{
				this.toolStripBar.Invoke(new Action(() => SetUndoButtonsEnabled(enabled)));
			}
			else
			{
				undoButton.Enabled = enabled;
			}
		}
	}
}
