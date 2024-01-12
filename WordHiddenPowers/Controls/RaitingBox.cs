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
    public partial class RaitingBox : UserControl
    {
        public RaitingBox()
        {
            InitializeComponent();

            ratingLabel.Text = trackBar1.Value.ToString();
        }

        private void RaitingBox_Resize(object sender, EventArgs e)
        {
            if (this.Height != 42)
                this.Height = 42;
            trackBar1.Location = new Point(42, 0);
            trackBar1.Width = this.Width - 42;
        }
       
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            ratingLabel.Text = trackBar1.Value.ToString();
        }

        public int Value
        {
            get
            {
                return trackBar1.Value;
            }
            set
            {
                trackBar1.Value = value;
            }
        }
    }
}
