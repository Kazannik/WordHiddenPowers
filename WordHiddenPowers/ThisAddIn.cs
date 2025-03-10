using System;
using System.Security.AccessControl;
using WordHiddenPowers.Panes;
using Word = Microsoft.Office.Interop.Word;
using Tools = Microsoft.Office.Tools;

namespace WordHiddenPowers
{
	public partial class ThisAddIn
	{
		public Documents.DocumentCollection Documents { get; private set; }

		private void ThisAddIn_Startup(object sender, EventArgs e)
		{
			Properties.Settings.Default.Reload();
			Documents = new Documents.DocumentCollection(Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton);
		}

		private void ThisAddIn_Shutdown(object sender, EventArgs e)
		{
			Documents.Dispose();
		}

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
