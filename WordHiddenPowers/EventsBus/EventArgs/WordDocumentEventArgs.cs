using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentEventArgs(Word.Document Doc) : System.EventArgs
	{
		public Word.Document Document { get; } = Doc;

	}
}
