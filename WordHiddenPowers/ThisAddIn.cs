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
        const string valueName = "HiddenPower";
        const string powerPaneTitle = "Дополнительные данные";

        RepositoryDataSet powersDataSet = new RepositoryDataSet();

        CustomTaskPane _powerPane;

        public CustomTaskPane PowerPane { get { return _powerPane; } }

        public RepositoryDataSet PowersDataSet { get { return powersDataSet; } }


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            #region PowerPane

            Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked = false;

            if (PowerPane == null)
            {
                _powerPane = CustomTaskPanes.Add(new Panes.WordHiddenPowersPane(), powerPaneTitle);
                this.PowerPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                this.PowerPane.Width = 400;
                this.PowerPane.Visible = Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked;
                this.PowerPane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
            }           

            #endregion
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }


        void Pane_VisibleChanged(object sender, System.EventArgs e)
        {
            Microsoft.Office.Tools.CustomTaskPane pane;
            pane = (Microsoft.Office.Tools.CustomTaskPane)sender;
            if (pane.Title == powerPaneTitle)
            {
                Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked = pane.Visible;
            }            
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
        }

        private void Application_DocumentOpen(Word.Document Doc)
        {
            if (Doc.Variables.Count > 0)
            {
                Word.Variable variable = GetVariable(Doc.Variables);
                if (variable !=null)
                {
                    StringReader reader = new StringReader(variable.Value);
                    powersDataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
                    reader.Close();
                }
            }
            else
            {
                powersDataSet.Categories.Rows.Add(new object[] { 0, "Поручения Генерального прокурора Российской Федерации", "Категория о поручениях Генерального прокурора Российской Федерации" });
                StringBuilder builder = new StringBuilder();
                StringWriter writer = new StringWriter(builder);
                powersDataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
                Doc.Variables.Add(valueName, builder.ToString());
                writer.Close();
            }            
        }


        public Word.Variable GetVariable(Word.Variables array)
        {
            for (int i = 1; i <=  array.Count; i++)
            {
                if (array[i].Name == valueName)
                {
                    return array[i];
                }
            }
            return null;
        }

        #endregion
    }
}
