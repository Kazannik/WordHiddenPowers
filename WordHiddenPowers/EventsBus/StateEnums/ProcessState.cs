namespace WordHiddenPowers.EventsBus.StateEnums
{
	public enum ProcessState : byte
	{
		Nothing = 0,
		Initialize = 1,
		Progress = 2,
		Canceled = 3,
		Completed = 4
	}
}
