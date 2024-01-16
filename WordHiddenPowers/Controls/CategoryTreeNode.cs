using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Controls
{
    public class CategoryTreeNode: TreeNode
    {
        public Category Category { get; }

        public CategoryTreeNode(Category category):base(text: category.Caption)
        {
            Category = category;

        }

        public CategoryTreeNode(string caption, string description): base(text: caption)
        {
            Category = Category.Create(caption: caption, description: description);
        }
    }
}
