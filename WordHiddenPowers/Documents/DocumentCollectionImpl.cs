using LLMConnectorLibrary.EventArgs;
using System;
using System.Collections.Generic;
using WordHiddenPowers.Dialogs;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class DocumentCollection : IEnumerable<Document>, IDisposable
	{
		private LLMChatStatusDialog dialog;

		private Word.Range aiRange;
		private int countProgress = 0;
		public const string AI_STATUS_TEXT = "Подождите, идет подготовка информации";

		private void AddDecimalNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null &&
				ActiveDocument.VariablesExists())
				ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void AddTextNote_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null &&
				ActiveDocument.VariablesExists())
				ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
		}

		private void AiButton1_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(selection: Globals.ThisAddIn.Selection, systemMessage: Properties.Settings.Default.LLMSystemMessage1, userMessage: Properties.Settings.Default.LLMPrefixUserMessage1);
		}

		private void AiButton2_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(selection: Globals.ThisAddIn.Selection, systemMessage: Properties.Settings.Default.LLMSystemMessage2, userMessage: Properties.Settings.Default.LLMPrefixUserMessage2);
		}

		public void Ai(string systemMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(Globals.ThisAddIn.Selection, systemMessage, string.Empty);
		}

		public void Ai(string systemMessage, string userMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(Globals.ThisAddIn.Selection, systemMessage, userMessage);
		}

		public void Ai()
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(Globals.ThisAddIn.Selection);
		}

		private void AddDecimal_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Selection);
		}

		private void AddText_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.AddTextNote(Globals.ThisAddIn.Selection);
		}

		public void AiShow(string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
		}

		public void AiShow(string systemMessage, string prefixUserMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessages: new string[] { prefixUserMessage, Globals.ThisAddIn.Selection.Text }, tag: null);
		}


		internal void AiShow(string llmName, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(selection: Globals.ThisAddIn.Selection, llmName: llmName, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
		}

		internal void AiShow(Word.Selection selection, string llmName, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			dialog = new LLMChatStatusDialog
			{
				Text = "Искусственный интеллект",
				Status = AI_STATUS_TEXT,
			};

			Utils.Dialogs.Show(dialog);
			countProgress = 0;
			aiRange = selection.Range;
			llmClient.Send(model: llmName, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		}
				
		public void AiEmbed(string input)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiEmbed(llmName: Services.OpenAIService.LLMName, input: input, tag: null);
		}

		internal void AiEmbed(string llmName, string input, object tag)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiEmbed(selection: Globals.ThisAddIn.Selection, llmName: llmName, input: input, tag: tag);
		}

		internal void AiEmbed(Word.Selection selection, string llmName, string input, object tag)
		{
			dialog = new LLMChatStatusDialog
			{
				Text = "Искусственный интеллект",
				Status = AI_STATUS_TEXT,
			};

			Utils.Dialogs.Show(dialog);
			countProgress = 0;
			aiRange = selection.Range;
			llmClient.Embed(model: llmName, input: input, tag: tag);
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		}

		private void LLMClient_ChatProgress(object sender, ChatProgressEventArgs e)
		{
			dialog.Status = AI_STATUS_TEXT + new string('.', countProgress);
			countProgress++;
			if (countProgress > 5) countProgress = 0;
		}

		private void LLMClient_ChatCompleted(object sender, ChatCompletedEventArgs e)
		{
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightGreen;
			aiRange.Text = e.Message;
			dialog.Close();
			dialog.Dispose();
		}

		private void LLMClient_EmbedProgress(object sender, EmbedProgressEventArgs e)
		{
			dialog.Status = AI_STATUS_TEXT + new string('.', countProgress);
			countProgress++;
			if (countProgress > 5) countProgress = 0;
		}

		private void LLMClient_EmbedCompleted(object sender, EmbedCompletedEventArgs e)
		{		
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
			string result = string.Empty;
			foreach (ReadOnlyMemory<float> embedding in e.Embedding)
			{
				result += string.Join(", ", embedding.ToArray());
			}
			aiRange.Text = result;
			dialog.Close();
			dialog.Dispose();
		}


		private void LlmClient_EmbedCanceled(object sender, EmbedCanceledEventArgs e)
		{
			if (e.Error)
				Utils.Dialogs.ShowErrorDialog(e.Exception.InnerException.Message);
			dialog.Close();
			dialog.Dispose();
		}

		private void LlmClient_ChatCanceled(object sender, ChatCanceledEventArgs e)
		{
			if (e.Error)
				Utils.Dialogs.ShowErrorDialog(e.Exception.InnerException.Message);
			dialog.Close();
			dialog.Dispose();
		}
	}
}
