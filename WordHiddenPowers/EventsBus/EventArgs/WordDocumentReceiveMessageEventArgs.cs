using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentReceiveMessageEventArgs(Word.Range range, string systemMessage, IEnumerable<string> userMessages, string message, IModel model, IChatOptions options) 
		: WordDocumentSendMessageEventArgs(range: range, systemMessage: systemMessage, userMessages: userMessages)
	{
		public string Message { get; } = message;

		public IModel Model { get; } = model;

		public IChatOptions Options { get; } = options;

	}
}
