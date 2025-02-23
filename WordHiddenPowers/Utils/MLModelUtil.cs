using NeuronetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NeuronetLibrary.MLModel;

namespace WordHiddenPowers.Utils
{
    public static class MLModelUtil
    {
		private static readonly string MLNetModelFilterPath = Path.Combine(Environment.CurrentDirectory, "MLModel.filters");

		private static readonly Filters filters = new Filters();

		public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAll(string text)
		{
			text = filters.Clean(text);
			if (string.IsNullOrEmpty(text)) return null;
			ModelInput sampleData = new MLModel.ModelInput()
			{
				Title = text,
			};
			return MLModel.PredictAllLabels(sampleData);			
		}


		public static string ConvertToCompliance(string text)
		{
			return filters.ConvertToCompliance(text);
		}
		
		private class Filters
		{
			private static readonly string[] oldVal = new string[] { "\r\n", "\r", "\n", "\t", ".", ",", "-", "/", "\\", "(", ")", "\u0002", "\u00a0", "«", "»", "№" };
			private static readonly string[] patterns = new string[] { "(\\d\\s*){1,}([,](\\d\\s*){1,})*", "^\\S{1,2}\\s", "\\s\\S{1,2}\\s", "\\s{2,}" };
			private static readonly Regex regexWords = new Regex("\\w{1,}");


			public IEnumerable<string> ExcludedStrings { get; }
			public IEnumerable<string> ExcludedPattersn { get; }
			public IEnumerable<string> ExcludedWords { get; }
			public IEnumerable<string> RecommendWords { get; }

			public bool IsInitialize { get; }

			public Filters()
			{
				List<string> excludedStrings = new List<string>();
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
								line == "[excluded_string]"
								|| line == "[excluded_patterns]"
								|| line == "[excluded_words]"
								|| line == "[recommend_word]")
							{
								mode = line;
							}
							else if (!string.IsNullOrWhiteSpace(line) 
								&& mode == "[excluded_string]")
							{
								excludedStrings.Add(line.Trim());
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
					ExcludedStrings = excludedStrings;
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
				Clean(text: text);

				if (ExcludedWords.Any())
				{
					MatchCollection words = regexWords.Matches(text);
					text = string.Empty;
					foreach (Match item in words)
					{
						text += IsExcludedWords(item.Value);
					}
				}
				if (RecommendWords.Any())
				{
					MatchCollection words = regexWords.Matches(text);
					text = string.Empty;
					foreach (Match item in words)
					{
						text += IsRecommendWords(item.Value);
					}					
				}
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

			public string Clean(string text)
			{
				text = text.ToLower();

				foreach (string item in ExcludedStrings)
				{
					text = text.Replace(item, " ");
				}

				foreach (string pattern in ExcludedPattersn)
				{
					text = Regex.Replace(text, pattern, " ");
				}
				return text.Trim();
			}
		}	
	}
}
