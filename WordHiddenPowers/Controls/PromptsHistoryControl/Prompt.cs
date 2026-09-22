using LLMConnectorLibrary;
using System;
using System.Collections.Generic;
using WordHiddenPowers.Repository.History;
using static WordHiddenPowers.Documents.Document;
using MessageMode = WordHiddenPowers.Documents.Document.ChatMessageModeEnum;


namespace WordHiddenPowers.Controls.PromptsHistoryControl
{
	public class Prompt : DtoPrompt
	{
		public Prompt(DtoPrompt prompt) : this(
				id: prompt.Id,
				description: prompt.Description,
				date: prompt.Date,
				modelName: prompt.ModelName,
				modelDescription: prompt.ModelDescription,
				profileEndpoint: prompt.ProfileEndpoint,
				systemMessages: prompt.SystemMessages,
				userMessages: prompt.UserMessages,
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
				isFavorite: prompt.IsFavorite)
		{ }

		public Prompt(DtoPrompt prompt, int newId) : this(
				id: newId,
				description: prompt.Description,
				date: prompt.Date,
				modelName: prompt.ModelName,
				modelDescription: prompt.ModelDescription,
				profileEndpoint: prompt.ProfileEndpoint,
				systemMessages: prompt.SystemMessages,
				userMessages: prompt.UserMessages,
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
				isFavorite: prompt.IsFavorite)
		{ }

		public Prompt(
			int newId,
			string description,
			string modelName,
			string modelDescription,
			string profileEndpoint,
			string[] systemMessages,
			string[] userMessages,
			IChatOptions chatOptions,
			MessageMode messageMode,
			bool isFavorite
			) : this(
				id: newId,
				description: description,
				date: DateTime.Now,
				modelName: modelName,
				modelDescription: modelDescription,
				profileEndpoint: profileEndpoint,
				systemMessages: systemMessages,
				userMessages: userMessages,
				maxOutputTokenCount: chatOptions.MaxOutputTokenCount.GetValueOrDefault(),
				frequencyPenalty: chatOptions.FrequencyPenalty.GetValueOrDefault(),
				presencePenalty: chatOptions.PresencePenalty.GetValueOrDefault(),
				temperature: chatOptions.Temperature.GetValueOrDefault(),
				topP: chatOptions.TopP.GetValueOrDefault(),
				insertHereMessage: messageMode.HasFlag(MessageMode.InsertHere),
				replaceSelectionMessage: messageMode.HasFlag(MessageMode.ReplaceSelection),
				insertNextMessage: messageMode.HasFlag(MessageMode.InsertNext),
				insertPreviousMessage: messageMode.HasFlag(MessageMode.InsertPrevious),
				insertBetweenMessage: messageMode.HasFlag(MessageMode.InsertBetween),
				isFavorite: isFavorite)
		{
		}

		public Prompt(
			int newId,
			string description,
			string modelName,
			string modelDescription,
			string profileEndpoint,
			string[] systemMessages,
			string[] userMessages,
			IChatOptions chatOptions,
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertBetweenMessage,

			bool isFavorite
			) : this(
				id: newId,
				description: description,
				date: DateTime.Now,
				modelName: modelName,
				modelDescription: modelDescription,
				profileEndpoint: profileEndpoint,
				systemMessages: systemMessages,
				userMessages: userMessages,
				maxOutputTokenCount: chatOptions.MaxOutputTokenCount.GetValueOrDefault(),
				frequencyPenalty: chatOptions.FrequencyPenalty.GetValueOrDefault(),
				presencePenalty: chatOptions.PresencePenalty.GetValueOrDefault(),
				temperature: chatOptions.Temperature.GetValueOrDefault(),
				topP: chatOptions.TopP.GetValueOrDefault(),
				insertHereMessage: insertHereMessage,
				replaceSelectionMessage: replaceSelectionMessage,
				insertNextMessage: insertNextMessage,
				insertPreviousMessage: insertPreviousMessage,
				insertBetweenMessage: insertBetweenMessage,
				isFavorite: isFavorite)
		{
		}

		private Prompt(
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
			): base(
				id:  id,
				description: description,
				date: date,
				modelName: modelName,
				modelDescription: modelDescription,
				profileEndpoint: profileEndpoint,
				systemMessages: systemMessages,
				userMessages: userMessages,
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
				isFavorite: isFavorite)
		{
		}

		public override bool Equals(object obj)
		{
			return obj is Prompt prompt &&
				   ModelName == prompt.ModelName &&
				   ProfileEndpoint == prompt.ProfileEndpoint &&
				   EqualityComparer<string[]>.Default.Equals(SystemMessages, prompt.SystemMessages) &&
				   EqualityComparer<string[]>.Default.Equals(UserMessages, prompt.UserMessages) &&
				   MaxOutputTokenCount == prompt.MaxOutputTokenCount &&
				   FrequencyPenalty == prompt.FrequencyPenalty &&
				   PresencePenalty == prompt.PresencePenalty &&
				   Temperature == prompt.Temperature &&
				   TopP == prompt.TopP &&
				   InsertHereMessage == prompt.InsertHereMessage &&
				   ReplaceSelectionMessage == prompt.ReplaceSelectionMessage &&
				   InsertNextMessage == prompt.InsertNextMessage &&
				   InsertPreviousMessage == prompt.InsertPreviousMessage &&
				   InsertBetweenMessage == prompt.InsertBetweenMessage;
		}

		public bool Equals(DtoPrompt prompt)
		{
			return ModelName == prompt.ModelName &&
				   ProfileEndpoint == prompt.ProfileEndpoint &&
				   EqualityComparer<string[]>.Default.Equals(SystemMessages, prompt.SystemMessages) &&
				   EqualityComparer<string[]>.Default.Equals(UserMessages, prompt.UserMessages) &&
				   MaxOutputTokenCount == prompt.MaxOutputTokenCount &&
				   FrequencyPenalty == prompt.FrequencyPenalty &&
				   PresencePenalty == prompt.PresencePenalty &&
				   Temperature == prompt.Temperature &&
				   TopP == prompt.TopP &&
				   InsertHereMessage == prompt.InsertHereMessage &&
				   ReplaceSelectionMessage == prompt.ReplaceSelectionMessage &&
				   InsertNextMessage == prompt.InsertNextMessage &&
				   InsertPreviousMessage == prompt.InsertPreviousMessage &&
				   InsertBetweenMessage == prompt.InsertBetweenMessage;
		}

		public bool Equals(string[] systemMessages, string[] userMessages, string modelName, string endpoint, ChatOptions options,  ChatMessageModeEnum mode)
		{
			return ModelName == modelName &&
				   ProfileEndpoint == endpoint &&
				   EqualityComparer<string[]>.Default.Equals(SystemMessages, systemMessages) &&
				   EqualityComparer<string[]>.Default.Equals(UserMessages, userMessages) &&
				   MaxOutputTokenCount == options.MaxOutputTokenCount &&
				   FrequencyPenalty == options.FrequencyPenalty &&
				   PresencePenalty == options.PresencePenalty &&
				   Temperature == options.Temperature &&
				   TopP == options.TopP &&
				   InsertHereMessage == mode.HasFlag(MessageMode.InsertHere) &&
				   ReplaceSelectionMessage == mode.HasFlag(MessageMode.ReplaceSelection) &&
				   InsertNextMessage == mode.HasFlag(MessageMode.InsertNext) &&
				   InsertPreviousMessage == mode.HasFlag(MessageMode.InsertPrevious) &&
				   InsertBetweenMessage == mode.HasFlag(MessageMode.InsertBetween);
		}

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

		public IChatOptions ChatOptions => LLMConnectorLibrary.ChatOptions.Create(
			maxOutputTokenCount: MaxOutputTokenCount,
			frequencyPenalty: FrequencyPenalty,
			presencePenalty: PresencePenalty,
			temperature: Temperature,
			topP: TopP);

		public new string Description
		{
			get => base.Description;
			set => description = value;
		}

		public new DateTime Date 
		{
			get => base.Date;
			set => date = value;
		}

		public new string ModelDescription
		{
			get => base.ModelDescription;
			set => modelDescription = value;
		}

		public new bool IsFavorite
		{
			get => base.IsFavorite;
			set => isFavorite = value;
		}		
	
		public Prompt Copy()
		{
			return new Prompt(this);
		}

		public Prompt Copy(int newId)
		{
			return new Prompt(this, newId: newId);
		}
	}
}
