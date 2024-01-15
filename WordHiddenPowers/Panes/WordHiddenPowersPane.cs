using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using WordHiddenPowers.Repositoryes;
using Microsoft.Office.Core;
using System.Text;
using WordHiddenPowers.Dialogs;
using System.Reflection;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        private System.Collections.Generic.IList<Form> dialogs = null;
              
        public Word.Document Document { get; }

        public RepositoryDataSet PowersDataSet { get; }

        public WordHiddenPowersPane(Word.Document Doc)
        {
            dialogs = new System.Collections.Generic.List<Form>();

            Document = Doc;
            PowersDataSet = new RepositoryDataSet();

            InitializeComponent();

            noteListBox.PowersDataSet = PowersDataSet;

            InitializeVariables();            

        }

        public string Title
        {
            get { return titleTextBox.Text; }
        }

        public DateTime Date
        {
            get { return dateTimePicker.Value; }
        }

        public string Description
        {
            get { return descriptionTextBox.Text; }
        }
        
       

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            SplitterPanel panel = (SplitterPanel)sender;

            titleLabel.Location = new Point(0, 0);
            titleTextBox.Location = new Point(0, titleLabel.Height);
            titleTextBox.Width = panel.Width;
            dateLabel.Location = new Point(0, titleTextBox.Top + titleTextBox.Height + 4);
            dateTimePicker.Location = new Point(dateLabel.Width + 8, titleTextBox.Top + titleTextBox.Height + 4);
            descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);
            descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
            descriptionTextBox.Width = panel.Width;
        }
        
        public void InitializeVariables() {

            if (Document.Variables.Count > 0)
            {
                DataSetRefresh();

                Word.Variable title = GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    titleTextBox.Text = title.Value;
                }

                Word.Variable date = GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                   dateTimePicker.Value = DateTime.Parse(date.Value);
                }

                Word.Variable description = GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    descriptionTextBox.Text = description.Value;
                }

            }
        }
        
        public void DeleteVariables()
        {
            PowersDataSet.DecimalPowers.Clear();
            PowersDataSet.TextPowers.Clear();
            PowersDataSet.Categories.Clear();
            PowersDataSet.Subcategories.Clear();
            PowersDataSet.ColumnsHeaders.Clear();
            PowersDataSet.RowsHeaders.Clear();

            if (Document.Variables.Count > 0)
            {
                titleTextBox.Text = string.Empty;
                dateTimePicker.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;

                Word.Variable title = GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    title.Delete();
                }

                Word.Variable date = GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    date.Delete();
                }

                Word.Variable description = GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    description.Delete();
                }

                Word.Variable categories = GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
                if (categories != null)
                {
                    categories.Delete();
                }

                Word.Variable table = GetVariable(Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    table.Delete();
                }
            }
        }

        public bool VariablesExists()
        {
            if (Document.Variables.Count > 0)
            {
                Word.Variable title = GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    return true;
                }

                Word.Variable date = GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return true;
                }

                Word.Variable categories = GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
                if (categories != null)
                {
                    return true;
                }

                Word.Variable table = GetVariable(Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void DataSetRefresh()
        {
            Word.Variable categories = GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (categories != null)
            {
                StringReader reader = new StringReader(categories.Value);

                PowersDataSet.DecimalPowers.Clear();
                PowersDataSet.TextPowers.Clear();
                PowersDataSet.Categories.Clear();
                PowersDataSet.Subcategories.Clear();
                PowersDataSet.ColumnsHeaders.Clear();
                PowersDataSet.RowsHeaders.Clear();

                PowersDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                reader.Close();
            }
        }

        public void ShowEditCategoriesDialog()
        {
            Form dialog = new CategoriesEditorDialog(this);
            dialogs.Add(dialog);
            dialog.ShowDialog();
        }

        public void ShowCreateTableDialog()
        {
            Form dialog = new CreateTableDialog(this);
            dialogs.Add(dialog);
            dialog.ShowDialog();
        }

        public void ShowEditTableDialog()
        {
            Form dialog = new TableEditorDialog(this);
            dialogs.Add(dialog);
            dialog.Show();
        }
        
        public void AddTextNote(Word.Selection selection)
        {
            TextNoteDialog dialog = new TextNoteDialog(selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.TextPowers.Rows.Add(new object[]
                { null, 0, 0, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }
        }

        public void AddDecimalNote(Word.Selection selection)
        {
            DecimalNoteDialog dialog = new DecimalNoteDialog(selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.DecimalPowers.Rows.Add(new object[] 
                { null, 0, 0, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }                
        }      

       

        // add the button to the context menus that you need to support
        //AddButton(applicationObject.CommandBars["Text"]);
        //AddButton(applicationObject.CommandBars["Table Text"]);
        //AddButton(applicationObject.CommandBars["Table Cells"]);


        private Word.Variable GetVariable(Word.Variables array, string variableName)
        {
            for (int i = 1; i <= array.Count; i++)
            {
                if (array[i].Name == variableName)
                {
                    return array[i];
                }
            }
            return null;
        }

        public void CommitVariables()
        {
            Word.Variable title = GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
            if (title == null)
            {
                Document.Variables.Add(Const.Globals.TITLE_VARIABLE_NAME, titleTextBox.Text);
            }
            else
            {
                title.Value = titleTextBox.Text;
            }

            Word.Variable date = GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
            if (date == null)
            {
                Document.Variables.Add(Const.Globals.DATE_VARIABLE_NAME, dateTimePicker.Value.ToShortDateString());
            }
            else
            {
                date.Value = dateTimePicker.Value.ToShortDateString();
            }

            Word.Variable description = GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
            if (description == null)
            {
                Document.Variables.Add(Const.Globals.DESCRIPTION_VARIABLE_NAME, descriptionTextBox.Text);
            }
            else
            {
                description.Value = descriptionTextBox.Text;
            }
            
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            PowersDataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
            writer.Close();

            Word.Variable categories = GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (categories == null)
            {
                Document.Variables.Add(Const.Globals.CATEGORIES_VARIABLE_NAME, builder.ToString());
            }
            else
            {
                categories.Value = builder.ToString();
            }            
        }

        private void Controls_TextChanged(object sender, EventArgs e)
        {
            CommitVariables();
        }

        private void NoteOpen_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                Word.Range range = Document.Range(note.WordSelectionStart, note.WordSelectionEnd);
                range.Select();
            }
        }

        private void NoteEdit_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                if (note.IsText)
                {
                    TextNoteDialog dialog = new TextNoteDialog(note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.TextPowers.Set(note.Id,
                            0,
                            0,
                            dialog.Description,
                            dialog.Value,
                            dialog.Reiting,
                            dialog.SelectionStart,
                            dialog.SelectionEnd);
                    }
                }
                else
                {
                    DecimalNoteDialog dialog = new DecimalNoteDialog(note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.DecimalPowers.Set(note.Id,
                            0,
                            0,
                            dialog.Description,
                            dialog.Value,
                            dialog.Reiting,
                            dialog.SelectionStart,
                            dialog.SelectionEnd);
                    }
                }
                CommitVariables();
            }          
        }

        private void NoteRemove_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                if (note.IsText)
                    PowersDataSet.TextPowers.Remove(note);
                else
                    PowersDataSet.DecimalPowers.Remove(note);
                CommitVariables();
            }            
        }        
    }
}
