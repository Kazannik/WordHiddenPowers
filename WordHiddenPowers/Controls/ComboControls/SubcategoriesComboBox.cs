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
	public class SubcategoriesComboBox : ComboControl<Subcategory>
	{
		#region Initialize

		public SubcategoriesComboBox() : base() { }

		[DebuggerNonUserCode()]
		public SubcategoriesComboBox(IContainer container) : base(container: container) { }

		#endregion

		public void InitializeSource(DocumentDataSet dataSet, Category category, bool isText)
		{
			Items.Clear();

			if (dataSet != null && category != null)
			{
				foreach (DocumentDataSet.SubcategoriesRow dataRow in dataSet.Subcategories.GetSubcategoriesRows(category.Guid, isText))
				{
					Subcategory subcategory = Subcategory.Create(category, dataRow);
					Add(subcategory);
				}
			}
		}

		protected override Size OnMeasurePrefixBound(Graphics graphics, Font font)
		{
			return graphics.MeasureString("FFFFF", font).ToSize();
		}
	}
}
