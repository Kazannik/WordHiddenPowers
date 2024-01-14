using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositoryes.Models
{
    public class Category: IComparable<Category>
    {

        public static Category Create(DataRow dataRow)
        {
            return new Category(id: (int)dataRow["id"],
                caption: dataRow["Caption"] as string,
                description: dataRow["Description"] as string);
        }

        public static Category Create(RepositoryDataSet.CategoriesRow dataRow)
        {
            return new Category(id: dataRow.id,
                caption: dataRow.Caption,
                description: dataRow.Description);
        }

        public static Category Default()
        {
            return new Category(id: 0,
                caption: "Не определено",
                description: "Значение категории не определено");
        }

        private Category(int id, string caption, string description)
        {
            Id = id;
            Caption = caption;
            Description = description;
        }

        public int Id { get; }

        public string Caption { get; }

        public string Description { get; }

        public object[] ToObjectsArray()
        {
            return (new object[]{ Id,
                Caption,
                Description });
        }

        public int CompareTo(Category value)
        {
            return Compare(this, value);
        }

        public int CompareTo(Object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (value is Category)
            {
                Category c = (Category)value;
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
                    return Decimal.Compare(x.Id, y.Id);
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
