namespace WordHiddenPowers.Dialogs
{
    partial class AnalyzerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyzerDialog));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExportToCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExportDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileImportFromFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileImportFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileInsertToWord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.insertToDocumentButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1036, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileExportToCsv,
            this.mnuFileExportDictionary,
            this.toolStripMenuItem2,
            this.mnuFileImportFromFolder,
            this.mnuFileImportFromFile,
            this.toolStripMenuItem3,
            this.mnuFileInsertToWord,
            this.toolStripMenuItem4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(65, 29);
            this.mnuFile.Text = "Файл";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(371, 30);
            this.mnuFileNew.Text = "Создать";
            this.mnuFileNew.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(371, 30);
            this.mnuFileOpen.Text = "Открыть...";
            this.mnuFileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(368, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(371, 30);
            this.mnuFileSave.Text = "Сохранить";
            this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(371, 30);
            this.mnuFileSaveAs.Text = "Сохранить как...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.FileSaveAs_Click);
            // 
            // mnuFileExportToCsv
            // 
            this.mnuFileExportToCsv.Name = "mnuFileExportToCsv";
            this.mnuFileExportToCsv.Size = new System.Drawing.Size(371, 30);
            this.mnuFileExportToCsv.Text = "Экспорт данных...";
            this.mnuFileExportToCsv.Click += new System.EventHandler(this.FileExportTo_Click);
            // 
            // mnuFileExportDictionary
            // 
            this.mnuFileExportDictionary.Name = "mnuFileExportDictionary";
            this.mnuFileExportDictionary.Size = new System.Drawing.Size(371, 30);
            this.mnuFileExportDictionary.Text = "Экспорт словаря...";
            this.mnuFileExportDictionary.Click += new System.EventHandler(this.FileExportDictionary_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(368, 6);
            // 
            // mnuFileImportFromFolder
            // 
            this.mnuFileImportFromFolder.Name = "mnuFileImportFromFolder";
            this.mnuFileImportFromFolder.Size = new System.Drawing.Size(371, 30);
            this.mnuFileImportFromFolder.Text = "Импортировать данные из папки...";
            this.mnuFileImportFromFolder.Click += new System.EventHandler(this.FileImportFromFolder_Click);
            // 
            // mnuFileImportFromFile
            // 
            this.mnuFileImportFromFile.Name = "mnuFileImportFromFile";
            this.mnuFileImportFromFile.Size = new System.Drawing.Size(371, 30);
            this.mnuFileImportFromFile.Text = "Импортировать данные из файла...";
            this.mnuFileImportFromFile.Click += new System.EventHandler(this.FileImportFromFile_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(368, 6);
            // 
            // mnuFileInsertToWord
            // 
            this.mnuFileInsertToWord.Name = "mnuFileInsertToWord";
            this.mnuFileInsertToWord.Size = new System.Drawing.Size(371, 30);
            this.mnuFileInsertToWord.Text = "Вставить в открытый документ";
            this.mnuFileInsertToWord.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(368, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(371, 30);
            this.mnuFileExit.Text = "Выход";
            this.mnuFileExit.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewHide});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(54, 29);
            this.mnuView.Text = "Вид";
            // 
            // mnuViewHide
            // 
            this.mnuViewHide.CheckOnClick = true;
            this.mnuViewHide.Name = "mnuViewHide";
            this.mnuViewHide.Size = new System.Drawing.Size(217, 30);
            this.mnuViewHide.Text = "Скрытые записи";
            this.mnuViewHide.Click += new System.EventHandler(this.ViewHide_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToDocumentButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1036, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // insertToDocumentButton
            // 
            this.insertToDocumentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertToDocumentButton.Image = ((System.Drawing.Image)(resources.GetObject("insertToDocumentButton.Image")));
            this.insertToDocumentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertToDocumentButton.Name = "insertToDocumentButton";
            this.insertToDocumentButton.Size = new System.Drawing.Size(24, 25);
            this.insertToDocumentButton.Text = "Вставить в открытый документ";
            this.insertToDocumentButton.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1036, 30);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(179, 25);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 61);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.SplitContainer1_Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(1036, 501);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 3;
            // 
            // AnalyzerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 592);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AnalyzerDialog";
            this.Text = "Анализ данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnalyzerDialog_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }		

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton insertToDocumentButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileInsertToWord;
		private System.Windows.Forms.ToolStripMenuItem mnuFileImportFromFolder;
		private System.Windows.Forms.ToolStripMenuItem mnuFileImportFromFile;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private WordHiddenPowers.Controls.ListControls.StatusListBox statusListBox;
		private WordHiddenPowers.Controls.ListControls.ContentListBox contentListBox;
		private Controls.CategoryBox categoryBox1;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewHide;
		private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExportToCsv;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExportDictionary;
	}
}