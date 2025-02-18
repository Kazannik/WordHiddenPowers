using ControlLibrary.Controls.ListControls;
using ControlLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using static WordHiddenPowers.Repositories.RepositoryDataSet;
using Category = WordHiddenPowers.Repositories.Categories.Category;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	public class AnalyzerListBox : ListControl<AnalyzerListControl.ListItem, AnalyzerListControl.ListItemNote>
	{
		private RepositoryDataSet source;

		public AnalyzerListBox() : base() { }

		public event EventHandler<ItemMouseEventArgs<AnalyzerListControl.ListItem, AnalyzerListControl.BottomBarNote>> ItemApplyClick;
		public event EventHandler<ItemMouseEventArgs<AnalyzerListControl.ListItem, AnalyzerListControl.BottomBarNote>> ItemCancelClick;

		public RepositoryDataSet DataSet
		{
			get
			{
				return source;
			}
			set
			{
				if (source != null)
				{
					source.DecimalPowers.DecimalPowersRowChanged -= DecimalPowers_RowChanged;
					source.TextPowers.TextPowersRowChanged -= TextPowers_RowChanged;
					source.DecimalPowers.DecimalPowersRowDeleted -= DecimalPowers_RowChanged;
					source.TextPowers.TextPowersRowDeleted -= TextPowers_RowChanged;
					source.DecimalPowers.TableCleared -= TablesPowers_TableCleared;
					source.TextPowers.TableCleared -= TablesPowers_TableCleared;

					source.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
					source.Subcategories.SubcategoriesRowDeleted -= Subcategories_RowChanged;
					source.Subcategories.TableCleared -= Subcategories_TableCleared;

					source.WordFiles.WordFilesRowChanged -= WordFiles_RowChanged;
					source.WordFiles.WordFilesRowDeleted -= WordFiles_RowChanged;
					source.WordFiles.TableCleared -= WordFiles_TableCleared;
				}
				source = value;

				Items.Clear();

				ReadData();

				if (source != null)
				{
					source.DecimalPowers.DecimalPowersRowChanged += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
					source.TextPowers.TextPowersRowChanged += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
					source.DecimalPowers.DecimalPowersRowDeleted += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
					source.TextPowers.TextPowersRowDeleted += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
					source.DecimalPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
					source.TextPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

					source.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.SubcategoriesRowDeleted += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);

					source.WordFiles.WordFilesRowChanged += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
					source.WordFiles.WordFilesRowDeleted += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
					source.WordFiles.TableCleared += new DataTableClearEventHandler(WordFiles_TableCleared);
				}
			}
		}

		protected override void OnItemMouseClick(ItemMouseEventArgs<AnalyzerListControl.ListItem, AnalyzerListControl.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is AnalyzerListControl.BottomBarNote note)
			{
				if (!note.IsAppled && note.ApplyButton.Contains(e.Location))
				{
					ItemApplyClick?.Invoke(this,
						new ItemMouseEventArgs<AnalyzerListControl.ListItem, AnalyzerListControl.BottomBarNote>(e.Item, note, e.Argument,
						new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)));
				}
				else if (!note.IsAppled && note.CancelButton.Contains(e.Location))
				{
					ItemCancelClick?.Invoke(this,
						new ItemMouseEventArgs<AnalyzerListControl.ListItem, AnalyzerListControl.BottomBarNote>(e.Item, note, e.Argument,
						new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)));
				}
				else
				{
					base.OnItemMouseClick(e);
				}
			}
			else
			{
				base.OnItemMouseClick(e);
			}
		}


		private void WordFiles_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void WordFiles_RowChanged(object sender, WordFilesRowChangeEvent e)
		{
			ReadData();
		}

		private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Subcategories_RowChanged(object sender, SubcategoriesRowChangeEvent e)
		{
			ReadData();
		}

		private void DecimalPowers_RowChanged(object sender, DecimalPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				//AddNote(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) RemoveNote(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Rating = e.Row.Rating;
				note.Value = e.Row.Value;
				//Category category = source.Categories.Get(e.Row.category_id);
				//note.Subcategory = source.Subcategories.Get(category, e.Row.subcategory_id);
				//if (note.Rectangle != null)
				//	Invalidate(note.Rectangle);
			}
		}

		private void TextPowers_RowChanged(object sender, TextPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				//AddNote(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) RemoveNote(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Rating = e.Row.Rating;
				note.Value = e.Row.Value;
				//if (note.Rectangle != null)
				//	Invalidate(note.Rectangle);
			}
		}

		private void TablesPowers_TableCleared(object sender, DataTableClearEventArgs e)
		{
			Items.Clear();
		}

		public void ReadData()
		{
			if (DesignMode || source == null) return;

			BeginUpdate();
					

			if (source.TextPowers.Rows.Count > 0 ||
				source.DecimalPowers.Rows.Count > 0)
			{
				foreach (Note note in source.GetNotes())
				{
					AddNote(note);
				}
			}
			EndUpdate();
		}

		private void AddNote(Note note)
		{
			Items.Add(new AnalyzerListControl.ListItem(note));
		}

		private void RemoveNote(Note note)
		{
			Items.Remove(GetNoteListItem(note.DataRow as DataRow));
		}

		private Note GetNote(DataRow dataRow)
		{
			return (from AnalyzerListControl.ListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item.Note).First();
		}

		private AnalyzerListControl.ListItem GetNoteListItem(DataRow dataRow)
		{
			return (from AnalyzerListControl.ListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item).First();
		}
	}
	
	namespace AnalyzerListControl
	{
		public class ListItem : ControlLibrary.Controls.ListControls.ListItem
		{
			public ListItem() : base()
			{
				Note = default;
			}

			public ListItem(Note note) : base(
				new ListItemNote[] {
				//new TitleNote(note.Category.Guid, note.Subcategory.Id, note.Category.Text),
				new TextNote(note.Value.ToString()),
				new DescriptionNote(note.Description),
				new BottomBarNote() })
			{
				Note = note;
			}

			public Note Note { get; }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				base.OnDraw(e);
				if (notes.Length > 2 && (!notes[1].Size.IsEmpty || !notes[2].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(e.ForeColor) : SystemPens.InactiveCaption;
					e.Graphics.DrawLine(linePen,
					e.Bounds.X + 7, notes[0].Size.Height - 2,
					e.Bounds.X + e.Bounds.Width - 15, notes[0].Size.Height - 2);
				}
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

			public ListItemNote(string text)
			{
				this.text = text;
			}

			public string Text
			{
				get
				{
					return text;
				}
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
				get
				{
					return code;
				}
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
				Rectangle codeRectagle = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2,
					codeSize.Width, codeSize.Height);
				DrawCode(new DrawItemEventArgs(e.Graphics, boldFont, codeRectagle, e.Index, e.State, e.ForeColor, e.BackColor));

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle rectangle = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y,
						textSize.Width, textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, rectangle, LEFT_STRING_FORMAT);
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				SizeF measure = graphics.MeasureString("XXX.XXX", boldFont, itemWidth - 3, CENTER_STRING_FORMAT);

				codeSize = new Size((int)measure.Width, (int)measure.Height);
				textSize = GetTextSize(graphics, Text, font, itemWidth - (int)measure.Width - 4, CENTER_STRING_FORMAT);

				if (codeSize.Height < textSize.Height)
				{
					return new Size(itemWidth, textSize.Height + 8);
				}
				else
				{
					return new Size(itemWidth, codeSize.Height + 8);
				}
			}

			private void DrawCode(DrawItemEventArgs e)
			{
				Brush ForeColorBrush = new SolidBrush(e.BackColor);
				Brush BackColorBrush = new SolidBrush(e.ForeColor);
				Pen BackColorPen = new Pen(e.ForeColor, 1);
				ControlLibrary.Utils.Drawing.FillRoundedRectangle(e.Graphics, BackColorBrush, e.Bounds, (e.Bounds.Height / 3) == 0 ? 1 : (e.Bounds.Height / 3));
				e.Graphics.DrawString(Code.ToString(), e.Font, ForeColorBrush, e.Bounds, CENTER_STRING_FORMAT);
				ControlLibrary.Utils.Drawing.DrawRoundedRectangle(e.Graphics, BackColorPen, e.Bounds, (e.Bounds.Height / 3) == 0 ? 1 : (e.Bounds.Height / 3));
				ForeColorBrush.Dispose();
				BackColorBrush.Dispose();
				BackColorPen.Dispose();
			}
		}

		public class TextNote : ListItemNote
		{
			public TextNote(string text) : base(text: text) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Brush brush = new SolidBrush(e.ForeColor);
				e.Graphics.DrawString(Text, e.Font, brush, e.Bounds, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				return GetTextSize(graphics: graphics, Text, font: font, width: itemWidth, LEFT_STRING_FORMAT);
			}
		}

		public class DescriptionNote : ListItemNote
		{
			public DescriptionNote(string text) : base(text: text) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font italicFont = new Font(e.Font.FontFamily, e.Font.Size - 1, FontStyle.Italic);
				Brush brush = new SolidBrush(e.ForeColor);
				e.Graphics.DrawString(Text, italicFont, brush, e.Bounds, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font italicFont = new Font(font.FontFamily, font.Size - 1, FontStyle.Italic);
				return GetTextSize(graphics: graphics, Text, font: italicFont, width: itemWidth, LEFT_STRING_FORMAT);
			}
		}

		public class BottomBarNote : ListItemNote
		{
			private Rating rating;
			private bool isAppled;
			public Rectangle ApplyButton { get; private set; }
			public Rectangle CancelButton { get; private set; }

			public BottomBarNote() : base(text: string.Empty)
			{
				rating = Rating.Empty;
				ApplyButton = Rectangle.Empty;
				CancelButton = Rectangle.Empty;
			}

			public Rating Rating
			{
				get => rating;
				set
				{
					if (rating != value)
					{
						rating = value;
						DoContentChanged();
					}
				}
			}

			public bool IsAppled
			{
				get => isAppled;
				set
				{
					if (isAppled != value)
					{
						isAppled = value;
						DoContentChanged();
					}
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				for (int i = 0; i < 5; i++)
				{
					int percent = Rating.GetPercent(i + 1, 5);
					Rectangle star = new Rectangle(e.Bounds.X + ((e.Bounds.Height + 1) * i), e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
					ControlLibrary.Utils.Drawing.DrawStar(e.Graphics, e.ForeColor, Color.Red, percent, star);
				}
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
				Rectangle rect = new Rectangle(e.Bounds.X + ((e.Bounds.Height + 1) * 5), e.Bounds.Y + 1,
					e.Bounds.Height * 2, e.Bounds.Height);
				e.Graphics.DrawString(Rating.ToString(), boldFont, Brushes.Red, rect);

				if (!isAppled)
				{
					ApplyButton = new Rectangle(e.Bounds.Width - (int)(e.Bounds.Height * 2.3), e.Bounds.Y,
						e.Bounds.Height - 3, e.Bounds.Height - 3);
					ControlLibrary.Utils.Drawing.DrawOkIcon(e.Graphics, Color.Green, e.BackColor, ApplyButton);

					CancelButton = new Rectangle(e.Bounds.Width - e.Bounds.Height - 3, e.Bounds.Y,
						e.Bounds.Height - 3, e.Bounds.Height - 3);
					ControlLibrary.Utils.Drawing.DrawCancelIcon(e.Graphics, Color.Red, e.BackColor, CancelButton);
				}
				else
				{
					ApplyButton = Rectangle.Empty;
					CancelButton = Rectangle.Empty;
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				SizeF measure = graphics.MeasureString("8888", boldFont, itemWidth - 3, CENTER_STRING_FORMAT);
				return new Size(itemWidth, (int)measure.Height + 4);
			}
		}

	}	
}
