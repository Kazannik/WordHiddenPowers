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
			if (document != null) Document = document;

			InitializeComponent();
		}

		public Documents.DocumentCollection.ChartMessageMode MessageMode
		{
			get => sendMessageButtonsBar.MessageMode;
			set => sendMessageButtonsBar.MessageMode = value;
		}

		private void SendMessageButtonsBar_ClickInsertMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Insert, systemMessage: Services.OpenAIService.SystemMessage, userMessage: UserMessage);
		
		private void SendMessageButtonsBar_ClickReplaceMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Replace, systemMessage: Services.OpenAIService.SystemMessage, userMessage: UserMessage);
		
		private void SendMessageButtonsBar_ClickInsertNextMessage(object sender, EventArgs e) =>
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Next, systemMessage: Services.OpenAIService.SystemMessage, userMessage: UserMessage);
		
		private void SendMessageButtonsBar_ClickInsertPreviousMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Previous, systemMessage: Services.OpenAIService.SystemMessage, userMessage: UserMessage);
		
		private void SendMessageButtonsBar_ClickInsertCenterMessage(object sender, EventArgs e) => 
			Globals.ThisAddIn.Documents.InsertMessage(mode: DocumentCollection.ChartMessageMode.Center, systemMessage: Services.OpenAIService.SystemMessage, userMessage: UserMessage);
		



		private void SystemMessageEditButton_Click(object sender, EventArgs e)
		{
			TextEditorDialog dialog = new TextEditorDialog(Services.OpenAIService.SystemMessage);
			if (dialog.ShowDialog() != null)
			{
				Services.OpenAIService.SystemMessage = dialog.Text;
			}
		}





		

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			//Services.OpenAIService.Example03_FunctionCalling();


		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			if (Globals.ThisAddIn.Selection != null && Globals.ThisAddIn.Selection.Text.Length > 1)
			{
				Globals.ThisAddIn.Documents.AiEmbedShow(input: UserMessage);
			}
			else if (Globals.ThisAddIn.Selection != null)
			{
				Globals.ThisAddIn.Selection.Text = "*";
				Globals.ThisAddIn.Documents.AiEmbedShow(input: UserMessage);
			}
		}

		

		

				
	}
}
