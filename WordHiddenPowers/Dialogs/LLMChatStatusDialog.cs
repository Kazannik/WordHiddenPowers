// Ignore Spelling: Prosecutorial

using System.Drawing;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMChatStatusDialog : Form
	{
		private bool isDragging = false;
		private Point lastCursorPosition;
		private Size textSize;

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

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(e.ClipRectangle.Location, Size.Subtract(e.ClipRectangle.Size, new Size(1, 1))));
			Size size = graphics.MeasureString(Text, Font, Width).ToSize();
			if (textSize != size)
			{
				textSize = size;
				OnResize(e);
			}
			graphics.FillRectangle(SystemBrushes.ActiveCaption, new Rectangle(Point.Add(e.ClipRectangle.Location, new Size(2, 2)), new Size(e.ClipRectangle.Width - 4, textSize.Height + 4)));
			graphics.DrawString(Text, Font, SystemBrushes.ActiveCaptionText, 4, 4);
			base.OnPaint(e);
		}
	}
}
