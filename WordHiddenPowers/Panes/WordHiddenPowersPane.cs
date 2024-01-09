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

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        public Word.Document Document { get; }
       
        public WordHiddenPowersPane(Word.Document Doc)
        {
            this.Document = Doc;

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
                Word.Variable title = GetVariable(Document.Variables, Globals.ThisAddIn.TitleVariableName);
                if (title != null)
                {
                    titleTextBox.Text = title.Value;
                }

                Word.Variable date = GetVariable(Document.Variables, Globals.ThisAddIn.DateVariableName);
                if (date != null)
                {
                   dateTimePicker.Value = DateTime.Parse(date.Value);
                }

                Word.Variable description = GetVariable(Document.Variables, Globals.ThisAddIn.DescriptionVariableName);
                if (description != null)
                {
                    descriptionTextBox.Text = description.Value;
                }

                Word.Variable table = GetVariable(Document.Variables, Globals.ThisAddIn.TableVariableName);
                if (table != null)
                {
                    StringReader reader = new StringReader(table.Value);
                    powersDataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
                    reader.Close();
                }
            }
        }


        public void DeleteVariables()
        {
            if (Document.Variables.Count > 0)
            {
                titleTextBox.Text = string.Empty;
                dateTimePicker.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;

                Word.Variable title = GetVariable(Document.Variables, Globals.ThisAddIn.TitleVariableName);
                if (title != null)
                {
                    title.Delete();
                }

                Word.Variable date = GetVariable(Document.Variables, Globals.ThisAddIn.DateVariableName);
                if (date != null)
                {
                    date.Delete();
                }

                Word.Variable description = GetVariable(Document.Variables, Globals.ThisAddIn.DescriptionVariableName);
                if (description != null)
                {
                    description.Delete();
                }

                Word.Variable table = GetVariable(Document.Variables, Globals.ThisAddIn.TableVariableName);
                if (table != null)
                {
                    table.Delete();
                }
            }
        }

        public bool VariablesExitst()
        {
            if (Document.Variables.Count > 0)
            {
                Word.Variable title = GetVariable(Document.Variables, Globals.ThisAddIn.TitleVariableName);
                if (title != null)
                {
                    return true;
                }

                Word.Variable date = GetVariable(Document.Variables, Globals.ThisAddIn.DateVariableName);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = GetVariable(Document.Variables, Globals.ThisAddIn.DescriptionVariableName);
                if (description != null)
                {
                    return true;
                }

                Word.Variable table = GetVariable(Document.Variables, Globals.ThisAddIn.TableVariableName);
                if (table != null)
                {
                    return true;
                }                
            }
            return false;
        }

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

            Word.Variable title = GetVariable(Document.Variables, Globals.ThisAddIn.TitleVariableName);
            if (title == null)
            {
                Document.Variables.Add(Globals.ThisAddIn.TitleVariableName, textBox.Text);
            } else
            {
                title.Value = textBox.Text;
            }            
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker datePicker = (DateTimePicker)sender;
            Word.Variable date = GetVariable(Document.Variables, Globals.ThisAddIn.DateVariableName);
            if (date == null)
            {
                Document.Variables.Add(Globals.ThisAddIn.DateVariableName, datePicker.Value.ToShortDateString());
            }
            else
            {
                date.Value = datePicker.Value.ToShortDateString();
            }            
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Word.Variable description = GetVariable(Document.Variables, Globals.ThisAddIn.DescriptionVariableName);
            if (description == null)
            {
                Document.Variables.Add(Globals.ThisAddIn.DescriptionVariableName, textBox.Text);
            }
            else
            {
                description.Value = textBox.Text;
            }            
        }
    }
}
