using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using ChromaDB.Client;
using System.Net.Http;



namespace DocumentsGrinder.MsWord
{
	internal class Converter
	{
		public void ToText(string fileName) => ToText(new string[] { fileName });
		
		public void ToText(string[] files)
		{
			//OllamaApiClient ollamaClient = new OllamaApiClient("http://localhost:11434");

			Word._Application oWord;
			oWord = new Word.Application
			{
				Visible = true
			};

			foreach (string file in files) 
			{
				//ToText(ollamaClient, "mistral:latest", oWord, file);			
			}
			//oWord.Quit();			
		}

		private async Task ToText(OllamaApiClient ollamaClient, string model, Word._Application word, string fileName)
		{
			object oFileName = fileName;
			object oMissing = Missing.Value;

			//Start Word and open document.
			Word._Document oDoc;
			
			oDoc = word.Documents.Open( ref oFileName, 
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			foreach (Word.Paragraph paragraph in oDoc.Content.Paragraphs)
			{
				string paragraphText = paragraph.Range.Text;

				var embed = await EmbedAsync(ollamaClient: ollamaClient,
					model: model,
					values: new string[] { paragraphText });


				var configOptions = new ChromaConfigurationOptions(uri: "http://localhost:8000/api/v1/");
				var httpClient = new HttpClient();
				var client = new ChromaClient(configOptions, httpClient);

				string version = await client.GetVersion();

				var string5Collection = await client.GetOrCreateCollection("string5");
				var string5Client = new ChromaCollectionClient(string5Collection, configOptions, httpClient);

				await string5Client.Add(ids: embed.Select(x=> x.Dimensions.ToString()).ToList(), embeddings: embed.Select(x=>x.Vector).ToList());

				var getResult = await string5Client.Get("340a36ad-c38a-406c-be38-250174aee5a4", include: ChromaGetInclude.Metadatas | ChromaGetInclude.Documents | ChromaGetInclude.Embeddings);
				Console.WriteLine($"ID: {getResult.Id}");

				//var queryData = await string5Client.Query([new([1f, 0.5f, 0f, -0.5f, -1f]), new([1.5f, 0f, 2f, -1f, -1.5f])], include: ChromaQueryInclude.Metadatas | ChromaQueryInclude.Distances);
				//foreach (var item in queryData)
				//{
				//	foreach (var entry in item)
				//	{
				//		Console.WriteLine($"ID: {entry.Id} | Distance: {entry.Distance}");
				//	}
				//}

				string result = string.Empty;
				foreach (Embedding<float> embedding in embed)
				{
					result += string.Join(", ", embedding.Vector.ToArray());
				}
			}
		}


		//private async Task<IEnumerable<Embedding<float>>> EmbedAsync(OllamaApiClient ollamaClient, string model, IEnumerable<string> values)
		//{
		//	ollamaClient.SelectedModel = model;
		//	IEmbeddingGenerator<string, Embedding<float>> generator = ollamaClient;
		//	return await generator.GenerateAsync(values);
		//}


		private void button1_Click(object sender, System.EventArgs e)
		{
			object oMissing = Missing.Value;
			object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

			//Start Word and create a new document.
			Word._Application oWord;
			Word._Document oDoc;
			oWord = new Word.Application
			{
				Visible = true
			};
			oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			//Insert a paragraph at the beginning of the document.
			Word.Paragraph oPara1;
			oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
			oPara1.Range.Text = "Heading 1";
			oPara1.Range.Font.Bold = 1;
			oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
			oPara1.Range.InsertParagraphAfter();

			//Insert a paragraph at the end of the document.
			Word.Paragraph oPara2;
			object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
			oPara2.Range.Text = "Heading 2";
			oPara2.Format.SpaceAfter = 6;
			oPara2.Range.InsertParagraphAfter();

			//Insert another paragraph.
			Word.Paragraph oPara3;
			oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
			oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
			oPara3.Range.Font.Bold = 0;
			oPara3.Format.SpaceAfter = 24;
			oPara3.Range.InsertParagraphAfter();

			//Insert a 3 x 5 table, fill it with data, and make the first row
			//bold and italic.
			Word.Table oTable;
			Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oTable = oDoc.Tables.Add(wrdRng, 3, 5, ref oMissing, ref oMissing);
			oTable.Range.ParagraphFormat.SpaceAfter = 6;
			int r, c;
			string strText;
			for (r = 1; r <= 3; r++)
				for (c = 1; c <= 5; c++)
				{
					strText = "r" + r + "c" + c;
					oTable.Cell(r, c).Range.Text = strText;
				}
			oTable.Rows[1].Range.Font.Bold = 1;
			oTable.Rows[1].Range.Font.Italic = 1;

			//Add some text after the table.
			Word.Paragraph oPara4;
			oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
			oPara4.Range.InsertParagraphBefore();
			oPara4.Range.Text = "And here's another table:";
			oPara4.Format.SpaceAfter = 24;
			oPara4.Range.InsertParagraphAfter();

			//Insert a 5 x 2 table, fill it with data, and change the column widths.
			wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oTable = oDoc.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
			oTable.Range.ParagraphFormat.SpaceAfter = 6;
			for (r = 1; r <= 5; r++)
				for (c = 1; c <= 2; c++)
				{
					strText = "r" + r + "c" + c;
					oTable.Cell(r, c).Range.Text = strText;
				}
			oTable.Columns[1].Width = oWord.InchesToPoints(2); //Change width of columns 1 & 2
			oTable.Columns[2].Width = oWord.InchesToPoints(3);

			//Keep inserting text. When you get to 7 inches from top of the
			//document, insert a hard page break.
			object oPos;
			double dPos = oWord.InchesToPoints(7);
			oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertParagraphAfter();
			do
			{
				wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
				wrdRng.ParagraphFormat.SpaceAfter = 6;
				wrdRng.InsertAfter("A line of text");
				wrdRng.InsertParagraphAfter();
				oPos = wrdRng.get_Information
									   (Word.WdInformation.wdVerticalPositionRelativeToPage);
			}
			while (dPos >= Convert.ToDouble(oPos));
			object oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd;
			object oPageBreak = Word.WdBreakType.wdPageBreak;
			wrdRng.Collapse(ref oCollapseEnd);
			wrdRng.InsertBreak(ref oPageBreak);
			wrdRng.Collapse(ref oCollapseEnd);
			wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
			wrdRng.InsertParagraphAfter();

			//Insert a chart.
			Word.InlineShape oShape;
			object oClassType = "MSGraph.Chart.8";
			wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing,
			ref oMissing, ref oMissing, ref oMissing,
			ref oMissing, ref oMissing, ref oMissing);

			//Demonstrate use of late bound oChart and oChartApp objects to
			//manipulate the chart object with MSGraph.
			object oChart;
			object oChartApp;
			oChart = oShape.OLEFormat.Object;
			oChartApp = oChart.GetType().InvokeMember("Application",
			BindingFlags.GetProperty, null, oChart, null);

			//Change the chart type to Line.
			object[] Parameters = new Object[1];
			Parameters[0] = 4; //xlLine = 4
			oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty,
			null, oChart, Parameters);

			//Update the chart image and quit MSGraph.
			oChartApp.GetType().InvokeMember("Update",
			BindingFlags.InvokeMethod, null, oChartApp, null);
			oChartApp.GetType().InvokeMember("Quit",
			BindingFlags.InvokeMethod, null, oChartApp, null);
			//... If desired, you can proceed from here using the Microsoft Graph 
			//Object model on the oChart and oChartApp objects to make additional
			//changes to the chart.

			//Set the width of the chart.
			oShape.Width = oWord.InchesToPoints(6.25f);
			oShape.Height = oWord.InchesToPoints(3.57f);

			//Add text after the chart.
			wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
			wrdRng.InsertParagraphAfter();
			wrdRng.InsertAfter("THE END.");

			//Close this form.
			//this.Close();
		}
	}
}
