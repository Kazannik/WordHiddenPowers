using System.Data;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Controls
{
    public partial class TableEditBox : UserControl
    {
        private RepositoryDataSet source;
        private Data.Table table;

        public TableEditBox()
        {
            InitializeComponent();
        }
               
        public RepositoryDataSet PowersDataSet
        {
            get
            {
                return source;
            }
            
            set
            {
                source = value;
                ReadStructure();
            }            
        }
        
        public Data.Table Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
                ReadValues();
            }
        }

        private void ReadStructure()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            if (source.ColumnsHeaders.Rows.Count > 0)
            {
                for (int i = 0; i < source.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = source.ColumnsHeaders.Rows[i]["Header"].ToString();
                    int columnIndex = dataGridView.Columns.Add(text, text);
                    dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dataGridView.RowHeadersWidth = 120;
                foreach (DataRow item in source.RowsHeaders.Rows)
                {
                    int rowIndex = dataGridView.Rows.Add();
                    dataGridView.Rows[rowIndex].HeaderCell.Value = item["Header"].ToString();
                }
            }
        }
        
        private void ReadValues()
        {
            for (int r = 0; r < table.Rows.Count; r++)
            {
                for (int c = 0; c < table.ColumnCount; c++)
                {
                    dataGridView.Rows[r].Cells[c].Value = table.Rows[r][c].Value;
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
    }
}

