using ControlLibrary.Controls.ComboControls;
using System;
using System.Collections.Generic;
using System.Data;
using static WordHiddenPowers.Controls.ListControls.CategoriesListBox;

namespace WordHiddenPowers.Repositories.Categories
{
	public class Subcategory : IComparable<Subcategory>, ComboControl<Subcategory>.IComboControlItem, ICategoriesListItem
	{
		public static Subcategory Create(Category category, DataRow dataRow)
		{
			return new Subcategory(
				category: category,
				guid: dataRow["key_guid"] as string,
				position: (int)dataRow["position"],
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("Description") ? string.Empty : dataRow["Description"] as string,
				isDecimal: (bool)dataRow["IsDecimal"],
				isText: (bool)dataRow["IsText"],
				isObligatory: (bool)dataRow["IsObligatory"],
				beforeText: dataRow.IsNull("BeforeText") ? string.Empty : dataRow["BeforeText"] as string,
				afterText: dataRow.IsNull("AfterText") ? string.Empty : dataRow["AfterText"] as string,
				keywords: dataRow.IsNull("Keywords") ? string.Empty : dataRow["Keywords"] as string);
		}

		public static Subcategory Create(Category category, RepositoryDataSet.SubcategoriesRow dataRow)
		{
			return new Subcategory(
				category: category,
				guid: dataRow.key_guid,
				position: dataRow.position,
				caption: dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				isDecimal: dataRow.IsDecimal,
				isText: dataRow.IsText,
				isObligatory: dataRow.IsObligatory,
				beforeText: dataRow.IsBeforeTextNull() ? string.Empty : dataRow.BeforeText,
				afterText: dataRow.IsAfterTextNull() ? string.Empty : dataRow.AfterText,
				keywords: dataRow.IsKeywordsNull() ? string.Empty : dataRow.Keywords);
		}

		public static Subcategory Create(Category category, int position, string caption, string description, bool isDecimal, bool isText, bool isObligatory, string beforeText, string afterText, string keywords)
		{
			return new Subcategory(
				category: category,
				guid: System.Guid.NewGuid().ToString(),
				position: position,
				caption: caption,
				description: description,
				isDecimal: isDecimal,
				isText: isText,
				isObligatory: isObligatory,
				beforeText: beforeText,
				afterText: afterText,
				keywords: keywords);
		}

		public static Subcategory GetDefault(Category category)
		{
			return new Subcategory(
				category: category,
				guid: string.Empty,
				position: 0,
				caption: "Не определено",
				description: "Значение подкатегории не определено",
				isDecimal: true,
				isText: true,
				isObligatory: false,
				beforeText: string.Empty,
				afterText: string.Empty,
				keywords: string.Empty);
		}

		public static readonly Subcategory Dafault =
			new Subcategory(
				category: Category.Default,
				guid: string.Empty,
				position: 0,
				caption: "Не определено",
				description: "Значение подкатегории не определено",
				isDecimal: true,
				isText: true,
				isObligatory: false,
				beforeText: string.Empty,
				afterText: string.Empty,
				keywords: string.Empty);

		private Subcategory(Category category, string guid, int position, string caption, string description, bool isDecimal, bool isText, bool isObligatory, string beforeText, string afterText, string keywords)
		{
			Category = category;
			Guid = guid;
			Position = position;
			Caption = caption;
			Description = description;
			IsDecimal = isDecimal;
			IsText = isText;
			IsObligatory = isObligatory;
			BeforeText = beforeText;
			AfterText = afterText;
			Keywords = keywords;
		}
		
		public Category Category { get; }

		public string Guid { get; }

		public int Position { get; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public bool IsDecimal { get; set; }

		public bool IsText { get; set; }

		public bool IsObligatory { get; set; }

		public string Code => Position.ToString();

		public string Text => Caption;

		public string BeforeText { get; set; }

		public string AfterText { get; set; }

		public string Keywords { get; set; }

		long ComboControl<Subcategory>.IComboControlItem.Id => Position;

		string ComboControl<Subcategory>.IComboControlItem.Code => Code;

		ControlLibrary.Structures.Version ICategoriesListItem.Code => ControlLibrary.Structures.Version.Create(major: Category.Position, minor: Position, guid: Guid);

		public object[] ToObjectsArray()
		{
			return new object[]{
				Guid,
				Category.Guid,
				Position,
				Caption,
				Description,
				IsDecimal,
				IsText,
				IsObligatory,
				BeforeText,
				AfterText,
				Keywords};
		}

		public bool Equals(RepositoryDataSet.SubcategoriesRow dataRow)
		{
			if (Guid != dataRow.key_guid) return false;
			if (Category.Guid != dataRow.category_guid) return false;
			if (Position != dataRow.position)
			if (Caption != dataRow.Caption) return false;
			if (!string.IsNullOrEmpty(Description) && !dataRow.IsDescriptionNull() && Description != dataRow.Description) return false;
			if (IsDecimal != dataRow.IsDecimal) return false;
			if (IsText != dataRow.IsText) return false;
			if (IsObligatory != dataRow.IsObligatory) return false;
			if (!string.IsNullOrEmpty(BeforeText) && !dataRow.IsBeforeTextNull() && BeforeText != dataRow.BeforeText) return false;
			if (!string.IsNullOrEmpty(AfterText) && !dataRow.IsAfterTextNull() && AfterText != dataRow.AfterText) return false;
			if (!string.IsNullOrEmpty(Keywords) && !dataRow.IsKeywordsNull() && Keywords != dataRow.Keywords) return false;
			return true;
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
						x.Position.CompareTo(y.Position) : 0;
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
	
	    public object Tag { get; set; }
	
	}
}
