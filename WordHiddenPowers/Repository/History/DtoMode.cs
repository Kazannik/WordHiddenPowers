using static WordHiddenPowers.Repository.PromptsHistory;

namespace WordHiddenPowers.Repository.History
{
	public class DtoMode
	{
		public static DtoMode Create(PromptModeRow row)
		{
			return new DtoMode(
				id: row.id,
				insertHereMessage: row.insert_here_message,
				replaceSelectionMessage: row.replace_selection_message,
				insertNextMessage: row.insert_next_message,
				insertPreviousMessage: row.insert_previous_message,
				insertCenterMessage: row.insert_between_message);
		}

		private DtoMode(
			int id,
			bool insertHereMessage,
			bool replaceSelectionMessage,
			bool insertNextMessage,
			bool insertPreviousMessage,
			bool insertCenterMessage
			)
		{
			Id = id;
			InsertHereMessage = insertHereMessage;
			ReplaceSelectionMessage = replaceSelectionMessage;
			InsertNextMessage = insertNextMessage;
			InsertPreviousMessage = insertPreviousMessage;
			InsertCenterMessage = insertCenterMessage;
		}

		public int Id { get; }

		public bool InsertHereMessage { get; }

		public bool ReplaceSelectionMessage { get; }

		public bool InsertNextMessage { get; }

		public bool InsertPreviousMessage { get; }

		public bool InsertCenterMessage { get; }
	}
}
