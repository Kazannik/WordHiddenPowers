using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.Design;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class CategoriesComboBox : ComboControl<CategoriesComboBox.CategoriesItem, Category>
    {
        #region Initialize

        public CategoriesComboBox() : base() { }

        [DebuggerNonUserCode()]
        public CategoriesComboBox(IContainer container):base(container: container) { }
        
        public int Add(Category category)
        {
            return Add(new CategoriesItem(category: category));
        }

        #endregion

        public class CategoriesItem : IComboBoxItem
        {
            public CategoriesItem(Category category)
            {
                Content = category;
                Code = category.Id;
                Text = category.Caption;
            }

            public int Code { get; }
            public string Text { get; }
            public Category Content { get; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
