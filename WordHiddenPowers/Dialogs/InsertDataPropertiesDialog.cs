using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class InsertDataPropertiesDialog : Form
	{
		public InsertDataPropertiesDialog()
		{
			InitializeComponent();
		}

		public int MinRating { get => (int)minRatingNumericUpDown.Value; }

		public int MaxRating { get => (int)maxRatingNumericUpDown.Value; }

		public int NoteCount { get => (int)noteCountNumericUpDown.Value; }

		public bool ViewHide { get => viewHideCheckBox.Checked; }

		public bool AllRating { get => allRatingCheckBox.Checked; }

		private void AllRatingCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			minRatingNumericUpDown.Enabled = !allRatingCheckBox.Checked;
			maxRatingNumericUpDown.Enabled = !allRatingCheckBox.Checked;
		}
	}
}
