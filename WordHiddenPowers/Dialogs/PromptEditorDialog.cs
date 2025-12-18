// Ignore Spelling: Dialogs

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
