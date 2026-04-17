// Ignore Spelling: Dialogs

using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class PromptEditorDialog : Form
	{
		public PromptEditorDialog(
			string caption,
			string systemMessage,
			string prefixUserMessage = "",
			string postfixUserMessage = "")
		{
			InitializeComponent();
			captionTextBox.Text = caption;
			systemMessageTextBox.Text = systemMessage;
			prefixUserMessageTextBox.Text = prefixUserMessage;
			postfixUserMessageTextBox.Text = postfixUserMessage;
		}

		public string Caption => captionTextBox.Text;

		public string SystemMessage => systemMessageTextBox.Text;

		public string PrefixUserMessage => prefixUserMessageTextBox.Text;

		public string PostfixUserMessage => postfixUserMessageTextBox.Text;
	}
}
