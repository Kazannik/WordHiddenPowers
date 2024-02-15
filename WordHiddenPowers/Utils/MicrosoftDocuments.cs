using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using WordHiddenPowers.Documents;

namespace WordHiddenPowers.Utils
{
    static class MicrosoftDocuments
    {

        public static void ExportToWord(DocumentCollection collection)
        {
            object oMissing = Missing.Value;

            //Start Word and create a new document.
            Word._Application application;
            Word._Document document;
            application = new Word.Application();
            application.Visible = true;
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


    }
}