using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    static class Xml
    {

        public static RepositoryDataSet GetDataSet(Word._Document Doc)
        {
            RepositoryDataSet dataSet = new RepositoryDataSet();
            Word.Variable content = HiddenPowerDocument.GetVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
            if (content != null)
            {
                SetXml(dataSet, content.Value);               
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

        private static void SetXml(RepositoryDataSet dataSet, string xml)
        {
            StringReader reader = new StringReader(xml);
            dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
            reader.Close();
        }
    }
}
