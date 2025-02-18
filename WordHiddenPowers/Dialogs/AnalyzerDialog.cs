using System;
using System.Reflection;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class AnalyzerDialog : Form
	{
		private readonly RepositoryDataSet dataSet;

		public AnalyzerDialog(RepositoryDataSet dataSet)
		{
			this.dataSet = dataSet;

			InitializeComponent();

			analyzerListBox.DataSet = this.dataSet;
		}

		private void InsertToDocumentButton_Click(object sender, EventArgs e)
		{
			InsertToWord();
		}



		private void InsertToWord()
		{
			object oMissing = Missing.Value;

			Word._Application application;
			Word._Document document;
			application = Globals.ThisAddIn.Application;
			application.Visible = true;
			document = application.ActiveDocument;

			Category category = null;
			Subcategory subcategory = null;

			foreach (Note note in analyzerListBox.Items)
			{
				if (category == null || note.Category.Guid != category.Guid)
				{
					category = note.Category;

					Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
					paragraph.Range.Text = category.Caption;
					paragraph.Range.Font.Bold = 1;
					paragraph.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
					paragraph.Range.InsertParagraphAfter();
				}

				if (subcategory == null || note.Subcategory.Guid != subcategory.Guid)
				{
					subcategory = note.Subcategory;

					Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
					paragraph.Range.Text = subcategory.Caption;
					paragraph.Range.Font.Bold = 1;
					paragraph.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
					paragraph.Range.InsertParagraphAfter();
				}

				Word.Paragraph oPara;
				oPara = document.Content.Paragraphs.Add(ref oMissing);
				oPara.Range.Text = note.FileCaption + ":  " + note.Value.ToString();
				oPara.Range.Font.Bold = 0;
				oPara.Format.SpaceAfter = 24;
				oPara.Range.InsertParagraphAfter();
			}








			////Insert a paragraph at the end of the document.
			//Word.Paragraph oPara2;
			//oPara2 = document.Content.Paragraphs.Add(ref oMissing);
			//oPara2.Range.Text = "Heading 2";
			//oPara2.Format.SpaceAfter = 6;
			//oPara2.Range.InsertParagraphAfter();

			////Insert another paragraph.
			//Word.Paragraph oPara3;
			//oPara3 = document.Content.Paragraphs.Add(ref oMissing);
			//oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
			//oPara3.Range.Font.Bold = 0;
			//oPara3.Format.SpaceAfter = 24;
			//oPara3.Range.InsertParagraphAfter();




		}

	}
}
