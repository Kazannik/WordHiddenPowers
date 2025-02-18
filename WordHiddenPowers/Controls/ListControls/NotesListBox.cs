using ControlLibrary.Controls.ListControls;
using ControlLibrary.Structures;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Notes;
using static WordHiddenPowers.Repositories.RepositoryDataSet;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(ListBox))]
	[ComVisible(false)]
	public class NotesListBox : ListControl<NotesListControl.ListItem, NotesListControl.ListItemNote>
	{
		private RepositoryDataSet source;
		private bool showButtons;
		public event EventHandler<ItemMouseEventArgs<NotesListControl.ListItem, NotesListControl.BottomBarNote>> ItemApplyClick;
		public event EventHandler<ItemMouseEventArgs<NotesListControl.ListItem, NotesListControl.BottomBarNote>> ItemCancelClick;

		public NotesListBox() : base()
		{
			showButtons = false;
		}

		public RepositoryDataSet DataSet
		{
			get
			{
				return source;
			}
			set
			{
				if (source != value)
				{
					if (source != null)
					{
						SuspendEvents();
					}

					source = value;
					ReadData();
				}
			}
		}

		public bool ShowButtons
		{
			get { return showButtons; }
			set
			{
				if (showButtons != value)
				{
					showButtons = value;
				}
			}
		}

		protected override void OnItemMouseClick(ItemMouseEventArgs<NotesListControl.ListItem, NotesListControl.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is NotesListControl.BottomBarNote note)
			{
				if (!note.ShowButtons && note.ApplyButton.Contains(e.Location))
				{
					ItemApplyClick?.Invoke(this,
						new ItemMouseEventArgs<NotesListControl.ListItem, NotesListControl.BottomBarNote>(e.Item, note, e.Argument,
						new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)));
				}
				else if (!note.ShowButtons && note.CancelButton.Contains(e.Location))
				{
					ItemCancelClick?.Invoke(this,
						new ItemMouseEventArgs<NotesListControl.ListItem, NotesListControl.BottomBarNote>(e.Item, note, e.Argument,
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
			//ReadData();
		}
				
		private void Categories_RowChanged(object sender, CategoriesRowChangeEvent e)
		{
			//ReadData();
		}
		
		private void Subcategories_RowChanged(object sender, SubcategoriesRowChangeEvent e)
		{
			//ReadData();
		}

		private void DecimalPowers_RowChanged(object sender, DecimalPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row,
					fileRow: DataSet.WordFiles.GetRow(e.Row.file_id),
					subcategory: DataSet.Subcategories.Get(e.Row.subcategory_guid)));
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
				note.Subcategory = DataSet.Subcategories.Get(e.Row.subcategory_guid);

				ListItem item = GetListItem(e.Row);
				((NotesListControl.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((NotesListControl.TitleNote)item[0]).Text = note.Category.Text;
				((NotesListControl.TextNote)item[1]).Text = e.Row.Value.ToString();
				((NotesListControl.DescriptionNote)item[2]).Text = e.Row.Description;

				int index = Items.IndexOf(item);
				Rectangle rectangle = GetItemRectangle(index);
				Invalidate(rectangle);
			}
		}

		private void TextPowers_RowChanged(object sender, TextPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row, 
					fileRow: DataSet.WordFiles.GetRow(id: e.Row.file_id),
					subcategory: DataSet.Subcategories.Get(subcategoryGuid: e.Row.subcategory_guid)));
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
				note.Subcategory = DataSet.Subcategories.Get(e.Row.subcategory_guid);

				ListItem item = GetListItem(e.Row);
				((NotesListControl.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((NotesListControl.TitleNote)item[0]).Text = note.Category.Text;
				((NotesListControl.TextNote)item[1]).Text = e.Row.Value;
				((NotesListControl.DescriptionNote)item[2]).Text = e.Row.Description;
				
				int index = Items.IndexOf(item);
				Rectangle rectangle = GetItemRectangle(index);
				Invalidate(rectangle);
			}
		}

		private void TablesPowers_TableCleared(object sender, DataTableClearEventArgs e)
		{
			Items.Clear();
		}

		private void SuspendEvents()
		{
			if (DataSet != null)
			{
				DataSet.DecimalPowers.DecimalPowersRowChanged -= DecimalPowers_RowChanged;
				DataSet.TextPowers.TextPowersRowChanged -= TextPowers_RowChanged;
				
				DataSet.DecimalPowers.DecimalPowersRowDeleted -= DecimalPowers_RowChanged;
				DataSet.TextPowers.TextPowersRowDeleted -= TextPowers_RowChanged;
				DataSet.DecimalPowers.TableCleared -= TablesPowers_TableCleared;
				DataSet.TextPowers.TableCleared -= TablesPowers_TableCleared;

				DataSet.Categories.CategoriesRowChanged -= Categories_RowChanged;
				DataSet.Categories.CategoriesRowDeleted -= Categories_RowChanged;
				DataSet.Categories.TableCleared -= TablesPowers_TableCleared;

				DataSet.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
				DataSet.Subcategories.SubcategoriesRowDeleted -= Subcategories_RowChanged;
				DataSet.Subcategories.TableCleared -= TablesPowers_TableCleared;

				DataSet.WordFiles.WordFilesRowChanged -= WordFiles_RowChanged;
				DataSet.WordFiles.WordFilesRowDeleted -= WordFiles_RowChanged;
				DataSet.WordFiles.TableCleared -= WordFiles_TableCleared;
			}
		}

		private void ResumeEvents()
		{
			if (DataSet != null)
			{
				DataSet.DecimalPowers.DecimalPowersRowChanged += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
				DataSet.TextPowers.TextPowersRowChanged += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
				DataSet.DecimalPowers.DecimalPowersRowDeleted += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
				DataSet.TextPowers.TextPowersRowDeleted += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
				DataSet.DecimalPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
				DataSet.TextPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

				DataSet.Categories.CategoriesRowChanged += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				DataSet.Categories.CategoriesRowDeleted += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				DataSet.Categories.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

				DataSet.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				DataSet.Subcategories.SubcategoriesRowDeleted += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				DataSet.Subcategories.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

				DataSet.WordFiles.WordFilesRowChanged += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
				DataSet.WordFiles.WordFilesRowDeleted += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
				DataSet.WordFiles.TableCleared += new DataTableClearEventHandler(WordFiles_TableCleared);
			}
		}

		public void ReadData()
		{
			if (DesignMode || DataSet == null) return;

			SuspendEvents();

			BeginUpdate();

			Items.Clear();

			if (DataSet.TextPowers.Rows.Count > 0 ||
				DataSet.DecimalPowers.Rows.Count > 0)
			{
				foreach (Note note in DataSet.GetNotes())
				{
					AddNote(note);
				}
			}
			EndUpdate();
			ResumeEvents();
		}

		private void AddNote(Note note)
		{
			Items.Add(new NotesListControl.ListItem(note: note, showButtons: showButtons));
		}

		private void RemoveNote(Note note)
		{
			Items.Remove(GetListItem(note.DataRow as DataRow));
		}

		private Note GetNote(DataRow dataRow)
		{
			return GetListItem(dataRow: dataRow).Note;
		}

		private NotesListControl.ListItem GetListItem(DataRow dataRow)
		{
			return (from NotesListControl.ListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item).First();
		}
	}

	namespace NotesListControl
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
				new TextNote(note.Value.ToString()),
				new DescriptionNote(note.Description),
				new BottomBarNote() {ShowButtons = showButtons } })
			{
				Note = note;
			}

			public Note Note { get; }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				e.DrawBackground();
				base.OnDraw(e);
				if (notes.Length > 2 && (!notes[1].Size.IsEmpty || !notes[2].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(e.ForeColor) : SystemPens.InactiveCaption;
					e.Graphics.DrawLine(linePen,
					e.Bounds.X + 7, notes[0].Size.Height - 2,
					e.Bounds.X + e.Bounds.Width - 15, notes[0].Size.Height - 2);
				}
				e.DrawFocusRectangle();
			}

			public bool ShowButtons
			{
				get => ((BottomBarNote)notes[3]).ShowButtons;
				set
				{
					((BottomBarNote)notes[3]).ShowButtons = value;
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
			private bool showButtons;
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

				if (showButtons)
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
