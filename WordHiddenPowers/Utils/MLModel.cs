// Ignore Spelling: Util Pattersn Utils

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static WordHiddenPowers.MLService.MLModel;

namespace WordHiddenPowers.Utils
{
    static class MLModel
    {
		private static readonly string MLNetModelFilterPath = Path.Combine(FileSystem.UserDirectory.FullName, "MLModel.filters");

		private static Filters filters = null;

		public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAll(string text, string mlNetModelPath)
		{
			if (filters == null) filters = new Filters();

			text = filters.Clean(text);
			if (string.IsNullOrEmpty(text)) return null;
			ModelInput sampleData = new ModelInput()
			{
				Title = text,
			};
			return PredictAllLabels(sampleData, mlNetModelPath);
		}

		/// <summary>
		///  Конвертирование текста в упорядоченный вид.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string ConvertToCompliance(string text)
		{
			if (filters == null) filters = new Filters();

			return filters.ConvertToCompliance(text);
		}
		
		private class Filters
		{
			private static readonly Regex regexWords = new Regex("\\w+");
			private static readonly Regex regexDecimal = new Regex("(\\d\\s*)+([,](\\d\\s*)+)*");
			private static readonly Regex regexSpace = new Regex("\\s{2,}");

			/// <summary>
			/// Регулярные выражения для удаления 
			/// </summary>
			public IEnumerable<string> ExcludedPattersn { get; }
			public IEnumerable<string> ExcludedWords { get; }
			public IEnumerable<string> RecommendWords { get; }

			public bool IsInitialize { get; }

			public Filters()
			{
				List<string> excludedPatterns = new List<string>();
				List<string> excludedWords = new List<string>();
				List<string> recommendWords = new List<string>();

				string mode = string.Empty;

				if (File.Exists(MLNetModelFilterPath))
				{
					using (StreamReader reader = new StreamReader(MLNetModelFilterPath, System.Text.Encoding.Default))
					{
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine().Trim();
							if (
								line == "[excluded_patterns]"
								|| line == "[excluded_words]"
								|| line == "[recommend_word]")
							{
								mode = line;
							}
							else if (!string.IsNullOrWhiteSpace(line) 
								&& mode == "[excluded_patterns]")
							{
								excludedPatterns.Add(line.Trim());
							}
							else if (!string.IsNullOrWhiteSpace(line) 
								&& mode == "[excluded_words]")
							{							
								excludedWords.Add(line.Trim());
							}
							else if (!string.IsNullOrWhiteSpace(line)
								&& mode == "[recommend_word]")
							{
								recommendWords.Add(line.Trim());
							}
						}
					}
					ExcludedPattersn = excludedPatterns;
					ExcludedWords = excludedWords
						.OrderBy(s => s)
						.OrderByDescending(s => s.Length);
					RecommendWords = recommendWords
						.OrderBy(s => s)
						.OrderByDescending(s => s.Length);
					IsInitialize = true;
				}
				else
				{
					IsInitialize = false;
				}
			}

			public string ConvertToCompliance(string text)
			{
				text = Clean(text: text);

				if (IsInitialize && ExcludedWords.Any())
				{
					MatchCollection words = regexWords.Matches(text);
					text = string.Empty;
					foreach (Match item in words)
					{
						text += IsExcludedWords(item.Value);
					}
				}
				if (IsInitialize && RecommendWords.Any())
				{
					MatchCollection words = regexWords.Matches(text);
					text = string.Empty;
					foreach (Match item in words)
					{
						text += IsRecommendWords(item.Value);
					}					
				}
				text = regexSpace.Replace(text, " ");
				return text.Trim();
			}

			private string IsRecommendWords(string text)
			{
				foreach (string word in RecommendWords)
				{
					if (text.Length >= word.Length)
					{
						if (text.IndexOf(word) == 0)
							return word + " ";
					}
				}
				return text + " ";
			}


			private string IsExcludedWords(string text)
			{
				foreach (string word in ExcludedWords)
				{
					if (text.Length >= word.Length)
					{
						if (text.IndexOf(word) == 0)
							return string.Empty;
					}
				}
				return text + " ";
			}

			/// <summary>
			/// Очистка текста от символов
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public string Clean(string text)
			{
				text = text.ToLower();
				text = text.Replace("ё", "е");
				
				if (IsInitialize)
				{
					foreach (string pattern in ExcludedPattersn)
					{
						text = Regex.Replace(text, pattern, " ");
					}
				}
				
				text = regexDecimal.Replace(text, " [число] ");
				text = regexSpace.Replace(text, " ");
				return text.Trim();
			}
		}	
	}
}
