using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using System;

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
                RefreshValues();
            }
        }

        private void ReadStructure()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            if (source == null) return;
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
        
        public void ClearValues()
        {
            dataGridView.CancelEdit();
            for (int r = 0; r < dataGridView.RowCount; r++)
            {
                for (int c = 0; c < dataGridView.ColumnCount; c++)
                {
                    dataGridView.Rows[r].Cells[c].Value = 0;
                }
            }
        }

        public void RefreshValues()
        {
            dataGridView.CancelEdit();
            if (table == null) return;
            for (int r = 0; r < table.Rows.Count; r++)
            {
                for (int c = 0; c < table.ColumnCount; c++)
                {
                    dataGridView.Rows[r].Cells[c].Value = table.Rows[r][c].Value;
                }
            }            
        }

        public void CommitValue()
        {
            dataGridView.CancelEdit();
            if (table == null) return;
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
            
            if (IsChanged == true)
            {
                IsChanged = false;
                DoValueChanged();
            }
        }

        public bool IsChanged { get; private set; }
              

        private void data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int value = 0;
                bool correct = int.TryParse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out value);
                if (!correct)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }

                bool changed = false;
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        if (dataGridView.Rows[r].Cells[c].Value != null)
                        {
                            if (table.Rows[r][c].Value != int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString()))
                            {                                
                                changed = true;
                                break;
                            }
                        }
                    }
                }

                if (IsChanged != changed)
                {
                    IsChanged = changed;
                    DoValueChanged();
                }
            }
        }

        public event EventHandler ValueChanged;

        public void DoValueChanged()
        {
            OnValueChanged(new EventArgs());
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }        
    }
}

