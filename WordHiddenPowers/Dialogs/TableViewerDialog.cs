using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TableViewerDialog : Form
    {
        public Table SumTable;

        public TableViewerDialog(RepositoryDataSet powersDataSet, Table table)
        {
            SumTable = table;

            InitializeComponent();

            nameLabel.Text = "";

            RepositoryDataSet dataSet = new RepositoryDataSet();

            string xml = GetXml(powersDataSet);
            SetXml(dataSet, xml);
            dataSet.DecimalPowers.Clear();
            dataSet.TextPowers.Clear();
            
            tableEditBox.DataSet = dataSet;
            
            tableEditBox.Table = SumTable;
        }

        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
        }

        private void SetXml(RepositoryDataSet dataSet, string xml)
        {
            StringReader reader = new StringReader(xml);
            dataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
            reader.Close();
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

                //Word.Variable variable = GetVariable(pane.Document.Variables, WordHiddenPowers.Const.Globals.TABLE_VARIABLE_NAME);
                //if (variable != null)
                //{
                //    variable.Value = tableEditBox.Table.ToString();
                //}
                //else
                //{
                //    pane.Document.Variables.Add(WordHiddenPowers.Const.Globals.TABLE_VARIABLE_NAME, tableEditBox.Table.ToString());
                //}
            }
        }
    }
}
