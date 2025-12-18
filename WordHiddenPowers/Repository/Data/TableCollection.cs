using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

namespace WordHiddenPowers.Repository.Data
{
	public class TableCollection: List<(Table current, Table old)>
	{
		public TableCollection() { }







		#region

		private static string GetCurrentLocation()
		{
			return "Северное Тушино, Москва";
		}

		private static string GetCurrentWeather(string location, string unit = "celsius")
		{
			return $"31 {unit}";
		}

		#endregion

		private static readonly string WEAR = "{'type': 'object', 'properties': {'location': {'type': 'string', 'description': 'текущее местоположение (из GetCurrentLocation)'}, 'unit': {'type': 'string', 'enum': [ 'celsius', 'fahrenheit' ], 'description': 'единица измерения температуры.' } }, 'required': [ 'location' ] }".Replace("'", "\"");

		#region

		private static readonly ChatTool getCurrentLocationTool = ChatTool.CreateFunctionTool(
			functionName: nameof(GetCurrentLocation),
			functionDescription: "Местоположение пользователя"
		);

		private static readonly ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
			functionName: nameof(GetCurrentWeather),
			functionDescription: "Погода в указанном месте",
			functionParameters: BinaryData.FromBytes(Utils.Resource.GetBytesResource("get_we.json"))
		);

		#endregion

		public static void Example03_FunctionCalling(ChatClient chatClient)
		{
			


			#region

			List<ChatMessage> messages = new List<ChatMessage>(new ChatMessage[]
			{
				new UserChatMessage("Какая погода сегодня?"),
			});

			ChatCompletionOptions options = new ChatCompletionOptions()
			{
				Tools = { getCurrentLocationTool, getCurrentWeatherTool },
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

			foreach (ChatMessage message in messages)
			{
				switch (message)
				{
					case UserChatMessage userMessage:
						stringBuilder.Append($"[USER]:");
						stringBuilder.Append($"{userMessage.Content[0].Text}");
						break;
					case AssistantChatMessage assistantMessage when assistantMessage.Content.Count > 0:
						stringBuilder.Append($"[ASSISTANT]:");
						stringBuilder.Append($"{assistantMessage.Content[0].Text}");
						break;
					case ToolChatMessage toolMessage:
						stringBuilder.Append($"[TOOL]:");
						stringBuilder.Append($"{toolMessage.Content[0].Text}");
						break;
					default:
						break;
				}
			}
			#endregion

			stringBuilder.ToString();

		}
	}
}
