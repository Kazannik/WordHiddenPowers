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
        Office.CommandBarButton buttonSelectStringCategory;

        public PaneCollection(CustomTaskPaneCollection collection, RibbonToggleButton button)
        {
            this.links = collection;
            this.buttonVisible = button;
            
            Word.Application application = Globals.ThisAddIn.Application as Word.Application;
            application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(ApplicationObject_WindowSelectionChange);

            application.CommandBars["Text"].Reset();

            buttonSelectStringCategory = AddButtons(application.CommandBars["Text"], "Текстовые дополнительные данные...", 9267, Const.Panes.BUTTON_STRING_TAG, true);
            buttonSelectDecimalCategory = AddButtons(application.CommandBars["Text"], "Числовые дополнительные данные...", 9267, Const.Panes.BUTTON_DECIMAL_TAG, false);
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
            return base.ContainsKey(Doc.DocID);
        }

        public void WindowActivate(Word.Document Doc)
        {
            if (ContainsKey(Doc.DocID))
            {
                ActivePane = base[Doc.DocID];
            }
            else
            {
                ActivePane = links.Add(new WordHiddenPowersPane(Doc), Const.Panes.PANE_TITLE);
                Add(Doc.DocID, ActivePane);
                ActivePane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                ActivePane.Width = 400;
           }
            ActivePane.Visible = buttonVisible.Checked;
            ActivePane.VisibleChanged += new EventHandler(Pane_VisibleChanged);

            WordHiddenPowersPane pane = ActivePane.Control as WordHiddenPowersPane;
            
            buttonSelectDecimalCategory.Click += new Office._CommandBarButtonEvents_ClickEventHandler(pane.DecimalCategoryDelegate);
            buttonSelectStringCategory.Click += new Office._CommandBarButtonEvents_ClickEventHandler(pane.StringCategoryDelegate);
        }

        public void WindowDeactivate(Word.Document Doc)
        {
            if (ContainsKey(Doc.DocID))
            {
                base[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;

                WordHiddenPowersPane pane = base[Doc.DocID].Control as WordHiddenPowersPane;
                buttonSelectDecimalCategory.Click -= pane.DecimalCategoryDelegate;
                buttonSelectStringCategory.Click -= pane.StringCategoryDelegate;
            }               
        }

        public void Remove(Word.Document Doc)
        {
            if (ContainsKey(Doc.DocID))
            {
                base[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;
                links.Remove(base[Doc.DocID]);
                base.Remove(Doc.DocID);
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
                if (Sel.Text.Length > 1)
                {
                    button.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                }
            }            
        }

        private Office.CommandBarButton AddButtons(Office.CommandBar popupCommandBar, string caption, int faceId, string tag, bool beginGroup)
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
            foreach (CustomTaskPane item in Values)
            {
                item.Dispose();                
            }               
        }
    }
}
