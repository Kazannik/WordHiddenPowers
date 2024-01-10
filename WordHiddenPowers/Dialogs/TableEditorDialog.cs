using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TableEditorDialog : Form
    {
        Word.Document document;
        RepositoryDataSet powersDataSet;

        public TableEditorDialog(Word.Document Doc, RepositoryDataSet DataSet)
        {
            InitializeComponent();

            this.powersDataSet = DataSet;
            this.document = Doc;

            ReadTableStructure();
            ReadValues();
        }

        private void ReadTableStructure()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            if (powersDataSet.ColumnsHeaders.Rows.Count > 0)
            {
                for (int i = 0; i < powersDataSet.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = powersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                    int columnIndex = dataGridView1.Columns.Add(text, text);
                    dataGridView1.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dataGridView1.RowHeadersWidth = 120;
                foreach (DataRow item in powersDataSet.RowsHeaders.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].HeaderCell.Value = item["Header"].ToString();                    
                }
            }
        }


        private void ReadValues()
        {
            Word.Variable variable = GetVariable(document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            if (variable != null)
            {
                Data.Table table = Data.Table.Create(variable.Value);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        dataGridView1.Rows[r].Cells[c].Value = table.Rows[r][c].Value;
                    }
                }
            }
        }

        private void data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >=0 && e.RowIndex >= 0)
            {
                int value = 0;
                bool correct= int.TryParse( dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),out value);
                if (!correct)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }           
        }

        private void TableEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Data.Table table = new Data.Table(dataGridView1.Rows.Count, dataGridView1.ColumnCount);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        if (dataGridView1.Rows[r].Cells[c].Value != null)
                        {
                            table.Rows[r][c].Value = int.Parse(dataGridView1.Rows[r].Cells[c].Value.ToString());
                        }                        
                    }
                }

                Word.Variable variable = GetVariable(document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (variable != null)
                {
                    variable.Value = table.ToString();
                }
                else
                {
                    document.Variables.Add(Const.Globals.TABLE_VARIABLE_NAME, table.ToString());
                }
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

    }
}
