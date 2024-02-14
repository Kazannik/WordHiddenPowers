using System.Data;
using System.Linq;
using WordHiddenPowers.Repositoryes;
using System.Collections.Generic;
using WordHiddenPowers.Categories;

namespace WordHiddenPowers.Documents
{


    partial class DocumentsDataSet
    {
        partial class RowsHeadersDataTable
        {
        }

        partial class TextPowersDataTable
        {
            public void Set(int id, int categoryId, int subcategoryId, string description, string value, int reiting, int wordSelectionStart, int wordSelectionEnd)
            {
                TextPowersRow row = GetRow(id) as TextPowersRow;
                if (row != null)
                {
                    row.BeginEdit();
                    row.category_id = categoryId;
                    row.Description = description;
                    row.Reiting = reiting;
                    row.subcategory_id = subcategoryId;
                    row.Value = value as string;
                    row.WordSelectionEnd = wordSelectionEnd;
                    row.WordSelectionStart = wordSelectionStart;
                    row.EndEdit();
                }
            }

            public void Remove(Note note)
            {
                DataRow row = GetRow(note);
                if (row != null)
                    Rows.Remove(row);
            }

            private DataRow GetRow(Note note)
            {
                return GetRow(note.Id);
            }

            private DataRow GetRow(int id)
            {
                foreach (DataRow row in Rows)
                {
                    if ((int)row["id"] == id)
                        return row;
                }
                return null;
            }
        }

        partial class DecimalPowersDataTable
        {

            public void Set(int id, int categoryId, int subcategoryId, string description, double value, int reiting, int wordSelectionStart, int wordSelectionEnd)
            {
                DecimalPowersRow row = GetRow(id) as DecimalPowersRow;
                if (row != null)
                {
                    row.BeginEdit();
                    row.category_id = categoryId;
                    row.Description = description;
                    row.Reiting = reiting;
                    row.subcategory_id = subcategoryId;
                    row.Value = (double)value;
                    row.WordSelectionEnd = wordSelectionEnd;
                    row.WordSelectionStart = wordSelectionStart;
                    row.EndEdit();
                }
            }

            public void Remove(Note note)
            {
                DataRow row = GetRow(note);
                if (row != null)
                    Rows.Remove(row);
            }

            private DataRow GetRow(Note note)
            {
                return GetRow(note.Id);
            }

            private DataRow GetRow(int id)
            {
                foreach (DataRow row in Rows)
                {
                    if ((int)row["id"] == id)
                        return row;
                }
                return null;
            }
        }

        partial class SubcategoriesDataTable
        {
            public Subcategory Get(Category category, int subcategoryId)
            {
                SubcategoriesRow row = (SubcategoriesRow)GetRow(subcategoryId);
                return Subcategory.Create(category, row);
            }

            public IEnumerable<SubcategoriesRow> Get(int categoryId, bool isText)
            {
                return this.AsEnumerable().Where(x => x.category_id == categoryId && x.IsText == isText);
            }

            public Subcategory Add(Category category, Subcategory subcategory)
            {
                SubcategoriesRow row = (SubcategoriesRow)Rows.Add(subcategory.ToObjectsArray());

                return Subcategory.Create(category, row);
            }

            public void Write(Subcategory subcategory)
            {
                SubcategoriesRow row = GetRow(subcategory.Id) as SubcategoriesRow;
                if (row != null)
                {
                    row.BeginEdit();
                    row.Caption = subcategory.Caption;
                    row.category_id = subcategory.Category.Id;
                    row.Description = subcategory.Description;
                    row.IsDecimal = subcategory.IsDecimal;
                    row.IsObligatory = subcategory.IsObligatory;
                    row.IsText = subcategory.IsText;
                    row.EndEdit();
                }
            }

            private DataRow GetRow(int id)
            {
                foreach (DataRow row in Rows)
                {
                    if ((int)row["id"] == id)
                        return row;
                }
                return null;
            }
        }

        partial class CategoriesDataTable
        {
            public Category Add(Category category)
            {
                CategoriesRow row = (CategoriesRow)Rows.Add(category.ToObjectsArray());
                return Category.Create(row);
            }

            public Category Get(int id)
            {
                CategoriesRow row = GetRow(id) as CategoriesRow;
                return Category.Create(row);
            }

            public void Write(Category category)
            {
                CategoriesRow row = GetRow(category.Id) as CategoriesRow;
                if (row != null)
                {
                    row.BeginEdit();
                    row.Caption = category.Caption;
                    row.Description = category.Description;
                    row.IsObligatory = category.IsObligatory;
                    row.EndEdit();
                }
            }

            private DataRow GetRow(int id)
            {
                foreach (DataRow row in Rows)
                {
                    if ((int)row["id"] == id)
                        return row;
                }
                return null;
            }

        }
    }
}
