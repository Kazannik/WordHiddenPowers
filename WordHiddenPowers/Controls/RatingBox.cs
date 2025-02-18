using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
	public partial class RatingBox : UserControl
	{
		protected const int CONTROL_HEIGHT = 42;

		public RatingBox()
		{
			InitializeComponent();

			ratingLabel.Text = ratingTrackBar.Value.ToString();
		}

		private void RatingBox_Resize(object sender, EventArgs e)
		{
			if (Height != CONTROL_HEIGHT)
				Height = CONTROL_HEIGHT;
			ratingTrackBar.Location = new Point(CONTROL_HEIGHT, 0);
			ratingTrackBar.Width = Width - CONTROL_HEIGHT;
		}

		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			ratingLabel.Text = ratingTrackBar.Value.ToString();
		}

		public int Value
		{
			get
			{
				return ratingTrackBar.Value;
			}
			set
			{
				ratingTrackBar.Value = value;
			}
		}
	}
}
