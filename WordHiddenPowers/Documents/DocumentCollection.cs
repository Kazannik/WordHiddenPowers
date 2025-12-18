using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Office = Microsoft.Office.Core;
using Tools = Microsoft.Office.Tools;
using Word = Microsoft.Office.Interop.Word;
using WordHiddenPowers.LLMService;



#if WORD
using Content = WordHiddenPowers.Utils.WordDocuments.Content;

namespace WordHiddenPowers.Documents
#else
using Content = ProsecutorialSupervision.Utils.WordDocuments.Content;

namespace ProsecutorialSupervision.Documents
#endif
{
	public partial class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		internal RibbonToggleButton PaneVisibleButton;

		private readonly Office.CommandBarButton buttonSelectDecimalCategory;
		private readonly Office.CommandBarButton buttonSelectTextCategory;
		private readonly Office.CommandBarButton buttonLMMChat1;
		private readonly Office.CommandBarButton buttonLMMChat2;

		private readonly LLMClient llmClient;

		private readonly IDictionary<string, Document> documents;

		public DocumentCollection(RibbonToggleButton paneVisibleButton)
		{
			documents = new Dictionary<string, Document>();

			PaneVisibleButton = paneVisibleButton;
			PaneVisibleButton.Checked = false;
			PaneVisibleButton.Click += new RibbonControlEventHandler(PaneVisibleButtonClick);

			Globals.ThisAddIn.Application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Document_WindowSelectionChange);

			Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNote_Click);
			buttonSelectDecimalCategory = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNote_Click);

			buttonLMMChat1 = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Properties.Settings.Default.LLMButton1, Const.Content.LLM_BUTTON_IMAGE_ID, $"Системный промпт: {Properties.Settings.Default.LLMSystemMessage1}", true, AiButton1_Click);
			buttonLMMChat2 = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Properties.Settings.Default.LLMButton2, Const.Content.LLM_BUTTON_IMAGE_ID, $"Системный промпт: {Properties.Settings.Default.LLMSystemMessage2}", false, AiButton2_Click);

			llmClient = new LLMClient();

			llmClient.ChatCompleted += new EventHandler<LLMClient.ChatCompletedEventArgs>(LLMClient_ChatCompleted);
			llmClient.ChatProgress += new EventHandler<LLMClient.ChatProgressEventArgs>(LLMClient_ChatProgress);

			llmClient.EmbedCompleted += new EventHandler<LLMClient.EmbedCompletedEventArgs>(LLMClient_EmbedCompleted);
			llmClient.EmbedProgress += new EventHandler<LLMClient.EmbedProgressEventArgs>(LLMClient_EmbedProgress);

			//if (!string.IsNullOrEmpty(aiClient.SelectedModel))
			//{
			//	AiModelName = aiClient.SelectedModel;
			//}

			//if (!string.IsNullOrEmpty(aiClient.Url))
			//{
			//	AiUrl = aiClient.Url;
			//}
		}

		public Document ActiveDocument => GetDocument(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveDocument);
		
		private void PaneVisibleButtonClick(object sender, RibbonControlEventArgs e)
		{
			RibbonToggleButton button = (RibbonToggleButton)sender;
			ActiveDocument.CustomPane.Visible = button.Checked;
		}

		private void Document_WindowSelectionChange(Word.Selection Sel)
		{			
			Office.CommandBarButton button = GetButton(Globals.ThisAddIn.Application.CommandBars["Text"], Const.Panes.BUTTON_STRING_TAG);
			if (button != null)
			{
				if (Sel != null &&
					!string.IsNullOrWhiteSpace(Sel.Text))
				{
					button.Enabled = true;
				}
				else
				{
					button.Enabled = false;
				}
			}
		}
		
		private Office.CommandBarButton AddButtons(Office.CommandBar popupCommandBar, string caption, int faceId, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
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

		private Office.CommandBarButton AddButtons(Word.Application application, Office.CommandBar popupCommandBar, string caption, string idMso, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
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

		#region Documents Command
		
		public void Activate(Word._Document Doc, Word.Window Wn)
		{
			List<Tools.CustomTaskPane> panes = new List<Tools.CustomTaskPane>(Globals.ThisAddIn.CustomTaskPanes
				.Where(pane => pane.Window == Wn && ((Panes.WordHiddenPowersPane)pane.Control).Document.Doc != Doc));

			panes.ForEach(x => Globals.ThisAddIn.CustomTaskPanes.Remove(x));
			
			

			HashSet<Word._Document> seen = new HashSet<Word._Document>();
			panes.Clear();

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				if (!seen.Add(((Panes.WordHiddenPowersPane)pane.Control).Document.Doc)) // Если Add вернул false, значит элемент уже есть
				{
					panes.Add(pane);
				}
			}

			foreach (Tools.CustomTaskPane pane in panes)
			{
				Globals.ThisAddIn.CustomTaskPanes.Remove(pane);
			}

			PaneVisibleButton.Checked = Globals.ThisAddIn.CustomTaskPanes
				.Where(x => x.Window == Wn).Any(x => x.Visible);
			
			bool isContent = ActiveDocument.VariablesExists();

			Globals.Ribbons.AddInMainRibbon.saveDataButton.Enabled = isContent;
			Globals.Ribbons.AddInMainRibbon.deleteDataButton.Enabled = isContent;

			Globals.Ribbons.AddInMainRibbon.editCategoriesButton.Enabled = isContent;
			Globals.Ribbons.AddInMainRibbon.createTableButton.Enabled = isContent;
			Globals.Ribbons.AddInMainRibbon.editDocumentKeysButton.Enabled = isContent;

			Globals.Ribbons.AddInMainRibbon.aggregatedImportFolderButton.Enabled = !isContent;
			Globals.Ribbons.AddInMainRibbon.aggregatedImportFileButton.Enabled = !isContent;
			Globals.Ribbons.AddInMainRibbon.oldAggregatedImportFolderButton.Enabled = ActiveDocument.CurrentDataSet.WordFiles.Any();
			Globals.Ribbons.AddInMainRibbon.oldAggregatedImportFileButton.Enabled = ActiveDocument.CurrentDataSet.WordFiles.Any();
			Globals.Ribbons.AddInMainRibbon.aggregatedTableViewerButton.Enabled = isContent;
			Globals.Ribbons.AddInMainRibbon.aggregatedDialogButton.Enabled = isContent;

			Globals.Ribbons.AddInMainRibbon.addLastNoteTypeButton.Enabled = isContent && ActiveDocument.CurrentDataSet.Subcategories.Any();
			Globals.Ribbons.AddInMainRibbon.addTextNoteButton.Enabled = isContent && ActiveDocument.CurrentDataSet.Subcategories.Any(x=> x.IsText);
			Globals.Ribbons.AddInMainRibbon.addDecimalNoteButton.Enabled = isContent && ActiveDocument.CurrentDataSet.Subcategories.Any(x => x.IsDecimal);
			Globals.Ribbons.AddInMainRibbon.searchServiceButton.Enabled = isContent && ActiveDocument.CurrentDataSet.Subcategories.Any();
			Globals.Ribbons.AddInMainRibbon.aiServiceButton.Enabled = isContent && ActiveDocument.CurrentDataSet.Subcategories.Any();

			Globals.Ribbons.AddInMainRibbon.editTableButton.Enabled = isContent;

			//Globals.Ribbons.AddInMainRibbon.paneVisibleButton.Enabled = isContent;

			try
			{
				buttonSelectTextCategory.Enabled = isContent;
                buttonSelectDecimalCategory.Enabled = isContent;
            }
			catch (Exception) { }
		}

		public void Add(Word._Document Doc)
		{
			string guid = Content.GetGuidOrDefault(Doc);
			if (!documents.ContainsKey(guid))
			{
				documents.Add(guid, Document.Create(this, Doc.FullName, Doc));
			}

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				pane.Visible = true;
			}

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				pane.Visible = PaneVisibleButton.Checked;
			}
		}

		public void Remove(Word._Document Doc)
		{
			string guid = Content.GetGuid(Doc);
			if (documents.ContainsKey(guid))
			{
				documents.Remove(guid);
			}
		}

		private Document GetDocument(Word.Application application, Word._Document Doc)
		{
			string guid = Content.GetGuidOrDefault(Doc);
			if (!documents.ContainsKey(guid))
			{
				documents.Add(guid, Document.Create(this, Doc.FullName, Doc));
			}

			List<string> closedDocs = (from Word._Document document in application.Documents
									  let id = Content.GetGuidOrDefault(Doc)
									  where !documents.ContainsKey(id)
									  select id)
									  .ToList();

			foreach (string id in closedDocs)
			{
				if (documents.ContainsKey(id))
				{
					Document document = documents[id];
					document.Dispose();
					documents.Remove(id);
				}
			}
			return documents[guid];
		}

		#endregion

		public void Dispose() => Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

		public IEnumerator<Document> GetEnumerator() => documents.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
