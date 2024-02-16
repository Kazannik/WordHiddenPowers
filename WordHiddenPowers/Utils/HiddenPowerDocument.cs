using System;
using System.Data;
using System.IO;
using System.Text;
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



    }
}
