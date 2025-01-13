using ControlLibrary.Controls.ComboControls;
using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositoryes.Categories
{
	public class Category : IComparable<Category>, ComboControl<Category>.IComboControlItem
	{
		public static Category Create(DataRow dataRow)
		{
			return new Category(id: (int)dataRow["id"],
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("Description") ? string.Empty : dataRow["Description"] as string,
				isObligatory: (bool)dataRow["IsObligatory"],
				beforeText: dataRow.IsNull("BeforeText") ? string.Empty : dataRow["BeforeText"] as string,
				afterText: dataRow.IsNull("AfterText") ? string.Empty : dataRow["AfterText"] as string);
		}

		public static Category Create(RepositoryDataSet.CategoriesRow dataRow)
		{
			return new Category(id: dataRow.id,
				caption: dataRow.IsCaptionNull() ? string.Empty : dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				isObligatory: dataRow.IsObligatory,
				beforeText: dataRow.IsBeforeTextNull() ? string.Empty : dataRow.BeforeText,
				afterText: dataRow.IsAfterTextNull() ? string.Empty : dataRow.AfterText);
		}

		public static Category Create(string caption, string description, bool isObligatory, string beforeText, string afterText)
		{
			return new Category(id: -1,
				caption: caption,
				description: description,
				isObligatory: isObligatory,
				beforeText: beforeText,
				afterText: afterText);
		}

		public static Category Default()
		{
			return new Category(id: 0,
				caption: "Не определено",
				description: "Значение категории не определено",
				isObligatory: false,
				beforeText: string.Empty,
				afterText: string.Empty);
		}

		private Category(int id, string caption, string description, bool isObligatory, string beforeText, string afterText)
		{
			Id = id;
			Caption = caption;
			Description = description;
			IsObligatory = isObligatory;
			BeforeText = beforeText;
			AfterText = afterText;
		}

		public int Id { get; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public bool IsObligatory { get; set; }

		public string Code => Id.ToString();

		public string Text => Caption;

		public string BeforeText { get; set; }

		public string AfterText { get; set; }
				
		long ComboControl<Category>.IComboControlItem.Id => Id;

		string ComboControl<Category>.IComboControlItem.Code => Code;

		public object[] ToObjectsArray()
		{
			return new object[] {
				Id < 0 ? null: (object) Id,
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

		public static int Compare(Category x, Category y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					return x.Id.CompareTo(y.Id);
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
