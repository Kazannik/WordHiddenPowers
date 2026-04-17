using ControlLibrary.Controls.ListControls;
using ControlLibrary.Structures;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Notes;
using static WordHiddenPowers.Repository.RepositoryDataSet;
using Control = WordHiddenPowers.Controls.ListControls.NotesListControl;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(ListBox))]
	[ComVisible(false)]
	public class NotesListBox : ListControl<Control.ListItem, Control.ListItemNote>
	{
		private RepositoryDataSet source;
		private bool showButtons;

		public NotesListBox() : base()
		{
			showButtons = false;
		}

		public RepositoryDataSet DataSet
		{
			get => source;
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
			get => showButtons;
			set
			{
				if (showButtons != value)
				{
					showButtons = value;
				}
			}
		}

		protected override void OnItemMouseClick(ItemMouseEventArgs<Control.ListItem, Control.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is Control.BottomBarNote note)
			{
				if (note.ShowButtons && note.ApplyButtonRectangle.Contains(e.Location))
				{
					e.Item.ShowButtons = false;
				}
				else if (note.ShowButtons && note.CancelButtonRectangle.Contains(e.Location))
				{
					DataSet.Remove(e.Item.Note);
				}
				else if (note.AdditionButtonRectangle.Contains(e.Location))
				{
					if (e.Item.Rating < Rating.MaxValue) e.Item.Rating++;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					if (e.Item.Rating > Rating.MinValue) e.Item.Rating--;
				}
			}
			base.OnItemMouseClick(e);
		}

		private Rectangle additionButtonRectangle = Rectangle.Empty;
		private Rectangle subtractionButtonRectangle = Rectangle.Empty;
		private Rectangle applyButtonRectangle = Rectangle.Empty;
		private Rectangle cancelButtonRectangle = Rectangle.Empty;

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (additionButtonRectangle.Contains(e.Location))
			{
				Cursor = Cursors.Hand;
				subtractionButtonRectangle = Rectangle.Empty;
				applyButtonRectangle = Rectangle.Empty;
				cancelButtonRectangle = Rectangle.Empty;
			}
			else if (subtractionButtonRectangle.Contains(e.Location))
			{
				additionButtonRectangle = Rectangle.Empty;
				applyButtonRectangle = Rectangle.Empty;
				cancelButtonRectangle = Rectangle.Empty;
				Cursor = Cursors.Hand;
			}
			else if (applyButtonRectangle.Contains(e.Location))
			{
				additionButtonRectangle = Rectangle.Empty;
				subtractionButtonRectangle = Rectangle.Empty;
				cancelButtonRectangle = Rectangle.Empty;
				Cursor = Cursors.Hand;
			}
			else if (cancelButtonRectangle.Contains(e.Location))
			{
				additionButtonRectangle = Rectangle.Empty;
				subtractionButtonRectangle = Rectangle.Empty;
				applyButtonRectangle = Rectangle.Empty;
				Cursor = Cursors.Hand;
			}
			else
			{
				additionButtonRectangle = Rectangle.Empty;
				subtractionButtonRectangle = Rectangle.Empty;
				applyButtonRectangle = Rectangle.Empty;
				cancelButtonRectangle = Rectangle.Empty;
				Cursor = Cursors.Default;
			}
			base.OnMouseMove(e);
		}

		protected override void OnItemMouseMove(ItemMouseEventArgs<Control.ListItem, Control.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is Control.BottomBarNote note)
			{
				if (note.AdditionButtonRectangle.Contains(e.Location))
				{
					additionButtonRectangle = note.AdditionButtonRectangle;
					subtractionButtonRectangle = Rectangle.Empty;
					applyButtonRectangle = Rectangle.Empty;
					cancelButtonRectangle = Rectangle.Empty;
					Cursor = Cursors.Hand;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					additionButtonRectangle = Rectangle.Empty;
					subtractionButtonRectangle = note.SubtractionButtonRectangle;
					applyButtonRectangle = Rectangle.Empty;
					cancelButtonRectangle = Rectangle.Empty;
					Cursor = Cursors.Hand;
				}
				else if (note.ApplyButtonRectangle.Contains(e.Location))
				{
					additionButtonRectangle = Rectangle.Empty;
					subtractionButtonRectangle = Rectangle.Empty;
					applyButtonRectangle = note.ApplyButtonRectangle;
					cancelButtonRectangle = Rectangle.Empty;
					Cursor = Cursors.Hand;
				}
				else if (note.CancelButtonRectangle.Contains(e.Location))
				{
					additionButtonRectangle = Rectangle.Empty;
					subtractionButtonRectangle = Rectangle.Empty;
					applyButtonRectangle = Rectangle.Empty;
					cancelButtonRectangle = note.CancelButtonRectangle;
					Cursor = Cursors.Hand;
				}
				else
				{
					additionButtonRectangle = Rectangle.Empty;
					subtractionButtonRectangle = Rectangle.Empty;
					applyButtonRectangle = Rectangle.Empty;
					cancelButtonRectangle = Rectangle.Empty;
					Cursor = Cursors.Default;
				}
			}
			else
			{
				additionButtonRectangle = Rectangle.Empty;
				subtractionButtonRectangle = Rectangle.Empty;
				applyButtonRectangle = Rectangle.Empty;
				cancelButtonRectangle = Rectangle.Empty;
				Cursor = Cursors.Default;
			}
			base.OnItemMouseMove(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyData == Keys.Left && SelectedItem != null)
			{
				if (SelectedItem.Note.IsText && SelectedItem.Rating > Rating.MinValue)
					SelectedItem.Rating--;
				e.SuppressKeyPress = true;
			}
			else if (e.KeyData == Keys.Right && SelectedItem != null)
			{
				if (SelectedItem.Note.IsText && SelectedItem.Rating < Rating.MaxValue)
					SelectedItem.Rating++;
				e.SuppressKeyPress = true;
			}
			else
			{
				base.OnKeyDown(e);
			}
		}

		protected override void OnItemContentChanged(ItemEventArgs<Control.ListItem> e)
		{
			DataSet.Set(e.Item.Note);
			base.OnItemContentChanged(e);
		}

		private void WordFiles_TableCleared(object sender, DataTableClearEventArgs e) => ReadData();

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

		private void DecimalPowers_RowChanged(object sender, DecimalNotesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row,
					subcategory: DataSet.GetSubcategory(e.Row.subcategory_guid)));
				TopIndex = Items.Count - 1;
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
				note.Subcategory = DataSet.GetSubcategory(e.Row.subcategory_guid);

				ListItem item = GetListItem(e.Row);
				int index = Items.IndexOf(item);

				((Control.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((Control.TitleNote)item[0]).Text = note.Category.Text;
				((Control.SubtitleNote)item[1]).Text = note.Subcategory.Text;
				((Control.TextNote)item[2]).Text = e.Row.Value.ToString();
				((Control.DescriptionNote)item[3]).Text = e.Row.Description;

				Rectangle rectangle = GetItemRectangle(index);
				Invalidate(rectangle);
			}
		}

		private void TextNotes_RowChanged(object sender, TextNotesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row,
					subcategory: DataSet.GetSubcategory(guid: e.Row.subcategory_guid)));
				TopIndex = Items.Count - 1;
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
				note.Subcategory = DataSet.GetSubcategory(e.Row.subcategory_guid);

				ListItem item = GetListItem(e.Row);
				int index = Items.IndexOf(item);

				((Control.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((Control.TitleNote)item[0]).Text = note.Category.Text;
				((Control.SubtitleNote)item[1]).Text = note.Subcategory.Text;
				((Control.TextNote)item[2]).Text = e.Row.Value;
				((Control.DescriptionNote)item[3]).Text = e.Row.Description;

				Rectangle rectangle = GetItemRectangle(index);
				Invalidate(rectangle);
			}
		}

		private void TablesNotes_TableCleared(object sender, DataTableClearEventArgs e) => Items.Clear();

		private void SuspendEvents()
		{
			if (DataSet != null)
			{
				DataSet.DecimalNotes.DecimalNotesRowChanged -= DecimalPowers_RowChanged;
				DataSet.TextNotes.TextNotesRowChanged -= TextNotes_RowChanged;

				DataSet.DecimalNotes.DecimalNotesRowDeleted -= DecimalPowers_RowChanged;
				DataSet.TextNotes.TextNotesRowDeleted -= TextNotes_RowChanged;
				DataSet.DecimalNotes.TableCleared -= TablesNotes_TableCleared;
				DataSet.TextNotes.TableCleared -= TablesNotes_TableCleared;

				DataSet.Categories.CategoriesRowChanged -= Categories_RowChanged;
				DataSet.Categories.CategoriesRowDeleted -= Categories_RowChanged;
				DataSet.Categories.TableCleared -= TablesNotes_TableCleared;

				DataSet.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
				DataSet.Subcategories.SubcategoriesRowDeleted -= Subcategories_RowChanged;
				DataSet.Subcategories.TableCleared -= TablesNotes_TableCleared;

				DataSet.WordFiles.WordFilesRowChanged -= WordFiles_RowChanged;
				DataSet.WordFiles.WordFilesRowDeleted -= WordFiles_RowChanged;
				DataSet.WordFiles.TableCleared -= WordFiles_TableCleared;
			}
		}

		private void ResumeEvents()
		{
			if (DataSet != null)
			{
				DataSet.DecimalNotes.DecimalNotesRowChanged += new DecimalNotesRowChangeEventHandler(DecimalPowers_RowChanged);
				DataSet.TextNotes.TextNotesRowChanged += new TextNotesRowChangeEventHandler(TextNotes_RowChanged);
				DataSet.DecimalNotes.DecimalNotesRowDeleted += new DecimalNotesRowChangeEventHandler(DecimalPowers_RowChanged);
				DataSet.TextNotes.TextNotesRowDeleted += new TextNotesRowChangeEventHandler(TextNotes_RowChanged);
				DataSet.DecimalNotes.TableCleared += new DataTableClearEventHandler(TablesNotes_TableCleared);
				DataSet.TextNotes.TableCleared += new DataTableClearEventHandler(TablesNotes_TableCleared);

				DataSet.Categories.CategoriesRowChanged += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				DataSet.Categories.CategoriesRowDeleted += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				DataSet.Categories.TableCleared += new DataTableClearEventHandler(TablesNotes_TableCleared);

				DataSet.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				DataSet.Subcategories.SubcategoriesRowDeleted += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				DataSet.Subcategories.TableCleared += new DataTableClearEventHandler(TablesNotes_TableCleared);

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

			int selectedIndex = SelectedIndex;

			Items.Clear();

			if (DataSet.TextNotes.Rows.Count > 0 ||
				DataSet.DecimalNotes.Rows.Count > 0)
			{
				foreach (Note note in DataSet.GetNotes())
				{
					AddNote(note);
				}
			}

			if (selectedIndex < Items.Count &&
				SelectedIndex != selectedIndex)
			{
				SelectedIndex = selectedIndex;
			}

			EndUpdate();
			ResumeEvents();
		}

		private void AddNote(Note note) => Items.Add(new Control.ListItem(note: note, showButtons: showButtons));

		private void RemoveNote(Note note) => Items.Remove(GetListItem(note.DataRow as DataRow));

		private Note GetNote(DataRow dataRow) => GetListItem(dataRow: dataRow).Note;

		private Control.ListItem GetListItem(DataRow dataRow)
		{
			return (from Control.ListItem item in Items
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
				Rectangle codeRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, codeSize.Width, codeSize.Height);
				Utils.Drawing.DrawCode(Code, new DrawItemEventArgs(e.Graphics, boldFont, codeRect, e.Index, e.State, e.ForeColor, e.BackColor));

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle textRect = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y, textSize.Width, textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, textRect, LEFT_STRING_FORMAT);
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
				Rectangle textRect = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y, textSize.Width, textSize.Height);
				e.Graphics.DrawString(Text, boldItalicFont, brush, textRect, LEFT_STRING_FORMAT);
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
