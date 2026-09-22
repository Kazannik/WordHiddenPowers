using System;

namespace WordHiddenPowers.EventsBus.StateEnums
{
	[Flags]
	public enum PromptsHistoryState: byte
	{
		Unchanged = 0,
		Added = 1 << 1,
		Deleted = 1 << 2,
		Modified = 1 << 3,
		Cleared = 1 << 4,
	}
}
