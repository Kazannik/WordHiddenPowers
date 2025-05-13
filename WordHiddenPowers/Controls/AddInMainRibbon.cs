using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
	public partial class AddInMainRibbon
	{
		private void NewContent_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument?.NewData();
		}

		private void OpenContent_Click(object sender, RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument != null)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = Const.Globals.DIALOG_XML_FILTER
				};
				if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
				{
					Globals.ThisAddIn.ActiveDocument.LoadCurrentData(dialog.FileName);
				}
			}
		}

		private void SaveContent_Click(object sender, RibbonControlEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_XML_FILTER
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				Globals.ThisAddIn.ActiveDocument.CommitVariables();
				Globals.ThisAddIn.ActiveDocument.SaveData(dialog.FileName);
			}
		}

		private void DeleteContent_Click(object sender, RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument != null)
			{
				if (Globals.ThisAddIn.ActiveDocument.VariablesExists())
				{
					if (MessageBox.Show("Удалить дополнительные данные из документа?",
						"Удаление скрытых данных",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question,
						MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						Globals.ThisAddIn.ActiveDocument.DeleteVariables();
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
			Globals.ThisAddIn.ActiveDocument.ShowCreateTableDialog();
		}

		private void EditTable_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditTableDialog();
		}

		private void EditCategories_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditCategoriesDialog();
		}

		private void EditDocumentKeys_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowDocumentKeysDialog();
		}

		private void AnalizerImportFolder_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocuments();
		}

		private void AnalizerImportFile_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocument();
		}

		private void AnalizerImportOldFolder_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocuments();
		}

		private void AnalizerImportOldFile_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocument();
		}

		private void AnalizerTableViewer_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowTableViewerDialog();
		}

		private void AnalizerDialog_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowAnalyzerDialog();
		}

		private enum NoteType : int
		{
			Text = 0,
			Decimal = 1
		}

		private NoteType lastNoteType = NoteType.Text;

		private void AddTextNote_Click(object sender, RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);

			lastNoteType = NoteType.Text;
			addLastNoteTypeButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
			addLastNoteTypeButton.Label = Const.Content.TEXT_NOTE_LABEL;
			addLastNoteTypeButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
			addLastNoteTypeButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
			addLastNoteTypeButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;
		}

		private void AddDecimalNote_Click(object sender, RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);

			lastNoteType = NoteType.Decimal;
			addLastNoteTypeButton.Description = Const.Content.DECIMAL_NOTE_DESCRIPTION;
			addLastNoteTypeButton.Label = Const.Content.DECIMAL_NOTE_LABEL;
			addLastNoteTypeButton.OfficeImageId = Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID;
			addLastNoteTypeButton.ScreenTip = Const.Content.DECIMAL_NOTE_SCREEN_TIP;
			addLastNoteTypeButton.SuperTip = Const.Content.DECIMAL_NOTE_SUPER_TIP;
		}

		private void AddLastNoteType_Click(object sender, RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null 
				&& lastNoteType == NoteType.Text)
				Globals.ThisAddIn.ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
			else
				Globals.ThisAddIn.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void SearchService_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowSearchServiceDialog();
		}

		private void AiService_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.AiSearchService();
		}

		private void NotesGroup_DialogLauncherClick(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowNotesSettingDialog();
		}
	}
}
