using System;
using System.Drawing;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Notes;
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
			ControlsResize();
		}

		public DecimalNoteDialog(RepositoryDataSet dataSet, Note note) : base(dataSet, note, false)
		{
			InitializeComponent();
			ControlsResize();
			numericTextBox1.Value = long.Parse(note.Value.ToString());
		}

		private void ValueTextBox_TextChanged(object sender, EventArgs e)
		{
			Text = numericTextBox1.Value.ToString();
		}

		private void DecimalNoteDialog_Resize(object sender, EventArgs e)
		{
			ControlsResize();
		}

		private void ControlsResize()
		{
			numericTextBox1.Location = new Point(12, ControlTop);
			MinimumSize = new Size(500, MinHeight);
		}
	}
}
