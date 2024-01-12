using System;
using System.Windows.Forms;
using WordHiddenPowers.Panes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TableEditorDialog : Form
    {
        WordHiddenPowersPane pane;

        public TableEditorDialog(WordHiddenPowersPane pane)
        {
            this.pane = pane;

            InitializeComponent();

            nameLabel.Text = this.pane.Document.Name;

            tableEditBox.PowersDataSet = this.pane.PowersDataSet;

            ReadValues();
        }

        
        private void ReadValues()
        {
            Word.Variable variable = GetVariable(pane.Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            if (variable != null)
            {
                tableEditBox.Table = Data.Table.Create(variable.Value);                
            } else
            {
                tableEditBox.Table = new Data.Table(tableEditBox.PowersDataSet.RowsHeaders.Count, tableEditBox.PowersDataSet.ColumnsHeaders.Count);
            }
        }
        
        private void TableEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            
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
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }

                Word.Variable variable = GetVariable(pane.Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (variable != null)
                {
                    variable.Value = tableEditBox.Table.ToString();
                }
                else
                {
                    pane.Document.Variables.Add(Const.Globals.TABLE_VARIABLE_NAME, tableEditBox.Table.ToString());
                }
            }
        }
    }
}
