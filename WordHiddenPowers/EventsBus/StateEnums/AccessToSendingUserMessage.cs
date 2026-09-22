using System;

namespace WordHiddenPowers.EventsBus.StateEnums
{
	[Flags]
	public enum AccessToSendingUserMessage: int
	{
		None = 0,
		SelectModel = 1 << 1,
		SelectDocument = 1 << 2,
		UserMessage = 1 << 3,
		Full = SelectModel | SelectDocument | UserMessage
	}
}
