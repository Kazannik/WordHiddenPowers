using WordHiddenPowers.Documents;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class DocumentChatMessageModeEventArgs(Document document, Document.ChatMessageModeEnum messageMode) : DocumentEventArgs(document)
	{
		public Document.ChatMessageModeEnum MessageMode { get; } = messageMode;
	}
}
