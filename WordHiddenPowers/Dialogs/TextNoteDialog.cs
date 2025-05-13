using System.Drawing;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class TextNoteDialog : NoteDialog
	{
		public string Value => valueTextBox.Text;
		
		public TextNoteDialog(RepositoryDataSet dataSet, Word.Selection selection) : base(dataSet, selection, true)
		{
			InitializeComponent();
			valueTextBox.Text = selection.Text;
		}

		public TextNoteDialog(RepositoryDataSet dataSet, Note note) : base(dataSet, note, true)
		{
			InitializeComponent();
			valueTextBox.Text = note.Value as string;
		}
				
		protected override void ControlsResize()
		{
			base.ControlsResize();

			valueTextBox.Location = new Point(12, ControlTop);
			valueTextBox.Size = new Size(ClientSize.Width - 24, ControlHeight);
			MinimumSize = new Size(500, MinHeight);
		}		
	}
}
