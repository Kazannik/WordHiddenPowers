using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositoryes.Models
{
    public class Subcategory : IComparable<Subcategory>
    {
        public static Subcategory Create(Category category, DataRow dataRow)
        {
            return new Subcategory(category: category,
                id: (int)dataRow["id"],
                caption: dataRow["Caption"] as string,
                description: dataRow["Description"] as string,
                isDecimal: (bool)dataRow["IsDecimal"],
                isText: (bool)dataRow["IsText"], 
                isObligatory: (bool)dataRow["IsObligatory"]);
        }

        public static Subcategory Create(Category category, RepositoryDataSet.SubcategoriesRow dataRow)
        {
            return new Subcategory(category: category,
                id: dataRow.id,
                caption: dataRow.Caption,
                description: dataRow.Description,
                isDecimal: dataRow.IsDecimal,
                isText: dataRow.IsText,
                isObligatory: dataRow.IsObligatory);
        }

        public static Subcategory Create(Category category, string caption, string description, bool isDecimal, bool isText, bool isObligatory)
        {
            return new Subcategory(category: category,
                id: -1,
                caption: caption,
                description: description,
                isDecimal: isDecimal,
                isText: isText,
                isObligatory: isObligatory);
        }

        public static Subcategory Default(Category category)
        {
            return new Subcategory(category: category,
                id: -1,
                caption: "Не определено",
                description: "Значение подкатегории не определено",
                isDecimal: true,
                isText: true,
                isObligatory: false);
        }

        private Subcategory(Category category, int id, string caption, string description, bool isDecimal, bool isText, bool isObligatory)
        {
            Category = category;
            Id = id;
            Caption = caption;
            Description = description;
            IsDecimal = isDecimal;
            IsText = isText;
            IsObligatory = isObligatory;
        }

        public Category Category { get; }

        public int Id { get; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public bool IsDecimal { get; set; }

        public bool IsText { get; set; }

        public bool IsObligatory { get; set; }
        public object[] ToObjectsArray()
        {           
            return (new object[]{ Id < 0 ? null: (object) Id,
                Category.Id,
                Caption,
                Description,
                IsDecimal,
                IsText,
                IsObligatory });
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
            if (value is Subcategory)
            {
                Subcategory s = (Subcategory)value;
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
                    return Decimal.Compare(x.Category.Id, y.Category.Id) == 0 ?  
                        Decimal.Compare(x.Id, y.Id): 0;
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
