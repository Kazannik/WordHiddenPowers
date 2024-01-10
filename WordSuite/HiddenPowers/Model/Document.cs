using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;
using HiddenPowerConst = WordHiddenPowers.Const.Globals;
using System.IO;
using WordHiddenPowers.Data;

namespace WordSuite.HiddenPowers.Model
{
    public class Document
    {
        RepositoryDataSet powersDataSet = new RepositoryDataSet();
        
        public string FileName { get; }

        public string Title { get; }

        public DateTime Date { get; }

        public string Description { get; }        

        public Table Table {get;}

        public RepositoryDataSet PowersDataSet { get { return powersDataSet; } }
        
        private Document (string fileName, string title, DateTime date, string description, Table table)
        {
            this.powersDataSet = new RepositoryDataSet();
            this.FileName = fileName;
            this.Title = title;
            this.Date = date;
            this.Description = description;
            this.Table = table;
        }

        public static Document Create (string fileName, Word._Document Doc)
        {
            string titleValue = string.Empty;
            DateTime dateValue = DateTime.MinValue;
            string descriptionValue = string.Empty;
            Table tableValue = null;

            if (Doc.Variables.Count > 0)
            {
                Word.Variable title = GetVariable(Doc.Variables, HiddenPowerConst.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    titleValue = title.Value;
                }

                Word.Variable date = GetVariable(Doc.Variables, HiddenPowerConst.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    dateValue = DateTime.Parse(date.Value);
                }

                Word.Variable description = GetVariable(Doc.Variables, HiddenPowerConst.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    descriptionValue = description.Value;
                }


                Word.Variable table = GetVariable(Doc.Variables, HiddenPowerConst.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    tableValue = Table.Create(table.Value);
                }

                Document document = new Document(fileName, titleValue, dateValue, descriptionValue, tableValue);
                Word.Variable categories = GetVariable(Doc.Variables, HiddenPowerConst.CATEGORIES_VARIABLE_NAME);
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


    }
}
