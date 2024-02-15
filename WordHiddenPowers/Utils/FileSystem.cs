using System;
using System.IO;
using WordHiddenPowers.Documents;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Utils
{
    static class FileSystem
    {
        public static DocumentCollection ImportFiles(DocumentCollection collection, string path)
        {
            if (Directory.Exists(path))
            {
                Word._Application application = Globals.ThisAddIn.Application;
                application.Visible = true;

                foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                {
                    if (file.Extension == ".doc" || file.Extension == ".docx")
                    {
                        try
                        {
                            Word._Document document = application.Documents.Open(FileName: file.FullName, ReadOnly: true, Visible: false);
                            collection.Add(Document.Create(collection, file.FullName, document));
                            document.Close();
                        }
                        catch (Exception) { }
                    }
                }
                return collection;
            }
            else
            {
                throw new ArgumentException();
            }
        }

    }
}
