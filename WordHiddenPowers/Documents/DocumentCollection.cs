
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.EventsBus.EventArgs;
using Tools = Microsoft.Office.Tools;
using Word = Microsoft.Office.Interop.Word;

#if WORD

namespace WordHiddenPowers.Documents
#else
using Content = ProsecutorialSupervision.Utils.WordDocuments.Content;

namespace ProsecutorialSupervision.Documents
#endif
{
	public partial class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		private readonly IDictionary<int, Document> documents;

		public DocumentCollection(Tools.Ribbon.RibbonToggleButton paneVisibleButton)
		{
			documents = new Dictionary<int, Document>();

			GlobalsEventsBus.DocumentBeforeClose += new EventHandler<WordDocumentBeforeCloseEventArgs>(GlobalsEventsBus_DocunentBeforeClose);
			GlobalsEventsBus.DocumentOpen += new EventHandler<WordDocumentEventArgs>(GlobalsEventsBus_DocumentOpen);
			GlobalsEventsBus.NewDocument += new EventHandler<WordDocumentEventArgs>(GlobalsEventsBus_NewDocument);
		}

		private void GlobalsEventsBus_NewDocument(object sender, WordDocumentEventArgs e)
		{
			Add(e.Document);
		}

		private void GlobalsEventsBus_DocunentBeforeClose(object sender, WordDocumentBeforeCloseEventArgs e)
		{
			int hwnd = e.Document.Windows[1].Hwnd;

			Debug.WriteLine(string.Format("Remove: {0}", hwnd));

			if (documents.ContainsKey(hwnd))
			{
				Tools.CustomTaskPane pane = documents[hwnd].CustomPane;
				documents.Remove(hwnd);
				Globals.ThisAddIn.CustomTaskPanes.Remove(pane);
			}
		}

		private void GlobalsEventsBus_DocumentOpen(object sender, WordDocumentEventArgs e)
		{
			Add(e.Document);
		}

		private void Add(Word._Document Doc)
		{
			int hwnd = Doc.Windows[1].Hwnd;

			Debug.WriteLine(string.Format("Add: {0}", hwnd));


			if (!documents.ContainsKey(hwnd))
			{
				documents.Add(hwnd, Document.Create(this, Doc.FullName, Doc));
			}

			GlobalsEventsBus.DoDocumentPropertiesChanged(documents[hwnd]);
		}

		public Document ActiveDocument => GetDocument(Globals.ThisAddIn.Application.Documents.Count > 0 ? Globals.ThisAddIn.Application.ActiveDocument : null);

		private readonly object _lock = new();
		public Document GetDocument(Word._Document Doc)
		{
			if (Doc is null) return null;

			lock (_lock)
			{
				int hwnd = Doc.Windows[1].Hwnd;

				if (!documents.ContainsKey(hwnd))
				{
					documents.Add(hwnd, Document.Create(this, Doc.FullName, Doc));
				}
				return documents[hwnd];
			}
		}

		public void Dispose()
		{
			documents.Clear();
		}

		public IEnumerator<Document> GetEnumerator() => documents.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
