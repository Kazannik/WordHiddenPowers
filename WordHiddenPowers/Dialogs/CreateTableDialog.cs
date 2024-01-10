using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;


namespace WordHiddenPowers.Dialogs
{
    public partial class CreateTableDialog : Form
    {

        Word.Document document;
        RepositoryDataSet powersDataSet;


        public CreateTableDialog(Word.Document Doc, RepositoryDataSet DataSet)
        {
            InitializeComponent();

            this.powersDataSet = DataSet;
            this.document = Doc;
            ReadTableStructure();
        }

        private void FileNew_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("HEADER","");
            dataGridView.Columns.Add("HEADER 1", "");
            dataGridView.Columns.Add("HEADER 2", "");
            dataGridView.Columns.Add("HEADER 3", "");

            int rowIndex = dataGridView.Rows.Add(new object[] {"", "Графа 1", "Графа 2" , "Графа 3" });
            dataGridView.Rows[rowIndex].Cells["HEADER"].ReadOnly = true;
            dataGridView.Rows[rowIndex].Cells["HEADER"].Style.BackColor = dataGridView.BackgroundColor;

            dataGridView.Rows.Add("Строка 1");
            dataGridView.Rows.Add("Строка 2");
            dataGridView.Rows.Add("Строка 3");
            dataGridView.Rows.Add("Строка 4");
            dataGridView.Rows.Add("Строка 5");
            dataGridView.Rows.Add("Строка 6");

            for (int r = 1; r < dataGridView.Rows.Count; r++)
            {
                for (int c = 1; c < dataGridView.Columns.Count; c++)
                {
                    dataGridView.Rows[r].Cells[c].ReadOnly = true;
                    dataGridView.Rows[r].Cells[c].Style.BackColor = System.Drawing.SystemColors.Control;
                }
            }
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            SaveTableStructure();
        }


        private void ReadTableStructure()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            if (powersDataSet.ColumnsHeaders.Rows.Count>0)
            {
                dataGridView.Columns.Add("HEADER", "");
                int rowIndex = dataGridView.Rows.Add();
                for (int i = 0; i < powersDataSet.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = powersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                    dataGridView.Columns.Add(text,  "");
                    dataGridView.Rows[rowIndex].Cells[i + 1].Value = text;
                }

                foreach (DataRow item in powersDataSet.RowsHeaders.Rows)
                {
                    rowIndex = dataGridView.Rows.Add(item["Header"].ToString());

                    for (int i = 0; i < powersDataSet.ColumnsHeaders.Rows.Count; i++)
                    {
                        dataGridView.Rows[rowIndex].Cells[i + 1].ReadOnly = true;
                        dataGridView.Rows[rowIndex].Cells[i + 1].Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }            
        }


        private void SaveTableStructure()
        {
            powersDataSet.ColumnsHeaders.Clear();

            for (int i = 1; i < dataGridView.Columns.Count; i++)
            {
                string text = dataGridView.Rows[0].Cells[i].Value.ToString();
                powersDataSet.ColumnsHeaders.Rows.Add(new object[] { null, text });
            }

            powersDataSet.RowsHeaders.Clear();

            for (int i = 1; i < dataGridView.Rows.Count; i++)
            {
                powersDataSet.RowsHeaders.Rows.Add(new object[] { null, dataGridView.Rows[i].Cells["HEADER"].Value.ToString() });
            }


            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            powersDataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);

            Word.Variable table = GetVariable(document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (table != null)
            {
                table.Value = builder.ToString();
            }
            else
            {
                document.Variables.Add(Const.Globals.CATEGORIES_VARIABLE_NAME, builder.ToString());
            }
            writer.Close();
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

        private void Delete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (clickedCell.RowIndex <= 0 && clickedCell.ColumnIndex <=0 )
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
                bool edited = false;
                if (powersDataSet.RowsHeaders.Columns.Count != dataGridView.Rows.Count ||
                    powersDataSet.ColumnsHeaders.Columns.Count != dataGridView.Columns.Count)
                {
                    edited = true;
                }
                else
                {
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        string gridText = dataGridView.Rows[0].Cells[i].Value.ToString();
                        string dataText = powersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                        if (!gridText.Equals(dataText))
                        {
                            edited = true;
                        }
                    }
                    powersDataSet.RowsHeaders.Clear();
                    for (int i = 1; i < dataGridView.Rows.Count; i++)
                    {
                        string gridText = dataGridView.Rows[i].Cells["HEADER"].Value.ToString();
                        string dataText = powersDataSet.RowsHeaders.Rows[i]["Header"].ToString();
                        if (!gridText.Equals(dataText))
                        {
                            edited = true;
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
    }
}
