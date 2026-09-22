using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentWindowActivateEventArgs(Word.Document Doc, Word.Window Wn) : WordDocumentEventArgs(Doc)
	{
		public Word.Window Window { get; } = Wn;

	}
}
