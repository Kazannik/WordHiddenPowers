using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
	public class PaneCollection2 : Dictionary<int, CustomTaskPane>, IDisposable
	{
		private readonly CustomTaskPaneCollection links;
		private readonly RibbonToggleButton buttonVisible;

		public PaneCollection2(CustomTaskPaneCollection collection, RibbonToggleButton button)
		{
			links = collection;
			buttonVisible = button;
		}

		public CustomTaskPane ActivePane { get; private set; }

		public CustomTaskPane this[Word.Document Doc]
		{
			get
			{
				return base[Doc.DocID];
			}
		}

		public bool Contains(Word.Document Doc)
		{
			return ContainsKey(Doc.DocID);
		}


		public void WindowDeactivate(Word.Document Doc)
		{
			if (ContainsKey(Doc.DocID))
			{
				base[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;
			}
		}

		public void Remove(Word.Document Doc)
		{
			if (ContainsKey(Doc.DocID))
			{
				NotesPane pane = base[Doc.DocID].Control as NotesPane;
				base[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;
				links.Remove(base[Doc.DocID]);
				Remove(Doc.DocID);
			}
		}

		void Pane_VisibleChanged(object sender, EventArgs e)
		{
			CustomTaskPane pane = (CustomTaskPane)sender;
			bool visible = pane.Visible;
			foreach (CustomTaskPane item in Values)
			{
				if (!pane.Equals(item))
				{
					item.Visible = visible;
				}
			}
			buttonVisible.Checked = pane.Visible;
		}



		public void Dispose()
		{
			Word.Application application = Globals.ThisAddIn.Application as Word.Application;
			application.CommandBars["Text"].Reset();
			foreach (CustomTaskPane item in Values)
			{
				item.Dispose();
			}
		}
	}
}
