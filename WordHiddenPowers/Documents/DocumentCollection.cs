using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Categories;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using System.Collections;

namespace WordHiddenPowers.Documents
{
    public class DocumentCollection : IEnumerable<Document>, IDisposable
    {
        internal RibbonToggleButton paneVisibleButton;

        private IDictionary<int, Document> documents;

        private IDictionary<int, Category> categories;
        private IDictionary<int, Subcategory> subcategories;
        private IList<Note> notes;

        private IDictionary<Subcategory, double> sumDecimalNotes;

        public DocumentCollection(RibbonToggleButton paneVisibleButton)
        {
            documents = new Dictionary<int, Document>();

            this.paneVisibleButton = paneVisibleButton;
            this.paneVisibleButton.Checked = false;
            this.paneVisibleButton.Click += new RibbonControlEventHandler(PaneVisibleButtonClick);

           /// this.panes = Globals.ThisAddIn.CustomTaskPanes;

            PowersDataSet = new RepositoryDataSet();

            subcategories = new Dictionary<int,Subcategory>();
            categories = new Dictionary<int, Category>();
            notes = new List<Note>();
            sumDecimalNotes = new Dictionary<Subcategory, double>();
        }

        public Document ActiveDocument { get; private set; }

        private void PaneVisibleButtonClick(object sender, RibbonControlEventArgs e)
        {
            RibbonToggleButton button = (RibbonToggleButton)sender;
                ActiveDocument.CustomPane.Visible = button.Checked;


            //if (button.Id == Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Id &&
            //    Globals.ThisAddIn.Panes.Count > 0)
            //{
            //}
        }

        #region Documents Command

        public void Activate(Word.Document Doc, Word.Window Wn)
        {
            if (!documents.ContainsKey(Doc.DocID))
            {
                documents.Add(Doc.DocID, Document.Create(this, Doc.FullName, Doc));
            }
            ActiveDocument = documents[Doc.DocID];
            paneVisibleButton.Checked = ActiveDocument.CustomPane.Visible;
        }

        public void Deactivate(Word.Document Doc, Word.Window Wn)
        {
            ActiveDocument = null;
        }

        public void Open(Word.Document Doc)
        {
            if (!documents.ContainsKey(Doc.DocID))
            {
                documents.Add(Doc.DocID, Document.Create(this, Doc.FullName, Doc));
            }
            ActiveDocument = documents[Doc.DocID];
            paneVisibleButton.Checked = ActiveDocument.CustomPane.Visible;
        }

        public void Remove(Word.Document Doc)
        {
            if (documents.ContainsKey(Doc.DocID))
            {
                documents.Remove(Doc.DocID);
            }
        }

        #endregion

        #region HiddenData Command




        #endregion


        public RepositoryDataSet PowersDataSet { get; }

        public Table SumTable { get; private set; }

        public void Add(Document item)
        {
            //if (Count == 0)
            //{
            //    SumTable = new Table(item.Table.Rows.Count, item.Table.ColumnCount);

            //    string xml = item.PowersDataSetToXml();
            //    SetXml(PowersDataSet, xml);
            //    PowersDataSet.DecimalPowers.Clear();
            //    PowersDataSet.TextPowers.Clear();
            //}

            //for (int r = 0; r < SumTable.Rows.Count; r++)
            //{
            //    for (int c = 0; c < SumTable.ColumnCount; c++)
            //    {
            //        SumTable.Rows[r][c].Value += item.Table.Rows[r][c].Value;
            //    }
            //}
            //ReadData(item);
            //base.Add(item);
        }

        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
        }

        private void SetXml(RepositoryDataSet dataSet, string xml)
        {
            StringReader reader = new StringReader(xml);
            dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
            reader.Close();
        }

        public void ReadData(Document item)
        {
            if (item.DataSet.Categories.Rows.Count > 0)
            {
                foreach (DataRow row in item.DataSet.Categories.Rows)
                {
                    Category category = Category.Create(row);
                    if (!categories.ContainsKey(category.Id))
                        categories.Add(category.Id, category);
                }
            }
            else
            {
                Category category = Category.Default();
                if (!categories.ContainsKey(category.Id))
                    categories.Add(category.Id, category);
            }

            if (item.DataSet.Subcategories.Rows.Count > 0)
            {
                foreach (DataRow row in item.DataSet.Subcategories.Rows)
                {
                    Subcategory subcategory = Subcategory.Create(categories[(int)row["category_id"]], row);
                    if (!subcategories.ContainsKey(subcategory.Id))
                        subcategories.Add(subcategory.Id, subcategory);
                }
            }
            else
            {
                foreach (Category category in categories.Values)
                {
                    Subcategory subcategory = Subcategory.Default(category: category);
                    if (!subcategories.ContainsKey(subcategory.Id))
                        subcategories.Add(subcategory.Id, subcategory);
                }
            }

            foreach (DataRow row in item.DataSet.DecimalPowers.Rows)
            {
                Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((RepositoryDataSet.DecimalPowersRow)row, subcategory);
                notes.Add(note);
                if (!sumDecimalNotes.ContainsKey(note.Subcategory))
                {
                    sumDecimalNotes.Add(note.Subcategory, (double)note.Value);
                }
                else
                {
                    sumDecimalNotes[note.Subcategory] += (double)note.Value;
                }
            }

            foreach (DataRow row in item.DataSet.TextPowers.Rows)
            {
                Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((RepositoryDataSet.TextPowersRow)row, subcategory);
                notes.Add(note);
            }
        }

        public void Dispose()
        {
            Word.Application application = Globals.ThisAddIn.Application as Word.Application;
            application.CommandBars["Text"].Reset();
            //foreach (CustomTaskPane item in Values)
            //{
            //    item.Dispose();
            //}
        }

        public IEnumerator<Document> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
