using System;
using System.Windows.Forms;
using WordHiddenPowers.Repositories.Data;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableEditorDialog : Form
	{
		private readonly Documents.Document document;

		public TableEditorDialog(Documents.Document document)
		{
			this.document = document;

			InitializeComponent();

			nameLabel.Text = this.document.Doc.Name;

			tableEditBox.DataSet = this.document.CurrentDataSet;

			deleteButton.Enabled = Content.ExistsVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME);

			ReadValues();
		}

		private void ReadValues()
		{
			string tableContext = Content.GetVariableValueOrDefault(document.Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
			if (string.IsNullOrWhiteSpace(tableContext))
			{
				tableEditBox.Table = new Table(tableEditBox.DataSet.RowsHeaders.Count, tableEditBox.DataSet.ColumnsHeaders.Count);
			}
			else
			{
				tableEditBox.Table = Table.Create(tableContext);
			}
		}

		private void SaveValues()
		{
			tableEditBox.CommitValue();
			Content.CommitVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME, value: tableEditBox.Table.ToString());
			document.Doc.Saved = false;
		}

		private void TableEditBox_ValueChanged(object sender, EventArgs e)
		{
			saveButton.Enabled = tableEditBox.IsChanged;
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			tableEditBox.ClearValues();
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (Content.ExistsVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME))
			{
				DialogResult result = MessageBox.Show(this, "Удалить таблицу с данными из документа?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					Content.DeleteVariable(array: document.Doc.Variables, variableName: Const.Globals.TABLE_VARIABLE_NAME);
					Close();
				}
			}
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			tableEditBox.RefreshValues();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveValues();
		}

		private void TableEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				tableEditBox.EndEdit();
				
				if (tableEditBox.IsChanged)
				{
					DialogResult result = MessageBox.Show(this, "Зафиксировать табличные данные?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						SaveValues();
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
