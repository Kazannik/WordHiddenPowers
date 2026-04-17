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
			this.components = new System.ComponentModel.Container();
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
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsDataEmbedding = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.insertToDocumentButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripRatingBox = new ControlLibrary.Controls.ToolStripControls.ToolStripRatingBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.statusListBox = new WordHiddenPowers.Controls.ListControls.StatusListBox();
			this.categoryBox1 = new WordHiddenPowers.Controls.CategoryBox();
			this.contentListBox = new WordHiddenPowers.Controls.ListControls.ContentListBox();
			this.noteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuNewTextContentMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNewDecimalContentMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditContentMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuHideContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuCopyContentMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.noteContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuTools,
            this.mnuServiceToolStripMenuItem});
			resources.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
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
			resources.ApplyResources(this.mnuFile, "mnuFile");
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Name = "mnuFileNew";
			resources.ApplyResources(this.mnuFileNew, "mnuFileNew");
			this.mnuFileNew.Click += new System.EventHandler(this.FileNew_Click);
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Name = "mnuFileOpen";
			resources.ApplyResources(this.mnuFileOpen, "mnuFileOpen");
			this.mnuFileOpen.Click += new System.EventHandler(this.FileOpen_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.Name = "mnuFileSave";
			resources.ApplyResources(this.mnuFileSave, "mnuFileSave");
			this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.Name = "mnuFileSaveAs";
			resources.ApplyResources(this.mnuFileSaveAs, "mnuFileSaveAs");
			this.mnuFileSaveAs.Click += new System.EventHandler(this.FileSaveAs_Click);
			// 
			// mnuFileExportToCsv
			// 
			this.mnuFileExportToCsv.Name = "mnuFileExportToCsv";
			resources.ApplyResources(this.mnuFileExportToCsv, "mnuFileExportToCsv");
			this.mnuFileExportToCsv.Click += new System.EventHandler(this.FileExportTo_ClickAsync);
			// 
			// mnuFileExportDictionary
			// 
			this.mnuFileExportDictionary.Name = "mnuFileExportDictionary";
			resources.ApplyResources(this.mnuFileExportDictionary, "mnuFileExportDictionary");
			this.mnuFileExportDictionary.Click += new System.EventHandler(this.FileExportDictionary_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			// 
			// mnuFileImportFromFolder
			// 
			this.mnuFileImportFromFolder.Name = "mnuFileImportFromFolder";
			resources.ApplyResources(this.mnuFileImportFromFolder, "mnuFileImportFromFolder");
			this.mnuFileImportFromFolder.Click += new System.EventHandler(this.FileImportFromFolder_Click);
			// 
			// mnuFileImportFromFile
			// 
			this.mnuFileImportFromFile.Name = "mnuFileImportFromFile";
			resources.ApplyResources(this.mnuFileImportFromFile, "mnuFileImportFromFile");
			this.mnuFileImportFromFile.Click += new System.EventHandler(this.FileImportFromFile_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			// 
			// mnuFileInsertToWord
			// 
			this.mnuFileInsertToWord.Name = "mnuFileInsertToWord";
			resources.ApplyResources(this.mnuFileInsertToWord, "mnuFileInsertToWord");
			this.mnuFileInsertToWord.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			resources.ApplyResources(this.mnuFileExit, "mnuFileExit");
			this.mnuFileExit.Click += new System.EventHandler(this.FileExit_Click);
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewHide,
            this.toolStripMenuItem5,
            this.mnuViewRefresh});
			this.mnuView.Name = "mnuView";
			resources.ApplyResources(this.mnuView, "mnuView");
			// 
			// mnuViewHide
			// 
			this.mnuViewHide.CheckOnClick = true;
			this.mnuViewHide.Name = "mnuViewHide";
			resources.ApplyResources(this.mnuViewHide, "mnuViewHide");
			this.mnuViewHide.Click += new System.EventHandler(this.ViewHide_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			// 
			// mnuViewRefresh
			// 
			this.mnuViewRefresh.Name = "mnuViewRefresh";
			resources.ApplyResources(this.mnuViewRefresh, "mnuViewRefresh");
			this.mnuViewRefresh.Click += new System.EventHandler(this.ViewRefresh_Click);
			// 
			// mnuTools
			// 
			this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsDataEmbedding});
			this.mnuTools.Name = "mnuTools";
			resources.ApplyResources(this.mnuTools, "mnuTools");
			// 
			// mnuToolsDataEmbedding
			// 
			this.mnuToolsDataEmbedding.Name = "mnuToolsDataEmbedding";
			resources.ApplyResources(this.mnuToolsDataEmbedding, "mnuToolsDataEmbedding");
			this.mnuToolsDataEmbedding.Click += new System.EventHandler(this.ToolsDataEmbedding_Click);
			// 
			// mnuServiceToolStripMenuItem
			// 
			this.mnuServiceToolStripMenuItem.Name = "mnuServiceToolStripMenuItem";
			resources.ApplyResources(this.mnuServiceToolStripMenuItem, "mnuServiceToolStripMenuItem");
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToDocumentButton,
            this.toolStripRatingBox});
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Name = "toolStrip1";
			// 
			// insertToDocumentButton
			// 
			this.insertToDocumentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.insertToDocumentButton, "insertToDocumentButton");
			this.insertToDocumentButton.Name = "insertToDocumentButton";
			this.insertToDocumentButton.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
			// 
			// toolStripRatingBox
			// 
			resources.ApplyResources(this.toolStripRatingBox, "toolStripRatingBox");
			this.toolStripRatingBox.Name = "toolStripRatingBox";
			this.toolStripRatingBox.StarsColor = System.Drawing.SystemColors.ControlText;
			this.toolStripRatingBox.RatingChanged += new System.EventHandler(this.RatingBox_RatingChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.statusListBox);
			this.splitContainer1.Panel1.Resize += new System.EventHandler(this.SplitContainer1_Panel1_Resize);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.contentListBox);
			this.splitContainer1.Panel2.Controls.Add(this.categoryBox1);
			this.splitContainer1.Panel2.Resize += new System.EventHandler(this.SplitContainer1_Panel2_Resize);
			// 
			// statusListBox
			// 
			resources.ApplyResources(this.statusListBox, "statusListBox");
			this.statusListBox.LastDataSet = null;
			this.statusListBox.Name = "statusListBox";
			this.statusListBox.NowDataSet = null;
			this.statusListBox.FirstViewItemChanged += new System.EventHandler<ControlLibrary.Controls.ListControls.ItemEventArgs<WordHiddenPowers.Controls.ListControls.StatusListControl.ListItem>>(this.StatusListBox_FirstViewItemChanged);
			// 
			// categoryBox1
			// 
			resources.ApplyResources(this.categoryBox1, "categoryBox1");
			this.categoryBox1.Name = "categoryBox1";
			this.categoryBox1.Owner = null;
			// 
			// contentListBox
			// 
			this.contentListBox.ContextMenuStrip = this.noteContextMenu;
			this.contentListBox.DataSet = null;
			resources.ApplyResources(this.contentListBox, "contentListBox");
			this.contentListBox.Filter = null;
			this.contentListBox.FilterRating = 0;
			this.contentListBox.Hide = true;
			this.contentListBox.Name = "contentListBox";
			this.contentListBox.ItemMouseDown += new System.EventHandler<ControlLibrary.Controls.ListControls.ItemMouseEventArgs<WordHiddenPowers.Controls.ListControls.ContentListControl.ListItem, WordHiddenPowers.Controls.ListControls.ContentListControl.ListItemNote>>(this.ContentListBox_ItemMouseDown);
			// 
			// noteContextMenu
			// 
			this.noteContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.noteContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewTextContentMenu,
            this.mnuNewDecimalContentMenu,
            this.mnuEditContentMenu,
            this.toolStripMenuItem6,
            this.mnuHideContextMenu,
            this.toolStripMenuItem7,
            this.mnuCopyContentMenu});
			this.noteContextMenu.Name = "contextMenuStrip1";
			resources.ApplyResources(this.noteContextMenu, "noteContextMenu");
			// 
			// mnuNewTextContentMenu
			// 
			this.mnuNewTextContentMenu.Name = "mnuNewTextContentMenu";
			resources.ApplyResources(this.mnuNewTextContentMenu, "mnuNewTextContentMenu");
			this.mnuNewTextContentMenu.Click += new System.EventHandler(this.NewTextContentMenuItem_Click);
			// 
			// mnuNewDecimalContentMenu
			// 
			this.mnuNewDecimalContentMenu.Name = "mnuNewDecimalContentMenu";
			resources.ApplyResources(this.mnuNewDecimalContentMenu, "mnuNewDecimalContentMenu");
			this.mnuNewDecimalContentMenu.Click += new System.EventHandler(this.NewDecimalContentMenuItem_Click);
			// 
			// mnuEditContentMenu
			// 
			this.mnuEditContentMenu.Name = "mnuEditContentMenu";
			resources.ApplyResources(this.mnuEditContentMenu, "mnuEditContentMenu");
			this.mnuEditContentMenu.Click += new System.EventHandler(this.EditContentMenuItem_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			// 
			// mnuHideContextMenu
			// 
			this.mnuHideContextMenu.Name = "mnuHideContextMenu";
			resources.ApplyResources(this.mnuHideContextMenu, "mnuHideContextMenu");
			this.mnuHideContextMenu.Click += new System.EventHandler(this.HideMenuItem_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			// 
			// mnuCopyContentMenu
			// 
			this.mnuCopyContentMenu.Name = "mnuCopyContentMenu";
			resources.ApplyResources(this.mnuCopyContentMenu, "mnuCopyContentMenu");
			this.mnuCopyContentMenu.Click += new System.EventHandler(this.CopyContentMenu_Click);
			// 
			// AnalyzerDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "AnalyzerDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnalyzerDialog_FormClosing);
			this.Load += new System.EventHandler(this.AnalyzerDialog_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.noteContextMenu.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem mnuViewRefresh;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsDataEmbedding;
		private System.Windows.Forms.ToolStripMenuItem mnuServiceToolStripMenuItem;
		private ControlLibrary.Controls.ToolStripControls.ToolStripRatingBox toolStripRatingBox;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ContextMenuStrip noteContextMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuEditContentMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem mnuHideContextMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuNewTextContentMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuNewDecimalContentMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuCopyContentMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
	}
}