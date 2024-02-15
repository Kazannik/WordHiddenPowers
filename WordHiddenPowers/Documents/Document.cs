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
                StringBuilder builder = new StringBuilder();
                StringWriter writer = new StringWriter(builder);
                DataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
                writer.Close();
                CommitVariables(Const.Globals.XML_VARIABLE_NAME, builder.ToString());
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
                Word.Variable caption = GetVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
                if (caption != null)
                {
                    return caption.Value;
                }
                else
                {
                    return string.Empty;
                }
            } 
            set
            {
                CommitVariables(Const.Globals.CAPTION_VARIABLE_NAME, value: value);
            }
        }

        public DateTime Date
        {
            get
            {
                Word.Variable date = GetVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return DateTime.Parse(date.Value);
                }
                else
                {
                    return DateTime.Today;
                }
            }
            set
            {
                CommitVariables(Const.Globals.DATE_VARIABLE_NAME, value: value.ToShortDateString());
            }
        }

        public string Description
        {
            get
            {
                Word.Variable description = GetVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return description.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                CommitVariables(Const.Globals.DESCRIPTION_VARIABLE_NAME, value: value);
            }
        }

        public Table Table
        {
            get
            {
                Word.Variable table = GetVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return Table.Create(table.Value);
                }
                else
                {
                    return Table.Create(string.Empty);
                }
            }
            set
            {
                CommitVariables(Const.Globals.TABLE_VARIABLE_NAME, value: value.ToString());
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
                    Word.Variable content = GetVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
                    if (content != null)
                    {
                        StringReader reader = new StringReader(content.Value);
                        dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                        reader.Close();
                    }
                }
                return dataSet;
            }
        }

        public Word._Document Doc { get; }
                
        private Document(DocumentCollection parent, string fileName, Word._Document doc)
        {
            this.parent = parent;
            dataSet = new RepositoryDataSet();
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

        private static Word.Variable GetVariable(Word.Variables array, string variableName)
        {
            for (int i = 1; i <= array.Count; i++)
            {
                if (array[i].Name == variableName)
                {
                    return array[i];
                }
            }
            return null;
        }

        public string PowersDataSetToXml()
        {
            string xml = GetXml(DataSet);
            return xml;
        }

        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
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

        public void CommitVariables()
        {
            CommitVariables(Const.Globals.CAPTION_VARIABLE_NAME, Caption);
            CommitVariables(Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
            CommitVariables(Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
            CommitVariables(Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

            if (DataSet.HasChanges())
            {
                StringBuilder builder = new StringBuilder();
                StringWriter writer = new StringWriter(builder);
                DataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
                writer.Close();
                CommitVariables(Const.Globals.XML_VARIABLE_NAME, builder.ToString());
            }
        }
           

        protected void CommitVariables(string name, string value)
        {
            Word.Variable variable = GetVariable(Doc.Variables, name);
            if (variable == null && !string.IsNullOrWhiteSpace(value))
                Doc.Variables.Add(name, value);
            else if (variable != null && variable.Value != value)
                variable.Value = value;
        }


                
       




        public bool VariablesExists()
        {
            if (Doc.Variables.Count > 0)
            {
                Word.Variable caption = GetVariable(Doc.Variables,
                    Const.Globals.CAPTION_VARIABLE_NAME);
                if (caption != null)
                {
                    return true;
                }

                Word.Variable date = GetVariable(Doc.Variables,
                    Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = GetVariable(Doc.Variables,
                    Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return true;
                }

                Word.Variable table = GetVariable(Doc.Variables,
                    Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return true;
                }

                Word.Variable content = GetVariable(Doc.Variables,
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
                DeleteVariable(Const.Globals.CAPTION_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DATE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DESCRIPTION_VARIABLE_NAME);
                DeleteVariable(Const.Globals.TABLE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.XML_VARIABLE_NAME);
            }
        }

        private void DeleteVariable(string name)
        {
            Word.Variable variable = GetVariable(Doc.Variables, name);
            if (variable != null)
                variable.Delete();
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
            Form dialog = new CategoriesEditorDialog(DataSet);
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
            dialog.Show();
            //OnPropertiesChanged(new EventArgs());
        }


        public void ImportDataFromWordDocuments()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
            {
                // importDocuments = FileSystem.ImportFiles(dialog.SelectedPath);
            }
        }

        public void ShowAnalizerDialog()
        {

        }

        public void ShowTableViewerDialog()
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
