using ControlLibrary.Controls.ListControls;
using ControlLibrary.Structures;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Notes;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{	
	namespace ListItems
	{
		public class ListItem : ControlLibrary.Controls.ListControls.ListItem
		{
			public ListItem() : base()
			{
				Note = default;
			}

			public ListItem(Note note, bool showButtons) : base(
				new ListItemNote[] {
				new TitleNote(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid, note.Category.Text),
				new SubtitleNote(note),
				new TextNote(note),
				new DescriptionNote(note),
				new BottomBarNote(note) {ShowButtons = showButtons } })
			{
				Note = note;
			}

			public Note Note { get; }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				e.DrawBackground();
				base.OnDraw(e);
				Utils.Drawing.DrawLine(e, notes);
				e.DrawFocusRectangle();
			}

			public bool ShowButtons
			{
				get => ((BottomBarNote)notes.Last()).ShowButtons;
				set => ((BottomBarNote)notes.Last()).ShowButtons = value;
			}

			public Rating Rating
			{
				get => ((BottomBarNote)notes.Last()).Rating;
				set => ((BottomBarNote)notes.Last()).Rating = value;
			}
		}

		public abstract class ListItemNote : ControlLibrary.Controls.ListControls.ListItemNote
		{
			protected static readonly StringFormat CENTER_STRING_FORMAT = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			protected static readonly StringFormat LEFT_STRING_FORMAT = new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Near
			};

			private string text;

			public ListItemNote(string text) => this.text = text;

			public string Text
			{
				get => text;
				set
				{
					if (text != value)
					{
						text = value;
						DoContentChanged();
					}
				}
			}
		}

		public class TitleNote : ListItemNote
		{
			private Version code;
			private Size codeSize;
			private Size textSize;

			public TitleNote(int major, int minor, string guid, string text) : base(text: text)
			{
				code = Version.Create(major: major, minor: minor, guid: guid);
				codeSize = Size.Empty;
				textSize = Size.Empty;
			}

			public Version Code
			{
				get => code;
				set
				{
					if (code != value)
					{
						code = value;
						DoContentChanged();
					}
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
				Rectangle codeRectangle = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2,
					codeSize.Width, codeSize.Height);
				Utils.Drawing.DrawCode(Code, new DrawItemEventArgs(e.Graphics, boldFont, codeRectangle, e.Index, e.State, e.ForeColor, e.BackColor));

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle rectangle = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y, textSize.Width, textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, rectangle, LEFT_STRING_FORMAT);
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				codeSize = Utils.Drawing.GetCodeSize(graphics, boldFont);
				textSize = GetTextSize(graphics, Text, font, itemWidth - codeSize.Width - 4, CENTER_STRING_FORMAT);

				if (codeSize.Height < textSize.Height)
				{
					return new Size(itemWidth, textSize.Height + 2);
				}
				else
				{
					return new Size(itemWidth, codeSize.Height + 2);
				}
			}
		}

		public class SubtitleNote : ListItemNote
		{
			private Size codeSize;
			private Size textSize;

			public SubtitleNote(Note note) : base(text: note.Subcategory.Text)
			{
				codeSize = Size.Empty;
				textSize = Size.Empty;
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldItalicFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold | FontStyle.Italic);
				Brush brush = new SolidBrush(e.State == (e.State | DrawItemState.Selected) ? e.ForeColor : Color.DarkGreen);
				Rectangle rectangle = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y,
					textSize.Width, textSize.Height);
				e.Graphics.DrawString(Text, boldItalicFont, brush, rectangle, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldItalicFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic);
				codeSize = Utils.Drawing.GetCodeSize(graphics, boldItalicFont);
				textSize = GetTextSize(graphics, Text, font, itemWidth - codeSize.Width - 4, CENTER_STRING_FORMAT);
				return new Size(itemWidth, textSize.Height + 8);
			}
		}

		public class TextNote : ListItemNote
		{
			private readonly bool IsText;

			public TextNote(Note note) : base(text: note.Value.ToString())
			{
				IsText = note.IsText;
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldLargeFont = new Font(e.Font.FontFamily, e.Font.Size + 4, FontStyle.Bold);

				Brush brush = new SolidBrush(e.ForeColor);
				Utils.Drawing.DrawRoundedText(
					Text,
					IsText ? LEFT_STRING_FORMAT : CENTER_STRING_FORMAT,
					4,
					new DrawItemEventArgs(e.Graphics, IsText ? e.Font : boldLargeFont, new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), e.Index, e.State, Color.FromArgb(alpha: 250, red: 250, green: 250, blue: 255), e.ForeColor));
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldLargeFont = new Font(font.FontFamily, font.Size + 4, FontStyle.Bold);
				return GetTextSize(graphics: graphics, Text, font: IsText ? font : boldLargeFont, width: itemWidth - 5, IsText ? LEFT_STRING_FORMAT : CENTER_STRING_FORMAT);
			}
		}

		public class DescriptionNote : ListItemNote
		{
			public DescriptionNote(Note note) : base(text: note.Description) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font italicSmallFont = new Font(e.Font.FontFamily, e.Font.Size - 1, FontStyle.Italic);
				Brush brush = new SolidBrush(e.State == (e.State | DrawItemState.Selected) ? e.ForeColor : Color.DarkGreen);
				e.Graphics.DrawString(Text, italicSmallFont, brush, new Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font italicSmallFont = new Font(font.FontFamily, font.Size - 1, FontStyle.Italic);
				return GetTextSize(graphics: graphics, Text, font: italicSmallFont, width: itemWidth, LEFT_STRING_FORMAT) + new Size(0, 2);
			}
		}

		public class BottomBarNote : ListItemNote
		{
			private readonly Note note;
			protected Size ratingBoxSize;

			private bool showButtons;

			public Rectangle AdditionButtonRectangle { get; private set; }
			public Rectangle SubtractionButtonRectangle { get; private set; }
			public Rectangle ApplyButtonRectangle { get; private set; }
			public Rectangle CancelButtonRectangle { get; private set; }

			public BottomBarNote(Note note) : base(text: string.Empty)
			{
				this.note = note;
				AdditionButtonRectangle = Rectangle.Empty;
				SubtractionButtonRectangle = Rectangle.Empty;
				ApplyButtonRectangle = Rectangle.Empty;
				CancelButtonRectangle = Rectangle.Empty;
			}

			public Rating Rating
			{
				get => (Rating)note.Rating;
				set
				{
					if (note.Rating != value.Value)
					{
						note.Rating = value.Value;
						DoContentChanged();
					}
				}
			}

			public bool ShowButtons
			{
				get => showButtons;
				set
				{
					if (showButtons != value)
					{
						showButtons = value;
						DoContentChanged();
					}
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				if (note.IsText)
				{
					Color starsColor = e.ForeColor;
					if (e.State == (e.State | DrawItemState.Selected) || note.Rating == 0)
					{
					}
					else if (note.Rating < 0)
					{
						starsColor = Const.Globals.COLOR_NEGATIVE_STAR_ICON;
					}
					else if (note.Rating > 0)
					{
						starsColor = Const.Globals.COLOR_STAR_ICON;
					}
					Font boldFont = new Font(e.Font.FontFamily, e.Font.Size + 4, FontStyle.Bold);
					Rectangle[] rectangles = ControlLibrary.Utils.Drawing.DrawRating(graphics: e.Graphics, font: boldFont, borderColor: e.ForeColor, backBrush: new SolidBrush(starsColor), textColor: starsColor, rect: new Rectangle(e.Bounds.Location, ratingBoxSize), rating: note.Rating);
					SubtractionButtonRectangle = rectangles[0];
					AdditionButtonRectangle = rectangles[4];
				}

				if (showButtons)
				{
					ApplyButtonRectangle = new Rectangle(e.Bounds.Width - (int)(e.Bounds.Height * 2.3), e.Bounds.Y,
						e.Bounds.Height - 3, e.Bounds.Height - 3);
					ControlLibrary.Utils.Drawing.DrawOkIcon(e.Graphics, e.State == (e.State | DrawItemState.Selected) ? e.ForeColor : Const.Globals.COLOR_OK_ICON, e.BackColor, ApplyButtonRectangle);

					CancelButtonRectangle = new Rectangle(e.Bounds.Width - e.Bounds.Height - 3, e.Bounds.Y,
						e.Bounds.Height - 3, e.Bounds.Height - 3);
					ControlLibrary.Utils.Drawing.DrawCancelIcon(e.Graphics, e.State == (e.State | DrawItemState.Selected) ? e.ForeColor : Const.Globals.COLOR_CANCEL_ICON, e.BackColor, CancelButtonRectangle);
				}
				else
				{
					ApplyButtonRectangle = Rectangle.Empty;
					CancelButtonRectangle = Rectangle.Empty;
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size + 4, FontStyle.Bold);
				ratingBoxSize = ControlLibrary.Utils.Drawing.MeasureRating(graphics: graphics, font: boldFont, starCount: 5);
				return new Size(itemWidth, note.IsText || showButtons ? ratingBoxSize.Height + 4 : 4);
			}
		}
	}
}
