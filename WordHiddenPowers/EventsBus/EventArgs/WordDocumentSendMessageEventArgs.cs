using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentSendMessageEventArgs(Word.Range range, string systemMessage, IEnumerable<string> userMessages) : System.EventArgs
	{
		public Word.Range Range { get; } = range;
		public string SystemMessage { get; } = systemMessage;
		public IEnumerable<string> UserMessages { get; } = userMessages;

	}
}
