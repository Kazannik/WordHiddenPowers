using System;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TableEditorDialog : Form
    {
        private Documents.Document document;

        public TableEditorDialog(Documents.Document document)
        {
            this.document = document;

            InitializeComponent();

            nameLabel.Text = this.document.Doc.Name;

            tableEditBox.DataSet = this.document.DataSet;

            ReadValues();
        }

        
        private void ReadValues()
        {
            Word.Variable variable = GetVariable(document.Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            if (variable != null)
            {
                tableEditBox.Table = Data.Table.Create(variable.Value);                
            } else
            {
                tableEditBox.Table = new Data.Table(tableEditBox.DataSet.RowsHeaders.Count, tableEditBox.DataSet.ColumnsHeaders.Count);
            }
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

        private void tableEditBox_ValueChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = tableEditBox.IsChanged;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            tableEditBox.ClearValues();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            tableEditBox.RefreshValues();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            tableEditBox.CommitValue();
        }

        private void TableEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (tableEditBox.IsChanged)
                {
                    DialogResult result = MessageBox.Show("Зафиксировать табличные данные?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        tableEditBox.CommitValue();

                        Word.Variable variable = GetVariable(document.Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                        if (variable != null)
                        {
                            variable.Value = tableEditBox.Table.ToString();
                        }
                        else
                        {
                            document.Doc.Variables.Add(Const.Globals.TABLE_VARIABLE_NAME, tableEditBox.Table.ToString());
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }                
            }
        }
    }
}
