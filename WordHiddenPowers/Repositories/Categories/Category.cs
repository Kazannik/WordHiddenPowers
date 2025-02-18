using ControlLibrary.Controls.ComboControls;
using System;
using System.Collections.Generic;
using System.Data;
using static WordHiddenPowers.Controls.ListControls.CategoriesListBox;

namespace WordHiddenPowers.Repositories.Categories
{
	public class Category : IComparable<Category>, ComboControl<Category>.IComboControlItem, ICategoriesListItem
	{
		public static Category Create(DataRow dataRow)
		{
			return Create(
				guid: dataRow["key_guid"] as string,
				position: (int)dataRow["position"],
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("Description") ? string.Empty : dataRow["Description"] as string,
				isObligatory: (bool)dataRow["IsObligatory"],
				beforeText: dataRow.IsNull("BeforeText") ? string.Empty : dataRow["BeforeText"] as string,
				afterText: dataRow.IsNull("AfterText") ? string.Empty : dataRow["AfterText"] as string);
		}

		public static Category Create(RepositoryDataSet.CategoriesRow dataRow)
		{
			return Create(
				guid: dataRow.key_guid,
				position: dataRow.position,
				caption: dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				isObligatory: dataRow.IsObligatory,
				beforeText: dataRow.IsBeforeTextNull() ? string.Empty : dataRow.BeforeText,
				afterText: dataRow.IsAfterTextNull() ? string.Empty : dataRow.AfterText);
		}

		public static Category Create(int position, string caption, string description, bool isObligatory, string beforeText, string afterText)
		{
			return Create(
				guid: System.Guid.NewGuid().ToString(),
				position: position,
				caption: caption,
				description: description,
				isObligatory: isObligatory,
				beforeText: beforeText,
				afterText: afterText);
		}

		public static Category Create(string guid, int position, string caption, string description, bool isObligatory, string beforeText, string afterText)
		{
			return new Category(
				guid: guid,
				position: position,
				caption: caption,
				description: description,
				isObligatory: isObligatory,
				beforeText: beforeText,
				afterText: afterText);
		}

		public static readonly Category Default = new Category(
			guid: string.Empty,
			position: 0,
			caption: "Не определено",
			description: "Значение категории не определено",
			isObligatory: false,
			beforeText: string.Empty,
			afterText: string.Empty);
		

		private Category(string guid, int position, string caption, string description, bool isObligatory, string beforeText, string afterText)
		{
			Guid = guid;
			Position = position;
			Caption = caption;
			Description = description;
			IsObligatory = isObligatory;
			BeforeText = beforeText;
			AfterText = afterText;
		}

		public string Guid { get; }

		public int Position { get; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public bool IsObligatory { get; set; }

		public string Code => Position.ToString();

		public string Text => Caption;

		public string BeforeText { get; set; }

		public string AfterText { get; set; }
				
		long ComboControl<Category>.IComboControlItem.Id => Position;

		string ComboControl<Category>.IComboControlItem.Code => Code;

		ControlLibrary.Structures.Version ICategoriesListItem.Code => ControlLibrary.Structures.Version.Create(major: Position, guid: Guid);

		public object[] ToObjectsArray()
		{
			return new object[] {
				Guid,
				Position,
				Caption,
				Description,
				IsObligatory,
				BeforeText,
				AfterText};
		}

		public int CompareTo(Category value)
		{
			return Compare(this, value);
		}

		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Category c)
			{
				return c.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public bool Equals(RepositoryDataSet.CategoriesRow dataRow)
		{
			if (Guid != dataRow.key_guid) return false;
			if (Position != dataRow.position) return false;
			if (Caption != dataRow.Caption) return false;
			if (!string.IsNullOrEmpty(Description) && !dataRow.IsDescriptionNull() && Description != dataRow.Description) return false;
			if (IsObligatory != dataRow.IsObligatory) return false;
			if (!string.IsNullOrEmpty(BeforeText) && !dataRow.IsBeforeTextNull() &&	BeforeText != dataRow.BeforeText) return false;
			if (!string.IsNullOrEmpty(AfterText) && !dataRow.IsAfterTextNull() && AfterText != dataRow.AfterText) return false;
			return true;
		}

		public static int Compare(Category x, Category y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					return x.Guid.CompareTo(y.Guid);
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

		public class CategoryComparer : IComparer<Category>
		{
			public int Compare(Category x, Category y)
			{
				return Category.Compare(x, y);
			}
		}
	}
}
