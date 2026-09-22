using WordHiddenPowers.Documents;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class PaneStateEventArgs(Document document, bool visible) : DocumentEventArgs(document)
	{
		public PaneStateEventArgs(bool visible) : this(document: null, visible: visible) { }

		public bool Visible { get; } = visible;

		public bool IsVisible => Document != null && Visible;
	}
}
