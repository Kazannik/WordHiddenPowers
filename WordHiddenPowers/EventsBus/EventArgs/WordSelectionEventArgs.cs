using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordSelectionEventArgs(Word.Selection Sel) : System.EventArgs
	{
		public Word.Selection Selection { get; } = Sel;

	}
}
