using Microsoft.Office.Tools;
using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Data;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using WordHiddenPowers.Dialogs;
using System.Windows.Forms;
using WordHiddenPowers.Utils;
using System.Collections.Generic;

namespace WordHiddenPowers.Documents
{
    public class Document: IDisposable
    {
        private DocumentCollection parent;

        private RepositoryDataSet dataSet;

        private CustomTaskPane pane;

        private RepositoryDataSet importDataSet;

        public CustomTaskPane CustomPane
        {
            get
            {
                if (pane == null)
                {
                    pane = Globals.ThisAddIn.CustomTaskPanes.Add(new NotesPane(this), Const.Panes.PANE_TITLE);
                    pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                    pane.Width = 400;
                    pane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
                    NotesPane note = pane.Control as NotesPane;
                    note.PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);
                }
                return pane;
            }
        }

        private void NotesPane_PropertiesChanged(object sender, EventArgs e)
        {
            if (Caption != Pane.Caption) Caption = Pane.Caption;
            if (Date != Pane.Date) Date = Pane.Date;
            if (Description != Pane.Description) Description = Pane.Description;
            if (DataSet.HasChanges())
            {
                HiddenPowerDocument.CommitVariable(array: Doc.Variables, variableName: Const.Globals.XML_VARIABLE_NAME, dataSet: DataSet);
            }
        }

        public NotesPane Pane
        {
            get
            {                
                return CustomPane.Control as NotesPane;
            }           
        }

        public bool HasChanges { get; }

        public string FileName { get; }

        public string Caption
        {
            get
            {
                return HiddenPowerDocument.GetCaption(Doc);                
            } 
            set
            {
                HiddenPowerDocument.CommitVariable(array: Doc.Variables, variableName: Const.Globals.CAPTION_VARIABLE_NAME, value: value);
            }
        }

        public DateTime Date
        {
            get
            {
                return HiddenPowerDocument.GetDate(Doc);        
            }
            set
            {
                HiddenPowerDocument.CommitVariable(array: Doc.Variables, variableName: Const.Globals.DATE_VARIABLE_NAME, value: value.ToShortDateString());
            }
        }

        public string Description
        {
            get
            {
                return HiddenPowerDocument.GetDescription(Doc);                
            }
            set
            {
                HiddenPowerDocument.CommitVariable(array: Doc.Variables, variableName: Const.Globals.DESCRIPTION_VARIABLE_NAME, value: value);
            }
        }

        public Table Table
        {
            get
            {
                return HiddenPowerDocument.GetTable(Doc);               
            }
            set
            {
                HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, value: value.ToString());
            }
        }

        public bool ContentHide { get; set; }

        public RepositoryDataSet DataSet
        {
            get
            {
                if (dataSet == null)
                {
                    dataSet = new RepositoryDataSet();
                    Word.Variable content = HiddenPowerDocument.GetVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
                    if (content != null)
                    {
                        StringReader reader = new StringReader(content.Value);
                        dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                        reader.Close();                        
                    }
                    dataSet.AcceptChanges();
                }
                return dataSet;
            }
        }

        public Word._Document Doc { get; }
                
        private Document(DocumentCollection parent, string fileName, Word._Document doc)
        {
            this.parent = parent;
            dialogs = new List<Form>();
            FileName = fileName;
            ContentHide = false;
            Doc = doc;
        }

        void Pane_VisibleChanged(object sender, EventArgs e)
        {
            CustomTaskPane pane = (CustomTaskPane)sender;
            parent.paneVisibleButton.Checked = pane.Visible;           
        }

        public static Document Create(DocumentCollection parent, string fileName, Word._Document Doc)
        {
            Document document = new Document(parent: parent, fileName: fileName, doc: Doc);
            return document;
        }
               
        public void NewData()
        {
            DataSet.RowsHeaders.Clear();
            DataSet.ColumnsHeaders.Clear();
            DataSet.Categories.Clear();
            DataSet.Subcategories.Clear();
            DataSet.DecimalPowers.Clear();
            DataSet.TextPowers.Clear();

            CommitVariables();
        }

        public void LoadData(string fileName)
        {
            try
            {
                DataSet.RowsHeaders.Clear();
                DataSet.ColumnsHeaders.Clear();
                DataSet.Categories.Clear();
                DataSet.Subcategories.Clear();
                DataSet.DocumentKeys.Clear();

                DataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

                DataSet.DecimalPowers.Clear();
                DataSet.TextPowers.Clear();

                CommitVariables();
            }
            catch (Exception ex)
            {
                ShowDialogUtil.ShowErrorDialog(ex.Message);
            }
        }

        public void SaveData(string fileName)
        {
            Xml.SaveData(Globals.ThisAddIn.Documents.ActiveDocument.DataSet, fileName);           
        }
               

        public void CommitVariables()
        {
            HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME, Caption);
            HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
            HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
            HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

            if (DataSet.HasChanges())
            {                
                HiddenPowerDocument.CommitVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME, DataSet);
            }
        }
           

                
        public bool VariablesExists()
        {
            if (Doc.Variables.Count > 0)
            {
                Word.Variable caption = HiddenPowerDocument.GetVariable(Doc.Variables,
                    Const.Globals.CAPTION_VARIABLE_NAME);
                if (caption != null)
                {
                    return true;
                }

                Word.Variable date = HiddenPowerDocument.GetVariable(Doc.Variables,
                    Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = HiddenPowerDocument.GetVariable(Doc.Variables,
                    Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return true;
                }

                Word.Variable table = HiddenPowerDocument.GetVariable(Doc.Variables,
                    Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return true;
                }

                Word.Variable content = HiddenPowerDocument.GetVariable(Doc.Variables,
                    Const.Globals.XML_VARIABLE_NAME);
                if (content != null)
                {
                    return true;
                }               
            }
            return false;
        }

        public void DeleteVariables()
        {
            foreach (DataTable table in DataSet.Tables)
            {
                table.Clear();
            }

            if (Doc.Variables.Count > 0)
            {
                HiddenPowerDocument.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
                HiddenPowerDocument.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
                HiddenPowerDocument.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                HiddenPowerDocument.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                HiddenPowerDocument.DeleteVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
            }
        }

       
        protected IList<Form> dialogs = null;

        public void ShowDocumentKeysDialog()
        {
            Form dialog = new DocumentKeysDialog(DataSet);
            dialogs.Add(dialog);
            ShowDialogUtil.ShowDialog(dialog);
            //OnPropertiesChanged(new EventArgs());
        }

        public void ShowEditCategoriesDialog()
        {
            Form dialog = new CategoriesEditorDialog(this);
            dialogs.Add(dialog);
            if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
            {
                Pane.DataSetRefresh();
            }
        }

        public void ShowCreateTableDialog()
        {
            Form dialog = new CreateTableDialog(this);
            dialogs.Add(dialog);
            ShowDialogUtil.ShowDialog(dialog);
            //OnPropertiesChanged(new EventArgs());
        }

        public void ShowEditTableDialog()
        {
            Form dialog = new TableEditorDialog(this);
            dialogs.Add(dialog);
            ShowDialogUtil.ShowDialog(dialog);
            //OnPropertiesChanged(new EventArgs());
        }


        public void ImportDataFromWordDocuments()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
            {
                importDataSet = FileSystem.ImportFiles(dialog.SelectedPath);
            }
        }

        public void ShowTableViewerDialog()
        {
            TableViewerDialog dialog = new TableViewerDialog(importDataSet);
            dialogs.Add(dialog);
            ShowDialogUtil.ShowDialog(dialog);
        }

        public void ShowAnalizerDialog()
        {

        }

       

        public void AddTextNote(Word.Selection selection)
        {
            TextNoteDialog dialog = new TextNoteDialog(DataSet, selection);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataSet.TextPowers.Rows.Add(new object[]
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }
        }

        public void AddDecimalNote(Word.Selection selection)
        {
            DecimalNoteDialog dialog = new DecimalNoteDialog(DataSet, selection);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataSet.DecimalPowers.Rows.Add(new object[]
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                CommitVariables();
            }
        }

        public void Dispose()
        {
            if (dialogs != null)
            {
                foreach (Form form in dialogs)
                {
                    if (form != null)
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                dialogs.Clear();
            }
        }
    }
}
