using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
	public partial class ReitingBox : UserControl
	{
		protected const int CONTROL_HEIGHT = 42;

		public ReitingBox()
		{
			InitializeComponent();

			ratingLabel.Text = reitingTrackBar.Value.ToString();
		}

		private void ReitingBox_Resize(object sender, EventArgs e)
		{
			if (Height != CONTROL_HEIGHT)
				Height = CONTROL_HEIGHT;
			reitingTrackBar.Location = new Point(CONTROL_HEIGHT, 0);
			reitingTrackBar.Width = Width - CONTROL_HEIGHT;
		}

		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			ratingLabel.Text = reitingTrackBar.Value.ToString();
		}

		public int Value
		{
			get
			{
				return reitingTrackBar.Value;
			}
			set
			{
				reitingTrackBar.Value = value;
			}
		}
	}
}
