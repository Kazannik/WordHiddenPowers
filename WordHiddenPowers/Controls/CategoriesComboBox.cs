using ControlLibrary.Controls.ComboControls;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.Design;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;

namespace WordHiddenPowers.Controls
{
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public class CategoriesComboBox : ComboControl<Category>
	{
		#region Initialize

		public CategoriesComboBox() : base() { }

		[DebuggerNonUserCode()]
		public CategoriesComboBox(IContainer container) : base(container: container) { }

		public void InitializeSource(RepositoryDataSet dataSet, bool isText)
		{
			Items.Clear();

			foreach (RepositoryDataSet.CategoriesRow dataRow in dataSet.GetCategories(isText))
			{
				Category category = Category.Create(dataRow);
				Add(category);
			}
		}

		#endregion
	}
}
