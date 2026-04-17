// Ignore Spelling: dest


using System;
using System.Linq;
using WordHiddenPowers.Repository;
using static WordHiddenPowers.Repository.VectorDataSet;
using Task = System.Threading.Tasks.Task;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class Document
	{
		public void EmbeddingAllParagraphs()
		{
			bool checkHost = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(Services.OpenAIService.Uri);

			if (!checkHost)
			{
				for (int i = 1; i <= Doc.Paragraphs.Count; i++)
				{
					Word.Paragraph paragraph = Doc.Paragraphs[i];
					string text = paragraph.Range.Text;
					if (text.Trim().Length > 10)
					{
						ReadOnlyMemory<float> vector =
							LLMConnectorLibrary.LLMOpenAI.GetEmbeddingAsync(
							Services.OpenAIService.Uri,
							Services.OpenAIService.Timeout,
							Services.OpenAIService.EmbeddingLLMName,
							text).GetAwaiter().GetResult();

						string stringVector = string.Join(";", vector.ToArray().Select(f => f.ToString()));

						if (!VectorDataSet.WordFiles.Exists(FileName))
							VectorDataSet.WordFiles.AddWordFilesRow(FileName, Caption, Description, Date);

						WordFilesRow wordFiles = VectorDataSet.WordFiles.Get(FileName);

						VectorDataSet.ParagraphVectorStore.AddParagraphVectorStoreRow(wordFiles,
							text, stringVector, paragraph.Range.Start, paragraph.Range.End);
					}
				}
			}
		}

		public void EmbeddingDataSet(RepositoryDataSet sourceDataSet) => EmbeddingDataSet(sourceDataSet: sourceDataSet, vectorDataSet: VectorDataSet);

		public static void EmbeddingDataSet(RepositoryDataSet sourceDataSet, VectorDataSet vectorDataSet)
		{
			bool checkHost = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(Services.OpenAIService.Uri);

			if (!checkHost)
			{
				foreach (Repository.Notes.Note note in sourceDataSet.GetTextNotes())
				{
					ReadOnlyMemory<float> vector =
							LLMConnectorLibrary.LLMOpenAI.GetEmbeddingAsync(
							Services.OpenAIService.Uri,
							Services.OpenAIService.Timeout,
							Services.OpenAIService.EmbeddingLLMName,
							note.Value as string).GetAwaiter().GetResult();

					string stringVector = string.Join(";", vector.ToArray().Select(f => f.ToString()));

					if (!vectorDataSet.WordFiles.Exists(note.FileName))
						vectorDataSet.WordFiles.AddWordFilesRow(note.FileName, note.FileCaption, note.FileDescription, note.FileDate);

					WordFilesRow wordFiles = vectorDataSet.WordFiles.Get(note.FileName);

					vectorDataSet.ParagraphVectorStore.AddParagraphVectorStoreRow(wordFiles,
						note.Value as string, stringVector, note.WordSelectionStart, note.WordSelectionEnd);
				}
			}
		}



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

		internal void Ai(Word.Selection selection, string systemMessage, string userMessage)
		{
			Ai(selection: selection, systemMessage: systemMessage, userMessages: new string[] { userMessage, selection.Text });
		}

		internal void Ai(Word.Selection selection, string systemMessage, string[] userMessages)
		{
			_ = Ai(range: selection.Range, systemMessage: systemMessage, userMessages: userMessages);
		}

		internal void Ai(Word.Selection selection)
		{
			_ = Ai(range: selection.Range, systemMessage: string.Empty, userMessages: new string[] { selection.Text });
		}

		internal async Task Ai(Word.Range range, string systemMessage, string[] userMessages)
		{
			bool isAvailable = LLMConnectorLibrary.Utils.Net.CheckHostByHttp(new Uri(baseUri: Services.OpenAIService.Uri, relativeUri: Services.OpenAIService.Uri.AbsolutePath + "/models"));

			if (isAvailable)
			{
				try
				{
					range.Text = DocumentCollection.AI_STATUS_TEXT + " ...";
					range.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
					range.Text = await LLMConnectorLibrary.LLMOpenAI.SendMessageAsync(uri: Services.OpenAIService.Uri, timeout: Services.OpenAIService.Timeout, model: Services.OpenAIService.LLMName, options: Services.OpenAIService.Options, systemMessage: systemMessage, userMessages: userMessages);
					range.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightGreen;
				}
				catch (Exception ex)
				{
					Utils.Dialogs.ShowMessageDialog(ex.Message);
				}
			}
			else
			{
				Utils.Dialogs.ShowMessageDialog(string.Format("Проверьте доступ к провайдеру по адресу: [{0}]", Services.OpenAIService.Uri));
			}
		}



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





	}
}
