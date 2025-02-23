// Ignore Spelling: Utils

using System;
using System.Linq;
using System.Text.RegularExpressions;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;

namespace WordHiddenPowers.Utils
{
	static class CategoriesUtil
	{
		private const string TEXT = "/*\r\n! # Наименование категории\r\n      : Пояснение к первой категории\r\n  S ## Наименование текстовой подкатегории\r\n      : Пояснение к подкатегории\r\n  D ## Наименование числовой подкатегории\r\n      : Пояснение к подкатегории\r\n  D! ## Наименование обязательной числовой подкатегории\r\n      : Пояснение к подкатегории  \r\n*/\r\n";

		private static readonly Regex regexCategory = new Regex(@"^\s*(\x21\s*){0,1}\x23\s");

		private static readonly Regex regexSubcategory = new Regex(@"^\s*([DS!]\s*){1,3}\x23{2}\s*\S");

		private static readonly Regex regexObligatoryDecimalSubcategory = new Regex(@"^\s*\x44\s*\x21\s*\x23{2}\s{1,}\S");
		private static readonly Regex regexDecimalSubcategory = new Regex(@"^\s*\x44\s*\x23{2}\s{1,}\S");

		private static readonly Regex regexObligatoryTextSubcategory = new Regex(@"^\s*\x53\s*\x21\s*\x23{2}\s{1,}\S");
		private static readonly Regex regexTextSubcategory = new Regex(@"^\s*\x53\s*\x23{2}\s{1,}\S");

		private static readonly Regex regexDescription = new Regex(@"^\s*\x3a\s*\S");

		private static readonly Regex regexKeywordCollection = new Regex(@"Keywords[:](\s*[""][^""]{1,}[""](\s*[,]\s*)*){1,}");
		private static readonly Regex regexKeyword = new Regex(@"\x22[^\x22]{1,}\x22");

		public static void CreateFromText(RepositoryDataSet dataSet, string text)
		{
			dataSet.Subcategories.Clear();
			dataSet.Subcategories.AcceptChanges();

			dataSet.Categories.Clear();
			dataSet.Categories.AcceptChanges();

			string[] arrayLines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			ReadMode mode = ReadMode.Default;
			
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
					string attr = line.Substring(0, match.Value.IndexOf("#"));
					addingCategory = Category.Create(
											caption: line.Substring(match.Index + match.Value.IndexOf("#") + 1),
											position: dataSet.Categories.Count + 1,
											description: string.Empty,
											isObligatory: attr.Contains("!"),
											beforeText: string.Empty,
											afterText: string.Empty); 
					addingCategory = dataSet.Add(addingCategory);
					addingSubcategory = null;
				}				
				else if (mode == ReadMode.Default &&
					regexSubcategory.IsMatch(line))
				{
					Match match = regexSubcategory.Match(line);
					string caption = line.Substring(match.Index + match.Value.IndexOf("##") + 2);
					string attr = line.Substring(0, match.Value.IndexOf("##"));

					addingSubcategory = Subcategory.Create(
						category: addingCategory,
						position: dataSet.Subcategories.Count + 1,
						caption: caption,
						description: string.Empty,
						isDecimal: attr.Contains("D"),
						isText: attr.Contains("S"),
						isObligatory: attr.Contains("!"),
						beforeText: string.Empty,
						afterText: string.Empty,
						keywords: keywords);
					addingSubcategory = dataSet.Add(addingCategory, addingSubcategory);
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
						dataSet.Write(addingSubcategory);
						addingSubcategory = null;
					}
					else if (addingCategory != null)
					{
						addingCategory.Description = description;
						dataSet.Write(addingCategory);
					}
				}
			}
		}

		public static void Save()
		{

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
