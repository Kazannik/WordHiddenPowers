using static WordHiddenPowers.Repository.PromptsHistory;

namespace WordHiddenPowers.Repository.History
{
	public class DtoModel
	{
		public static DtoModel Create(ModelsRow row)
		{
			return new DtoModel(
				id: row.id,
				name: row.name,
				description: row.IsdescriptionNull() ? string.Empty : row.description,
				profileEndpoint: row.ProfilesRow.endpoint);
		}

		private DtoModel(int id, string name, string description, string profileEndpoint)
		{
			Id = id;
			Name = name;
			Description = description;
			ProfileEndpoint = profileEndpoint;
		}

		public int Id { get; }
		public string Name { get; }
		public string Description { get; }
		public string ProfileEndpoint { get; }
	}
}
