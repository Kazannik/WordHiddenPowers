using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class WordDocumentBeforeCloseEventArgs : WordDocumentEventArgs
	{
		public WordDocumentBeforeCloseEventArgs(Word.Document Doc, ref bool Cancel) : base(Doc)
		{
			this.Cancel = Cancel;
		}

		public bool Cancel { get; }

	}
}
