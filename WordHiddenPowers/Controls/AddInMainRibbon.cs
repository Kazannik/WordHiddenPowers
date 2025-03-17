using Microsoft.Office.Tools.Ribbon;
using System;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
	public partial class AddInMainRibbon
	{

		private Documents.Document ActiveDocument
		{
			get { return Globals.ThisAddIn.Documents.ActiveDocument; }
		}

		private Word.Selection Selection
		{
			get { return Globals.ThisAddIn.Application.ActiveWindow?.Selection; }
		}

		private void NewContent_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument?.NewData();
		}

		private void OpenContent_Click(object sender, RibbonControlEventArgs e)
		{
			if (ActiveDocument != null)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = Const.Globals.DIALOG_XML_FILTER
				};
				if (Utils.ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
				{
					ActiveDocument.LoadCurrentData(dialog.FileName);
				}
			}
		}

		private void SaveContent_Click(object sender, RibbonControlEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (Utils.ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				ActiveDocument.CommitVariables();
				ActiveDocument.SaveData(dialog.FileName);
			}
		}

		private void DeleteContent_Click(object sender, RibbonControlEventArgs e)
		{
			if (ActiveDocument != null)
			{
				if (ActiveDocument.VariablesExists())
				{
					if (MessageBox.Show("Удалить дополнительные данные из документа?",
						"Удаление скрытых данных",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question,
						MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						ActiveDocument.DeleteVariables();
					}
				}
				else
				{
					MessageBox.Show("Дополнительные данные отсутствую!",
						"Удаление скрытых данных",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
			}
		}

		private void CreateTable_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowCreateTableDialog();
		}

		private void EditTable_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowEditTableDialog();
		}

		private void EditCategories_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowEditCategoriesDialog();
		}

		private void EditDocumentKeys_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowDocumentKeysDialog();
		}

		private void AnalizerImportFolder_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ImportDataFromWordDocuments();
		}

		private void AnalizerImportFile_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ImportDataFromWordDocument();
		}

		private void AnalizerTableViewer_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowTableViewerDialog();
		}

		private void AnalizerDialog_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowAnalyzerDialog();
		}

		private enum NoteType : int
		{
			Text = 0,
			Decimal = 1
		}

		private NoteType lastNoteType = NoteType.Text;

		private void AddTextNote_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.AddTextNote(Selection);

			lastNoteType = NoteType.Text;
			addLastNoteTypeButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
			addLastNoteTypeButton.Label = Const.Content.TEXT_NOTE_LABEL;
			addLastNoteTypeButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
			addLastNoteTypeButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
			addLastNoteTypeButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;
		}

		private void AddDecimalNote_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.AddDecimalNote(Selection);
			
			lastNoteType = NoteType.Decimal;
			addLastNoteTypeButton.Description = Const.Content.DECIMAL_NOTE_DESCRIPTION;
			addLastNoteTypeButton.Label = Const.Content.DECIMAL_NOTE_LABEL;
			addLastNoteTypeButton.OfficeImageId = Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID;
			addLastNoteTypeButton.ScreenTip = Const.Content.DECIMAL_NOTE_SCREEN_TIP;
			addLastNoteTypeButton.SuperTip = Const.Content.DECIMAL_NOTE_SUPER_TIP;
		}

		private void AddLastNoteType_Click(object sender, RibbonControlEventArgs e)
		{
			if (lastNoteType == NoteType.Text)
				ActiveDocument.AddTextNote(Selection);
			else
				ActiveDocument.AddDecimalNote(Selection);
		}

		private void SearchService_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowSearchServiceDialog();
		}

		private void AiService_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.AiSearchService();
		}
	}
}
