using MyMicrosoft.Office.Hooks;
using System;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
	public partial class ThisAddIn
	{
		public Documents.DocumentCollection Documents { get; private set; }

		public Documents.Document ActiveDocument => Documents.ActiveDocument;

		public Word.Selection Selection => Globals.ThisAddIn.Application.ActiveWindow?.Selection;
		
		private void ThisAddIn_Startup(object sender, EventArgs e)
		{
			Properties.Settings.Default.Reload();

			Services.OpenAIService.MainSystemMessage = Properties.Settings.Default.MainSystemMessage;

			Services.OpenAIService.Uri = Properties.Settings.Default.LLMHostUri;
			Services.OpenAIService.LLMName = Properties.Settings.Default.LLMName;

			Services.OpenAIService.CaptionButton1 = Properties.Settings.Default.LLMButton1;
			Services.OpenAIService.SystemMessageButton1 = Properties.Settings.Default.LLMSystemMessage1;
			Services.OpenAIService.PrefixUserMessageButton1 = Properties.Settings.Default.LLMPrefixUserMessage1;
			Services.OpenAIService.PostfixUserMessageButton1 = Properties.Settings.Default.LLMPostfixUserMessage1;

			Services.OpenAIService.CaptionButton2 = Properties.Settings.Default.LLMButton2;
			Services.OpenAIService.SystemMessageButton2 = Properties.Settings.Default.LLMSystemMessage2;
			Services.OpenAIService.PrefixUserMessageButton2 = Properties.Settings.Default.LLMPrefixUserMessage2;
			Services.OpenAIService.PostfixUserMessageButton2 = Properties.Settings.Default.LLMPostfixUserMessage2;

			//mouseProc = MouseHookCallback;
			keyboardProc = KeyboardHookCallback;

			SetWindowsHooks();

			Documents = new Documents.DocumentCollection(paneVisibleButton: Globals.Ribbons.AddInMainRibbon.paneVisibleButton);

			try
			{
				Globals.Ribbons.AddInMainRibbon.LLMButtonUpdate();
			}
			catch (Exception) { }
		}

		private void ThisAddIn_Shutdown(object sender, EventArgs e)
		{
			UnhookWindowsHooks();

			Utils.Dialogs.CloseAllDialogs();

			Documents.Dispose();

			Properties.Settings.Default.MainSystemMessage = Properties.Settings.Default.MainSystemMessage;

			Properties.Settings.Default.LLMHostUri = Services.OpenAIService.Uri;
			Properties.Settings.Default.LLMName = Services.OpenAIService.LLMName;

			Properties.Settings.Default.LLMButton1 = Services.OpenAIService.CaptionButton1;
			Properties.Settings.Default.LLMSystemMessage1 = Services.OpenAIService.SystemMessageButton1;
			Properties.Settings.Default.LLMPrefixUserMessage1 = Services.OpenAIService.PrefixUserMessageButton1;
			Properties.Settings.Default.LLMPostfixUserMessage1 = Services.OpenAIService.PostfixUserMessageButton1;

			Properties.Settings.Default.LLMButton2 = Services.OpenAIService.CaptionButton2;
			Properties.Settings.Default.LLMSystemMessage2 = Services.OpenAIService.SystemMessageButton2;
			Properties.Settings.Default.LLMPrefixUserMessage2 = Services.OpenAIService.PrefixUserMessageButton2;
			Properties.Settings.Default.LLMPostfixUserMessage2 = Services.OpenAIService.PostfixUserMessageButton2;

			Properties.Settings.Default.Save();
		}

		#region Hooks

		//private SafeNativeMethods.HookProc mouseProc;
		private SafeNativeMethods.HookProc keyboardProc;

		//private IntPtr hookIdMouse;
		private IntPtr hookIdKeyboard;

		private void SetWindowsHooks()
		{
			uint threadId = (uint)SafeNativeMethods.GetCurrentThreadId();

			//hookIdMouse =
			//	SafeNativeMethods.SetWindowsHookEx(
			//		(int)SafeNativeMethods.HookType.WH_MOUSE,
			//		mouseProc,
			//		IntPtr.Zero,
			//		threadId);

			hookIdKeyboard =
				SafeNativeMethods.SetWindowsHookEx(
					(int)SafeNativeMethods.HookType.WH_KEYBOARD,
					keyboardProc,
					IntPtr.Zero,
					threadId);
		}

		private void UnhookWindowsHooks()
		{
			//SafeNativeMethods.UnhookWindowsHookEx(hookIdMouse);
			SafeNativeMethods.UnhookWindowsHookEx(hookIdKeyboard);
		}

		private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				var mouseHookStruct =
					(SafeNativeMethods.MouseHookStructEx)
						Marshal.PtrToStructure(lParam, typeof(SafeNativeMethods.MouseHookStructEx));

				// handle mouse message here
				var message = (SafeNativeMethods.WindowMessages)wParam;
				//Debug.WriteLine(
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
				Word.Range range;
				string systemPrompt, prompt;

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

			((Word.ApplicationEvents4_Event)Application).NewDocument += new Word.ApplicationEvents4_NewDocumentEventHandler(Application_NewDocument);
			Application.DocumentOpen += new Word.ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
			Application.DocumentBeforeClose += new Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentBeforeClose);
			Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);
		}
		
		private void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
		{
			Documents.Activate(Doc, Wn);
		}

		private void Application_NewDocument(Word.Document Doc)
		{
			Documents.Add(Doc);
		}

		private void Application_DocumentOpen(Word.Document Doc)
		{
			Documents.Add(Doc);
		}

		private void Application_DocumentBeforeClose(Word.Document Doc, ref bool Cancel)
		{
			Documents.Remove(Doc);
		}

		#endregion
	}
}
