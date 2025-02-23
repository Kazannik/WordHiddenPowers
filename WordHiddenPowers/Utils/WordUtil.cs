// Ignore Spelling: Mso Utils Util

using System;
using System.Drawing;
using System.Reflection;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using WordHiddenPowers.Repositories;
using static WordHiddenPowers.Utils.Gdi32;
using Word = Microsoft.Office.Interop.Word;
using static WordHiddenPowers.Repositories.RepositoryDataSet;
using Microsoft.Office.Interop.Word;
using Category = WordHiddenPowers.Repositories.Categories.Category;
using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using static WordHiddenPowers.Repositories.Notes.Note;

namespace WordHiddenPowers.Utils
{
	public static class WordUtil
	{
		public static void InsertToWordDocument(Documents.Document sourceDocument,
			int minRating = 0,
			int maxRating = 0,
			int maxCount = 3,
			bool viewHide = true)
		{
			object oMissing = Missing.Value;
			Word._Application application = Globals.ThisAddIn.Application;
			Word._Document destDocument = application.ActiveDocument;
			application.Visible = true;
			
			IEnumerable<string> allCaptions = sourceDocument.ImportDataSet.GetFilesCaption();
		    
			string categoryGuid = string.Empty;
			foreach (SubcategoriesRow row in sourceDocument.ImportDataSet.Subcategories.GetSubcategoriesRows())
			{
				if (categoryGuid != row.category_guid)
				{
					if (!string.IsNullOrEmpty(categoryGuid))
					{
						InsertCategoryLastParagraph(sourceDocument, destDocument, categoryGuid);
					}
					categoryGuid = row.category_guid;
					InsertCategoryFirstParagraph(sourceDocument, destDocument, categoryGuid);
				}
				
				if (row.IsText)
				{
					InsertTextSubcategoryContent(
						sourceDocument: sourceDocument,
						destDocument: destDocument,
						subcategoryGuid: row.key_guid,
						allCaptions: allCaptions,
						minRating: minRating,
						maxRating: maxRating,
						maxCount: maxCount,
						viewHide: viewHide);
				}
				else if (row.IsDecimal)
				{
					InsertDecimalSubcategoryContent(sourceDocument, destDocument, row.key_guid, allCaptions);
				}
			}
		}

		private static void InsertCategoryFirstParagraph(
			Documents.Document sourceDocument,
			Word._Document destDocument,
			string categoryGuid)
		{
			Category category = sourceDocument.ImportDataSet.GetCategory(guid: categoryGuid);
			InsertParagraph(destDocument, string.Format("{0}) {1}", category.Code, category.Caption), true);
			if (!string.IsNullOrEmpty(category.BeforeText))
			{
				InsertParagraph(destDocument, category.BeforeText);
			}
		}

		private static void InsertCategoryLastParagraph(
			Documents.Document sourceDocument,
			Word._Document destDocument,
			string categoryGuid)
		{
			Category category = sourceDocument.ImportDataSet.GetCategory(guid: categoryGuid);
			if (!string.IsNullOrEmpty(category.AfterText))
			{
				InsertParagraph(destDocument, category.AfterText);
			}
		}

		private static void InsertSubcategoryFirstParagraph(
			Documents.Document sourceDocument,
			Word._Document destDocument,
			Subcategory subcategory)
		{
			InsertParagraph(destDocument, string.Format("{0}.{1}) {2}", subcategory.Category.Code, subcategory.Code, subcategory.Caption), true);
			if (!string.IsNullOrEmpty(subcategory.BeforeText))
			{
				InsertParagraph(destDocument, subcategory.BeforeText);
			}
		}

		private static void InsertSubcategoryLastParagraph(
			Documents.Document sourceDocument,
			Word._Document destDocument,
			Subcategory subcategory)
		{
			if (!string.IsNullOrEmpty(subcategory.AfterText))
			{
				InsertParagraph(destDocument, subcategory.AfterText);
			}
		}

		private static void InsertTextSubcategoryContent(Documents.Document sourceDocument,
			Word._Document destDocument,
			string subcategoryGuid,
			IEnumerable<string> allCaptions,
			int minRating = 0,
			int maxRating = 0,
			int maxCount = 3,
			bool viewHide = true)
		{
			Subcategory subcategory = sourceDocument.ImportDataSet.GetSubcategory(guid: subcategoryGuid);
			
			InsertSubcategoryFirstParagraph(sourceDocument, destDocument, subcategory);

			IEnumerable<string> captions = sourceDocument.ImportDataSet.GetFilesCaption(subcategoryGuid : subcategoryGuid);

			if (captions.Any()) 
			{
				InsertParagraph(
					document: destDocument, 
					text: string.Format("Информация представлена ({0}): {1}", captions.Count(), string.Join(", ", captions)), 
					bold: false, italic: true);
			}
			if (subcategory.IsObligatory)
			{
				IEnumerable<string> exceptCaptions = allCaptions.Except(captions);
				if (exceptCaptions.Any())
				{
					InsertParagraph(
					document: destDocument,
					text: string.Format("Внимание! Информация не представлена ({0}): {1}", exceptCaptions.Count(), string.Join(", ", exceptCaptions)),
					bold: false, italic: true);
				}
			}
			
			IEnumerable<Note> allNotes = sourceDocument.ImportDataSet.GetTextNotes(subcategoryGuid: subcategoryGuid, viewHide: viewHide);
			if (minRating != 0 || maxRating != 0)
			{
				if (maxRating != 0)
				{
					IEnumerable<Note> maxNotes = allNotes.Where(note => note.Rating >= maxRating).OrderBy(note => note.FileCaption);

					IEnumerable<Note> notes = maxNotes.Select(note => note);
					if (notes.Count() < maxCount)
					{
						notes = notes
							.Union(allNotes.OrderByDescending(note => note.Rating), new NoteComparer())
							.Take(maxCount);
					}

					foreach (Note note in notes)
					{
						InsertParagraph(
							document: destDocument,
							text: string.Format("{0}: {1}", note.FileCaption, note.Value),
							bold: false, italic: true);
					}
				}
				if (minRating != 0)
				{
					IEnumerable<Note> minNotes = allNotes.Where(note => note.Rating <= minRating).OrderBy(note => note.FileCaption);
					if (minNotes.Any())
					{
						InsertParagraph(
						document: destDocument,
						text: string.Format("Внимание! С отрицательны рейтингом имеется {0} заметок:", minNotes.Count()),
						bold: true, italic: true);

						foreach (Note note in minNotes)
						{
							InsertParagraph(
								document: destDocument,
								text: string.Format("{0}: {1}", note.FileCaption, note.Value),
								bold: false, italic: true);
						}
					}
				}
			}
			else
			{
				foreach (Note note in (
					maxCount == 0 
					? allNotes 
					: allNotes.OrderByDescending(note => note.Rating).Take(maxCount)).OrderBy(note=> note.FileCaption))
				{
					InsertParagraph(
						document: destDocument,
						text: string.Format("{0}: {1}", note.FileCaption, note.Value),
						bold: false, italic: true);
				}
			}
			InsertSubcategoryLastParagraph(sourceDocument, destDocument, subcategory);
		}

		private static void InsertDecimalSubcategoryContent(Documents.Document sourceDocument,
		Word._Document destDocument,
		string subcategoryGuid,
		IEnumerable<string> allCaptions)
		{
			Subcategory subcategory = sourceDocument.ImportDataSet.GetSubcategory(guid: subcategoryGuid);

			InsertSubcategoryFirstParagraph(sourceDocument, destDocument, subcategory);

			IEnumerable<string> captions = sourceDocument.ImportDataSet.GetFilesCaption(subcategoryGuid: subcategoryGuid);
			if (captions.Any())
			{
				double sum = sourceDocument.ImportDataSet.SumDecimalNote(subcategoryGuid: subcategoryGuid);
				
				InsertParagraph(
					document: destDocument,
					text: string.Format("Общее количество: {0}", sum),
					bold: false, italic: true);

				IEnumerable<Note> notes = sourceDocument.ImportDataSet.GetDecimalNotes(subcategoryGuid: subcategoryGuid);
				IEnumerable<string> values = notes.Where(note => (double)note.Value != 0)
					.Select(note => string.Format("{0} ({1} - {2})", note.FileCaption, note.Value, string.Format("{0:0.0} %", ((double)note.Value)*100/sum)));

				InsertParagraph(
					document: destDocument,
					text: string.Format("В том числе ({0}): {1}", values.Count(), string.Join(", ", values)),
					bold: false, italic: true);
			}
			if (subcategory.IsObligatory)
			{
				IEnumerable<string> exceptCaptions = allCaptions.Except(captions);
				if (exceptCaptions.Any())
				{
					InsertParagraph(
					document: destDocument,
					text: string.Format("Внимание! Информация не представлена: {0}", string.Join(", ", exceptCaptions)),
					bold: false, italic: true);
				}
			}
			InsertSubcategoryLastParagraph(sourceDocument, destDocument, subcategory);
		}

		private static void InsertParagraph(Word._Document document)
		{
			object oMissing = Missing.Value;
			Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
		}

		private static void InsertParagraph(Word._Document document, string text, bool bold = false, bool italic = false)
		{
			object oMissing = Missing.Value;
			Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
			paragraph.Range.Text = text.Trim().Replace("  "," ").Replace("  ", " ").Replace("  ", " ");
			paragraph.Range.Font.Bold = bold ? 1 : 0;
			paragraph.Range.Font.Italic = italic ? 1 : 0;
			paragraph.Range.InsertParagraphAfter();
		}

	


		public static Icon GetIconMso(string idMso, int width, int height)
		{
			stdole.IPictureDisp img = Globals.ThisAddIn.Application.CommandBars.GetImageMso(idMso, width, height);
			Bitmap bitmap = ConvertPixelByPixel(img);
			IntPtr hicon = bitmap.GetHicon();
			return Icon.FromHandle(hicon);
		}

		public static Image GetImageMso(string idMso, int width, int height)
		{
			stdole.IPictureDisp img = Globals.ThisAddIn.Application.CommandBars.GetImageMso(idMso, width, height);
			return ConvertPixelByPixel(img);
		}


		public static void AddField(Word.Document document, Word.Selection selection, int categoryIndex)
		{
			string name = "WORD_HIDDEN_" + categoryIndex.ToString("000");

			if (document.Variables[name] == null)
				document.Variables.Add(name, "C:\\Users\\Mikhail\\Pictures\\image.jpg");

			Word.Field f = selection.Fields.Add(Range: selection.Range, Type: Word.WdFieldType.wdFieldEmpty, Text: "DOCVARIABLE  " + name, PreserveFormatting: true);
			//Word.Field f = selection.Fields.Add(Range: selection.Range, Type: Word.WdFieldType.wdFieldEmpty, Text: "INCLUDEPICTURE  ''C:\\Users\\Mikhail\\Pictures\\image.jpg''", PreserveFormatting: true);
			f.Update();
		}


		// Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, Text:= _
		// "INCLUDEPICTURE  ""C:\\Users\\Mikhail\\Pictures\\image.jpg"" ", _
		// PreserveFormatting:=True

		public static void UpdateField(Word.Document document, Word.Selection selection, int categoryIndex)
		{
			string name = "WORD_HIDDEN_" + categoryIndex.ToString("000");
			if (document.Variables[name] != null)
			{
				document.Variables[name].Value = "C:\\Users\\Mikhail\\Pictures\\image.jpg";
				document.Fields.Update();
			}
		}
	}
}
