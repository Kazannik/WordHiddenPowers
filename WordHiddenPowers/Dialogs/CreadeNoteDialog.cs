using System;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class CreateNoteDialog : Form
    {
        private Repositoryes.RepositoryDataSet dataSet;

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

        
        public string Description
        {
            get
            {
                return descriptionTextBox.Text;
            }
        }

        public CreateNoteDialog()
        {
            InitializeComponent();
        }

        public CreateNoteDialog(Repositoryes.RepositoryDataSet dataSet, Word.Selection selection)
        {
            this.dataSet = dataSet;

            InitializeComponent();

            SelectionText = selection.Text;
            SelectionStart = selection.Start;
            SelectionEnd = selection.End;

            categoriesComboBox.InitializeSource(this.dataSet);

        }

        public CreateNoteDialog(Note note)
        {
            InitializeComponent();

            raitingBox.Value = note.Reiting;
            descriptionTextBox.Text = note.Description;

            SelectionText = string.Empty;
            SelectionStart = note.WordSelectionStart;
            SelectionEnd = note.WordSelectionEnd;
        }


        protected override void OnResize(EventArgs e)
        {
            okButton.Location = new System.Drawing.Point(Width - 242, Height - 89);
            cancelButton.Location = new System.Drawing.Point(Width - 132, Height - 89);
            raitingBox.Location = new System.Drawing.Point(16, Height - 95);
            descriptionTextBox.Location = new System.Drawing.Point(16, Height - 167);

            base.OnResize(e);
        }
    }
}
