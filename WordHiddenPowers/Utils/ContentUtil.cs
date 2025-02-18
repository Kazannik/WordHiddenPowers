// Ignore Spelling: Utils

using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Data;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	public static class ContentUtil
	{
		public static Word.Variable GetVariable(Word.Variables array, string variableName)
		{
			for (int i = 1; i <= array.Count; i++)
			{
				if (array[i].Name.Equals(variableName, StringComparison.CurrentCultureIgnoreCase))
					return array[i];
			}
			return null;
		}

		public static bool ExistsVariable(Word.Variables array, string variableName)
		{
			return GetVariable(array: array, variableName: variableName) != null;
		}

		public static string GetVariableValue(Word.Variables array, string variableName)
		{
			Word.Variable variable = GetVariable(array: array, variableName: variableName);
			if (variable != null)
				return variable.Value;
			else
				return string.Empty;
		}

		public static void CommitVariable(Word.Variables array, string variableName, string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length > 65280)
			{
				ShowDialogUtil.ShowErrorDialog("Длина значения сохраняемой переменной не может превышать 65 280 знаков!");
				return;
			}

			Word.Variable variable = GetVariable(array: array, variableName: variableName);
			if (variable == null && !string.IsNullOrWhiteSpace(value))
				array.Add(variableName, value);
			else if (variable != null && variable.Value != value)
				variable.Value = value;
		}

		public static void CommitVariable(Word.Variables array, string variableName, RepositoryDataSet dataSet)
		{
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
			writer.Close();

			string xml = builder.ToString();
			if (xml.Length <= 65280)
			{
				CommitVariable(
					array: array, 
					variableName: variableName,
					value: xml);
			}
			else
			{
				if (ExistsVariable(array, variableName))
					DeleteVariable(array, variableName);
				
				int i = 0;
				do
				{
					CommitVariable(
						array: array, 
						variableName: variableName + "_" + i.ToString(),
						value: xml.Substring(0, 65280));
					i += 1;
					xml = xml.Substring(65280);
				} while (xml.Length > 65280);

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
		
		public static string NewGuid(Word._Document Doc)
		{
			bool oldSaved = Doc.Saved;
			CommitVariable(
				array: Doc.Variables,
				variableName: Const.Globals.DOC_ID_VARIABLE_NAME,
				value: Guid.NewGuid().ToString());
			
			Doc.Saved = oldSaved;
			return GetGuid(Doc);
		}
		
		public static string GetGuid(Word._Document Doc)
		{
			return GetVariableValue(
				array: Doc.Variables,
				variableName: Const.Globals.DOC_ID_VARIABLE_NAME);
		}

		public static string GetGuidOrDefault(Word._Document Doc)
		{ 
			string guid = GetGuid(Doc);
			if (!string.IsNullOrEmpty(guid))
			{
				return guid;
			}
			else
			{
				return NewGuid(Doc);
			}
		}
		public static string GetCaption(Word._Document Doc)
		{
			return GetVariableValue(
				array: Doc.Variables, 
				variableName: Const.Globals.CAPTION_VARIABLE_NAME);
		}

		public static string GetDescription(Word._Document Doc)
		{
			return GetVariableValue(
				array: Doc.Variables, 
				variableName: Const.Globals.DESCRIPTION_VARIABLE_NAME);
		}

		public static DateTime GetDate(Word._Document Doc)
		{
			string value = GetVariableValue(
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
			string value = GetVariableValue(
				array: Doc.Variables, 
				variableName: Const.Globals.TABLE_VARIABLE_NAME);
			return Table.Create(value);
		}

		public static bool ExistsContent(Word._Document Doc)
		{
			return GetVariable(
				array: Doc.Variables, 
				variableName: Const.Globals.XML_VARIABLE_NAME) != null;
		}


		public static void UnregisterAllEvents(object objectWithEvents)
		{
			Type theType = objectWithEvents.GetType();

			//Even though the events are public, the FieldInfo associated with them is private
			foreach (System.Reflection.FieldInfo field in theType.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
			{
				//eventInfo will be null if this is a normal field and not an event.
				System.Reflection.EventInfo eventInfo = theType.GetEvent(field.Name);
				if (eventInfo != null)
				{
					if (field.GetValue(objectWithEvents) is MulticastDelegate multicastDelegate)
					{
						foreach (Delegate _delegate in multicastDelegate.GetInvocationList())
						{
							eventInfo.RemoveEventHandler(objectWithEvents, _delegate);
						}
					}
				}
			}
		}
	}
}
