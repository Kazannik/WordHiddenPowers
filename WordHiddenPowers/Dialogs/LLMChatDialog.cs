// Ignore Spelling: Prosecutorial Dialogs

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMChatDialog : Form
	{
		private bool isDragging = false;
		private Point lastCursorPosition;
		private Size textSize;

		public string UserMessage => messageTextBox.Text;

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<UserMessageEventArgs> SendUserMessage;

		protected virtual void OnSendUserMessage(UserMessageEventArgs e)
		{
			SendUserMessage?.Invoke(this, e);
		}

		private void DoSendUserMessage()
		{
			OnSendUserMessage(new UserMessageEventArgs(UserMessage));
		}

		public LLMChatDialog()
		{
			InitializeComponent();
		}

		private void Dialog_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true;
				lastCursorPosition = e.Location;
				Opacity = 0.4;
			}
		}

		private void Dialog_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDragging)
			{
				Left += e.X - lastCursorPosition.X;
				Top += e.Y - lastCursorPosition.Y;
			}
		}

		private void Dialog_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = false;
				Opacity = 0.8;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			if (textSize.Width + 8 > Width) Width = textSize.Width + 8;
			Height = textSize.Height * 2 + 8 * 2;
			closeButton.Size = new Size(textSize.Height + 3, textSize.Height + 3);
			closeButton.Location = new Point(Width - closeButton.Width - 3, 2);
			messageTextBox.Location = new Point(2, Height - messageTextBox.Height - 2);
			sendButton.Location = new Point(Width - sendButton.Width - 2, Height - sendButton.Height - 2);
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(LLMChatDialog));
			Graphics graphics = e.Graphics;
			graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(e.ClipRectangle.Location, Size.Subtract(e.ClipRectangle.Size, new Size(1,1))));
			Size size = graphics.MeasureString(Text, Font, Width).ToSize();
			if (textSize != size)
			{
				textSize = size;
				OnResize(e);
			}
			graphics.FillRectangle(SystemBrushes.ActiveCaption, new Rectangle(Point.Add(e.ClipRectangle.Location, new Size(2, 2)), new Size(e.ClipRectangle.Width - 10 - textSize.Height, textSize.Height + 4)));
			graphics.DrawImage((Image)resources.GetObject("LLM_48"), new Rectangle(2,2, textSize.Height + 4, textSize.Height + 4));

			graphics.DrawString(Text, Font, SystemBrushes.ActiveCaptionText, textSize.Height + 6, 4);

			base.OnPaint(e);
		}
		
		private void SendButton_Click(object sender, EventArgs e)
		{
			DoSendUserMessage();
			messageTextBox.Text = string.Empty;
		}
		
		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		

		public class UserMessageEventArgs : EventArgs
		{
			public string UserMessage { get; }

			public UserMessageEventArgs(string userMessage)
			{
				UserMessage = userMessage;
			}
		}
	}
}
