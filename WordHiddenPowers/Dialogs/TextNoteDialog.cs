using WordHiddenPowers.Repositoryes.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class TextNoteDialog : CreateNoteDialog
    {
        
        public string Value
        {
            get
            {
                return valueTextBox.Text;
            }
        }
        
        public TextNoteDialog(Repositoryes.RepositoryDataSet dataSet, Word.Selection selection) : base(dataSet, selection)
        {
            InitializeComponent();

            valueTextBox.Text = selection.Text;
        }

        public TextNoteDialog(Note note): base(note)
        {
            InitializeComponent();

            valueTextBox.Text = note.Value as string;
        }
    }
}
