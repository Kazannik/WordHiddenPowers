using ControlLibrary.Controls.ComboControls;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Categories;

namespace WordHiddenPowers.Controls.ComboControls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	[ComVisible(false)]
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

			if (dataSet != null)
			{
				foreach (Category category in dataSet.GetCategories(isText))
				{
					Add(category);
				}
			}			
		}
		#endregion
	}
}
