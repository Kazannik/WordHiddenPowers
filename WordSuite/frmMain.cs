using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordSuite.HiddenPowers.Model;

namespace WordSuite
{
    public partial class frmMain : Form
    {

        DocumentCollection collection;

        public frmMain()
        {
            InitializeComponent();
            collection = new DocumentCollection();
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {

        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {

        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {

        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void FileImport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this)== DialogResult.OK)
            {
                collection = HiddenPowers.Utils.FileSystem.ImportFiles(dialog.SelectedPath);
                ListRefresh();
            }            
        }

        private void FileExport_Click(object sender, EventArgs e)
        {
            HiddenPowers.Utils.MicrosoftDocuments.ExportToWord(collection);
        }

        private void FileExit_Click(object sender, EventArgs e)
        {

        }




        private void ListRefresh()
        {
            //listBox1.Items.Clear();
            //foreach (Document item in collection)
            //{
            //    listBox1.Items.Add(item.Title);
            //}
        }

        private void TableOpen_Click(object sender, EventArgs e)
        {

        }
    }
}
