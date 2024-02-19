using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    public static class HiddenPowerDocument
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
            CommitVariable(array: array, variableName: variableName, value: builder.ToString());
        }

        public static void DeleteVariable(Word.Variables array, string variableName)
        {
            Word.Variable variable = GetVariable(array: array, variableName: variableName);
            if (variable != null) variable.Delete();
        }
                
        public static string GetCaption(Word._Document Doc)
        {
            return GetVariableValue(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
        }

        public static string GetDescription(Word._Document Doc)
        {
            return GetVariableValue(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
        }

        public static DateTime GetDate(Word._Document Doc)
        {
            string value = GetVariableValue(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
            DateTime result;
            if (DateTime.TryParse(value, out result))
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
            string value = GetVariableValue(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
            return Table.Create(value);            
        }
        
        public static bool ExistsContent(Word._Document Doc)
        {
            Word.Variable content = HiddenPowerDocument.GetVariable(Doc.Variables,
                   Const.Globals.XML_VARIABLE_NAME);
            return content != null;            
        }
    }
}
