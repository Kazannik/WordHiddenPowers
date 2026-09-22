using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MessageMode = WordHiddenPowers.Documents.Document.ChatMessageModeEnum;

namespace WordHiddenPowers.Controls.PromptsHistoryControl
{
	public class PromptCollection : IEnumerable<Prompt>
	{
		private ModeType _mode = ModeType.Nothing;

		private CollectionItem first;

		private CollectionItem current = new(new Prompt(
			newId: -1,
			description: string.Empty,
			modelName: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName,
			modelDescription: string.Empty,
			profileEndpoint: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint,
			systemMessages: [Globals.ThisAddIn.GlobalsSetting.DefaultSystemMessage],
			userMessages: [],
			chatOptions: Globals.ThisAddIn.GlobalsSetting.DefaultChatOptions,
			messageMode: MessageMode.Nothing,
			isFavorite: false));

		public PromptCollection() { }

		#region Event

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ModeChanged;
		protected virtual void OnModeChanged(EventArgs e) => ModeChanged?.Invoke(this, e);

#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
		private void DoModeChanged(ModeType mode)
		{
			OnModeChanged(new EventArgs());
		}

#pragma warning restore IDE0060 // Удалите неиспользуемый параметр

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler StatusChanged;
		public void OnStatusChanged() => StatusChanged?.Invoke(this, new EventArgs());

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler SelectedPromptPropertiesChanged;
		public void OnSelectedPromptPropertiesChanged() => SelectedPromptPropertiesChanged?.Invoke(this, new EventArgs());

		#endregion

		public Prompt this[int index] => first.GetByIndex(index).Prompt;

		public Prompt CreateCurrentPrompt(
			string description,
			string modelName,
			string modelDescription,
			string profileEndpoint,
			string[] systemMessages,
			string[] userMessages,
			IChatOptions chatOptions,
			MessageMode messageMode,
			bool isFavorite)
		{
			current = new(
				new Prompt(
					newId: -1,
					description: description,
					modelName: modelName,
					modelDescription: modelDescription,
					profileEndpoint: profileEndpoint,
					systemMessages: systemMessages,
					userMessages: userMessages,
					chatOptions: chatOptions,
					messageMode: messageMode,
					isFavorite: isFavorite));
			
			return current.Prompt;
		}

		public Prompt CreateCurrentPrompt()
		{
			current = new(new Prompt(
				newId: -1,
				description: string.Empty,
				modelName: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName,
				modelDescription: string.Empty,
				profileEndpoint: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint,
				systemMessages: [Globals.ThisAddIn.GlobalsSetting.DefaultSystemMessage],
				userMessages: [],
				chatOptions: Globals.ThisAddIn.GlobalsSetting.DefaultChatOptions,
				messageMode: MessageMode.Nothing,
				isFavorite: false));
			return current.Prompt;
		}

		public void Add(Prompt prompt)
		{
			AddItem(prompt: prompt);
			if (Count > 1)
				Mode |= ModeType.Collection;
			else 
				Mode &= ~ModeType.Collection;
			
			OnStatusChanged();
		}

		public void AddRange(Prompt[] array)
		{
			AddItems(array: array);			
			if (Count > 1)
				Mode |= ModeType.Collection;
			else
				Mode &= ~ModeType.Collection;

			OnStatusChanged();
		}

		public void Clear()
		{
			first = null;
			Mode &= ~ModeType.Collection;
			OnStatusChanged();
		}

		/// <summary>
		/// Проверяет наличие хотя бы одного промпта с таким же пользовательских запросом. 
		/// </summary>
		/// <param name="userMessages"></param>
		/// <returns></returns>
		public bool ExistsPrompt(string[] userMessages, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
		{
			List<string> messages = [.. userMessages];
			messages.Sort();
			return first.Exists(userMessages: messages, comparisonType: comparisonType);
		}

		/// <summary>
		/// Обновить историю пропмтов и добавить буфер в конец коллекции.
		/// Активным является буфер.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="description"></param>
		/// <param name="modelName"></param>
		/// <param name="modelDescription"></param>
		/// <param name="profileEndpoint"></param>
		/// <param name="systemMessages"></param>
		/// <param name="userMessages"></param>
		/// <param name="chatOptions"></param>
		/// <param name="messageMode"></param>
		/// <param name="isFavorite"></param>
		public void RefreshHistory(
			Prompt[] array,
			string description,
			string modelName,
			string modelDescription,
			string profileEndpoint,
			string[] systemMessages,
			string[] userMessages,
			IChatOptions chatOptions,
			MessageMode messageMode,
			bool isFavorite)
		{
			first = null;
			AddItems(array: array);
			AddItem(prompt: new Prompt(
				newId: -1,
				description: description,
				modelName: modelName,
				modelDescription: modelDescription,
				profileEndpoint: profileEndpoint,
				systemMessages: systemMessages,
				userMessages: userMessages,
				chatOptions: chatOptions,
				messageMode: messageMode,
				isFavorite: isFavorite));

			current = (first.Last is not null) ? first.Last : first;
			current.IsBuffer = true;

			Mode = ModeType.Buffer;

			if (Count > 0)
				Mode |= ModeType.Collection;
			else
				Mode &= ~ModeType.Collection;

			OnStatusChanged();
		}

		/// <summary>
		/// Обновить историю пропмтов и продублировать в конец коллекции в буфер последний промпт.
		/// Активным является буфер.
		/// </summary>
		/// <param name="array"></param>
		public void RefreshHistory(
			Prompt[] array)
		{
			first = null;
			AddItems(array: array);
			if (Count > 0)
			{
				CollectionItem item = first.Last;
				AddItem(prompt: new Prompt(
					newId: -1,
					description: item.Prompt.Description,
					modelName: item.Prompt.ModelName,
					modelDescription: item.Prompt.ModelDescription,
					profileEndpoint: item.Prompt.ProfileEndpoint,
					systemMessages: item.Prompt.SystemMessages,
					userMessages: item.Prompt.UserMessages,
					chatOptions: item.Prompt.ChatOptions,
					messageMode: Documents.Document.CreateChatMessageMode(
						insertHereMessage: item.Prompt.InsertHereMessage,
						replaceSelectionMessage: item.Prompt.ReplaceSelectionMessage,
						insertNextMessage: item.Prompt.InsertNextMessage,
						insertPreviousMessage: item.Prompt.InsertPreviousMessage,
						insertBetweenMessage: item.Prompt.InsertBetweenMessage),
					isFavorite: false));
				
				current = (first.Last is not null) ? first.Last : first;
				current.IsBuffer = true;
				Mode = ModeType.Buffer | ModeType.Collection;
			}
			else
			{
				Mode = ModeType.Nothing;
			}
			OnStatusChanged();
		}

		private CollectionItem AddItem(Prompt prompt)
		{
			if (first is null)
			{
				first = new CollectionItem(prompt);
				return first;
			}
			else
			{
				return first.Last.Add(prompt);
			}
		}

		private void AddItems(Prompt[] array)
		{
			foreach (Prompt prompt in array.OrderBy(p => p.Date))
			{
				AddItem(prompt);
			}			
		}

		public ModeType Mode
		{
			get => _mode;
			set
			{
				if (value == _mode) return;
				_mode = value;
				DoModeChanged(_mode);
			}
		}

		public Prompt First => first?.Prompt;
		
		public Prompt Previous => current?.Previous?.Prompt;
		
		public Prompt SelectedPrompt => current?.Prompt;
		
		public int SelectedIndex => current is not null ? current.Index : -1;
		
		public Prompt Next => current?.Next?.Prompt;

		public Prompt Last => first.Last.Prompt;

		public bool IsPrevious => current?.Previous is not null;

		public bool IsNext => current?.Next is not null;

		public bool IsBuffer => current is not null && current.IsBuffer;

		public int Count => first is not null ? first.Last.Index : 0;

		public Prompt GoPrevious()
		{
			try
			{
				if (IsPrevious)
				{
					current = current.Previous;
					return current.Prompt;
				}
				else
					return null;
			}
			catch (Exception) { throw; }
			finally
			{
				OnStatusChanged();
			}
		}

		public Prompt GoNext()
		{
			try
			{
				if (IsNext)
				{
					current = current.Next;
					return current.Prompt;
				}
				else
					return null;
			}
			catch (Exception) { throw; }
			finally
			{
				OnStatusChanged();
			}
		}

		public Prompt GoLast()
		{
			try
			{
				current = first.Last;
				return current.Prompt;
			}
			catch (Exception) { throw; }
			finally
			{
				OnStatusChanged();
			}
		}

		public Prompt Go(int id)
		{
			try
			{
				CollectionItem item = first?.GetById(id);
				if (item is not null)
					current = item;

				return current.Prompt;
			}
			catch (Exception) { throw; }
			finally
			{
				OnStatusChanged();
			}
		}

		public Prompt GoByIndex(int index)
		{
			try
			{
				CollectionItem item = first?.GetByIndex(index);
				if (item is not null)
					current = item;

				return current.Prompt;
			}
			catch (Exception) { throw; }
			finally
			{
				OnStatusChanged();
			}
		}

		public int IndexOf(int id)
		{
			return first is not null ? first.IndexOf(id) : -1;
		}

		public void SetOptions(IChatOptions options) => current?.SetOptions(options);

		public void SetSystemMessage(string message) => current?.SetSystemMessage(message);

		public void SetModel(IModel model) => current?.SetModel(model);

		public void SetMessageMode(MessageMode mode) => current?.SetMessageMode(mode);

		public void SetUserMessage(string message) => current?.SetUserMessage(message);

		public IEnumerator<Prompt> GetEnumerator() => new PromptEnum(firstItem: first);
		
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		private class CollectionItem(Prompt prompt)
		{
			private bool isBuffer = false;

			public CollectionItem(CollectionItem previous, Prompt prompt) : this(prompt: prompt)
			{
				Previous = previous;
			}

			public CollectionItem(CollectionItem previous, CollectionItem next, Prompt prompt) : this(prompt: prompt)
			{
				Previous = previous;
				Next = next;
			}

			public Prompt Prompt { get; private set; } = prompt;

			public CollectionItem Previous { get; private set; }

			public CollectionItem Next { get; private set; }

			public CollectionItem Last => Next is not null ? Next.Last : this;
			
			/// <summary>
			/// Признак того, что элемент является временным буфером.
			/// </summary>
			public bool IsBuffer
			{
				get { return isBuffer; }
				set
				{
					if (value == false)
					{
						isBuffer = false;
					}
					else
					{
						DisabledPreviousBuffer();
						DisabledNextBuffer();
						isBuffer = true;
					}
				}
			}
			
			public CollectionItem Add(Prompt prompt)
			{
				Next = new CollectionItem(previous: this, prompt: prompt);
				return Next;
			}

			public CollectionItem Insert (Prompt prompt)
			{
				CollectionItem item = Next;
				Next = new CollectionItem(previous: this, next: item, prompt);
				item.Previous = Next;
				return Next;
			}

			/// <summary>
			/// Переместить текущий элемент в конец очереди.
			/// </summary>
			public void MoveThisToLast()
			{
				CollectionItem lastItem = Last;
				Previous.Next = Next;
				Next = null;
				lastItem.Next = this;
			}

			/// <summary>
			/// Переместить последний элемент в позицию после текущего элемента. 
			/// </summary>
			public void MoveLastAfterCurrent()
			{
				CollectionItem movedItem = Last;
				movedItem.Previous.Next = null;
				movedItem.Next = Next;
				Next = movedItem;				
			}

			#region Edit SelectedPrompt

			public void SetOptions(IChatOptions options)
			{
				Prompt = new(
					newId: Prompt.Id,
					description: Prompt.Description,
					modelName: Prompt.ModelName,
					modelDescription: Prompt.ModelDescription,
					profileEndpoint: Prompt.ProfileEndpoint,
					systemMessages: Prompt.SystemMessages,
					userMessages: Prompt.UserMessages,
					chatOptions: options,
					insertHereMessage: Prompt.InsertHereMessage,
					replaceSelectionMessage: Prompt.ReplaceSelectionMessage,
					insertNextMessage: Prompt.InsertNextMessage,
					insertPreviousMessage: Prompt.InsertPreviousMessage,
					insertBetweenMessage: Prompt.InsertBetweenMessage,
					isFavorite: Prompt.IsFavorite);
			}

			public void SetSystemMessage(string message)
			{
				Prompt = new(
					newId: Prompt.Id,
					description: Prompt.Description,
					modelName: Prompt.ModelName,
					modelDescription: Prompt.ModelDescription,
					profileEndpoint: Prompt.ProfileEndpoint,
					systemMessages: [message],
					userMessages: Prompt.UserMessages,
					chatOptions: Prompt.ChatOptions,
					insertHereMessage: Prompt.InsertHereMessage,
					replaceSelectionMessage: Prompt.ReplaceSelectionMessage,
					insertNextMessage: Prompt.InsertNextMessage,
					insertPreviousMessage: Prompt.InsertPreviousMessage,
					insertBetweenMessage: Prompt.InsertBetweenMessage,
					isFavorite: Prompt.IsFavorite);
			}

			public void SetModel(IModel model)
			{
				Prompt = new(
					newId: Prompt.Id,
					description: Prompt.Description,
					modelName: model.Id,
					modelDescription: Prompt.ModelDescription,
					profileEndpoint: model.Profile.ClientOptions.Endpoint.OriginalString,
					systemMessages: Prompt.SystemMessages,
					userMessages: Prompt.UserMessages,
					chatOptions: Prompt.ChatOptions,
					insertHereMessage: Prompt.InsertHereMessage,
					replaceSelectionMessage: Prompt.ReplaceSelectionMessage,
					insertNextMessage: Prompt.InsertNextMessage,
					insertPreviousMessage: Prompt.InsertPreviousMessage,
					insertBetweenMessage: Prompt.InsertBetweenMessage,
					isFavorite: Prompt.IsFavorite);
			}

			public void SetMessageMode(MessageMode mode)
			{
				Prompt = new(
					newId: Prompt.Id,
					description: Prompt.Description,
					modelName: Prompt.ModelName,
					modelDescription: Prompt.ModelDescription,
					profileEndpoint: Prompt.ProfileEndpoint,
					systemMessages: Prompt.SystemMessages,
					userMessages: Prompt.UserMessages,
					chatOptions: Prompt.ChatOptions,
					insertHereMessage: mode == MessageMode.Insert,
					replaceSelectionMessage: mode == MessageMode.ReplaceSelection,
					insertNextMessage: mode == MessageMode.InsertNext,
					insertPreviousMessage: mode == MessageMode.InsertPrevious,
					insertBetweenMessage: mode == MessageMode.InsertBetween,
					isFavorite: Prompt.IsFavorite);
			}

			public void SetUserMessage(string message)
			{
				Prompt = new(
					newId: Prompt.Id,
					description: Prompt.Description,
					modelName: Prompt.ModelName,
					modelDescription: Prompt.ModelDescription,
					profileEndpoint: Prompt.ProfileEndpoint,
					systemMessages: Prompt.SystemMessages,
					userMessages: [message],
					chatOptions: Prompt.ChatOptions,
					insertHereMessage: Prompt.InsertHereMessage,
					replaceSelectionMessage: Prompt.ReplaceSelectionMessage,
					insertNextMessage: Prompt.InsertNextMessage,
					insertPreviousMessage: Prompt.InsertPreviousMessage,
					insertBetweenMessage: Prompt.InsertBetweenMessage,
					isFavorite: Prompt.IsFavorite);
			}

			#endregion


			public int Index => Previous is not null ? Previous.Index + 1 : 0;

			/// <summary>
			/// Индекс промпта в базе данных.
			/// </summary>
			public int Id => Prompt.Id;

			public CollectionItem GetById(int id)
			{
				if (Prompt.Id == id)
					return this;
				else if (Next is not null)
					return Next.GetById(id);
				else
					return null;
			}

			public CollectionItem GetByIndex(int index)
			{
				if (Index == index)
					return this;
				else
					return Next.GetByIndex(index);
			}

			public int IndexOf(int id)
			{
				if (Prompt.Id == id)
					return Index;
				else if (Next is not null)
					return Next.IndexOf(id);
				else
					return -1;
			}

			public bool Exists(List<string> userMessages, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
			{
				if (MessagesEquals(messages1: userMessages, Prompt.UserMessages, comparisonType: comparisonType))
					return true;
				else if (Next is not null)
					return Next.Exists(userMessages: userMessages, comparisonType: comparisonType);
				else
					return false;
			}

			private void DisabledPreviousBuffer()
			{
				if (Previous is not null)
				{
					Previous.isBuffer = false;
					Previous.DisabledPreviousBuffer();
				}
			}

			private void DisabledNextBuffer()
			{
				if (Next is not null)
				{
					Next.isBuffer = false;
					Next.DisabledNextBuffer();
				}
			}

			private static bool MessagesEquals(List<string> messages1, string[] messages2, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
			{
				if (messages1.Count != messages2.Length)  return false;
				List<string> list = [.. messages2];
				list.Sort();

				for (int i = 0; i < messages1.Count; i++) 
				{
					if (!list[i].Equals(messages2[i], comparisonType: comparisonType)) return false;
				}
				return true;
			}

			public static bool operator ==(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) == 0;

			public static bool operator !=(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) != 0;

			public static bool operator >(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) > 0;

			public static bool operator <(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) < 0;

			public static bool operator >=(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) >= 0;

			public static bool operator <=(CollectionItem x, CollectionItem y) => DateTime.Compare(x.Prompt.Date, y.Prompt.Date) <= 0;
		}

		private class PromptEnum(CollectionItem firstItem) : IEnumerator<Prompt>
		{
			private readonly CollectionItem firstItem = firstItem;
			private CollectionItem currentItem;

			public bool MoveNext()
			{
				currentItem = currentItem.Next;
				return currentItem is not null;
			}

			public void Reset() => currentItem = firstItem;

			public void Dispose() => throw new NotImplementedException();

			public Prompt Current
			{
				get
				{
					try
					{
						return currentItem.Prompt;
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidOperationException();
					}
				}
			}

			Prompt IEnumerator<Prompt>.Current => Current;
			object IEnumerator.Current => Current;
		}

		/// <summary>
		/// Режим работы.
		/// </summary>
		[Flags]
		public enum ModeType : byte
		{
			Nothing = 0,
			Buffer = 1 << 1,
			Collection = 1 << 2,
			All = Buffer | Collection,
		}
	}
}
