using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Utils;

namespace WordHiddenPowers.Dialogs
{
	public partial class AnalyzerDialog : Form
	{
		private readonly Documents.Document document;

		public AnalyzerDialog(Documents.Document document)
		{
			this.Icon = WordUtil.GetIconMso(Const.Content.ANALAIZER_DIALOG_OFFICE_IMAGE_ID, SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			this.document = document;

			InitializeComponent();

			this.statusListBox.SelectedItemChanged += new System.EventHandler<ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.StatusListControl.ListItem>>(this.StatusListBox_SelectedItemChanged);

			statusListBox.DataSet = document.AggregatedDataSet;
			contentListBox.DataSet = document.AggregatedDataSet;
		}

		private void StatusListBox_SelectedItemChanged(object sender, ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.StatusListControl.ListItem> e)
		{
			contentListBox.Filer = e.Item.owner.Guid;
			categoryBox1.Owner = e.Item.owner;
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(this, "Создание чистого хранилища приведет к утрате данных. Создать хранилище данных?", "Анализ данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				document.AggregatedDataSet.RowsHeaders.Clear();
				document.AggregatedDataSet.ColumnsHeaders.Clear();

				document.AggregatedDataSet.Subcategories.Clear();
				document.AggregatedDataSet.Categories.Clear();

				document.AggregatedDataSet.DecimalPowers.Clear();
				document.AggregatedDataSet.TextPowers.Clear();
				
				document.AggregatedDataSet.WordFiles.Clear();
				document.AggregatedDataSet.DocumentKeys.Clear();

				document.AggregatedDataSet.AcceptChanges();
				ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
			}				
		}
		
		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Multiselect = false,
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				statusListBox.DataSet = null;
				contentListBox.DataSet = null;

				document.LoadAggregatedData(dialog.FileName);

				statusListBox.DataSet = document.AggregatedDataSet;
				contentListBox.DataSet = document.AggregatedDataSet;
			}
		}
		
		private void FileSave_Click(object sender, EventArgs e)
		{
			document.AggregatedDataSet.AcceptChanges();
			ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();
				Xml.SaveData(document.AggregatedDataSet, dialog.FileName);
			}
		}

		private void ViewHide_Click(object sender, EventArgs e)
		{
			contentListBox.Hide = mnuViewHide.Checked;
		}
		

		private void FileImportFromFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				//importDataSet = FileSystemUtil.ImportFiles(dialog.SelectedPath);
				ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
				document.Doc.Saved = false;
			}
		}

		private void FileImportFromFile_Click(object sender, EventArgs e)
		{

		}

		private void InsertToDocumentButton_Click(object sender, EventArgs e)
		{
			if (document.AggregatedDataSet.HasChanges())
			{
				DialogResult result = MessageBox.Show(this, "Для вставки данных в документ необходимо зафиксировать все изменения. Зафиксировать измененные данные?", "Анализ данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					document.AggregatedDataSet.AcceptChanges();
					ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
				}
				else
				{
					return;
				}
			}
			InsertDataPropertiesDialog dialog = new InsertDataPropertiesDialog();
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				WordUtil.InsertToWordDocument(
					sourceDocument: document,
					minRating: dialog.MinRating,
					maxRating: dialog.MaxRating,
					maxCount: dialog.NoteCount,
					viewHide: dialog.ViewHide);			
			}
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SplitContainer1_Panel2_Resize(object sender, EventArgs e)
		{
			categoryBox1.Location = new Point(0, 0);
			categoryBox1.Width = splitContainer1.Panel2.Width;
			contentListBox.Location = new Point(0, categoryBox1.Height);
			contentListBox.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - categoryBox1.Height);
		}
				
		private void AnalyzerDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (document.AggregatedDataSet.HasChanges())
				{
					DialogResult result = MessageBox.Show(this, "Зафиксировать измененные данные?", "Анализ данных", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						document.AggregatedDataSet.AcceptChanges();
						ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void FileExportTo_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_TSV_FILTER + "|" + Const.Globals.DIALOG_TXT_FILTER,
				FileName = "Content_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")				
			};
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();
			
				IEnumerable<string> content = dialog.FilterIndex == 1
					? document.AggregatedDataSet.GetMlModelDataSetTsvContent()
					: document.AggregatedDataSet.GetTxtContent() ;

				using (StreamWriter outputFile = new StreamWriter(dialog.FileName, false, System.Text.Encoding.GetEncoding(1251)))
				{
					foreach (string line in content)
					{
						outputFile.WriteLine(line);
					}
				}
				ShowDialogUtil.ShowMessageDialog("Экспорт данных завершен!");
			}
		}

		private void FileExportDictionary_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_DIC_FILTER,
				FileName = "MLModelDictionary" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")

			};
			if (Utils.ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();
				IList<string> result = new List<string>();
				foreach (string line in document.AggregatedDataSet.GetContent())
				{
					string[] buffer = line.Split(' ');
					foreach (string word in buffer)
					{
						if (!string.IsNullOrWhiteSpace(word)
							&& word.Length > 2
							&& !result.Contains(word))
						{
							result.Add(word);
						}						
					}
				}

				using (StreamWriter outputFile = new StreamWriter(dialog.FileName, false, System.Text.Encoding.GetEncoding(1251)))
				{
					foreach (string line in result.OrderBy(s=>s))
					{
						outputFile.WriteLine(line);
					}
				}
			}
		}
	}
}
