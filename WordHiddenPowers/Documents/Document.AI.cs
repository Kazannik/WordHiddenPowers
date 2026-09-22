// Ignore Spelling: dest


using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using static WordHiddenPowers.Dialogs.LLMProcessDialog;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class Document
	{

		/// <summary>
		/// Цвет текста, добавленного с помощью технологий ИИ.
		/// </summary>
		private const Word.WdColorIndex RESULT_MESSAGE_COLOR = Word.WdColorIndex.wdBrightGreen;
		private const string OPEN_TAG = ""; // >>
		private const string CLOSE_TAG = ""; // <<

		public void InsertChatMessage(IModel model, IChatOptions options, ChatMessageModeEnum mode, string systemMessage, IEnumerable<string> userMessages, string postfixUserMessage = "")
		{
			string prefix = previousCharacter == default || previousCharacter.Text == default || previousCharacter.Text.StartsWith("\r") ? OPEN_TAG : previousCharacter.Text.StartsWith("\u0020") ? OPEN_TAG : "\u0020" + OPEN_TAG;
			string postfix = nextCharacter == default || nextCharacter.Text == default || nextCharacter.Text.EndsWith("\r") ? CLOSE_TAG : nextCharacter.Text.EndsWith("\u0020") ? CLOSE_TAG : CLOSE_TAG + "\u0020";

			if (mode.HasFlag(ChatMessageModeEnum.InsertHere))
			{
				if (!string.IsNullOrWhiteSpace(postfixUserMessage))
					userMessages = userMessages.Append(postfixUserMessage);

				if (!mode.HasFlag(ChatMessageModeEnum.Repeat))
					AiShow(model, options, systemMessage, userMessages, new Arguments(InsertText, Globals.ThisAddIn.Selection.Range, prefix, postfix));
			}
			else if (mode.HasFlag(ChatMessageModeEnum.ReplaceSelection))
			{
				userMessages = userMessages.Append(messageRange.Text.Trim());
				if (!string.IsNullOrWhiteSpace(postfixUserMessage))
					userMessages = userMessages.Append(postfixUserMessage);

				if (!mode.HasFlag(ChatMessageModeEnum.Repeat))
					AiShow(model, options, systemMessage, userMessages, new Arguments(ReplaceText, Globals.ThisAddIn.Selection.Range, prefix, postfix));
			}
			else if (mode.HasFlag(ChatMessageModeEnum.InsertPrevious)) {
				userMessages = userMessages.Append(messageRange.Text.Trim());
				if (!string.IsNullOrWhiteSpace(postfixUserMessage))
					userMessages = userMessages.Append(postfixUserMessage);

				if (!mode.HasFlag(ChatMessageModeEnum.Repeat))
					AiShow(model, options, systemMessage, userMessages, new Arguments(InsertText, previousParagraph.Range, prefix, postfix));
			}
			else if (mode.HasFlag(ChatMessageModeEnum.InsertBetween)) {
				userMessages = userMessages
					.Append(firstRange.Text.Trim())
					.Append(lastRange.Text.Trim());
				if (!string.IsNullOrWhiteSpace(postfixUserMessage))
					userMessages = userMessages.Append(postfixUserMessage);

				if (!mode.HasFlag(ChatMessageModeEnum.Repeat))
					AiShow(model, options, systemMessage, userMessages, new Arguments(InsertText, centerParagraph.Range, prefix, postfix));
			}
			else if (mode.HasFlag(ChatMessageModeEnum.InsertNext)) {
				userMessages = userMessages.Append(messageRange.Text.Trim());
				if (!string.IsNullOrWhiteSpace(postfixUserMessage))
					userMessages = userMessages.Append(postfixUserMessage);

				if (!mode.HasFlag(ChatMessageModeEnum.Repeat)) 
					AiShow(model, options, systemMessage, userMessages, new Arguments(InsertText, nextParagraph.Range, prefix, postfix));
			}
			
			if (mode.HasFlag(ChatMessageModeEnum.Repeat))
			{
				AiShow(model, options, systemMessage, userMessages, new Arguments(ReplaceText, outRange, prefix, postfix));
			}		
		}

		private Word.Range InsertText(Word.Range range, string prefix, string text, string postfix)
		{
			if (range.Text != null && range.Text.EndsWith("\r"))
			{
				range.SetRange(range.Start, range.End - 1);
				postfix = postfix.TrimEnd();
			}
			range.InsertBefore(prefix + text + postfix);
			range.HighlightColorIndex = RESULT_MESSAGE_COLOR;

			ChatMessageMode = ChatMessageModeEnum.Nothing;
			return outRange = range;
		}

		private Word.Range ReplaceText(Word.Range range, string prefix, string text, string postfix)
		{
			if (range.Text != null && range.Text.EndsWith("\r"))
			{
				range.SetRange(range.Start, range.End - 1);
				postfix = postfix.TrimEnd();
			}
			range.Text = prefix + text + postfix;
			range.HighlightColorIndex = RESULT_MESSAGE_COLOR;

			ChatMessageMode = ChatMessageModeEnum.Nothing;

			return outRange = range;
		}

		public Word.Range ReplaceText(Word.Range range, string text)
		{
			if (range.Text != null && range.Text.EndsWith("\r"))
			{
				range.SetRange(range.Start, range.End - 1);
			}
			range.Text = text;
			range.HighlightColorIndex = RESULT_MESSAGE_COLOR;

			ChatMessageMode = ChatMessageModeEnum.Nothing;

			return outRange = range;
		}

		#region ChatMessageMode

		public ChatMessageModeEnum ChatMessageMode { get; private set; }

		private Word.Range messageRange;
		private Word.Range firstRange;
		private Word.Paragraph previousParagraph;
		private Word.Paragraph centerParagraph;
		private Word.Paragraph nextParagraph;
		private Word.Range lastRange;

		private Word.Range previousCharacter;
		private Word.Range nextCharacter;

		private Word.Range outRange;


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
				List<Word.Paragraph> firstSelections = [];
				List<Word.Paragraph> lastSelections = [];
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

		public static ChatMessageModeEnum CreateChatMessageMode(
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertBetweenMessage)
		{
			ChatMessageModeEnum mode = ChatMessageModeEnum.Nothing;
			if (insertHereMessage) mode |= ChatMessageModeEnum.InsertHere;
			if (replaceSelectionMessage) mode |= ChatMessageModeEnum.ReplaceSelection;
			if (insertNextMessage) mode |= ChatMessageModeEnum.InsertNext;
			if (insertPreviousMessage) mode |= ChatMessageModeEnum.InsertPrevious;
			if (insertBetweenMessage) mode |= ChatMessageModeEnum.InsertBetween;
			return mode;
		}

		/// <summary>
		/// Тип вставки результата.
		/// </summary>
		[Flags]
		public enum ChatMessageModeEnum : int
		{
			Nothing = 0,

			/// <summary>
			/// Действие "Повторить".
			/// </summary>
			Repeat = 1 << 0,
			
			/// <summary>
			/// Действие "Вернуть".
			/// </summary>
			Redo = 1 << 1,
			
			/// <summary>
			/// Действие "Отменить".
			/// </summary>
			Undo = 1 << 2,
			
			/// <summary>
			/// Действие "Вставить".
			/// </summary>
			Insert = 1 << 3,
			
			/// <summary>
			/// Действие "Заменить".
			/// </summary>
			Replace = 1 << 4,

			/// <summary>
			/// Место действия "В выеделенном тексте".
			/// </summary>
			Selection = 1 << 5,

			/// <summary>
			/// Место действия "В текущем тексте".
			/// </summary>
			Here = 1 << 6,
			
			/// <summary>
			/// Место действия "В предыдущем абзаце".
			/// </summary>
			Previous = 1 << 7,

			/// <summary>
			/// Место действия "Между абзацами".
			/// </summary>
			Between = 1 << 8,
			
			/// <summary>
			/// Место действия "В следующем абзаце".
			/// </summary>
			Next = 1 << 9,
			
			/// <summary>
			/// Вставка в текущем тексте.
			/// </summary>
			InsertHere = Insert | Here,
						
			/// <summary>
			/// Вставка в следующем абзаце.
			/// </summary>
			InsertNext = Insert | Next,
			
			/// <summary>
			/// Вставка в предыдущем абзаце.
			/// </summary>
			InsertPrevious = Insert | Previous,

			/// <summary>
			/// Вставка между абзацами.
			/// </summary>
			InsertBetween = Insert | Between,

			/// <summary>
			/// Заменить в выделенном тексте.
			/// </summary>
			ReplaceSelection = Replace | Selection,

			All = Repeat | Redo | Undo | Insert | Replace | Selection | Here | Previous | Between | Next,
		}

		private readonly struct Message(string text)
		{
			public string Text { get; } = text;
		}

		#endregion

		public void ShowSearchServiceDialog()
		{
			Form dialog = new SelectCategoriesDialog(this);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				Pane.NotesControl.ShowButtons = true;
				Services.Searcher.Search(
					document: this,
					subcategories: ((SelectCategoriesDialog)dialog).CheckedSubcategories);

				MessageBox.Show("Разметка документа с помощью поисковых функций выполнена!",
							"Разметка документа",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				Pane.NotesControl.ShowButtons = false;
			}
		}

		public void MLSearchService()
		{
			Pane.NotesControl.ShowButtons = true;
			Services.MLService.Search(document: this, Const.Globals.LEVEL_PASSAGE);
			MessageBox.Show("Разметка документа с помощью нейронной сети выполнена!",
						"Разметка документа",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
			Pane.NotesControl.ShowButtons = false;
		}

		public void LLMSearchService()
		{
			Pane.NotesControl.ShowButtons = true;
			Services.MLService.Search(document: this, Const.Globals.LEVEL_PASSAGE);
			MessageBox.Show("Разметка документа с помощью нейронной сети выполнена!",
						"Разметка документа",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
			Pane.NotesControl.ShowButtons = false;
		}



		//public void EmbeddingAllParagraphs()
		//{
		//	bool checkHost = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(Services.LLMService.DefaultAuthenticationProfile);

		//	if (!checkHost)
		//	{
		//		for (int i = 1; i <= Doc.Paragraphs.Count; i++)
		//		{
		//			Word.Paragraph paragraph = Doc.Paragraphs[i];
		//			string text = paragraph.Range.Text;
		//			if (text.Trim().Length > 10)
		//			{
		//				ReadOnlyMemory<float> vector =
		//					LLMConnectorLibrary.OpenAIService.GetEmbeddingAsync(
		//					Services.LLMService.DefaultAuthenticationProfile,
		//					Services.LLMService.DefaultEmbeddingLLMName,
		//					text).GetAwaiter().GetResult();

		//				string stringVector = string.Join(";", vector.ToArray().Select(f => f.ToString()));

		//				if (!VectorDataSet.WordFiles.Exists(FileName))
		//					VectorDataSet.WordFiles.AddWordFilesRow(FileName, Caption, Description, Date);

		//				WordFilesRow wordFiles = VectorDataSet.WordFiles.Get(FileName);

		//				VectorDataSet.ParagraphVectorStore.AddParagraphVectorStoreRow(wordFiles,
		//					text, stringVector, paragraph.Range.Start, paragraph.Range.End);
		//			}
		//		}
		//	}
		//}

		//public void EmbeddingDataSet(RepositoryDataSet sourceDataSet) => EmbeddingDataSet(sourceDataSet: sourceDataSet, vectorDataSet: VectorDataSet);

		//public static void EmbeddingDataSet(RepositoryDataSet sourceDataSet, VectorDataSet vectorDataSet)
		//{
		//	bool checkHost = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(Services.LLMService.DefaultAuthenticationProfile);

		//	if (!checkHost)
		//	{
		//		foreach (Repository.Notes.Note note in sourceDataSet.GetTextNotes())
		//		{
		//			ReadOnlyMemory<float> vector =
		//					LLMConnectorLibrary.OpenAIService.GetEmbeddingAsync(
		//					Services.LLMService.DefaultAuthenticationProfile,
		//					Services.LLMService.DefaultEmbeddingLLMName,
		//					note.Value as string).GetAwaiter().GetResult();

		//			string stringVector = string.Join(";", vector.ToArray().Select(f => f.ToString()));

		//			if (!vectorDataSet.WordFiles.Exists(note.FileName))
		//				vectorDataSet.WordFiles.AddWordFilesRow(note.FileName, note.FileCaption, note.FileDescription, note.FileDate);

		//			WordFilesRow wordFiles = vectorDataSet.WordFiles.Get(note.FileName);

		//			vectorDataSet.ParagraphVectorStore.AddParagraphVectorStoreRow(wordFiles,
		//				note.Value as string, stringVector, note.WordSelectionStart, note.WordSelectionEnd);
		//		}
		//	}
		//}



		//#region AI


		////private const string MODEL = "gemma3:1b";// "gemma3:latest";

		//internal void AiChat(Word.Selection selection)
		//{
		//	if (string.IsNullOrEmpty(AiModelName) &&
		//			!string.IsNullOrEmpty(aiClient.SelectedModel))
		//	{
		//		AiModelName = aiClient.SelectedModel;
		//	}

		//	AiChatDialog dialog = new AiChatDialog
		//	{

		//	};

		//	dialog.SendPrompt += new EventHandler<PromptEventArgs>(Dialog_SendPrompt);

		//	Utils.Dialogs.ShowDialog(dialog);
		//}

		//private void Dialog_SendPrompt(object sender, PromptEventArgs e)
		//{
		//	dialog = new AiStatusDialog
		//	{
		//		Text = "Искусственный интеллект",
		//		Status = AI_STATUS_TEXT,
		//	};
		//	Utils.Dialogs.Show(dialog);

		//	countProgress = 0;

		//	aiClient.Send(model: AiModelName, systemPrompt: string.Empty, prompt: e.Prompt);

		//	aiRange = Globals.ThisAddIn.Selection.Range;
		//	aiRange.Text = AI_STATUS_TEXT + " ...";
		//	aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		//}

		//internal void Ai(Word.Selection selection, string systemMessage, string userMessage)
		//{
		//	Ai(selection: selection, systemMessage: systemMessage, userMessages: new string[] { userMessage, selection.Text });
		//}

		//internal void Ai(Word.Selection selection, string systemMessage, string[] userMessages)
		//{
		//	_ = Ai(range: selection.Range, systemMessage: systemMessage, userMessages: userMessages);
		//}

		//internal void Ai(Word.Selection selection)
		//{
		//	_ = Ai(range: selection.Range, systemMessage: string.Empty, userMessages: new string[] { selection.Text });
		//}

		//internal async Task Ai(Word.Range range, string systemMessage, string[] userMessages)
		//{
		//	bool isAvailable = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(Services.LLMService.DefaultAuthenticationProfile);

		//	if (isAvailable)
		//	{
		//		try
		//		{
		//			//range.Text = DocumentCollection.AI_STATUS_TEXT + " ...";
		//			range.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		//			range.Text = await LLMConnectorLibrary.OpenAIService.SendMessageAsync(model: Services.LLMService.DefaultLLMName, options: Services.LLMService.Options, systemMessage: systemMessage, userMessages: userMessages);
		//			range.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightGreen;
		//		}
		//		catch (Exception ex)
		//		{
		//			Utils.Dialogs.ShowMessageDialog(ex.Message);
		//		}
		//	}
		//	else
		//	{
		//		//Utils.Dialogs.ShowMessageDialog(string.Format("Проверьте доступ к провайдеру по адресу: [{0}]", Services.LLMService.Uri));
		//	}
		//}



		//internal void Ai(Word.Selection selection, IEnumerable<string> values)
		//{
		//	if (string.IsNullOrEmpty(AiModelName) &&
		//		!string.IsNullOrEmpty(aiClient.SelectedModel))
		//	{
		//		AiModelName = aiClient.SelectedModel;
		//	}

		//	dialog = new AiStatusDialog
		//	{
		//		Text = "Искусственный интеллект",
		//		Status = AI_STATUS_TEXT,
		//	};

		//	Utils.Dialogs.Show(dialog);
		//	countProgress = 0;
		//	aiRange = selection.Range;
		//	aiClient.Embed(model: AiModelName, values: values);
		//	aiRange.Text = AI_STATUS_TEXT + " ...";
		//	aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		//}

		//internal void Ai(Word.Range range, IEnumerable<string> values)
		//{
		//	if (string.IsNullOrEmpty(AiModelName) &&
		//		!string.IsNullOrEmpty(aiClient.SelectedModel))
		//	{
		//		AiModelName = aiClient.SelectedModel;
		//	}

		//	dialog = new AiStatusDialog
		//	{
		//		Text = "Искусственный интеллект",
		//		Status = AI_STATUS_TEXT,
		//	};

		//	Utils.Dialogs.Show(dialog);
		//	countProgress = 0;
		//	aiRange = range;
		//	aiClient.Embed(model: AiModelName, values: values);
		//	aiRange.Text = AI_STATUS_TEXT + " ...";
		//	aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		//}








		//#endregion



		public void AiShow(IModel model, IChatOptions options, string systemMessage, string userMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(model: model, options: Globals.ThisAddIn.GlobalsSetting.DefaultChatOptions, systemMessage: systemMessage, userMessages: [userMessage, Globals.ThisAddIn.Selection.Text], tag: null);
		}

		internal void AiShow(IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			LLMProcessDialog llmDialog = new(model: model, options: options, systemMessage: systemMessage, userMessages: userMessages, tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiShow(IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, Arguments arg)
		{
			LLMProcessDialog llmDialog = new(model: model, options: options, systemMessage, userMessages, arg);
			Utils.Dialogs.Show(llmDialog);
		}


		internal void AiEmbedShow(IModel model, string input, object tag = null)
		{
			LLMProcessDialog llmDialog = new(model: model, input: input, tag: tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiEmbedShow(IModel model, IEnumerable<(int key, string description)> store, object tag = null)
		{
			LLMProcessDialog llmDialog = new(model: model, store: store, tag: tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiEmbedShow(object tag = null)
		{
			//LLMProcessDialog llmDialog = new LLMProcessDialog(LLMService.Uri, LLMService.Timeout, LLMService.LLMName, ActiveDocument.NowAggregatedDataSet, ActiveDocument.VectorDataSet, tag);
			//Utils.Dialogs.Show(llmDialog);
		}
	}
}
