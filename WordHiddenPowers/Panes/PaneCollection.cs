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
    public class PaneCollection: IDisposable
    {
        IDictionary<int, CustomTaskPane> panes;
        CustomTaskPaneCollection links;
        RibbonToggleButton button;

        public PaneCollection(CustomTaskPaneCollection collection, RibbonToggleButton button)
        {
            this.links = collection;
            this.button = button;
            this.panes = new Dictionary<int, CustomTaskPane>();
        }

        public CustomTaskPane ActivePane { get; private set; }

        public CustomTaskPane this[Word.Document Doc]
        {
            get
            {
                return panes[Doc.DocID];
            }
        }

        public bool Contains(Word.Document Doc)
        {
            return panes.ContainsKey(Doc.DocID);
        }

        public void WindowActivate(Word.Document Doc)
        {
            if (panes.ContainsKey(Doc.DocID))
            {
                ActivePane = panes[Doc.DocID];
            }
            else
            {
                ActivePane = links.Add(new WordHiddenPowersPane(Doc), Const.Panes.PANE_TITLE);
                panes.Add(Doc.DocID, ActivePane);
                ActivePane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                ActivePane.Width = 400;
           }
            ActivePane.Visible = button.Checked;
            ActivePane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
        }

        public void WindowDeactivate(Word.Document Doc)
        {
            if (panes.ContainsKey(Doc.DocID))
            {
                panes[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;
            }               
        }

        public void Remove(Word.Document Doc)
        {
            if (panes.ContainsKey(Doc.DocID))
            {
                panes[Doc.DocID].VisibleChanged -= Pane_VisibleChanged;
                links.Remove(panes[Doc.DocID]);
                panes.Remove(Doc.DocID);
            }           
        }


        void Pane_VisibleChanged(object sender, EventArgs e)
        {
            CustomTaskPane pane = (CustomTaskPane)sender;
            bool visible = pane.Visible;
            foreach (CustomTaskPane item in panes.Values)
            {
                if (!pane.Equals(item))
                {
                    item.Visible = visible;
                }                
            }            
            button.Checked = pane.Visible;
        }

                
        public void Dispose()
        {
            foreach (CustomTaskPane item in panes.Values)
            {
                item.Dispose();                
            }
            panes.Clear();
            panes = null;
        }
    }
}
