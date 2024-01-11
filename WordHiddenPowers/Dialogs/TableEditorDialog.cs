using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositoryes;
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
                       
            ReadTableStructure();
            ReadValues();
        }

        private void ReadTableStructure()
        {
            pane.DataSetRefresh();

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            if (pane.PowersDataSet.ColumnsHeaders.Rows.Count > 0)
            {
                for (int i = 0; i < pane.PowersDataSet.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = pane.PowersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                    int columnIndex = dataGridView.Columns.Add(text, text);
                    dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dataGridView.RowHeadersWidth = 120;
                foreach (DataRow item in pane.PowersDataSet.RowsHeaders.Rows)
                {
                    int rowIndex = dataGridView.Rows.Add();
                    dataGridView.Rows[rowIndex].HeaderCell.Value = item["Header"].ToString();                    
                }
            }
        }


        private void ReadValues()
        {
            Word.Variable variable = GetVariable(pane.Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            if (variable != null)
            {
                Data.Table table = Data.Table.Create(variable.Value);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        dataGridView.Rows[r].Cells[c].Value = table.Rows[r][c].Value;
                    }
                }
            }
        }

        private void data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >=0 && e.RowIndex >= 0)
            {
                int value = 0;
                bool correct= int.TryParse( dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),out value);
                if (!correct)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }           
        }

        private void TableEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Data.Table table = new Data.Table(dataGridView.Rows.Count, dataGridView.ColumnCount);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        if (dataGridView.Rows[r].Cells[c].Value != null)
                        {
                            table.Rows[r][c].Value = int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString());
                        }                        
                    }
                }

                Word.Variable variable = GetVariable(pane.Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (variable != null)
                {
                    variable.Value = table.ToString();
                }
                else
                {
                    pane.Document.Variables.Add(Const.Globals.TABLE_VARIABLE_NAME, table.ToString());
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
