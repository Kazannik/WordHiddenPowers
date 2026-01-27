// Ignore Spelling: dest


using System;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class Document
    {

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
			bool isAvailable = await LLMConnectorLibrary.Utils.Net.CheckHostByHttpAsync(new Uri(baseUri: Services.OpenAIService.Uri, relativeUri: Services.OpenAIService.Uri.AbsolutePath + "/models"));

			if (isAvailable)
			{
				try
				{
					range.Text = DocumentCollection.AI_STATUS_TEXT + " ...";
					range.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
					range.Text = await LLMConnectorLibrary.LLMOpenAI.SendMessageAsync(uri: Services.OpenAIService.Uri, model: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessages: userMessages);
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
