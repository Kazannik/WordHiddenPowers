using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;
using WordHiddenPowers.Repositoryes.Notes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static WordHiddenPowers.Repositoryes.RepositoryDataSet;

namespace WordHiddenPowers.Controls
{
	[DefaultBindingProperty("SelectedValue")]
	public class NoteListBox1 : ListBox
	{
		private const int CB_SETITEMHEIGHT = 0x0153;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
 //   ...
 //   При этом высота будет установлена на 100 для элемента с индексом 2  

	//SendMessage(comboBox1.Handle, CB_SETITEMHEIGHT, 2, 100);


		private RepositoryDataSet source;

		private IDictionary<int, Subcategory> subcategories;

		private IDictionary<int, WordFilesRow> files;

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
				Items.Add(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) Items.Remove(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Reiting = e.Row.Reiting;
				note.Value = e.Row.Value;
				Category category = source.Categories.Get(e.Row.category_id);
				note.Subcategory = source.Subcategories.Get(category, e.Row.subcategory_id);
				if (note.Rectangle != null)
					Invalidate(note.Rectangle);
			}
		}

		private void TextPowers_RowChanged(object sender, TextPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Items.Add(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) Items.Remove(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Reiting = e.Row.Reiting;
				note.Value = e.Row.Value;
				if (note.Rectangle != null)
					Invalidate(note.Rectangle);
			}
		}

		private void TablesPowers_TableCleared(object sender, DataTableClearEventArgs e)
		{
			Items.Clear();
		}

		private Note GetNote(DataRow dataRow)
		{
			return (from Note item in Items
					where item.DataRow.Equals(dataRow)
					select item).First();
		}

		private Note GetNote(int id, bool isText)
		{
			foreach (Note item in Items)
			{
				if (item.Id == id && item.IsText == isText)
				{
					return item;
				}
			}
			return null;
		}

		private Note GetNote(int x, int y)
		{
			foreach (Note item in Items)
			{
				if (item.removeButton != null && item.removeButton.Contains(x, y))
				{
					return item;
				}
			}
			return null;
		}

		public NoteListBox1()
		{
			base.DrawMode = DrawMode.OwnerDrawVariable;
			base.ItemHeight = 100;
			InitializeComponent();
		}
		
		public NoteListBox1(IContainer container)
		{
			container.Add(this);
			base.DrawMode = DrawMode.OwnerDrawVariable;
			base.ItemHeight = 100;

			InitializeComponent();
		}

		[Browsable(false), ReadOnly(true)]
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
		}

		[Browsable(false), ReadOnly(true)]
		public new int ItemHeight
		{
			get { return base.ItemHeight; }
		}

		protected override void Sort()
		{
			if (Items.Count > 1)
			{
				bool swapped;
				do
				{
					int counter = Items.Count - 1;
					swapped = false;

					while (counter > 0)
					{
						if (Note.Compare((Note)Items[counter], (Note)Items[counter - 1]) != -1)
						{
							(Items[counter - 1], Items[counter]) = (Items[counter], Items[counter - 1]);
							swapped = true;
						}
						counter -= 1;
					}
				}
				while (swapped);
			}
		}

		private Note HoveringNote;

		public Note this[int x, int y]
		{
			get
			{
				foreach (Note item in Items)
				{
					if (item.Rectangle != null &&
						item.Rectangle.Contains(new Point(x, y)))
					{
						return item;
					}
				}
				return null;
			}
		}

		public Note this[int index]
		{
			get
			{
				return Items[index] as Note;				
			}
		}

		public int Add(Note item)
		{
			item.owner = this;
			return Items.Add(item);
		}

		public void Insert(int index, Note item)
		{
			item.owner = this;
			Items.Insert(index, item);
		}

		protected override void OnResize(EventArgs e)
		{
			//base.RefreshItems();
			base.OnResize(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			Note focusNote = this[e.X, e.Y];
			if (focusNote != null)
			{
				Cursor = Cursors.Hand;
				if (HoveringNote != focusNote)
				{
					HoveringNote = focusNote;
					Invalidate(HoveringNote.Rectangle);
				}
			}
			else
			{
				if (HoveringNote != null)
				{
					Rectangle rectangle = HoveringNote.Rectangle;
					HoveringNote = null;
					Invalidate(rectangle);
				}
				Cursor = Cursors.Default;
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			Note note = this[e.X, e.Y];
			if (note != null)
			{
				OnNoteMouseClick(new NoteListMouseEventArgs((Note)SelectedItem, e));
			}
			base.OnMouseClick(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Note note = this[e.X, e.Y];
			if (note != null)
			{
				OnNoteMouseDown(new NoteListMouseEventArgs((Note)SelectedItem, e));
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (SelectedItem != null)
			{
				if (e.Button == MouseButtons.Right)
				{
					OnNoteMouseClick(new NoteListMouseEventArgs((Note)SelectedItem, e));
				}
				OnNoteMouseUp(new NoteListMouseEventArgs((Note)SelectedItem, e));
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			Note note = this[e.X, e.Y];
			if (note != null)
			{
				OnNoteMouseDoubleClick(new NoteListMouseEventArgs((Note)SelectedItem, e));
			}
			base.OnMouseDoubleClick(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (HoveringNote != null)
			{
				Rectangle rectangle = HoveringNote.Rectangle;
				HoveringNote = null;
				Invalidate(rectangle);
			}
			base.OnMouseLeave(e);
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
			if (e.Index >= 0)
			{
				e.DrawBackground();
				if (DesignMode)
				{
					TextRenderer.DrawText(e.Graphics, GetType().Name, e.Font, e.Bounds, e.ForeColor, flags);
				}
				else
				{
					Note note = Items[e.Index] as Note;
					note.DrawItem(e);

					//if (e.Bounds.Height > 1)
					//{
					//	SendMessage(Handle, CB_SETITEMHEIGHT, e.Index, note.Size.Height);
					//}


				}
				e.DrawFocusRectangle();
			}
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (e.Index < Items.Count)
			{
				Note note = this[e.Index];
				Size size = note.GetSizeItem(e.Graphics, Font, ClientSize);				
				e.ItemWidth = size.Width;
				e.ItemHeight = size.Height;
			}
			base.OnMeasureItem(e);
		}


		private void DrawReiting(Note note, DrawItemEventArgs e)
		{
			var rect = e.Bounds;
			rect.Y += 20;
			rect.X += 20;
			rect.Width -= 20;

			e.Graphics.DrawRectangle(Pens.Red, rect);
		}

		private void DrawRemoveButton(Note note, DrawItemEventArgs e)
		{
			note.removeButton = e.Bounds;
			note.removeButton.X = e.Bounds.Width - 15;
			note.removeButton.Width = 14;
			note.removeButton.Height = 14;

			//if (note.Equals(noteHoverRemoveButton))
			//{
			//    e.Graphics.FillRectangle(Brushes.Red, note.removeButton);
			//    e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, note.removeButton);
			//}
			//else if (note.Equals(noteDownRemoveButton))
			//{
			//    e.Graphics.FillRectangle(Brushes.Blue, note.removeButton);
			//    e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, note.removeButton);
			//}
			//else
			//{
			//    e.Graphics.FillRectangle(new SolidBrush(BackColor), note.removeButton);
			//    e.Graphics.DrawRectangle(SystemPens.ControlDark, note.removeButton);
			//}            
		}

		private void InvalidateRemoveButtons()
		{
			foreach (Note item in Items)
			{
				Invalidate(item.removeButton);
			}
		}

		public void ReadData()
		{
			if (DesignMode || source == null) return;

			BeginUpdate();
			IDictionary<int, Category> categories = new Dictionary<int, Category>();
			if (source.Categories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Categories.Rows)
				{
					Category category = Category.Create(row);
					categories.Add(category.Id, category);
				}
			}
			else
			{
				Category category = Category.Default();
				categories.Add(category.Id, category);
			}

			subcategories = new Dictionary<int, Subcategory>();
			if (source.Subcategories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Subcategories.Rows)
				{
					Subcategory subcategory = Subcategory.Create(categories[(int)row["category_id"]], row);
					subcategories.Add(subcategory.Id, subcategory);
				}
			}
			else
			{
				foreach (Category category in categories.Values)
				{
					Subcategory subcategory = Subcategory.Default(category: category);
					subcategories.Add(subcategory.Id, subcategory);
				}
			}

			files = new Dictionary<int, WordFilesRow>();
			if (source.WordFiles.Rows.Count > 0)
			{
				foreach (WordFilesRow row in source.WordFiles.Rows)
				{
					files.Add(row.id, row);
				}
			}

			if (source.TextPowers.Rows.Count > 0 ||
				source.DecimalPowers.Rows.Count > 0)
			{
				foreach (Note note in source.GetNotes())
				{
					Add(note);
				}
			}
			EndUpdate();
		}

		internal enum NoteState : int
		{
			Disabled = 0,
			Passive = 1,
			Hovering = 2,
			Selected = 3
		}

		public event EventHandler<NoteListMouseEventArgs> NoteMouseClick;
		public event EventHandler<NoteListMouseEventArgs> NoteMouseDoubleClick;
		public event EventHandler<NoteListMouseEventArgs> NoteMouseDown;
		public event EventHandler<NoteListMouseEventArgs> NoteMouseUp;

		protected virtual void OnNoteMouseClick(NoteListMouseEventArgs e)
		{
			NoteMouseClick?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseDoubleClick(NoteListMouseEventArgs e)
		{
			NoteMouseDoubleClick?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseDown(NoteListMouseEventArgs e)
		{
			NoteMouseDown?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseUp(NoteListMouseEventArgs e)
		{
			NoteMouseUp?.Invoke(this, e);
		}

		public class NoteListMouseEventArgs : MouseEventArgs
		{
			public NoteListMouseEventArgs(Note note, MouseEventArgs arg) : base(arg.Button, arg.Clicks, arg.X, arg.Y, arg.Delta)
			{
				Note = note;
			}

			public Note Note { get; }
		}

		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// NoteListBox
			// 
			this.ClientSizeChanged += new System.EventHandler(this.NoteListBox_ClientSizeChanged);
			this.ResumeLayout(false);

		}

		private void NoteListBox_ClientSizeChanged(object sender, EventArgs e)
		{
			base.RefreshItems();
		}
	}
}
