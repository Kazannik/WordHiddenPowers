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
