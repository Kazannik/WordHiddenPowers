using System;
using System.Linq;
using System.Text.RegularExpressions;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;

namespace WordHiddenPowers.Utils
{
	static class Categories
	{
		public static void CreateFromText(RepositoryDataSet dataSet, string text)
		{
			string[] arrayLines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			ReadMode mode = ReadMode.Default;

			Regex regexObligatoryCategory = new Regex(@"^\s*\x21\s*\x23\s{1,}\S");
			Regex regexCategory = new Regex(@"^\s*\x23\s{1,}\S");

			Regex regexSubcategory = new Regex(@"^\s*([DS!]\s*){1,3}\x23{2}\s*\S");

			Regex regexObligatoryDecimalSubcategory = new Regex(@"^\s*\x44\s*\x21\s*\x23{2}\s{1,}\S");
			Regex regexDecimalSubcategory = new Regex(@"^\s*\x44\s*\x23{2}\s{1,}\S");

			Regex regexObligatoryTextSubcategory = new Regex(@"^\s*\x53\s*\x21\s*\x23{2}\s{1,}\S");
			Regex regexTextSubcategory = new Regex(@"^\s*\x53\s*\x23{2}\s{1,}\S");

			Regex regexDescription = new Regex(@"^\s*\x3a\s*\S");

			Regex regexKeywordCollection = new Regex(@"Keywords[:](\s*[""][^""]{1,}[""](\s*[,]\s*)*){1,}");
			Regex regexKeyword = new Regex(@"\x22[^\x22]{1,}\x22");

			Category addingCategory = null;
			Subcategory addingSubcategory = null;
			string keywords = string.Empty;

			foreach (string line in arrayLines)
			{
				if (mode == ReadMode.Default &&
					line.TrimStart().IndexOf(@"//") == 0)
				{
					mode = ReadMode.Default;
				}
				else if (mode == ReadMode.Default &&
					line.TrimStart().IndexOf(@"/*") == 0)
				{
					mode = ReadMode.Comment;
				}
				else if (mode == ReadMode.Comment &&
					line.TrimEnd().LastIndexOf(@"*/") == line.TrimEnd().Length - 2)
				{
					mode = ReadMode.Default;
				}
				else if (mode == ReadMode.Default &&
					regexCategory.IsMatch(line))
				{
					Match match = regexCategory.Match(line);
					addingCategory = Category.Create(line.Substring(match.Index + match.Value.IndexOf("#") + 1), string.Empty, false);
					addingCategory = dataSet.Categories.Add(addingCategory);
					addingSubcategory = null;
				}
				else if (mode == ReadMode.Default &&
					regexObligatoryCategory.IsMatch(line))
				{
					Match match = regexObligatoryCategory.Match(line);
					addingCategory = Category.Create(line.Substring(match.Index + match.Value.IndexOf("#") + 1), string.Empty, true);
					addingCategory = dataSet.Categories.Add(addingCategory);
					addingSubcategory = null;
				}
				else if (mode == ReadMode.Default &&
					regexSubcategory.IsMatch(line))
				{
					Match match = regexSubcategory.Match(line);
					string caption = line.Substring(match.Index + match.Value.IndexOf("##") + 2);
					string attr = line.Substring(0, match.Value.IndexOf("##"));

					addingSubcategory = Subcategory.Create(addingCategory, caption, string.Empty, attr.Contains("D"), attr.Contains("S"), attr.Contains("!"), keywords);
					addingSubcategory = dataSet.Subcategories.Add(addingCategory, addingSubcategory);
					keywords = string.Empty;
				}
				else if (mode == ReadMode.Default &&
					regexKeywordCollection.IsMatch(line))
				{
					Match match = regexKeywordCollection.Match(line);
					MatchCollection collection = regexKeyword.Matches(line);
					string[] stringArray = (from Match item in collection select item.Value).ToArray();
					keywords = string.Join(", ", stringArray);
				}

				else if (mode == ReadMode.Default &&
				   regexDescription.IsMatch(line))
				{
					Match match = regexDescription.Match(line);
					string description = line.Substring(match.Length - 1);

					if (addingSubcategory != null)
					{
						addingSubcategory.Description = description;
						dataSet.Subcategories.Write(addingSubcategory);
						addingSubcategory = null;
					}
					else if (addingCategory != null)
					{
						addingCategory.Description = description;
						dataSet.Categories.Write(addingCategory);
					}
				}
			}
		}

		private enum ReadMode : int
		{
			Default = 0,
			Comment = 1,
			Category = 2,
			ObligatoryCategory = 3,
			DecimalSubcategory = 4,
			DecimalObligatoryCategory = 5,
			TextSubcategory = 6,
			TextObligatoryCategory = 7,
			Description = 8
		}
	}
}
