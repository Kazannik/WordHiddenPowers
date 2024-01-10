using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSuite.HiddenPowers.Model;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace WordSuite.HiddenPowers.Utils
{
    public static class FileSystem
    {
        public static DocumentCollection ImportFiles(string path)
        {
            if (Directory.Exists(path))
            {
                DocumentCollection collection = new DocumentCollection();

                Word._Application application = new Word.Application();
                application.Visible = true;

                foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                {
                    if (file.Extension ==".doc" || file.Extension == ".docx")
                    {
                        Word._Document document = application.Documents.Open(FileName: file.FullName, ReadOnly: true, Visible: false);
                        collection.Add(Document.Create(file.FullName, document));
                        document.Close();
                    }
                }

                application.Quit();
                return collection;
            }
            else
            {
                throw new ArgumentException();
            }
        }       

    }
}
