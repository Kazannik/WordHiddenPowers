using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class SelectCategoryStringDialog : Form
    {
        public string SelectionText { get; }

        public int SelectionStart { get; }

        public int SelectionEnd { get; }

        public int Rating
        {
            get
            {
                return raitingBox.Value;
            }
        }

        public string Value
        {
            get
            {
                return valueTextBox.Text;
            }
        }

        public string Description
        {
            get
            {
                return descriptionTextBox.Text;
            }
        }

        public SelectCategoryStringDialog(Word.Selection selection)
        {
            InitializeComponent();
            valueTextBox.Text = selection.Text;
            SelectionText = selection.Text;
            SelectionStart = selection.Start;
            SelectionEnd = selection.End;
        }
    }
}
