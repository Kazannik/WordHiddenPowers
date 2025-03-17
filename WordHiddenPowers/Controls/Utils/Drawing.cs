using System.Drawing;
using System.Windows.Forms;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.Utils
{
    public static class Drawing
    {
		public static readonly StringFormat CENTER_STRING_FORMAT = new StringFormat
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center
		};

		public static readonly StringFormat LEFT_STRING_FORMAT = new StringFormat
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Near
		};

		public static void DrawCode(Version code, DrawItemEventArgs e)
		{
			Brush ForeColorBrush = new SolidBrush(e.BackColor);
			Brush BackColorBrush = new SolidBrush(e.ForeColor);
			Pen BackColorPen = new Pen(e.ForeColor, 1);
			ControlLibrary.Utils.Drawing.FillRoundedRectangle(e.Graphics, BackColorBrush, e.Bounds, (e.Bounds.Height / 3) == 0 ? 1 : (e.Bounds.Height / 3));
			e.Graphics.DrawString(code.ToString(), e.Font, ForeColorBrush, e.Bounds, CENTER_STRING_FORMAT);
			ControlLibrary.Utils.Drawing.DrawRoundedRectangle(e.Graphics, BackColorPen, e.Bounds, (e.Bounds.Height / 3) == 0 ? 1 : (e.Bounds.Height / 3));
			ForeColorBrush.Dispose();
			BackColorBrush.Dispose();
			BackColorPen.Dispose();
		}

		public static Size GetCodeSize(Graphics graphics, Font font)
		{
			SizeF measure = graphics.MeasureString("XX.XX", font);
			return new Size((int)measure.Width, (int)measure.Height);
		}

		public static Size GetTextSize(Graphics graphics, string text, Font font, int width, StringFormat stringFormat)
		{
			if (!string.IsNullOrEmpty(text))
			{
				SizeF measure = graphics.MeasureString(text, font, width, stringFormat);
				return new Size(width, (int)measure.Height);
			}
			else
			{
				return Size.Empty;
			}
		}
	}
}
