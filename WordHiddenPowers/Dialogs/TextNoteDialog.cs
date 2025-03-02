using System.Drawing;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Notes;
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

			ControlsResize();

			valueTextBox.Text = selection.Text;
		}

		public TextNoteDialog(RepositoryDataSet dataSet, Note note) : base(dataSet, note, true)
		{
			InitializeComponent();
			ControlsResize();

			valueTextBox.Text = note.Value as string;
		}

		private void TextNoteDialog_Resize(object sender, System.EventArgs e)
		{
			ControlsResize();
		}

		private void ControlsResize()
		{
			valueTextBox.Location = new Point(12, ControlTop);
			valueTextBox.Size = new Size(ClientSize.Width - 24, ControlHeight);
			MinimumSize = new Size(500, MinHeight);
		}
	}
}
