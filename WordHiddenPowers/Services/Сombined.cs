using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Services
{
	// Соответствует ли поиску и АИ
	// Если соответствует и Поиску и АИ -> Отражаем как АИ
	// Если соответствует только Поиску -> отражаем как Поиск
	// Если соответствует только АИ -> Пока пропускаем

    static class Сombined
    {
		public static void Search(Document document, float levelPassage)
		{
			string mlNetModelName = document.MLModelName;
			string mlNetModelPath = Path.Combine(FileSystem.UserDirectory.FullName, mlNetModelName);
			if (!Directory.Exists(mlNetModelPath)) return;

			IEnumerable<Subcategory> subcategories = document.CurrentDataSet.GetSubcategories();

			foreach (Word.Paragraph paragraph in document.Doc.Content.Paragraphs)
			{
				//MLModelService.MLModel.ModelInput sampleData = new MLModelService.MLModel.ModelInput()
				//{
				//	Title = paragraph.Range.Text,
				//};

				IOrderedEnumerable<KeyValuePair<string, float>> result = MLModel.PredictAll(paragraph.Range.Text, mlNetModelPath);
				IEnumerable<Subcategory> predictSubcategories = AI.GetSubcategories(document, result, levelPassage);
				
				if (predictSubcategories != null)
				{
					foreach (Subcategory subcategory in predictSubcategories)
					{
						document.AddTextNote(
							categoryGuid: subcategory.Category.Guid,
							subcategoryGuid: subcategory.Guid,
							string.Format("Добавлено с помощью ИИ ({0} %)", (100 * (float)subcategory.Tag).ToString("0.000")),
							value: paragraph.Range.Text,
							rating: 0,
							selectionStart: paragraph.Range.Start,
							selectionEnd: paragraph.Range.End);
					}
				}
			}
		}
	
		private static bool IsCompliance(Word.Paragraph paragraph, IEnumerable<Subcategory> subcategories, IEnumerable<Subcategory> predictSubcategories)
		{
			return true;
			//foreach (Subcategory subcategory in subcategories)
			//{
			//	if (!string.IsNullOrEmpty(subcategory.Keywords))
			//	{
			//		string[] keywords = subcategory.Keywords.Split(separator: new string[] { Environment.NewLine }, options: StringSplitOptions.RemoveEmptyEntries);
			//		foreach (string keyword in keywords)
			//		{
			//			SearchParagraphs(
			//				document: document,
			//				categoryGuid: subcategory.Category.Guid,
			//				subcategoryGuid: subcategory.Guid,
			//				keyword: keyword,
			//				isText: subcategory.IsText);
			//		}
			//	}
			//}
		}
	}
}
