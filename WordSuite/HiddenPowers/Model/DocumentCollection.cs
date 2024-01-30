using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHiddenPowers.Data;
using WordSuite.Controls;
using WordSuite.Repositoryes;
using AddIn = WordHiddenPowers.Categories;

namespace WordSuite.HiddenPowers.Model
{
    public class DocumentCollection: List<Document>
    {
        private IDictionary<int, AddIn.Category> categories;
        private IDictionary<int, AddIn.Subcategory> subcategories;
        private IList<Note> notes;

        private IDictionary<AddIn.Subcategory, double> sumDecimalNotes;
        
        public DocumentCollection()
        {
            PowersDataSet = new RepositoryDataSet();

            subcategories = new Dictionary<int, AddIn.Subcategory>();
            categories = new Dictionary<int, AddIn.Category>();
            notes = new List<Note>();
            sumDecimalNotes = new Dictionary<AddIn.Subcategory, double>();
        }


        public RepositoryDataSet PowersDataSet { get; }

        public Table SumTable { get; private set; }
        
        public new void Add(Document item)
        {
            if (base.Count==0)
            {
                SumTable = new Table(item.Table.Rows.Count, item.Table.ColumnCount);
                                
                string xml = item.PowersDataSetToXml();
                SetXml(PowersDataSet, xml);
                PowersDataSet.DecimalPowers.Clear();
                PowersDataSet.TextPowers.Clear();
            }

            for (int r = 0; r < SumTable.Rows.Count; r++)
            {
                for (int c = 0; c < SumTable.ColumnCount; c++)
                {
                    SumTable.Rows[r][c].Value += item.Table.Rows[r][c].Value;
                }
            }
            ReadData(item);
            base.Add(item);
        }

        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
        }

        private void SetXml(RepositoryDataSet dataSet, string xml)
        {
            StringReader reader = new StringReader(xml);
            dataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
            reader.Close();
        }

        public void ReadData(Document item)
        {
            if (item.PowersDataSet.Categories.Rows.Count > 0)
            {
                foreach (DataRow row in item.PowersDataSet.Categories.Rows)
                {
                    AddIn.Category category = AddIn.Category.Create(row);
                    if (!categories.ContainsKey(category.Id))
                        categories.Add(category.Id, category);
                }
            }
            else
            {
                AddIn.Category category = AddIn.Category.Default();
                if (!categories.ContainsKey(category.Id))
                    categories.Add(category.Id, category);
            }
                        
            if (item.PowersDataSet.Subcategories.Rows.Count > 0)
            {
                foreach (DataRow row in item.PowersDataSet.Subcategories.Rows)
                {
                    AddIn.Subcategory subcategory = AddIn.Subcategory.Create(categories[(int)row["category_id"]], row);
                    if (!subcategories.ContainsKey(subcategory.Id))
                        subcategories.Add(subcategory.Id, subcategory);
                }
            }
            else
            {
                foreach (AddIn.Category category in categories.Values)
                {
                    AddIn.Subcategory subcategory = AddIn.Subcategory.Default(category: category);
                    if (!subcategories.ContainsKey(subcategory.Id))
                        subcategories.Add(subcategory.Id, subcategory);
                }
            }

            foreach (DataRow row in item.PowersDataSet.DecimalPowers.Rows)
            {
                AddIn.Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((WordHiddenPowers.Repositoryes.RepositoryDataSet.DecimalPowersRow)row, subcategory);
                notes.Add(note);
                if (!sumDecimalNotes.ContainsKey(note.Subcategory))
                {
                    sumDecimalNotes.Add(note.Subcategory,(double) note.Value);
                }
                else
                {
                    sumDecimalNotes[note.Subcategory] += (double)note.Value;
                }
            }

            foreach (DataRow row in item.PowersDataSet.TextPowers.Rows)
            {
                AddIn.Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((WordHiddenPowers.Repositoryes.RepositoryDataSet.TextPowersRow)row, subcategory);
                notes.Add(note);
            }
        }
    }
}
