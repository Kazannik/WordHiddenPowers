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
        const string PANE_TITLE = "Дополнительные данные";

        const string titleValueName = "TitleHiddenPower";
        const string descriptionValueName = "DescriptionHiddenPower";
        const string dateValueName = "DateHiddenPower";
        const string TABLE_VARIABLE_NAME = "TableHiddenPower";

        Panes.PaneCollection panes; 

        public string PaneTitle { get { return PANE_TITLE; } }

        public string TableVariableName { get { return TABLE_VARIABLE_NAME; } }

        RepositoryDataSet powersDataSet = new RepositoryDataSet();

        public RepositoryDataSet PowersDataSet { get { return powersDataSet; } }

        public Panes.PaneCollection Panes { get { return panes; } }


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            #region PowerPane

            panes = new Panes.PaneCollection(CustomTaskPanes, Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton);
                        
            #endregion
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
                
        #region Код, автоматически созданный VSTO

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
            this.Application.DocumentOpen += new Word.ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
            this.Application.DocumentBeforeClose += new Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentBeforeClose);
            this.Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);
            this.Application.WindowDeactivate +=new Word.ApplicationEvents4_WindowDeactivateEventHandler(Application_WindowDeactivate);
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
            if (Doc.Variables.Count > 0)
            {
                Word.Variable variable = GetVariable(Doc.Variables, TABLE_VARIABLE_NAME);
                if (variable !=null)
                {
                    StringReader reader = new StringReader(variable.Value);
                    powersDataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
                    reader.Close();
                }
            }
            else
            {
                //powersDataSet.Categories.Rows.Add(new object[] { 0, "Поручения Генерального прокурора Российской Федерации", "Категория о поручениях Генерального прокурора Российской Федерации" });
                //StringBuilder builder = new StringBuilder();
                //StringWriter writer = new StringWriter(builder);
                //powersDataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
                //Doc.Variables.Add(TABLE_VARIABLE_NAME, builder.ToString());
                //writer.Close();
            }            
        }


        private void Application_DocumentBeforeClose(Word.Document Doc, ref bool Cancel)
        {
            panes.Remove(Doc);            
        }

        public Word.Variable GetVariable(Word.Variables array, string variableName)
        {
            for (int i = 1; i <=  array.Count; i++)
            {
                if (array[i].Name == variableName)
                {
                    return array[i];
                }
            }
            return null;
        }

        #endregion
    }
}
