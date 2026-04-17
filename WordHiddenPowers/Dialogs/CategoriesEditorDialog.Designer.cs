namespace WordHiddenPowers.Dialogs
{
    partial class CategoriesEditorDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoriesEditorDialog));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategories = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategoriesAddCategory = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategoriesAddSubcategory = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategoriesRemoveCategory = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategoriesRemoveSubcategory = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCategoriesClear = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.fileNewButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.fileSaveButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.addCategoryButton = new System.Windows.Forms.ToolStripButton();
			this.addSubcategoryButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBox = new WordHiddenPowers.Controls.ListControls.CategoriesListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.obligatoryСheckBox = new System.Windows.Forms.CheckBox();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.captionTextBox = new System.Windows.Forms.TextBox();
			this.captionLabel = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.keywordsTextBox = new System.Windows.Forms.TextBox();
			this.keywordsLabel = new System.Windows.Forms.Label();
			this.isDecimalRadioButton = new System.Windows.Forms.RadioButton();
			this.isTextRadioButton = new System.Windows.Forms.RadioButton();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.afterTextBox = new System.Windows.Forms.TextBox();
			this.afterLabel = new System.Windows.Forms.Label();
			this.beforeTextBox = new System.Windows.Forms.TextBox();
			this.beforeLabel = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileToolStripMenuItem,
            this.mnuEdit,
            this.mnuCategories});
			resources.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
			// 
			// mnuFileToolStripMenuItem
			// 
			this.mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem2,
            this.mnuFileExit});
			this.mnuFileToolStripMenuItem.Name = "mnuFileToolStripMenuItem";
			resources.ApplyResources(this.mnuFileToolStripMenuItem, "mnuFileToolStripMenuItem");
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
			this.mnuFileNew.Name = "mnuFileNew";
			resources.ApplyResources(this.mnuFileNew, "mnuFileNew");
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
			this.mnuFileSave.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
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
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Image = global::WordHiddenPowers.Properties.Resources.WindowClose_24;
			this.mnuFileExit.Name = "mnuFileExit";
			resources.ApplyResources(this.mnuFileExit, "mnuFileExit");
			// 
			// mnuEdit
			// 
			this.mnuEdit.Name = "mnuEdit";
			resources.ApplyResources(this.mnuEdit, "mnuEdit");
			// 
			// mnuCategories
			// 
			this.mnuCategories.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCategoriesAddCategory,
            this.mnuCategoriesAddSubcategory,
            this.mnuCategoriesRemoveCategory,
            this.mnuCategoriesRemoveSubcategory,
            this.mnuCategoriesClear});
			this.mnuCategories.Name = "mnuCategories";
			resources.ApplyResources(this.mnuCategories, "mnuCategories");
			// 
			// mnuCategoriesAddCategory
			// 
			this.mnuCategoriesAddCategory.Name = "mnuCategoriesAddCategory";
			resources.ApplyResources(this.mnuCategoriesAddCategory, "mnuCategoriesAddCategory");
			this.mnuCategoriesAddCategory.Click += new System.EventHandler(this.AddCategory_Click);
			// 
			// mnuCategoriesAddSubcategory
			// 
			this.mnuCategoriesAddSubcategory.Name = "mnuCategoriesAddSubcategory";
			resources.ApplyResources(this.mnuCategoriesAddSubcategory, "mnuCategoriesAddSubcategory");
			this.mnuCategoriesAddSubcategory.Click += new System.EventHandler(this.AddSubcategory_Click);
			// 
			// mnuCategoriesRemoveCategory
			// 
			this.mnuCategoriesRemoveCategory.Name = "mnuCategoriesRemoveCategory";
			resources.ApplyResources(this.mnuCategoriesRemoveCategory, "mnuCategoriesRemoveCategory");
			this.mnuCategoriesRemoveCategory.Click += new System.EventHandler(this.RemoveCategory_Click);
			// 
			// mnuCategoriesRemoveSubcategory
			// 
			this.mnuCategoriesRemoveSubcategory.Name = "mnuCategoriesRemoveSubcategory";
			resources.ApplyResources(this.mnuCategoriesRemoveSubcategory, "mnuCategoriesRemoveSubcategory");
			this.mnuCategoriesRemoveSubcategory.Click += new System.EventHandler(this.RemoveSubcategory_Click);
			// 
			// mnuCategoriesClear
			// 
			this.mnuCategoriesClear.Name = "mnuCategoriesClear";
			resources.ApplyResources(this.mnuCategoriesClear, "mnuCategoriesClear");
			this.mnuCategoriesClear.Click += new System.EventHandler(this.Clear_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewButton,
            this.toolStripSeparator1,
            this.fileSaveButton,
            this.toolStripSeparator2,
            this.addCategoryButton,
            this.addSubcategoryButton,
            this.toolStripSeparator3,
            this.toolStripButton5});
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Name = "toolStrip1";
			// 
			// fileNewButton
			// 
			this.fileNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fileNewButton.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
			resources.ApplyResources(this.fileNewButton, "fileNewButton");
			this.fileNewButton.Name = "fileNewButton";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// fileSaveButton
			// 
			this.fileSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fileSaveButton.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
			resources.ApplyResources(this.fileSaveButton, "fileSaveButton");
			this.fileSaveButton.Name = "fileSaveButton";
			this.fileSaveButton.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// addCategoryButton
			// 
			this.addCategoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.addCategoryButton, "addCategoryButton");
			this.addCategoryButton.Name = "addCategoryButton";
			this.addCategoryButton.Click += new System.EventHandler(this.AddCategory_Click);
			// 
			// addSubcategoryButton
			// 
			this.addSubcategoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.addSubcategoryButton, "addSubcategoryButton");
			this.addSubcategoryButton.Name = "addSubcategoryButton";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toolStripButton5, "toolStripButton5");
			this.toolStripButton5.Name = "toolStripButton5";
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel2.Resize += new System.EventHandler(this.Containers_Resize);
			// 
			// listBox
			// 
			this.listBox.DataSet = null;
			resources.ApplyResources(this.listBox, "listBox");
			this.listBox.FormattingEnabled = true;
			this.listBox.Name = "listBox";
			// 
			// tabControl1
			// 
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.obligatoryСheckBox);
			this.tabPage1.Controls.Add(this.descriptionTextBox);
			this.tabPage1.Controls.Add(this.descriptionLabel);
			this.tabPage1.Controls.Add(this.captionTextBox);
			this.tabPage1.Controls.Add(this.captionLabel);
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// obligatoryСheckBox
			// 
			resources.ApplyResources(this.obligatoryСheckBox, "obligatoryСheckBox");
			this.obligatoryСheckBox.Name = "obligatoryСheckBox";
			this.obligatoryСheckBox.UseVisualStyleBackColor = true;
			// 
			// descriptionTextBox
			// 
			resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
			this.descriptionTextBox.Name = "descriptionTextBox";
			// 
			// descriptionLabel
			// 
			resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
			this.descriptionLabel.Name = "descriptionLabel";
			// 
			// captionTextBox
			// 
			resources.ApplyResources(this.captionTextBox, "captionTextBox");
			this.captionTextBox.Name = "captionTextBox";
			// 
			// captionLabel
			// 
			resources.ApplyResources(this.captionLabel, "captionLabel");
			this.captionLabel.Name = "captionLabel";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.keywordsTextBox);
			this.tabPage2.Controls.Add(this.keywordsLabel);
			this.tabPage2.Controls.Add(this.isDecimalRadioButton);
			this.tabPage2.Controls.Add(this.isTextRadioButton);
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// keywordsTextBox
			// 
			resources.ApplyResources(this.keywordsTextBox, "keywordsTextBox");
			this.keywordsTextBox.Name = "keywordsTextBox";
			// 
			// keywordsLabel
			// 
			resources.ApplyResources(this.keywordsLabel, "keywordsLabel");
			this.keywordsLabel.Name = "keywordsLabel";
			// 
			// isDecimalRadioButton
			// 
			resources.ApplyResources(this.isDecimalRadioButton, "isDecimalRadioButton");
			this.isDecimalRadioButton.Name = "isDecimalRadioButton";
			this.isDecimalRadioButton.TabStop = true;
			this.isDecimalRadioButton.UseVisualStyleBackColor = true;
			// 
			// isTextRadioButton
			// 
			resources.ApplyResources(this.isTextRadioButton, "isTextRadioButton");
			this.isTextRadioButton.Name = "isTextRadioButton";
			this.isTextRadioButton.TabStop = true;
			this.isTextRadioButton.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.afterTextBox);
			this.tabPage3.Controls.Add(this.afterLabel);
			this.tabPage3.Controls.Add(this.beforeTextBox);
			this.tabPage3.Controls.Add(this.beforeLabel);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// afterTextBox
			// 
			resources.ApplyResources(this.afterTextBox, "afterTextBox");
			this.afterTextBox.Name = "afterTextBox";
			// 
			// afterLabel
			// 
			resources.ApplyResources(this.afterLabel, "afterLabel");
			this.afterLabel.Name = "afterLabel";
			// 
			// beforeTextBox
			// 
			resources.ApplyResources(this.beforeTextBox, "beforeTextBox");
			this.beforeTextBox.Name = "beforeTextBox";
			// 
			// beforeLabel
			// 
			resources.ApplyResources(this.beforeLabel, "beforeLabel");
			this.beforeLabel.Name = "beforeLabel";
			// 
			// CategoriesEditorDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "CategoriesEditorDialog";
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CategoriesEditorDialog_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }
		
		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton fileNewButton;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripButton fileSaveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton addCategoryButton;
        private System.Windows.Forms.ToolStripButton addSubcategoryButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem mnuCategories;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoriesAddCategory;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private WordHiddenPowers.Controls.ListControls.CategoriesListBox listBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox captionTextBox;
		private System.Windows.Forms.Label captionLabel;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox descriptionTextBox;
		private System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.CheckBox obligatoryСheckBox;
		private System.Windows.Forms.RadioButton isDecimalRadioButton;
		private System.Windows.Forms.RadioButton isTextRadioButton;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox afterTextBox;
		private System.Windows.Forms.Label afterLabel;
		private System.Windows.Forms.TextBox beforeTextBox;
		private System.Windows.Forms.Label beforeLabel;
		private System.Windows.Forms.TextBox keywordsTextBox;
		private System.Windows.Forms.Label keywordsLabel;
		private System.Windows.Forms.ToolStripMenuItem mnuCategoriesAddSubcategory;
		private System.Windows.Forms.ToolStripMenuItem mnuCategoriesRemoveCategory;
		private System.Windows.Forms.ToolStripMenuItem mnuCategoriesRemoveSubcategory;
		private System.Windows.Forms.ToolStripMenuItem mnuCategoriesClear;
	}
}