using LLMConnectorLibrary.EventArgs;
using System;
using System.Collections.Generic;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Services;
using static LLMConnectorLibrary.LLMOpenAI;
using static WordHiddenPowers.Dialogs.LLMProcessDialog;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class DocumentCollection : IEnumerable<Document>, IDisposable
	{
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
				AiShow(llmName: OpenAIService.LLMName, options: OpenAIService.Options, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
		}

		public void AiShow(string systemMessage, IEnumerable<string> userMessages, Arguments arg)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: OpenAIService.LLMName, options: OpenAIService.Options, systemMessage: systemMessage, userMessages: userMessages, tag: arg);
		}

		public void AiShow(string systemMessage, string userMessage)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiShow(llmName: OpenAIService.LLMName, options: OpenAIService.Options, systemMessage: systemMessage, userMessages: new string[] { userMessage, Globals.ThisAddIn.Selection.Text }, tag: null);
		}
		
		internal void AiShow(string llmName, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(OpenAIService.Uri, OpenAIService.Timeout, llmName, options, systemMessage, userMessages, tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiShow(string llmName, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, Arguments arg)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(OpenAIService.Uri, OpenAIService.Timeout, llmName, options, systemMessage, userMessages, arg);
			Utils.Dialogs.Show(llmDialog);
		}

		public void AiEmbedShow(string input, object tag = null)
		{
			if (Globals.ThisAddIn.Selection != null)
				AiEmbedShow(llmName: OpenAIService.LLMName, input: input, tag: tag);
		}

		internal void AiEmbedShow(string llmName, string input, object tag = null)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(OpenAIService.Uri, OpenAIService.Timeout, llmName, input, tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiEmbedShow(string llmName, IEnumerable<(int key, string description)> store, object tag = null)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(OpenAIService.Uri, OpenAIService.Timeout, llmName, store, tag);
			Utils.Dialogs.Show(llmDialog);
		}

		internal void AiEmbedShow(object tag = null)
		{
			LLMProcessDialog llmDialog = new LLMProcessDialog(OpenAIService.Uri, OpenAIService.Timeout, OpenAIService.LLMName, ActiveDocument.NowAggregatedDataSet, ActiveDocument.VectorDataSet, tag);
			Utils.Dialogs.Show(llmDialog);
		}
		
		public void EmbeddingParagraphs()
		{
			ActiveDocument.EmbeddingAllParagraphs();
		}

		public void EmbeddingDataset()
		{
			ActiveDocument.EmbeddingDataSet(ActiveDocument.NowAggregatedDataSet);
		}
	}
}
