using System;
using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;


namespace WordHiddenPowers.Dialogs
{
    public partial class CreateTableDialog : Form
    {

        WordHiddenPowersPane pane;

        public CreateTableDialog(WordHiddenPowersPane pane)
        {
            this.pane = pane;

            InitializeComponent();
                        
            ReadTableStructure();
        }

        private void FileNew_Click(object sender, EventArgs e)
        {
            TableSettingDialog dialog = new TableSettingDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CreateTable(dialog.ColumnsCount, dialog.RowsCount);
            }            
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            SaveTableStructure();
        }


        private void CreateTable(int columnsCount, int rowsCount)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("HEADER", "");
            object[] array = new object[columnsCount + 1];
            array[0] = string.Empty;

            for (int c = 1; c <= columnsCount; c++)
            {
                int columnIndex = dataGridView.Columns.Add("HEADER " + c.ToString(), "");
                dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                array[c] = "Графа " + c.ToString(); 
            }
            
            int rowIndex = dataGridView.Rows.Add(array);
            dataGridView.Rows[rowIndex].Cells["HEADER"].ReadOnly = true;
            dataGridView.Rows[rowIndex].Cells["HEADER"].Style.BackColor = dataGridView.BackgroundColor;

            for (int r = 0; r < rowsCount; r++)
            {
                dataGridView.Rows.Add("Строка " + (r + 1).ToString());
            }
                       
            for (int r = 1; r < dataGridView.Rows.Count; r++)
            {
                for (int c = 1; c < dataGridView.Columns.Count; c++)
                {
                    dataGridView.Rows[r].Cells[c].ReadOnly = true;
                    dataGridView.Rows[r].Cells[c].Style.BackColor = System.Drawing.SystemColors.Control;
                }
            }
        }

        private void ReadTableStructure()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            if (pane.PowersDataSet.ColumnsHeaders.Rows.Count>0)
            {
                dataGridView.Columns.Add("HEADER", "");
                int rowIndex = dataGridView.Rows.Add();
                dataGridView.Rows[rowIndex].Cells["HEADER"].ReadOnly = true;
                dataGridView.Rows[rowIndex].Cells["HEADER"].Style.BackColor = dataGridView.BackgroundColor;

                for (int i = 0; i < pane.PowersDataSet.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = pane.PowersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                    int columnIndex = dataGridView.Columns.Add(text,  "");
                    dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView.Rows[rowIndex].Cells[i + 1].Value = text;
                }                

                foreach (DataRow item in pane.PowersDataSet.RowsHeaders.Rows)
                {
                    rowIndex = dataGridView.Rows.Add(item["Header"].ToString());

                    for (int i = 0; i < pane.PowersDataSet.ColumnsHeaders.Rows.Count; i++)
                    {
                        dataGridView.Rows[rowIndex].Cells[i + 1].ReadOnly = true;
                        dataGridView.Rows[rowIndex].Cells[i + 1].Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }            
        }


        private void SaveTableStructure()
        {
            dataGridView.EndEdit();

            if (pane.PowersDataSet.RowsHeaders.Rows.Count != (dataGridView.Rows.Count - 1) ||
                    pane.PowersDataSet.ColumnsHeaders.Rows.Count != (dataGridView.Columns.Count - 1))
            {
                Word.Variable variable = HiddenPowerDocument.GetVariable(pane.Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (variable != null)
                {
                    variable.Delete();
                }
            }

            pane.PowersDataSet.ColumnsHeaders.Clear();

            for (int i = 1; i < dataGridView.Columns.Count; i++)
            {
                string text = dataGridView.Rows[0].Cells[i].Value.ToString();
                pane.PowersDataSet.ColumnsHeaders.Rows.Add(new object[] { null, text });
            }

            pane.PowersDataSet.RowsHeaders.Clear();

            for (int i = 1; i < dataGridView.Rows.Count; i++)
            {
                pane.PowersDataSet.RowsHeaders.Rows.Add(new object[] { null, dataGridView.Rows[i].Cells["HEADER"].Value.ToString() });
            }

            pane.CommitVariables();            
        }
                
        private void Delete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (e.RowIndex != 0 && e.ColumnIndex != 0)
            {
                buttonBold.Enabled = false;
            }
            else 
            {
                buttonBold.Enabled = true;
                if (e.RowIndex == 0)
                {
                    buttonBold.Checked = clickedCell.Style.Font.Bold;
                }
            }


            if (e.Button == MouseButtons.Right)
            {
                if (clickedCell.RowIndex <= 0 && clickedCell.ColumnIndex <= 0 )
                {
                    toolStripMenuItem2.Visible = false;
                    mnuTableDelete.Visible = false;
                    toolStripMenuItem3.Visible = false;
                    cmnuTableDelete.Visible = false;
                    
                }
                

                // Here you can do whatever you want with the cell
                dataGridView.CurrentCell = clickedCell;  // Select the clicked cell, for instance

                // Get mouse position relative to the vehicles grid
                var relativeMousePosition = dataGridView.PointToClient(Cursor.Position);

                // Show the context menu
                this.contextMenuStrip1.Show(dataGridView, relativeMousePosition);
            }
        }

        private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (dataGridView.Rows.Count == 0 && dataGridView.Columns.Count ==0)
                {
                    e.Cancel = false;
                    return;
                }
                
                bool edited = false;
                if (pane.PowersDataSet.RowsHeaders.Rows.Count != (dataGridView.Rows.Count - 1) ||
                    pane.PowersDataSet.ColumnsHeaders.Rows.Count != (dataGridView.Columns.Count - 1))
                {
                    edited = true;
                }
                else
                {
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        string gridText = dataGridView.Rows[0].Cells[i].Value.ToString();
                        string dataText = pane.PowersDataSet.ColumnsHeaders.Rows[i - 1]["Header"].ToString();
                        if (!gridText.Equals(dataText))
                        {
                            edited = true;
                            break;
                        }
                    }
                    for (int i = 1; i < dataGridView.Rows.Count; i++)
                    {
                        string gridText = dataGridView.Rows[i].Cells["HEADER"].Value.ToString();
                        string dataText = pane.PowersDataSet.RowsHeaders.Rows[i - 1]["Header"].ToString();
                        if (!gridText.Equals(dataText))
                        {
                            edited = true;
                            break;
                        }
                    }
                }

                if (edited)
                {
                    DialogResult result = MessageBox.Show("Сохранить макет таблицы?", "Макет таблицы", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SaveTableStructure();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void mnuDeleteTable_ButtonClick(object sender, EventArgs e)
        {
            mnuDeleteTable.ShowDropDown();
        }

        private void Bold_Click(object sender, EventArgs e)
        {

        }
    }
}
