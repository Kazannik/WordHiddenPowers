using ControlLibrary.Controls.ComboControls;
using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositoryes.Categories
{
	public class Subcategory : IComparable<Subcategory>, ComboControl<Subcategory>.IComboControlItem
	{
		public static Subcategory Create(Category category, DataRow dataRow)
		{
			return new Subcategory(
				category: category,
				id: (int)dataRow["id"],
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("Description") ? string.Empty : dataRow["Description"] as string,
				isDecimal: (bool)dataRow["IsDecimal"],
				isText: (bool)dataRow["IsText"],
				isObligatory: (bool)dataRow["IsObligatory"],
				beforeText: dataRow.IsNull("BeforeText") ? string.Empty : dataRow["BeforeText"] as string,
				afterText: dataRow.IsNull("AfterText") ? string.Empty : dataRow["AfterText"] as string,
				keywords: dataRow.IsNull("Keywords") ? string.Empty : dataRow["Keywords"] as string,
				guid: dataRow.IsNull("Guid") ? string.Empty : dataRow["Guid"] as string);
		}

		public static Subcategory Create(Category category, RepositoryDataSet.SubcategoriesRow dataRow)
		{
			return new Subcategory(
				category: category,
				id: dataRow.id,
				caption: dataRow.IsCaptionNull() ? string.Empty : dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				isDecimal: dataRow.IsDecimal,
				isText: dataRow.IsText,
				isObligatory: dataRow.IsObligatory,
				beforeText: dataRow.IsBeforeTextNull() ? string.Empty : dataRow.BeforeText,
				afterText: dataRow.IsAfterTextNull() ? string.Empty : dataRow.AfterText,
				keywords: dataRow.IsKeywordsNull() ? string.Empty : dataRow.Keywords,
				guid: dataRow.IsGuidNull() ? string.Empty : dataRow.Guid);
		}

		public static Subcategory Create(Category category, string caption, string description, bool isDecimal, bool isText, bool isObligatory, string beforeText, string afterText, string keywords)
		{
			return new Subcategory(category: category,
				id: -1,
				caption: caption,
				description: description,
				isDecimal: isDecimal,
				isText: isText,
				isObligatory: isObligatory,
				beforeText: beforeText,
				afterText: afterText,
				keywords: keywords,
				guid: System.Guid.NewGuid().ToString());
		}

		public static Subcategory Default(Category category)
		{
			return new Subcategory(category: category,
				id: -1,
				caption: "Не определено",
				description: "Значение подкатегории не определено",
				isDecimal: true,
				isText: true,
				isObligatory: false,
				string.Empty,
				string.Empty,
				string.Empty,
				System.Guid.NewGuid().ToString());
		}

		private Subcategory(Category category, int id, string caption, string description, bool isDecimal, bool isText, bool isObligatory, string beforeText, string afterText, string keywords, string guid)
		{
			Category = category;
			Id = id;
			Caption = caption;
			Description = description;
			IsDecimal = isDecimal;
			IsText = isText;
			IsObligatory = isObligatory;
			BeforeText = beforeText;
			AfterText = afterText;
			Keywords = keywords;
			Guid = guid;
		}

		public Category Category { get; }

		public int Id { get; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public bool IsDecimal { get; set; }

		public bool IsText { get; set; }

		public bool IsObligatory { get; set; }

		public string Code => Id.ToString();

		public string Text => Caption;

		public string BeforeText { get; set; }

		public string AfterText { get; set; }

		public string Keywords { get; set; }

		public string Guid { get; set; }

		long ComboControl<Subcategory>.IComboControlItem.Id => Id;

		string ComboControl<Subcategory>.IComboControlItem.Code => Code;

		public object[] ToObjectsArray()
		{
			return new object[]{ Id < 0 ? null: (object) Id,
				Category.Id,
				Caption,
				Description,
				IsDecimal,
				IsText,
				IsObligatory,
				Keywords};
		}

		public int CompareTo(Subcategory value)
		{
			return Compare(this, value);
		}

		public int CompareTo(Object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Subcategory s)
			{
				return s.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public static int Compare(Subcategory x, Subcategory y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					return x.Category.CompareTo(y.Category) == 0 ?
						x.Id.CompareTo(y.Id) : 0;
				}
				catch (Exception)
				{ return 0; }
			}
			else if (!Equals(x, null) & Equals(y, null))
			{ return 1; }
			else if (Equals(x, null) & !Equals(y, null))
			{ return -1; }
			else { return 0; }
		}

		public class SubcategoryComparer : IComparer<Subcategory>
		{
			public int Compare(Subcategory x, Subcategory y)
			{
				return Subcategory.Compare(x, y);
			}
		}
	}
}
