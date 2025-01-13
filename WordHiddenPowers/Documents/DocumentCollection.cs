using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		private readonly Word.Application application;

		internal RibbonToggleButton paneVisibleButton;
		private readonly Office.CommandBarButton buttonSelectDecimalCategory;
		private readonly Office.CommandBarButton buttonSelectTextCategory;

		private readonly IDictionary<int, Document> documents;

		public DocumentCollection(RibbonToggleButton paneVisibleButton)
		{
			application = Globals.ThisAddIn.Application as Word.Application;
			documents = new Dictionary<int, Document>();

			this.paneVisibleButton = paneVisibleButton;
			this.paneVisibleButton.Checked = false;
			this.paneVisibleButton.Click += new RibbonControlEventHandler(PaneVisibleButtonClick);

			application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Document_WindowSelectionChange);

			application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButtons(application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_FACE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNoteClick);
			buttonSelectDecimalCategory = AddButtons(application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_FACE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNoteClick);
		}

		public Document ActiveDocument
		{
			get
			{
				return GetDocument(application.ActiveDocument);
			}
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
			ActiveDocument?.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
		}

		private void AddTextNoteClick(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			ActiveDocument?.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
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
			paneVisibleButton.Checked = ActiveDocument.CustomPane.Visible;
		}

		public void Deactivate(Word.Document Doc, Word.Window Wn)
		{

		}

		public void Open(Word.Document Doc)
		{
			if (!documents.ContainsKey(Doc.DocID))
			{
				documents.Add(Doc.DocID, Document.Create(this, Doc.FullName, Doc));
			}
		}

		public void Remove(Word.Document Doc)
		{
			if (documents.ContainsKey(Doc.DocID))
			{
				documents.Remove(Doc.DocID);
			}
		}

		private Document GetDocument(Word.Document Doc)
		{
			if (!documents.ContainsKey(Doc.DocID))
			{
				documents.Add(Doc.DocID, Document.Create(this, Doc.FullName, Doc));
			}

			List<int> closeDocs = new List<int>();
			foreach (Word.Document document in application.Documents)
			{
				if (!documents.ContainsKey(document.DocID))
					closeDocs.Add(document.DocID);
			}

			foreach (int id in closeDocs)
			{
				if (documents.ContainsKey(id))
				{
					Document document = documents[id];
					document.Dispose();
					documents.Remove(id);
				}
			}
			return documents[Doc.DocID];
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
