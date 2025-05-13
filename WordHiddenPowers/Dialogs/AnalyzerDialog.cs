using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Utils;
using static System.Net.Mime.MediaTypeNames;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;

namespace WordHiddenPowers.Dialogs
{
	public partial class AnalyzerDialog : Form
	{
		private readonly Documents.Document document;

		public AnalyzerDialog(Documents.Document document)
		{
			this.Icon = WordDocument.GetIconMso(Const.Content.ANALAIZER_DIALOG_OFFICE_IMAGE_ID, SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			this.document = document;

			InitializeComponent();

			this.Visible = false;
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
				Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
			}				
		}
		
		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Multiselect = false,
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
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
			Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();
				Xml.SaveData(document.AggregatedDataSet, dialog.FileName);
			}
		}

		private void ViewHide_Click(object sender, EventArgs e)
		{
			contentListBox.Hide = mnuViewHide.Checked;
		}

		private void ViewRefresh_Click(object sender, EventArgs e)
		{
			contentListBox.ReadData();
		}

		private void FileImportFromFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				//importDataSet = FileSystemUtil.ImportFiles(dialog.SelectedPath);
				Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
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
					Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
				}
				else
				{
					return;
				}
			}
			InsertDataPropertiesDialog dialog = new InsertDataPropertiesDialog();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				WordDocument.InsertToWordDocument(
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

		private void SplitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			statusListBox.Location = new Point(0, 0);
			statusListBox.Size = splitContainer1.Panel1.ClientSize;
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
						Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, document.AggregatedDataSet);
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void FileExportTo_ClickAsync(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_TSV_FILTER + "|" + Const.Globals.DIALOG_TXT_FILTER,
				FileName = "Content_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")				
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();

    			IEnumerable<string> contents = dialog.FilterIndex == 1
					? document.AggregatedDataSet.GetMlModelDataSetTsvContent(true)
					: document.AggregatedDataSet.GetTxtContent();

				IEnumerable<string> oldContent = dialog.FilterIndex == 1
					? document.OldAggregatedDataSet.GetMlModelDataSetTsvContent(false)
					: document.OldAggregatedDataSet.GetTxtContent();

				contents = contents.Union(oldContent);

				File.WriteAllLines(dialog.FileName, contents, System.Text.Encoding.GetEncoding(1251));

				MessageBox.Show(owner: this, text: "Экспорт данных завершен!", caption: Const.Globals.ADDIN_TITLE, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			}
		}

		private void FileExportDictionary_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_DIC_FILTER,
				FileName = "MLModelDictionary" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")

			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				document.CommitVariables();
				IList<string> result = new List<string>();

				GetDictionary(ref result, document.AggregatedDataSet);
				GetDictionary(ref result, document.OldAggregatedDataSet);

				using (StreamWriter outputFile = new StreamWriter(dialog.FileName, false, System.Text.Encoding.GetEncoding(1251)))
				{
					foreach (string line in result.OrderBy(s=>s))
					{
						outputFile.WriteLine(line);
					}
				}
				Utils.Dialogs.ShowMessageDialog("Экспорт словаря завершен!");
			}
		}
		
		private void GetDictionary(ref IList<string> list, RepositoryDataSet sourceDataSet)
		{			
			foreach (string line in sourceDataSet.GetContent())
			{
				string[] buffer = line.Split(' ');
				foreach (string word in buffer)
				{
					if (!string.IsNullOrWhiteSpace(word)
						&& word.Length > 2
						&& !list.Contains(word))
					{
						list.Add(word);
					}
				}
			}
		}

		private void AnalyzerDialog_Load(object sender, EventArgs e)
		{
			this.Height++;
			this.Visible = true;
		}
	}
}
