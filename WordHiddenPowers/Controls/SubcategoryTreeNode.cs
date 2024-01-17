using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Models;

namespace WordHiddenPowers.Controls
{
    public class SubcategoryTreeNode : TreeNode
    {
        public Subcategory Subcategory { get; }

        public SubcategoryTreeNode(Subcategory subcategory):base(text: subcategory.Caption)
        {
            Subcategory = subcategory;
            Name = subcategory.Caption;
        }
        
        public SubcategoryTreeNode(Category category, int id, string caption, string description, bool isDecimal, bool isText, bool isObligatory) : base(text: caption)
        {
            Subcategory = Subcategory.Create(category: category, caption: caption, description: description, isDecimal: isDecimal, isText: isText, isObligatory: isObligatory);
            Name = caption;
        }
    }
}
