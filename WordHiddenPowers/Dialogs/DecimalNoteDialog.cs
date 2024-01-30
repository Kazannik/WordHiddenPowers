using System;
using WordHiddenPowers.Repositoryes.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class DecimalNoteDialog : CreateNoteDialog
    {
        public double Value
        {
            get
            {
                return numericTextBox1.Value;
            }
        }
        
        public DecimalNoteDialog(Repositoryes.RepositoryDataSet dataSet, Word.Selection selection): base(dataSet, selection)
        {
            InitializeComponent();
        }

        public DecimalNoteDialog(Note note): base(note)
        {
            InitializeComponent();
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            Text = numericTextBox1.Value.ToString();
        }       
    }
}
