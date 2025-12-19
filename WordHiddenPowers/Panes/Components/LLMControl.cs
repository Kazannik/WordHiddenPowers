using System;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;

namespace WordHiddenPowers.Panes.Components
{
	public partial class LLMControl : UserControl
	{
		public readonly Document Document;

		public string UserMessage => userMessageTextBox.Text;

		public LLMControl()
		{
			InitializeComponent();
		}

		public LLMControl(Document document)
		{
			Document = document;

			InitializeComponent();
		}

		private void SystemMessageEditButton_Click(object sender, EventArgs e)
		{
			TextEditorDialog dialog = new TextEditorDialog(Services.OpenAIService.MainSystemMessage);
			if (dialog.ShowDialog() != null)
			{
				Services.OpenAIService.MainSystemMessage = dialog.Text;
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null && Globals.ThisAddIn.Selection.Text.Length>1) 
			{
				Globals.ThisAddIn.Documents.AiShow(systemMessage: Services.OpenAIService.MainSystemMessage, prefixUserMessage: UserMessage);
			}
			else if (Globals.ThisAddIn.Selection != null)
			{
				Globals.ThisAddIn.Selection.Text = "*";
				Globals.ThisAddIn.Documents.AiShow(systemMessage: Services.OpenAIService.MainSystemMessage, userMessage: UserMessage, tag: null);
			}
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			Services.OpenAIService.Example03_FunctionCalling();
		}		
	}
}
