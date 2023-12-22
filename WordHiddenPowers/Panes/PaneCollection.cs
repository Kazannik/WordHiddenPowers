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
    public class PaneCollection
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


        public void WindowActivate(Word.Document Doc)
        {
            if (panes.ContainsKey(Doc.DocID))
            {
                ActivePane = panes[Doc.DocID];
            }
            else
            {
                ActivePane = links.Add(new WordHiddenPowersPane(Doc), Globals.ThisAddIn.PaneTitle);
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
    }
}
