// #define HOOK

#if HOOK
using MyMicrosoft.Office.Hooks;
using System.Runtime.InteropServices;
#endif

using LLMConnectorLibrary;
using LLMConnectorLibrary.Authentication;
using LLMConnectorLibrary.EventArgs;
using LLMConnectorLibrary.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Windows.Forms;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;


namespace WordHiddenPowers
{
	public partial class ThisAddIn
	{
		/// <summary>
		/// Основная коллекция документов для обепчения дополнительныхфункций.
		/// </summary>
		public Documents.DocumentCollection Documents { get; private set; }

		/// <summary>
		/// Активный документ.
		/// </summary>
		public Documents.Document ActiveDocument => Documents?.ActiveDocument;

		/// <summary>
		/// Выделенный фрагмент активного документа.
		/// </summary>
		public Word.Selection Selection => Globals.ThisAddIn.Application.ActiveWindow?.Selection;

		/// <summary>
		/// Глобальные настройки.
		/// </summary>
		public Repository.GlobalsSetting GlobalsSetting { get; } = new Repository.GlobalsSetting();

		/// <summary>
		/// Ностройки профилей подключения ИИ.
		/// </summary>
		public Repository.ProfilesCollection ProfilesSetting { get; } = new Repository.ProfilesCollection();

		/// <summary>
		/// Шаблоны профилей подключения ИИ.
		/// </summary>
		public Repository.ProfilesCollection ProfilesTemplates { get; } = new Repository.ProfilesCollection();

		/// <summary>
		/// История промптов.
		/// </summary>
		public Repository.PromptsHistory PromptsHistory { get; } = new Repository.PromptsHistory();

		/// <summary>
		/// Коллекция активных профилей подключения ИИ.
		/// </summary>
		public AuthenticationProfileCollection Profiles { get; } = [];

		public ModelsCollection Models { get; } = [];

		public LLMClient LLMClient { get; } = new LLMClient();

		private void ThisAddIn_Startup(object sender, EventArgs e)
		{
			LLMClient.HostChecked += new EventHandler<CheckHostEventArgs>(LLMClient_HostChecked);
			LLMClient.ModelsCollectionCompleted += new EventHandler<ModelsCollectionCompletedEventArgs>(LLMClient_ModelsCollectionCompleted);
			LLMClient.ClientError += new EventHandler<ClientErrorEventArgs>(LLMClient_ClientError);

			InitializeSettingFromXML();
			
			#if HOOK
				mouseProc = MouseHookCallback;
				keyboardProc = KeyboardHookCallback;
				SetWindowsHooks();
			#endif
			
			Documents = new Documents.DocumentCollection(paneVisibleButton: Globals.Ribbons.AddInMainRibbon.paneVisibleButton);
			try
			{
				Globals.Ribbons.AddInMainRibbon.LLMButtonUpdate();
			}
			catch (Exception) { }

			GlobalsEventsBus.DoInitializeComponent(this);
		}

		private void LLMClient_ClientError(object sender, ClientErrorEventArgs e)
		{
			Utils.Dialogs.ShowErrorDialog(e.Exception.Message);
		}

		private void ThisAddIn_Shutdown(object sender, EventArgs e)
		{
			#if HOOK			
				UnhookWindowsHooks();			
			#endif
			
			Utils.Dialogs.CloseAllDialogs();

			Documents.Dispose();

			SaveSettingToXML();
		}

		private void InitializeSettingFromXML()
		{
			if (File.Exists(FileSystem.GetGlobalsSettingFileName()))
				Xml.LoadGlobalsSettingData(GlobalsSetting, FileSystem.GetGlobalsSettingFileName());

			GlobalsSetting.AcceptChanges();

			if (File.Exists(FileSystem.GetProfileTemplatesFileName()))
				Xml.LoadProfilesData(ProfilesTemplates, FileSystem.GetProfileTemplatesFileName());

			ProfilesTemplates.InitializeTypesTables();
			ProfilesTemplates.AcceptChanges();

			if (File.Exists(FileSystem.GetProfilesFileName()))
				Xml.LoadProfilesData(ProfilesSetting, FileSystem.GetProfilesFileName());

			ProfilesSetting.InitializeTypesTables();
			ProfilesSetting.AcceptChanges();


			if (File.Exists(FileSystem.GetHistoryFileName()))
				Xml.LoadHistoryData(PromptsHistory, FileSystem.GetHistoryFileName());
			
			PromptsHistory.AcceptChanges();

			GlobalsEventsBus.DoInitializeSetting(this);
		}

		private void SaveSettingToXML()
		{
			if (GlobalsSetting.HasChanges())
			{
				GlobalsSetting.AcceptChanges();
				Xml.SaveGlobalsSettingData(GlobalsSetting, FileSystem.GetGlobalsSettingFileName());
			}
			if (ProfilesSetting.HasChanges())
			{
				ProfilesSetting.AcceptChanges();
				Xml.SaveProfilesData(ProfilesSetting, FileSystem.GetProfilesFileName());
			}
			if (PromptsHistory.HasChanges() || PromptsHistory.Prompts.Count == 0)
			{
				PromptsHistory.AcceptChanges();
				Xml.SaveHistoryData(PromptsHistory, FileSystem.GetHistoryFileName());
			}
		}
				

		private async void GlobalsEventsBus_InitializeSetting(object sender, EventArgs e)
		{
			Globals.Ribbons.AddInMainRibbon.llmButton1.Label = Globals.ThisAddIn.GlobalsSetting.CaptionButton1;
			Globals.Ribbons.AddInMainRibbon.llmButton1.SuperTip = $"Системный промпт: {Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1}";

			Globals.Ribbons.AddInMainRibbon.llmButton2.Label = Globals.ThisAddIn.GlobalsSetting.CaptionButton2;
			Globals.Ribbons.AddInMainRibbon.llmButton2.SuperTip = $"Системный промпт: {Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2}";

			Profiles.Clear();
			Profiles.AddRange(ProfilesSetting);
			await Profiles.CheckStateAsync();
			
			GlobalsEventsBus.DoProfilesStateChanged(this);
		}

		private async void GlobalsEventsBus_ProfilesStateChanged(object sender, EventArgs e)
		{
			try
			{
				Models.Clear();
				GlobalsEventsBus.DoModelsCollectionChanged(this, EventsBus.StateEnums.ProcessState.Initialize);
				
				foreach (IAuthenticationProfile profile in Globals.ThisAddIn.Profiles.ConnectedProfiles())
				{
					await Globals.ThisAddIn.LLMClient.ReadModelsNameAsync(profile: profile);
				}
				GlobalsEventsBus.DoModelsCollectionChanged(this, EventsBus.StateEnums.ProcessState.Completed);
			}
			catch (Exception)
			{
				GlobalsEventsBus.DoModelsCollectionChanged(this, EventsBus.StateEnums.ProcessState.Canceled);
			}
		}

		private void LLMClient_HostChecked(object sender, CheckHostEventArgs e)
		{
			if (!e.IsAvailable)
			{
				GlobalsEventsBus.DoModelsCollectionChanged(this, EventsBus.StateEnums.ProcessState.Initialize);
				List<IModel> list = [.. Models.ToArray()];
				foreach (IModel model in list)
				{
					if (model.Profile.Equals(e.Profile))
					{
						Models.Remove(model);
					}
				}
				GlobalsEventsBus.DoModelsCollectionChanged(this, EventsBus.StateEnums.ProcessState.Completed);
			}
		}

		private void LLMClient_ModelsCollectionCompleted(object sender, ModelsCollectionCompletedEventArgs e)
		{
			Models.AddRange(e.Models);
		}

		#region Hooks

		#if HOOK
		
		private SafeNativeMethods.HookProc mouseProc;
		private SafeNativeMethods.HookProc keyboardProc;

		private IntPtr hookIdMouse;
		private IntPtr hookIdKeyboard;

		private void SetWindowsHooks()
		{
			uint threadId = (uint)SafeNativeMethods.GetCurrentThreadId();

			hookIdMouse =
				SafeNativeMethods.SetWindowsHookEx(
					(int)SafeNativeMethods.HookType.WH_MOUSE,
					mouseProc,
					IntPtr.Zero,
					threadId);

			hookIdKeyboard =
				SafeNativeMethods.SetWindowsHookEx(
					(int)SafeNativeMethods.HookType.WH_KEYBOARD,
					keyboardProc,
					IntPtr.Zero,
					threadId);
		}

		private void UnhookWindowsHooks()
		{
			SafeNativeMethods.UnhookWindowsHookEx(hookIdMouse);
			SafeNativeMethods.UnhookWindowsHookEx(hookIdKeyboard);
		}

		private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				var mouseHookStruct =
					(SafeNativeMethods.MouseHookStructEx)
					Marshal.PtrToStructure(lParam, typeof(SafeNativeMethods.MouseHookStructEx));
				var message = (SafeNativeMethods.WindowMessages)wParam;

				//System.Diagnostics.Debug.WriteLine(
				//	"{0} event detected at position {1} - {2}",
				//	message,
				//	mouseHookStruct.pt.X,
				//	mouseHookStruct.pt.Y);
			}
			return SafeNativeMethods.CallNextHookEx(
				hookIdKeyboard,
				nCode,
				wParam,
				lParam);
		}

		private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				//Word.Range range;
				//string systemPrompt, prompt;

				//if (DocumentService.ReadPrompt(Doc: ActiveDocument.Doc, Sel: Selection, editRange: out range, systemPrompt: out systemPrompt, prompt: out prompt))
				//{
				//	ActiveDocument.Ai(range, systemPrompt, prompt);
				//}
			}
			return SafeNativeMethods.CallNextHookEx(
				hookIdKeyboard,
				nCode,
				wParam,
				lParam);
		}
#endif

		#endregion

		#region Код, автоматически созданный VSTO

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InternalStartup()
		{
			Startup += new EventHandler(ThisAddIn_Startup);
			Shutdown += new EventHandler(ThisAddIn_Shutdown);

			((Word.ApplicationEvents4_Event)Application).NewDocument += new Word.ApplicationEvents4_NewDocumentEventHandler(GlobalsEventsBus.DoNewDocument);
			Application.DocumentOpen += new Word.ApplicationEvents4_DocumentOpenEventHandler(GlobalsEventsBus.DoDocumentOpen);
			Application.DocumentBeforeClose += new Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(GlobalsEventsBus.DoDocumentBeforeClose);
			Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(GlobalsEventsBus.DoDocumentWindowActivate);
			Application.WindowDeactivate += new Word.ApplicationEvents4_WindowDeactivateEventHandler(GlobalsEventsBus.DoDocumentWindowDeactivate);
			Application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(GlobalsEventsBus.DoDocumentSelectionChange);

			GlobalsEventsBus.InitializeSetting += new EventHandler(GlobalsEventsBus_InitializeSetting);
			GlobalsEventsBus.ProfilesStateChanged += new EventHandler(GlobalsEventsBus_ProfilesStateChanged);
		}

		#endregion
	}
}
