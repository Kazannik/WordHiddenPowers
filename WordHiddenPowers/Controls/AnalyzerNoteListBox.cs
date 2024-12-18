using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;
using System;
using static WordHiddenPowers.Repositoryes.RepositoryDataSet;

namespace WordHiddenPowers.Controls
{
    public class AnalyzerNoteListBox : ListBox
    {
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
                    source.DecimalPowers.DecimalPowersRowChanged += new RepositoryDataSet.DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
                    source.TextPowers.TextPowersRowChanged += new RepositoryDataSet.TextPowersRowChangeEventHandler(TextPowers_RowChanged);
                    source.DecimalPowers.DecimalPowersRowDeleted += new RepositoryDataSet.DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
                    source.TextPowers.TextPowersRowDeleted += new RepositoryDataSet.TextPowersRowChangeEventHandler(TextPowers_RowChanged);
                    source.DecimalPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
                    source.TextPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

                    source.Subcategories.SubcategoriesRowChanged += new RepositoryDataSet.SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
                    source.Subcategories.SubcategoriesRowDeleted += new RepositoryDataSet.SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
                    source.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);

                    source.WordFiles.WordFilesRowChanged += new RepositoryDataSet.WordFilesRowChangeEventHandler(WordFiles_RowChanged);
                    source.WordFiles.WordFilesRowDeleted += new RepositoryDataSet.WordFilesRowChangeEventHandler(WordFiles_RowChanged);
                    source.WordFiles.TableCleared += new DataTableClearEventHandler(WordFiles_TableCleared);
                }
            }
        }

        private void WordFiles_TableCleared(object sender, DataTableClearEventArgs e)
        {
            ReadData();
        }

        private void WordFiles_RowChanged(object sender, RepositoryDataSet.WordFilesRowChangeEvent e)
        {
            ReadData();
        }

        private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e)
        {
            ReadData();
        }

        private void Subcategories_RowChanged(object sender, RepositoryDataSet.SubcategoriesRowChangeEvent e)
        {
            ReadData();
        }

        private void DecimalPowers_RowChanged(object sender, RepositoryDataSet.DecimalPowersRowChangeEvent e)
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
                if (note.rectangle != null)
                    Invalidate(note.rectangle);
            }
        }

        private void TextPowers_RowChanged(object sender, RepositoryDataSet.TextPowersRowChangeEvent e)
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
            else if(e.Action == DataRowAction.Change)
            {
                Note note = GetNote(e.Row);
                note.Description = e.Row.Description;
                note.Reiting = e.Row.Reiting;
                note.Value = e.Row.Value;
                if (note.rectangle !=null)
                    Invalidate(note.rectangle);     
            }
        }

        private void TablesPowers_TableCleared(object sender, DataTableClearEventArgs e)
        {
            Items.Clear();
        }
        
        private Note GetNote(DataRow dataRow)
        {
            foreach (Note item in Items)
            {
                if (item.DataRow.Equals(dataRow))
                {
                    return item;
                }
            }
            return null;
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
                if (item.removeButton != null && item.removeButton.Contains(x,y))
                {
                    return item;
                }
            }
            return null;
        }

        public AnalyzerNoteListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 200;
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
                            object temp = Items[counter];
                            Items[counter] = Items[counter - 1];
                            Items[counter - 1] = temp;
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
                    if (item.rectangle != null &&
                        item.rectangle.Contains(new Point(x, y)))
                    {
                        return item;
                    }
                }
                return null;
            }
        }
        
        public int Add(Note item)
        {
            return Items.Add(item);
        }
       
        public void Insert(int index, Note item)
        {
            Items.Insert(index, item);
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
                    Invalidate(HoveringNote.rectangle);
                }
            }
            else
            {
                if (HoveringNote != null)
                {
                    Rectangle rec = HoveringNote.rectangle;
                    HoveringNote = null;
                    Invalidate(rec);
                }
                this.Cursor = Cursors.Default;
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
                if (e.Button== MouseButtons.Right)
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
                Rectangle rec = HoveringNote.rectangle;
                HoveringNote = null;
                Invalidate(rec);
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
                    note.rectangle = e.Bounds;

                    //DrawRemoveButton(note, e);
                    //DrawReiting(note, e);

                    if (note.IsText)
                        DrawTextItem(note, e);
                    else
                        DrawDecimalItem(note, e);
                }
                e.DrawFocusRectangle();
            }
        }

        private void DrawDecimalItem(Note note, DrawItemEventArgs e)
        {
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.Top;

            Rectangle captionRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, 20);
            TextRenderer.DrawText(e.Graphics, note.Category.Caption as string, e.Font, captionRectangle, Color.RosyBrown, flags);

            Rectangle subcategoryRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + 20, e.Bounds.Width, 20);
            TextRenderer.DrawText(e.Graphics, note.Subcategory.Caption as string, e.Font, subcategoryRectangle, Color.YellowGreen, flags);

            Rectangle textRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + 40, e.Bounds.Width, e.Bounds.Height - 40);
            TextRenderer.DrawText(e.Graphics, note.Value.ToString() as string, e.Font, textRectangle, e.ForeColor, flags | TextFormatFlags.WordBreak);
        }

        private void DrawTextItem(Note note, DrawItemEventArgs e)
        {
                       
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.Top;

            Rectangle captionRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, 20);
            TextRenderer.DrawText(e.Graphics, note.Category.Caption as string, e.Font, captionRectangle, Color.Red, flags);

            Rectangle subcategoryRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + 20, e.Bounds.Width, 20);
            TextRenderer.DrawText(e.Graphics, note.Subcategory.Caption as string, e.Font, subcategoryRectangle, Color.Green, flags);
            
            Rectangle textRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + 40, e.Bounds.Width, e.Bounds.Height - 40);
            TextRenderer.DrawText(e.Graphics, note.Value as string, e.Font, textRectangle, e.ForeColor, flags| TextFormatFlags.WordBreak);

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
                    Subcategory subcategory = Subcategory.Create(categories[(int)row["category_id"]],row);
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
                foreach (Note note in source.GetNotesSort())
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
            public NoteListMouseEventArgs(Note note, MouseEventArgs arg): base(arg.Button, arg.Clicks, arg.X, arg.Y, arg.Delta)
            {
                Note = note;                
            }

            public Note Note { get; }
        }
    }
}
