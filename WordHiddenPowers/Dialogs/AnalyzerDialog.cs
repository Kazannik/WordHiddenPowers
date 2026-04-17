// Ignore Spelling: Dialogs

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Services;
using WordHiddenPowers.Utils;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using ListItem = WordHiddenPowers.Controls.ListControls.ContentListControl.ListItem;

namespace WordHiddenPowers.Dialogs
{
	public partial class AnalyzerDialog : Form
	{
		private readonly Documents.Document document;

		public AnalyzerDialog(Documents.Document document)
		{
			Icon = WordDocument.GetIconMso(Const.Content.ANALAIZER_DIALOG_OFFICE_IMAGE_ID, SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			this.document = document;

			InitializeComponent();

			Visible = false;
			statusListBox.SelectedItemChanged += new EventHandler<ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.StatusListControl.ListItem>>(StatusListBox_SelectedItemChanged);

			statusListBox.NowDataSet = document.NowAggregatedDataSet;
			statusListBox.LastDataSet = document.LastAggregatedDataSet;
			contentListBox.DataSet = document.NowAggregatedDataSet;
			contentListBox.ItemContentChanged += new EventHandler<ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.ContentListControl.ListItem>>(ContentListBox_ItemContentChanged);
		}

		private void ContentListBox_ItemContentChanged(object sender, ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.ContentListControl.ListItem> e)
		{
			statusListBox.Invalidate();
		}

		private void StatusListBox_SelectedItemChanged(object sender, ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.StatusListControl.ListItem> e)
		{
			contentListBox.Filter = e.Item.owner.Guid;
			categoryBox1.Owner = e.Item.owner;
		}

		private void StatusListBox_FirstViewItemChanged(object sender, ControlLibrary.Controls.ListControls.ItemEventArgs<Controls.ListControls.StatusListControl.ListItem> e)
		{
			if (e.Item != null && e.Item.IsCategory)
			{
				toolStripStatusLabel2.Text = e.Item.owner.Caption;
			}
			else if (e.Item != null && !e.Item.IsCategory)
			{
				if (statusListBox.NowDataSet.Subcategories.Exists(e.Item.owner.Guid))
				{
					RepositoryDataSet.SubcategoriesRow dataRow = (RepositoryDataSet.SubcategoriesRow)statusListBox.NowDataSet.Subcategories.GetRow(e.Item.owner.Guid);
					toolStripStatusLabel2.Text = dataRow.CategoriesRow.Caption;
				}
			}
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(this, "Создание чистого хранилища приведет к утрате данных. Создать хранилище данных?", "Анализ данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				document.NowAggregatedDataSet.RowsHeaders.Clear();
				document.NowAggregatedDataSet.ColumnsHeaders.Clear();

				document.NowAggregatedDataSet.Subcategories.Clear();
				document.NowAggregatedDataSet.Categories.Clear();

				document.NowAggregatedDataSet.DecimalNotes.Clear();
				document.NowAggregatedDataSet.TextNotes.Clear();

				document.NowAggregatedDataSet.WordFiles.Clear();
				document.NowAggregatedDataSet.DocumentKeys.Clear();

				document.NowAggregatedDataSet.AcceptChanges();
				Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, document.NowAggregatedDataSet);
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
				statusListBox.NowDataSet = null;
				contentListBox.DataSet = null;

				document.LoadNowAggregatedData(dialog.FileName);

				statusListBox.NowDataSet = document.NowAggregatedDataSet;
				contentListBox.DataSet = document.NowAggregatedDataSet;
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			document.NowAggregatedDataSet.AcceptChanges();
			Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, document.NowAggregatedDataSet);
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
				Xml.SaveData(document.NowAggregatedDataSet, dialog.FileName);
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
				Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, document.NowAggregatedDataSet);
				document.Doc.Saved = false;
			}
		}

		private void FileImportFromFile_Click(object sender, EventArgs e)
		{

		}

		private void InsertToDocumentButton_Click(object sender, EventArgs e)
		{
			if (document.NowAggregatedDataSet.HasChanges())
			{
				DialogResult result = MessageBox.Show(this, "Для вставки данных в документ необходимо зафиксировать все изменения. Зафиксировать измененные данные?", "Анализ данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					document.NowAggregatedDataSet.AcceptChanges();
					Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, document.NowAggregatedDataSet);
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
					allRating: dialog.AllRating,
					viewHide: dialog.ViewHide);
			}
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SplitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			//statusListBox.Location = new Point(0, 0);
			//statusListBox.Size = splitContainer1.Panel1.ClientSize;
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
				if (document.NowAggregatedDataSet.HasChanges())
				{
					DialogResult result = MessageBox.Show(this, "Зафиксировать измененные данные?", "Анализ данных", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						document.NowAggregatedDataSet.AcceptChanges();
						Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, document.NowAggregatedDataSet);
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
					? document.NowAggregatedDataSet.GetMlModelDataSetTsvContent(true)
					: document.NowAggregatedDataSet.GetTxtContent();

				IEnumerable<string> oldContent = dialog.FilterIndex == 1
					? document.LastAggregatedDataSet.GetMlModelDataSetTsvContent(false)
					: document.LastAggregatedDataSet.GetTxtContent();

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

				GetDictionary(ref result, document.NowAggregatedDataSet);
				GetDictionary(ref result, document.LastAggregatedDataSet);

				using (StreamWriter outputFile = new StreamWriter(dialog.FileName, false, System.Text.Encoding.GetEncoding(1251)))
				{
					foreach (string line in result.OrderBy(s => s))
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
			Height++;
			Visible = true;
		}

		private void ToolsDataEmbedding_Click(object sender, EventArgs e)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(
				OpenAIService.Uri,
				OpenAIService.Timeout,
				OpenAIService.EmbeddingLLMName,
				document.NowAggregatedDataSet,
				document.VectorDataSet, null);

			Utils.Dialogs.Show(llmDialog);
		}

		private void RatingBox_RatingChanged(object sender, EventArgs e)
		{
			contentListBox.FilterRating = toolStripRatingBox.Rating.Value;
			if (toolStripRatingBox.Rating.Value < 0)
				toolStripRatingBox.StarsColor = Const.Globals.COLOR_NEGATIVE_STAR_ICON;
			if (toolStripRatingBox.Rating.Value > 0)
				toolStripRatingBox.StarsColor = Const.Globals.COLOR_STAR_ICON;

		}

		private void ContentListBox_ItemMouseDown(object sender, ControlLibrary.Controls.ListControls.ItemMouseEventArgs<Controls.ListControls.ContentListControl.ListItem, Controls.ListControls.ContentListControl.ListItemNote> e)
		{
			if (e.Button == MouseButtons.Right)
			{
				noteContextMenu.Tag = e.Item;
				if (noteContextMenu.Tag is ListItem)
				{
					ListItem item = noteContextMenu.Tag as ListItem;
					if (!item.Hide)
						mnuHideContextMenu.Text = "Скрыть запись";
					else
						mnuHideContextMenu.Text = "Отразить запись";
				}
				noteContextMenu.Show(Cursor.Position);
			}
		}

		private void EditContentMenuItem_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				if (item.Note.IsText)
				{
					TextNoteDialog dialog = new TextNoteDialog(contentListBox.DataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						contentListBox.DataSet.TextNotes.Set(item.Note.Id,
							dialog.Category.Guid,
							dialog.Subcategory.Guid,
							dialog.Description,
							dialog.Value as string,
							dialog.Rating,
							dialog.SelectionStart,
							dialog.SelectionEnd,
							false);
						if (item.Note.Subcategory.Keywords != dialog.Subcategory.Keywords)
						{
							contentListBox.DataSet.Write(dialog.Subcategory);
						}
						document.CommitVariables();
					}
				}
				else
				{
					DecimalNoteDialog dialog = new DecimalNoteDialog(contentListBox.DataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						contentListBox.DataSet.DecimalNotes.Set(
							item.Note.Id,
							dialog.Category.Guid,
							dialog.Subcategory.Guid,
							dialog.Description,
							(double)dialog.Value,
							dialog.Rating,
							dialog.SelectionStart,
							dialog.SelectionEnd,
							false);
						document.CommitVariables();
					}
				}
			}
		}

		private void HideMenuItem_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				item.Hide = !item.Hide;
			}
		}

		private void NewTextContentMenuItem_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				TextNoteDialog dialog = new TextNoteDialog(contentListBox.DataSet, item.Note);
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					contentListBox.DataSet.TextNotes.Rows.Add(new object[]
					{
						null,
						dialog.Category.Guid,
						dialog.Subcategory.Guid,
						dialog.Description,
						dialog.Value,
						dialog.Rating,
						item.Note.WordSelectionStart,
						item.Note.WordSelectionEnd,
						item.Note.FileId
					});
					document.CommitVariables();
				}
			}
		}

		private void NewDecimalContentMenuItem_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				DecimalNoteDialog dialog = new DecimalNoteDialog(contentListBox.DataSet, item.Note);
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					contentListBox.DataSet.DecimalNotes.Rows.Add(new object[]
					{
						null,
						dialog.Category.Guid,
						dialog.Subcategory.Guid,
						dialog.Description,
						dialog.Value,
						dialog.Rating,
						item.Note.WordSelectionStart,
						item.Note.WordSelectionEnd,
						item.Note.FileId
					});
					document.CommitVariables();
				}
			}
		}

		private void CopyContentMenu_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				Clipboard.SetText(string.Format("{0}\n{1}\n{2}", item.Note.Category.Caption, item.Note.Subcategory.Caption, item.Note.Value));
			}
		}

		
	}
}
