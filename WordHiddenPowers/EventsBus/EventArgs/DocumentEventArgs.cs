using WordHiddenPowers.Documents;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class DocumentEventArgs(Document document) : System.EventArgs()
	{
		public Document Document { get; } = document;
	}
}
