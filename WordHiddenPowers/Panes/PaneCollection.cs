using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;

namespace WordHiddenPowers.Panes
{
    public class PaneCollection: Dictionary<int, CustomTaskPane>, IDisposable
    {
        CustomTaskPaneCollection links;        
        RibbonToggleButton buttonVisible;

        Office.CommandBarButton buttonSelectDecimalCategory;
        Office.CommandBarButton buttonSelectTextCategory;

        public PaneCollection(CustomTaskPaneCollection collection, RibbonToggleButton button)
        {
            links = collection;
            buttonVisible = button;
            
            Word.Application application = Globals.ThisAddIn.Application as Word.Application;
            application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(ApplicationObject_WindowSelectionChange);

            application.CommandBars["Text"].Reset();

            buttonSelectTextCategory = AddButtons(application.CommandBars["Text"], Const.Content.TEXT_NOTE_MENU_CAPTION, Const.Content.TEXT_NOTE_FACE_ID, Const.Panes.BUTTON_STRING_TAG, true, AddTextNoteClick);
            buttonSelectDecimalCategory = AddButtons(application.CommandBars["Text"], Const.Content.DECIMAL_NOTE_MENU_CAPTION, Const.Content.DECIMAL_NOTE_FACE_ID, Const.Panes.BUTTON_DECIMAL_TAG, false, AddDecimalNoteClick);   


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

        public CustomTaskPane GetPane(Word.Document Doc)
        {
            if (ContainsKey(Doc.DocID))
            {
                return base[Doc.DocID];
            }
            else
            {
                CustomTaskPane pane = links.Add(new NotesPane(Doc), Const.Panes.PANE_TITLE);
                Add(Doc.DocID, pane);
                pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                pane.Width = 400;
                return pane;
           }
        }


        public void WindowActivate(Word.Document Doc)
        {
            ActivePane = GetPane(Doc);           
            ActivePane.Visible = buttonVisible.Checked;
            ActivePane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
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
                pane.CommitVariables();

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
        
        private void ApplicationObject_WindowSelectionChange(Word.Selection Sel)
        {
            Word.Application application = Globals.ThisAddIn.Application as Word.Application;
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
            if (ActivePane != null)
            {
                NotesPane pane = ActivePane.Control as NotesPane;
                pane.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
            }
        }

        private void AddTextNoteClick(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (ActivePane != null)
            {
                NotesPane pane = ActivePane.Control as NotesPane;
                pane.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
            }               
        }
                
        private Office.CommandBarButton AddButtons(Office.CommandBar popupCommandBar, string caption, int faceId, string tag, bool beginGroup, Office._CommandBarButtonEvents_ClickEventHandler clickFunctionDelegate)
        {
            var commandBarButton = GetButton(popupCommandBar, tag);
            if (commandBarButton == null)
            {
                commandBarButton = (Office.CommandBarButton)popupCommandBar.Controls.Add
                    (Office.MsoControlType.msoControlButton);
                commandBarButton.Caption = caption;
                commandBarButton.FaceId = faceId;
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
