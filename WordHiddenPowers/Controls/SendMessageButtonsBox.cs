using ControlLibrary.Controls.ListControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
	public partial class SendMessageButtonsBox : UserControl
	{
		private static readonly Size SMALL_BUTTON_SIZE = new Size(48, 48);
		private static readonly Size LARGE_BUTTON_SIZE = new Size(132, 48);
		private const string BUTTON_TEXT = "Отправить";

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertMessage;
		protected virtual void OnClickInsertMessage(EventArgs e) => ClickInsertMessage?.Invoke(this, e);

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickReplaceMessage;
		protected virtual void OnClickReplaceMessage(EventArgs e) => ClickReplaceMessage?.Invoke(this, e);
		
		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertNextMessage;
		protected virtual void OnClickInsertNextMessage(EventArgs e) => ClickInsertNextMessage?.Invoke(this, e);
		
		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertPreviousMessage;
		protected virtual void OnClickInsertPreviousMessage(EventArgs e) => ClickInsertPreviousMessage?.Invoke(this, e);
		
		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickInsertCenterMessage;
		protected virtual void OnClickInsertCenterMessage(EventArgs e) => ClickInsertCenterMessage?.Invoke(this, e);


		public SendMessageButtonsBox()
		{
			InitializeComponent();
			ResizeComponent();
		}

		protected override void OnResize(EventArgs e)
		{
			ResizeComponent();
			base.OnResize(e);
		}

		Documents.DocumentCollection.ChartMessageMode messageMode = Documents.DocumentCollection.ChartMessageMode.Nothing;

		public Documents.DocumentCollection.ChartMessageMode MessageMode
		{
			get => messageMode;
			set
			{
				if (messageMode != value)
				{
					messageMode = value;
					ResizeComponent();
				}
			}
		}

		private void ResizeComponent()
		{
			Height = LARGE_BUTTON_SIZE.Height;

			insertButton.Size = LARGE_BUTTON_SIZE;
			replaceButton.Size = LARGE_BUTTON_SIZE;
			insertNextButton.Size = SMALL_BUTTON_SIZE;
			insertPreviousButton.Size = SMALL_BUTTON_SIZE;
			insertCenterButton.Size = SMALL_BUTTON_SIZE;

			insertButton.Visible =
				replaceButton.Visible =
				insertNextButton.Visible =
				insertPreviousButton.Visible =
				insertCenterButton.Visible = false;

			int left = Width - 2;

			if (messageMode == Documents.DocumentCollection.ChartMessageMode.Nothing)
			{
				left -= LARGE_BUTTON_SIZE.Width;
				ButtonToLarge(insertButton);
				insertButton.Location = new Point(left, 0);
				insertButton.Visible = left >= 0;
				return;
			}

			if (messageMode == (Documents.DocumentCollection.ChartMessageMode.Insert))
			{
				left -= LARGE_BUTTON_SIZE.Width;
				ButtonToLarge(insertButton);
				insertButton.Location = new Point(left, 0);
				insertButton.Visible = left >= 0;
			}

			if (messageMode.HasFlag(Documents.DocumentCollection.ChartMessageMode.Replace))
			{
				left -= LARGE_BUTTON_SIZE.Width - 2;
				ButtonToLarge(replaceButton);
				replaceButton.Location = new Point(left, 0);
				replaceButton.Visible = left >= 0;
			}

			if (messageMode.HasFlag(Documents.DocumentCollection.ChartMessageMode.Next))
			{
				left -= SMALL_BUTTON_SIZE.Width - 2;
				insertNextButton.Location = new Point(left, 0);
				insertNextButton.Visible = left >= 0;
			}
			
			if (messageMode.HasFlag(Documents.DocumentCollection.ChartMessageMode.Previous))
			{
				left -= SMALL_BUTTON_SIZE.Width - 2;
				insertPreviousButton.Location = new Point(left, 0);
				insertPreviousButton.Visible = left >= 0;
			}

			if (messageMode.HasFlag(Documents.DocumentCollection.ChartMessageMode.Center))
			{
				left -= SMALL_BUTTON_SIZE.Width - 2;
				insertCenterButton.Location = new Point(left, 0);
				insertCenterButton.Visible = left >= 0;
			}			
		}
		
		private void ButtonToSmall(Button button)
		{
			button.Size = SMALL_BUTTON_SIZE;
			button.Text = string.Empty;
			button.TextImageRelation = TextImageRelation.Overlay;
		}

		private void ButtonToLarge(Button button)
		{
			button.Size = LARGE_BUTTON_SIZE;
			button.Text = BUTTON_TEXT;
			button.TextImageRelation = TextImageRelation.ImageBeforeText;
		}

		private void InsertButton_Click(object sender, EventArgs e) => OnClickInsertMessage(e);

		private void ReplaceButton_Click(object sender, EventArgs e) => OnClickReplaceMessage(e);
		
		private void InsertNextButton_Click(object sender, EventArgs e) => OnClickInsertNextMessage(e);
		
		private void InsertPreviousButton_Click(object sender, EventArgs e) => OnClickInsertPreviousMessage(e);
		
		private void InsertCenterButton_Click(object sender, EventArgs e) => OnClickInsertCenterMessage(e);
	}
}
