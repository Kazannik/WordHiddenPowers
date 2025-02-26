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
					if (e.Item.Rating < 5) e.Item.Rating++;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					if (e.Item.Rating > -5) e.Item.Rating--;
				}
			}
			else
			{
				base.OnItemMouseClick(e);
			}
		}
		
		private Rectangle additionButtonRectage = Rectangle.Empty;
		private Rectangle subtractionButtonRectage = Rectangle.Empty;
		private Rectangle applyButtonRectage = Rectangle.Empty;
		private Rectangle cancelButtonRectage = Rectangle.Empty;

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (additionButtonRectage.Contains(e.Location))
			{
				Cursor = System.Windows.Forms.Cursors.Hand;
				subtractionButtonRectage = Rectangle.Empty;
				applyButtonRectage = Rectangle.Empty;
				cancelButtonRectage = Rectangle.Empty;
			}
			else if (subtractionButtonRectage.Contains(e.Location))
			{
				additionButtonRectage = Rectangle.Empty;
				applyButtonRectage = Rectangle.Empty;
				cancelButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Hand;
			}
			else if (applyButtonRectage.Contains(e.Location))
			{
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				cancelButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Hand;
			}
			else if (cancelButtonRectage.Contains(e.Location))
			{
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				applyButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Hand;
			}
			else
			{
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				applyButtonRectage = Rectangle.Empty;
				cancelButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Default;
			}
			base.OnMouseMove(e);
		}

		protected override void OnItemMouseMove(ItemMouseEventArgs<Control.ListItem, Control.ListItemNote> e)
		{
			if (e.SubItem != null && e.SubItem is Control.BottomBarNote note)
			{
				if (note.AdditionButtonRectangle.Contains(e.Location))
				{
					additionButtonRectage = note.AdditionButtonRectangle;
					subtractionButtonRectage = Rectangle.Empty;
					applyButtonRectage = Rectangle.Empty;
					cancelButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else if (note.SubtractionButtonRectangle.Contains(e.Location))
				{
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = note.SubtractionButtonRectangle;
					applyButtonRectage = Rectangle.Empty;
					cancelButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else if (note.ApplyButtonRectangle.Contains(e.Location))
				{
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					applyButtonRectage = note.ApplyButtonRectangle;
					cancelButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else if (note.CancelButtonRectangle.Contains(e.Location))
				{
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					applyButtonRectage = Rectangle.Empty;
					cancelButtonRectage = note.CancelButtonRectangle;
					Cursor = System.Windows.Forms.Cursors.Hand;
				}
				else
				{
					additionButtonRectage = Rectangle.Empty;
					subtractionButtonRectage = Rectangle.Empty;
					applyButtonRectage = Rectangle.Empty;
					cancelButtonRectage = Rectangle.Empty;
					Cursor = System.Windows.Forms.Cursors.Default;
				}
			}
			else
			{
				additionButtonRectage = Rectangle.Empty;
				subtractionButtonRectage = Rectangle.Empty;
				applyButtonRectage = Rectangle.Empty;
				cancelButtonRectage = Rectangle.Empty;
				Cursor = System.Windows.Forms.Cursors.Default;
			}
			base.OnItemMouseMove(e);
		}
				
		protected override void OnItemContentChanged(ItemEventArgs<Control.ListItem> e)
		{
			DataSet.Set(e.Item.Note);
			base.OnItemContentChanged(e);
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
				((Control.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((Control.TitleNote)item[0]).Text = note.Category.Text;
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
				((Control.TitleNote)item[0]).Code = Version.Create(note.Category.Position, note.Subcategory.Position, note.Subcategory.Guid);
				((Control.TitleNote)item[0]).Text = note.Category.Text;
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
			Items.Add(new Control.ListItem(note: note, showButtons: showButtons));
		}

		private void RemoveNote(Note note)
		{
			Items.Remove(GetListItem(note.DataRow as DataRow));
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
				new CommentNote(note),
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
				if (notes.Length > 3 && (!notes[2].Size.IsEmpty || !notes[3].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Pen linePen = e.State == (e.State | DrawItemState.Selected) ? new Pen(e.ForeColor) : SystemPens.InactiveCaption;
					e.Graphics.DrawLine(linePen,
					e.Bounds.X + 7, notes[0].Size.Height + notes[1].Size.Height - 2,
					e.Bounds.X + e.Bounds.Width - 15, notes[0].Size.Height + notes[1].Size.Height - 2);
				}
				e.DrawFocusRectangle();
			}

			public bool ShowButtons
			{
				get => ((BottomBarNote)notes.Last()).ShowButtons;
				set
				{
					((BottomBarNote)notes.Last()).ShowButtons = value;
				}
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
				Rectangle codeRectangle = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2,
					codeSize.Width, codeSize.Height);
				Utils.Drawing.DrawCode( Code, new DrawItemEventArgs(e.Graphics, boldFont, codeRectangle, e.Index, e.State, e.ForeColor, e.BackColor));

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

		public class CommentNote : ListItemNote
		{
			private Size codeSize;
			private Size textSize;

			public CommentNote(Note note) : base(text: note.Subcategory.Text) 
			{
				codeSize = Size.Empty;
				textSize = Size.Empty;
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font newFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold | FontStyle.Italic);
				Brush brush = new SolidBrush(e.ForeColor);
				Rectangle rectangle = new Rectangle(e.Bounds.Width - textSize.Width - 1, e.Bounds.Y,
	textSize.Width, textSize.Height);
				e.Graphics.DrawString(Text, newFont, brush, rectangle, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font newFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic);
				codeSize = Utils.Drawing.GetCodeSize(graphics, newFont);
				textSize = GetTextSize(graphics, Text, font, itemWidth - codeSize.Width - 4, CENTER_STRING_FORMAT);
				return new Size(itemWidth, textSize.Height + 8);
			}
		}

		public class TextNote : ListItemNote
		{
			public TextNote(Note note) : base(text: note.Value as string) { }

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
			public DescriptionNote(Note note) : base(text: note.Description) { }

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
					ControlLibrary.Utils.Drawing.DrawStar(e.Graphics, e.ForeColor, Const.Globals.COLOR_STAR_ICON, percent, star);
					if (i == 0) SubtractionButtonRectangle = star;
					else if (i == 4) AdditionButtonRectangle = star;
				}
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
				Rectangle rect = new Rectangle(e.Bounds.X + ((e.Bounds.Height + 1) * 5), e.Bounds.Y + 1,
					e.Bounds.Height * 2, e.Bounds.Height);
				e.Graphics.DrawString(Rating.ToString(), boldFont, e.State == (e.State | DrawItemState.Selected) ? new SolidBrush(e.ForeColor) : Brushes.Red, rect);

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
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				SizeF measure = graphics.MeasureString("8888", boldFont, itemWidth - 3, CENTER_STRING_FORMAT);
				ratingBoxSize = new Size(((int)measure.Height + 1) * 5, (int)measure.Height);
				return new Size(itemWidth, (int)measure.Height + 4);
			}
		}
	}
}
