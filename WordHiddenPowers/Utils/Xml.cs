// Ignore Spelling: Utils dest

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
		public static RepositoryDataSet GetCurrentDataSet(Word._Document Doc, out bool isCorrect)
		{
			return GetDataSet(Doc: Doc, variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME, out isCorrect);
		}

		public static RepositoryDataSet GetAggregatedDataSet(Word._Document Doc, out bool isCorrect)
		{
			return GetDataSet(Doc: Doc, variableName: Const.Globals.XML_AGGREGATED_VARIABLE_NAME, out isCorrect);
		}

		/// <summary>
		/// Получение данных.
		/// </summary>
		/// <param name="Doc">Документ Word</param>
		/// <param name="variableName">Имя хранилища данных.</param>
		/// <param name="isCorrect">Признак корректного получения данных их хранилища.</param>
		/// <returns>Хранилище данных.</returns>
		public static RepositoryDataSet GetDataSet(Word._Document Doc, string variableName, out bool isCorrect) 
		{			
			isCorrect = false;
			RepositoryDataSet dataSet = new RepositoryDataSet();
		
			if (ContentUtil.ExistsVariable(array: Doc.Variables, variableName: variableName + "_0"))
			{
				int i = 0;
				string xml = string.Empty;
				while (ContentUtil.ExistsVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString()))
				{
					Word.Variable content = ContentUtil.GetVariable(array: Doc.Variables, variableName: variableName + "_" + i.ToString());
					xml += content.Value;
					i += 1;
				}
				if (!string.IsNullOrEmpty(xml))
					isCorrect = SetXml(dataSet, xml);
			}
			else if (ContentUtil.ExistsVariable(array: Doc.Variables, variableName: variableName))
			{
				Word.Variable content = ContentUtil.GetVariable(array: Doc.Variables, variableName: variableName);
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
			Xml.CopyData(sourceDataSet, destDataSet);
			destDataSet.DecimalPowers.Clear();
			destDataSet.TextPowers.Clear();
			destDataSet.DocumentKeys.Clear();
			destDataSet.WordFiles.Clear();
			destDataSet.AcceptChanges();
			ContentUtil.CommitVariable(destDocument.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, destDataSet);
		}

		/// <summary>
		/// Копирование данных между хранилищами.
		/// </summary>
		/// <param name="sourceDataSet">Источник данных.</param>
		/// <param name="destDataSet">Хранилище данных, в которое производится копирование.</param>
		public static void CopyData(RepositoryDataSet sourceDataSet, RepositoryDataSet destDataSet)
		{
			string sourceXml = GetXml(sourceDataSet);
			SetXml(destDataSet, sourceXml);
		}

		/// <summary>
		/// Сохранить шаблон данных хранилища в файл в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <param name="fileName">Имя файла (включая путь) в который производится запись.</param>
		public static void SaveDataSchema(RepositoryDataSet dataSet, string fileName)
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
				SetXml(outDataSet, xml);
				outDataSet.WriteXml(fileName, XmlWriteMode.WriteSchema);
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		/// <summary>
		/// Получить из хранилища данные в формате XML.
		/// </summary>
		/// <param name="dataSet">Хранилище данных.</param>
		/// <returns>Данные в формате XML.</returns>
		private static string GetXml(RepositoryDataSet dataSet)
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
