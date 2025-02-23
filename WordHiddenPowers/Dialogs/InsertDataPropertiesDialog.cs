using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
    public partial class InsertDataPropertiesDialog: Form
    {
        public InsertDataPropertiesDialog()
        {
            InitializeComponent();
        }
        
        public int MinRating { get => (int)minRatingNumericUpDown.Value; }

		public int MaxRating { get => (int)maxRatingNumericUpDown.Value; }

		public int NoteCount { get => (int)noteCountNumericUpDown.Value; }

		public bool ViewHide { get => viewHideCheckBox.Checked; }
	}
}
