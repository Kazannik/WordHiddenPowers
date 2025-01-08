using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
	public partial class WordHiddenPowersRibbon
	{
		private const string DIALOG_FILTER = "XML Schema File (.xsd)|*.xsd";

		private Documents.Document ActiveDocument
		{
			get { return Globals.ThisAddIn.Documents.ActiveDocument; }
		}

		private Word.Selection Selection
		{
			get { return Globals.ThisAddIn.Application.ActiveWindow.Selection; }
		}

		private void NewPowers_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument?.NewData();
		}

		private void OpenPowers_Click(object sender, RibbonControlEventArgs e)
		{
			if (ActiveDocument != null)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = DIALOG_FILTER
				};
				if (Utils.ShowDialogUtil.ShowDialogO(dialog) == DialogResult.OK)
				{
					ActiveDocument.LoadData(dialog.FileName);
				}
			}
		}

		private void SavePowers_Click(object sender, RibbonControlEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = DIALOG_FILTER
			};
			if (Utils.ShowDialogUtil.ShowDialogO(dialog) == DialogResult.OK)
			{
				ActiveDocument.CommitVariables();
				ActiveDocument.SaveData(dialog.FileName);
			}
		}


		private void DeletePowers_Click(object sender, RibbonControlEventArgs e)
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
					MessageBox.Show("Дополнительные данные отсуствуют!",
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

		private void AnalizerImport_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ImportDataFromWordDocuments();
		}

		private void AnalizerTableViewer_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowTableViewerDialog();
		}

		private void AnalizerDialog_Click(object sender, RibbonControlEventArgs e)
		{
			ActiveDocument.ShowAnalizerDialog();
		}

		private enum NoteType
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
	}
}
