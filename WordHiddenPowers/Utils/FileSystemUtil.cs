// Ignore Spelling: Utils

using System;
using System.IO;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Data;
using WordHiddenPowers.Repositories.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
	static class FileSystemUtil
	{
		public static RepositoryDataSet ImportFiles(string path)
		{
			if (Directory.Exists(path))
			{
				Word._Application application = Globals.ThisAddIn.Application;
				application.Visible = true;

				RepositoryDataSet dataSet = new RepositoryDataSet();

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
									CopyModel(document, dataSet);
									loadModel = false;
								}

								bool isCorrect;
								RepositoryDataSet documentDataSet = Xml.GetDataSet(document, out isCorrect);
								if (isCorrect)
								{
									string fileName = document.FullName;
									string caption = ContentUtil.GetCaption(document);
									string description = ContentUtil.GetDescription(document);
									DateTime date = ContentUtil.GetDate(document);
									Table table = ContentUtil.GetTable(document);
									foreach (Note note in documentDataSet.GetNotes())
									{
										dataSet.AddNote(note, fileName, caption, description, date);
									}
									dataSet.DocumentKeys.AddDocumentKeysRow(caption, fileName, table.ToString());
								}
							}
							document.Close();
						}
						catch (Exception ex)
						{
							ShowDialogUtil.ShowErrorDialog(ex.Message);
						}
					}
				}
				return dataSet;
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

				RepositoryDataSet dataSet = new RepositoryDataSet();

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
									CopyModel(document, dataSet);
									loadModel = false;
								}

								bool isCorrect;
								RepositoryDataSet documentDataSet = Xml.GetDataSet(document, out isCorrect);
								if (isCorrect)
								{
									string fileName = document.FullName;
									string caption = ContentUtil.GetCaption(document);
									string description = ContentUtil.GetDescription(document);
									DateTime date = ContentUtil.GetDate(document);
									Table table = ContentUtil.GetTable(document);
									foreach (Note note in documentDataSet.GetNotes())
									{
										dataSet.AddNote(note, fileName, caption, description, date);
									}
									dataSet.DocumentKeys.AddDocumentKeysRow(caption, fileName, table.ToString());
								}
							}
							document.Close();
						}
						catch (Exception ex)
						{
							ShowDialogUtil.ShowErrorDialog(ex.Message);
						}
					}
				}
				return dataSet;
			}
			else
			{
				throw new ArgumentException();
			}
		}
						
		private static void CopyModel(Word._Document Doc, RepositoryDataSet destinationDataSet)
		{
			bool isCorrect;
			RepositoryDataSet inputSet = Xml.GetDataSet(Doc, out isCorrect);
			if (isCorrect)
			{
				Xml.CopyDataSet(inputSet, destinationDataSet);
				destinationDataSet.DecimalPowers.Clear();
				destinationDataSet.TextPowers.Clear();
				destinationDataSet.DocumentKeys.Clear();
				destinationDataSet.AcceptChanges();
			}
		}
	}
}
