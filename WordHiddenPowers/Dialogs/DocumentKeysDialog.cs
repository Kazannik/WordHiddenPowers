using System;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Dialogs
{
	public partial class DocumentKeysDialog : Form
	{
		private readonly RepositoryDataSet DataSet;

		public DocumentKeysDialog(RepositoryDataSet dataSet)
		{
			DataSet = dataSet;

			InitializeComponent();

			ReadDocumentKeysCollection();
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			WriteDocumentKeysCollection();
		}

		private void ReadDocumentKeysCollection()
		{
			collectionTextBox.Text = string.Empty;
			string result = string.Empty;
			for (int i = 0; i < DataSet.DocumentKeys.Rows.Count; i++)
			{
				if (!string.IsNullOrEmpty(result))
					result += Environment.NewLine;
				result += DataSet.DocumentKeys.Rows[i]["Caption"].ToString();
			}
			collectionTextBox.Text = result;
		}

		private void WriteDocumentKeysCollection()
		{
			string[] lines = collectionTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			DataSet.DocumentKeys.Clear();

			foreach (string item in lines)
			{
				DataSet.DocumentKeys.Rows.Add(new object[] { null, item });
			}
		}
	}
}
