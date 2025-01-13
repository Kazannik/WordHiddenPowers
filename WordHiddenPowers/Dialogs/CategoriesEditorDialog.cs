using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Categories;
using WordHiddenPowers.Utils;

namespace WordHiddenPowers.Dialogs
{
	public partial class CategoriesEditorDialog : Form
	{
		private readonly Documents.Document document;

		public CategoriesEditorDialog(Documents.Document document)
		{
			this.document = document;

			InitializeComponent();

			listBox1.DataSet = this.document.DataSet;

			AddCategoryFunctionEnabled();
			SelectCategoryType();
		}

		private void AddCategory_Click(object sender, EventArgs e)
		{
			//Category newCategory = Category.Create(caption: captionTextBox.Text, description: descriptionTextBox.Text, isObligatory: isObligatoryCheckBox.Checked);
			//TreeNode node = new TreeNode(captionTextBox.Text)
			//{
			//	Tag = newCategory
			//};
			//categoriesTreeView.Nodes.Add(node);
			//categoriesTreeView.SelectedNode = node;
			//AddCategoryFunctionEnabled();
		}


		private void SaveTableStructure()
		{
			document.DataSet.Categories.Clear();

			//foreach (TreeNode node in categoriesTreeView.Nodes)
			//{
			//	//pane.PowersDataSet.Categories.Rows.Add(new object[] { null, node.Category.Caption, node.Category.Description });
			//}


		}

		private void CaptionTextBox_TextChanged(object sender, EventArgs e)
		{
			AddCategoryFunctionEnabled();
		}

		private void CategoriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
			{
				if (e.Node.Tag is Category)
				{
					Category category = e.Node.Tag as Category;
					ReadProperties(category);
				}
				else if (e.Node.Tag is Subcategory)
				{
					Subcategory subcategory = e.Node.Tag as Subcategory;
					ReadProperties(subcategory);
				}
			}
		}


		private void SelectCategoryType()
		{
			//if (categoriesTreeView.SelectedNode == null)
			//{
			//	typeLabel.Text = "Редактрование категории";
			//	isDecimalCheckBox.Visible = false;
			//	isTextCheckBox.Visible = false;
			//	captionTextBox.Text = string.Empty;
			//	descriptionTextBox.Text = string.Empty;
			//	isObligatoryCheckBox.Checked = false;
			//}
			//else
			//{
			//	if (categoriesTreeView.SelectedNode.Tag is Category category)
			//		ReadProperties(category);
			//	else if (categoriesTreeView.SelectedNode.Tag is Subcategory subcategory)
			//		ReadProperties(subcategory);
			//}
		}
		private void ReadProperties(Category category)
		{
			typeLabel.Text = "Редактрование категории";
			isDecimalCheckBox.Visible = false;
			isTextCheckBox.Visible = false;
			captionTextBox.Text = category.Caption;
			descriptionTextBox.Text = category.Description;
			isObligatoryCheckBox.Checked = category.IsObligatory;
		}

		private void ReadProperties(Subcategory subcategory)
		{
			typeLabel.Text = "Редактрование подкатегории";
			isDecimalCheckBox.Visible = true;
			isTextCheckBox.Visible = true;
			captionTextBox.Text = subcategory.Caption;
			descriptionTextBox.Text = subcategory.Description;
			isObligatoryCheckBox.Checked = subcategory.IsObligatory;
			isDecimalCheckBox.Checked = subcategory.IsDecimal;
			isTextCheckBox.Checked = subcategory.IsText;
		}



		private void WriteProperies(Subcategory subcategory)
		{
			//    IList<string> list = new List<string>();
			//    foreach (SubcategoryTreeNode item in node.Parent.Nodes)
			//    {
			//        list.Add(item.Subcategory.Caption);
			//    }
			//    if (!list.Contains(captionTextBox.Text))
			//    {
			//        node.Name = captionTextBox.Text;
			//        node.Text = captionTextBox.Text;
			//        node.Subcategory.Caption = captionTextBox.Text;
			//        node.Subcategory.Description = descriptionTextBox.Text;
			//        node.Subcategory.IsObligatory = isObligatoryCheckBox.Checked;
			//        node.Subcategory.IsDecimal = isDecimalCheckBox.Checked;
			//        node.Subcategory.IsText = isTextCheckBox.Checked;
			//    }               
		}



		private void AddCategoryFunctionEnabled()
		{
			//bool enabled = !string.IsNullOrWhiteSpace(captionTextBox.Text);
			//if (enabled)
			//{
			//	enabled = !categoriesTreeView.Nodes.ContainsKey(captionTextBox.Text);
			//}
			//addCategoryButton.Enabled = enabled;
			//mnuCategoriesAddCategory.Enabled = enabled;
		}

		private void Controls_ValueChanged(object sender, EventArgs e)
		{
			//if (categoriesTreeView.SelectedNode != null)
			//{
			//	if (categoriesTreeView.SelectedNode.Tag is Category)
			//	{
			//		Category category = categoriesTreeView.SelectedNode.Tag as Category;
			//		if (!Contains(categoriesTreeView.SelectedNode, captionTextBox.Text))
			//		{
			//			categoriesTreeView.SelectedNode.Text = captionTextBox.Text;
			//			categoriesTreeView.SelectedNode.Name = captionTextBox.Text;
			//			category.Caption = captionTextBox.Text;
			//			category.Description = descriptionTextBox.Text;
			//			category.IsObligatory = isObligatoryCheckBox.Checked;
			//		}
			//	}
			//	else if (categoriesTreeView.SelectedNode.Tag is Subcategory subcategory)
			//	{
			//		WriteProperies(subcategory);
			//	}
			//}
			//else
			//{
			//	AddCategoryFunctionEnabled();
			//}
		}

		private bool Contains(TreeNode selectedNode, string caption)
		{
			//TreeNodeCollection nodes = selectedNode.Parent == null ? categoriesTreeView.Nodes : selectedNode.Parent.Nodes;

			//foreach (TreeNode node in nodes)
			//{
			//	if (node != selectedNode)
			//	{
			//		if (node.Tag is Category && (node.Tag as Category).Caption == caption)
			//			return true;
			//		else if (node.Tag is Subcategory && (node.Tag as Subcategory).Caption == caption)
			//			return true;
			//	}
			//}
			return false;
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = "Текстовый документ (.txt)|*.txt",
				Multiselect = false
			};
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				document.DataSet.Categories.Clear();
				document.DataSet.Subcategories.Clear();

				string content = File.ReadAllText(dialog.FileName, Encoding.GetEncoding(1251));
				Utils.Categories.CreateFromText(document.DataSet, content);
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{

		}

		private void CategoriesEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (document.DataSet.HasChanges())
				{
					DialogResult result = MessageBox.Show("Зафиксировать категории данных?", "Категории данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						Content.CommitVariable(document.Doc.Variables, Const.Globals.XML_VARIABLE_NAME, document.DataSet);
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}
	}
}
