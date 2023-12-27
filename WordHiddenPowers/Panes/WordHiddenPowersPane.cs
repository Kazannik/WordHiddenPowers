using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
    public partial class WordHiddenPowersPane : UserControl
    {
        public Word.Document Document { get; }
       
        public WordHiddenPowersPane(Word.Document Doc)
        {
            this.Document = Doc;

            InitializeComponent();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            SplitterPanel panel = (SplitterPanel)sender;

            titleLabel.Location = new Point(0, 0);
            titleTextBox.Location = new Point(0, titleLabel.Height);
            titleTextBox.Width = panel.Width;
            dateLabel.Location = new Point(0, titleTextBox.Top + titleTextBox.Height + 4);
            dateTimePicker.Location = new Point(dateLabel.Width + 8, titleTextBox.Top + titleTextBox.Height + 4);
            descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);
            textBox2.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
            textBox2.Width = panel.Width;
        }
    }
}
