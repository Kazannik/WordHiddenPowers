using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Repositories.Categories;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Services
{
	static class Searcher
    {
		private const string NOTE_DESCRIPTION = "Добавлено с помощью поискового алгоритма";

		public static void Search(Document document, IEnumerable<Subcategory> subcategories)
		{
			foreach (Subcategory subcategory in subcategories)
			{
				if (!string.IsNullOrEmpty(subcategory.Keywords))
				{
					string[] keywords = subcategory.Keywords.Split(separator: new string[] { Environment.NewLine }, options: StringSplitOptions.RemoveEmptyEntries);
					foreach (string keyword in keywords)
					{
						SearchParagraphs(
							document: document,
							categoryGuid: subcategory.Category.Guid,
							subcategoryGuid: subcategory.Guid,
							keyword: keyword,
							isText: subcategory.IsText);
					}
				}
			}
		}

		private static void SearchParagraphs(Document document, string categoryGuid, string subcategoryGuid, string keyword, bool isText)
		{
			string[] patterns = GetPatterns(keyword);

			if (patterns.Length > 0)
			{
				foreach (Word.Paragraph paragraph in document.Doc.Content.Paragraphs)
				{
					if (IsCompliance(paragraph: paragraph, pattern: patterns[0]))
					{
						if (isText)
						{
							if (patterns.Length == 1)
							{
								document.AddTextNote(
									categoryGuid: categoryGuid,
									subcategoryGuid: subcategoryGuid,
									description: NOTE_DESCRIPTION,
									value: paragraph.Range.Text,
									rating: 0,
									selectionStart: paragraph.Range.Start,
									selectionEnd: paragraph.Range.End
									);
							}
							else
							{
								AddTextNote(
									document: document,
									categoryGuid: categoryGuid,
									subcategoryGuid: subcategoryGuid,
									paragraph: paragraph,
									patterns: patterns
									);
							}
						}
						else
						{
							AddDecimalNote(
								document: document,
								categoryGuid: categoryGuid,
								subcategoryGuid: subcategoryGuid,
								paragraph: paragraph,
								patterns: patterns
								);
						}
					}
				}
			}
		}

		internal static string[] GetPatterns(string keyword)
		{
			string[] patterns = keyword.Split(new string[] { "';'" }, StringSplitOptions.RemoveEmptyEntries);
			if (patterns.Length > 0)
			{
				if (patterns[0].Length > 0
					&& patterns[0][0] == 39
					&& patterns[patterns.Length - 1][patterns[patterns.Length - 1].Length - 1] == 39)
				{
					patterns[0] = patterns[0].Substring(1);
					patterns[patterns.Length - 1] = patterns[patterns.Length - 1].Substring(0, patterns[patterns.Length - 1].Length - 1);
				}
			}
			return patterns;
		}

		private static bool IsCompliance(Word.Paragraph paragraph, string pattern)
		{
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(paragraph.Range.Text);
		}

		private static void AddTextNote(Document document, string categoryGuid, string subcategoryGuid, Word.Paragraph paragraph, string[] patterns)
		{
			string content = paragraph.Range.Text;
			int selectionStart = paragraph.Range.Start;
			for (int i = 1; i < patterns.Length; i++)
			{
				Regex regex = new Regex(patterns[i], RegexOptions.IgnoreCase);
				if (regex.IsMatch(content))
				{
					Match match = regex.Match(content);
					content = match.Value;
					selectionStart += match.Index;
				}
				else
				{
					return;
				}
			}
			document.AddTextNote(
				categoryGuid: categoryGuid,
				subcategoryGuid: subcategoryGuid,
				description: NOTE_DESCRIPTION,
				value: content,
				rating: 0,
				selectionStart: selectionStart,
				selectionEnd: selectionStart + content.Length
				);
		}

		private static void AddDecimalNote(Document document, string categoryGuid, string subcategoryGuid, Word.Paragraph paragraph, string[] patterns)
		{
			string content = paragraph.Range.Text;
			for (int i = 1; i < patterns.Length - 1; i++)
			{
				Regex regex = new Regex(patterns[i], RegexOptions.IgnoreCase);
				if (regex.IsMatch(content))
				{
					Match match = regex.Match(content);
					content = match.Value;
				}
				else
				{
					return;
				}
			}
			if (double.TryParse(content, out double result))
			{
				document.AddDecimalNote(
				categoryGuid: categoryGuid,
				subcategoryGuid: subcategoryGuid,
				description: NOTE_DESCRIPTION,
				value: result,
				rating: 0,
				selectionStart: paragraph.Range.Start,
				selectionEnd: paragraph.Range.End
				);
			}
		}
	}
}
