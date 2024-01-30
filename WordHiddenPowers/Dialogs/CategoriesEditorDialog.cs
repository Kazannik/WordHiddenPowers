using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Categories;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Dialogs
{
    public partial class CategoriesEditorDialog : Form
    {
        WordHiddenPowersPane pane;

        public CategoriesEditorDialog(WordHiddenPowersPane pane)
        {
            this.pane = pane;

            InitializeComponent();

            AddCategoryFunctionEnabled();
            SelectCategoryType();
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            Category newCategory = Category.Create(caption: captionTextBox.Text, description: descriptionTextBox.Text, isObligatory: isObligatoryCheckBox.Checked);
            TreeNode node = new TreeNode(captionTextBox.Text);
            node.Tag = newCategory;
            categoriesTreeView.Nodes.Add(node);
            categoriesTreeView.SelectedNode = node;
            AddCategoryFunctionEnabled();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }


        private void SaveTableStructure()
        {
            pane.PowersDataSet.Categories.Clear();

            foreach (TreeNode node in categoriesTreeView.Nodes)
            {
                //pane.PowersDataSet.Categories.Rows.Add(new object[] { null, node.Category.Caption, node.Category.Description });
            }

            pane.CommitVariables();
        }

        private void captionTextBox_TextChanged(object sender, EventArgs e)
        {
            AddCategoryFunctionEnabled();
        }

        private void categoriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
           if (e.Action== TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
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
            if (categoriesTreeView.SelectedNode == null)
            {
                typeLabel.Text = "Редактрование категории";
                isDecimalCheckBox.Visible = false;
                isTextCheckBox.Visible = false;
                captionTextBox.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;
                isObligatoryCheckBox.Checked = false;
            }
            else
            {
                if (categoriesTreeView.SelectedNode.Tag is Category)
                    ReadProperties((Category)categoriesTreeView.SelectedNode.Tag);
                else if (categoriesTreeView.SelectedNode.Tag is Subcategory)
                    ReadProperties((Subcategory)categoriesTreeView.SelectedNode.Tag);
            }
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
            bool enabled = !string.IsNullOrWhiteSpace(captionTextBox.Text);
            if (enabled)
            {
                enabled = !categoriesTreeView.Nodes.ContainsKey(captionTextBox.Text);
            }
            addCategoryButton.Enabled = enabled;
            mnuCategoriesAddCategory.Enabled = enabled;            
        }

        private void Controls_ValueChanged(object sender, EventArgs e)
        {
            if (categoriesTreeView.SelectedNode != null)
            {
                if (categoriesTreeView.SelectedNode.Tag is Category)
                {
                    Category category = categoriesTreeView.SelectedNode.Tag as Category;
                    if (!Contains(categoriesTreeView.SelectedNode, captionTextBox.Text))
                    {
                        categoriesTreeView.SelectedNode.Text = captionTextBox.Text;
                        categoriesTreeView.SelectedNode.Name = captionTextBox.Text;
                        category.Caption = captionTextBox.Text;
                        category.Description = descriptionTextBox.Text;
                        category.IsObligatory = isObligatoryCheckBox.Checked;
                    }
                }
                else if (categoriesTreeView.SelectedNode.Tag is Subcategory)
                {
                    WriteProperies((Subcategory)categoriesTreeView.SelectedNode.Tag);
               }
            }
            else
            {
                AddCategoryFunctionEnabled();
            }
        }

        private bool Contains(TreeNode selectedNode, string caption)
        {
            TreeNodeCollection nodes = selectedNode.Parent == null ? categoriesTreeView.Nodes:selectedNode.Parent.Nodes;

            foreach (TreeNode node in nodes)
            {
                if (node != selectedNode)
                {
                    if (node.Tag is Category && (node.Tag as Category).Caption == caption)
                        return true;
                    else if (node.Tag is Subcategory && (node.Tag as Subcategory).Caption == caption)
                        return true;
                }
            }
            return false;
        }

        private void FileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовый документ (.txt)|*.txt";
            dialog.Multiselect = false;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                pane.PowersDataSet.Categories.Clear();
                pane.PowersDataSet.Subcategories.Clear();

                string content = File.ReadAllText(dialog.FileName, Encoding.GetEncoding(1251));
                Utils.Categories.CreateFromText(pane.PowersDataSet, content);

                pane.CommitVariables();
            }
        }

        private void mnuFileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
