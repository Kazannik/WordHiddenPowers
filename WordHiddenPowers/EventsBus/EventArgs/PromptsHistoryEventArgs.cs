using WordHiddenPowers.EventsBus.StateEnums;
using WordHiddenPowers.Repository.History;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class PromptsHistoryEventArgs(PromptsHistoryState state, DtoPrompt prompt)
	{
		public PromptsHistoryState State { get; } = state;

		public DtoPrompt Prompt { get; } = prompt;		
	}
}
