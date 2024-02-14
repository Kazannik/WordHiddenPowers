using WordHiddenPowers.Repositoryes;
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
        
        public TextNoteDialog(RepositoryDataSet dataSet, Word.Selection selection) : base(dataSet, selection, true)
        {
            InitializeComponent();

            valueTextBox.Text = selection.Text;
        }

        public TextNoteDialog(RepositoryDataSet dataSet, Note note): base(dataSet, note, true)
        {
            InitializeComponent();

            valueTextBox.Text = note.Value as string;
        }
    }
}
