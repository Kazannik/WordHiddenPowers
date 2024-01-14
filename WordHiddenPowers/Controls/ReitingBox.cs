using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
    public partial class ReitingBox : UserControl
    {
        public ReitingBox()
        {
            InitializeComponent();

            ratingLabel.Text = reitingTrackBar.Value.ToString();
        }

        private void ReitingBox_Resize(object sender, EventArgs e)
        {
            if (this.Height != 42)
                this.Height = 42;
            reitingTrackBar.Location = new Point(42, 0);
            reitingTrackBar.Width = this.Width - 42;
        }
       
        private void trackBar1_ValueChanged(object sender, EventArgs e)
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
