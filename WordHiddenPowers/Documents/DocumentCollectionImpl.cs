using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using WordHiddenPowers.Dialogs;
using Office = Microsoft.Office.Core;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using DataTable = System.Data.DataTable;
using Table = WordHiddenPowers.Repository.Data.Table;
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
				ActiveDocument.Ai(selection: Globals.ThisAddIn.Selection, systemMessage: Properties.Settings.Default.LLMSystemMessage1, prefixUserMessage: Properties.Settings.Default.LLMPrefixUserMessage1);
		}

		private void AiButton2_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
		{
			if (Globals.ThisAddIn.Selection != null)
				ActiveDocument.Ai(selection: Globals.ThisAddIn.Selection, systemMessage: Properties.Settings.Default.LLMSystemMessage2, prefixUserMessage: Properties.Settings.Default.LLMPrefixUserMessage2);
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

		public void AiShow(string systemMessage, string userMessage, object tag)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessage: userMessage, tag: tag);
		}

		public void AiShow(string systemMessage, string prefixUserMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: Services.OpenAIService.LLMName, systemMessage: systemMessage, userMessage: prefixUserMessage + Globals.ThisAddIn.Selection.Text, tag: null);
		}


		internal void AiShow(string llmName, string systemMessage, string userMessage, object tag)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(selection: Globals.ThisAddIn.Selection, llmName: llmName, systemMessage: systemMessage, userMessage: userMessage, tag: tag);
		}

		internal void AiShow(Word.Selection selection, string llmName,  string systemMessage, string userMessage, object tag)
		{
			dialog = new LLMChatStatusDialog
			{
				Text = "Искусственный интеллект",
				Status = AI_STATUS_TEXT,
			};

			Utils.Dialogs.Show(dialog);
			countProgress = 0;
			aiRange = selection.Range;
			llmClient.Send(model: llmName, systemMessage: systemMessage, userMessage: userMessage, tag: tag);
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
		}



		private void LLMClient_ChatProgress(object sender, LLMService.LLMClient.ChatProgressEventArgs e)
		{
			dialog.Status = AI_STATUS_TEXT + new string('.', countProgress);
			countProgress++;
			if (countProgress > 5) countProgress = 0;
		}

		private void LLMClient_ChatCompleted(object sender, LLMService.LLMClient.ChatCompletedEventArgs e)
		{
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightGreen;
			aiRange.Text = e.Message;
			dialog.Close();
			dialog.Dispose();
		}

		private void LLMClient_EmbedProgress(object sender, LLMService.LLMClient.EmbedProgressEventArgs e)
		{
			dialog.Status = AI_STATUS_TEXT + new string('.', countProgress);
			countProgress++;
			if (countProgress > 5) countProgress = 0;
		}

		private void LLMClient_EmbedCompleted(object sender, LLMService.LLMClient.EmbedCompletedEventArgs e)
		{
			aiRange.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
			string result = string.Empty;
			foreach (Embedding<float> embedding in e.Embedding)
			{
				result += string.Join(", ", embedding.Vector.ToArray());
			}
			aiRange.Text = result;
			dialog.Close();
			dialog.Dispose();
		}
	}
}
