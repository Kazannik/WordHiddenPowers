using LLMConnectorLibrary;
using System.Data;
using System.Linq;
using Options = WordHiddenPowers.Repository.Setting.ChatOptions;

namespace WordHiddenPowers.Repository
{
	partial class GlobalsSetting
	{
		public string DefaultMLModelName
		{
			get => Setting.GetValue("DefaultMLModelName");
			set => Setting.SetValue("DefaultMLModelName", value: value);
		}

		public string DefaultChatLLModelName
		{
			get => Setting.GetValue(key: "DefaultChatLLModelName", defaultValue: "mistral:latest");
			set => Setting.SetValue(key: "DefaultChatLLModelName", value: value);
		}

		public string DefaultChatLLModelProfileEndpoint
		{
			get => Setting.GetValue(key: "DefaultChatLLModelProfileEndpoint", defaultValue: "http://localhost");
			set => Setting.SetValue(key: "DefaultChatLLModelProfileEndpoint", value: value);
		}

		public string DefaultEmbedLLModelName
		{
			get => Setting.GetValue(key: "DefaultEmbedLLModelName", defaultValue: "mistral:latest");
			set => Setting.SetValue(key: "DefaultEmbedLLModelName", value: value);
		}

		public string DefaultSystemMessage
		{
			get => Setting.GetValue(key: "DefaultSystemMessage",
				defaultValue: "Твоя аудитория профессиональные юристы. При подготовке ответа не фантазируй и не добавляй свои комментарии.");
			set => Setting.SetValue(key: "DefaultSystemMessage", value: value);
		}

		public IChatOptions DefaultChatOptions
		{
			get => ChatOptions.GetValue(key: "DefaultChatOptions",
				defaultValue: Options.Create("DefaultChatOptions"));
			set => ChatOptions.SetValue(key: "DefaultChatOptions", value: value);
		}

		public string CaptionButton1
		{
			get => Setting.GetValue(key: "CaptionButton_1", defaultValue: "Кнопка 1");
			set => Setting.SetValue(key: "CaptionButton_1", value: value);
		}

		public string SystemMessageButton1
		{
			get => Setting.GetValue(key: "SystemMessageButton_1", defaultValue: "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса");
			set => Setting.SetValue(key: "SystemMessageButton_1", value: value);
		}

		public string PrefixUserMessageButton1
		{
			get => Setting.GetValue(key: "PrefixUserMessageButton_1");
			set => Setting.SetValue(key: "PrefixUserMessageButton_1", value: value);
		}

		public string PostfixUserMessageButton1
		{
			get => Setting.GetValue(key: "PostfixUserMessageButton_1");
			set => Setting.SetValue(key: "PostfixUserMessageButton_1", value: value);
		}

		public string ChatLLModelNameButton1
		{
			get => Setting.GetValue(key: "ChatLLModelNameButton_1", defaultValue: "mistral:latest");
			set => Setting.SetValue(key: "ChatLLModelNameButton_1", value: value);
		}

		public IChatOptions ChatOptionsButton1
		{
			get => ChatOptions.GetValue(key: "ChatOptionsButton_1",
				defaultValue: Options.Create("ChatOptionsButton_1"));
			set => ChatOptions.SetValue(key: "ChatOptionsButton_1", value: value);
		}

		public string CaptionButton2
		{
			get => Setting.GetValue(key: "CaptionButton_2", defaultValue: "Кнопка 2");
			set => Setting.SetValue(key: "CaptionButton_2", value: value);
		}

		public string SystemMessageButton2
		{
			get => Setting.GetValue(key: "SystemMessageButton_2", defaultValue: "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса");
			set => Setting.SetValue(key: "SystemMessageButton_2", value: value);
		}

		public string PrefixUserMessageButton2
		{
			get => Setting.GetValue(key: "PrefixUserMessageButton_2");
			set => Setting.SetValue(key: "PrefixUserMessageButton_2", value: value);
		}

		public string PostfixUserMessageButton2
		{
			get => Setting.GetValue(key: "PostfixUserMessageButton_2");
			set => Setting.SetValue(key: "PostfixUserMessageButton_2", value: value);
		}

		public string ChatLLModelNameButton2
		{
			get => Setting.GetValue(key: "ChatLLModelNameButton_2", defaultValue: "mistral:latest");
			set => Setting.SetValue(key: "ChatLLModelNameButton_2", value: value);
		}

		public IChatOptions ChatOptionsButton2
		{
			get => ChatOptions.GetValue(key: "ChatOptionsButton_2",
				defaultValue: Options.Create("ChatOptionsButton_2"));
			set => ChatOptions.SetValue(key: "ChatOptionsButton_2", value: value);
		}

		partial class SettingDataTable
		{
			public string GetValue(string key, string defaultValue)
			{
				if (Exists(key))
					return GetValue(key: key);
				else
					return defaultValue;
			}

			public string GetValue(string key) => GetOrDefault(key: key)?["value"] as string;

			public void SetValue(string key, string value)
			{
				if (Exists(key))
				{
					SettingRow row = Get(key: key);
					row.BeginEdit();
					row.value = value;
					row.EndEdit();
				}
				else
				{
					Rows.Add([key, value]);
				}
			}

			public void RemoveValue(string key)
			{
				DataRow row = GetOrDefault(key: key);
				row?.Delete();
			}

			private DataRow GetOrDefault(string key)
			{
				if (Exists(key: key))
				{
					return Get(key: key);
				}
				else
				{
					return null;
				}
			}

			private SettingRow Get(string key) => (from SettingRow row in this
												   where row.RowState != DataRowState.Deleted
												   && row.key.Equals(key)
												   select row).First();
			private bool Exists(string key) => (from SettingRow row in this
												where row.RowState != DataRowState.Deleted
												&& row.key.Equals(key)
												select row).Any();
		}

		partial class ChatOptionsDataTable
		{
			public IChatOptions GetValue(string key, IChatOptions defaultValue)
			{
				if (Exists(key))
					return GetValue(key: key);
				else
					return defaultValue;
			}

			public IChatOptions GetValue(string key)
			{
				if (Exists(key))
				{
					ChatOptionsRow row = Get(key: key);
					return new Options(row.key, row.max_output_token_count, row.frequency_penalty, row.presence_penalty, row.temperature, row.top_p);
				}
				else
				{
					return null;
				}
			}

			public void SetValue(string key, IChatOptions value)
			{
				if (Exists(key))
				{
					ChatOptionsRow row = Get(key: key);
					row.BeginEdit();
					row.max_output_token_count = value.MaxOutputTokenCount.GetValueOrDefault(0);
					row.frequency_penalty = value.FrequencyPenalty.GetValueOrDefault(0);
					row.presence_penalty = value.PresencePenalty.GetValueOrDefault(0);
					row.temperature = value.Temperature.GetValueOrDefault(0);
					row.top_p = value.TopP.GetValueOrDefault(0);
					row.EndEdit();
				}
				else
				{
					Rows.Add([key, value.MaxOutputTokenCount, value.FrequencyPenalty, value.PresencePenalty, value.Temperature, value.TopP]);
				}
			}

			public void RemoveValue(string key)
			{
				DataRow row = GetOrDefault(key: key);
				row?.Delete();
			}

			private DataRow GetOrDefault(string key)
			{
				if (Exists(key: key))
				{
					return Get(key: key);
				}
				else
				{
					return null;
				}
			}

			private ChatOptionsRow Get(string key) => (from ChatOptionsRow row in this
													   where row.RowState != DataRowState.Deleted
													   && row.key.Equals(key)
													   select row).First();

			private bool Exists(string key) => (from ChatOptionsRow row in Rows
												where row.RowState != DataRowState.Deleted
												&& row.key.Equals(key)
												select row).Any();
		}
	}
}
