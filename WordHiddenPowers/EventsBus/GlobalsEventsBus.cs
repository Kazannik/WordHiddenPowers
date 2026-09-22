using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WordHiddenPowers.Documents;
using WordHiddenPowers.EventsBus.EventArgs;
using WordHiddenPowers.EventsBus.StateEnums;
using WordHiddenPowers.Repository.History;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus
{
	/// <summary>
	///  Глобальная шина событий. Инициатор должен производить действие после чего создает уведомление о произведенном действии, которое вызывает слбытие Event.
	///  Подписка производся путем создания ссылки на событие EventHandler
	/// </summary>
	public static class GlobalsEventsBus
	{
		#region Initialize

		/// <summary>
		/// Дополнительная инициализация компонентов.
		/// Вызывается последним в процедуре ThisAddIn_Startup(object sender, EventArgs e).
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler InitializeComponent;

		/// <summary>
		/// Создать уведомление о дополнительной инициализации компонентов.
		/// Вызывается последним в процедуре ThisAddIn_Startup(object sender, EventArgs e).
		/// </summary>
		/// <param name="sender">Объект, создавший уведомление.</param>
		public static void DoInitializeComponent(object sender) =>
			InitializeComponent?.Invoke(sender, new System.EventArgs());

		/// <summary>
		/// Инициализация настроек из файла XML. 
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler InitializeSetting;
		
		/// <summary>
		/// Создать уведомление об инициализации настроек из файла XML.
		/// </summary>
		/// <param name="sender">Объект, создавший уведомление.</param>
		public static void DoInitializeSetting(object sender) =>
			InitializeSetting?.Invoke(sender, new System.EventArgs());

		#endregion
		
		#region Word Document

		/// <summary>
		/// Активация окна документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentWindowActivateEventArgs> DocumentWindowActivate;
		/// <summary>
		/// Создать уведомление об активации окна документа.
		/// </summary>
		/// <param name="Doc"></param>
		/// <param name="Wn"></param>
		public static void DoDocumentWindowActivate(Word.Document Doc, Word.Window Wn)
		{
			try
			{
				Document document = Globals.ThisAddIn.Documents.GetDocument(Doc);
				DocumentWindowActivate?.Invoke(document, new WordDocumentWindowActivateEventArgs(Doc, Wn));
				
				if (document.Pane.SelectedModel != null)
					TogglingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.SelectModel);
				else
					RemovingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.SelectModel);
				
				if (document.Pane.UserMessage.Length > 0)
					TogglingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.UserMessage);
				else
					RemovingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.UserMessage);
				
				TogglingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.SelectDocument);
			}
			catch (Exception) { }
		}


		/// <summary>
		/// Деактивация окна документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentWindowDeactivateEventArgs> DocumentWindowDeactivate;
		
		/// <summary>
		/// Создать уведомление о деактивации окна документа.
		/// </summary>
		/// <param name="Doc"></param>
		/// <param name="Wn"></param>
		public static void DoDocumentWindowDeactivate(Word.Document Doc, Word.Window Wn)
		{
			try
			{
				Document document = Globals.ThisAddIn.Documents.GetDocument(Doc);
				DocumentWindowDeactivate?.Invoke(document, new WordDocumentWindowDeactivateEventArgs(Doc, Wn));
				RemovingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.SelectDocument);
			}
			catch (Exception) { }
		}

		/// <summary>
		/// Событие, связанное с созданием нового документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentEventArgs> NewDocument;
		
		/// <summary>
		/// Создать уведомление о создании нового документа.
		/// </summary>
		/// <param name="Doc"></param>
		public static void DoNewDocument(Word.Document Doc) =>
			NewDocument?.Invoke(Globals.ThisAddIn.Documents.GetDocument(Doc), new WordDocumentEventArgs(Doc));

		/// <summary>
		/// Событие, связанное с открытием документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentEventArgs> DocumentOpen;
		
		/// <summary>
		/// Создать уведомление об открытии документа.
		/// </summary>
		/// <param name="Doc"></param>
		public static void DoDocumentOpen(Word.Document Doc) =>
			DocumentOpen?.Invoke(Globals.ThisAddIn.Documents.GetDocument(Doc), new WordDocumentEventArgs(Doc));

		/// <summary>
		/// Событие, связанное с закрытием документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentBeforeCloseEventArgs> DocumentBeforeClose;
		
		/// <summary>
		/// Создать уведомление о закрытии документа.
		/// </summary>
		/// <param name="Doc"></param>
		/// <param name="Cancel"></param>
		public static void DoDocumentBeforeClose(Word.Document Doc, ref bool Cancel) =>
			DocumentOpen?.Invoke(Globals.ThisAddIn.Documents.GetDocument(Doc), new WordDocumentBeforeCloseEventArgs(Doc, ref Cancel));

		/// <summary>
		/// Событие, связанное с выделением части документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordSelectionEventArgs> DocumentSelectionChange;
		
		/// <summary>
		/// Создать уведомление о выделении части документа. 
		/// </summary>
		/// <param name="Sel"></param>
		public static void DoDocumentSelectionChange(Word.Selection Sel)
		{
			try
			{
				Document document = Globals.ThisAddIn.Documents.GetDocument(Sel.Document);
				DocumentSelectionChange?.Invoke(document, new WordSelectionEventArgs(Sel));
				TogglingStateForAccessToSendingUserMessage(document: document, AccessToSendingUserMessage.SelectDocument);
			}
			catch (Exception) { }
		}
		
		#endregion

		#region Documents.Document

		/// <summary>
		/// Событие, связанное с изменением свойств документа.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<DocumentEventArgs> DocumentPropertiesChanged;
		
		/// <summary>
		/// Создать уведомление об изменении свойств документа.
		/// </summary>
		/// <param name="document"></param>
		public static void DoDocumentPropertiesChanged(Document document) =>
			DocumentPropertiesChanged?.Invoke(document, new DocumentEventArgs(document));

		/// <summary>
		/// Событие, связанное с изменением типа вставки ответа ИИ модели в документ.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<DocumentChatMessageModeEventArgs> DocumentChatMessageModeChanged;
		
		/// <summary>
		/// Создать уведомление о событии изменения типа вставки ответа ИИ модели в документ.
		/// </summary>
		/// <param name="document"></param>
		/// <param name="messageMode"></param>
		public static void DoDocumentChatMessageModeChanged(Document document, Document.ChatMessageModeEnum messageMode) =>
			DocumentChatMessageModeChanged?.Invoke(document, new DocumentChatMessageModeEventArgs(document, messageMode));

		#endregion

		#region Commands

		/// <summary>
		/// Событие, связанное с открытием окна вставки текстовой записи.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordSelectionEventArgs> TextNodeClick;
		
		/// <summary>
		/// Создать уведомление об открытии окна вставки текстовой записи. 
		/// </summary>
		/// <param name="Sel"></param>
		public static void DoTextNodeClick(Word.Selection Sel) =>
			TextNodeClick?.Invoke(Globals.ThisAddIn.Documents.GetDocument(Sel.Document), new WordSelectionEventArgs(Sel));

		/// <summary>
		/// Событие, связанное с открытием окна вставки цифровой записи.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordSelectionEventArgs> DecimalNodeClick;
		
		/// <summary>
		/// Содзать уведомление об открытии окна вставки цифровой записи.
		/// </summary>
		/// <param name="Sel"></param>
		public static void DoDecimalNodeClick(Word.Selection Sel) =>
			DecimalNodeClick?.Invoke(Globals.ThisAddIn.Documents.GetDocument(Sel.Document), new WordSelectionEventArgs(Sel));

		#endregion
				
		#region AI

		/// <summary>
		/// Изменение статуса хотя бы одного из профилей подключения к LLM.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler ProfilesStateChanged;

		/// <summary>
		/// Создать уведомление об изменении профилей.
		/// </summary>
		/// <param name="sender">Объект, создавший уведомление.</param>
		public static void DoProfilesStateChanged(object sender) =>
			ProfilesStateChanged?.Invoke(sender, new System.EventArgs());

		/// <summary>
		/// Изменение коллекции больших языковых моделей. 
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<ProcessStateEventArgs> ModelsCollectionChanged;

		/// <summary>
		/// Создать уведомление об изменении коллекции моделей.
		/// </summary>
		/// <param name="sender">Объект, создавший уведомление.</param>
		public static void DoModelsCollectionChanged(object sender, ProcessState state)
		{
			ModelsCollectionChanged?.Invoke(sender, new ProcessStateEventArgs(state: state));

			if (Globals.ThisAddIn.Models.Count == 0)
			{
				RemovingStateForAccessToSendingUserMessage(document: Globals.ThisAddIn.Documents?.ActiveDocument, AccessToSendingUserMessage.SelectModel);
			}
		} 

		/// <summary>
		/// Событие, связанное с отправкой промпта.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentSendMessageEventArgs> SendChatMessage;

		/// <summary>
		/// Создать уведомление об отправке промпта.
		/// </summary>
		/// <param name="range"></param>
		/// <param name="systemMessage"></param>
		/// <param name="userMessages"></param>
		public static void DoSendChatMessage(Word.Range range, string systemMessage, IEnumerable<string> userMessages) =>
			SendChatMessage?.Invoke(Globals.ThisAddIn.Documents.GetDocument(range.Document), new WordDocumentSendMessageEventArgs(range, systemMessage, userMessages));
		
		/// <summary>
		/// Событие, связанное с получением ответа модели.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<WordDocumentReceiveMessageEventArgs> ReceiveChatMessage;

		/// <summary>
		/// Создать уведомление о получении ответа.
		/// </summary>
		/// <param name="range"></param>
		/// <param name="systemMessage"></param>
		/// <param name="userMessages"></param>
		/// <param name="message"></param>
		/// <param name="model"></param>
		/// <param name="options"></param>
		public static void DoReceiveChatMessage(Word.Range range, string systemMessage, IEnumerable<string> userMessages, string message, IModel model, IChatOptions options)
		{
			ReceiveChatMessage?.Invoke(Globals.ThisAddIn.Documents.GetDocument(range.Document), new WordDocumentReceiveMessageEventArgs(range, systemMessage, userMessages, message, model, options));
		}

		#endregion

		#region AccessToSendingUserMessage (доступ к возможности отправки промпта)

		private static AccessToSendingUserMessage accessToSendingUserMessageState = AccessToSendingUserMessage.None;

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<AccessToSendingUserMessageEventArgs> AccessToSendingUserMessageStateChanged;

		/// <summary>
		/// Удалить сведения о выполнении условий доступа к отправке промпта.
		/// </summary>
		/// <param name="condition"></param>
		public static void RemovingStateForAccessToSendingUserMessage(Document document, AccessToSendingUserMessage condition)
		{
			accessToSendingUserMessageState &= ~condition;
			DoAccessToSendingUserMessageStateChanged(document, accessToSendingUserMessageState);
		}

		/// <summary>
		/// Добавить сведения о выполнении условий доступа к отправке промпта.
		/// </summary>
		/// <param name="condition"></param>
		public static void TogglingStateForAccessToSendingUserMessage(Document document, AccessToSendingUserMessage condition)
		{
			accessToSendingUserMessageState |= condition;
			DoAccessToSendingUserMessageStateChanged(document, accessToSendingUserMessageState);			
		}

		/// <summary>
		/// Получить сведения о выполнении условий доступа к отправке промпта.
		/// </summary>
		/// <returns></returns>
		public static AccessToSendingUserMessage GetStateForAccessToSendingUserMessage()
		{
			return accessToSendingUserMessageState;
		}

		private static void DoAccessToSendingUserMessageStateChanged(Document document, AccessToSendingUserMessage access) =>
			AccessToSendingUserMessageStateChanged?.Invoke(document, new AccessToSendingUserMessageEventArgs(document: document, access: access));

		#endregion

		#region PaneState

		public static bool PaneVisible => paneVisible;

		private static bool paneVisible = false;

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<PaneStateEventArgs> PaneStateChanged;
				
		public static void SetPaneVisibleState(Document document)
		{
			paneVisible = true;
			DoPaneStateChanged(document, paneVisible);
		}

		public static void SetPaneHideState(Document document)
		{
			paneVisible = false;
			DoPaneStateChanged(document, paneVisible);
		}

		public static void SetPaneInvertVisibleState(Document document)
		{
			paneVisible = !paneVisible;
			DoPaneStateChanged(document, paneVisible);
		}

		private static void DoPaneStateChanged(Document document, bool visible) =>
			PaneStateChanged?.Invoke(document, new PaneStateEventArgs(document: document, visible: visible));

		#endregion

		#region Prompts History

		/// <summary>
		/// Событие, связанное с изменение коллекции промптов.
		/// </summary>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public static event EventHandler<PromptsHistoryEventArgs> PromptsHistoryCollectionChanged;

		/// <summary>
		/// Создать уведомление об изменении коллекции промптов.
		/// </summary>
		/// <param name="sender">Объект, создавший уведомление.</param>
		public static void DoPromptsHistoryCollectionChanged(object sender, PromptsHistoryState state, DtoPrompt prompt) => 
			PromptsHistoryCollectionChanged?.Invoke(sender,new PromptsHistoryEventArgs(state: state, prompt: prompt));
		
		#endregion

	}
}
