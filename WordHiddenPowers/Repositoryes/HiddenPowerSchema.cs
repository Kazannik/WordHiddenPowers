using System;
using System.Data;
using System.Linq;
using WordHiddenPowers.Repositoryes.Models;
using System.Collections.Generic;

namespace WordHiddenPowers.Repositoryes
{


    partial class RepositoryDataSet
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
            public IEnumerable<SubcategoriesRow> Get(int categoryId)
            {
                return this.AsEnumerable().Where(x => x.category_id == categoryId);
            }
        }

        partial class CategoriesDataTable
        {
        }
    }
}
