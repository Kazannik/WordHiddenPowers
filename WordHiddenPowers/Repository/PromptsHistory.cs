using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WordHiddenPowers.EventsBus;
using Document = WordHiddenPowers.Documents.Document;
using DtoModel = WordHiddenPowers.Repository.History.DtoModel;
using DtoPrompt = WordHiddenPowers.Repository.History.DtoPrompt;

namespace WordHiddenPowers.Repository
{
}

namespace WordHiddenPowers.Repository
{
	partial class PromptsHistory
	{
		public PromptsRow Add(
			IModel model,
			IChatOptions options,
			Document.ChatMessageModeEnum messageMode,
			IEnumerable<string> systemMessages,
			IEnumerable<string> userMessages,
			bool isFavorite = false)
		{
			return Add(systemMessages: systemMessages,
				userMessages: userMessages,
				description: string.Empty,
				modelName: model.Id,
				modelDescription: string.Empty,
				endpoint: model.Profile.ClientOptions.Endpoint.OriginalString,
				maxOutputTokenCount: options.MaxOutputTokenCount.GetValueOrDefault(),
				frequencyPenalty: options.FrequencyPenalty.GetValueOrDefault(),
				presencePenalty: options.PresencePenalty.GetValueOrDefault(),
				temperature: options.Temperature.GetValueOrDefault(),
				topP: options.TopP.GetValueOrDefault(),
				insertHereMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertHere),
				replaceSelectionMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.ReplaceSelection),
				insertNextMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertNext),
				insertPreviousMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertPrevious),
				insertBetweenMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertBetween),
				isFavorite: isFavorite);
		}

		public PromptsRow Add(
			string endpoint,
			string modelName,
			string modelDescription,
			IChatOptions options,
			Document.ChatMessageModeEnum messageMode,
			string promptDescription,
			IEnumerable<string> systemMessages,
			IEnumerable<string> userMessages,
			bool isFavorite = false)
		{
			return Add(systemMessages: systemMessages,
					userMessages: userMessages,
					description: promptDescription,
					modelName: modelName,
					modelDescription: modelDescription,
					endpoint: endpoint,
					maxOutputTokenCount: options.MaxOutputTokenCount.GetValueOrDefault(),
					frequencyPenalty: options.FrequencyPenalty.GetValueOrDefault(),
					presencePenalty: options.PresencePenalty.GetValueOrDefault(),
					temperature: options.Temperature.GetValueOrDefault(),
					topP: options.TopP.GetValueOrDefault(),
					insertHereMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertHere),
					replaceSelectionMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.ReplaceSelection),
					insertNextMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertNext),
					insertPreviousMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertPrevious),
					insertBetweenMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertBetween),
					isFavorite: isFavorite);
		}

		public PromptsRow Add(
			IEnumerable<string> systemMessages,
			IEnumerable<string> userMessages,
			string description,
			string modelName,
			string modelDescription,
			string endpoint,
			int maxOutputTokenCount,
			float frequencyPenalty,
			float presencePenalty,
			float temperature,
			float topP,
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertBetweenMessage,
			bool isFavorite = false)
		{
			return Add(
				date: DateTime.Now,
				systemMessages: systemMessages,
				userMessages: userMessages,
				description: description,
				modelName: modelName,
				modelDescription: modelDescription,
				endpoint: endpoint,
				maxOutputTokenCount: maxOutputTokenCount,
				frequencyPenalty: frequencyPenalty,
				presencePenalty: presencePenalty,
				temperature: temperature,
				topP: topP,
				insertHereMessage: insertHereMessage,
				replaceSelectionMessage: replaceSelectionMessage,
				insertNextMessage: insertNextMessage,
				insertPreviousMessage: insertPreviousMessage,
				insertBetweenMessage: insertBetweenMessage,
				isFavorite: isFavorite);
		}

		public PromptsRow Add(
			DateTime date,
			IEnumerable<string> systemMessages,
			IEnumerable<string> userMessages,
			string description,
			string modelName,
			string modelDescription,
			string endpoint,
			int maxOutputTokenCount,
			float frequencyPenalty,
			float presencePenalty,
			float temperature,
			float topP,
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertBetweenMessage,
			bool isFavorite = false)
		{
			ProfilesRow profilesRow = Profiles.GetOrNewRow(endpoint: endpoint);

			ModelsRow modelRow = Models.GetOrNewRow(modelName: modelName, modelDescription: modelDescription, profilesRow: profilesRow);

			PromptOptionsRow optionsRow = PromptOptions.GetOrNewRow(
				maxOutputTokenCount: maxOutputTokenCount,
				frequencyPenalty: frequencyPenalty,
				presencePenalty: presencePenalty,
				temperature: temperature,
				topP: topP);

			PromptModeRow modeRow = PromptMode.GetOrNewRow(
				insertHereMessage: insertHereMessage,
				replaceSelectionMessage: replaceSelectionMessage,
				insertNextMessage: insertNextMessage,
				insertPreviousMessage: insertPreviousMessage,
				insertBetweenMessage: insertBetweenMessage);

			IEnumerable<SystemMessagesRow> systemMessagesRows = systemMessages
				.Select(SystemMessages.GetOrNewRow);

			IEnumerable<UserMessagesRow> userMessagesRows = userMessages
				.Select(UserMessages.GetOrNewRow);

			IEnumerable<int> userMessagesIds = userMessagesRows
				.Select(row => row.id);

			IEnumerable<PromptsRow> promptRows = UserMessagesReference
				.Where(row => userMessagesIds.Contains(row.message_id))
				.Select(row => row.PromptsRow);

			PromptsRow promptsRow;

			if (promptRows.Any())
			{
				promptsRow = promptRows.First();
				promptsRow.BeginEdit();
				promptsRow.date = date;
				promptsRow.description = description;
				promptsRow.ModelsRow = modelRow;
				promptsRow.PromptOptionsRow = optionsRow;
				promptsRow.PromptModeRow = modeRow;
				promptsRow.is_favorite = isFavorite;
				promptsRow.EndEdit();
			}
			else
			{
				promptsRow = Prompts.AddPromptsRow(
				date: date,
				description: description,
				parentModelsRowBymodels_prompts: modelRow,
				parentPromptOptionsRowByprompt_options_prompts: optionsRow,
				parentPromptModeRowByprompt_mode_prompts: modeRow,
				is_favorite: isFavorite);

				foreach (UserMessagesRow row in userMessagesRows)
				{
					UserMessagesReference.GetOrNewRow(promptsRow, row);
				}
			}

			foreach (SystemMessagesRow messageRow in systemMessagesRows)
			{
				SystemMessagesReference.GetOrNewRow(promptsRow: promptsRow, messageRow);
			}

			GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this,
				state: EventsBus.StateEnums.PromptsHistoryState.Added,
				prompt: DtoPrompt.Create(row: promptsRow, systemMessageRows: [.. systemMessagesRows], userMessageRows: [.. userMessagesRows]));

			return promptsRow;
		}

		public IEnumerable<DtoPrompt> GetPrompts(Document.ChatMessageModeEnum messageMode)
		{
			return GetPrompts(
				insertHereMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertHere),
				replaceSelectionMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.ReplaceSelection),
				insertNextMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertNext),
				insertPreviousMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertPrevious),
				insertBetweenMessage: messageMode.HasFlag(Document.ChatMessageModeEnum.InsertBetween));
		}

		public IEnumerable<DtoPrompt> GetPrompts(
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertBetweenMessage)
		{
			return from row in Prompts.OrderBy(r => r.date)
				   where row.RowState != DataRowState.Deleted &&
				   row.PromptModeRow != null &&
				   (row.PromptModeRow.insert_here_message == insertHereMessage == true ||
				   row.PromptModeRow.replace_selection_message == replaceSelectionMessage == true ||
				   row.PromptModeRow.insert_next_message == insertNextMessage == true ||
				   row.PromptModeRow.insert_previous_message == insertPreviousMessage == true ||
				   row.PromptModeRow.insert_between_message == insertBetweenMessage == true)
				   select DtoPrompt.Create(row: row, SystemMessagesReference.GetRowsByPrompt(row), UserMessagesReference.GetRowsByPrompt(row));
		}

		public IEnumerable<DtoPrompt> GetPrompts()
		{
			return from row in Prompts.OrderBy(r => r.date)
				   where row.RowState != DataRowState.Deleted
				   select DtoPrompt.Create(row: row, SystemMessagesReference.GetRowsByPrompt(row), UserMessagesReference.GetRowsByPrompt(row));
		}

		/// <summary>
		/// Создание записи о новом промпте.
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public PromptsRow CreatePrompt(DtoPrompt prompt)
		{
			return Add(
				date: prompt.Date,
				systemMessages: prompt.SystemMessages,
				userMessages: prompt.UserMessages,
				description: prompt.Description,
				modelName: prompt.ModelName,
				modelDescription: prompt.ModelDescription,
				endpoint: prompt.ProfileEndpoint,
				maxOutputTokenCount: prompt.MaxOutputTokenCount,
				frequencyPenalty: prompt.FrequencyPenalty,
				presencePenalty: prompt.PresencePenalty,
				temperature: prompt.Temperature,
				topP: prompt.TopP,
				insertHereMessage: prompt.InsertHereMessage,
				replaceSelectionMessage: prompt.ReplaceSelectionMessage,
				insertNextMessage: prompt.InsertNextMessage,
				insertPreviousMessage: prompt.InsertPreviousMessage,
				insertBetweenMessage: prompt.InsertBetweenMessage,
				isFavorite: prompt.IsFavorite);
		}

		public DtoPrompt GetDtoPrompt(int id)
		{
			PromptsRow promptsRow = Prompts.Get(id: id);
			SystemMessagesRow[] systemMessagesRows = SystemMessagesReference.GetRowsByPrompt(promptsRow: promptsRow);
			UserMessagesRow[] userMessagesRows = UserMessagesReference.GetRowsByPrompt(promptsRow: promptsRow);
			return DtoPrompt.Create(row: promptsRow, systemMessageRows: systemMessagesRows, userMessageRows: userMessagesRows);
		}

		public PromptsRow EditPrompt(
			int promptId,
			DateTime date,
			string endpoint,
			string modelName,
			string modelDescription,
			IChatOptions options,
			string promptDescription,
			IEnumerable<string> systemMessages,
			IEnumerable<string> userMessages)
		{
			ProfilesRow profilesRow = Profiles.GetOrNewRow(endpoint: endpoint);
			ModelsRow modelRow = Models.GetOrNewRow(modelName: modelName, modelDescription: modelDescription, profilesRow: profilesRow);
			PromptOptionsRow optionsRow = PromptOptions.GetOrNewRow(options: options);

			PromptsRow promptsRow = Prompts.Get(id: promptId);

			promptsRow.BeginEdit();

			promptsRow.date = date;
			promptsRow.description = promptDescription;
			promptsRow.model_id = modelRow.id;
			promptsRow.prompt_options_id = optionsRow.id;
			
			promptsRow.EndEdit();

			RemoveSystemMessages(promptId: promptId);
			foreach (string message in systemMessages)
			{
				SystemMessagesRow messageRow = SystemMessages.GetOrNewRow(message: message);
				SystemMessagesReference.AddSystemMessagesReferenceRow(
					parentPromptsRowByprompts_system_messages_reference: promptsRow,
					parentSystemMessagesRowBysystem_messages_system_messages_reference: messageRow);
			}

			RemoveUserMessages(promptId: promptId);
			foreach (string message in userMessages)
			{
				UserMessagesRow messageRow = UserMessages.GetOrNewRow(message: message);
				UserMessagesReference.AddUserMessagesReferenceRow(
					parentPromptsRowByprompts_user_messages_reference: promptsRow,
					parentUserMessagesRowByuser_messages_user_messages_reference: messageRow);
			}

			GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Modified, prompt: CreateDtoPrompt(row: promptsRow));

			return promptsRow;
		}

		public void Remove(DtoPrompt prompt)
		{
			if (Prompts.Exists(prompt.Id))
			{
				PromptsRow promptsRow = Prompts.Get(prompt.Id);

				RemoveModel(promptsRow.ModelsRow);
				RemovePromptMode(promptsRow.PromptModeRow);
				RemovePromptOptions(promptsRow.PromptOptionsRow);
				RemoveSystemMessages(promptsRow.id);
				RemoveUserMessages(promptsRow.id);

				Prompts.Remove(promptsRow.id);
				GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Deleted, prompt: prompt);
			}
		}

		private void RemoveModel(ModelsRow modelRow)
		{
			if (Prompts.Count(row => row.RowState != DataRowState.Deleted &&
			row.model_id == modelRow.id) == 1)
			{
				RemoveProfile(modelRow.ProfilesRow);
				Models.Rows.Remove(modelRow);
			}
		}

		private void RemoveProfile(ProfilesRow profileRow)
		{
			if (Models.Count(row => row.RowState != DataRowState.Deleted &&
			row.profile_id == profileRow.id) == 1)
			{
				Profiles.Rows.Remove(profileRow);
			}
		}

		private void RemovePromptMode(PromptModeRow modeRow)
		{
			if (Prompts.Count(row => row.RowState != DataRowState.Deleted &&
			row.model_id == modeRow.id) == 1)
			{
				PromptMode.Rows.Remove(modeRow);
			}
		}

		private void RemovePromptOptions(PromptOptionsRow optionsRow)
		{
			if (Prompts.Count(row => row.RowState != DataRowState.Deleted &&
			row.prompt_options_id == optionsRow.id) == 1)
			{
				PromptOptions.Rows.Remove(optionsRow);
			}
		}

		private void RemoveSystemMessages(int promptId)
		{
			List<SystemMessagesReferenceRow> references = [.. SystemMessagesReference.Where(row => row.prompt_id == promptId)];
			foreach (SystemMessagesReferenceRow referenceRow in references)
			{
				if (SystemMessagesReference.Count(row => row.RowState != DataRowState.Deleted &&
				row.message_id == referenceRow.message_id) == 1)
				{
					SystemMessages.Rows.Remove(referenceRow.SystemMessagesRow);
				}
				SystemMessagesReference.Rows.Remove(referenceRow);
			}
		}

		private void RemoveUserMessages(int promptId)
		{
			List<UserMessagesReferenceRow> references = [.. UserMessagesReference.Where(row => row.prompt_id == promptId)];
			foreach (UserMessagesReferenceRow referenceRow in references)
			{
				if (UserMessagesReference.Count(row => row.RowState != DataRowState.Deleted &&
				row.message_id == referenceRow.message_id) == 1)
				{
					UserMessages.Rows.Remove(referenceRow.UserMessagesRow);
				}
				UserMessagesReference.Rows.Remove(referenceRow);
			}
		}

		public void Clear()
		{
			SystemMessagesReference.Clear();
			SystemMessages.Clear();
			UserMessagesReference.Clear();
			UserMessages.Clear();

			PromptMode.Clear();
			PromptOptions.Clear();
			Models.Clear();
			Profiles.Clear();
			Prompts.Clear();

			GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Cleared, prompt: null);
		}

		/// <summary>
		/// Удалить все записи кроме избранных.
		/// </summary>
		public void ClearExceptFavorites()
		{
			Prompts.ClearExceptFavorites();
			GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Cleared, prompt: null);
		}

		private DataRow GetPrompt(int id)
		{
			return Prompts.Get(id: id);
		}

		public bool ExistsPrompt(int id)
		{
			return Prompts.Exists(id: id);
		}

		public bool GetFavorite(int id)
		{
			if (GetPrompt(id: id) is PromptsRow row)
			{
				return row.is_favorite;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public void SetFavorite(int id, bool isFavorite)
		{
			if (GetPrompt(id: id) is PromptsRow row)
			{
				row.BeginEdit();
				row.is_favorite = isFavorite;
				row.EndEdit();
				GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Modified, prompt: CreateDtoPrompt(row));
			}
		}

		/// <summary>
		/// Наличие избранных записей.
		/// </summary>
		/// <returns></returns>
		public bool IsFavoriteAny() => Prompts.IsFavoriteAny();

		public DtoModel GetDtoModel(int id)
		{
			if (GetPrompt(id: id) is PromptsRow row)
			{
				return DtoModel.Create(row.ModelsRow);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Задать модель для промпта.
		/// </summary>
		/// <param name="id">Идентификатор промпта.</param>
		/// <param name="modelName">Имя модели.</param>
		/// <param name="endpoint">Ендпоинт профиля подключения модели.</param>
		public void SetModel(int id, string modelName, string endpoint)
		{
			if (GetPrompt(id: id) is PromptsRow promptRow)
			{
				if (Profiles.Exists(endpoint: endpoint))
				{
					RemoveModel(promptRow.ModelsRow);

					ProfilesRow profile = Profiles.GetOrNewRow(endpoint: endpoint);
					int modelId = Models.GetOrNewRow(modelName: modelName, modelDescription: string.Empty, profilesRow: profile).id;

					promptRow.BeginEdit();
					promptRow.model_id = modelId;
					promptRow.EndEdit();

					GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Modified, prompt: CreateDtoPrompt(promptRow));
				}
			}
		}

		public void SetOptions(int id, IChatOptions options)
		{
			if (GetPrompt(id: id) is PromptsRow promptRow)
			{
				RemovePromptOptions(promptRow.PromptOptionsRow);

				PromptOptionsRow optionsRow = PromptOptions.GetOrNewRow(options: options);

				promptRow.BeginEdit();
				promptRow.prompt_options_id = optionsRow.id;
				promptRow.EndEdit();

				GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Modified, prompt: CreateDtoPrompt(promptRow));
			}
		}

		/// <summary>
		/// Задать системный промпт.
		/// </summary>
		/// <param name="id">Идентификатор основного промпта</param>
		/// <param name="message">Системный промпт</param>
		public void SetSystemMessage(int id, string message)
		{
			if (GetPrompt(id: id) is PromptsRow promptsRow)
			{
				RemoveSystemMessages(id);

				SystemMessagesRow messageRow = SystemMessages.GetOrNewRow(message: message);
				SystemMessagesReference.GetOrNewRow(promptsRow: promptsRow, systemMessagesRow: messageRow);

				GlobalsEventsBus.DoPromptsHistoryCollectionChanged(this, state: EventsBus.StateEnums.PromptsHistoryState.Modified, prompt: CreateDtoPrompt(promptsRow));
			}
		}

		private DtoPrompt CreateDtoPrompt(PromptsRow row)
		{
			SystemMessagesRow[] systemMessageRows = SystemMessagesReference.GetRowsByPrompt(promptsRow: row);
			UserMessagesRow[] userMessageRows = UserMessagesReference.GetRowsByPrompt(promptsRow: row);
			return DtoPrompt.Create(row: row, systemMessageRows: systemMessageRows, userMessageRows: userMessageRows);
		}

		public bool ExistsPrompt(string[] userMessages)
		{
			return UserMessages.Exists(userMessages);
		}

		public DtoPrompt GetPrompt(string[] userMessages)
		{
			IEnumerable<UserMessagesRow> userMessagesRows = userMessages
				.Select(UserMessages.Get);

			IEnumerable<int> userMessagesIds = userMessagesRows
				.Select(row => row.id);

			return CreateDtoPrompt(UserMessagesReference
				.Where(row => userMessagesIds.Contains(row.message_id))
				.Select(row => row.PromptsRow).FirstOrDefault());
		}

		partial class PromptsDataTable
		{
			/// <summary>
			/// Получить или создать новую запись о промпте (не рекомендуется)
			/// </summary>
			/// <param name="date"></param>
			/// <param name="description"></param>
			/// <param name="modelRow"></param>
			/// <param name="optionsRow"></param>
			/// <param name="modeRow"></param>
			/// <param name="isFavorite"></param>
			/// <returns></returns>
			public PromptsRow GetOrNewRow(DateTime date, string description, ModelsRow modelRow, PromptOptionsRow optionsRow, PromptModeRow modeRow, bool isFavorite)
			{
				return Exists(
					model: modelRow.id,
					options: optionsRow.id,
					mode: modelRow.id)
				? GetAndSet(
					date: date,
					description: description,
					model: modelRow.id,
					options: optionsRow.id,
					mode: modelRow.id,
					isFavorite: isFavorite)
				: AddPromptsRow(
					date: date,
					description: description,
					parentModelsRowBymodels_prompts: modelRow,
					parentPromptOptionsRowByprompt_options_prompts: optionsRow,
					parentPromptModeRowByprompt_mode_prompts: modeRow,
					is_favorite: isFavorite);
			}

			public PromptsRow Get(int id)
			{
				return (from PromptsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.id == id
						select row).FirstOrDefault();
			}

			public bool Exists(int id)
			{
				return (from PromptsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.id == id
						select row).Any();
			}

			public void Remove(int id)
			{
				DataRow row = Get(id: id);
				Rows.Remove(row);
			}

			public PromptsRow Get(
				int model,
				int options,
				int mode)
			{
				return (from PromptsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.model_id == model &&
						row.prompt_options_id == options &&
						row.mode_id == mode
						select row).FirstOrDefault();
			}

			public bool Exists(
				int model,
				int options,
				int mode)
			{
				return (from PromptsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.model_id == model &&
						row.prompt_options_id == options &&
						row.mode_id == mode
						select row).Any();
			}

			private PromptsRow GetAndSet(
				DateTime date,
				string description,
				int model,
				int options,
				int mode,
				bool isFavorite)
			{
				PromptsRow promptsRow = (from PromptsRow row in Rows
										 where row.RowState != DataRowState.Deleted &&
										 row.model_id == model &&
										 row.prompt_options_id == options &&
										 row.mode_id == mode
										 select row).FirstOrDefault();

				promptsRow.BeginEdit();
				promptsRow.date = date;
				promptsRow.description = description;
				promptsRow.is_favorite = isFavorite;
				promptsRow.EndEdit();

				return promptsRow;
			}

			/// <summary>
			/// Наличие избранных записей.
			/// </summary>
			/// <returns></returns>
			public bool IsFavoriteAny()
			{
				return (from PromptsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.is_favorite
						select row).Any();
			}

			/// <summary>
			/// Удалить все записи кроме избранных.
			/// </summary>
			public void ClearExceptFavorites()
			{
				List<int> notFavorites = [.. from PromptsRow row in Rows
											 where row.RowState != DataRowState.Deleted &&
											 !row.is_favorite
											 select row.id];
				foreach (int index in notFavorites)
				{
					Remove(index);
				}
			}
		}

		partial class ProfilesDataTable
		{
			public ProfilesRow GetOrNewRow(string endpoint)
			{
				return Exists(endpoint: endpoint)
				? Get(endpoint: endpoint)
				: AddProfilesRow(endpoint: endpoint);
			}

			public ProfilesRow Get(string endpoint)
			{
				return (from ProfilesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.endpoint == endpoint
						select row).FirstOrDefault();
			}

			public bool Exists(string endpoint)
			{
				return (from ProfilesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.endpoint == endpoint
						select row).Any();
			}
		}

		partial class ModelsDataTable
		{
			public ModelsRow GetOrNewRow(string modelName, string modelDescription, ProfilesRow profilesRow)
			{
				return Exists(
						name: modelName,
						description: modelDescription,
						profileId: profilesRow.id)
					? Get(
						name: modelName,
						profileId: profilesRow.id)
					: AddModelsRow(
						name: modelName,
						description: modelDescription,
						parentProfilesRowByprofiles_models: profilesRow);
			}

			public ModelsRow GetOrNewRow(DtoPrompt prompt, ProfilesRow profilesRow)
			{
				if (Exists(prompt: prompt, profileId: profilesRow.id))
				{
					return Get(prompt: prompt, profilesRow: profilesRow);
				}
				else
				{
					return AddModelsRow(
						name: prompt.ModelName,
						description: prompt.ModelDescription,
						parentProfilesRowByprofiles_models: profilesRow);
				}
			}

			public ModelsRow Get(DtoPrompt prompt, ProfilesRow profilesRow) => Get(name: prompt.ModelName, profileId: profilesRow.id);

			public ModelsRow Get(DtoModel model, ProfilesRow profilesRow)
			{
				return Get(name: model.Name, profileId: profilesRow.id);
			}

			public ModelsRow Get(string name, int profileId)
			{
				return (from ModelsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.name == name &&
						row.profile_id == profileId
						select row).FirstOrDefault();
			}

			public bool Exists(string name)
			{
				return (from ModelsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.name == name
						select row).Any();
			}

			public bool Exists(DtoPrompt prompt, int profileId) => Exists(name: prompt.ModelName, description: prompt.ModelDescription, profileId: profileId);

			public bool Exists(DtoModel model, int profileId) => Exists(name: model.Name, description: model.Description, profileId: profileId);

			public bool Exists(string name, string description, int profileId)
			{
				return (from ModelsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.name == name &&
						row.description == description &&
						row.profile_id == profileId
						select row).Any();
			}
		}

		partial class PromptOptionsDataTable
		{
			public PromptOptionsRow GetOrNewRow(
				IChatOptions options)
			{
				return GetOrNewRow(
					maxOutputTokenCount: options.MaxOutputTokenCount.GetValueOrDefault(),
					frequencyPenalty: options.FrequencyPenalty.GetValueOrDefault(),
					presencePenalty: options.PresencePenalty.GetValueOrDefault(),
					temperature: options.Temperature.GetValueOrDefault(),
					topP: options.TopP.GetValueOrDefault());
			}

			public PromptOptionsRow GetOrNewRow(
				int maxOutputTokenCount,
				float frequencyPenalty,
				float presencePenalty,
				float temperature,
				float topP)
			{
				return Exists(
					maxOutputTokenCount: maxOutputTokenCount,
					frequencyPenalty: frequencyPenalty,
					presencePenalty: presencePenalty,
					temperature: temperature,
					topP: topP)
				? Get(
					maxOutputTokenCount: maxOutputTokenCount,
					frequencyPenalty: frequencyPenalty,
					presencePenalty: presencePenalty,
					temperature: temperature,
					topP: topP)
				: AddPromptOptionsRow(
					max_output_token_count: maxOutputTokenCount,
					frequency_penalty: frequencyPenalty,
					presence_penalty: presencePenalty,
					temperature: temperature,
					top_p: topP);
			}

			public PromptOptionsRow GetOrNewRow(DtoPrompt prompt)
			{
				if (Exists(prompt: prompt))
				{
					return Get(prompt: prompt);
				}
				else
				{
					return AddPromptOptionsRow(
						max_output_token_count: prompt.MaxOutputTokenCount,
						frequency_penalty: prompt.FrequencyPenalty,
						presence_penalty: prompt.PresencePenalty,
						temperature: prompt.Temperature,
						top_p: prompt.TopP);
				}
			}

			public PromptOptionsRow Get(DtoPrompt prompt) => Get(
				maxOutputTokenCount: prompt.MaxOutputTokenCount,
				frequencyPenalty: prompt.FrequencyPenalty,
				presencePenalty: prompt.PresencePenalty,
				temperature: prompt.Temperature,
				topP: prompt.TopP);

			public PromptOptionsRow Get(
				int maxOutputTokenCount,
				float frequencyPenalty,
				float presencePenalty,
				float temperature,
				float topP)
			{
				return (from PromptOptionsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.max_output_token_count == maxOutputTokenCount &&
						row.frequency_penalty == frequencyPenalty &&
						row.presence_penalty == presencePenalty &&
						row.temperature == temperature &&
						row.top_p == topP
						select row).FirstOrDefault();
			}

			public bool Exists(DtoPrompt prompt) => Exists(
				maxOutputTokenCount: prompt.MaxOutputTokenCount,
				frequencyPenalty: prompt.FrequencyPenalty,
				presencePenalty: prompt.PresencePenalty,
				temperature: prompt.Temperature,
				topP: prompt.TopP);

			public bool Exists(
				int maxOutputTokenCount,
				float frequencyPenalty,
				float presencePenalty,
				float temperature,
				float topP)
			{
				return (from PromptOptionsRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.max_output_token_count == maxOutputTokenCount &&
						row.frequency_penalty == frequencyPenalty &&
						row.presence_penalty == presencePenalty &&
						row.temperature == temperature &&
						row.top_p == topP
						select row).Any();
			}
		}

		partial class PromptModeDataTable
		{
			public PromptModeRow GetOrNewRow(Document.ChatMessageModeEnum mode)
			{
				return GetOrNewRow(
					insertHereMessage: mode.HasFlag(Document.ChatMessageModeEnum.InsertHere),
					replaceSelectionMessage: mode.HasFlag(Document.ChatMessageModeEnum.ReplaceSelection),
					insertNextMessage: mode.HasFlag(Document.ChatMessageModeEnum.InsertPrevious),
					insertPreviousMessage: mode.HasFlag(Document.ChatMessageModeEnum.InsertPrevious),
					insertBetweenMessage: mode.HasFlag(Document.ChatMessageModeEnum.InsertBetween));
			}

			public PromptModeRow GetOrNewRow(
				bool insertHereMessage,
				bool replaceSelectionMessage,
				bool insertNextMessage,
				bool insertPreviousMessage,
				bool insertBetweenMessage)
			{
				return Exists(
						insertHereMessage: insertHereMessage,
						replaceSelectionMessage: replaceSelectionMessage,
						insertNextMessage: insertNextMessage,
						insertPreviousMessage: insertPreviousMessage,
						insertBetweenMessage: insertBetweenMessage)
					? Get(
						insertHereMessage: insertHereMessage,
						replaceSelectionMessage: replaceSelectionMessage,
						insertNextMessage: insertNextMessage,
						insertPreviousMessage: insertPreviousMessage,
						insertBetweenMessage: insertBetweenMessage)
					: AddPromptModeRow(
						insert_here_message: insertHereMessage,
						replace_selection_message: replaceSelectionMessage,
						insert_next_message: insertNextMessage,
						insert_previous_message: insertPreviousMessage,
						insert_between_message: insertBetweenMessage);
			}

			public PromptModeRow GetOrNewRow(DtoPrompt prompt)
			{
				if (Exists(prompt: prompt))
				{
					return Get(prompt: prompt);
				}
				else
				{
					return AddPromptModeRow(
						insert_here_message: prompt.InsertHereMessage,
						replace_selection_message: prompt.ReplaceSelectionMessage,
						insert_next_message: prompt.InsertNextMessage,
						insert_previous_message: prompt.InsertPreviousMessage,
						insert_between_message: prompt.InsertBetweenMessage);
				}
			}

			public PromptModeRow Get(DtoPrompt prompt) => Get(
				insertHereMessage: prompt.InsertHereMessage,
				replaceSelectionMessage: prompt.ReplaceSelectionMessage,
				insertNextMessage: prompt.InsertNextMessage,
				insertPreviousMessage: prompt.InsertPreviousMessage,
				insertBetweenMessage: prompt.InsertBetweenMessage);

			public PromptModeRow Get(
				bool insertHereMessage,
				bool replaceSelectionMessage,
				bool insertNextMessage,
				bool insertPreviousMessage,
				bool insertBetweenMessage)
			{
				return (from PromptModeRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.insert_here_message == insertHereMessage &&
						row.replace_selection_message == replaceSelectionMessage &&
						row.insert_next_message == insertNextMessage &&
						row.insert_previous_message == insertPreviousMessage &&
						row.insert_between_message == insertBetweenMessage
						select row).FirstOrDefault();
			}

			public bool Exists(DtoPrompt prompt) => Exists(
				insertHereMessage: prompt.InsertHereMessage,
				replaceSelectionMessage: prompt.ReplaceSelectionMessage,
				insertNextMessage: prompt.InsertNextMessage,
				insertPreviousMessage: prompt.InsertPreviousMessage,
				insertBetweenMessage: prompt.InsertBetweenMessage);

			public bool Exists(
				bool insertHereMessage,
				bool replaceSelectionMessage,
				bool insertNextMessage,
				bool insertPreviousMessage,
				bool insertBetweenMessage)
			{
				return (from PromptModeRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.insert_here_message == insertHereMessage &&
						row.replace_selection_message == replaceSelectionMessage &&
						row.insert_next_message == insertNextMessage &&
						row.insert_previous_message == insertPreviousMessage &&
						row.insert_between_message == insertBetweenMessage
						select row).Any();
			}
		}

		partial class SystemMessagesDataTable
		{
			public SystemMessagesRow GetOrNewRow(string message)
			{
				return Exists(message: message)
					? Get(message: message)
					: AddSystemMessagesRow(message: message);
			}

			public SystemMessagesRow Get(string message)
			{
				return (from SystemMessagesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.message.Trim().Equals(message.Trim(), StringComparison.CurrentCulture)
						select row).FirstOrDefault();
			}

			public IEnumerable<SystemMessagesRow> GetRows(IEnumerable<string> messages)
			{
				return messages.Select(Get);
			}

			public bool Exists(string message)
			{
				return (from SystemMessagesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.message.Trim().Equals(message.Trim(), StringComparison.CurrentCulture)
						select row).Any();
			}

			public bool Exists(IEnumerable<string> messages)
			{
				return !messages.Any(m => !Exists(m));
			}			
		}

		partial class UserMessagesDataTable
		{
			public UserMessagesRow GetOrNewRow(string message)
			{
				return Exists(message: message)
					? Get(message: message)
					: AddUserMessagesRow(message: message);
			}

			public UserMessagesRow Get(string message)
			{
				return (from UserMessagesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.message.Trim().Equals(message.Trim(), StringComparison.CurrentCulture)
						select row).FirstOrDefault();
			}

			public IEnumerable<UserMessagesRow> GetRows(IEnumerable<string> messages)
			{
				return messages.Select(Get);
			}

			public bool Exists(string message)
			{
				return (from UserMessagesRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.message.Trim().Equals(message.Trim(), StringComparison.CurrentCultureIgnoreCase)
						select row).Any();
			}

			public bool Exists(IEnumerable<string> messages)
			{
				return !messages.Any(m => !Exists(m));
			}
		}

		partial class SystemMessagesReferenceDataTable
		{
			public SystemMessagesReferenceRow GetOrNewRow(PromptsRow promptsRow, SystemMessagesRow systemMessagesRow)
			{
				return Exists(
						promptsRow: promptsRow,
						systemMessagesRow: systemMessagesRow)
					? Get(
						promptsRow: promptsRow,
						systemMessagesRow: systemMessagesRow)
					: AddSystemMessagesReferenceRow(
						parentPromptsRowByprompts_system_messages_reference: promptsRow,
						parentSystemMessagesRowBysystem_messages_system_messages_reference: systemMessagesRow);
			}

			public SystemMessagesReferenceRow Get(PromptsRow promptsRow, SystemMessagesRow systemMessagesRow)
			{
				return (from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow &&
						row.SystemMessagesRow == systemMessagesRow
						select row).FirstOrDefault();
			}

			public IEnumerable<SystemMessagesRow> Get(PromptsRow promptsRow)
			{
				return from SystemMessagesReferenceRow row in Rows
					   where row.RowState != DataRowState.Deleted &&
					   row.PromptsRow == promptsRow
					   select row.SystemMessagesRow;
			}

			public bool Exists(PromptsRow promptsRow)
			{
				return (from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow
						select row).Any();
			}

			public bool Exists(PromptsRow promptsRow, SystemMessagesRow systemMessagesRow)
			{
				return (from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow &&
						row.SystemMessagesRow == systemMessagesRow
						select row).Any();
			}

			public bool Exists(string messages)
			{
				return (from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.SystemMessagesRow.message == messages
						select row).Any();
			}

			public SystemMessagesRow[] GetRowsByPrompt(PromptsRow promptsRow)
			{
				return [.. from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow
						select row.SystemMessagesRow];
			}

			public PromptsRow[] GetRowsByMessage(string message)
			{
				return [.. from SystemMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.SystemMessagesRow.message == message
						select row.PromptsRow];
			}			
		}

		partial class UserMessagesReferenceDataTable
		{
			public UserMessagesReferenceRow GetOrNewRow(PromptsRow promptsRow, UserMessagesRow userMessagesRow)
			{
				return Exists(
						promptsRow: promptsRow,
						userMessagesRow: userMessagesRow)
					? Get(
						promptsRow: promptsRow,
						userMessagesRow: userMessagesRow)
					: AddUserMessagesReferenceRow(
						parentPromptsRowByprompts_user_messages_reference: promptsRow,
						parentUserMessagesRowByuser_messages_user_messages_reference: userMessagesRow);
			}

			public UserMessagesReferenceRow Get(PromptsRow promptsRow, UserMessagesRow userMessagesRow)
			{
				return (from UserMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow &&
						row.UserMessagesRow == userMessagesRow
						select row).FirstOrDefault();
			}

			public bool Exists(PromptsRow promptsRow, UserMessagesRow userMessagesRow)
			{
				return (from UserMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow &&
						row.UserMessagesRow == userMessagesRow
						select row).Any();
			}

			public bool Exists(UserMessagesRow userMessagesRow)
			{
				return (from UserMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.UserMessagesRow == userMessagesRow
						select row).Any();
			}

			public UserMessagesRow[] GetRowsByPrompt(PromptsRow promptsRow)
			{
				return [.. from UserMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						row.PromptsRow == promptsRow
						select row.UserMessagesRow];
			}

			public UserMessagesRow[] GetRowsByMessages(string[] messages)
			{
				return [.. from UserMessagesReferenceRow row in Rows
						where row.RowState != DataRowState.Deleted &&
						messages.Contains(row.UserMessagesRow.message)
						select row.UserMessagesRow];
			}			
		}
	}
}

namespace WordHiddenPowers.Repository
{
	partial class PromptHistoryDataSet
	{
	}
}
