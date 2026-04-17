// Ignore Spelling: Utils

using ControlLibrary.Controls.ListControls;
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

		public static void DrawRoundedText(string text, StringFormat stringFormat, int radius, DrawItemEventArgs e)
		{
			Brush ForeColorBrush = new SolidBrush(e.BackColor);
			Brush BackColorBrush = new SolidBrush(e.ForeColor);
			Pen BackColorPen = new Pen(e.ForeColor, 1);
			ControlLibrary.Utils.Drawing.FillRoundedRectangle(e.Graphics, BackColorBrush, e.Bounds, radius);
			e.Graphics.DrawString(text, e.Font, ForeColorBrush, e.Bounds, stringFormat);
			ControlLibrary.Utils.Drawing.DrawRoundedRectangle(e.Graphics, BackColorPen, e.Bounds, radius);
			ForeColorBrush.Dispose();
			BackColorBrush.Dispose();
			BackColorPen.Dispose();
		}

		public static void DrawCode(Version code, DrawItemEventArgs e)
		{
			DrawRoundedText(code.ToString(), CENTER_STRING_FORMAT, (e.Bounds.Height / 3) == 0 ? 1 : (e.Bounds.Height / 3), e);
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

		public static void DrawLine(DrawItemEventArgs e, IListItemNote[] notes)
		{
			// Рисование линии после титульной части
			Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(e.ForeColor) : SystemPens.InactiveCaption;
			e.Graphics.DrawLine(linePen,
			e.Bounds.X + 7, e.Bounds.Y + notes[0].Size.Height + notes[1].Size.Height - 4,
			e.Bounds.X + e.Bounds.Width - 10, e.Bounds.Y + notes[0].Size.Height + notes[1].Size.Height - 4);
		}

		public static void DrawLine(DrawItemEventArgs e, IListItemNote note)
		{
			// Рисование линии после титульной части
			Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(e.ForeColor) : SystemPens.InactiveCaption;
			e.Graphics.DrawLine(linePen,
			e.Bounds.X + 7, e.Bounds.Y + note.Size.Height - 2,
			e.Bounds.X + e.Bounds.Width - 10, e.Bounds.Y + note.Size.Height - 2);
		}
	}
}
