using Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentWindowDeactivateEventArgs(Document Doc, Window Wn) : WordDocumentWindowActivateEventArgs(Doc, Wn)
	{
	}
}
