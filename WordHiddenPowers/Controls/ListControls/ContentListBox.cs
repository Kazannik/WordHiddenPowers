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
using Control = WordHiddenPowers.Controls.ListControls.ContentListControl;
using ListItem = WordHiddenPowers.Controls.ListControls.ContentListControl.ListItem;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(ListBox))]
	[ComVisible(false)]
	public class ContentListBox : ListControl<Control.ListItem, Control.ListItemNote>
	{
		private RepositoryDataSet source;
		private string filter;
		private bool hide;

		public event EventHandler<ItemMouseEventArgs<Control.ListItem, Control.BottomBarNote>> ItemApplyClick;
		public event EventHandler<ItemMouseEventArgs<Control.ListItem, Control.BottomBarNote>> ItemCancelClick;

		public ContentListBox() : base()
		{
			hide = true;
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

		public string Filer
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

		private bool FilterHide(Note note)
		{
			if (!Hide)
			{
				return note.Hide == false;
			}
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
					if (e.Item.Rating < 5) e.Item.Rating++;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					if (e.Item.Rating > -5) e.Item.Rating--;
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
				Cursor = System.Windows.Forms.Cursors.Hand;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
			}
			else if (additionButtonRectage.Contains(e.Location))
			{
				Cursor = System.Windows.Forms.Cursors.Hand;
				checkBoxRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
			}
			else if (subtractionButtonRectage.Contains(e.Location))
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Hand;
			}
			else
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Default;
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
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else if (note.AdditionButtonRectangle.Contains(e.Location))
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = note.AdditionButtonRectangle;
					subtractionButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = note.SubtractionButtonRectangle;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else
				{
					checkBoxRectage = Rectangle.Empty;
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Default;
				}
			}
			else
			{
				checkBoxRectage = Rectangle.Empty;
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Default;
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
				((ListItem)SelectedItem).Hide = !((ListItem)SelectedItem).Hide;
				e.SuppressKeyPress = false;
			}
			else if (e.KeyData == Keys.Left && SelectedItem != null)
			{
				if (((ListItem)SelectedItem).Rating > -5)
					((ListItem)SelectedItem).Rating--;
				e.SuppressKeyPress = true;
			}
			else if (e.KeyData == Keys.Right && SelectedItem != null)
			{
				if (((ListItem)SelectedItem).Rating < 5) 
					((ListItem)SelectedItem).Rating++;
				e.SuppressKeyPress = true;
			}
			else
			{
				base.OnKeyDown(e);
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

		private void TextPowers_RowChanged(object sender, TextPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(
					dataRow: e.Row,
					fileRow: DataSet.WordFiles.GetRow(id: e.Row.file_id),
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
			if (!string.IsNullOrEmpty(Filer)
				&& note.Subcategory.Guid.Equals(Filer) 
				&& (FilterHide(note)))
			{
				Items.Add(new Control.ListItem(note: note));
			}			
		}

		private void RemoveNote(Note note)
		{
			if (!string.IsNullOrEmpty(Filer)
				&& !note.Subcategory.Guid.Equals(Filer))
			{
				Items.Remove(GetListItem(note.DataRow as DataRow));
			}
		}

		private Note GetNote(DataRow dataRow)
		{
			return GetListItem(dataRow: dataRow).Note;
		}

		private Control.ListItem GetListItem(DataRow dataRow)
		{
			return (from Control.ListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item).First();
		}
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
					Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(arg.ForeColor) : SystemPens.InactiveCaption;
					arg.Graphics.DrawLine(linePen,
					arg.Bounds.X + 7, notes[0].Size.Height - 2,
					arg.Bounds.X + e.Bounds.Width - 15, notes[0].Size.Height - 2);
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
					Rectangle rectangle = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y,
						textSize.Width, textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, rectangle, LEFT_STRING_FORMAT);
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
				get => (Rating)(note.Rating);
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
				Rectangle checkBoxRectangle = new Rectangle(
					e.Bounds.X + 4,
					e.Bounds.Y + 2,
					checkBoxSize.Width,
					checkBoxSize.Height);				
				DrawCheckBox(new DrawItemEventArgs(e.Graphics, e.Font, checkBoxRectangle, e.Index, e.State, e.ForeColor, e.BackColor));

				Rectangle ratingBoxRectangle = new Rectangle(
					e.Bounds.X + checkBoxSize.Width + 10,
					e.Bounds.Y + 6,
					ratingBoxSize.Width,
					ratingBoxSize.Height);
				DrawRatingBox(new DrawItemEventArgs(e.Graphics, e.Font, ratingBoxRectangle, e.Index, e.State, e.ForeColor, e.BackColor));
			}

			private void DrawCheckBox(DrawItemEventArgs e)
			{
				CheckButtonRectangle = new Rectangle(
					e.Bounds.X + 1,
					e.Bounds.Y + 2,
					e.Bounds.Width - 3,
					e.Bounds.Height - 3);

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
				for (int i = 0; i < 5; i++)
				{
					int percent = Rating.GetPercent(i + 1, 5);
					Rectangle star = new Rectangle(e.Bounds.X + ((e.Bounds.Height + 1) * i), e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
					ControlLibrary.Utils.Drawing.DrawStar(e.Graphics, e.ForeColor, Const.Globals.COLOR_STAR_ICON, percent, star);
					if (i == 0) SubtractionButtonRectangle = star;
					else if (i == 4) AdditionButtonRectangle = star;
				}

				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
				Rectangle rect = new Rectangle(e.Bounds.X + ((e.Bounds.Height + 1) * 5), e.Bounds.Y + 1,
					e.Bounds.Height * 2, e.Bounds.Height);
				e.Graphics.DrawString(Rating.ToString(), boldFont, Brushes.Red, rect);
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				SizeF measure = graphics.MeasureString("8888", boldFont, itemWidth - 3, CENTER_STRING_FORMAT);
				checkBoxSize = new Size((int)measure.Height + 8, (int)measure.Height + 8);
				ratingBoxSize = new Size(((int)measure.Height + 1) * 5, (int)measure.Height);
				return new Size(itemWidth, (int)measure.Height + 14);
			}
		}
	}
}