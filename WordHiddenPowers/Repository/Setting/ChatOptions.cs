using LLMConnectorLibrary;

namespace WordHiddenPowers.Repository.Setting
{
	public class ChatOptions(
		string key,
		int maxOutputTokenCount,
		float frequencyPenalty,
		float presencePenalty,
		float temperature,
		float topP) : IChatOptions
	{
		public static IChatOptions Create(string key) => new ChatOptions(key: key, maxOutputTokenCount: 0, frequencyPenalty: 0, presencePenalty: 0, temperature: 0, topP: 0);

		public string Key { get; } = key;

		public int MaxOutputTokenCount { get; } = maxOutputTokenCount;

		public float FrequencyPenalty { get; } = frequencyPenalty;

		public float PresencePenalty { get; } = presencePenalty;

		public float Temperature { get; } = temperature;

		public float TopP { get; } = topP;

		int? IChatOptions.MaxOutputTokenCount => MaxOutputTokenCount;

		float? IChatOptions.FrequencyPenalty => FrequencyPenalty;

		float? IChatOptions.PresencePenalty => PresencePenalty;

		float? IChatOptions.Temperature => Temperature;

		float? IChatOptions.TopP => TopP;
	}
}
