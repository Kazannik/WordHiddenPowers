using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Controls.PromptsHistoryControl;
using WordHiddenPowers.Controls.SendMessagesControl;
using WordHiddenPowers.Documents;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.EventsBus.EventArgs;
using WordHiddenPowers.EventsBus.StateEnums;
using MessageMode = WordHiddenPowers.Documents.Document.ChatMessageModeEnum;
using Word = Microsoft.Office.Interop.Word;


namespace WordHiddenPowers.Panes.Components
{
	public partial class LLMControl : UserControl
	{
		private const string CLEAR_USER_MESSAGE = "Очистить окно пользовательского промпта";
		private const string DELETE_HISTORY_PROMPT = "Удалить промпт из истории";

		private readonly PromptCollection promptHistoryCollection = [];
		private readonly MessageCollection chatMessageCollection = [];
		
		private Word.Range replaceRange = null;
		private int sendingPromptRowId = -1;
		
		private MessageMode oldMessageMode = MessageMode.Nothing;

		/// <summary>
		/// Текущий документ.
		/// </summary>
		public Document Document
		{
			get => sendMessageButtonsBar.Document;
			set => sendMessageButtonsBar.Document = value;
		}
		
		/// <summary>
		/// Текущий системный промпт.
		/// </summary>
		public string SystemMessage => promptsHistoryBox.SystemMessage;

		/// <summary>
		/// Пользовательский промпт.
		/// </summary>
		public string UserMessage
		{
			get => userMessageTextBox.Text;
			private set => userMessageTextBox.Text = value;
		}
		
		/// <summary>
		/// Текущая большая языковая модель. 
		/// </summary>
		public IModel SelectedModel => promptsHistoryBox.SelectedModel;

		/// <summary>
		/// Текущие настройки промпта.
		/// </summary>
		public IChatOptions ChatOptions => promptsHistoryBox.ChatOptions;

		/// <summary>
		/// Тип вставки результата в документ.
		/// </summary>
		public MessageMode ChatMessageMode
		{
			get => sendMessageButtonsBar.ChatMessageMode;
			set => sendMessageButtonsBar.ChatMessageMode = value;
		}
				
		public LLMControl()
		{
			InitializeComponent();

			GlobalsEventsBus.ModelsCollectionChanged += new EventHandler<ProcessStateEventArgs>(GlobalsEventsBus_ModelsCollectionChanged);
			GlobalsEventsBus.PromptsHistoryCollectionChanged += new EventHandler<PromptsHistoryEventArgs>(GlobalsEventsBus_PromptsHistoryCollectionChanged);
			GlobalsEventsBus.DocumentChatMessageModeChanged += new EventHandler<DocumentChatMessageModeEventArgs>(GlobalsEventsBus_DocumentChatMessageModeChanged);

			GlobalsEventsBus.SendChatMessage += new EventHandler<WordDocumentSendMessageEventArgs>(GlobalsEventsBus_SendChatMessage);
			GlobalsEventsBus.ReceiveChatMessage += new EventHandler<WordDocumentReceiveMessageEventArgs>(GlobalsEventsBus_ReceiveChatMessage);

			chatMessageCollection.StatusChanged += new EventHandler(ChatMessageCollection_StatusChanged);			
			promptHistoryCollection.StatusChanged += new EventHandler(PromptCollection_StatusChanged);
			promptsHistoryBox.SelectedModelChanged += new EventHandler<EventArgs>(PromptsHistoryBox_SelectedModelChanged);


			if (!DesignMode)
			{
				
				promptsHistoryBox.InitializeModelsSource(Globals.ThisAddIn.Models);

				promptsHistoryBox.ChatOptions = Globals.ThisAddIn.GlobalsSetting.DefaultChatOptions;
				promptsHistoryBox.SystemMessage = Globals.ThisAddIn.GlobalsSetting.DefaultSystemMessage;

				promptsHistoryBox.SelectModel(
					model: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName,
					profileEndpoint: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint);
				
				ReadHistoryAndAddBuffer(MessageMode.All);				
			}			
		}

		/// <summary>
		/// Прочитать историю и добавить буфер в конец коллекции.
		/// </summary>
		/// <param name="chatMessageMode">Тип вставки результата.</param>
		private void ReadHistoryAndAddBuffer(MessageMode chatMessageMode)
		{
			IEnumerable<Prompt> prompts = Globals.ThisAddIn.PromptsHistory.GetPrompts(messageMode: chatMessageMode)
					.Select(p => new Prompt(p));

			if (SelectedModel is not null)
				promptHistoryCollection.RefreshHistory(
				array: [.. prompts],
				description: string.Empty,
				modelName: SelectedModel.Id,
				modelDescription: string.Empty,
				profileEndpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
				systemMessages: [SystemMessage],
				userMessages: [UserMessage],
				chatOptions: ChatOptions,
				messageMode: chatMessageMode,
				isFavorite: false);
			else
				promptHistoryCollection.RefreshHistory(array: [.. prompts]);
		}

		private void PromptCollection_StatusChanged(object sender, EventArgs e)
		{
			SetPromptHistoryStatus();
		}

		private void SetPromptHistoryStatus()
		{
			UserMessage = promptHistoryCollection.SelectedPrompt.UserMessages.FirstOrDefault();
			
			promptsHistoryBox.PreviousButtonEnabled = promptHistoryCollection.IsPrevious;
			promptsHistoryBox.NextButtonEnabled = promptHistoryCollection.IsNext;
			promptsHistoryBox.FavoriteButtonEnabled = promptHistoryCollection.First is not null &&
				!promptHistoryCollection.IsBuffer;
			promptsHistoryBox.ClearButtonEnabled = Globals.ThisAddIn.PromptsHistory.Prompts.Any();
						
			promptsHistoryBox.SetChatOptions(
				maxOutputTokenCount: promptHistoryCollection.SelectedPrompt.MaxOutputTokenCount,
				frequencyPenalty: promptHistoryCollection.SelectedPrompt.FrequencyPenalty,
				presencePenalty: promptHistoryCollection.SelectedPrompt.PresencePenalty,
				temperature: promptHistoryCollection.SelectedPrompt.Temperature,
				topP: promptHistoryCollection.SelectedPrompt.TopP);
			
			promptsHistoryBox.SetSystemMessage(promptHistoryCollection.SelectedPrompt.SystemMessages.FirstOrDefault());
			promptsHistoryBox.SelectModel(promptHistoryCollection.SelectedPrompt.ModelName, promptHistoryCollection.SelectedPrompt.ProfileEndpoint);
			promptsHistoryBox.Favorite = promptHistoryCollection.SelectedPrompt.IsFavorite;
			
			if (promptHistoryCollection.IsBuffer)
			{
				promptsHistoryBox.DeleteButtonImage = Properties.Resources.DocumentClear32;
				promptsHistoryBox.DeleteButtonToolTipText = CLEAR_USER_MESSAGE;
				promptsHistoryBox.DeleteButtonEnabled = !string.IsNullOrEmpty(UserMessage);
			}
			else
			{
				promptsHistoryBox.DeleteButtonImage = Properties.Resources.DocumentDelete32;
				promptsHistoryBox.DeleteButtonToolTipText = DELETE_HISTORY_PROMPT;
				promptsHistoryBox.DeleteButtonEnabled = promptHistoryCollection.First is not null;
			}

			if (promptHistoryCollection.SelectedIndex != promptHistoryCollection.Count)
				promptsHistoryBox.StateText = string.Format("История промптов: {0} из {1}", promptHistoryCollection.SelectedIndex + 1, promptHistoryCollection.Count);
			else
				promptsHistoryBox.StateText = string.Empty;
		}

		#region Prompt Properties Changed

		private void PromptsHistoryBox_ChatOptionsChanged(object sender, EventArgs e)
		{
			if (promptHistoryCollection.IsBuffer)
			{
				Globals.ThisAddIn.GlobalsSetting.DefaultChatOptions = ChatOptions;
				promptHistoryCollection.SetOptions(ChatOptions);
			}
			else
			{
				int id = promptHistoryCollection.SelectedPrompt.Id;
				Globals.ThisAddIn.PromptsHistory.SetOptions(id: id, options: ChatOptions);
			}
		}

		private void PromptsHistoryBox_SystemMessageChanged(object sender, EventArgs e)
		{
			if (promptHistoryCollection.IsBuffer)
			{
				Globals.ThisAddIn.GlobalsSetting.DefaultSystemMessage = SystemMessage;
				promptHistoryCollection.SetSystemMessage(SystemMessage);
			}
			else
			{
				int id = promptHistoryCollection.SelectedPrompt.Id;
				Globals.ThisAddIn.PromptsHistory.SetSystemMessage(id: id, message: SystemMessage);
			}
		}

		private void PromptsHistoryBox_SelectedModelChanged(object sender, EventArgs e)
		{
			if (SelectedModel is not null)
			{
				GlobalsEventsBus.TogglingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.SelectModel);
				
				if (promptHistoryCollection.IsBuffer)
				{
					Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName = SelectedModel.Id;
					Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint = SelectedModel.Profile.ClientOptions.Endpoint.OriginalString;
				}
				else if (promptHistoryCollection.SelectedPrompt is not null)
				{
					int id = promptHistoryCollection.SelectedPrompt.Id;
					string modelName = SelectedModel.Id;
					string profileEndpoint = SelectedModel.Profile.ClientOptions.Endpoint.OriginalString;
					Globals.ThisAddIn.PromptsHistory.SetModel(id: id, modelName: modelName, endpoint: profileEndpoint);
				}

				//promptHistoryCollection.SetModel(SelectedModel);
			}
			else
			{
				GlobalsEventsBus.RemovingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.SelectModel);
			}
		}

		#endregion

		#region HistoryBox Command 

		private void PromptsHistoryBox_ClickClear(object sender, EventArgs e)
		{
			if (Globals.ThisAddIn.PromptsHistory.IsFavoriteAny()) 
			{
				DialogResult result = Utils.Dialogs.ShowMessageDialog(text: "При удалении истории сохранить избранные промпты?\nДа (Yes) - удаление всех промптов кроме избранных.\nНет (No) - полное удаление всей коллекции.\nОтмена (Cancel) - отмена операции удаления коллекции.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, defaultButton: MessageBoxDefaultButton.Button3);
				if (result == DialogResult.Yes)
					Globals.ThisAddIn.PromptsHistory.ClearExceptFavorites();
				else if (result == DialogResult.No)
					Globals.ThisAddIn.PromptsHistory.Clear();
			}
			else
			{
				DialogResult result = Utils.Dialogs.ShowMessageDialog(text: "Вы действительно ходите удалить коллекцию промптов?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton: MessageBoxDefaultButton.Button2);
				if (result == DialogResult.Yes)
					Globals.ThisAddIn.PromptsHistory.Clear();
			}
		}

		private void PromptsHistoryBox_ClickDelete(object sender, EventArgs e)
		{
			if (promptHistoryCollection.IsBuffer)
				UserMessage = string.Empty;
			else
				Globals.ThisAddIn.PromptsHistory.Remove(promptHistoryCollection.SelectedPrompt);
		}

		private void PromptsHistoryBox_ClickFavorite(object sender, EventArgs e)
		{
			if (promptHistoryCollection.Mode.HasFlag(PromptCollection.ModeType.Collection) &&
				!promptHistoryCollection.IsBuffer) 
			{
				int id = promptHistoryCollection.SelectedPrompt.Id;
				bool favorite = Globals.ThisAddIn.PromptsHistory.GetFavorite(id);
				Globals.ThisAddIn.PromptsHistory.SetFavorite(id: id, isFavorite: !favorite);
			}
		}

		private void PromptsHistoryBox_ClickPreviousPrompt(object sender, EventArgs e) => promptHistoryCollection.GoPrevious();
		
		private void PromptsHistoryBox_ClickNextPrompt(object sender, EventArgs e) => promptHistoryCollection.GoNext();
		
		#endregion

		#region SendMessageButtonsBar Command

		private void SendMessageButtonsBar_ClickInsertHereMessage(object sender, EventArgs e) => SendMessage(MessageMode.InsertHere);
		
		private void SendMessageButtonsBar_ClickReplaceSelectionMessage(object sender, EventArgs e) => SendMessage(MessageMode.ReplaceSelection);
		
		private void SendMessageButtonsBar_ClickInsertNextMessage(object sender, EventArgs e) => SendMessage(MessageMode.InsertNext);
		
		private void SendMessageButtonsBar_ClickInsertPreviousMessage(object sender, EventArgs e) => SendMessage(MessageMode.InsertPrevious);

		private void SendMessageButtonsBar_ClickInsertBetweenMessage(object sender, EventArgs e) => SendMessage(MessageMode.InsertBetween);

		private void SendMessage(MessageMode messageMode)
		{
			oldMessageMode = messageMode;

			InsertMessage(oldMessageMode);
			CreateHistoryPrompt(oldMessageMode);
		}

		private void SendMessageButtonsBar_ClickRepeatMessage(object sender, EventArgs e)
		{
			InsertMessage(MessageMode.Repeat | oldMessageMode);

			if (sendingPromptRowId >= 0)
			{
				if (Globals.ThisAddIn.PromptsHistory.Prompts.Exists(id: sendingPromptRowId))
				{
					string userMessage = Globals.ThisAddIn.PromptsHistory.GetDtoPrompt(id: sendingPromptRowId).UserMessages.FirstOrDefault();
					if (userMessage == UserMessage)
					{
						Globals.ThisAddIn.PromptsHistory.EditPrompt(
							promptId: sendingPromptRowId,
							date: DateTime.Now,
							endpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
							modelName: SelectedModel.Id,
							modelDescription: string.Empty,
							options: ChatOptions,
							promptDescription: string.Empty,
							systemMessages: [SystemMessage],
							userMessages: [UserMessage]);
					}
					else
					{
						CreateHistoryPrompt(oldMessageMode);
					}
				}
				else
				{
					CreateHistoryPrompt(oldMessageMode);
				}
			}
			else
			{
				CreateHistoryPrompt(oldMessageMode);
			}
		}

		private void InsertMessage(MessageMode messageMode)
		{
			if (promptHistoryCollection.IsBuffer)
				sendingPromptRowId = -1;
			else
				sendingPromptRowId = promptHistoryCollection.SelectedPrompt.Id;

			Globals.ThisAddIn.ActiveDocument.InsertChatMessage(
				model: SelectedModel,
				options: ChatOptions,
				mode: messageMode,
				systemMessage: SystemMessage,
				userMessages: [UserMessage]);
		}

		private void CreateHistoryPrompt(MessageMode messageMode)
		{
			if (Globals.ThisAddIn.PromptsHistory.ExistsPrompt(userMessages: [UserMessage]))
			{
				int editPromptId = Globals.ThisAddIn.PromptsHistory.GetPrompt(userMessages: [UserMessage]).Id;
				
				Globals.ThisAddIn.PromptsHistory.EditPrompt(
							promptId: editPromptId,
							date: DateTime.Now,
							endpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
							modelName: SelectedModel.Id,
							modelDescription: string.Empty,
							options: ChatOptions,
							promptDescription: string.Empty,
							systemMessages: [SystemMessage],
							userMessages: [UserMessage]);
			}
			else 
			{ 
				Globals.ThisAddIn.PromptsHistory.Add(
					endpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
					modelName: SelectedModel.Id,
					modelDescription: string.Empty,
					options: ChatOptions,
					messageMode: messageMode,
					promptDescription: string.Empty,
					systemMessages: [SystemMessage],
					userMessages: [UserMessage],
					isFavorite: false);
			}
		}

		private void SendMessageButtonsBar_ClickUndoMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.ActiveDocument.ReplaceText(replaceRange, chatMessageCollection.GetUndo());
		
		private void SendMessageButtonsBar_ClickRedoMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.ActiveDocument.ReplaceText(replaceRange, chatMessageCollection.GetRedo());
		
		#endregion

		private void ChatMessageCollection_StatusChanged(object sender, EventArgs e)
		{
			if (chatMessageCollection.IsRedo)
			{
				ChatMessageMode = MessageMode.Undo | MessageMode.Redo;
			}
			else if (chatMessageCollection.IsRepeat)
			{
				ChatMessageMode = MessageMode.Undo | MessageMode.Repeat;
			}
			sendMessageButtonsBar.SetUndoButtonsEnabled(chatMessageCollection.IsUndo);
		}

		private void UserMessageTextBox_TextChanged(object sender, EventArgs e)
		{			
			if (Globals.ThisAddIn.Selection is null)
				GlobalsEventsBus.RemovingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.SelectDocument);
			else
				GlobalsEventsBus.TogglingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.SelectDocument);

			if (string.IsNullOrWhiteSpace(UserMessage))
				GlobalsEventsBus.RemovingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.UserMessage);
			else
				GlobalsEventsBus.TogglingStateForAccessToSendingUserMessage(Document, AccessToSendingUserMessage.UserMessage);

			if (promptHistoryCollection.IsBuffer)
			{
				promptHistoryCollection.SetUserMessage(UserMessage);
				promptsHistoryBox.DeleteButtonEnabled = !string.IsNullOrEmpty(UserMessage);
			}				
		}

		/// <summary>
		/// Событие, связанное с отправкой промпта.
		/// </summary>
		private void GlobalsEventsBus_SendChatMessage(object sender, WordDocumentSendMessageEventArgs e)
		{
			if (e.Range.Document.Windows[1].Hwnd != Document.Hwnd) return;
			
			sendMessageButtonsBar.SetButtonsEnabled(false);
		}
				
		/// <summary>
		/// Событие, связанное с получением ответа от модели.
		/// </summary>
		private void GlobalsEventsBus_ReceiveChatMessage(object sender, WordDocumentReceiveMessageEventArgs e)
		{
			if (e.Range.Document.Windows[1].Hwnd != Document.Hwnd) return;
			
			replaceRange = e.Range;
			sendMessageButtonsBar.SetButtonsEnabled();
			chatMessageCollection.Add(e.Message);
		}

		/// <summary>
		/// Событие, связанное с изменением типа вставки ответа ИИ в документ.
		/// </summary>
		private void GlobalsEventsBus_DocumentChatMessageModeChanged(object sender, DocumentChatMessageModeEventArgs e)
		{
			if (e.Document.Hwnd != Document.Hwnd) return;

			int id = promptHistoryCollection.SelectedPrompt.Id;

			chatMessageCollection.Clear();

			IEnumerable<Prompt> prompts = Globals.ThisAddIn.PromptsHistory.GetPrompts(e.MessageMode)
				.Select(p => new Prompt(p));

			if (SelectedModel is not null)
				promptHistoryCollection.RefreshHistory(
					array: [.. prompts],
					description: string.Empty,
					modelName: SelectedModel.Id,
					modelDescription: string.Empty,
					profileEndpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
					systemMessages: [SystemMessage],
					userMessages: [UserMessage],
					chatOptions: ChatOptions,
					messageMode: e.MessageMode,
					isFavorite: false);
			else
				promptHistoryCollection.RefreshHistory(array: [.. prompts]);
		}

		/// <summary>
		/// Событие, связанное с изменением истории промптов в хранилище. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlobalsEventsBus_PromptsHistoryCollectionChanged(object sender, PromptsHistoryEventArgs e)
		{
			int index = e.Prompt is not null ?  promptHistoryCollection.IndexOf(e.Prompt.Id) : -1;					
			
			IEnumerable<Prompt> prompts = Globals.ThisAddIn.PromptsHistory.GetPrompts(messageMode: ChatMessageMode)
				.Select(p => new Prompt(p));

			if (SelectedModel is not null)
				promptHistoryCollection.RefreshHistory(
							array: [.. prompts],
							description: string.Empty,
							modelName: SelectedModel.Id,
							modelDescription: string.Empty,
							profileEndpoint: SelectedModel.Profile.ClientOptions.Endpoint.OriginalString,
							systemMessages: [SystemMessage],
							userMessages: [UserMessage],
							chatOptions: ChatOptions,
							messageMode: ChatMessageMode,
							isFavorite: false);
			else
				promptHistoryCollection.RefreshHistory(array: [.. prompts]);

				switch (e.State)
				{
					case PromptsHistoryState.Unchanged:
					case PromptsHistoryState.Added:
					case PromptsHistoryState.Cleared:
					default:
						if (promptHistoryCollection.First is not null)
							promptHistoryCollection.GoLast();
						break;
					case PromptsHistoryState.Deleted:
						if (index >= 0 &&
							promptHistoryCollection.Count < index)
							promptHistoryCollection.GoByIndex(index);
						else
							promptHistoryCollection.GoLast();
						break;
					case PromptsHistoryState.Modified:
						promptHistoryCollection.Go(e.Prompt.Id);
						break;
				}			
		}

		/// <summary>
		/// Событие, связанное с изменением коллекции больших языковых моделей. 
		/// </summary>
		private void GlobalsEventsBus_ModelsCollectionChanged(object sender, ProcessStateEventArgs e)
		{
			if (e.ProcessState == ProcessState.Completed)
			{
				promptsHistoryBox.InitializeModelsSource(Globals.ThisAddIn.Models);
				promptsHistoryBox.ModelsComboBoxEnabled = true;
			}
			else
				promptsHistoryBox.ModelsComboBoxEnabled = false;
		}				
	}
}
