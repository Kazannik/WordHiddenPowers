
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Panes;
using Office = Microsoft.Office.Core;
using Tools = Microsoft.Office.Tools;
using Word = Microsoft.Office.Interop.Word;

#if WORD

namespace WordHiddenPowers.Documents
#else
using Content = ProsecutorialSupervision.Utils.WordDocuments.Content;

namespace ProsecutorialSupervision.Documents
#endif
{
	public partial class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		/// <summary>
		/// Цвет текста, добавленного с помощью технологий ИИ.
		/// </summary>
		private const Word.WdColorIndex RESULT_MESSAGE_COLOR = Word.WdColorIndex.wdBrightGreen;
		private const string OPEN_TAG = ""; // >>
		private const string CLOSE_TAG = ""; // <<

		internal readonly Tools.Ribbon.RibbonToggleButton paneVisibleButton;

		private readonly Office.CommandBarButton buttonSelectDecimalCategory;
		private readonly Office.CommandBarButton buttonSelectTextCategory;
		private readonly Office.CommandBarButton buttonLMMChat1;
		private readonly Office.CommandBarButton buttonLMMChat2;

		private readonly IDictionary<int, Document> documents;

		public DocumentCollection(Tools.Ribbon.RibbonToggleButton paneVisibleButton)
		{
			documents = new Dictionary<int, Document>();

			this.paneVisibleButton = paneVisibleButton;
			this.paneVisibleButton.Checked = false;
			this.paneVisibleButton.Click += new Tools.Ribbon.RibbonControlEventHandler(PaneVisibleButtonClick);

			Globals.ThisAddIn.Application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Document_WindowSelectionChange);

			Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNote_Click);
			buttonSelectDecimalCategory = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNote_Click);

			buttonLMMChat1 = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Properties.Settings.Default.LLMButton1, Const.Content.LLM_BUTTON_IMAGE_ID, $"Системный промпт: {Properties.Settings.Default.LLMSystemMessage1}", true, AiButton1_Click);
			buttonLMMChat2 = AddButton(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Properties.Settings.Default.LLMButton2, Const.Content.LLM_BUTTON_IMAGE_ID, $"Системный промпт: {Properties.Settings.Default.LLMSystemMessage2}", false, AiButton2_Click);
		}

		public Document ActiveDocument => GetDocument(Globals.ThisAddIn.Application.ActiveDocument);

		private void PaneVisibleButtonClick(object sender, Tools.Ribbon.RibbonControlEventArgs e)
		{
			Tools.Ribbon.RibbonToggleButton button = (Tools.Ribbon.RibbonToggleButton)sender;
			ActiveDocument.CustomPane.Visible = button.Checked;
		}

		private void Document_WindowSelectionChange(Word.Selection Sel)
		{
			SetToolBarState();

			if (Sel.Start == Sel.End)
			{
				MessageMode = ChartMessageMode.Insert;
				previousCharacter = Sel.Start == 0 ? default: Sel.Document.Range(Sel.Start - 1, Sel.Start);
				nextCharacter = Sel.End == Sel.Document.Range().End ? default : Sel.Document.Range(Sel.End, Sel.End + 1);
			}
			else
			{
				bool isPrevious = IsPreviousParagraphIsNull(Sel, out previousParagraph);
				bool isCenter = IsCenterParagraphIsNull(Sel, out firstRange, out centerParagraph, out lastRange);
				bool isNext = IsNextParagraphIsNull(Sel, out nextParagraph);
				messageRange = Sel.Range;

				if (!isPrevious && !isCenter && !isNext) 
				{
					MessageMode = ChartMessageMode.Replace;
					previousCharacter = Sel.Start == 0 ? default : Sel.Document.Range(Sel.Start - 1, Sel.Start);
					nextCharacter = Sel.End == Sel.Document.Range().End ? default : Sel.Document.Range(Sel.End, Sel.End + 1);
				}
				else
				{
					previousCharacter = default;
					nextCharacter = default;
					
					MessageMode =  ChartMessageMode.Replace | ChartMessageMode.Insert;
					if (isPrevious) MessageMode = MessageMode | ChartMessageMode.Previous;
					if (isCenter) MessageMode = MessageMode | ChartMessageMode.Center;
					if (isNext) MessageMode = MessageMode | ChartMessageMode.Next;
				}
			}
			((AddInPane)ActiveDocument.CustomPane.Control).MessageMode = MessageMode;
		}

		public void InsertMessage(ChartMessageMode mode, string systemMessage, string userMessage)
		{
			string prefix = previousCharacter == default || previousCharacter.Text == default || previousCharacter.Text == "\r" ? OPEN_TAG : previousCharacter.Text == "\u0020" ? OPEN_TAG : "\u0020" + OPEN_TAG;
			string postfix = nextCharacter == default || nextCharacter.Text == default || nextCharacter.Text == "\r" ? CLOSE_TAG : nextCharacter.Text == "\u0020" ? CLOSE_TAG : CLOSE_TAG + "\u0020";

			switch (mode)
			{
				case ChartMessageMode.Insert:
					AiShow(systemMessage, new string[] { userMessage }, new LLMProcessDialog
						.Arguments(InsertText, Globals.ThisAddIn.Selection.Range, prefix, postfix));
					//InsertText(Globals.ThisAddIn.Selection.Range, prefix, "ОTBET-ВСТАВКА", postfix);
					break;
				case ChartMessageMode.Replace:
					AiShow(systemMessage, new string[] { messageRange.Text.Trim(), userMessage }, new LLMProcessDialog
						.Arguments(ReplaceText, Globals.ThisAddIn.Selection.Range, prefix, postfix));
					//ReplaceText(Globals.ThisAddIn.Selection.Range, prefix, "ОTBET-ЗАМЕНА", postfix);
					break;
				case ChartMessageMode.Previous:
					AiShow(systemMessage, new string[] { messageRange.Text.Trim(), userMessage }, new LLMProcessDialog
						.Arguments(InsertText, previousParagraph.Range, prefix, postfix));
					//InsertText(previousParagraph.Range, prefix, "ОTBET ПЕРЕД", postfix);
					break;
				case ChartMessageMode.Center:
					AiShow(systemMessage, new string[] { firstRange.Text.Trim(), lastRange.Text.Trim(), userMessage }, new LLMProcessDialog
						.Arguments(InsertText, centerParagraph.Range, prefix, postfix));
					//InsertText(centerParagraph.Range, prefix, "ОTBET В ЦЕНТРЕ", postfix);
					break;
				case ChartMessageMode.Next:
					AiShow(systemMessage, new string[] { messageRange.Text.Trim(), userMessage }, new LLMProcessDialog
						.Arguments(InsertText, nextParagraph.Range, prefix, postfix));
					//InsertText(nextParagraph.Range, prefix, "ОTBET ПОСЛЕ", postfix);
					break;
			}
		}

		private void InsertText(Word.Range range, string prefix, string text, string postfix)
		{
			range.InsertBefore(prefix + text + postfix);
			if (range.Text.EndsWith("\r")) range.SetRange(range.Start, range.End - 1);
			range.HighlightColorIndex = RESULT_MESSAGE_COLOR;
		}

		private void ReplaceText(Word.Range range, string prefix, string text, string postfix)
		{
			if (range.Text.EndsWith("\r"))
			{ 
				range.SetRange(range.Start, range.End - 1);
				postfix = postfix.TrimEnd();
			}
			range.Text = prefix + text + postfix;
			range.HighlightColorIndex = RESULT_MESSAGE_COLOR;
		}

		#region ChatMessageMode

		public ChartMessageMode MessageMode { get; private set; }

		private Word.Range messageRange;
		private Word.Paragraph previousParagraph;
		private Word.Range firstRange;
		private Word.Paragraph centerParagraph;
		private Word.Range lastRange;
		private Word.Paragraph nextParagraph;

		private Word.Range previousCharacter;
		private Word.Range nextCharacter;

		/// <summary>
		/// Паред выделенным фрагментом есть пустой абзац.
		/// </summary>
		/// <param name="Sel"></param>
		/// <returns></returns>
		private bool IsPreviousParagraphIsNull(Word.Selection Sel, out Word.Paragraph paragraph)
		{
			Word.Paragraph previous = Sel.Paragraphs[1].Previous();
			if (previous != null && string.IsNullOrWhiteSpace(previous.Range.Text))
			{
				paragraph = previous; return true;
			}
			else
			{
				paragraph = default; return false;
			}
		}

		/// <summary>
		/// После выделенного фрагмента есть пустой абзац.
		/// </summary>
		/// <param name="Sel"></param>
		/// <returns></returns>
		private static bool IsNextParagraphIsNull(Word.Selection Sel, out Word.Paragraph paragraph)
		{
			Word.Paragraph next = Sel.Paragraphs[Sel.Paragraphs.Count].Next();
			if (next != null && string.IsNullOrWhiteSpace(next.Range.Text))
			{
				paragraph = next; return true;
			}
			else
			{
				paragraph = default; return false;
			}			
		}

		/// <summary>
		/// Между первым и последним выделенными абзацами есть пустой абзац.
		/// </summary>
		/// <param name="selection"></param>
		/// <returns></returns>
		private static bool IsCenterParagraphIsNull(Word.Selection selection, out Word.Range first, out Word.Paragraph center, out Word.Range last)
		{
			if (selection.Paragraphs.Count >= 3)
			{
				List<Word.Paragraph> firstSelections = new List<Word.Paragraph>();
				List<Word.Paragraph> lastSelections = new List<Word.Paragraph>();
				Word.Paragraph centerParagraph = default;
				for (int i = 1; i <= selection.Paragraphs.Count; i++)
				{
					if (centerParagraph == default && !string.IsNullOrWhiteSpace(selection.Paragraphs[i].Range.Text))
						firstSelections.Add(selection.Paragraphs[i]);
					else if (centerParagraph == default && firstSelections.Count > 0 && string.IsNullOrWhiteSpace(selection.Paragraphs[i].Range.Text))
						centerParagraph = selection.Paragraphs[i];
					else if (centerParagraph != default && !string.IsNullOrWhiteSpace(selection.Paragraphs[i].Range.Text))
						lastSelections.Add(selection.Paragraphs[i]);
				}
				if (firstSelections.Count > 0 &&
					centerParagraph != default &&
					lastSelections.Count > 0)
				{
					first = selection.Document.Range(firstSelections.First().Range.Start, firstSelections.Last().Range.End);
					center = centerParagraph;
					last = selection.Document.Range(lastSelections.First().Range.Start, lastSelections.Last().Range.End);
					return true;
				}
				else
				{
					first = default; center = default; last = default;
					return false;
				}
			}
			else
			{
				first = default; center = default; last = default;
				return false;
			}
		}

		[Flags]
		public enum ChartMessageMode: int
		{
			Nothing = 0,
			Insert = 1,
			Replace = 2,
			Previous = 4,
			Center = 8,
			Next = 16
		}

		#endregion

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

		#endregion

		#region Documents Command

		public void Activate(Word._Document Doc, Word.Window Wn)
		{
			int hwnd = Doc.Windows[1].Hwnd;

			if (documents.ContainsKey(hwnd))
			{
				Debug.WriteLine(string.Format("Activate: {0}", hwnd));
				paneVisibleButton.Checked = documents[hwnd].CustomPane.Visible;				
			}
			SetToolBarState();
		}

		public void Add(Word._Document Doc)
		{
			int hwnd = Doc.Windows[1].Hwnd;

			Debug.WriteLine(string.Format("Add: {0}", hwnd));


			if (!documents.ContainsKey(hwnd))
			{
				documents.Add(hwnd, Document.Create(this, Doc.FullName, Doc));
			}

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				pane.Visible = paneVisibleButton.Checked;
			}
		}

		public void Remove(Word._Document Doc)
		{
			int hwnd = Doc.Windows[1].Hwnd;

			Debug.WriteLine(string.Format("Remove: {0}", hwnd));

			if (documents.ContainsKey(hwnd))
			{
				Tools.CustomTaskPane pane = documents[hwnd].CustomPane;
				documents.Remove(hwnd);				
				Globals.ThisAddIn.CustomTaskPanes.Remove(pane);
			}
			SetToolBarState();
		}

		private Document GetDocument(Word._Document Doc)
		{
			int hwnd = Doc.Windows[1].Hwnd;

			if (!documents.ContainsKey(hwnd))
			{
				documents.Add(hwnd, Document.Create(this, Doc.FullName, Doc));
			}

			Debug.WriteLine(string.Format("GetDocument: {0}", hwnd));
			return documents[hwnd];
		}

		#endregion

		internal void SetToolBarState()
		{
			Globals.Ribbons.AddInMainRibbon.newDataButton.Enabled =
			Globals.Ribbons.AddInMainRibbon.openDataButton.Enabled = true;

			Globals.Ribbons.AddInMainRibbon.saveDataButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.deleteDataButton.Enabled = ActiveDocument.State != Document.WordDocumentMode.Default;

			Globals.Ribbons.AddInMainRibbon.editCategoriesButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.createTableButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.editDocumentKeysButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Separate;

			Globals.Ribbons.AddInMainRibbon.aggregatedTableViewerButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Combine && (ActiveDocument.NowAggregatedDataSet != null && ActiveDocument.NowAggregatedDataSet.IsTables);
			Globals.Ribbons.AddInMainRibbon.aggregatedDialogButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Combine;

			Globals.Ribbons.AddInMainRibbon.aggregatedImportFolderButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.aggregatedImportFileButton.Enabled =
			
			Globals.Ribbons.AddInMainRibbon.oldAggregatedImportFolderButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.oldAggregatedImportFileButton.Enabled = ActiveDocument.State != Document.WordDocumentMode.Separate;

			Globals.Ribbons.AddInMainRibbon.addLastNoteTypeButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any();
			Globals.Ribbons.AddInMainRibbon.addTextNoteButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any(x => x.IsText);
			Globals.Ribbons.AddInMainRibbon.addDecimalNoteButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any(x => x.IsDecimal);
			
			Globals.Ribbons.AddInMainRibbon.searchServiceButton.Enabled = 
			Globals.Ribbons.AddInMainRibbon.aiServiceButton.Enabled = ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any();

			Globals.Ribbons.AddInMainRibbon.editTableButton.Enabled = ActiveDocument.IsTableSchema;

			SetContextMenuButtonEnabled(Const.Panes.BUTTON_STRING_TAG, ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any(x => x.IsText));
			SetContextMenuButtonEnabled(Const.Panes.BUTTON_DECIMAL_TAG, ActiveDocument.State == Document.WordDocumentMode.Separate && ActiveDocument.CurrentDataSet.Subcategories.Any(x => x.IsDecimal));
		}

		private void SetContextMenuButtonEnabled(string buttonTag, bool enabled)
		{
			Office.CommandBarButton button = GetButton(Globals.ThisAddIn.Application.CommandBars["Text"], buttonTag);
			if (button != null)
			{
				button.Enabled = enabled;
			}
		}

		public void Dispose() => Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

		public IEnumerator<Document> GetEnumerator() => documents.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();		
	}
}
