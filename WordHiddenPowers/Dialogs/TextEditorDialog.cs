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
	public partial class TextEditorDialog : Form
	{
		public string Text => textBox.Text;

		public TextEditorDialog()
		{
			InitializeComponent();
		}

		public TextEditorDialog(string text)
		{
			InitializeComponent();

			this.textBox.Text = text;
		}
	}
}
