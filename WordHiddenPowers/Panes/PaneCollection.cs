using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;

namespace WordHiddenPowers.Panes
{
    public class PaneCollection: Dictionary<int, CustomTaskPane>, IDisposable
    {
        CustomTaskPaneCollection links;
        RibbonToggleButton buttonVisible;

        public PaneCollection(CustomTaskPaneCollection collection, RibbonToggleButton button)
        {
            this.links = collection;
            this.buttonVisible = button;
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

                
        public void Dispose()
        {
            foreach (CustomTaskPane item in Values)
            {
                item.Dispose();                
            }
        }
    }
}
