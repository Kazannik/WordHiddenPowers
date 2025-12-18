// Ignore Spelling: Dialogs Prosecutorial

using System.Windows.Forms;

#if WORD
namespace WordHiddenPowers.Dialogs
#else
namespace ProsecutorialSupervision.Dialogs
#endif
{
	public partial class ProgressDialog : Form
	{
		public ProgressDialog()
		{
			InitializeComponent();
			ProgressBar.Minimum = 0;
			ProgressBar.Maximum = 100;
		}

		public int Percent
		{
			get => ProgressBar.Value;
			set
			{
				ProgressBar.Value = value;
				ProgressLabel.Text = value.ToString() + " %";
				Refresh();
			}		
		}		
	}
}
