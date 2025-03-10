// Ignore Spelling: Utils dest

using System;
using System.Collections.Generic;
using System.IO;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Notes;
using Table = WordHiddenPowers.Repositories.Data.Table;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	static class FileSystemUtil
	{
		public static void GetDataSetFromWordFiles(string path, ref RepositoryDataSet destDataSet)
		{
			if (Directory.Exists(path))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;

				if (destDataSet == null) destDataSet = new RepositoryDataSet();

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
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public static void GetDataSetFromWordFile(string fileName, ref RepositoryDataSet destDataSet)
		{
			if (Directory.Exists(fileName))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;
				if (destDataSet == null) destDataSet = new RepositoryDataSet();
				FileInfo file = new FileInfo(fileName);
				bool loadModel = true;
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
			else
			{
				throw new ArgumentException();
			}
		}
		
		public static void GetContentFromTextFile(RepositoryDataSet sourceDataSet, string fileName)
		{
			if (File.Exists(fileName))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;
				string lastFileName = string.Empty;
				Word._Document wordDocument = null;
				Documents.Document document;
				List<string> modelFiles = new List<string>();

				using (StreamReader reader = new StreamReader(fileName))
				{
					while (!reader.EndOfStream)
					{
						string[] items = reader.ReadLine().Split('\t');
						if (lastFileName != items[0])
						{
							wordDocument?.Save();
							wordDocument?.Close();
							if (File.Exists(items[0]))
							{
								lastFileName = items[0];
								wordDocument = application.Documents.Open(FileName: lastFileName, ReadOnly: false, Visible: false);
								if (!modelFiles.Contains(items[0]))
								{
									Xml.CopyModel(sourceDataSet: sourceDataSet, destDocument: wordDocument);
									modelFiles.Add(items[0]);
								}
							}
						}
						if (items.Length == 5)
						{
							Documents.Document.AddTextNote(document: wordDocument, categoryId: int.Parse(items[1]), subcategoryId: int.Parse(items[2]), rating: int.Parse(items[3]), selectionStart: int.Parse(items[4]), selectionEnd: int.Parse(items[5]));
						}
						else if (items.Length == 6)
						{
							Documents.Document.AddDecimalNote(document: wordDocument, categoryId: int.Parse(items[1]), subcategoryId: int.Parse(items[2]), value: double.Parse(items[6]), rating: int.Parse(items[3]), selectionStart: int.Parse(items[4]), selectionEnd: int.Parse(items[5]));
						}
					}
				}
				wordDocument?.Save();
				wordDocument?.Close();
				ShowDialogUtil.ShowMessageDialog("Копирование данных завешено!");
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
			RepositoryDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out bool isCorrect);
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
			RepositoryDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out bool isCorrect);
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
