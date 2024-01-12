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

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        CreateTableDialog createDialog;
        TableEditorDialog editorDialog;

        SelectCategoryStringDialog selectedStringDialog;
        SelectCategoryDecimalDialog selectedDecimalDialog;

       
        public Word.Document Document { get; }

        public RepositoryDataSet PowersDataSet { get; }

        public WordHiddenPowersPane(Word.Document Doc)
        {
            this.Document = Doc;
            this.PowersDataSet = new RepositoryDataSet();

            InitializeComponent();
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
        
        public _CommandBarButtonEvents_ClickEventHandler DecimalCategoryDelegate
        {
            get { return DecimalCategoryClick; }
        }

        public _CommandBarButtonEvents_ClickEventHandler StringCategoryDelegate
        {
            get { return StringCategoryClick; }
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

                categoriesListBox1.PowersDataSet = PowersDataSet;                                
            }
        }
        
        public void DeleteVariables()
        {
            PowersDataSet.DecimalPowers.Clear();
            PowersDataSet.StringPowers.Clear();
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
                PowersDataSet.StringPowers.Clear();
                PowersDataSet.Categories.Clear();
                PowersDataSet.Subcategories.Clear();
                PowersDataSet.ColumnsHeaders.Clear();
                PowersDataSet.RowsHeaders.Clear();

                PowersDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                reader.Close();
            }
        }
        
        public void ShowCreateTableDialog()
        {
            createDialog = new CreateTableDialog(this);
            createDialog.ShowDialog();
        }

        public void ShowEditTableDialog()
        {
            editorDialog = new TableEditorDialog(this);
            editorDialog.Show();
        }
        
        public void AddStringSelection(Word.Selection selection)
        {
            selectedStringDialog = new SelectCategoryStringDialog(selection);
            if (selectedStringDialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.StringPowers.Rows.Add(new object[]
                { null, 0, 0, selectedStringDialog.Description, selectedStringDialog.Value, selectedStringDialog.Rating, selectedStringDialog.StartPosition, selectedStringDialog.SelectionEnd });

                CommitVariables();
            }
        }

        public void AddDecimalSelection(Word.Selection selection)
        {
            selectedDecimalDialog = new SelectCategoryDecimalDialog(selection);
            if (selectedDecimalDialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.DecimalPowers.Rows.Add(new object[] 
                { null, 0, 0, selectedDecimalDialog.Description, selectedDecimalDialog.Value, selectedDecimalDialog.Rating, selectedDecimalDialog.StartPosition, selectedDecimalDialog.SelectionEnd });

                CommitVariables();
            }                
        }      

        private void StringCategoryClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddStringSelection(Globals.ThisAddIn.Application.ActiveWindow.Selection);
        }

        private void DecimalCategoryClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddDecimalSelection(Globals.ThisAddIn.Application.ActiveWindow.Selection);
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
    }
}
