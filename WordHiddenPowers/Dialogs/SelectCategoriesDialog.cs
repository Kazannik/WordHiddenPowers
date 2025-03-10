using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WordHiddenPowers.Controls.ListControls.CategoriesCheckedListControl;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Utils;

namespace WordHiddenPowers.Dialogs
{
    public partial class SelectCategoriesDialog: Form
    {
		private readonly Documents.Document document;

		public SelectCategoriesDialog(Documents.Document document)
		{
			this.document = document;

			this.Icon = WordUtil.GetIconMso("MindMapExportWord", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			InitializeComponent();

			checkedListBox1.DataSet = this.document.CurrentDataSet;
			
			foreach (ListItem item in checkedListBox1.Items)
			{
				item.IsChecked = true;
			}
			okButton.Enabled = checkedListBox1.IsAnyChecked;
		}

		public SelectCategoriesDialog()
        {
            InitializeComponent();
        }

		private void CheckButton_Click(object sender, EventArgs e)
		{
			foreach (ListItem item in checkedListBox1.Items)
			{
				item.IsChecked = true;
			}
			okButton.Enabled = checkedListBox1.IsAnyChecked;
		}

		private void UnckeckButton_Click(object sender, EventArgs e)
		{
			foreach (ListItem item in checkedListBox1.Items)
			{
				item.IsChecked = false;
			}
			okButton.Enabled = checkedListBox1.IsAnyChecked;
		}

		private void InverseButton_Click(object sender, EventArgs e)
		{
			foreach (ListItem item in checkedListBox1.Items)
			{
				item.IsChecked = !item.IsChecked;
			}
			okButton.Enabled = checkedListBox1.IsAnyChecked;
		}
		
		private void CheckedListBox1_ItemContentChanged(object sender, DrawItemEventArgs e)
		{
			okButton.Enabled = checkedListBox1.IsAnyChecked;
		}

		public IEnumerable<Subcategory> CheckedSubcategories => checkedListBox1.CheckedSubcategories;
	}
}
