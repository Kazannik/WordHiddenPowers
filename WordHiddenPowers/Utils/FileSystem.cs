using System;
using System.Diagnostics;
using System.IO;
using WordHiddenPowers.Data;
using WordHiddenPowers.Repositoryes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    static class FileSystem
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

                            if (HiddenPowerDocument.ExistsContent(document))
                            {
                                if (loadModel)
                                {
                                    CopyModel(document, dataSet);
                                    loadModel = false;
                                }

                                RepositoryDataSet documentDataSet = Xml.GetDataSet(document);
                                string fileName = document.FullName;
                                string caption = HiddenPowerDocument.GetCaption(document);
                                string description = HiddenPowerDocument.GetDescription(document);
                                DateTime date = HiddenPowerDocument.GetDate(document);
                                Table table = HiddenPowerDocument.GetTable(document);
                                foreach (Note note in documentDataSet.GetNotes())
                                {
                                    dataSet.AddNote(note, fileName, caption, description, date);
                                }

                                dataSet.DocumentKeys.AddDocumentKeysRow(caption, fileName, table.ToString());
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
            RepositoryDataSet inputSet = Xml.GetDataSet(Doc);
            Xml.CopyDataSet(inputSet, destinationDataSet);
            destinationDataSet.DecimalPowers.Clear();
            destinationDataSet.TextPowers.Clear();
            destinationDataSet.DocumentKeys.Clear();
            destinationDataSet.AcceptChanges();
        }

        
    }
}
