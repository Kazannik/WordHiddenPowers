using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.Design;
using WordHiddenPowers.Categories;
using WordHiddenPowers.Repositoryes;

namespace WordHiddenPowers.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class CategoriesComboBox : ComboControl<Category>
    {
        #region Initialize

        public CategoriesComboBox() : base() { }

        [DebuggerNonUserCode()]
        public CategoriesComboBox(IContainer container):base(container: container) { }
        

        public void InitializeSource(RepositoryDataSet dataSet)
        {
            Items.Clear();

            foreach (RepositoryDataSet.CategoriesRow dataRow in dataSet.Categories)
            {
                Category category = Category.Create(dataRow);
                Add(category);
            }
        }      

        #endregion       
    }
}
