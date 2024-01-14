using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TextNoteDialog : Form
    {
        public string SelectionText { get; }

        public int SelectionStart { get; }

        public int SelectionEnd { get; }

        public int Reiting
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

        public TextNoteDialog(Word.Selection selection)
        {
            InitializeComponent();

            valueTextBox.Text = selection.Text;
            SelectionText = selection.Text;
            SelectionStart = selection.Start;
            SelectionEnd = selection.End;
        }

        public TextNoteDialog(Note note)
        {
            InitializeComponent();

            valueTextBox.Text = note.Value as string;
            raitingBox.Value = note.Reiting;
            descriptionTextBox.Text = note.Description;

            SelectionText = string.Empty;
            SelectionStart = note.WordSelectionStart;
            SelectionEnd = note.WordSelectionEnd;
        }
    }
}
