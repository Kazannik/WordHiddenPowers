using System.ComponentModel;
using System.Diagnostics;
using WordHiddenPowers.Categories;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Controls
{
    public class SubcategoriesComboBox : ComboControl<SubcategoriesComboBox.SubcategoriesItem>
    {
        #region Initialize

        public SubcategoriesComboBox() : base() { }

        [DebuggerNonUserCode()]
        public SubcategoriesComboBox(IContainer container) : base(container: container) { }

        public int Add(Subcategory subcategory)
        {
            return Add(new SubcategoriesItem(subcategory: subcategory));
        }

        #endregion

        public void InitializeSource(RepositoryDataSet dataSet, Category category)
        {
            Items.Clear();
            foreach (RepositoryDataSet.SubcategoriesRow dataRow in dataSet.Subcategories.Get(category.Id))
            {
                Subcategory subcategory = Subcategory.Create( category, dataRow);
                Add(subcategory);
            }
        }

        public class SubcategoriesItem : IComboBoxItem
        {
            public SubcategoriesItem(Subcategory subcategory)
            {
                Content = subcategory;
                Code = subcategory.Id;
                Text = subcategory.Caption;
            }

            public int Code { get; }
            public string Text { get; }
            public Subcategory Content { get; }
            public override string ToString()
            {
                return Text;
            }
        }
    }
}
