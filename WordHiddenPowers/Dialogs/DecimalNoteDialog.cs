using System;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Notes;
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

		public DecimalNoteDialog(RepositoryDataSet dataSet, Word.Selection selection) : base(dataSet, selection, false)
		{
			InitializeComponent();
		}

		public DecimalNoteDialog(RepositoryDataSet dataSet, Note note) : base(dataSet, note, false)
		{
			InitializeComponent();
			numericTextBox1.Value = long.Parse(note.Value.ToString());
		}

		private void ValueTextBox_TextChanged(object sender, EventArgs e)
		{
			Text = numericTextBox1.Value.ToString();
		}
	}
}
