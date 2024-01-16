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
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            categoriesTreeView.Nodes.Add(new CategoryTreeNode(captionTextBox.Text, descriptionTextBox.Text));
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
    }
}
