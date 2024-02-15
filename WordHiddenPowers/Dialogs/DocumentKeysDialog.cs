using System;
using System.Windows.Forms;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Dialogs
{
    public partial class DocumentKeysDialog : Form
    {
        private RepositoryDataSet DataSet;

        public DocumentKeysDialog(RepositoryDataSet dataSet)
        {
            DataSet = dataSet;

            InitializeComponent();

            ReadDocumentKeysCollection();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            WriteDocumentKeysCollection();
        }

        private void ReadDocumentKeysCollection()
        {
            collectionTextBox1.Text = string.Empty;
            string result = string.Empty;
            for (int i = 0; i < DataSet.DocumentKeys.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(result))
                    result += Environment.NewLine;
                result += DataSet.DocumentKeys.Rows[i]["Caption"].ToString();
            }
            collectionTextBox1.Text = result;
        }
        
        private void WriteDocumentKeysCollection()
        {
            string[] lines = collectionTextBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            DataSet.DocumentKeys.Clear();

            foreach (string item in lines)
            {
                DataSet.DocumentKeys.Rows.Add(new object[] { null, item });
            }
        }        
    }
}
