// Ignore Spelling: Utils

using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Repositories;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	static class Xml
	{
		public static RepositoryDataSet GetDataSet(Word._Document Doc, out bool isCorrect)
		{
			return GetDataSet(Doc: Doc, variableName: Const.Globals.XML_VARIABLE_NAME, out isCorrect);
		}

		public static RepositoryDataSet GetDataSet(Word._Document Doc, string variableName, out bool isCorrect) 
		{
			isCorrect = false;

			RepositoryDataSet dataSet = new RepositoryDataSet();
			Word.Variable content = ContentUtil.GetVariable(array: Doc.Variables, variableName: variableName);
			if (content != null)
			{
				isCorrect = SetXml(dataSet, content.Value);
			}
			else
			{
				int i = 0;
				string xml = string.Empty;
				while (ContentUtil.ExistsVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString()))
				{
					content = ContentUtil.GetVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString());
					xml += content.Value;
					i += 1;
				}
				if (!string.IsNullOrEmpty(xml)) isCorrect = SetXml(dataSet, xml);
			}
			dataSet.AcceptChanges();
			return dataSet;
		}

		public static void CopyDataSet(RepositoryDataSet sourceDataSet, RepositoryDataSet destinationDataSet)
		{
			string sourceXml = GetXml(sourceDataSet);
			SetXml(destinationDataSet, sourceXml);
		}

		public static void SaveData(RepositoryDataSet dataSet, string fileName)
		{
			try
			{
				string xml = GetXml(dataSet);
				RepositoryDataSet outDataSet = new RepositoryDataSet();
				SetXml(outDataSet, xml);
				outDataSet.DecimalPowers.Clear();
				outDataSet.TextPowers.Clear();
				outDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		private static string GetXml(RepositoryDataSet dataSet)
		{
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
			writer.Close();
			return builder.ToString();
		}

		private static bool SetXml(RepositoryDataSet dataSet, string xml)
		{
			StringReader reader = new StringReader(xml);
			try
			{
				dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
			}
			catch (Exception ex)
			{
				dataSet.Reset();
				ShowDialogUtil.ShowErrorDialog(ex.Message);
				return false;
			}
			finally
			{
				reader.Close();				
			}
			return true;
		}
	}
}
