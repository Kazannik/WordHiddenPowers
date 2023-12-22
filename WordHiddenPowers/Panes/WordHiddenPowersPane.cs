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
    }
}
