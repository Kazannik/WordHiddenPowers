// Ignore Spelling: Prosecutorial

using System.Drawing;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMChatStatusDialog : Form
	{
		private bool isDragging = false;
		private Point lastCursorPosition;

		public string Status
		{
			get => statusLabel.Text;
			set =>statusLabel.Text = value;
		}

		public LLMChatStatusDialog()
		{
			InitializeComponent();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true;
				lastCursorPosition = e.Location;
				Opacity = 0.4;
			}
			base.OnMouseDown(e);
		}
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (isDragging)
			{
				Left += e.X - lastCursorPosition.X;
				Top += e.Y - lastCursorPosition.Y;
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = false;
				Opacity = 0.8;
			}
			base.OnMouseUp(e);
		}		
	}
}
