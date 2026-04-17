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
using Control = WordHiddenPowers.Controls.ListControls.ContentListControl;
using ListItem = WordHiddenPowers.Controls.ListControls.ContentListControl.ListItem;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(ListBox))]
	[ComVisible(false)]
	public class ContentListBox : ListControl<ListItem, Control.ListItemNote>
	{
		private RepositoryDataSet source;
		private string filter;
		private bool hide;
		private int rating;

		public ContentListBox() : base()
		{
			hide = true;
			rating = 0;
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

		public string Filter
		{
			get => filter;
			set
			{
				if (filter != value)
				{
					filter = value;
					ReadData();
				}
			}
		}

		public bool Hide
		{
			get => hide;
			set
			{
				if (hide != value)
				{
					hide = value;
					ReadData();
				}
			}
		}


		public int FilterRating
		{
			get => rating;
			set
			{
				if (rating != value)
				{
					rating = value;
					ReadData();
				}
			}
		}

		private bool GetFilterHide(Note note)
		{
			if (!Hide)
			{
				return note.Hide == false;
			}
			return true;
		}

		private bool GetFilterRating(Note note)
		{
			if (note.IsText)
				return note.Rating >= rating;
			else
				return true;
		}

		protected override void OnItemMouseClick(ItemMouseEventArgs<Control.ListItem, Control.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is Control.BottomBarNote note)
			{
				if (note.CheckButtonRectangle.Contains(e.Location))
				{
					e.Item.Hide = !e.Item.Hide;
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

		private Rectangle checkBoxRectage = Rectangle.Empty;
		private Rectangle additionButtonRectage = Rectangle.Empty;
		private Rectangle subtractionButtonRectage = Rectangle.Empty;

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (checkBoxRectage.Contains(e.Location))
			{
				Cursor = Cursors.Hand;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
			}
			else if (additionButtonRectage.Contains(e.Location))
			{
				Cursor = Cursors.Hand;
				checkBoxRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
			}
			else if (subtractionButtonRectage.Contains(e.Location))
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				Cursor = Cursors.Hand;
			}
			else
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				Cursor = Cursors.Default;
			}
			base.OnMouseMove(e);
		}

		protected override void OnItemMouseMove(ItemMouseEventArgs<Control.ListItem, Control.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is Control.BottomBarNote note)
			{
				if (note.CheckButtonRectangle.Contains(e.Location))
				{
					checkBoxRectage = note.CheckButtonRectangle;
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					Cursor = Cursors.Hand;
				}
				else if (note.AdditionButtonRectangle.Contains(e.Location))
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = note.AdditionButtonRectangle;
					subtractionButtonRectage = Rectangle.Empty;
					Cursor = Cursors.Hand;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = note.SubtractionButtonRectangle;
					Cursor = Cursors.Hand;
				}
				else
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					Cursor = Cursors.Default;
				}
			}
			else
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				Cursor = Cursors.Default;
			}
			base.OnItemMouseMove(e);
		}

		protected override void OnItemContentChanged(ItemEventArgs<Control.ListItem> e)
		{
			DataSet.Set(e.Item.Note);
			base.OnItemContentChanged(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space && SelectedItem != null)
			{
				SelectedItem.Hide = !SelectedItem.Hide;
				e.SuppressKeyPress = false;
			}
			else if (e.KeyData == Keys.Left && SelectedItem != null)
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

		private void DecimalNotes_RowChanged(object sender, DecimalNotesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row,
					subcategory: DataSet.GetSubcategory(e.Row.subcategory_guid)));
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
				((Control.TitleNote)item[0]).Text = note.FileCaption;
				((Control.TextNote)item[1]).Text = e.Row.Value.ToString();
				((Control.DescriptionNote)item[2]).Text = e.Row.Description;

				int index = Items.IndexOf(item);
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
				((Control.TitleNote)item[0]).Text = note.FileCaption;
				((Control.TextNote)item[1]).Text = e.Row.Value;
				((Control.DescriptionNote)item[2]).Text = e.Row.Description;

				int index = Items.IndexOf(item);
				Rectangle rectangle = GetItemRectangle(index);
				Invalidate(rectangle);
			}
		}

		private void TablesNotes_TableCleared(object sender, DataTableClearEventArgs e) => Items.Clear();

		private void SuspendEvents()
		{
			if (DataSet != null)
			{
				DataSet.DecimalNotes.DecimalNotesRowChanged -= DecimalNotes_RowChanged;
				DataSet.TextNotes.TextNotesRowChanged -= TextNotes_RowChanged;

				DataSet.DecimalNotes.DecimalNotesRowDeleted -= DecimalNotes_RowChanged;
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
				DataSet.DecimalNotes.DecimalNotesRowChanged += new DecimalNotesRowChangeEventHandler(DecimalNotes_RowChanged);
				DataSet.TextNotes.TextNotesRowChanged += new TextNotesRowChangeEventHandler(TextNotes_RowChanged);
				DataSet.DecimalNotes.DecimalNotesRowDeleted += new DecimalNotesRowChangeEventHandler(DecimalNotes_RowChanged);
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

			Items.Clear();

			if (DataSet.TextNotes.Rows.Count > 0 ||
				DataSet.DecimalNotes.Rows.Count > 0)
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
			if (note.Subcategory.Guid.Equals(filter) && GetFilterHide(note) && GetFilterRating(note))
			{
				Items.Add(new ListItem(note: note));
			}
		}

		private void RemoveNote(Note note)
		{
			if (!string.IsNullOrEmpty(Filter)
				&& !note.Subcategory.Guid.Equals(Filter))
			{
				Items.Remove(GetListItem(note.DataRow as DataRow));
			}
		}

		private Note GetNote(DataRow dataRow) => GetListItem(dataRow: dataRow).Note;

		private ListItem GetListItem(DataRow dataRow) =>
			(from ListItem item in Items
			 where item.Note.DataRow.Equals(dataRow)
			 select item)
			.First();

	}

	namespace ContentListControl
	{
		public class ListItem : ControlLibrary.Controls.ListControls.ListItem
		{
			public ListItem() : base()
			{
				Note = default;
			}

			public ListItem(Note note) : base(
				new ListItemNote[] {
				new TitleNote(note),
				new TextNote(note.Value.ToString()),
				new DescriptionNote(note.Description),
				new BottomBarNote(note: note) })
			{
				Note = note;
			}

			public Note Note { get; }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				DrawItemEventArgs arg = new DrawItemEventArgs(
					graphics: e.Graphics,
					font: e.Font,
					rect: e.Bounds,
					index: e.Index,
					state: DrawItemState.None,
					foreColor: SystemColors.WindowText,
					backColor: e.State == (e.State | DrawItemState.Selected) ? Color.FromArgb(250, Color.LightBlue) : SystemColors.Window);
				arg.DrawBackground();
				base.OnDraw(arg);
				if (notes.Length > 2 && (!notes[1].Size.IsEmpty || !notes[2].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Utils.Drawing.DrawLine(e, notes[0]);
				}
				arg.DrawFocusRectangle();
			}

			public bool Hide
			{
				get => ((BottomBarNote)notes[3]).Hide;
				set => ((BottomBarNote)notes[3]).Hide = value;
			}

			public Rating Rating
			{
				get => ((BottomBarNote)notes[3]).Rating;
				set => ((BottomBarNote)notes[3]).Rating = value;
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
			private Size textSize;

			public TitleNote(Note note) : base(text: note.FileCaption)
			{
				textSize = Size.Empty;
			}


			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle textRect = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y,
						textSize.Width, textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, textRect, LEFT_STRING_FORMAT);
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				textSize = GetTextSize(graphics, Text, font, itemWidth - 2, CENTER_STRING_FORMAT);
				return new Size(itemWidth, textSize.Height + 8);			
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
			private readonly Note note;
			protected Size checkBoxSize;
			protected Size ratingBoxSize;

			public Rectangle CheckButtonRectangle { get; private set; }
			public Rectangle AdditionButtonRectangle { get; private set; }
			public Rectangle SubtractionButtonRectangle { get; private set; }

			public BottomBarNote(Note note) : base(text: string.Empty)
			{
				this.note = note;
				CheckButtonRectangle = Rectangle.Empty;
				AdditionButtonRectangle = Rectangle.Empty;
				SubtractionButtonRectangle = Rectangle.Empty;
			}

			public virtual bool Hide
			{
				get => note.Hide;
				set
				{
					if (note.Hide != value)
					{
						note.Hide = value;
						DoContentChanged();
					}
				}
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

			protected override void OnDraw(DrawItemEventArgs e)
			{
				CheckButtonRectangle = new Rectangle(
					e.Bounds.X + 4,
					e.Bounds.Y,
					checkBoxSize.Width,
					checkBoxSize.Height);
				DrawCheckBox(new DrawItemEventArgs(e.Graphics, e.Font, CheckButtonRectangle, e.Index, e.State, e.ForeColor, e.BackColor));

				if (note.IsText)
				{
					Rectangle ratingBoxRectangle = new Rectangle(
						e.Bounds.X + checkBoxSize.Width + 10,
						e.Bounds.Y,
						ratingBoxSize.Width,
						ratingBoxSize.Height);
					DrawRatingBox(new DrawItemEventArgs(e.Graphics, e.Font, ratingBoxRectangle, e.Index, e.State, e.ForeColor, e.BackColor));
				}
			}

			private void DrawCheckBox(DrawItemEventArgs e)
			{
				if (Hide)
				{
					ControlLibrary.Utils.Drawing.DrawCancelIcon(e.Graphics, Const.Globals.COLOR_CANCEL_ICON, e.BackColor, CheckButtonRectangle);
				}
				else
				{
					ControlLibrary.Utils.Drawing.DrawCheckedIcon(e.Graphics, Const.Globals.COLOR_OK_ICON, e.BackColor, CheckButtonRectangle);
				}
			}

			private void DrawRatingBox(DrawItemEventArgs e)
			{
				Color starColor = note.Rating < 0 ? (e.State == (e.State | DrawItemState.Selected) ? e.ForeColor : Const.Globals.COLOR_NEGATIVE_STAR_ICON) : Const.Globals.COLOR_STAR_ICON;
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size + 4, FontStyle.Bold);
				Rectangle[] rectangles = ControlLibrary.Utils.Drawing.DrawRating(e.Graphics, boldFont, e.ForeColor, backBrush: new SolidBrush(starColor), textColor: starColor, rect: e.Bounds, rating: note.Rating, starCount: 5);
				SubtractionButtonRectangle = rectangles[0];
				AdditionButtonRectangle = rectangles[4];
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size + 4, FontStyle.Bold);
				ratingBoxSize = ControlLibrary.Utils.Drawing.MeasureRating(graphics: graphics, font: boldFont, starCount: 5);
				checkBoxSize = new Size(ratingBoxSize.Height, ratingBoxSize.Height);
				return new Size(itemWidth, ratingBoxSize.Height + 12);
			}
		}
	}
}