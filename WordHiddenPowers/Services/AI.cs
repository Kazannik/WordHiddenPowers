using System.Collections.Generic;
using System.Linq;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Services
{
	static class AI
    {
		private const string NOTE_DESCRIPTION = "Добавлено с помощью ИИ ({0} %)";

		public static void Search(Document document)
		{
			foreach (Word.Paragraph paragraph in document.Doc.Content.Paragraphs)
			{
				IOrderedEnumerable<KeyValuePair<string, float>> result = MLModelUtil.PredictAll(paragraph.Range.Text);
				IEnumerable<Subcategory> subcategories = GetSubcategories(document, result);
				if (subcategories != null)
				{
					foreach (Subcategory subcategory in subcategories)
					{
						document.AddTextNote(
							categoryGuid: subcategory.Category.Guid,
							subcategoryGuid: subcategory.Guid,
							string.Format(NOTE_DESCRIPTION, (100 * (float)subcategory.Tag).ToString("0.000")),
							value: paragraph.Range.Text,
							rating: 0,
							selectionStart: paragraph.Range.Start,
							selectionEnd: paragraph.Range.End);
					}
				}
			}
		}

		internal static IEnumerable<Subcategory> GetSubcategories(Document document, IOrderedEnumerable<KeyValuePair<string, float>> result)
		{
			if (result == null) return null;
			return result
				.OrderByDescending(p => p.Value)
				.Where(p => p.Value > Const.Globals.LEVEL_PASSAGE)
				.Select(p => document.CurrentDataSet.GetSubcategory(guid: p.Key, tag: p.Value));
		}
	}
}
