using System;
using System.Windows.Forms;
using WordHiddenPowers.Panes;

namespace WordHiddenPowers.Dialogs
{
    public partial class DocumentKeysDialog : Form
    {
        WordHiddenPowersPane pane;

        public DocumentKeysDialog(WordHiddenPowersPane pane)
        {
            this.pane = pane;

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
            for (int i = 0; i < pane.PowersDataSet.DocumentKeys.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(result))
                    result += Environment.NewLine;
                result += pane.PowersDataSet.DocumentKeys.Rows[i]["Caption"].ToString();
            }
            collectionTextBox1.Text = result;
        }
        
        private void WriteDocumentKeysCollection()
        {
            string[] lines = collectionTextBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            pane.PowersDataSet.DocumentKeys.Clear();

            foreach (string item in lines)
            {
                pane.PowersDataSet.DocumentKeys.Rows.Add(new object[] { null, item });
            }

            pane.CommitVariables();
        }        
    }
}
