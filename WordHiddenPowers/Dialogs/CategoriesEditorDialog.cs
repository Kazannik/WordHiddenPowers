using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Controls;
using WordHiddenPowers.Panes;

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
            categoriesTreeView.Nodes.Add(new CategoryTreeNode(captionTextBox.Text, descriptionTextBox.Text, isObligatoryCheckBox.Checked));
            AddCategoryFunctionEnabled();
            SelectCategoryType();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }


        private void SaveTableStructure()
        {
            pane.PowersDataSet.Categories.Clear();

            foreach (CategoryTreeNode node in categoriesTreeView.Nodes)
            {
                pane.PowersDataSet.Categories.Rows.Add(new object[] { null, node.Category.Caption, node.Category.Description });
            }

            pane.CommitVariables();
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            captionPanel.Location = new Point(0, 0);
            captionPanel.Width = splitContainer1.Panel2.Width - splitContainer1.SplitterWidth;
            captionPanel.Height = typeLabel.Height + splitContainer1.SplitterWidth;
            typeLabel.Location = new Point(splitContainer1.SplitterWidth, 2 + (captionPanel.Height - typeLabel.Height) / 2);

            captionLabel.Location = new Point(0, captionPanel.Height + splitContainer1.SplitterWidth);
            captionTextBox.Location = new Point(0, captionLabel.Top + captionLabel.Height + 2);
            captionTextBox.Width = splitContainer1.Panel2.Width - splitContainer1.SplitterWidth;
            descriptionLabel.Location = new Point(0, captionTextBox.Top + captionTextBox.Height + splitContainer1.SplitterWidth);

            isObligatoryCheckBox.Location = new Point(0, splitContainer1.Panel2.Height - isObligatoryCheckBox.Height - splitContainer1.SplitterWidth);
            isDecimalCheckBox.Location = new Point(isObligatoryCheckBox.Width + splitContainer1.SplitterWidth, isObligatoryCheckBox.Top);
            isTextCheckBox.Location = new Point(isDecimalCheckBox.Left + isDecimalCheckBox.Width + splitContainer1.SplitterWidth, isObligatoryCheckBox.Top);

            descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height + 2);
            descriptionTextBox.Width = splitContainer1.Panel2.Width - splitContainer1.SplitterWidth;
            descriptionTextBox.Height = splitContainer1.Panel2.Height - descriptionTextBox.Top - isObligatoryCheckBox.Height - splitContainer1.SplitterWidth * 2;
        }

        private void captionTextBox_TextChanged(object sender, EventArgs e)
        {
            AddCategoryFunctionEnabled();
        }

        private void categoriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectCategoryType();
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
                if (categoriesTreeView.SelectedNode is CategoryTreeNode)
                {
                    typeLabel.Text = "Редактрование категории";
                    CategoryTreeNode node = (CategoryTreeNode)categoriesTreeView.SelectedNode;
                    isDecimalCheckBox.Visible = false;
                    isTextCheckBox.Visible = false;
                    captionTextBox.Text = node.Category.Caption;
                    descriptionTextBox.Text = node.Category.Description;
                    isObligatoryCheckBox.Checked = node.Category.IsObligatory;
                }
                else if (categoriesTreeView.SelectedNode is SubcategoryTreeNode)
                {
                    typeLabel.Text = "Редактрование подкатегории";
                    SubcategoryTreeNode node = (SubcategoryTreeNode)categoriesTreeView.SelectedNode;
                    isDecimalCheckBox.Visible = true;
                    isTextCheckBox.Visible = true;
                    captionTextBox.Text = node.Subcategory.Caption;
                    descriptionTextBox.Text = node.Subcategory.Description;
                    isObligatoryCheckBox.Checked = node.Subcategory.IsObligatory;
                    isDecimalCheckBox.Checked = node.Subcategory.IsDecimal;
                    isTextCheckBox.Checked = node.Subcategory.IsText;
                }
            }
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

       
        private void categoriesTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (categoriesTreeView.SelectedNode != null)
            {
                if (categoriesTreeView.SelectedNode is CategoryTreeNode)
                {
                    CategoryTreeNode node = (CategoryTreeNode)categoriesTreeView.SelectedNode;
                    if (!categoriesTreeView.Nodes.ContainsKey(captionTextBox.Text))
                    {
                        node.Name = captionTextBox.Text;
                        node.Text = captionTextBox.Text;
                    }
                    node.Category.Caption = captionTextBox.Text;
                    node.Category.Description = descriptionTextBox.Text;
                    node.Category.IsObligatory = isObligatoryCheckBox.Checked;
                }
                else if (categoriesTreeView.SelectedNode is SubcategoryTreeNode)
                {
                    SubcategoryTreeNode node = (SubcategoryTreeNode)categoriesTreeView.SelectedNode;
                    node.Name = captionTextBox.Text;
                    node.Text = captionTextBox.Text;
                    node.Subcategory.Caption = captionTextBox.Text;
                    node.Subcategory.Description = descriptionTextBox.Text;
                    node.Subcategory.IsObligatory = isObligatoryCheckBox.Checked;
                    node.Subcategory.IsDecimal = isDecimalCheckBox.Checked;
                    node.Subcategory.IsText = isTextCheckBox.Checked;
                }
            }
        }
    }
}
