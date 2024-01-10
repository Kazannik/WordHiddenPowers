using System;
using System.Data;
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
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("HEADER","");
            dataGridView1.Columns.Add("HEADER 1", "");
            dataGridView1.Columns.Add("HEADER 2", "");
            dataGridView1.Columns.Add("HEADER 3", "");

            dataGridView1.Rows.Add(new object[] {"", "Графа 1", "Графа 2" , "Графа 3" });

            dataGridView1.Rows.Add("Строка 1");
            dataGridView1.Rows.Add("Строка 2");
            dataGridView1.Rows.Add("Строка 3");
            dataGridView1.Rows.Add("Строка 4");
            dataGridView1.Rows.Add("Строка 5");
            dataGridView1.Rows.Add("Строка 6");
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            powersDataSet.ColumnsHeaders.Clear();

            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                string text = dataGridView1.Rows[0].Cells[i].Value.ToString();
                powersDataSet.ColumnsHeaders.Rows.Add(new object[] { null, text });
            }
                                   
            powersDataSet.RowsHeaders.Clear();

            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                powersDataSet.RowsHeaders.Rows.Add(new object[] { null, dataGridView1.Rows[i].Cells["HEADER"].Value.ToString()});
            }
                        

            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            powersDataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);

            Word.Variable table = GetVariable(document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (table !=null)
            {
                table.Value = builder.ToString();
            }
            else
            {
                document.Variables.Add(Const.Globals.CATEGORIES_VARIABLE_NAME, builder.ToString());
            }
            writer.Close();
        }


        private void ReadTableStructure()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            if (powersDataSet.ColumnsHeaders.Rows.Count>0)
            {
                dataGridView1.Columns.Add("HEADER", "");
                int rowIndex = dataGridView1.Rows.Add();
                for (int i = 0; i < powersDataSet.ColumnsHeaders.Rows.Count; i++)
                {
                    string text = powersDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
                    dataGridView1.Columns.Add(text,  "");
                    dataGridView1.Rows[rowIndex].Cells[i + 1].Value = text;
                }

                foreach (DataRow item in powersDataSet.RowsHeaders.Rows)
                {
                    rowIndex = dataGridView1.Rows.Add(item["Header"].ToString());

                    for (int i = 0; i < powersDataSet.ColumnsHeaders.Rows.Count; i++)
                    {
                        dataGridView1.Rows[rowIndex].Cells[i + 1].ReadOnly = true;
                        dataGridView1.Rows[rowIndex].Cells[i + 1].Style.BackColor = System.Drawing.SystemColors.Control;
                    }
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
