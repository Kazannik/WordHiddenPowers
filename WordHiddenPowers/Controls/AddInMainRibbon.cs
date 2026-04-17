using System;
using System.ComponentModel;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;

namespace WordHiddenPowers
{
	/// <summary>
	/// [TypeDescriptionProvider(typeof(AbstractCommunicatorProvider))]
	/// </summary>
	public partial class AddInMainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{

		#region CONTENT

		private void NewContent_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument?.NewData();
			Globals.ThisAddIn.Documents.SetToolBarState();
		}

		private void OpenContent_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument != null)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = Const.Globals.DIALOG_XML_FILTER,
					FilterIndex = 2
				};
				if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
				{
					switch (dialog.FilterIndex)
					{
						case 1:
							Globals.ThisAddIn.ActiveDocument.LoadCurrentData(dialog.FileName);
							break;
						case 2:
							Globals.ThisAddIn.ActiveDocument.LoadClearData(dialog.FileName);
							break;
						case 3:
							Globals.ThisAddIn.ActiveDocument.LoadCurrentData(dialog.FileName);
							break;
						case 4:
							Globals.ThisAddIn.ActiveDocument.LoadNowAggregatedData(dialog.FileName);
							break;
						case 5:
							Globals.ThisAddIn.ActiveDocument.LoadLastAggregatedData(dialog.FileName);
							break;
						case 6:
							Globals.ThisAddIn.ActiveDocument.LoadVectorData(dialog.FileName);
							break;
						default:
							break;
					}
				}
			}
			Globals.ThisAddIn.Documents.SetToolBarState();
		}

		private void SaveContent_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = Const.Globals.DIALOG_XML_FILTER,
				FilterIndex = 2
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				Globals.ThisAddIn.ActiveDocument.CommitVariables();
				switch (dialog.FilterIndex)
				{
					case 1:
						Globals.ThisAddIn.ActiveDocument.SaveSchema(dialog.FileName);
						break;
					case 2:
						Globals.ThisAddIn.ActiveDocument.SaveClearData(dialog.FileName);
						break;
					case 3:
						Globals.ThisAddIn.ActiveDocument.SaveCurrentData(dialog.FileName);
						break;
					case 4:
						Globals.ThisAddIn.ActiveDocument.SaveNowAggregatedData(dialog.FileName);
						break;
					case 5:
						Globals.ThisAddIn.ActiveDocument.SaveLastAggregatedData(dialog.FileName);
						break;
					case 6:
						Globals.ThisAddIn.ActiveDocument.SaveVectorData(dialog.FileName);
						break;
					default:
						break;
				}
			}
		}

		private void DeleteContent_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
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
			Globals.ThisAddIn.Documents.SetToolBarState();
		}

		private void ContentGroup_DialogLauncherClick(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument != null)
			{
				Form dialog = new ContentPrintDialog(Globals.ThisAddIn.ActiveDocument);
				Utils.Dialogs.ShowDialog(dialog);
			}
		}

		#endregion

		private void CreateTable_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowCreateTableDialog();
			Globals.ThisAddIn.Documents.SetToolBarState();
		}

		private void EditTable_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditTableDialog();
		}

		private void EditCategories_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditCategoriesDialog();
		}

		private void EditDocumentKeys_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowDocumentKeysDialog();
		}

		private void AnalizerImportFolder_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocuments();
		}

		private void AnalizerImportFile_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocument();
		}

		private void AnalizerImportOldFolder_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocumentsFolder();
		}

		private void AnalizerImportOldFile_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocument();
		}

		private void AnalizerTableViewer_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowTableViewerDialog();
		}

		private void AnalizerDialog_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowAnalyzerDialog();
		}

		private enum NoteType : int
		{
			Text = 0,
			Decimal = 1
		}

		private NoteType lastNoteType = NoteType.Text;

		private void AddTextNote_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
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

		private void AddDecimalNote_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
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

		private void AddLastNoteType_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null
				&& lastNoteType == NoteType.Text)
				Globals.ThisAddIn.ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
			else
				Globals.ThisAddIn.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void SearchService_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowSearchServiceDialog();
		}

		private void AiService_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.Documents.Ai(systemMessage: "Ты юрист; изложи текст коротко, в официальном стиле.");
		}

		private void LLMGroup_DialogLauncherClick(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Services.OpenAIService.ShowSettingDialog(Globals.ThisAddIn.ActiveDocument);
			LLMButtonUpdate();
		}


		public void LLMButtonUpdate()
		{
			llmButton1.Label = Services.OpenAIService.CaptionButton1;
			llmButton1.SuperTip = $"Системный промпт: {Services.OpenAIService.SystemMessageButton1}";

			llmButton2.Label = Services.OpenAIService.CaptionButton2;
			llmButton2.SuperTip = $"Системный промпт: {Services.OpenAIService.SystemMessageButton2}";
		}

		private void LLMButton1_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Replace, systemMessage: Properties.Settings.Default.LLMSystemMessage1, userMessage: Properties.Settings.Default.LLMPrefixUserMessage1);
		}

		private void LLMButton2_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.Documents.AiShow(systemMessage: Properties.Settings.Default.LLMSystemMessage2, userMessage: Properties.Settings.Default.LLMPrefixUserMessage2);
		}

		private void LLMReplaceButton_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.Documents.Ai();
		}

		private void LLMChatButton_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.Documents.AiEmbedShow();


			//Dialogs.LLMChatDialog chatDialog = new Dialogs.LLMChatDialog();
			//if (Globals.ThisAddIn.Selection != null &&
			//	Utils.Dialogs.ShowDialog(chatDialog) == DialogResult.OK)
			//{
			//	Globals.ThisAddIn.Documents.Ai("Ты юрист; изложи текст развернуто, в официальном стиле.", chatDialog.UserMessage);
			//}
		}
	}

	public class AbstractCommunicatorProvider : TypeDescriptionProvider
	{
		public AbstractCommunicatorProvider() : base(TypeDescriptor.GetProvider(typeof(UserControl)))
		{
		}
		public override Type GetReflectionType(Type objectType, object instance) => typeof(UserControl);

		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			objectType = typeof(UserControl);
			return base.CreateInstance(provider, objectType, argTypes, args);
		}
	}

}
