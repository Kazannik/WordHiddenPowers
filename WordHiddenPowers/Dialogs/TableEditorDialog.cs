using System;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Data;

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

            deleteButton.Enabled = Utils.HiddenPowerDocument.ExistsVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME);

            ReadValues();
        }
                
        private void ReadValues()
        {
            string tableContext = Utils.HiddenPowerDocument.GetVariableValue(document.Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            if (string.IsNullOrWhiteSpace(tableContext))
            {
                tableEditBox.Table = new Table(tableEditBox.DataSet.RowsHeaders.Count, tableEditBox.DataSet.ColumnsHeaders.Count);
            }
            else
            {
                tableEditBox.Table = Table.Create(tableContext);
            }
        }

        private void SaveValues()
        {
            tableEditBox.CommitValue();
            Utils.HiddenPowerDocument.CommitVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME, value: tableEditBox.Table.ToString());
            document.Doc.Saved = false;
        }
        
        private void tableEditBox_ValueChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = tableEditBox.IsChanged;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            tableEditBox.ClearValues();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (Utils.HiddenPowerDocument.ExistsVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME))
            {
                DialogResult result = MessageBox.Show(this, "Удалить таблицу с данными из документа?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Utils.HiddenPowerDocument.DeleteVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME);
                    Close();
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            tableEditBox.RefreshValues();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveValues();
        }

        private void TableEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (tableEditBox.IsChanged)
                {
                    DialogResult result = MessageBox.Show(this, "Зафиксировать табличные данные?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SaveValues();
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
