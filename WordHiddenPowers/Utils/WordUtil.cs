// Ignore Spelling: Mso Utils Util

using System;
using System.Drawing;
using System.Reflection;
using WordHiddenPowers.Documents;
using static WordHiddenPowers.Utils.Gdi32;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	public static class WordUtil
	{
		public static void ExportToWord(DocumentCollection collection)
		{
			object oMissing = Missing.Value;

			//Start Word and create a new document.
			Word._Application application;
			Word._Document document;
			application = new Word.Application
			{
				Visible = true
			};
			document = application.Documents.Add(ref oMissing, ref oMissing,
			ref oMissing, ref oMissing);

			foreach (Document item in collection)
			{
				Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
				paragraph.Range.Text = item.Caption;
				paragraph.Range.Font.Bold = 1;
				paragraph.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
				paragraph.Range.InsertParagraphAfter();

				paragraph = document.Content.Paragraphs.Add(ref oMissing);
				paragraph.Range.Text = item.Date.ToShortDateString();
				paragraph.Range.Font.Bold = 1;
				paragraph.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
				paragraph.Range.InsertParagraphAfter();
			}





			//Insert a paragraph at the end of the document.
			Word.Paragraph oPara2;
			oPara2 = document.Content.Paragraphs.Add(ref oMissing);
			oPara2.Range.Text = "Heading 2";
			oPara2.Format.SpaceAfter = 6;
			oPara2.Range.InsertParagraphAfter();

			//Insert another paragraph.
			Word.Paragraph oPara3;
			oPara3 = document.Content.Paragraphs.Add(ref oMissing);
			oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
			oPara3.Range.Font.Bold = 0;
			oPara3.Format.SpaceAfter = 24;
			oPara3.Range.InsertParagraphAfter();




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
