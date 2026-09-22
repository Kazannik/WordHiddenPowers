using WordHiddenPowers.Documents;
using WordHiddenPowers.EventsBus.StateEnums;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class AccessToSendingUserMessageEventArgs (Document document, AccessToSendingUserMessage access) : DocumentEventArgs(document)
	{
		public AccessToSendingUserMessageEventArgs(AccessToSendingUserMessage access): this(document: null, access: access) { }

		public AccessToSendingUserMessage AccessToSendingUserMessage { get; } = access;

		public bool IsAccess => Document != null &&
			AccessToSendingUserMessage == AccessToSendingUserMessage.Full;
	}
}
