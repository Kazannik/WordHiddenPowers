// Ignore Spelling: Utils Util dest

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


using Word = Microsoft.Office.Interop.Word;


#if WORD
using WordHiddenPowers.Repository.Data;

namespace WordHiddenPowers.Utils.WordDocuments

#else
using ProsecutorialSupervision.Repository;

namespace ProsecutorialSupervision.Utils.WordDocuments
#endif
{
	static class Content
	{
		private const int BUFFER_SIZE = 65280;

		public static bool ExistsVariable(Word.Variables array, string variableName)
		{
			Regex regex = new Regex("^" + variableName + "(_\\d+)*$");
			for (int i = 1; i <= array.Count; i++)
			{
				if (regex.IsMatch(array[i].Name)) return true;
			}
			return false;
		}

		public static Word.Variable GetVariable(Word.Variables array, string variableName)
		{
			Regex regex = new Regex("^" + variableName + "(_\\d+)*$");
			for (int i = 1; i <= array.Count; i++)
			{
				if (regex.IsMatch(array[i].Name)) return array[i];
			}
			return default;
		}

		public static string GetVariableValueOrDefault(Word.Variables array, string variableName)
		{
			if (ExistsVariable(array: array, variableName: variableName))
			{
				Word.Variable variable = GetVariable(array: array, variableName: variableName);
				return variable.Value;
			}
			else
			{
				return default;
			}
		}

		public static void CommitVariable(Word.Variables array, string variableName, string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length > BUFFER_SIZE)
			{
				Dialogs.ShowErrorDialog("Длина значения сохраняемой переменной не может превышать 65 280 знаков!");
				return;
			}

			if (ExistsVariable(array: array, variableName: variableName))
			{
				Word.Variable variable = GetVariable(array: array, variableName: variableName);
				variable.Value = value;
			}
			else if (!string.IsNullOrWhiteSpace(value))
			{
				array.Add(variableName, value);
			}
		}

		public static void CommitVariable(Word.Variables array, string variableName, DataSet dataSet)
		{
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
			writer.Close();

			if (ExistsVariable(array, variableName))
				DeleteVariable(array, variableName);

			string xml = builder.ToString();

			if (xml.Length <= BUFFER_SIZE)
			{
				CommitVariable(
					array: array,
					variableName: variableName,
					value: xml);
			}
			else
			{
				int i = 0;
				do
				{
					CommitVariable(
						array: array,
						variableName: variableName + "_" + i.ToString(),
						value: xml.Substring(0, BUFFER_SIZE));
					i += 1;
					xml = xml.Substring(BUFFER_SIZE);
				} while (xml.Length >= BUFFER_SIZE);

				if (xml.Length > 0)
					CommitVariable(
						array: array,
						variableName: variableName + "_" + i.ToString(),
						value: xml);
			}
		}

		public static void DeleteVariable(Word.Variables array, string variableName)
		{
			Word.Variable variable = GetVariable(
				array: array,
				variableName: variableName);
			variable?.Delete();
		}

		public static string GetCaption(Word._Document Doc)
		{
			return GetVariableValueOrDefault(
				array: Doc.Variables,
				variableName: Const.Globals.CAPTION_VARIABLE_NAME);
		}

		public static string GetDescription(Word._Document Doc)
		{
			return GetVariableValueOrDefault(
				array: Doc.Variables,
				variableName: Const.Globals.DESCRIPTION_VARIABLE_NAME);
		}

		public static string GetMLModelName(Word._Document Doc)
		{
			return GetVariableValueOrDefault(
				array: Doc.Variables,
				variableName: Const.Globals.ML_MODEL_VARIABLE_NAME);
		}

		public static DateTime GetDate(Word._Document Doc)
		{
			string value = GetVariableValueOrDefault(
				array: Doc.Variables,
				variableName: Const.Globals.DATE_VARIABLE_NAME);
			if (DateTime.TryParse(value, out DateTime result))
			{
				return result;
			}
			else
			{
				return DateTime.Today;
			}
		}

		public static Table GetTable(Word._Document Doc)
		{
			string value = GetVariableValueOrDefault(
				array: Doc.Variables,
				variableName: Const.Globals.TABLE_VARIABLE_NAME);
			return Table.Create(value);
		}

		public static bool ExistsContent(Word._Document Doc)
		{
			bool result = ExistsVariable(
				array: Doc.Variables,
				variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME);
			if (result)
			{
				return true;
			}
			else
			{
				return ExistsVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME + "_0");
			}
		}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="Doc"></param>
		///// <returns></returns>
		//public static bool ExistsGuid(Word._Document Doc) => ExistsVariable(
		//	array: Doc.Variables,
		//	variableName: Const.Globals.DOC_ID_VARIABLE_NAME);
	}
}
