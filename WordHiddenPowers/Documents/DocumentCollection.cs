using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Office = Microsoft.Office.Core;
using Tools = Microsoft.Office.Tools;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		internal RibbonToggleButton PaneVisibleButton;

		private readonly Office.CommandBarButton buttonSelectDecimalCategory;
		private readonly Office.CommandBarButton buttonSelectTextCategory;

		private readonly IDictionary<string, Document> documents;

		public DocumentCollection(RibbonToggleButton paneVisibleButton)
		{
			Word.Application application = Globals.ThisAddIn.Application;
			documents = new Dictionary<string, Document>();

			this.PaneVisibleButton = paneVisibleButton;
			this.PaneVisibleButton.Checked = false;
			this.PaneVisibleButton.Click += new RibbonControlEventHandler(PaneVisibleButtonClick);

			application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Document_WindowSelectionChange);

			application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButtons(application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNoteClick);
			buttonSelectDecimalCategory = AddButtons(application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_FACE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNoteClick);
		}

		public Document ActiveDocument
		{
			get
			{
				return GetDocument(Globals.ThisAddIn.Application.ActiveDocument);
			}
		}

		private Word.Selection Selection
		{
			get { return Globals.ThisAddIn.Application.ActiveWindow?.Selection; }
		}

		private void PaneVisibleButtonClick(object sender, RibbonControlEventArgs e)
		{
			RibbonToggleButton button = (RibbonToggleButton)sender;
			ActiveDocument.CustomPane.Visible = button.Checked;
		}

		private void Document_WindowSelectionChange(Word.Selection Sel)
		{
			Word.Application application = Globals.ThisAddIn.Application;
			Office.CommandBarButton button = GetButton(application.CommandBars["Text"], Const.Panes.BUTTON_STRING_TAG);
			if (button != null)
			{
				if (Sel != null &&
					!string.IsNullOrWhiteSpace(Sel.Text))
				{
					button.Enabled = true;
				}
				else
				{
					button.Enabled = false;
				}
			}
		}

		private void AddDecimalNoteClick(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Selection != null) ActiveDocument.AddDecimalNote(Selection);
		}

		private void AddTextNoteClick(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Selection !=null) ActiveDocument.AddTextNote(Selection);
		}

		private Office.CommandBarButton AddButtons(Office.CommandBar popupCommandBar, string caption, int faceId, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
		{
			Office.CommandBarButton commandBarButton = GetButton(popupCommandBar, tag);
			if (commandBarButton == null)
			{
				commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlButton);
				commandBarButton.Caption = caption;
				commandBarButton.FaceId = faceId;
				commandBarButton.Tag = tag;
				commandBarButton.BeginGroup = beginGroup;
				commandBarButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(clickFunctionDelegate);
			}
			return commandBarButton;
		}

		private Office.CommandBarButton AddButtons(Office.CommandBar popupCommandBar, string caption, string idMso, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
		{
			Office.CommandBarButton commandBarButton = GetButton(popupCommandBar, tag);
			if (commandBarButton == null)
			{
				commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlButton);
				commandBarButton.Caption = caption;
				commandBarButton.Picture = Globals.ThisAddIn.Application.CommandBars.GetImageMso(idMso, 16, 16);
				commandBarButton.Tag = tag;
				commandBarButton.BeginGroup = beginGroup;
				commandBarButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(clickFunctionDelegate);
			}
			return commandBarButton;
		}

		private Office.CommandBarButton GetButton(Office.CommandBar popupCommandBar, string tag)
		{
			foreach (var commandBarButton in popupCommandBar.Controls.OfType<Office.CommandBarButton>())
			{
				if (commandBarButton.Tag.Equals(tag))
				{
					return commandBarButton;
				}
			}
			return null;
		}

		#region Documents Command
		
		public void Activate(Word.Document Doc, Word.Window Wn)
		{
			List<Tools.CustomTaskPane> panes = new List<Tools.CustomTaskPane>(Globals.ThisAddIn.CustomTaskPanes
				.Where(pane => pane.Window == Wn && ((Panes.WordHiddenPowersPane)pane.Control).Document.Doc != Doc));

			foreach (Tools.CustomTaskPane pane in panes)
			{
				Globals.ThisAddIn.CustomTaskPanes.Remove(pane);
			}			
			
			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				if (pane.Window == Wn)
				{
					PaneVisibleButton.Checked = pane.Visible;
				}
			}
		}

		public void Add(Word.Document Doc)
		{
			string guid = Utils.ContentUtil.GetGuidOrDefault(Doc);
			if (!documents.ContainsKey(guid))
			{
				documents.Add(guid, Document.Create(this, Doc.FullName, Doc));
			}

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				pane.Visible = false;
			}

			foreach (Tools.CustomTaskPane pane in Globals.ThisAddIn.CustomTaskPanes)
			{
				pane.Visible = PaneVisibleButton.Checked;
			}

		}

		public void Remove(Word.Document Doc)
		{
			string guid = Utils.ContentUtil.GetGuid(Doc);
			if (documents.ContainsKey(guid))
			{
				documents.Remove(guid);
			}
		}

		private Document GetDocument(Word.Document Doc)
		{
			string guid = Utils.ContentUtil.GetGuidOrDefault(Doc);
			if (!documents.ContainsKey(guid))
			{
				documents.Add(guid, Document.Create(this, Doc.FullName, Doc));
			}

			List<string> closeDocs = (from Word.Document document in Globals.ThisAddIn.Application.Documents
									  let id = Utils.ContentUtil.GetGuidOrDefault(Doc)
									  where !documents.ContainsKey(id)
									  select id).ToList();

			foreach (string id in closeDocs)
			{
				if (documents.ContainsKey(id))
				{
					Document document = documents[id];
					document.Dispose();
					documents.Remove(id);
				}
			}
			return documents[guid];
		}

		#endregion

		public void Dispose()
		{
			Word.Application application = Globals.ThisAddIn.Application as Word.Application;
			application.CommandBars["Text"].Reset();
		}

		public IEnumerator<Document> GetEnumerator()
		{
			return documents.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
