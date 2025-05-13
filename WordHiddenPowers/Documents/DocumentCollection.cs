using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
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
			documents = new Dictionary<string, Document>();

			PaneVisibleButton = paneVisibleButton;
			PaneVisibleButton.Checked = false;
			PaneVisibleButton.Click += new RibbonControlEventHandler(PaneVisibleButtonClick);

			Globals.ThisAddIn.Application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Document_WindowSelectionChange);

			Globals.ThisAddIn.Application.CommandBars["Text"].Reset();

			buttonSelectTextCategory = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNote_Click);
			buttonSelectDecimalCategory = AddButtons(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNote_Click);
		}

		public Document ActiveDocument
		{
			get
			{
				return GetDocument(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveDocument);
			}
		}
		
		private void PaneVisibleButtonClick(object sender, RibbonControlEventArgs e)
		{
			RibbonToggleButton button = (RibbonToggleButton)sender;
			ActiveDocument.CustomPane.Visible = button.Checked;
		}

		private void Document_WindowSelectionChange(Word.Selection Sel)
		{			
			Office.CommandBarButton button = GetButton(Globals.ThisAddIn.Application.CommandBars["Text"], Const.Panes.BUTTON_STRING_TAG);
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

		private void AddDecimalNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void AddTextNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
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

		private Office.CommandBarButton AddButtons(Word.Application application, Office.CommandBar popupCommandBar, string caption, string idMso, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
		{
			Office.CommandBarButton commandBarButton = GetButton(popupCommandBar, tag);
			if (commandBarButton == null)
			{
				commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlButton);
				commandBarButton.Caption = caption;
				commandBarButton.Picture = application.CommandBars.GetImageMso(idMso, 16, 16);
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
		
		public void Activate(Word._Document Doc, Word.Window Wn)
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

		public void Add(Word._Document Doc)
		{
			string guid = Content.GetGuidOrDefault(Doc);
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

		public void Remove(Word._Document Doc)
		{
			string guid = Content.GetGuid(Doc);
			if (documents.ContainsKey(guid))
			{
				documents.Remove(guid);
			}
		}

		private Document GetDocument(Word.Application application, Word._Document Doc)
		{
			string guid = Content.GetGuidOrDefault(Doc);
			if (!documents.ContainsKey(guid))
			{
				documents.Add(guid, Document.Create(this, Doc.FullName, Doc));
			}

			List<string> closeDocs = (from Word._Document document in application.Documents
									  let id = Content.GetGuidOrDefault(Doc)
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
			Globals.ThisAddIn.Application.CommandBars["Text"].Reset();
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
