using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
    public partial class SelectCategoryDecimalDialog : Form
    {
        public string SelectionText { get; }

        public int SelectionStart { get; }

        public int SelectionEnd { get; }

        public int Rating
        {
            get
            {
                return raitingBox.Value;
            }
        }

        public int Value
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
        }

        public string Description
        {
            get
            {
                return descriptionTextBox.Text;
            }
        }

        public SelectCategoryDecimalDialog(Word.Selection selection)
        {
            InitializeComponent();
            SelectionText = selection.Text;
            SelectionStart = selection.Start;
            SelectionEnd = selection.End;
        }
    }
}
