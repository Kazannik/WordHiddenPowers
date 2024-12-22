using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositoryes.Categories
{
	public class Category : IComparable<Category>, Controls.ComboControl<Category>.IComboBoxItem
	{
		public static Category Create(DataRow dataRow)
		{
			return new Category(id: (int)dataRow["id"],
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("Description") ? string.Empty : dataRow["Description"] as string,
				isObligatory: (bool)dataRow["IsObligatory"]);
		}

		public static Category Create(RepositoryDataSet.CategoriesRow dataRow)
		{
			return new Category(id: dataRow.id,
				caption: dataRow.IsCaptionNull() ? string.Empty : dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				isObligatory: dataRow.IsObligatory);
		}

		public static Category Create(string caption, string description, bool isObligatory)
		{
			return new Category(id: -1,
				caption: caption,
				description: description,
				isObligatory: isObligatory);
		}

		public static Category Default()
		{
			return new Category(id: 0,
				caption: "Не определено",
				description: "Значение категории не определено",
				isObligatory: false);
		}

		private Category(int id, string caption, string description, bool isObligatory)
		{
			Id = id;
			Caption = caption;
			Description = description;
			IsObligatory = isObligatory;
		}

		public int Id { get; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public bool IsObligatory { get; set; }

		public int Code
		{
			get
			{
				return Id;
			}
		}

		public string Text
		{
			get
			{
				return Caption;
			}
		}

		public object[] ToObjectsArray()
		{
			return (new object[] { Id < 0 ? null: (object) Id,
				Caption,
				Description,
				IsObligatory });
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
