using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace WordHiddenPowers.Utils
{
	public static class HTMLClipboard
	{
		internal const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
		private static readonly Regex beginFragment = new Regex("<!--StartFragment-->", options);
		private static readonly Regex endFragment = new Regex("<!--EndFragment-->", options);

		public static void Copy(DataGridViewSelectedCellCollection cells, Encoding encoding)
		{
			int minColumn = (from DataGridViewCell cell in cells select cell.ColumnIndex).Min();
			int maxColumn = (from DataGridViewCell cell in cells select cell.ColumnIndex).Max();
			int minRow = (from DataGridViewCell cell in cells select cell.RowIndex).Min();
			int maxRow = (from DataGridViewCell cell in cells select cell.RowIndex).Max();

			string[,] copytext = new string[maxColumn - minColumn, maxRow - minRow];
			foreach (DataGridViewCell cell in cells)
			{
				copytext[cell.ColumnIndex - minColumn, cell.RowIndex - minRow] = cell.Value.ToString();
			}
			Copy(copytext, encoding, maxRow - minRow);
		}

		// HTMLClipboard.Copy(New String(,) {{1, 2, 3, 4, 5, 6}, {3, 4, 5, 6, 7, 8}, {"один", "два", " three ", "for", "five", Nothing}}, 2)

		public static void Copy(Array array, Encoding encoding, int textAlignCenterRowCount)
		{
			int rowCount = array.GetLength(0);
			int columnCount = array.GetLength(1);

			int startHTML, endHTML, startFragment, endFragment;

			XmlWriterSettings settings = new XmlWriterSettings
			{
				//Indent = true,
				//IndentChars = "    ",
				NewLineChars = Environment.NewLine,
				//NewLineOnAttributes = true,
				Encoding = Encoding.UTF8
			};

			XmlDocument document = new XmlDocument();
			XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", settings.Encoding.WebName, null);
			document.AppendChild(declaration);
			document.Schemas = XmlSchemaCreate();
			document.XmlResolver = null;

			int declarationLength = settings.NewLineChars.Length + GetLength(document, settings);
			try
			{
				XmlDocumentType docType = document.CreateDocumentType("HTML", null, null, null);
				document.AppendChild(docType);
			}
			catch { }

			XmlElement xmlHTML = document.CreateElement("HTML");
			document.AppendChild(xmlHTML);
			XmlElement xmlHEAD = document.CreateElement("HEAD");
			xmlHTML.AppendChild(xmlHEAD);
			XmlElement xmlStyle = document.CreateElement("style");
			xmlStyle.SetAttribute("type", "text/css");
			xmlStyle.InnerText = @".style1 {width: 100%; border-collapse: collapse; border-style: solid; border-width: 1px;}";
			xmlHEAD.AppendChild(xmlStyle);

			XmlElement xmlBODY = document.CreateElement("BODY");
			xmlHTML.AppendChild(xmlBODY);

			XmlComment xmlBegin = document.CreateComment("StartFragment");
			xmlBODY.AppendChild(xmlBegin);

			XmlElement xmlTable = document.CreateElement("table");
			xmlTable.SetAttribute("border", "1");
			xmlTable.SetAttribute("cellpadding", "0");
			xmlTable.SetAttribute("class", "style1");

			for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
			{
				XmlElement xmlRow = document.CreateElement("tr");
				xmlTable.AppendChild(xmlRow);
				for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
				{
					XmlElement xmlCell = document.CreateElement("td");
					// cell.SetAttribute("style", "text-align: left");
					if (rowIndex < textAlignCenterRowCount)
					{
						xmlCell.SetAttribute("style", "text-align: center");
					}
					else
					{
						xmlCell.SetAttribute("style", "text-align: right");
					}
					// cell.SetAttribute("style", "text-align: justify");
					xmlRow.AppendChild(xmlCell);

					if (array.GetValue(rowIndex, columnIndex) == null)
					{
						xmlCell.InnerText = string.Empty;
					}
					else
					{
						xmlCell.InnerText = encoding.GetString(Encoding.Convert(encoding, settings.Encoding, encoding.GetBytes(array.GetValue(rowIndex, columnIndex).ToString())));
					}
				}
			}
			int fragmentLength = GetLength(xmlTable, settings) - declarationLength;

			xmlBODY.AppendChild(xmlTable);

			XmlComment xmlEnd = document.CreateComment("EndFragment");
			xmlBODY.AppendChild(xmlEnd);

			string documentText = GetText(document: document, settings: settings);
			documentText = documentText.Substring(declarationLength);

			startHTML = 0;
			endHTML = startHTML + GetLength(document, settings) - declarationLength;
			startFragment = startHTML;
			endFragment = startFragment + fragmentLength;

			string description = GetDescription(startHTML, endHTML, startFragment, endFragment);
			Match beginMatch = beginFragment.Match(documentText);
			Match endMatch = HTMLClipboard.endFragment.Match(documentText);
			do
			{
				startHTML = description.Length;
				endHTML = startHTML + documentText.Length;
				startFragment = startHTML + beginMatch.Index + beginMatch.Length;
				endFragment = startHTML + endMatch.Index - settings.NewLineChars.Length;
				description = GetDescription(startHTML, endHTML, startFragment, endFragment);
			}
			while (startHTML != description.Length);
			string data = description + documentText;
			Clipboard.SetDataObject(new DataObject(DataFormats.Html, data), true);
		}

		private static string GetDescription(int startHTML, int endHTML, int startFragment, int endFragment)
		{
			return string.Format("Version:0.9\r\nStartHTML:{0}\r\nEndHTML:{1}\r\nStartFragment:{2}\r\nEndFragment:{3}\r\n",
				startHTML, endHTML, startFragment, endFragment);
		}

		private static int GetLength(XmlElement element, XmlWriterSettings settings)
		{
			StringWriter stringWriter = new StringWriter();
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
			element.WriteTo(xmlWriter);
			xmlWriter.Flush();
			string context = stringWriter.ToString();
			return context.Length;
		}

		private static int GetLength(XmlDocument document, XmlWriterSettings settings)
		{
			string context = GetText(document: document, settings: settings);
			return context.Length;
		}

		private static string GetText(XmlDocument document, XmlWriterSettings settings)
		{
			StringWriter stringWriter = new StringWriter();
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
			document.WriteTo(xmlWriter);
			xmlWriter.Flush();
			return stringWriter.ToString();
		}

		private static XmlSchemaSet XmlSchemaCreate()
		{
			XmlSchemaElement HTML = new XmlSchemaElement
			{
				Name = "HTML"
			};

			XmlSchemaElement HEAD = new XmlSchemaElement
			{
				Name = "HEAD"
			};

			XmlSchemaElement style = new XmlSchemaElement
			{
				Name = "style",
				SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaElement BODY = new XmlSchemaElement
			{
				Name = "BODY"
			};

			XmlSchemaElement table = new XmlSchemaElement
			{
				Name = "table"
			};

			XmlSchemaElement tr = new XmlSchemaElement
			{
				Name = "tr"
			};

			XmlSchemaElement td = new XmlSchemaElement
			{
				Name = "td",
				SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaAttribute typeAttribute = new XmlSchemaAttribute
			{
				Name = "type",
				Use = XmlSchemaUse.Required,
				SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaAttribute borderAttribute = new XmlSchemaAttribute
			{
				Name = "border",
				Use = XmlSchemaUse.Required,
				SchemaTypeName = new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaAttribute cellPaddingAttribute = new XmlSchemaAttribute
			{
				Name = "cellpadding",
				Use = XmlSchemaUse.Required,
				SchemaTypeName = new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaAttribute classAttribute = new XmlSchemaAttribute
			{
				Name = "class",
				Use = XmlSchemaUse.Required,
				SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaAttribute styleAttribute = new XmlSchemaAttribute
			{
				Name = "style",
				Use = XmlSchemaUse.Required,
				SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema")
			};

			XmlSchemaComplexType tdType = new XmlSchemaComplexType();
			tdType.Attributes.Add(styleAttribute);
			td.SchemaType = tdType;

			XmlSchemaComplexType trType = new XmlSchemaComplexType();
			XmlSchemaSequence trSequence = new XmlSchemaSequence();
			trSequence.Items.Add(td);
			trType.Particle = trSequence;

			XmlSchemaComplexType tableType = new XmlSchemaComplexType();
			XmlSchemaSequence tableSequence = new XmlSchemaSequence();
			tableSequence.Items.Add(tr);
			tableType.Particle = tableSequence;
			tableType.Attributes.Add(borderAttribute);
			tableType.Attributes.Add(cellPaddingAttribute);
			tableType.Attributes.Add(classAttribute);
			table.SchemaType = tableType;

			XmlSchemaComplexType bodyType = new XmlSchemaComplexType();
			XmlSchemaSequence bodySequence = new XmlSchemaSequence();
			bodySequence.Items.Add(table);
			bodyType.Particle = bodySequence;

			XmlSchemaComplexType styleType = new XmlSchemaComplexType();
			XmlSchemaSequence styleSequence = new XmlSchemaSequence();
			styleSequence.Items.Add(HEAD);
			styleType.Particle = styleSequence;
			styleType.Attributes.Add(typeAttribute);
			style.SchemaType = styleType;

			XmlSchemaComplexType headerType = new XmlSchemaComplexType();
			XmlSchemaSequence headerSequence = new XmlSchemaSequence();
			headerSequence.Items.Add(style);
			headerType.Particle = headerSequence;

			XmlSchemaComplexType htmlType = new XmlSchemaComplexType();
			XmlSchemaSequence htmlSequence = new XmlSchemaSequence();
			htmlSequence.Items.Add(HEAD);
			htmlSequence.Items.Add(BODY);
			htmlType.Particle = htmlSequence;

			XmlSchema clipboadrSchema = new XmlSchema();
			clipboadrSchema.Items.Add(HTML);

			XmlSchemaSet clipboadrSchemaSet = new XmlSchemaSet();
			clipboadrSchemaSet.ValidationEventHandler += ValidationCallback;
			clipboadrSchemaSet.Add(clipboadrSchema);
			clipboadrSchemaSet.Compile();

			return clipboadrSchemaSet;
		}

		static void ValidationCallback(object sender, ValidationEventArgs args)
		{
			//string promt = string.Empty;
			//MsgBoxStyle Dim style As  = MsgBoxStyle.Information
			if (args.Severity == XmlSeverityType.Warning)
			{
				//promt = "Внимание: "
				//style = MsgBoxStyle.Exclamation
			}
			else if (args.Severity == XmlSeverityType.Error)
			{
				//promt = "Ошибка: "
				//style = MsgBoxStyle.Critical
			}
			//MsgBox(promt & args.Message, style)
		}
	}
}

