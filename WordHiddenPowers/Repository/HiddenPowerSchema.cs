// Ignore Spelling: Tsv

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using WordHiddenPowers.Repository.Categories;
using WordHiddenPowers.Repository.Notes;
using WordHiddenPowers.Repository.WordFiles;
using Category = WordHiddenPowers.Repository.Categories.Category;

namespace WordHiddenPowers.Repository
{
	partial class RepositoryDataSet
	{
		private static readonly Func<Note, int> sortBySelectionStart = new Func<Note, int>(note => note.WordSelectionStart);
		private static readonly Func<Note, int> sortByCategoryPosition = new Func<Note, int>(note => note.Subcategory.Position);

		private static readonly Func<TextPowersRow, string, bool> whereGuidTextPowers = new Func<TextPowersRow, string, bool>((row, guid) => row.subcategory_guid == guid);
		private static readonly Func<TextPowersRow, string, bool> whereNotGuidTextPowers = new Func<TextPowersRow, string, bool>((row, guid) => row.subcategory_guid != guid);

		private static readonly Func<DecimalPowersRow, string, bool> whereGuidDecimalPowers = new Func<DecimalPowersRow, string, bool>((row, guid) => row.subcategory_guid == guid);
		private static readonly Func<DecimalPowersRow, string, bool> whereNotGuidDecimalPowers = new Func<DecimalPowersRow, string, bool>((row, guid) => row.subcategory_guid != guid);


		public bool IsTables => DocumentKeys.Any();

		public bool IsModel => (RowsHeaders.Any() && ColumnsHeaders.Any()) ||
			(Categories.Any() && Subcategories.Any());

		public enum SortType : int
		{
			SelectionStart = 0,
			CategoryPosition = 1
		}


		public string GetValue(string key)
		{
			return GetOrDefault(key: key)?["value"] as string;
		}

		public void SetValue(string key, string value)
		{
			if (Exists(key))
			{
				SettingRow row = Get(key: key) as SettingRow;
				row.BeginEdit();
				row.value = value;
				row.EndEdit();
			}
			else
			{
				Setting.Rows.Add(new object[] { key, value });
			}
		}

		public void RemoveValue(string key)
		{
			DataRow row = GetOrDefault(key: key);
			row?.Delete();
		}

		private DataRow GetOrDefault(string key)
		{
			if (Exists(key: key))
			{
				return Get(key: key);
			}
			else
			{
				return null;
			}
		}

		private DataRow Get(string key)
		{
			return (from DataRow row in Setting
					where row.RowState != DataRowState.Deleted
					&& row["key"].Equals(key)
					select row).First();
		}

		private bool Exists(string key)
		{
			return (from DataRow row in Setting
					where row.RowState != DataRowState.Deleted
					&& row["key"].Equals(key)
					select row).Any();
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
				if (!TextPowers.Exists(note: note, fileId: fileId))
					TextPowers.AddTextPowersRow(note.Category.Guid, note.Subcategory.Guid, note.Description, note.Value.ToString(), note.Rating, note.WordSelectionStart, note.WordSelectionEnd, fileId, false);
			}
			else
			{
				if (!DecimalPowers.Exists(note: note, fileId: fileId))
					DecimalPowers.AddDecimalPowersRow(note.Category.Guid, note.Subcategory.Guid, note.Description, (double)note.Value, note.Rating, note.WordSelectionStart, note.WordSelectionEnd, fileId, false);
			}
		}

		public IEnumerable<string> GetFilesCaption(string subcategoryGuid = "", bool not = false)
		{
			return (from row in TextPowers
					where row.RowState != DataRowState.Deleted
					&& (string.IsNullOrEmpty(subcategoryGuid) || (not
					? whereNotGuidTextPowers(row, subcategoryGuid)
					: whereGuidTextPowers(row, subcategoryGuid)))
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.Union(from row in DecimalPowers
					   where row.RowState != DataRowState.Deleted
					   && (string.IsNullOrEmpty(subcategoryGuid) || (not
					   ? whereNotGuidDecimalPowers(row, subcategoryGuid)
					   : whereGuidDecimalPowers(row, subcategoryGuid)))
					   select Note.Create(
						   dataRow: row,
						   fileRow: WordFiles.GetRow(row.file_id),
						   subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.OrderBy(note => note.FileCaption)
				.Select(note => note.FileCaption)
				.Distinct();
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
				.OrderBy(note => note.FileCaption)
				.OrderBy(sort == SortType.SelectionStart ? sortBySelectionStart : sortByCategoryPosition);
		}

		public IEnumerable<Note> GetTextNotes(string subcategoryGuid, bool viewHide = true)
		{
			return (from row in TextPowers
					where row.RowState != DataRowState.Deleted
					&& row.subcategory_guid == subcategoryGuid
					&& (viewHide || row.Hide == false)
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.OrderBy(note => note.FileCaption);
		}

		public IEnumerable<Note> GetDecimalNotes(string subcategoryGuid)
		{
			return (from row in DecimalPowers
					where row.RowState != DataRowState.Deleted
					&& row.subcategory_guid == subcategoryGuid
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.OrderBy(note => note.FileCaption);
		}

		/// <summary>
		/// Получить сумму значений по всей подкатегории.
		/// </summary>
		/// <param name="subcategoryGuid">Guid подкатегории.</param>
		/// <returns></returns>
		public double SumDecimalNote(string subcategoryGuid)
		{
			return (from row in DecimalPowers
					where row.RowState != DataRowState.Deleted
					&& row.subcategory_guid == subcategoryGuid
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				 .Sum(note => (double)note.Value);
		}

		/// <summary>
		///  Количество записей в в подкатегории.
		/// </summary>
		/// <param name="subcategoryGuid"></param>
		/// <returns></returns>
		public int GetNotesCount(string subcategoryGuid)
		{
			return TextPowers.Count(row => row.RowState != DataRowState.Deleted
			&& row.subcategory_guid == subcategoryGuid)
				+ DecimalPowers.Count(row => row.RowState != DataRowState.Deleted
				&& row.subcategory_guid == subcategoryGuid);
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

		public void Set(Note note)
		{
			if (note.IsText)
				TextPowers.Set(note);
			else
				DecimalPowers.Set(note);
		}

		/// <summary>
		/// Получить контент в формате TSV для обучения нейронной сети. 
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetMlModelDataSetTsvContent(bool header)
		{
			IEnumerable<string> content = (from row in TextPowers
										   where row.RowState != DataRowState.Deleted
										   select Note.Create(
											   dataRow: row,
											   fileRow: WordFiles.GetRow(row.file_id),
											   subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.OrderBy(note => note.Subcategory.Guid)
				.Select(note => string.Format("{0}\t{1}",
				note.Subcategory.Guid,
				Utils.MLModel.ConvertToCompliance(note.Value.ToString())));
			if (header)
				return (new string[] { "Area\tTitle" }).Union(content);
			else
				return content;
		}

		public IEnumerable<string> GetTxtContent()
		{
			return GetNotes()
				.OrderBy(note => note.Subcategory.Guid)
				.Select(note => string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}" + (note.IsText ? string.Empty : "\t{6}"),
				note.FileName,
				note.Category.Code,
				note.Subcategory.Code,
				note.Rating,
				note.WordSelectionStart,
				note.WordSelectionEnd,
				note.Value));
		}

		public IEnumerable<string> GetContent()
		{
			Regex regex = new Regex("(^\\w\\s)|(\\s\\w\\s)|(\\s\\w$)|(^\\w$)|(\\W{1,})|(\\s{2,})|(\\d{1,})", RegexOptions.Multiline);
			return (from row in TextPowers
					where row.RowState != DataRowState.Deleted
					select Note.Create(
						dataRow: row,
						fileRow: WordFiles.GetRow(row.file_id),
						subcategory: GetSubcategoryOrDefault(row.subcategory_guid)))
				.Select(note => regex.Replace(note.Value.ToString(), " "))
				.Select(s => s.ToLower())
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Where(s => s.Length > 2)
				.Distinct();
		}

		partial class TextPowersDataTable
		{
			public void Set(int id, string categoryGuid, string subcategoryGuid, string description, string value, int rating, int wordSelectionStart, int wordSelectionEnd, bool hide)
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
					row.Hide = hide;

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

			public void Set(Note note)
			{
				Set(
					id: note.Id,
					categoryGuid: note.Category.Guid,
					subcategoryGuid: note.Subcategory.Guid,
					description: note.Description,
					value: note.Value as string,
					rating: note.Rating,
					wordSelectionStart: note.WordSelectionStart,
					wordSelectionEnd: note.WordSelectionEnd,
					hide: note.Hide);
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

			public bool Exists(Note note, int fileId)
			{
				return Exists(category_guid: note.Category.Guid,
					subcategory_guid: note.Subcategory.Guid,
					Description: note.Description,
					Value: note.Value.ToString(),
					Rating: note.Rating,
					WordSelectionStart: note.WordSelectionStart,
					WordSelectionEnd: note.WordSelectionEnd,
					file_id: fileId);
			}

			public bool Exists(string category_guid, string subcategory_guid, string Description, string Value, int Rating, int WordSelectionStart, int WordSelectionEnd, int file_id)
			{
				return (from TextPowersRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.category_guid == category_guid
						&& row.subcategory_guid == subcategory_guid
						&& row.Description == Description
						&& row.Value == Value
						&& row.Rating == Rating
						&& row.WordSelectionStart == WordSelectionStart
						&& row.WordSelectionEnd == WordSelectionEnd
						&& row.file_id == file_id
						select row).Any();
			}
		}

		partial class DecimalPowersDataTable
		{
			public void Set(int id, string categoryGuid, string subcategoryGuid, string description, double value, int rating, int wordSelectionStart, int wordSelectionEnd, bool hide)
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
					row.Hide = hide;

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

			public void Set(Note note)
			{
				Set(
					id: note.Id,
					categoryGuid: note.Category.Guid,
					subcategoryGuid: note.Subcategory.Guid,
					description: note.Description,
					value: (double)note.Value,
					rating: note.Rating,
					wordSelectionStart: note.WordSelectionStart,
					wordSelectionEnd: note.WordSelectionEnd,
					hide: note.Hide);
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

			public bool Exists(Note note, int fileId)
			{
				return Exists(category_guid: note.Category.Guid,
					subcategory_guid: note.Subcategory.Guid,
					Description: note.Description,
					Value: (double)note.Value,
					Rating: note.Rating,
					WordSelectionStart: note.WordSelectionStart,
					WordSelectionEnd: note.WordSelectionEnd,
					file_id: fileId);
			}

			public bool Exists(string category_guid, string subcategory_guid, string Description, double Value, int Rating, int WordSelectionStart, int WordSelectionEnd, int file_id)
			{
				return (from DecimalPowersRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.category_guid == category_guid
						&& row.subcategory_guid == subcategory_guid
						&& row.Description == Description
						&& row.Value == Value
						&& row.Rating == Rating
						&& row.WordSelectionStart == WordSelectionStart
						&& row.WordSelectionEnd == WordSelectionEnd
						&& row.file_id == file_id
						select row).Any();
			}

		}

		public Category Add(Category category)
		{
			DataRow row = this.Categories.Rows.Add(category.ToObjectsArray());
			return Category.Create(row);
		}

		public Category GetCategory(string guid)
		{
			DataRow row = this.Categories.GetRow(guid: guid);
			return Category.Create(row);
		}

		public Category GetCategoryOrDefault(string guid)
		{
			if (this.Categories.Exists(guid: guid))
			{
				DataRow row = this.Categories.GetRow(guid: guid);
				return Category.Create(row);
			}
			else
			{
				return Category.Default;
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

		public IEnumerable<Category> GetCategories(bool subcategoryIsText)
		{
			return (from subcategory in Subcategories
					.Where(s => s.RowState != DataRowState.Deleted
					&& s.IsText == subcategoryIsText)
					join category in Categories
					.Where(c => c.RowState != DataRowState.Deleted)
					on subcategory.category_guid equals category.key_guid
					select Category.Create(category))
					.GroupBy(x => x.Guid).Select(y => y.First());
		}

		public void Write(Category category)
		{
			if (this.Categories.Exists(category.Guid))
			{
				CategoriesRow row = this.Categories.GetRow(guid: category.Guid) as CategoriesRow;
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

		public void Remove(string guid)
		{
			if (Categories.Exists(guid: guid))
			{
				List<SubcategoriesRow> rows = new List<SubcategoriesRow>(Subcategories.GetSubcategoriesRows(categoryGuid: guid));
				foreach (SubcategoriesRow refRow in rows)
				{
					refRow.Delete();
				}
				Categories.Remove(guid: guid);
			}
			else if (Subcategories.Exists(guid: guid))
			{
				Subcategories.Remove(guid: guid);
			}
		}

		partial class CategoriesDataTable
		{
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

		public Subcategory Add(Category category, Subcategory subcategory)
		{
			DataRow row = this.Subcategories.Rows.Add(subcategory.ToObjectsArray());
			return Subcategory.Create(category, row);
		}

		public Subcategory GetSubcategory(string guid, object tag)
		{
			Subcategory subcategory = GetSubcategory(guid: guid);
			subcategory.Tag = tag;
			return subcategory;
		}

		public Subcategory GetSubcategory(string guid)
		{
			SubcategoriesRow row = this.Subcategories.GetRow(guid) as SubcategoriesRow;
			return GetSubcategory(categoryGuid: row.category_guid, subcategoryGuid: guid);
		}

		public Subcategory GetSubcategory(string categoryGuid, string subcategoryGuid)
		{
			Category category = this.GetCategory(categoryGuid);
			return GetSubcategory(category: category, subcategoryGuid: subcategoryGuid);
		}

		public Subcategory GetSubcategory(Category category, string subcategoryGuid)
		{
			DataRow row = this.Subcategories.GetRow(guid: subcategoryGuid);
			return Subcategory.Create(category, row);
		}

		private Subcategory GetSubcategoryOrDefault(string guid)
		{
			if (Subcategories.Exists(guid: guid))
			{
				return GetSubcategory(guid: guid);
			}
			else
			{
				return Subcategory.Dafault;
			}
		}

		public IEnumerable<Subcategory> GetSubcategories()
		{
			return from subcategoryRow in Subcategories
				   .Where(s => s.RowState != DataRowState.Deleted)
				   join categoryRow in Categories
				   .Where(c => c.RowState != DataRowState.Deleted)
				   on subcategoryRow.category_guid equals categoryRow.key_guid
				   select Subcategory.Create(Category.Create(categoryRow), subcategoryRow);
		}

		public void Write(Subcategory subcategory)
		{
			if (this.Subcategories.Exists(subcategory.Guid))
			{
				SubcategoriesRow row = this.Subcategories.GetRow(guid: subcategory.Guid) as SubcategoriesRow;
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

		partial class SubcategoriesDataTable
		{
			public IEnumerable<SubcategoriesRow> GetSubcategoriesRows(string categoryGuid)
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted
					&& x.category_guid == categoryGuid);
			}

			public IEnumerable<SubcategoriesRow> GetSubcategoriesRows(string categoryGuid, bool isText)
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted
					&& x.category_guid == categoryGuid
					&& x.IsText == isText);
			}

			public IEnumerable<SubcategoriesRow> GetSubcategoriesRows()
			{
				return this.AsEnumerable()
					.Where(x => x.RowState != DataRowState.Deleted);
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

			public WordFile GetSubcategory(int id)
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

		public partial class DocumentKeysDataTable
		{
			public void Write(string caption, string fileName, string tableContent)
			{
				if (getRow(caption) is DocumentKeysRow row)
				{
					row.BeginEdit();
					row.Caption = caption;
					row.Description1 = fileName;
					row.Description2 = tableContent;
					row.EndEdit();
				}
			}

			public DocumentKeysRow GetRow(int id)
			{
				return getRow(id) as DocumentKeysRow;
			}

			private DataRow getRow(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).First();

			}

			private DataRow getRow(string caption)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["Caption"].Equals(caption)
						select row).First();
			}

			public bool Exists(int id)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["id"].Equals(id)
						select row).Any();
			}

			public bool Exists(string caption)
			{
				return (from DataRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row["Caption"].Equals(caption)
						select row).Any();
			}
		}
	}
}
