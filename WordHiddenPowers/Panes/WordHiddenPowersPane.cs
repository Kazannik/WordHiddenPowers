using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Models;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

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

            PowersDataSet.DocumentKeys.DocumentKeysRowChanged += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_DocumentKeysRowChanged);

            InitializeComponent();

            noteListBox.PowersDataSet = PowersDataSet;

            InitializeVariables();            

        }

        private void DocumentKeys_DocumentKeysRowChanged(object sender, RepositoryDataSet.DocumentKeysRowChangeEvent e)
        {
            titleComboBox.BeginUpdate();
            titleComboBox.Items.Clear();
            foreach (DataRow row in PowersDataSet.DocumentKeys.Rows)
            {
                titleComboBox.Items.Add(row["Caption"]);
            }
            titleComboBox.EndUpdate();
        }

        public string Title
        {
            get { return titleComboBox.Text; }
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
            titleComboBox.Location = new Point(0, titleLabel.Height);
            titleComboBox.Width = panel.Width;
            dateLabel.Location = new Point(0, titleComboBox.Top + titleComboBox.Height + 4);
            dateTimePicker.Location = new Point(dateLabel.Width + 8, titleComboBox.Top + titleComboBox.Height + 4);
            descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);
            descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
            descriptionTextBox.Width = panel.Width;
        }
        
        public void InitializeVariables() {

            if (Document.Variables.Count > 0)
            {
                DataSetRefresh();

                titleComboBox.Text = GetVariable(Const.Globals.TITLE_VARIABLE_NAME);
                dateTimePicker.Value = DateTime.Parse(GetVariable(Const.Globals.DATE_VARIABLE_NAME));
                descriptionTextBox.Text = GetVariable(Const.Globals.DESCRIPTION_VARIABLE_NAME);
            }
        }
        
        private string GetVariable(string name)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable != null)
                return variable.Value;
            else
                return string.Empty;
        }

        public void DeleteVariables()
        {
            foreach (DataTable table in PowersDataSet.Tables)
            {
                table.Clear();
            }

            if (Document.Variables.Count > 0)
            {
                titleComboBox.Items.Clear();
                titleComboBox.Text = string.Empty;
                dateTimePicker.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;

                DeleteVariable(Const.Globals.TITLE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DATE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DESCRIPTION_VARIABLE_NAME);
                DeleteVariable(Const.Globals.CATEGORIES_VARIABLE_NAME);
                DeleteVariable(Const.Globals.TABLE_VARIABLE_NAME);                
            }
        }

        private void DeleteVariable(string name)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable != null)
                variable.Delete();            
        }

        public bool VariablesExists()
        {
            if (Document.Variables.Count > 0)
            {
                Word.Variable title = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    return true;
                }

                Word.Variable date = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return true;
                }

                Word.Variable categories = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
                if (categories != null)
                {
                    return true;
                }

                Word.Variable table = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void DataSetRefresh()
        {
            Word.Variable categories = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (categories != null)
            {
                StringReader reader = new StringReader(categories.Value);

                foreach (DataTable table in PowersDataSet.Tables)
                {
                    table.Clear();
                }
                                
                PowersDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                reader.Close();
            }
        }


        public void AddTextNote(Word.Selection selection)
        {
            TextNoteDialog dialog = new TextNoteDialog(PowersDataSet, selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.TextPowers.Rows.Add(new object[]
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }
        }

        public void AddDecimalNote(Word.Selection selection)
        {
            DecimalNoteDialog dialog = new DecimalNoteDialog(PowersDataSet, selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.DecimalPowers.Rows.Add(new object[] 
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }                
        }      

       

        // add the button to the context menus that you need to support
        //AddButton(applicationObject.CommandBars["Text"]);
        //AddButton(applicationObject.CommandBars["Table Text"]);
        //AddButton(applicationObject.CommandBars["Table Cells"]);


        

        public void CommitVariables()
        {
            CommitVariables(Const.Globals.TITLE_VARIABLE_NAME, titleComboBox.Text);
            CommitVariables(Const.Globals.DATE_VARIABLE_NAME, dateTimePicker.Value.ToShortDateString());
            CommitVariables(Const.Globals.DESCRIPTION_VARIABLE_NAME, descriptionTextBox.Text);
                        
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            PowersDataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
            writer.Close();

            CommitVariables(Const.Globals.CATEGORIES_VARIABLE_NAME,builder.ToString());

        }
               
        private void CommitVariables(string name, string value)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable == null)
                Document.Variables.Add(name, value);
            else
                variable.Value = value;
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
                    TextNoteDialog dialog = new TextNoteDialog(PowersDataSet, note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.TextPowers.Set(note.Id,
                            dialog.Category.Id,
                            dialog.Subcategory.Id,
                            dialog.Description,
                            dialog.Value,
                            dialog.Reiting,
                            dialog.SelectionStart,
                            dialog.SelectionEnd);
                    }
                }
                else
                {
                    DecimalNoteDialog dialog = new DecimalNoteDialog(PowersDataSet, note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.DecimalPowers.Set(note.Id,
                            dialog.Category.Id,
                            dialog.Subcategory.Id,
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
