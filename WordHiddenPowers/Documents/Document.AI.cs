// Ignore Spelling: dest

using Microsoft.Extensions.AI;
using Microsoft.Office.Tools;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Embeddings;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorboard;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Utils;
using static WordHiddenPowers.Dialogs.LLMChatDialog;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using DataTable = System.Data.DataTable;
using Office = Microsoft.Office.Core;
using Table = WordHiddenPowers.Repository.Data.Table;
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

		internal void Ai(Word.Selection selection, string systemMessage, string prefixUserMessage)
		{
			Ai(selection: selection, systemMessage: systemMessage, prefixUserMessage: prefixUserMessage, userMessage: selection.Text);			
		}

		internal void Ai(Word.Selection selection, string systemMessage, string prefixUserMessage, string userMessage)
		{
			_ = Ai(range: selection.Range, systemMessage: systemMessage, userMessage: prefixUserMessage + userMessage);
		}

		internal void Ai(Word.Selection selection)
		{
			_ = Ai(range: selection.Range, systemMessage: string.Empty, userMessage: selection.Text);
		}

		internal async Task Ai(Word.Range range, string systemMessage, string userMessage)
		{
			bool isAvailable = await Services.NetService.CheckHostByHttpAsync(new Uri(baseUri: Services.OpenAIService.Uri, relativeUri: Services.OpenAIService.Uri.AbsolutePath + "/models"));

			if (isAvailable)
			{
				try
				{
					range.Text = DocumentCollection.AI_STATUS_TEXT + " ...";
					range.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
					range.Text = await Services.OpenAIService.SendMessageAsync(model: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessage: userMessage);
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


		#region OpenAi

		public void CreateMultipleClients()

		{
			OpenAIClientOptions options = new OpenAIClientOptions
			{
				Endpoint = Services.OpenAIService.Uri
			};


			OpenAIClient client = new OpenAIClient(
				new ApiKeyCredential("ollama"),
				options); // new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

			OpenAI.Models.OpenAIModelClient openAIModelClient = client.GetOpenAIModelClient();
			 openAIModelClient.GetModels();

			EmbeddingClient embeddingClient = client.GetEmbeddingClient("mistral");
			ChatClient chatClient = client.GetChatClient("mistral");

			ClientResult<ChatCompletion> creativeWriterResult = chatClient.CompleteChat(
			new OpenAI.Chat.ChatMessage[] {
				new SystemChatMessage("Ты профессиональный юрист. Проводишь консультацию клиента. Не фантазируй. Давай короткий ответ."),
				new UserChatMessage("Ответь на вопрос: Какой суд будет рассматривать иск к представителям из другой галактики?"),
			},
			new ChatCompletionOptions()
			{
				MaxOutputTokenCount = 2048,
			});

			string description = creativeWriterResult.Value.Content[0].Text;
			Console.WriteLine($"Creative helper's creature description:\n{description}");
		}

		#endregion

		
	}
}
