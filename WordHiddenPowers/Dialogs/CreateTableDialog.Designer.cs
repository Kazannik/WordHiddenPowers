namespace WordHiddenPowers.Dialogs
{
    partial class CreateTableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTableDialog));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableAddColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableInsertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableInsertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTableDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonNewTable = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTable});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(449, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileSave,
            this.toolStripMenuItem1,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(57, 24);
            this.mnuFile.Text = "Файл";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(199, 26);
            this.mnuFileNew.Text = "Создать таблицу";
            this.mnuFileNew.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(199, 26);
            this.mnuFileSave.Text = "Сохранить";
            this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(199, 26);
            this.mnuFileExit.Text = "Закрыть";
            // 
            // mnuTable
            // 
            this.mnuTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableAdd,
            this.mnuTableInsert,
            this.toolStripMenuItem2,
            this.mnuTableDelete});
            this.mnuTable.Name = "mnuTable";
            this.mnuTable.Size = new System.Drawing.Size(80, 24);
            this.mnuTable.Text = "Таблица";
            // 
            // mnuTableAdd
            // 
            this.mnuTableAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableAddRow,
            this.mnuTableAddColumn});
            this.mnuTableAdd.Name = "mnuTableAdd";
            this.mnuTableAdd.Size = new System.Drawing.Size(151, 26);
            this.mnuTableAdd.Text = "Добавить";
            // 
            // mnuTableAddRow
            // 
            this.mnuTableAddRow.Name = "mnuTableAddRow";
            this.mnuTableAddRow.Size = new System.Drawing.Size(131, 26);
            this.mnuTableAddRow.Text = "Строку";
            // 
            // mnuTableAddColumn
            // 
            this.mnuTableAddColumn.Name = "mnuTableAddColumn";
            this.mnuTableAddColumn.Size = new System.Drawing.Size(131, 26);
            this.mnuTableAddColumn.Text = "Графу";
            // 
            // mnuTableInsert
            // 
            this.mnuTableInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableInsertRowToolStripMenuItem,
            this.mnuTableInsertColumnToolStripMenuItem});
            this.mnuTableInsert.Name = "mnuTableInsert";
            this.mnuTableInsert.Size = new System.Drawing.Size(151, 26);
            this.mnuTableInsert.Text = "Вставить";
            // 
            // mnuTableInsertRowToolStripMenuItem
            // 
            this.mnuTableInsertRowToolStripMenuItem.Name = "mnuTableInsertRowToolStripMenuItem";
            this.mnuTableInsertRowToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.mnuTableInsertRowToolStripMenuItem.Text = "mnuTableInsertRow";
            // 
            // mnuTableInsertColumnToolStripMenuItem
            // 
            this.mnuTableInsertColumnToolStripMenuItem.Name = "mnuTableInsertColumnToolStripMenuItem";
            this.mnuTableInsertColumnToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.mnuTableInsertColumnToolStripMenuItem.Text = "mnuTableInsertColumn";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 6);
            // 
            // mnuTableDelete
            // 
            this.mnuTableDelete.Name = "mnuTableDelete";
            this.mnuTableDelete.Size = new System.Drawing.Size(151, 26);
            this.mnuTableDelete.Text = "Удалить";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewTable,
            this.buttonSave,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(449, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonNewTable
            // 
            this.buttonNewTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNewTable.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewTable.Image")));
            this.buttonNewTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNewTable.Name = "buttonNewTable";
            this.buttonNewTable.Size = new System.Drawing.Size(24, 24);
            this.buttonNewTable.Text = "Создать таблицу";
            this.buttonNewTable.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(24, 24);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(449, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 55);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(449, 360);
            this.dataGridView1.TabIndex = 3;
            // 
            // CreateTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 437);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CreateTableDialog";
            this.Text = "Создать таблицу";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuTable;
        private System.Windows.Forms.ToolStripButton buttonNewTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuTableAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuTableAddRow;
        private System.Windows.Forms.ToolStripMenuItem mnuTableAddColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuTableInsert;
        private System.Windows.Forms.ToolStripMenuItem mnuTableInsertRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTableInsertColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuTableDelete;
    }
}