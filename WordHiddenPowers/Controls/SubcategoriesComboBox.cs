using System.ComponentModel;
using System.Diagnostics;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Controls
{
    public class SubcategoriesComboBox : ComboControl<SubcategoriesComboBox.SubcategoriesItem, Subcategory>
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
