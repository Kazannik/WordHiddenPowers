// Ignore Spelling: Utils dest Prosecutorial

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

#if WORD
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Notes;
using WordHiddenPowers.Dialogs;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using Table = WordHiddenPowers.Repository.Data.Table;

namespace WordHiddenPowers.Utils
#else
using ProsecutorialSupervision.Repository;
using ProsecutorialSupervision.Dialogs;
using Content = ProsecutorialSupervision.Utils.WordDocuments.Content;

namespace ProsecutorialSupervision.Utils
#endif
{
	static class FileSystem
	{
		/// <summary>
		/// Местоположение библиотек надстройки.
		/// </summary>
		public static DirectoryInfo CurrentDirectory => Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

		/// <summary>
		/// Папка с моделими, размещенная в папке Мои документы.
		/// </summary>
		public static DirectoryInfo MLModelesDirectory
		{
			get
			{
				string userDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Const.Globals.APP_MLMODEL_FOLDER_NAME);
				if (!Directory.Exists(userDirectoryPath))
				{
					Directory.CreateDirectory(userDirectoryPath);
				}
				return new DirectoryInfo(userDirectoryPath);
			}
		}

		public static void GetDataSetFromWordDirectory(string path, ref DocumentDataSet destDataSet)
		{
			if (Directory.Exists(path))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;
				application.ChangeFileOpenDirectory(path);

				destDataSet ??= new DocumentDataSet();

				FileInfo[] files = new DirectoryInfo(path).GetFiles("*.doc*");

				ProgressDialog dialog = new()
				{
					Text = "Импорт данных из документов Word",
					Percent = 0
				};
				Dialogs.Show(dialog);

				int count = 0;
				foreach (FileInfo file in files)
				{
					Debug.WriteLine(file.FullName);

					count++;
					dialog.Percent = count * 100 / files.Length;

					if (file.Extension == ".doc" || file.Extension == ".docx")
					{
						try
						{
							CopyWordDocument(fileName: file.Name, application: application, destDataSet: ref destDataSet);
						}
						catch (COMException ex)
						{
							if (ex.ErrorCode == -2146824090)
							{
								string tmpFile = GetTempFileName(file.Name);
								File.Copy(file.FullName, tmpFile);
								CopyWordDocument(fileName: tmpFile, application: application, destDataSet: ref destDataSet);
								File.Delete(tmpFile);
							}
						}
						finally
						{
							Application.DoEvents();
						}
					}
				}
				dialog.Close();
			}
			else
			{
				throw new ArgumentException();
			}
		}

		private static void CopyWordDocument(string fileName, Word._Application application, ref DocumentDataSet destDataSet)
		{
			Word._Document document = application.Documents.Open(FileName: fileName, ConfirmConversions: false, ReadOnly: true, Visible: false, AddToRecentFiles: false);

			if (Content.ExistsContent(document))
			{
				if (!destDataSet.IsModel)
				{
					CopyModel(document, destDataSet);
				}
				CopyData(document, fileName: fileName, destDataSet);
			}
			document.Close();
		}

		public static void GetDataSetFromWordFile(string fileName, ref DocumentDataSet destDataSet)
		{
			if (File.Exists(fileName))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;
				destDataSet ??= new DocumentDataSet();
				FileInfo file = new(fileName);
				if (file.Extension == ".doc" || file.Extension == ".docx")
				{
					try
					{
						Word._Document document = application.Documents.Open(FileName: file.FullName, ReadOnly: true, Visible: false);
						if (Content.ExistsContent(document))
						{
							if (!destDataSet.IsModel)
							{
								CopyModel(document, destDataSet);
							}
							CopyData(document, fileName, destDataSet);
						}
						document.Close();
					}
					catch (Exception ex)
					{
						Dialogs.ShowErrorDialog(ex.Message);
					}
				}
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public static void GetContentFromTextFile(DocumentDataSet sourceDataSet, string fileName)
		{
			if (File.Exists(fileName))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;
				string lastFileName = string.Empty;
				Word._Document wordDocument = null;
				Documents.Document document;
				List<string> modelFiles = [];

				using (StreamReader reader = new(fileName))
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
				Dialogs.ShowMessageDialog("Копирование данных завешено!");
			}
		}

		/// <summary>
		/// Копирование данных из документа Word в хранилище.
		/// </summary>
		/// <param name="application">Приложение Word.</param>
		/// <param name="filename">Имя файла документа Word (включая путь) из которого копируются данные.</param>
		/// <param name="destDataSet">Хранилище для загрузки данных.</param>
		/// <returns></returns>
		private static bool CopyData(Word._Application application, string fileName, DocumentDataSet destDataSet)
		{
			bool isCorrect = false;
			try
			{
				Word._Document document = application.Documents.Open(FileName: fileName, ReadOnly: true, Visible: false);
				if (Content.ExistsContent(document))
				{
					isCorrect = CopyData(document, fileName, destDataSet);
				}
				document.Close();
			}
			catch (Exception ex)
			{
				isCorrect = false;
				Dialogs.ShowErrorDialog(ex.Message);
			}
			return isCorrect;
		}

		/// <summary>
		/// Копирование данных из документа Word в хранилище.
		/// </summary>
		/// <param name="sourceDocument">Документ Word - источник данных.</param>
		/// <param name="destDataSet">Хранилище для загрузки данных.</param>
		/// <returns></returns>
		private static bool CopyData(Word._Document sourceDocument, string fileName, DocumentDataSet destDataSet)
		{
			DocumentDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out bool isCorrect);
			if (isCorrect)
			{
				string caption = Content.GetCaption(sourceDocument);
				if (string.IsNullOrEmpty(caption))
				{
					Dialogs.ShowErrorDialog(string.Format("В данных файла {0} отсутствуют сведения о наименовании.", fileName));
					return false;
				}
				string description = Content.GetDescription(sourceDocument);
				DateTime date = Content.GetDate(sourceDocument);
				Table table = Content.GetTable(sourceDocument);
				foreach (Note note in sourceDataSet.GetNotes())
				{
					destDataSet.AddNote(note, fileName, caption, description, date);
				}

				// Добавление таблицы.
				if (destDataSet.DocumentKeys.Exists(caption))
					destDataSet.DocumentKeys.Write(caption, fileName, table.ToString());
				else
					destDataSet.DocumentKeys.AddDocumentKeysRow(caption, fileName, table.ToString());
			}
			return isCorrect;
		}

		/// <summary>
		/// Копирование модели хранения данных из документа Word.
		/// </summary>
		/// <param name="sourceDocument">Документ Word - источник данных.</param>
		/// <param name="destDataSet">Хранилище для загрузки модели хранения данных.</param>
		private static void CopyModel(Word._Document sourceDocument, DocumentDataSet destDataSet)
		{
			DocumentDataSet sourceDataSet = Xml.GetCurrentDataSet(sourceDocument, out bool isCorrect);
			if (isCorrect)
			{
				Xml.CopyData(sourceDataSet, destDataSet);
				destDataSet.DecimalNotes.Clear();
				destDataSet.TextNotes.Clear();
				destDataSet.DocumentKeys.Clear();
				destDataSet.WordFiles.Clear();
				destDataSet.DecimalTable.Clear();
				destDataSet.AcceptChanges();
			}
		}

		public static string GetTempFileName(string fileName)
		{
			return Path.Combine(Path.GetTempPath(), fileName);
		}

		/// <summary>
		/// Получает полный к папке, которая служит общим репозиторием для данных, относящихся к приложению, используемых текущим не перемещающимся пользователем.
		/// </summary>
		public static DirectoryInfo LocalApplicationSettingDataDirectory
		{
			get
			{				
				string applicationDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Const.Globals.APP_FOLDER_NAME);
				if (!Directory.Exists(applicationDirectoryPath))
				{
					Directory.CreateDirectory(applicationDirectoryPath);
				}
				return new DirectoryInfo(applicationDirectoryPath);
			}
		}


		public static DirectoryInfo ApplicationUserSettingDirectory
		{
			get
			{
				string applicationDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Const.Globals.APP_FOLDER_NAME);
				if (!Directory.Exists(applicationDirectoryPath))
				{
					Directory.CreateDirectory(applicationDirectoryPath);
				}
				return new DirectoryInfo(applicationDirectoryPath);
			}
		}

		/// <summary>
		/// Получает полный путь к файлу настроек.
		/// </summary>
		/// <returns></returns>
		public static string GetGlobalsSettingFileName()
		{
			return Path.Combine(LocalApplicationSettingDataDirectory.FullName, Const.Globals.APP_FOLDER_NAME + ".setting");
		}

		/// <summary>
		/// Получает полный путь к файлу профилей подключения к большим языковым моделям.
		/// </summary>
		/// <returns></returns>
		public static string GetProfilesFileName()
		{
			return Path.Combine(LocalApplicationSettingDataDirectory.FullName, Const.Globals.APP_FOLDER_NAME + ".profiles");
		}

		/// <summary>
		/// Получает полный путь к файлу истории промптов.
		/// </summary>
		/// <returns></returns>
		public static string GetHistoryFileName()
		{
			return Path.Combine(LocalApplicationSettingDataDirectory.FullName, Const.Globals.APP_FOLDER_NAME + ".story");
		}

		/// <summary>
		/// Получает полный путь к файлу шаблонов профилей подключения к большим языковым моделям.
		/// </summary>
		/// <returns></returns>
		public static string GetProfileTemplatesFileName()
		{
			return Path.Combine(LocalApplicationSettingDataDirectory.FullName, Const.Globals.APP_FOLDER_NAME + ".templates");
		}
	}
}
