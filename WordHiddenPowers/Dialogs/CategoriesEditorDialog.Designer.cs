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
            this.mnuFileNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategoriesAddCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addCategoryButton = new System.Windows.Forms.ToolStripButton();
            this.addSubcategoryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.categoriesTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.captionTextBox = new System.Windows.Forms.TextBox();
            this.captionLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.isTextCheckBox = new System.Windows.Forms.CheckBox();
            this.isDecimalCheckBox = new System.Windows.Forms.CheckBox();
            this.isObligatoryCheckBox = new System.Windows.Forms.CheckBox();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(524, 22);
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
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFileToolStripMenuItem
            // 
            this.mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNewToolStripMenuItem,
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileSaveToolStripMenuItem,
            this.mnuFileSaveAs,
            this.toolStripMenuItem2,
            this.mnuFileExitToolStripMenuItem});
            this.mnuFileToolStripMenuItem.Name = "mnuFileToolStripMenuItem";
            this.mnuFileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mnuFileToolStripMenuItem.Text = "Файл";
            // 
            // mnuFileNewToolStripMenuItem
            // 
            this.mnuFileNewToolStripMenuItem.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
            this.mnuFileNewToolStripMenuItem.Name = "mnuFileNewToolStripMenuItem";
            this.mnuFileNewToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mnuFileNewToolStripMenuItem.Text = "Создать структуру";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(174, 22);
            this.mnuFileOpen.Text = "Открыть файл...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // mnuFileSaveToolStripMenuItem
            // 
            this.mnuFileSaveToolStripMenuItem.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
            this.mnuFileSaveToolStripMenuItem.Name = "mnuFileSaveToolStripMenuItem";
            this.mnuFileSaveToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mnuFileSaveToolStripMenuItem.Text = "Сохранить";
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(174, 22);
            this.mnuFileSaveAs.Text = "Сохранить как...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
            // 
            // mnuFileExitToolStripMenuItem
            // 
            this.mnuFileExitToolStripMenuItem.Image = global::WordHiddenPowers.Properties.Resources.WindowClose_24;
            this.mnuFileExitToolStripMenuItem.Name = "mnuFileExitToolStripMenuItem";
            this.mnuFileExitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mnuFileExitToolStripMenuItem.Text = "Закрыть";
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(64, 20);
            this.mnuEdit.Text = "mnuEdit";
            // 
            // mnuCategories
            // 
            this.mnuCategories.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCategoriesAddCategory});
            this.mnuCategories.Name = "mnuCategories";
            this.mnuCategories.Size = new System.Drawing.Size(76, 20);
            this.mnuCategories.Text = "Категории";
            // 
            // mnuCategoriesAddCategory
            // 
            this.mnuCategoriesAddCategory.Name = "mnuCategoriesAddCategory";
            this.mnuCategoriesAddCategory.Size = new System.Drawing.Size(188, 22);
            this.mnuCategoriesAddCategory.Text = "Добавить категорию";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.addCategoryButton,
            this.addSubcategoryButton,
            this.toolStripSeparator3,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(524, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::WordHiddenPowers.Properties.Resources.GroupContTypeNew_24;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
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
            this.addCategoryButton.Click += new System.EventHandler(this.addCategoryButton_Click);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.categoriesTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(524, 266);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 3;
            // 
            // categoriesTreeView
            // 
            this.categoriesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoriesTreeView.Location = new System.Drawing.Point(0, 0);
            this.categoriesTreeView.Name = "categoriesTreeView";
            this.categoriesTreeView.Size = new System.Drawing.Size(214, 266);
            this.categoriesTreeView.TabIndex = 0;
            this.categoriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.categoriesTreeView_AfterSelect);
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 266);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // descriptionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.descriptionTextBox, 3);
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 135);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(300, 104);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // descriptionLabel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.descriptionLabel, 3);
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 116);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(300, 16);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // captionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.captionTextBox, 3);
            this.captionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captionTextBox.Location = new System.Drawing.Point(3, 39);
            this.captionTextBox.Multiline = true;
            this.captionTextBox.Name = "captionTextBox";
            this.captionTextBox.Size = new System.Drawing.Size(300, 74);
            this.captionTextBox.TabIndex = 1;
            this.captionTextBox.TextChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // captionLabel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.captionLabel, 3);
            this.captionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captionLabel.Location = new System.Drawing.Point(3, 20);
            this.captionLabel.Name = "captionLabel";
            this.captionLabel.Size = new System.Drawing.Size(300, 16);
            this.captionLabel.TabIndex = 0;
            this.captionLabel.Text = "Заголовок:";
            // 
            // typeLabel
            // 
            this.typeLabel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tableLayoutPanel1.SetColumnSpan(this.typeLabel, 3);
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typeLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.typeLabel.Location = new System.Drawing.Point(3, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(300, 20);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "label1";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // isTextCheckBox
            // 
            this.isTextCheckBox.AutoSize = true;
            this.isTextCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isTextCheckBox.Location = new System.Drawing.Point(206, 245);
            this.isTextCheckBox.Name = "isTextCheckBox";
            this.isTextCheckBox.Size = new System.Drawing.Size(97, 18);
            this.isTextCheckBox.TabIndex = 5;
            this.isTextCheckBox.Text = "Для текста";
            this.isTextCheckBox.UseVisualStyleBackColor = true;
            this.isTextCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // isDecimalCheckBox
            // 
            this.isDecimalCheckBox.AutoSize = true;
            this.isDecimalCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isDecimalCheckBox.Location = new System.Drawing.Point(104, 245);
            this.isDecimalCheckBox.Name = "isDecimalCheckBox";
            this.isDecimalCheckBox.Size = new System.Drawing.Size(96, 18);
            this.isDecimalCheckBox.TabIndex = 4;
            this.isDecimalCheckBox.Text = "Для чисел";
            this.isDecimalCheckBox.UseVisualStyleBackColor = true;
            this.isDecimalCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // isObligatoryCheckBox
            // 
            this.isObligatoryCheckBox.AutoSize = true;
            this.isObligatoryCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isObligatoryCheckBox.Location = new System.Drawing.Point(2, 244);
            this.isObligatoryCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.isObligatoryCheckBox.Name = "isObligatoryCheckBox";
            this.isObligatoryCheckBox.Size = new System.Drawing.Size(97, 20);
            this.isObligatoryCheckBox.TabIndex = 6;
            this.isObligatoryCheckBox.Text = "Обязательный";
            this.isObligatoryCheckBox.UseVisualStyleBackColor = true;
            this.isObligatoryCheckBox.CheckedChanged += new System.EventHandler(this.Controls_ValueChanged);
            // 
            // CategoriesEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 343);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoriesEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Редактор категорий";
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
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
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