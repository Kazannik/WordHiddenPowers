namespace WordHiddenPowers.Controls.ToolBar
{
	public readonly struct ChatButtonProperties(
		string caption,
		string systemMessage,
		string prefixUserMessage,
		string postfixUserMessage)
	{
		public readonly string Caption = caption;
		public readonly string SystemMessage = systemMessage;
		public readonly string PrefixUserMessage = prefixUserMessage;
		public readonly string PostfixUserMessage = postfixUserMessage;
	}
}
