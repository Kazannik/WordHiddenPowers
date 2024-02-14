using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordHiddenPowers.Repositoryes;
using System.IO;
using Microsoft.Office.Tools;

namespace WordHiddenPowers
{
    public partial class ThisAddIn
    {
        Documents.DocumentCollection documents;


        Panes.PaneCollection panes; 
                      
        public Panes.PaneCollection Panes { get { return panes; } }
        
        public Panes.WordHiddenPowersPane ActivePane
        {
            get
            {
                if (panes.Contains(Application.ActiveDocument))
                {
                    return (Panes.WordHiddenPowersPane) panes[Application.ActiveDocument].Control;
                }
                else
                {
                    return null;
                }
            }
        }


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            #region PowerPane

            panes = new Panes.PaneCollection(CustomTaskPanes, Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton);

            #endregion

            if (Application.Documents.Count > 0)
            {
                panes.WindowActivate(Application.ActiveDocument);
                Panes.NotesPane pane = (Panes.NotesPane)panes.ActivePane.Control;
                pane.InitializeVariables();
            }          
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            panes.Dispose();
        }
                
        #region Код, автоматически созданный VSTO

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            Startup += new EventHandler(ThisAddIn_Startup);
            Shutdown += new EventHandler(ThisAddIn_Shutdown);
            Application.DocumentOpen += new Word.ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
            Application.DocumentBeforeClose += new Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentBeforeClose);
            Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);
            Application.WindowDeactivate +=new Word.ApplicationEvents4_WindowDeactivateEventHandler(Application_WindowDeactivate);
        }
        
        private void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
        {
            panes.WindowActivate(Doc);
        }

        private void Application_WindowDeactivate(Word.Document Doc, Word.Window Wn)
        {
            panes.WindowDeactivate(Doc);
        }


        private void Application_DocumentOpen(Word.Document Doc)
        {
            if (panes.Count > 0)
            {
                Panes.NotesPane pane = (Panes.NotesPane)panes.ActivePane.Control;
                pane.InitializeVariables();
            }
        }


        private void Application_DocumentBeforeClose(Word.Document Doc, ref bool Cancel)
        {
            panes.Remove(Doc);            
        }

        
        #endregion
    }
}
