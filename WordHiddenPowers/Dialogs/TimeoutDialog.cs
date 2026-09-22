using System;
using System.Windows.Forms;
using WordHiddenPowers.Controls;

namespace WordHiddenPowers.Dialogs
{
	public partial class TimeoutDialog : Form
	{
		public TimeoutDialog()
		{
			InitializeComponent();
		}

		public TimeoutDialog(int pingTimeout, TimeSpan timeout, int maxPingTimeout, TimeSpan maxTimeout) : this()
		{
			pingTimeoutNumericUpDown.Maximum = maxPingTimeout;
			pingTimeoutNumericUpDown.Value = pingTimeout;
			timeoutBox.MaxValue = maxTimeout;
			timeoutBox.Value = timeout;
		}

		public TimeoutDialog(TimeSpan timeout, TimeSpan maxTimeout) : this()
		{
			pingTimeoutNumericUpDown.Value = 0;
			pingTimeoutLabel.Visible = false;
			pingTimeoutNumericUpDown.Visible = false;
			timeoutBox.MaxValue = maxTimeout;
			timeoutBox.Value = timeout;
		}

		public int PingTimeout => (int)pingTimeoutNumericUpDown.Value;

		public TimeSpan Timeout => timeoutBox.Value;
		
	}
}
