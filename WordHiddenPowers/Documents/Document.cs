using System;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using WordHiddenPowers.Data;
using System.Text;
namespace WordHiddenPowers.Documents
{
    public class Document
    {
        public string FileName { get; }

        public string Title { get; }

        public DateTime Date { get; }

        public string Description { get; }

        public Table Table { get; }

        public RepositoryDataSet PowersDataSet { get; }

        private Document(string fileName, string title, DateTime date, string description, Table table)
        {
            PowersDataSet = new RepositoryDataSet();
            FileName = fileName;
            Title = title;
            Date = date;
            Description = description;
            Table = table;
        }

        public static Document Create(string fileName, Word._Document Doc)
        {
            string titleValue = string.Empty;
            DateTime dateValue = DateTime.MinValue;
            string descriptionValue = string.Empty;
            Table tableValue = null;

            if (Doc.Variables.Count > 0)
            {
                Word.Variable title = GetVariable(Doc.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    titleValue = title.Value;
                }

                Word.Variable date = GetVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    dateValue = DateTime.Parse(date.Value);
                }

                Word.Variable description = GetVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    descriptionValue = description.Value;
                }


                Word.Variable table = GetVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    tableValue = Table.Create(table.Value);
                }

                Document document = new Document(fileName, titleValue, dateValue, descriptionValue, tableValue);
                Word.Variable categories = GetVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
                if (categories != null)
                {
                    StringReader reader = new StringReader(categories.Value);
                    document.PowersDataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
                    reader.Close();
                }
                return document;
            }
            else
            {
                return null;
            }
        }

        private static Word.Variable GetVariable(Word.Variables array, string variableName)
        {
            for (int i = 1; i <= array.Count; i++)
            {
                if (array[i].Name == variableName)
                {
                    return array[i];
                }
            }
            return null;
        }

        public string PowersDataSetToXml()
        {
            string xml = GetXml(PowersDataSet);
            return xml;
        }

        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
        }
    }
}
