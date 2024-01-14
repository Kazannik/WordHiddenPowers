using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHiddenPowers.Data;
using WordSuite.Controls;
using AddIn = WordHiddenPowers.Repositoryes.Models;

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
            subcategories = new Dictionary<int, AddIn.Subcategory>();
            categories = new Dictionary<int, AddIn.Category>();
            notes = new List<Note>();
            sumDecimalNotes = new Dictionary<AddIn.Subcategory, double>();
        }

        public Table SumTable { get; private set; }
        
        public new void Add(Document item)
        {
            if (base.Count==0)
            {
                SumTable = new Table(item.Table.Rows.Count, item.Table.ColumnCount);
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
