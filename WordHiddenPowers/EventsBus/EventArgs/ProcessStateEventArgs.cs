using WordHiddenPowers.EventsBus.StateEnums;

namespace WordHiddenPowers.EventsBus.EventArgs
{
	public class ProcessStateEventArgs(ProcessState state) : System.EventArgs
	{
		public ProcessState ProcessState { get; } = state;
	}
}
