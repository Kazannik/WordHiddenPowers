using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using Word = WordHiddenPowers.Categories;

namespace WordSuite.Controls
{
    class NoteListBox : ListBox
    {

        private RepositoryDataSet source;

        private IDictionary<int, Word.Subcategory> subcategories;

        public RepositoryDataSet PowersDataSet
        {
            get
            {
                return source;
            }

            set
            {
                if (source != null)
                {
                    source.DecimalPowers.DecimalPowersRowChanged -= DecimalPowers_DecimalPowersRowChanged;
                    source.TextPowers.TextPowersRowChanged -= TextPowers_TextPowersRowChanged;
                    source.DecimalPowers.DecimalPowersRowDeleted -= DecimalPowers_DecimalPowersRowChanged;
                    source.TextPowers.TextPowersRowDeleted -= TextPowers_TextPowersRowChanged;
                    source.DecimalPowers.TableCleared -= TablesPowers_TableCleared;
                    source.TextPowers.TableCleared -= TablesPowers_TableCleared;
                }
                source = value;

                if (source != null)
                {
                    ReadData();
                    source.DecimalPowers.DecimalPowersRowChanged += new RepositoryDataSet.DecimalPowersRowChangeEventHandler(DecimalPowers_DecimalPowersRowChanged);
                    source.TextPowers.TextPowersRowChanged += new RepositoryDataSet.TextPowersRowChangeEventHandler(TextPowers_TextPowersRowChanged);
                    source.DecimalPowers.DecimalPowersRowDeleted += new RepositoryDataSet.DecimalPowersRowChangeEventHandler(DecimalPowers_DecimalPowersRowChanged);
                    source.TextPowers.TextPowersRowDeleted += new RepositoryDataSet.TextPowersRowChangeEventHandler(TextPowers_TextPowersRowChanged);
                    source.DecimalPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
                    source.TextPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
                }
                else
                {
                    Items.Clear();
                }
            }
        }

        private void DecimalPowers_DecimalPowersRowChanged(object sender, RepositoryDataSet.DecimalPowersRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add)
            {
                Items.Add(Note.Create(e.Row, subcategories[e.Row.subcategory_id]));
            }
            else if (e.Action == DataRowAction.Delete)
            {
                Note note = GetNote(e.Row);
                if (note != null) Items.Remove(note);
            }
            else if (e.Action == DataRowAction.Change)
            {
                //Note note = GetNote(e.Row);
                //note.Description = e.Row.Description;
                //note.Reiting = e.Row.Reiting;
                //note.Value = e.Row.Value;
                //if (note.rectagle != null)
                //    Invalidate(note.rectagle);
            }
        }

        private void TextPowers_TextPowersRowChanged(object sender, RepositoryDataSet.TextPowersRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add)
            {
                Items.Add(Note.Create(e.Row, subcategories[e.Row.subcategory_id]));

            }
            else if (e.Action == DataRowAction.Delete)
            {
                Note note = GetNote(e.Row);
                if (note != null) Items.Remove(note);
            }
            else if (e.Action == DataRowAction.Change)
            {
                //Note note = GetNote(e.Row);
                //note.Description = e.Row.Description;
                //note.Reiting = e.Row.Reiting;
                //note.Value = e.Row.Value;
                //if (note.rectagle != null)
                //    Invalidate(note.rectagle);
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
                //if (item.removeButton != null && item.removeButton.Contains(x, y))
                //{
                //    return item;
                //}
            }
            return null;
        }

        public NoteListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 40;
        }

        protected override void Sort()
        {
            if (this.Items.Count > 1)
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

        Note noteHoverRemoveButton;
        Note oldNoteHoverRemoveButton;

        Note noteDownRemoveButton;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                oldNoteHoverRemoveButton = noteDownRemoveButton;
                noteHoverRemoveButton = GetNote(e.X, e.Y);
                //if (oldNoteHoverRemoveButton != null)
                    //Invalidate(oldNoteHoverRemoveButton.removeButton);
                //if (noteHoverRemoveButton != null)
                    //Invalidate(noteHoverRemoveButton.removeButton);
                //else
                    InvalidateRemoveButtons();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                noteHoverRemoveButton = null;
                noteDownRemoveButton = GetNote(e.X, e.Y);
                //if (noteDownRemoveButton != null)
                    //Invalidate(noteDownRemoveButton.removeButton);
            }
            base.OnMouseDown(e);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            noteHoverRemoveButton = null;
            noteDownRemoveButton = null;
            InvalidateRemoveButtons();
            base.OnMouseUp(e);
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
                    //note.rectagle = e.Bounds;

                    DrawRemoveButton(note, e);
                    DrawReiting(note, e);

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


            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            string noteValue = note.Value.ToString();

            //e.Graphics.DrawRectangle(Pens.Red, 2, e.Bounds.Y + 2, 14, 14); // Simulate an icon.

            //var textRect = e.Bounds;
            //textRect.X += 20;
            //textRect.Width -= 20;
            //string itemText = DesignMode ? "NoteListBox" : Items[e.Index].ToString();
            TextRenderer.DrawText(e.Graphics, noteValue, e.Font, e.Bounds, e.ForeColor, flags);
        }

        private void DrawTextItem(Note note, DrawItemEventArgs e)
        {
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, note.Value as string, e.Font, e.Bounds, e.ForeColor, flags);

        }

        private void DrawReiting(Note note, DrawItemEventArgs e)
        {
            var rect = e.Bounds;
            rect.Y += 20;
            rect.X += 20;
            rect.Width -= 20;

            //e.Graphics.DrawRectangle(Pens.Red, rect);
        }

        private void DrawRemoveButton(Note note, DrawItemEventArgs e)
        {
            //note.removeButton = e.Bounds;
            //note.removeButton.X = e.Bounds.Width - 15;
            //note.removeButton.Width = 14;
            //note.removeButton.Height = 14;

            if (note.Equals(noteHoverRemoveButton))
            {
                //e.Graphics.FillRectangle(Brushes.Red, note.removeButton);
                //e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, note.removeButton);
            }
            else if (note.Equals(noteDownRemoveButton))
            {
                //e.Graphics.FillRectangle(Brushes.Blue, note.removeButton);
                //e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, note.removeButton);
            }
            else
            {
                //e.Graphics.FillRectangle(new SolidBrush(BackColor), note.removeButton);
                //e.Graphics.DrawRectangle(SystemPens.ControlDark, note.removeButton);
            }
        }

        private void InvalidateRemoveButtons()
        {
            foreach (Note item in Items)
            {
                //this.Invalidate(item.removeButton);
            }
        }

        public void ReadData()
        {
            if (DesignMode) return;

            this.BeginUpdate();
            Items.Clear();

            if (source == null)
            {
                this.EndUpdate();
                return;
            }

            IDictionary<int, Word.Category> categories = new Dictionary<int, Word.Category>();
            if (source.Categories.Rows.Count > 0)
            {
                foreach (DataRow row in source.Categories.Rows)
                {
                    Word.Category category = Word.Category.Create(row);
                    categories.Add(category.Id, category);
                }
            }
            else
            {
                Word.Category category = Word.Category.Default();
                categories.Add(category.Id, category);
            }

            subcategories = new Dictionary<int, Word.Subcategory>();
            if (source.Subcategories.Rows.Count > 0)
            {
                foreach (DataRow row in source.Subcategories.Rows)
                {
                    Word.Subcategory subcategory = Word.Subcategory.Create(categories[(int)row["category_id"]], row);
                    subcategories.Add(subcategory.Id, subcategory);
                }
            }
            else
            {
                foreach (Word.Category category in categories.Values)
                {
                    Word.Subcategory subcategory = Word.Subcategory.Default(category: category);
                    subcategories.Add(subcategory.Id, subcategory);
                }
            }

            //foreach (DataRow row in source.DecimalPowers.Rows)
            //{
            //    Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
            //    Note note = Note.Create(row, subcategory);
            //    this.Items.Add(note);
            //}

            this.EndUpdate();
        }
    }
}