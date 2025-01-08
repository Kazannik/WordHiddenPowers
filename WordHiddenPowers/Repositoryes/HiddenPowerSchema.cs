using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WordHiddenPowers.Repositoryes.Categories;
using WordHiddenPowers.Repositoryes.WordFiles;
using WordHiddenPowers.Repositoryes.Notes;

namespace WordHiddenPowers.Repositoryes
{
	partial class RepositoryDataSet
	{

		public void AddNote(Note note, string fileName, string caption, string description, DateTime date)
		{
			int fileId;
			if (WordFiles.Exists(fileName))
			{
				fileId = WordFiles.Get(fileName).Id;
			}
			else
			{
				fileId = WordFiles.Add(fileName, caption, description, date).Id;
			}

			if (note.IsText)
			{
				TextPowers.AddTextPowersRow(note.Category.Id, note.Subcategory.Id, note.Description, note.Value.ToString(), note.Reiting, note.WordSelectionStart, note.WordSelectionEnd, fileId);
			}
			else
			{
				DecimalPowers.AddDecimalPowersRow(note.Category.Id, note.Subcategory.Id, note.Description, (double)note.Value, note.Reiting, note.WordSelectionStart, note.WordSelectionEnd, fileId);
			}
		}

		public IEnumerable<Note> GetNotes()
		{
			return (from row in TextPowers select Note.Create(row, WordFiles.GetRow(row.file_id), GetSubcategory(row.subcategory_id)))
				.Union(from row in DecimalPowers where row.RowState != DataRowState.Deleted select Note.Create(row, WordFiles.GetRow(row.file_id), GetSubcategory(row.subcategory_id)))
				.OrderBy(n => n.WordSelectionStart);
		}


		public IEnumerable<Note> GetNotesSort()
		{
			return ((from row in TextPowers
					 .Where(r => r.RowState != DataRowState.Deleted)
					 select Note.Create(row, WordFiles.GetRow(row.file_id), GetSubcategory(row.subcategory_id)))
				.Union(from row in DecimalPowers
					   .Where(r => r.RowState != DataRowState.Deleted) 
					   select Note.Create(row, WordFiles.GetRow(row.file_id), GetSubcategory(row.subcategory_id))))
				.OrderBy(n => n.Subcategory.Id);
		}

		private Subcategory GetSubcategory(int id)
		{
			int categoryId = (int)Subcategories.GetRow(id)["category_id"];
			Category category = Categories.Get(categoryId);
			return Subcategories.Get(category, id);
		}

		public IEnumerable<CategoriesRow> GetCategories(bool isText)
		{
			return (from subcategory in Subcategories
					.Where(s => s.RowState != DataRowState.Deleted && s.IsText == isText)
					join category in Categories
					.Where(c => c.RowState != DataRowState.Deleted)
					on subcategory.category_id equals category.id
					select category)
					.GroupBy(x => x.id).Select(y => y.First());
		}

		partial class TextPowersDataTable
		{
			public void Set(int id, int categoryId, int subcategoryId, string description, string value, int reiting, int wordSelectionStart, int wordSelectionEnd)
			{
				if (GetRow(id) is TextPowersRow row)
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
				row?.Delete();
			}

			private DataRow GetRow(Note note)
			{
				return GetRow(note.Id);
			}

			private DataRow GetRow(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
			}
		}

		partial class DecimalPowersDataTable
		{
			public void Set(int id, int categoryId, int subcategoryId, string description, double value, int reiting, int wordSelectionStart, int wordSelectionEnd)
			{
				if (GetRow(id) is DecimalPowersRow row)
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
				row?.Delete();
			}

			private DataRow GetRow(Note note)
			{
				return GetRow(note.Id);
			}

			private DataRow GetRow(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
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
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted 
					&& x.category_id == categoryId 
					&& x.IsText == isText);
			}

			public Subcategory Add(Category category, Subcategory subcategory)
			{
				SubcategoriesRow row = (SubcategoriesRow)Rows.Add(subcategory.ToObjectsArray());
				return Subcategory.Create(category, row);
			}

			public void Write(Subcategory subcategory)
			{
				if (GetRow(subcategory.Id) is SubcategoriesRow row)
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

			internal DataRow GetRow(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
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
				if (GetRow(category.Id) is CategoriesRow row)
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
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
			}
		}

		partial class WordFilesDataTable
		{
			public WordFile Add(string fileName, string caption, string description, DateTime date)
			{
				WordFilesRow row = (WordFilesRow)Rows.Add(new object[] {
					null,
					fileName,
					caption,
					description,
					date });
				return WordFile.Create(row);
			}

			public WordFile Add(WordFile file)
			{
				WordFilesRow row = (WordFilesRow)Rows.Add(file.ToObjectsArray());
				return WordFile.Create(row);
			}

			public WordFile Get(int id)
			{
				WordFilesRow row = getRow(id) as WordFilesRow;
				return WordFile.Create(row);
			}

			public WordFile Get(string fileName)
			{
				WordFilesRow row = getRow(fileName) as WordFilesRow;
				return WordFile.Create(row);
			}

			public void Write(WordFile file)
			{
				if (GetRow(file.Id) is WordFilesRow row)
				{
					row.BeginEdit();
					row.FileName = file.Filename;
					row.Caption = file.Caption;
					row.Description = file.Description;
					row.Date = file.Date;
					row.EndEdit();
				}
			}

			public WordFilesRow GetRow(int id)
			{
				return getRow(id) as WordFilesRow;
			}

			private DataRow getRow(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();

			}

			private DataRow getRow(string fileName)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["FileName"].Equals(fileName)
						select row).First();
			}

			public bool Exists(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).Any();
			}

			public bool Exists(string fileName)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["FileName"].Equals(fileName)
						select row).Any();
			}
		}
	}
}
