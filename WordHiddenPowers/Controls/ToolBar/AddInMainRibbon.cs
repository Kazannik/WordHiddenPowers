using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.EventsBus.EventArgs;
using WordHiddenPowers.Services;
using static WordHiddenPowers.Documents.Document;
using Office = Microsoft.Office.Core;
using Tools = Microsoft.Office.Tools;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
	/// <summary>
	/// [TypeDescriptionProvider(typeof(AbstractCommunicatorProvider))]
	/// </summary>
	public partial class AddInMainRibbon : Tools.Ribbon.RibbonBase
	{
		private Office.CommandBarButton buttonSelectDecimalCategory;
		private Office.CommandBarButton buttonSelectTextCategory;
		private Office.CommandBarButton buttonSelectChatMessage1;
		private Office.CommandBarButton buttonSelectChatMessage2;

		private bool isSelection = false;
		private bool isModel = false;

		private void GlobalsEventsBus_InitializeComponent(object sender, System.EventArgs e)
		{
			InitializePopumMenuButton();
		}

		private void InitializePopumMenuButton()
		{
			Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNote_Click);
			buttonSelectDecimalCategory = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNote_Click);

			buttonSelectChatMessage1 = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Globals.ThisAddIn.GlobalsSetting.CaptionButton1, Const.Content.LLM_BUTTON_IMAGE_ID, Const.Panes.BUTTON_PROMPT_01_TAG, true, ChatMessage1_Click);
			buttonSelectChatMessage2 = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Globals.ThisAddIn.GlobalsSetting.CaptionButton2, Const.Content.LLM_BUTTON_IMAGE_ID, Const.Panes.BUTTON_PROMPT_02_TAG, false, ChatMessage2_Click);
		}
				
		#region Global Events

		private void GlobalsEventsBus_DocumentPropertiesChanged(object sender, DocumentEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument.Hwnd == e.Document.Hwnd)
			{
				SetToolBarState(e.Document);
			}
		}

		private void GlobalsEventsBus_DocumentChatMessageModeChanged(object sender, DocumentChatMessageModeEventArgs e)
		{
			isSelection = e.MessageMode.HasFlag(ChatMessageModeEnum.ReplaceSelection);
			
			llmButton1.Enabled = llmButton2.Enabled = isSelection & isModel;
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_PROMPT_01_TAG, isSelection & isModel);
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_PROMPT_02_TAG, isSelection & isModel);
		}

		private void GlobalsEventsBus_NewDocument(object sender, WordDocumentEventArgs e)
		{
			SetToolBarState(Globals.ThisAddIn.Documents.GetDocument(e.Document));
		}

		private void GlobalsEventsBus_DocumentOpen(object sender, WordDocumentEventArgs e)
		{
			SetToolBarState(Globals.ThisAddIn.Documents.GetDocument(e.Document));
		}

		private void GlobalsEventsBus_DocumentWindowActivate(object sender, WordDocumentWindowActivateEventArgs e)
		{
			SetToolBarState(Globals.ThisAddIn.Documents.GetDocument(e.Document));
		}

		private void GlobalsEventsBus_DocumentWindowDeactivate(object sender, WordDocumentWindowDeactivateEventArgs e)
		{
			paneVisibleButton.Checked = false;
		}

		private void GlobalsEventsBus_DocumentBeforeClose(object sender, WordDocumentBeforeCloseEventArgs e)
		{
			SetToolBarState(Globals.ThisAddIn.ActiveDocument);
		}

		private void GlobalsEventsBus_AccessToSendingUserMessageStateChanged(object sender, AccessToSendingUserMessageEventArgs e)
		{
			isModel = e.AccessToSendingUserMessage.HasFlag(EventsBus.StateEnums.AccessToSendingUserMessage.SelectModel
				| EventsBus.StateEnums.AccessToSendingUserMessage.SelectDocument);

			llmButton1.Enabled = llmButton2.Enabled = isSelection & isModel;
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_PROMPT_01_TAG, isSelection & isModel);
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_PROMPT_02_TAG, isSelection & isModel);
		}

		#endregion

		internal void SetToolBarState(Document document)
		{
			paneVisibleButton.Checked = document != null && document.CustomPane != null && document.CustomPane.Visible;

			newDataButton.Enabled = openDataButton.Enabled = true;

			saveDataButton.Enabled = document != null && (deleteDataButton.Enabled = document.State != WordDocumentMode.Default);

			editCategoriesButton.Enabled =
			createTableButton.Enabled =
			editDocumentKeysButton.Enabled = document != null && document.State == WordDocumentMode.Separate;

			aggregatedTableViewerButton.Enabled = document != null && document.State == WordDocumentMode.Combine && document.NowAggregatedDataSet != null && document.NowAggregatedDataSet.IsTables;
			aggregatedDialogButton.Enabled = document != null && document.State == WordDocumentMode.Combine;

			aggregatedImportFolderButton.Enabled =
			aggregatedImportFileButton.Enabled =

			oldAggregatedImportFolderButton.Enabled =
			oldAggregatedImportFileButton.Enabled = document != null && document.State != WordDocumentMode.Separate;

			addLastNoteTypeButton.Enabled = document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any();
			addTextNoteButton.Enabled = document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any(x => x.IsText);
			addDecimalNoteButton.Enabled = document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any(x => x.IsDecimal);

			searchServiceButton.Enabled =
			aiServiceButton.Enabled = document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any();

			editTableButton.Enabled = document != null && document.IsTableSchema;

			SetContextMenuButtonEnabled(Const.Panes.BUTTON_STRING_TAG, document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any(x => x.IsText));
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_DECIMAL_TAG, document != null && document.State == WordDocumentMode.Separate && document.CurrentDataSet.Subcategories.Any(x => x.IsDecimal));
		}

		#region CONTENT

		private void NewContent_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument?.NewData();
		}

		private void OpenContent_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
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
		}

		private void SaveContent_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
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

		private void DeleteContent_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
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

		private void ContentGroup_DialogLauncherClick(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument != null)
			{
				Form dialog = new ContentPrintDialog(Globals.ThisAddIn.ActiveDocument);
				Utils.Dialogs.ShowDialog(dialog);
			}
		}

		#endregion

		private void CreateTable_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowCreateTableDialog();
		}

		private void EditTable_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditTableDialog();
		}

		private void EditCategories_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowEditCategoriesDialog();
		}

		private void EditDocumentKeys_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowDocumentKeysDialog();
		}

		private void AnalizerImportFolder_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocuments();
		}

		private void AnalizerImportFile_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportDataFromWordDocument();
		}

		private void AnalizerImportOldFolder_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocumentsFolder();
		}

		private void AnalizerImportOldFile_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ImportOldDataFromWordDocument();
		}

		private void AnalizerTableViewer_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowTableViewerDialog();
		}

		private void AnalizerDialog_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowAnalyzerDialog();
		}

		private enum NoteType : int
		{
			Text = 0,
			Decimal = 1
		}

		private NoteType lastNoteType = NoteType.Text;

		private void AddTextNote_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
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

		private void AddDecimalNote_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
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

		private void AddLastNoteType_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null
				&& lastNoteType == NoteType.Text)
				Globals.ThisAddIn.ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
			else
				Globals.ThisAddIn.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void AddTextNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
		}

		private void AddDecimalNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				Globals.ThisAddIn.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void ChatMessage1_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.ActiveDocument.ChatMessageMode.HasFlag(ChatMessageModeEnum.ReplaceSelection))
			{
				Globals.ThisAddIn.ActiveDocument.InsertChatMessage(
					model: Globals.ThisAddIn.ActiveDocument.Pane.SelectedModel,
					options: Globals.ThisAddIn.ActiveDocument.Pane.ChatOptions,
					mode: ChatMessageModeEnum.ReplaceSelection,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1,
					userMessages: [Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton1],
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton1);
			}
		}

		private void ChatMessage2_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.ActiveDocument.ChatMessageMode.HasFlag(ChatMessageModeEnum.ReplaceSelection))
			{
				Globals.ThisAddIn.ActiveDocument.InsertChatMessage(
					model: Globals.ThisAddIn.ActiveDocument.Pane.SelectedModel,
					options: Globals.ThisAddIn.ActiveDocument.Pane.ChatOptions,
					mode: ChatMessageModeEnum.ReplaceSelection,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2,
					userMessages: [Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton2],
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton2);
			}
		}

		private void SearchService_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ActiveDocument.ShowSearchServiceDialog();
		}

		private void AiService_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			//if (Globals.ThisAddIn.Selection != null)
			//	Globals.ThisAddIn.Documents.Ai(systemMessage: "Ты юрист; изложи текст коротко, в официальном стиле.");



		}

		private void LLMGroup_DialogLauncherClick(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			LLMService.ShowSettingDialog();
			LLMButtonUpdate();
		}

		public void LLMButtonUpdate()
		{
			llmButton1.Label = Globals.ThisAddIn.GlobalsSetting.CaptionButton1;
			llmButton1.SuperTip = $"Системный промпт: {Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1}";

			llmButton2.Label = Globals.ThisAddIn.GlobalsSetting.CaptionButton2;
			llmButton2.SuperTip = $"Системный промпт: {Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2}";

			InitializePopumMenuButton();
		}

		private void LLMButton1_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument.ChatMessageMode.HasFlag(ChatMessageModeEnum.ReplaceSelection))
			{
				Globals.ThisAddIn.ActiveDocument.InsertChatMessage(
					model: Globals.ThisAddIn.ActiveDocument.Pane.SelectedModel,
					options: Globals.ThisAddIn.ActiveDocument.Pane.ChatOptions,
					mode: ChatMessageModeEnum.ReplaceSelection,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1,
					userMessages: [Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton1],
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton1);
			}
		}

		private void LLMButton2_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			if (Globals.ThisAddIn.ActiveDocument.ChatMessageMode.HasFlag(ChatMessageModeEnum.ReplaceSelection))
			{
				Globals.ThisAddIn.ActiveDocument.InsertChatMessage(
					model: Globals.ThisAddIn.ActiveDocument.Pane.SelectedModel,
					options: Globals.ThisAddIn.ActiveDocument.Pane.ChatOptions,
					mode: ChatMessageModeEnum.ReplaceSelection,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2,
					userMessages: [Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton2],
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton2);
			}
		}

		private void LLMReplaceButton_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			//if (Globals.ThisAddIn.Selection != null)
			//	Globals.ThisAddIn.Documents.Ai();
		}

		private void LLMChatButton_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			//Globals.ThisAddIn.Documents.AiEmbedShow();


			//Dialogs.LLMChatDialog chatDialog = new Dialogs.LLMChatDialog();
			//if (Globals.ThisAddIn.Selection != null &&
			//	Utils.Dialogs.ShowDialog(chatDialog) == DialogResult.OK)
			//{
			//	Globals.ThisAddIn.Documents.Ai("Ты юрист; изложи текст развернуто, в официальном стиле.", chatDialog.UserMessage);
			//}
		}



		#region Buttons Helper

		private Office.CommandBarButton AddButton(Office.CommandBar popupCommandBar, string caption, int faceId, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
		{
			Office.CommandBarButton commandBarButton = GetButton(popupCommandBar, tag);
			if (commandBarButton == null)
			{
				commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlButton);
				commandBarButton.Caption = caption;
				commandBarButton.FaceId = faceId;
				commandBarButton.Tag = tag;
				commandBarButton.BeginGroup = beginGroup;
				commandBarButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(clickFunctionDelegate);
			}
			return commandBarButton;
		}

		private Office.CommandBarButton AddButton(Word.Application application, Office.CommandBar popupCommandBar, string caption, string idMso, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
		{
			Office.CommandBarButton commandBarButton = GetButton(popupCommandBar, tag);
			if (commandBarButton == null)
			{
				commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlButton);
				commandBarButton.Caption = caption;
				commandBarButton.Picture = application.CommandBars.GetImageMso(idMso, 16, 16);
				commandBarButton.Tag = tag;
				commandBarButton.BeginGroup = beginGroup;
				commandBarButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(clickFunctionDelegate);
			}
			return commandBarButton;
		}

		private Office.CommandBarButton GetButton(Office.CommandBar popupCommandBar, string tag)
		{
			foreach (var commandBarButton in popupCommandBar.Controls.OfType<Office.CommandBarButton>())
			{
				if (commandBarButton.Tag.Equals(tag))
				{
					return commandBarButton;
				}
			}
			return null;
		}

		private void SetContextMenuButtonEnabled(string buttonTag, bool enabled)
		{
			Office.CommandBarButton button = GetButton(Globals.ThisAddIn.Application.CommandBars["Text"], buttonTag);
			if (button != null)
			{
				button.Enabled = enabled;
			}
		}

		#endregion

		private void PaneVisibleButton_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Tools.Ribbon.RibbonToggleButton button = (Tools.Ribbon.RibbonToggleButton)sender;
			if (button.Checked)
				GlobalsEventsBus.SetPaneVisibleState(Globals.ThisAddIn.ActiveDocument);
			else
				GlobalsEventsBus.SetPaneHideState(Globals.ThisAddIn.ActiveDocument);
		}

		private void AboutButton_Click(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Form dialog = new AboutBox();
			Utils.Dialogs.ShowDialog(dialog);			
		}		
	}
}
