using System;
using System.Linq;
using static WordHiddenPowers.Repository.PromptsHistory;

namespace WordHiddenPowers.Repository.History
{
	public class DtoPrompt
	{
		protected string description;
		protected DateTime date;
		protected string modelDescription;
		protected bool isFavorite;

		public static DtoPrompt Create(PromptsRow row, SystemMessagesRow[] systemMessageRows, UserMessagesRow[] userMessageRows)
		{
			return new DtoPrompt(
				id: row.id,
				description: row.description,
				date: row.date,
				modelName: row.ModelsRow.name,
				modelDescription: row.ModelsRow.description,
				profileEndpoint: row.ModelsRow.ProfilesRow.endpoint,
				systemMessages: [.. systemMessageRows.Select(r => r.message)],
				userMessages: [.. userMessageRows.Select(r => r.message)],
				maxOutputTokenCount: row.PromptOptionsRow.max_output_token_count,
				frequencyPenalty: row.PromptOptionsRow.frequency_penalty,
				presencePenalty: row.PromptOptionsRow.presence_penalty,
				temperature: row.PromptOptionsRow.temperature,
				topP: row.PromptOptionsRow.top_p,
				insertHereMessage: row.PromptModeRow.insert_here_message,
				replaceSelectionMessage: row.PromptModeRow.replace_selection_message,
				insertNextMessage: row.PromptModeRow.insert_next_message,
				insertPreviousMessage: row.PromptModeRow.insert_previous_message,
				insertBetweenMessage: row.PromptModeRow.insert_between_message,
				isFavorite: row.is_favorite);
		}
		
		protected DtoPrompt(
			int id,
			string description,
			DateTime date,
			string modelName,
			string modelDescription,
			string profileEndpoint,
			string[] systemMessages,
			string[] userMessages,
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
			bool isFavorite
			)
		{
			Id = id;
			this.description = description;
			this.date = date;
			ModelName = modelName;
			this.modelDescription = modelDescription;
			ProfileEndpoint = profileEndpoint;
			SystemMessages = systemMessages;
			UserMessages = userMessages;
			MaxOutputTokenCount = maxOutputTokenCount;
			FrequencyPenalty = frequencyPenalty;
			PresencePenalty = presencePenalty;
			Temperature = temperature;
			TopP = topP;
			InsertHereMessage = insertHereMessage;
			ReplaceSelectionMessage = replaceSelectionMessage;
			InsertNextMessage = insertNextMessage;
			InsertPreviousMessage = insertPreviousMessage;
			InsertBetweenMessage = insertBetweenMessage;
			this.isFavorite = isFavorite;
		}

		/// <summary>
		/// Индекс (ключ) промпта в базе данных.
		/// </summary>
		public int Id { get; }

		public virtual string Description => description; 

		public virtual DateTime Date => date;

		public string ModelName { get; }

		public string ProfileEndpoint { get; }

		public virtual string ModelDescription => modelDescription; 

		public string[] SystemMessages { get; }

		public string[] UserMessages { get; }

		public int MaxOutputTokenCount { get; }

		public float FrequencyPenalty { get; }

		public float PresencePenalty { get; }

		public float Temperature { get; }

		public float TopP { get; }

		/// <summary>
		/// Вставка в текущем тексте.
		/// </summary>
		public bool InsertHereMessage { get; }

		/// <summary>
		/// Замена текущего текста.
		/// </summary>
		public bool ReplaceSelectionMessage { get; }

		/// <summary>
		/// Вставка в следующем абзаце.
		/// </summary>
		public bool InsertNextMessage { get; }

		/// <summary>
		/// Вставка в предыдущем абзаце.
		/// </summary>
		public bool InsertPreviousMessage { get; }

		/// <summary>
		/// Вставка между абзацами
		/// </summary>
		public bool InsertBetweenMessage { get; }

		public virtual bool IsFavorite => isFavorite;

		public override int GetHashCode()
		{
			HashCode hash = new();
			hash.Add(ModelName);
			hash.Add(ProfileEndpoint);
			hash.Add(SystemMessages);
			hash.Add(UserMessages);
			hash.Add(MaxOutputTokenCount);
			hash.Add(FrequencyPenalty);
			hash.Add(PresencePenalty);
			hash.Add(Temperature);
			hash.Add(TopP);
			hash.Add(InsertHereMessage);
			hash.Add(ReplaceSelectionMessage);
			hash.Add(InsertNextMessage);
			hash.Add(InsertPreviousMessage);
			hash.Add(InsertBetweenMessage);
			return hash.ToHashCode();
		}
	}
}
