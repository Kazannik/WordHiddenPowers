using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TableViewerDialog : Form
    {
        private RepositoryDataSet dataSet;

        private List<Table> dataBase;

        private Table summTable;

        public TableViewerDialog(RepositoryDataSet dataSet)
        {
            InitializeComponent();

            nameLabel.Text = "";

            this.dataSet = dataSet;
            
            tableEditBox.DataSet = this.dataSet;

            InitializeDatabase();
        }

        
        private void InitializeDatabase()
        {
            if (dataSet == null) return;

            dataBase = new List<Table>();

            foreach (RepositoryDataSet.DocumentKeysRow row in  dataSet.DocumentKeys)
            {
                Table table = Table.Create(row.Description2, row.Caption, row.Description1);
                if (!table.IsEmpty) dataBase.Add(table);
            }

            if (dataBase.Count > 0)
            {
                summTable = dataBase[0].Clone();
                listView1.Items.Clear();
                foreach (Table table in dataBase)
                {
                    ListViewItem item = listView1.Items.Add(table.Caption);
                    item.SubItems.Add("0");
                    item.SubItems.Add("0");
                    item.Tag = table;

                    summTable = summTable + table;
                }

                dataBase.Add(summTable);

                ListViewItem summItem = listView1.Items.Add("Итого:");
                summItem.SubItems.Add("0");
                summItem.SubItems.Add("0");
                summItem.Tag = summTable;

                summItem.Selected = true;

                listView1.Refresh();
            }
        }

        private void tableEditBox_SelectedCell(object sender, Controls.TableEditBox.TableEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                Table table =(Table) item.Tag;
                item.SubItems[1].Text = table.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value.ToString();
                item.SubItems[2].Text = (((double)table.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value) * 100 / ((double)summTable.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value)).ToString("0.00");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Table table = (Table)listView1.SelectedItems[0].Tag;
                tableEditBox.Table = table;
                nameLabel.Text = listView1.SelectedItems[0].Text;
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
            }
        }        
    }
}
