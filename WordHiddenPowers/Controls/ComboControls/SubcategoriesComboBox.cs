using ControlLibrary.Controls.ComboControls;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;

namespace WordHiddenPowers.Controls.ComboControls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	[ComVisible(false)]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
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
			foreach (RepositoryDataSet.SubcategoriesRow dataRow in dataSet.Subcategories.GetSubcategoriesRows(category.Guid, isText))
			{
				Subcategory subcategory = Subcategory.Create(category, dataRow);
				Add(subcategory);
			}
		}
	}
}
