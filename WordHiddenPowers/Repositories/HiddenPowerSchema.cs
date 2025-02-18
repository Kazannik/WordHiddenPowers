using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using WordHiddenPowers.Repositories.WordFiles;
using Category = WordHiddenPowers.Repositories.Categories.Category;

namespace WordHiddenPowers.Repositories
{
	partial class RepositoryDataSet
	{
		private static readonly Func<Note, int> sortBySelectionStart = new Func<Note, int>(note => note.WordSelectionStart);
		private static readonly Func<Note, int> sortByPosition = new Func<Note, int>(note => note.Subcategory.Position);

		public enum SortType : int
		{
			SelectionStart = 0,
			Position = 1
		}

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
				TextPowers.AddTextPowersRow(note.Category.Guid, note.Subcategory.Guid, note.Description, note.Value.ToString(), note.Rating, note.WordSelectionStart, note.WordSelectionEnd, fileId);
			}
			else
			{
				DecimalPowers.AddDecimalPowersRow(note.Category.Guid, note.Subcategory.Guid, note.Description, (double)note.Value, note.Rating, note.WordSelectionStart, note.WordSelectionEnd, fileId);
			}
		}

		public IEnumerable<Note> GetNotes(SortType sort = SortType.SelectionStart)
		{

			return (from row in TextPowers
					where row.RowState != DataRowState.Deleted
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.Union(from row in DecimalPowers
					   where row.RowState != DataRowState.Deleted
					   select Note.Create(
						   dataRow: row,
						   fileRow: WordFiles.GetRow(row.file_id),
						   subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.OrderBy(sort == SortType.SelectionStart ? sortBySelectionStart : sortByPosition);
		}

		private Subcategory GetSubcategory(string guid)
		{
			string categoryGuid = Subcategories.GetRow(guid)["category_guid"] as string;
			Category category = Categories.Get(categoryGuid);
			return Subcategories.Get(category, guid);
		}

		private Subcategory GetSubcategoryOrDefault(string guid)
		{
			if (Subcategories.Exists(guid: guid))
			{
				string categoryGuid = Subcategories.GetRow(guid)["category_guid"] as string;
				Category category = Categories.GetOrDefault(categoryGuid);
				return Subcategories.Get(category, guid);
			}
			else
			{
				return Subcategory.Dafault;
			}
		}

		public IEnumerable<CategoriesRow> GetCategories()
		{
			return (from subcategory in Subcategories
					.Where(s => s.RowState != DataRowState.Deleted)
					join category in Categories
					.Where(c => c.RowState != DataRowState.Deleted)
					on subcategory.category_guid equals category.key_guid
					select category)
					.GroupBy(x => x.key_guid).Select(y => y.First());
		}

		public IEnumerable<CategoriesRow> GetCategories(bool subcategoryIsText)
		{
			return (from subcategory in Subcategories
					.Where(s => s.RowState != DataRowState.Deleted && s.IsText == subcategoryIsText)
					join category in Categories
					.Where(c => c.RowState != DataRowState.Deleted)
					on subcategory.category_guid equals category.key_guid
					select category)
					.GroupBy(x => x.key_guid).Select(y => y.First());
		}

		public void Remove(Note note)
		{
			if (note.IsText)
				TextPowers.Remove(note);
			else
				DecimalPowers.Remove(note);
		}

		public void Insert(int index, Category category)
		{

			//Categories.Add(Category.Create(
			//	id: index,
			//	caption: category.Caption,
			//	description: category.Description,
			//	isObligatory: category.IsObligatory,
			//	beforeText: category.BeforeText,
			//	afterText: category.AfterText));
		}

		partial class TextPowersDataTable
		{
			public void Set(int id, string categoryGuid, string subcategoryGuid, string description, string value, int rating, int wordSelectionStart, int wordSelectionEnd)
			{
				if (Get(id: id) is TextPowersRow row)
				{
					row.BeginEdit();

					row.category_guid = categoryGuid;
					row.subcategory_guid = subcategoryGuid;
					row.Description = description;
					row.Value = value as string;
					row.Rating = rating;
					row.WordSelectionEnd = wordSelectionEnd;
					row.WordSelectionStart = wordSelectionStart;

					row.EndEdit();
				}
			}

			public void Set(int id, string categoryGuid, string subcategoryGuid)
			{
				if (Get(id: id) is TextPowersRow row)
				{
					row.BeginEdit();

					row.category_guid = categoryGuid;
					row.subcategory_guid = subcategoryGuid;

					row.EndEdit();
				}
			}

			public void Remove(Note note)
			{
				DataRow row = GetOrDefault(note: note);
				row?.Delete();
			}

			private DataRow GetOrDefault(Note note)
			{
				if (Exists(id: note.Id))
				{
					return Get(id: note.Id);
				}
				else
				{
					return null;
				}
			}

			private DataRow Get(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
			}

			private bool Exists(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).Any();
			}
		}

		partial class DecimalPowersDataTable
		{
			public void Set(int id, string categoryGuid, string subcategoryGuid, string description, double value, int rating, int wordSelectionStart, int wordSelectionEnd)
			{
				if (Get(id: id) is DecimalPowersRow row)
				{
					row.BeginEdit();

					row.category_guid = categoryGuid;
					row.subcategory_guid = subcategoryGuid;
					row.Description = description;
					row.Value = (double)value;
					row.Rating = rating;
					row.WordSelectionEnd = wordSelectionEnd;
					row.WordSelectionStart = wordSelectionStart;

					row.EndEdit();
				}
			}

			public void Set(int id, string categoryGuid, string subcategoryGuid)
			{
				if (Get(id: id) is TextPowersRow row)
				{
					row.BeginEdit();

					row.category_guid = categoryGuid;
					row.subcategory_guid = subcategoryGuid;

					row.EndEdit();
				}
			}

			public void Remove(Note note)
			{
				DataRow row = GetOrDefault(note: note);
				row?.Delete();
			}

			private DataRow GetOrDefault(Note note)
			{
				if (Exists(id: note.Id))
				{
					return Get(id: note.Id);
				}
				else
				{
					return null;
				}
			}

			private DataRow Get(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();
			}

			private bool Exists(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).Any();
			}
		}

		partial class CategoriesDataTable
		{

			public Category Add(Category category)
			{
				CategoriesRow row = (CategoriesRow)Rows.Add(category.ToObjectsArray());
				return Category.Create(row);
			}

			public void Remove(string guid)
			{
				List<SubcategoriesRow> rows = new List<SubcategoriesRow>(SubcategoriesTable.GetSubcategories(categoryGuid: guid));
				foreach (SubcategoriesRow refRow in rows)
				{
					refRow.Delete();
				}
				CategoriesRow row = GetRow(guid: guid) as CategoriesRow;
				row.Delete();
			}

			public new void Clear()
			{
				(from DataRow row in Rows
				 where row.RowState != DataRowState.Deleted
				 select row).ToList().ForEach(x => x.Delete());
			}

			public Category Get(string guid)
			{
				CategoriesRow row = GetRow(guid: guid) as CategoriesRow;
				return Category.Create(row);
			}

			public Category GetOrDefault(string guid)
			{
				if (Exists(guid: guid))
				{
					CategoriesRow row = GetRow(guid: guid) as CategoriesRow;
					return Category.Create(row);
				}
				else
				{
					return Category.Default;
				}
			}

			public void Write(Category category)
			{
				if (GetRow(category.Guid) is CategoriesRow row)
				{
					if (!category.Equals(row))
					{
						row.BeginEdit();

						row.position = category.Position;
						row.Caption = category.Caption;
						row.Description = category.Description;
						row.IsObligatory = category.IsObligatory;
						row.BeforeText = category.BeforeText;
						row.AfterText = category.AfterText;

						row.EndEdit();
					}
				}
			}

			private DataRow GetRow(string guid)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["key_guid"].Equals(guid)
						select row).First();
			}

			public bool Exists(string guid)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["key_guid"].Equals(guid)
						select row).Any();
			}

			private SubcategoriesDataTable SubcategoriesTable
			{
				get { return Globals.ThisAddIn.Documents.ActiveDocument.DataSet.Subcategories; }
			}
		}

		partial class SubcategoriesDataTable
		{
			private CategoriesDataTable CategoriesTable
			{
				get { return Globals.ThisAddIn.Documents.ActiveDocument.DataSet.Categories; }
			}

			public Subcategory Get(string subcategoryGuid)
			{
				SubcategoriesRow row = (SubcategoriesRow)GetRow(subcategoryGuid);
				return Get(categoryGuid: row.category_guid, subcategoryGuid: subcategoryGuid);
			}

			public Subcategory Get(string categoryGuid, string subcategoryGuid)
			{
				Category category = CategoriesTable.Get(categoryGuid);
				return Get(category: category, subcategoryGuid: subcategoryGuid);
			}

			public Subcategory Get(Category category, string subcategoryGuid)
			{
				SubcategoriesRow row = (SubcategoriesRow)GetRow(subcategoryGuid);
				return Subcategory.Create(category, row);
			}

			public IEnumerable<SubcategoriesRow> GetSubcategories(string categoryGuid)
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted
					&& x.category_guid == categoryGuid);
			}

			public IEnumerable<SubcategoriesRow> GetSubcategories(string categoryGuid, bool isText)
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted
					&& x.category_guid == categoryGuid
					&& x.IsText == isText);
			}

			public IEnumerable<SubcategoriesRow> GetSubcategories()
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted);
			}

			public Subcategory Add(Category category, Subcategory subcategory)
			{
				SubcategoriesRow row = (SubcategoriesRow)Rows.Add(subcategory.ToObjectsArray());
				return Subcategory.Create(category, row);
			}

			public void Remove(string guid)
			{
				if (Exists(guid: guid))
				{
					DataRow row = GetRow(guid: guid);
					row.Delete();
				}
			}

			public new void Clear()
			{
				(from DataRow row in Rows
				 where row.RowState != DataRowState.Deleted
				 select row).ToList().ForEach(x => x.Delete());
			}

			public void Write(Subcategory subcategory)
			{
				if (GetRow(subcategory.Guid) is SubcategoriesRow row)
				{
					if (!subcategory.Equals(row))
					{
						row.BeginEdit();

						row.category_guid = subcategory.Category.Guid;
						row.position = subcategory.Position;
						row.Caption = subcategory.Caption;
						row.Description = subcategory.Description;
						row.IsDecimal = subcategory.IsDecimal;
						row.IsObligatory = subcategory.IsObligatory;
						row.IsText = subcategory.IsText;
						row.BeforeText = subcategory.BeforeText;
						row.AfterText = subcategory.AfterText;
						row.Keywords = subcategory.Keywords;

						row.EndEdit();
					}
				}
			}

			internal DataRow GetRow(string guid)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["key_guid"].Equals(guid)
						select row).First();
			}

			public bool Exists(string guid)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["key_guid"].Equals(guid)
						select row).Any();
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
				WordFilesRow row = getRow(id: id) as WordFilesRow;
				return WordFile.Create(row);
			}

			public WordFile Get(string fileName)
			{
				WordFilesRow row = getRow(fileName: fileName) as WordFilesRow;
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
