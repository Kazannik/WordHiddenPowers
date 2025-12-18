// Ignore Spelling: uri

using Microsoft.Extensions.Primitives;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Embeddings;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WordHiddenPowers.Dialogs;
using ChatMessage = OpenAI.Chat.ChatMessage;

#pragma warning disable OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.

namespace WordHiddenPowers.Services
{

	static class OpenAIService
	{
		public static string MainSystemMessage { get; set; } = "Твоя аудитория профессиональные юристы. При подготовке ответа не фантазируй и не добавляй свои комментарии.";

		public static string CaptionButton1 { get; set; } = "Кнопка 1";
		public static string SystemMessageButton1 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton1 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton1 { get; set; } = string.Empty;

		public static string CaptionButton2 { get; set; } = "Кнопка 2";
		public static string SystemMessageButton2 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton2 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton2 { get; set; } = string.Empty;


		private const string OPENAI_API_KEY = "OPENAI_API_KEY";

		public static Uri Uri { get; set; } = new Uri(baseUri: new Uri("http://localhost:11434"), relativeUri: "/v1");

		public static string LLMName { get; set; } = "mistral:latest";

		public static void ShowSettingDialog(Documents.Document document)
		{
			SettingDialog dialog = new SettingDialog(document: document, selectedLLM: LLMName, uri: Uri);
			if (Utils.Dialogs.ShowDialog(dialog) == System.Windows.Forms.DialogResult.OK)
			{
				Uri = dialog.SelectedUri;
				LLMName = dialog.SelectedLLMName;
			}
		}

		public static IEnumerable<OpenAIModel> GetModels() => GetModels(uri: Uri);

		public static IEnumerable<OpenAIModel> GetModels(Uri uri)
		{
			Task<IEnumerable<OpenAIModel>> task = GetModelsAsync(uri: uri);
			task.Wait();
			return task.Result;
		}

		public async static Task<IEnumerable<OpenAIModel>> GetModelsAsync() => await GetModelsAsync(uri: Uri);

		public async static Task<IEnumerable<OpenAIModel>> GetModelsAsync(Uri uri)
		{
			OpenAIClientOptions options = new OpenAIClientOptions
			{
				Endpoint = uri
			};

			bool isAvailable = await NetService.CheckHostByHttpAsync(new Uri(baseUri: options.Endpoint, relativeUri: options.Endpoint.AbsolutePath + "/models"));

			if (isAvailable)
			{
				OpenAIClient client = new OpenAIClient(
					new ApiKeyCredential(OPENAI_API_KEY),
					options);
				OpenAIModelClient modelClient = client.GetOpenAIModelClient();
				try
				{
					ClientResult<OpenAIModelCollection> modelsResult = await modelClient.GetModelsAsync();
					return modelsResult.Value;
				}
				catch (Exception)
				{
					return new OpenAIModel[] { };
				}
			}
			else
			{
				return new OpenAIModel[] { };
			}
		}

		public async static Task<string> SendMessageAsync(string model, string systemMessage, string userMessage)
		{
			OpenAIClientOptions options = new OpenAIClientOptions
			{
				Endpoint = Uri,
				NetworkTimeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0),
			};

			OpenAIClient client = new OpenAIClient(
				new ApiKeyCredential(OPENAI_API_KEY),
				options);

			ChatClient chatClient = client.GetChatClient(model: model);
			List<ChatMessage> chatMessages = new List<ChatMessage>();
			if (!string.IsNullOrWhiteSpace(systemMessage))
				chatMessages.Add(new SystemChatMessage(systemMessage));

			chatMessages.Add(new UserChatMessage(userMessage));

			ClientResult<ChatCompletion> creativeWriterResult = await chatClient.CompleteChatAsync(
				chatMessages,
				new ChatCompletionOptions()
				{
					MaxOutputTokenCount = 2048,
					Temperature = 0,
				});
			return creativeWriterResult.Value.Content[0].Text;
		}








		public static void CreateMultipleClients()
		{
			OpenAIClientOptions options = new OpenAIClientOptions
			{
				Endpoint = Uri
			};

			OpenAIClient client = new OpenAIClient(
				new ApiKeyCredential(OPENAI_API_KEY),
				options);

			OpenAIModelClient modelClient = client.GetOpenAIModelClient();
			modelClient.GetModels();

			EmbeddingClient embeddingClient = client.GetEmbeddingClient("mistral");
			ChatClient chatClient = client.GetChatClient("mistral");

			ClientResult<ChatCompletion> creativeWriterResult = chatClient.CompleteChat(
			new ChatMessage[] {
				new SystemChatMessage("Ты профессиональный юрист. Проводишь консультацию клиента. Не фантазируй. Дай короткий ответ."),
				new UserChatMessage("Ответь на вопрос: Какой суд будет рассматривать иск к представителям из другой галактики?"),
			},
			new ChatCompletionOptions()
			{
				MaxOutputTokenCount = 2048,
			});

			string description = creativeWriterResult.Value.Content[0].Text;
			Console.WriteLine($"Creative helper's creature description:\n{description}");
		}


		#region

		private static string GetDateTimeNow()
		{
			return DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
		}

		private static string GetCurrentLocation()
		{
			return "Северное Тушино, Москва";
		}

		private static string GetCurrentWeather(string location, string unit = "celsius")
		{
			return $"31 {unit} в {location}";
		}

		#endregion

		#region

		private static readonly ChatTool getDateTimeNowTool = ChatTool.CreateFunctionTool(
			functionName: nameof(GetDateTimeNow),
			functionDescription: "Текущие дата и время"
		);

		private static readonly ChatTool getCurrentLocationTool = ChatTool.CreateFunctionTool(
			functionName: nameof(GetCurrentLocation),
			functionDescription: "Местоположение пользователя"
		);

		private static readonly ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
			functionName: nameof(GetCurrentWeather),
			functionDescription: "Погода в указанном месте",
			functionParameters: BinaryData.FromBytes(Utils.Resource.GetBytesResource("WordHiddenPowers.Repository.Data.get_we.json"))
		);

		#endregion

		public static void Example03_FunctionCalling()
		{
			OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions
			{
				Endpoint = Uri,
				NetworkTimeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0),
			};

			OpenAIClient client = new OpenAIClient(
				new ApiKeyCredential(OPENAI_API_KEY),
				openAIClientOptions);

			ChatClient chatClient = client.GetChatClient(model: LLMName);

			#region

			List<ChatMessage> messages = new List<ChatMessage>(new ChatMessage[]
			{
				new UserChatMessage("Какая сегодня дата? Сколько сейчас времени? Какая погода в данное время?"),
			});

			ChatCompletionOptions options = new ChatCompletionOptions()
			{
				Tools = { getCurrentLocationTool, getCurrentWeatherTool, getDateTimeNowTool },
			};

			#endregion

			#region
			bool requiresAction;

			do
			{
				requiresAction = false;
				ChatCompletion completion = chatClient.CompleteChat(messages, options);

				switch (completion.FinishReason)
				{
					case ChatFinishReason.Stop:
						{
							messages.Add(new AssistantChatMessage(completion));
							break;
						}

					case ChatFinishReason.ToolCalls:
						{
							messages.Add(new AssistantChatMessage(completion));

							foreach (ChatToolCall toolCall in completion.ToolCalls)
							{
								switch (toolCall.FunctionName)
								{
									case nameof(GetDateTimeNow):
										{
											string toolResult = GetDateTimeNow();
											messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
											break;
										}
									case nameof(GetCurrentLocation):
										{
											string toolResult = GetCurrentLocation();
											messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
											break;
										}
									case nameof(GetCurrentWeather):
										{
											JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
											bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
											bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

											if (!hasLocation)
											{
												throw new ArgumentNullException(nameof(location), "The location argument is required.");
											}

											string toolResult = hasUnit
												? GetCurrentWeather(location.GetString(), unit.GetString())
												: GetCurrentWeather(location.GetString());
											messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
											break;
										}

									default:
										{
											throw new NotImplementedException();
										}
								}
							}
							requiresAction = true;
							break;
						}

					case ChatFinishReason.Length:
						throw new NotImplementedException("Incomplete model output due to MaxTokens parameter or token limit exceeded.");

					case ChatFinishReason.ContentFilter:
						throw new NotImplementedException("Omitted content due to a content filter flag.");

					case ChatFinishReason.FunctionCall:
						throw new NotImplementedException("Deprecated in favor of tool calls.");

					default:
						throw new NotImplementedException(completion.FinishReason.ToString());
				}
			} while (requiresAction);

			#endregion

			#region

			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"DateTime: {DateTime.Now.ToString()}\n\n");
			stringBuilder.Append($"Model: {LLMName}\n\n");

			foreach (ChatMessage message in messages)
			{
				switch (message)
				{
					case UserChatMessage userMessage:
						stringBuilder.Append($"[USER]:");
						stringBuilder.Append($"{userMessage.Content[0].Text}\n");
						break;
					case AssistantChatMessage assistantMessage when assistantMessage.Content.Count > 0:
						stringBuilder.Append($"[ASSISTANT]:");
						stringBuilder.Append($"{assistantMessage.Content[0].Text}\n");
						break;
					case ToolChatMessage toolMessage when toolMessage.Content.Count > 0:
						stringBuilder.Append($"[TOOL]:");
						stringBuilder.Append($"{toolMessage.Content[0].Text}\n");
						break;
					default:
						stringBuilder.Append($"[other]\n");
						break;
				}
			}
			#endregion

		 Utils.Dialogs.ShowMessageDialog(stringBuilder.ToString());

		}	
	}
}

#pragma warning restore OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.
