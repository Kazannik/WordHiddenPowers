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
            this.categoriesTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.typeLabel = new System.Windows.Forms.Label();
            this.isTextCheckBox = new System.Windows.Forms.CheckBox();
            this.captionLabel = new System.Windows.Forms.Label();
            this.isDecimalCheckBox = new System.Windows.Forms.CheckBox();
            this.isObligatoryCheckBox = new System.Windows.Forms.CheckBox();
            this.captionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 400);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(815, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileToolStripMenuItem,
            this.mnuEdit,
            this.mnuCategories});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(815, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
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
            this.mnuFileToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.mnuFileToolStripMenuItem.Text = "Файл";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(208, 26);
            this.mnuFileNew.Text = "Создать структуру";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(208, 26);
            this.mnuFileOpen.Text = "Открыть файл...";
            this.mnuFileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(208, 26);
            this.mnuFileSave.Text = "Сохранить";
            this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(208, 26);
            this.mnuFileSaveAs.Text = "Сохранить как...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Image = global::WordHiddenPowers.Properties.Resources.WindowClose_24;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(208, 26);
            this.mnuFileExit.Text = "Закрыть";
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(76, 24);
            this.mnuEdit.Text = "mnuEdit";
            // 
            // mnuCategories
            // 
            this.mnuCategories.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCategoriesAddCategory});
            this.mnuCategories.Name = "mnuCategories";
            this.mnuCategories.Size = new System.Drawing.Size(94, 24);
            this.mnuCategories.Text = "Категории";
            // 
            // mnuCategoriesAddCategory
            // 
            this.mnuCategoriesAddCategory.Name = "mnuCategoriesAddCategory";
            this.mnuCategoriesAddCategory.Size = new System.Drawing.Size(229, 26);
            this.mnuCategoriesAddCategory.Text = "Добавить категорию";
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(815, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileNewButton
            // 
            this.fileNewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileNewButton.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
            this.fileNewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileNewButton.Name = "fileNewButton";
            this.fileNewButton.Size = new System.Drawing.Size(28, 28);
            this.fileNewButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // fileSaveButton
            // 
            this.fileSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileSaveButton.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
            this.fileSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileSaveButton.Name = "fileSaveButton";
            this.fileSaveButton.Size = new System.Drawing.Size(28, 28);
            this.fileSaveButton.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // addCategoryButton
            // 
            this.addCategoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addCategoryButton.Image = ((System.Drawing.Image)(resources.GetObject("addCategoryButton.Image")));
            this.addCategoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCategoryButton.Name = "addCategoryButton";
            this.addCategoryButton.Size = new System.Drawing.Size(28, 28);
            this.addCategoryButton.Text = "Добавить категорию";
            this.addCategoryButton.Click += new System.EventHandler(this.AddCategory_Click);
            // 
            // addSubcategoryButton
            // 
            this.addSubcategoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addSubcategoryButton.Image = ((System.Drawing.Image)(resources.GetObject("addSubcategoryButton.Image")));
            this.addSubcategoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSubcategoryButton.Name = "addSubcategoryButton";
            this.addSubcategoryButton.Size = new System.Drawing.Size(28, 28);
            this.addSubcategoryButton.Text = "Добавить подкатегорию";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 59);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.categoriesTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(815, 341);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // categoriesTreeView
            // 
            this.categoriesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoriesTreeView.Location = new System.Drawing.Point(0, 0);
            this.categoriesTreeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.categoriesTreeView.Name = "categoriesTreeView";
            this.categoriesTreeView.Size = new System.Drawing.Size(332, 341);
            this.categoriesTreeView.TabIndex = 0;
            this.categoriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CategoriesTreeView_AfterSelect);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.typeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.isTextCheckBox, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.captionLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.isDecimalCheckBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.isObligatoryCheckBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.captionTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(478, 341);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // typeLabel
            // 
            this.typeLabel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tableLayoutPanel1.SetColumnSpan(this.typeLabel, 3);
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typeLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.typeLabel.Location = new System.Drawing.Point(4, 0);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(470, 25);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "label1";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // isTextCheckBox
            // 
            this.isTextCheckBox.AutoSize = true;
            this.isTextCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isTextCheckBox.Location = new System.Drawing.Point(322, 315);
            this.isTextCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.isTextCheckBox.Name = "isTextCheckBox";
            this.isTextCheckBox.Size = new System.Drawing.Size(152, 22);
            this.isTextCheckBox.TabIndex = 5;
            this.isTextCheckBox.Text = "Для текста";
            this.isTextCheckBox.UseVisualStyleBackColor = true;
            this.isTextCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // captionLabel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.captionLabel, 3);
            this.captionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captionLabel.Location = new System.Drawing.Point(4, 25);
            this.captionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.captionLabel.Name = "captionLabel";
            this.captionLabel.Size = new System.Drawing.Size(470, 20);
            this.captionLabel.TabIndex = 0;
            this.captionLabel.Text = "Заголовок:";
            // 
            // isDecimalCheckBox
            // 
            this.isDecimalCheckBox.AutoSize = true;
            this.isDecimalCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isDecimalCheckBox.Location = new System.Drawing.Point(163, 315);
            this.isDecimalCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.isDecimalCheckBox.Name = "isDecimalCheckBox";
            this.isDecimalCheckBox.Size = new System.Drawing.Size(151, 22);
            this.isDecimalCheckBox.TabIndex = 4;
            this.isDecimalCheckBox.Text = "Для чисел";
            this.isDecimalCheckBox.UseVisualStyleBackColor = true;
            this.isDecimalCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // isObligatoryCheckBox
            // 
            this.isObligatoryCheckBox.AutoSize = true;
            this.isObligatoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isObligatoryCheckBox.Location = new System.Drawing.Point(3, 313);
            this.isObligatoryCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.isObligatoryCheckBox.Name = "isObligatoryCheckBox";
            this.isObligatoryCheckBox.Size = new System.Drawing.Size(153, 26);
            this.isObligatoryCheckBox.TabIndex = 6;
            this.isObligatoryCheckBox.Text = "Обязательный";
            this.isObligatoryCheckBox.UseVisualStyleBackColor = true;
            this.isObligatoryCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // captionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.captionTextBox, 3);
            this.captionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captionTextBox.Location = new System.Drawing.Point(4, 49);
            this.captionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.captionTextBox.Multiline = true;
            this.captionTextBox.Name = "captionTextBox";
            this.captionTextBox.Size = new System.Drawing.Size(470, 90);
            this.captionTextBox.TabIndex = 1;
            this.captionTextBox.TextChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // descriptionLabel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.descriptionLabel, 3);
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionLabel.Location = new System.Drawing.Point(4, 143);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(470, 20);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // descriptionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.descriptionTextBox, 3);
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(4, 167);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(470, 140);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // CategoriesEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 422);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoriesEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Редактор категорий";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CategoriesEditorDialog_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.TreeView categoriesTreeView;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox captionTextBox;
        private System.Windows.Forms.Label captionLabel;
        private System.Windows.Forms.CheckBox isTextCheckBox;
        private System.Windows.Forms.CheckBox isDecimalCheckBox;
        private System.Windows.Forms.CheckBox isObligatoryCheckBox;
        private System.Windows.Forms.ToolStripMenuItem mnuCategories;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoriesAddCategory;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label typeLabel;
    }
}