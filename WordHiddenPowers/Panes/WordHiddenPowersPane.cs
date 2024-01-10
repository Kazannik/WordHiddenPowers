using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using WordHiddenPowers.Repositoryes;
using Microsoft.Office.Core;

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        public Word.Document Document { get; }
       
        public WordHiddenPowersPane(Word.Document Doc)
        {
            this.Document = Doc;

            InitializeComponent();

            Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            applicationObject.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(ApplicationObject_WindowSelectionChange);
            AddButton(applicationObject.CommandBars["Text"]);

            InitializeVariables();
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

        RepositoryDataSet powersDataSet = new RepositoryDataSet();

        public RepositoryDataSet PowersDataSet { get { return powersDataSet; } }

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

                Word.Variable categories = GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
                if (categories != null)
                {
                    StringReader reader = new StringReader(categories.Value);

                    powersDataSet.DecimalPowers.Clear();
                    powersDataSet.StringPowers.Clear();
                    powersDataSet.Categories.Clear();
                    powersDataSet.Subcategories.Clear();
                    powersDataSet.ColumnsHeaders.Clear();
                    powersDataSet.RowsHeaders.Clear();

                    powersDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                    reader.Close();
                }
            }
        }


        public void DeleteVariables()
        {
            powersDataSet.DecimalPowers.Clear();
            powersDataSet.StringPowers.Clear();
            powersDataSet.Categories.Clear();
            powersDataSet.Subcategories.Clear();
            powersDataSet.ColumnsHeaders.Clear();
            powersDataSet.RowsHeaders.Clear();

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
                commandBarButton.Caption = "Hello !!!";
                commandBarButton.FaceId = 356;
                commandBarButton.Tag = "HELLO_TAG";
                commandBarButton.BeginGroup = true;               
            }
        }


        private CommandBarButton GetButton(CommandBar popupCommandBar)
        {
            foreach (var commandBarButton in popupCommandBar.Controls.OfType<CommandBarButton>())
            {
                if (commandBarButton.Tag.Equals("HELLO_TAG"))
                {
                    return commandBarButton;                  
                }
            }
            return null;
        }

        private void CommandBarButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            throw new NotImplementedException();
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

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Word.Variable title = GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
            if (title == null)
            {
                Document.Variables.Add(Const.Globals.TITLE_VARIABLE_NAME, textBox.Text);
            }
            else
            {
                title.Value = textBox.Text;
            }            
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker datePicker = (DateTimePicker)sender;
            Word.Variable date = GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
            if (date == null)
            {
                Document.Variables.Add(Const.Globals.DATE_VARIABLE_NAME, datePicker.Value.ToShortDateString());
            }
            else
            {
                date.Value = datePicker.Value.ToShortDateString();
            }            
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Word.Variable description = GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
            if (description == null)
            {
                Document.Variables.Add(Const.Globals.DESCRIPTION_VARIABLE_NAME, textBox.Text);
            }
            else
            {
                description.Value = textBox.Text;
            }            
        }
    }
}
