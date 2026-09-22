using WordHiddenPowers.Documents;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class StatusEventArgs(Document document, bool status) :  DocumentEventArgs(document: document)
	{
		public bool Status { get; } = status;

	}
}

