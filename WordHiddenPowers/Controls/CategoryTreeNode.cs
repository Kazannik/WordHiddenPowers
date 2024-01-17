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
            Name = category.Caption;
        }
               

        public CategoryTreeNode(string caption, string description, bool isObligatory) : base(text: caption)
        {
            Category = Category.Create(caption: caption, description: description, isObligatory:isObligatory);
            Name = caption;
        }        
    }
}
