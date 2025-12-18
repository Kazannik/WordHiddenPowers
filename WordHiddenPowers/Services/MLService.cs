using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Repository.Categories;
using WordHiddenPowers.Utils;
using Document = WordHiddenPowers.Documents.Document;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Services
{
	static class MLService
    {
		private const string NOTE_DESCRIPTION = "Добавлено с помощью ИИ ({0} %)";

		public static void Search(Document document, float levelPassage)
		{
			string mlNetModelName = document.MLModelName;
			string mlNetModelPath = Path.Combine(FileSystem.UserDirectory.FullName, mlNetModelName);
			if (!File.Exists(mlNetModelPath)) return;

			ProgressDialog dialog = new ProgressDialog
			{
				Text = "Разметка документа с помощью ИИ",
				Percent = 0
			};
			Utils.Dialogs.Show(dialog);

			int count = 0;

			foreach (Word.Paragraph paragraph in document.Doc.Content.Paragraphs)
			{
				count++;
				dialog.Percent = count * 100 / document.Doc.Content.Paragraphs.Count;

				string paragraphText = paragraph.Range.Text;

				if (paragraphText.Split(' ').Length < 15) continue;
								
				IOrderedEnumerable<KeyValuePair<string, float>> result = MLModel.PredictAll(paragraph.Range.Text, mlNetModelPath);
				IEnumerable<Subcategory> subcategories = GetSubcategories(document, result, levelPassage);
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
			dialog.Close();
		}
		
		internal static IEnumerable<Subcategory> GetSubcategories(Document document, IOrderedEnumerable<KeyValuePair<string, float>> result, float levelPassage)
		{
			if (result == null) return null;
			return result
				.OrderByDescending(p => p.Value)
				.Where(p => p.Value >= levelPassage)
				.Select(p => document.CurrentDataSet.GetSubcategory(guid: p.Key, tag: p.Value));
		}
	}
}


//public static DirectoryInfo UserDirectory
//{
//	get
//	{
//		string userDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Microsoft Word MLModel");
//		if (!Directory.Exists(userDirectoryPath))
//		{
//			Directory.CreateDirectory(userDirectoryPath);
//		}
//		return new DirectoryInfo(userDirectoryPath);
//	}
//}

//public static string MLNetModelPath = Path.Combine(UserDirectory.FullName, "MLModel.mlnet");
