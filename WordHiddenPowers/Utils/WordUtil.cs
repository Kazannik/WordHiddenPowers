using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	public static class WordUtil
	{
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
