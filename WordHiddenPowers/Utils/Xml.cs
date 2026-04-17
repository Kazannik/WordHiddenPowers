// Ignore Spelling: Utils dest

using System;
using System.Data;
using System.IO;
using System.Text;
using WordHiddenPowers.Repository;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	static class Xml
	{
		public static RepositoryDataSet GetCurrentDataSet(Word._Document Doc, out bool isCorrect)
		{
			return (RepositoryDataSet)GetDataSet(Doc: Doc, variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME, out isCorrect);
		}

		public static RepositoryDataSet GetNowAggregatedDataSet(Word._Document Doc, out bool isCorrect)
		{
			return (RepositoryDataSet)GetDataSet(Doc: Doc, variableName: Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, out isCorrect);
		}

		public static RepositoryDataSet GetLastAggregatedDataSet(Word._Document Doc, out bool isCorrect)
		{
			return (RepositoryDataSet)GetDataSet(Doc: Doc, variableName: Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, out isCorrect);
		}

		public static VectorDataSet GetVectorDataSet(Word._Document Doc, out bool isCorrect)
		{
			return (VectorDataSet)GetDataSet(Doc: Doc, variableName: Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, out isCorrect, true);
		}

		/// <summary>
		/// Получение данных.
		/// </summary>
		/// <param name="Doc">Документ Word</param>
		/// <param name="variableName">Имя хранилища данных.</param>
		/// <param name="isCorrect">Признак корректного получения данных их хранилища.</param>
		/// <returns>Хранилище данных.</returns>
		public static DataSet GetDataSet(Word._Document Doc, string variableName, out bool isCorrect, bool isVector = false)
		{
			isCorrect = false;

			DataSet dataSet;
			if (isVector)
				dataSet = new VectorDataSet();
			else
				dataSet = new RepositoryDataSet();

			if (Content.ExistsVariable(array: Doc.Variables, variableName: variableName + "_0"))
			{
				int i = 0;
				string xml = string.Empty;
				while (Content.ExistsVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString()))
				{
					Word.Variable content = Content.GetVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString());
					xml += content.Value;
					i += 1;
				}
				if (!string.IsNullOrEmpty(xml))
					isCorrect = SetXml(dataSet, xml);
			}
			else if (Content.ExistsVariable(array: Doc.Variables, variableName: variableName))
			{
				Word.Variable content = Content.GetVariable(array: Doc.Variables, variableName: variableName);
				if (content != null)
				{
					isCorrect = SetXml(dataSet, content.Value);
				}
			}
			dataSet.AcceptChanges();
			return dataSet;
		}

		/// <summary>
		/// Копирование модели из хранилища в документ.
		/// </summary>
		/// <param name="sourceDataSet">Хранилище данных, в котором содержится модель (шаблон).</param>
		/// <param name="destDocument">Документ, в который производится копирование модели (шаблона)</param>
		public static void CopyModel(RepositoryDataSet sourceDataSet, Word._Document destDocument)
		{
			RepositoryDataSet destDataSet = new RepositoryDataSet();
			CopyModel(sourceDataSet, destDataSet);
			Content.CommitVariable(destDocument.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, destDataSet);
		}

		/// <summary>
		/// Копирование модели из одного хранилище в другое.
		/// </summary>
		/// <param name="sourceDataSet">Хранилище данных, в котором содержится модель (шаблон).</param>
		/// <param name="destDataSet">Хранилище назначения, в которое копируется модель (шаблон).</param>
		public static void CopyModel(RepositoryDataSet sourceDataSet, RepositoryDataSet destDataSet)
		{
			CopyData(sourceDataSet, destDataSet);
			destDataSet.DecimalNotes.Clear();
			destDataSet.TextNotes.Clear();
			destDataSet.DocumentKeys.Clear();
			destDataSet.WordFiles.Clear();
			destDataSet.AcceptChanges();
		}


		/// <summary>
		/// Копирование данных между хранилищами.
		/// </summary>
		/// <param name="sourceDataSet">Источник данных.</param>
		/// <param name="destDataSet">Хранилище данных, в которое производится копирование.</param>
		public static void CopyData(RepositoryDataSet sourceDataSet, RepositoryDataSet destDataSet)
		{
			string sourceXml = GetXml(sourceDataSet);
			if (!SetXml(destDataSet, sourceXml))
			{
				foreach (var row in sourceDataSet.DecimalNotes)
				{
					destDataSet.DecimalNotes.ImportRow(row);
				}
				foreach (var row in sourceDataSet.TextNotes)
				{
					destDataSet.TextNotes.ImportRow(row);
				}
			}
		}

		/// <summary>
		/// Сохранить шаблон данных хранилища в файл в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="fileName">Имя файла (включая путь) в который производится запись.</param>
		public static void SaveSchema(RepositoryDataSet dataSet, string fileName)
		{
			try
			{
				string xml = GetXml(dataSet);
				RepositoryDataSet outDataSet = new RepositoryDataSet();
				if (SetXml(outDataSet, xml))
				{
					outDataSet.WriteXmlSchema(fileName);
				}
			}
			catch (Exception ex)
			{
				Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		/// <summary>
		/// Сохранить данные из хранилища в файл в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="fileName">Имя файла (включая путь) в который производится запись.</param>
		public static void SaveClearData(RepositoryDataSet dataSet, string fileName)
		{
			try
			{
				string xml = GetXml(dataSet);
				RepositoryDataSet outDataSet = new RepositoryDataSet();
				if (SetXml(outDataSet, xml))
				{
					outDataSet.DecimalNotes.Clear();
					outDataSet.TextNotes.Clear();
					outDataSet.WordFiles.Clear();
					outDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);
				}
			}
			catch (Exception ex)
			{
				Dialogs.ShowErrorDialog(ex.Message);
			}
		}


		/// <summary>
		/// Сохранить данные из хранилища в файл в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="fileName">Имя файла (включая путь) в который производится запись.</param>
		public static void SaveData(RepositoryDataSet dataSet, string fileName)
		{
			try
			{
				string xml = GetXml(dataSet);
				RepositoryDataSet outDataSet = new RepositoryDataSet();
				if (SetXml(outDataSet, xml))
					outDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);
			}
			catch (Exception ex)
			{
				Dialogs.ShowErrorDialog(ex.Message);
			}
		}


		/// <summary>
		/// Сохранить данные из хранилища векторов в файл в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="fileName">Имя файла (включая путь) в который производится запись.</param>
		public static void SaveVectorData(VectorDataSet dataSet, string fileName)
		{
			try
			{
				string xml = GetXml(dataSet);
				VectorDataSet outDataSet = new VectorDataSet();
				if (SetXml(outDataSet, xml))
					outDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);
			}
			catch (Exception ex)
			{
				Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		/// <summary>
		/// Получить из хранилища данные в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <returns>Данные в формате XML.</returns>
		private static string GetXml(DataSet dataSet)
		{
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
			writer.Close();
			return builder.ToString();
		}

		/// <summary>
		/// Загрузить XML данные в хранилище.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="xml">Данные в формате XML</param>
		/// <returns></returns>
		private static bool SetXml(DataSet dataSet, string xml)
		{
			xml = FixXml(xml);
			
			StringReader reader = new StringReader(xml);
			bool result = false;
			try
			{
				dataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
				result = true;
			}
			catch (Exception ex)
			{
				dataSet.Reset();
				result = false;
			}
			finally
			{
				reader.Close();
			}
			return result;
		}

		private static string FixXml(string xml)
		{
			return xml
				.Replace("TextPowers", "TextNotes")
				.Replace("DecimalPowers", "DecimalNotes");
		}
	}
}
