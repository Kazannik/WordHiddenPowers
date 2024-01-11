using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using WordHiddenPowers.Repositoryes;
using Microsoft.Office.Core;
using System.Text;

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        public Word.Document Document { get; }

        public RepositoryDataSet PowersDataSet { get; }

        public WordHiddenPowersPane(Word.Document Doc)
        {
            this.Document = Doc;
            this.PowersDataSet = new RepositoryDataSet();

            Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            applicationObject.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(ApplicationObject_WindowSelectionChange);
            AddButton(applicationObject.CommandBars["Text"]);

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
        
        private void ApplicationObject_WindowSelectionChange(Word.Selection Sel)
        {
            Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            if (Sel.Text.Length > 1)
            {
                GetButton(applicationObject.CommandBars["Text"]).Enabled = true;
            }
            else
            {
                GetButton(applicationObject.CommandBars["Text"]).Enabled = false;
            }
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

        private void AddButton(CommandBar popupCommandBar)
        {
            var commandBarButton = GetButton(popupCommandBar);
            if (commandBarButton !=null)
            {
                commandBarButton.Click += new _CommandBarButtonEvents_ClickEventHandler(CommandBarButton_Click);
            }
            else
            {
                commandBarButton = (CommandBarButton)popupCommandBar.Controls.Add
                    (MsoControlType.msoControlButton);
                commandBarButton.Click += new _CommandBarButtonEvents_ClickEventHandler(CommandBarButton_Click);
                commandBarButton.Caption = "Дополнительные данные...";
                commandBarButton.FaceId = 9267;
                commandBarButton.Tag = Const.Panes.BUTTON_TAG;
                commandBarButton.BeginGroup = true;               
            }
        }
        
        private CommandBarButton GetButton(CommandBar popupCommandBar)
        {
            foreach (var commandBarButton in popupCommandBar.Controls.OfType<CommandBarButton>())
            {
                if (commandBarButton.Tag.Equals(Const.Panes.BUTTON_TAG))
                {
                    return commandBarButton;                  
                }
            }
            return null;
        }

        private void CommandBarButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            
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
