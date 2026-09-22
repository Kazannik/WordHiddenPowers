using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class TextEditorDialog : Form
	{
		public string Text => textEditorBox.Text;

		public TextEditorDialog()
		{
			InitializeComponent();
			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			base.Text = "Системный промпт";
		}

		public TextEditorDialog(string text) : this()
		{
			this.textEditorBox.Text = text;
			this.textEditorBox.SelectionStart = text.Length;
			this.textEditorBox.SelectionLength = 0;
		}
	}
}
