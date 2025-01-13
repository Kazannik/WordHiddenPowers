using ControlLibrary.Controls.ComboControls;
using System.ComponentModel;
using System.Diagnostics;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;

namespace WordHiddenPowers.Controls.ComboControls
{
	public class SubcategoriesComboBox : ComboControl<Subcategory>
	{
		#region Initialize

		public SubcategoriesComboBox() : base() { }

		[DebuggerNonUserCode()]
		public SubcategoriesComboBox(IContainer container) : base(container: container) { }

		#endregion

		public void InitializeSource(RepositoryDataSet dataSet, Category category, bool isText)
		{
			Items.Clear();
			foreach (RepositoryDataSet.SubcategoriesRow dataRow in dataSet.Subcategories.Get(category.Id, isText))
			{
				Subcategory subcategory = Subcategory.Create(category, dataRow);
				Add(subcategory);
			}
		}
	}
}
