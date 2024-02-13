using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Categories;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Documents
{
    public class DocumentCollection : List<Document>
    {
        private IDictionary<int, Category> categories;
        private IDictionary<int, Subcategory> subcategories;
        private IList<Note> notes;

        private IDictionary<Subcategory, double> sumDecimalNotes;

        public DocumentCollection()
        {
            PowersDataSet = new RepositoryDataSet();

            subcategories = new Dictionary<int,Subcategory>();
            categories = new Dictionary<int, Category>();
            notes = new List<Note>();
            sumDecimalNotes = new Dictionary<Subcategory, double>();
        }


        public RepositoryDataSet PowersDataSet { get; }

        public Table SumTable { get; private set; }

        public new void Add(Document item)
        {
            if (Count == 0)
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
            dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
            reader.Close();
        }

        public void ReadData(Document item)
        {
            if (item.PowersDataSet.Categories.Rows.Count > 0)
            {
                foreach (DataRow row in item.PowersDataSet.Categories.Rows)
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

            if (item.PowersDataSet.Subcategories.Rows.Count > 0)
            {
                foreach (DataRow row in item.PowersDataSet.Subcategories.Rows)
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

            foreach (DataRow row in item.PowersDataSet.DecimalPowers.Rows)
            {
                Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((WordHiddenPowers.Repositoryes.RepositoryDataSet.DecimalPowersRow)row, subcategory);
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

            foreach (DataRow row in item.PowersDataSet.TextPowers.Rows)
            {
                Subcategory subcategory = subcategories[(int)row["subcategory_id"]];
                Note note = Note.Create((WordHiddenPowers.Repositoryes.RepositoryDataSet.TextPowersRow)row, subcategory);
                notes.Add(note);
            }
        }
    }
}
