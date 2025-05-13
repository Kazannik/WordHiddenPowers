using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Controls;
using WordHiddenPowers.Utils;
using WordHiddenPowers.Utils.WordDocuments;

namespace WordHiddenPowers.Dialogs
{
	public partial class NotesSettingDialog : Form
	{
		private readonly Documents.Document document;

		/// <summary>
		/// public static string MLNetModelPath = Path.Combine(Utils.FileSystem.UserDirectory.FullName, "LbfgsMaximumEntropyMulti_26.04.2025.mlnet");
		/// </summary>
		/// <param name="document"></param>



		public NotesSettingDialog(Documents.Document document)
		{
			this.document = document;
			string mlNetModelName = document.MLModelName;

			InitializeComponent();

			DirectoryInfo directory = new DirectoryInfo(FileSystem.UserDirectory.FullName);

			int index = -1;
			foreach (FileInfo file in directory.GetFiles("*.mlnet")) {
				int id = mlNetModelNameComboBox.Items.Add(file.Name);
				
				if (mlNetModelName == file.Name)
				{
					index = id;
				}
			}
			mlNetModelNameComboBox.SelectedIndex = index;			
		}

		public string MLNetModelPath { get; private set; }

		private void MlNetModelNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			if (comboBox.SelectedIndex >= 0)
			{ 
				MLNetModelPath = Path.Combine(FileSystem.UserDirectory.FullName, comboBox.SelectedItem as string);			
			}
			else
			{
				MLNetModelPath = string.Empty;
			}
		}

		private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SaveValues();
			}
			else if (e.CloseReason == CloseReason.UserClosing &&
				!string.IsNullOrEmpty(MLNetModelPath) &&
				document.MLModelName != mlNetModelNameComboBox.SelectedItem as string) 
			{
				DialogResult result = MessageBox.Show(this, "Сохранить выбор ИИ модели?", "Выбор ИИ модели", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

		private void SaveValues()
		{
			document.MLModelName = mlNetModelNameComboBox.SelectedItem as string;
			document.CommitVariables();
			document.Doc.Saved = false;
		}
	}
}
