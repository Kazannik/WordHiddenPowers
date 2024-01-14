using System;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class DecimalNoteDialog : Form
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

        public double Value
        {
            get
            {
                return numericTextBox1.Value;
            }
        }

        public string Description
        {
            get
            {
                return descriptionTextBox.Text;
            }
        }

        public DecimalNoteDialog(Word.Selection selection)
        {
            InitializeComponent();

            SelectionText = selection.Text;
            SelectionStart = selection.Start;
            SelectionEnd = selection.End;
        }

        public DecimalNoteDialog(Note note)
        {
            InitializeComponent();

            numericTextBox1.Value = (double)note.Value;
            raitingBox.Value = note.Reiting;
            descriptionTextBox.Text = note.Description;

            SelectionText = string.Empty;
            SelectionStart = note.WordSelectionStart;
            SelectionEnd = note.WordSelectionEnd;
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            Text = numericTextBox1.Value.ToString();
        }
    }
}
