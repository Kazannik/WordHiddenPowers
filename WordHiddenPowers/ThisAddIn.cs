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
        public Documents.DocumentCollection Documents { get; private set; }


        //Panes.PaneCollection panes; 
                      
        //public Panes.PaneCollection Panes { get { return panes; } }
        
        //public Panes.WordHiddenPowersPane ActivePane
        //{
        //    get
        //    {
        //        if (panes.Contains(Application.ActiveDocument))
        //        {
        //            return (Panes.WordHiddenPowersPane) panes[Application.ActiveDocument].Control;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Documents = new Documents.DocumentCollection(Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton);
                      
            if (Application.Documents.Count > 0)
            {
                Documents.Activate(Application.ActiveDocument, Application.ActiveDocument.ActiveWindow);
            }          
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            Documents.Dispose();
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
            Documents.Activate(Doc, Wn);
        }

        private void Application_WindowDeactivate(Word.Document Doc, Word.Window Wn)
        {
            Documents.Deactivate(Doc, Wn);
        }
        
        private void Application_DocumentOpen(Word.Document Doc)
        {
            Documents.Open(Doc);           
        }
        
        private void Application_DocumentBeforeClose(Word.Document Doc, ref bool Cancel)
        {
            Documents.Remove(Doc);            
        }
        
        #endregion
    }
}
