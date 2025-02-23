// Ignore Spelling: Utils

using System;
using System.Data;
using System.IO;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Data;
using WordHiddenPowers.Repositories.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	static class FileSystemUtil
	{
		public static RepositoryDataSet GetDataSetFromWordFiles(string path)
		{
			if (Directory.Exists(path))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;

				RepositoryDataSet destDataSet = new RepositoryDataSet();

				FileInfo[] files = new DirectoryInfo(path).GetFiles("*.doc*");
				bool loadModel = true;

				foreach (FileInfo file in files)
				{
					if (file.Extension == ".doc" || file.Extension == ".docx")
					{
						try
						{
							Word._Document document = application.Documents.Open(FileName: file.FullName, ReadOnly: true, Visible: false);

							if (ContentUtil.ExistsContent(document))
							{
								if (loadModel)
								{
									CopyModel(document, destDataSet);
									loadModel = false;
								}
								CopyData(document, destDataSet);								
							}
							document.Close();
						}
						catch (Exception ex)
						{
							ShowDialogUtil.ShowErrorDialog(ex.Message);
						}
					}
				}
				return destDataSet;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public static RepositoryDataSet ImportFile(string path)
		{
			if (Directory.Exists(path))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;

				RepositoryDataSet destDataSet = new RepositoryDataSet();

				FileInfo[] files = new DirectoryInfo(path).GetFiles("*.doc*");
				bool loadModel = true;

				foreach (FileInfo file in files)
				{
					if (file.Extension == ".doc" || file.Extension == ".docx")
					{
						try
						{
							Word._Document document = application.Documents.Open(FileName: file.FullName, ReadOnly: true, Visible: false);

							if (ContentUtil.ExistsContent(document))
							{
								if (loadModel)
								{
									CopyModel(document, destDataSet);
									loadModel = false;
								}
								CopyData(document, destDataSet);
							}
							document.Close();
						}
						catch (Exception ex)
						{
							ShowDialogUtil.ShowErrorDialog(ex.Message);
						}
					}
				}
				return destDataSet;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		/// <summary>
		/// Копирование данных из документа Word в хранилище.
		/// </summary>
		/// <param name="application">Приложение Word.</param>
		/// <param name="filename">Имя файла документа Word (включая путь) из которого копируются данные.</param>
		/// <param name="destDataSet">Хранилище для загрузки данных.</param>
		/// <returns></returns>
		private static bool CopyData(Word._Application application, string filename, RepositoryDataSet destDataSet)
		{
			bool isCorrect = false;
			try
			{
				Word._Document document = application.Documents.Open(FileName: filename, ReadOnly: true, Visible: false);
				if (ContentUtil.ExistsContent(document))
				{
					isCorrect = CopyData(document, destDataSet);
				}
				document.Close();
			}
			catch (Exception ex)
			{
				isCorrect = false;
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
			return isCorrect;
		}

		
		/// <summary>
		/// Копирование данных из документа Word в хранилище.
		/// </summary>
		/// <param name="sourceDocument">Документ Word - источник данных.</param>
		/// <param name="destDataSet">Хранилище для загрузки данных.</param>
		/// <returns></returns>
		private static bool CopyData(Word._Document sourceDocument, RepositoryDataSet destDataSet)
		{
			bool isCorrect = false;
			RepositoryDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out isCorrect);
			if (isCorrect)
			{
				string fileName = sourceDocument.FullName;
				string caption = ContentUtil.GetCaption(sourceDocument);
				string description = ContentUtil.GetDescription(sourceDocument);
				DateTime date = ContentUtil.GetDate(sourceDocument);
				Table table = ContentUtil.GetTable(sourceDocument);
				foreach (Note note in sourceDataSet.GetNotes())
				{
					destDataSet.AddNote(note, fileName, caption, description, date);
				}
				// Добавление таблицы.
				destDataSet.DocumentKeys.AddDocumentKeysRow(caption, fileName, table.ToString());
			}
			return isCorrect;
		}

		/// <summary>
		/// Копирование модели хранения данных из документа Word.
		/// </summary>
		/// <param name="sourceDocument">Документ Word - источник данных.</param>
		/// <param name="destDataSet">Хранилище для загрузки модели хранения данных.</param>
		private static void CopyModel(Word._Document sourceDocument, RepositoryDataSet destDataSet)
		{
			bool isCorrect;
			RepositoryDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out isCorrect);
			if (isCorrect)
			{
				Xml.CopyData(sourceDataSet, destDataSet);
				destDataSet.DecimalPowers.Clear();
				destDataSet.TextPowers.Clear();
				destDataSet.DocumentKeys.Clear();
				destDataSet.WordFiles.Clear();
				destDataSet.AcceptChanges();
			}
		}
	}
}
