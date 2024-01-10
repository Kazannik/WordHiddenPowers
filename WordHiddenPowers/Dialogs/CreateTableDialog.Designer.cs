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
            this.components = new System.ComponentModel.Container();
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
            this.mnuTableInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableInsertColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTableDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTableDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonNewTable = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuTableInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTableInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTableInserColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuTableDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTable});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(337, 24);
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
            this.mnuFile.Size = new System.Drawing.Size(48, 20);
            this.mnuFile.Text = "Файл";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::WordHiddenPowers.Properties.Resources.Table_AddHS;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(165, 22);
            this.mnuFileNew.Text = "Создать таблицу";
            this.mnuFileNew.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::WordHiddenPowers.Properties.Resources.saveHS;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(165, 22);
            this.mnuFileSave.Text = "Сохранить";
            this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Image = global::WordHiddenPowers.Properties.Resources.ExitHS;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(165, 22);
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
            this.mnuTable.Size = new System.Drawing.Size(66, 20);
            this.mnuTable.Text = "Таблица";
            // 
            // mnuTableAdd
            // 
            this.mnuTableAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableAddRow,
            this.mnuTableAddColumn});
            this.mnuTableAdd.Name = "mnuTableAdd";
            this.mnuTableAdd.Size = new System.Drawing.Size(126, 22);
            this.mnuTableAdd.Text = "Добавить";
            // 
            // mnuTableAddRow
            // 
            this.mnuTableAddRow.Image = global::WordHiddenPowers.Properties.Resources.Row_AddHS1;
            this.mnuTableAddRow.Name = "mnuTableAddRow";
            this.mnuTableAddRow.Size = new System.Drawing.Size(113, 22);
            this.mnuTableAddRow.Text = "Строку";
            // 
            // mnuTableAddColumn
            // 
            this.mnuTableAddColumn.Image = global::WordHiddenPowers.Properties.Resources.Column_AddHS1;
            this.mnuTableAddColumn.Name = "mnuTableAddColumn";
            this.mnuTableAddColumn.Size = new System.Drawing.Size(113, 22);
            this.mnuTableAddColumn.Text = "Графу";
            // 
            // mnuTableInsert
            // 
            this.mnuTableInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableInsertRow,
            this.mnuTableInsertColumn});
            this.mnuTableInsert.Name = "mnuTableInsert";
            this.mnuTableInsert.Size = new System.Drawing.Size(126, 22);
            this.mnuTableInsert.Text = "Вставить";
            // 
            // mnuTableInsertRow
            // 
            this.mnuTableInsertRow.Image = global::WordHiddenPowers.Properties.Resources.Row_AddHS1;
            this.mnuTableInsertRow.Name = "mnuTableInsertRow";
            this.mnuTableInsertRow.Size = new System.Drawing.Size(113, 22);
            this.mnuTableInsertRow.Text = "Строку";
            // 
            // mnuTableInsertColumn
            // 
            this.mnuTableInsertColumn.Image = global::WordHiddenPowers.Properties.Resources.Column_AddHS1;
            this.mnuTableInsertColumn.Name = "mnuTableInsertColumn";
            this.mnuTableInsertColumn.Size = new System.Drawing.Size(113, 22);
            this.mnuTableInsertColumn.Text = "Графу";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(123, 6);
            // 
            // mnuTableDelete
            // 
            this.mnuTableDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableDeleteRow,
            this.mnuTableDeleteColumn});
            this.mnuTableDelete.Image = global::WordHiddenPowers.Properties.Resources.DeleteRedHS;
            this.mnuTableDelete.Name = "mnuTableDelete";
            this.mnuTableDelete.Size = new System.Drawing.Size(126, 22);
            this.mnuTableDelete.Text = "Удалить";
            this.mnuTableDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // mnuTableDeleteRow
            // 
            this.mnuTableDeleteRow.Name = "mnuTableDeleteRow";
            this.mnuTableDeleteRow.Size = new System.Drawing.Size(113, 22);
            this.mnuTableDeleteRow.Text = "Строку";
            // 
            // mnuTableDeleteColumn
            // 
            this.mnuTableDeleteColumn.Name = "mnuTableDeleteColumn";
            this.mnuTableDeleteColumn.Size = new System.Drawing.Size(113, 22);
            this.mnuTableDeleteColumn.Text = "Графу";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewTable,
            this.buttonSave,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(337, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonNewTable
            // 
            this.buttonNewTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNewTable.Image = global::WordHiddenPowers.Properties.Resources.Table_AddHS;
            this.buttonNewTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNewTable.Name = "buttonNewTable";
            this.buttonNewTable.Size = new System.Drawing.Size(23, 22);
            this.buttonNewTable.Text = "Создать таблицу";
            this.buttonNewTable.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = global::WordHiddenPowers.Properties.Resources.saveHS;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(23, 22);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 333);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(337, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(0, 49);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(337, 284);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTableInsert,
            this.toolStripMenuItem3,
            this.cmnuTableDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 54);
            // 
            // cmnuTableInsert
            // 
            this.cmnuTableInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTableInsertRow,
            this.cmnuTableInserColumn});
            this.cmnuTableInsert.Name = "cmnuTableInsert";
            this.cmnuTableInsert.Size = new System.Drawing.Size(167, 22);
            this.cmnuTableInsert.Text = "cmnuTableInsert";
            // 
            // cmnuTableInsertRow
            // 
            this.cmnuTableInsertRow.Image = global::WordHiddenPowers.Properties.Resources.Row_AddHS1;
            this.cmnuTableInsertRow.Name = "cmnuTableInsertRow";
            this.cmnuTableInsertRow.Size = new System.Drawing.Size(202, 22);
            this.cmnuTableInsertRow.Text = "cmnuTableInsertRow";
            // 
            // cmnuTableInserColumn
            // 
            this.cmnuTableInserColumn.Image = global::WordHiddenPowers.Properties.Resources.Column_AddHS1;
            this.cmnuTableInserColumn.Name = "cmnuTableInserColumn";
            this.cmnuTableInserColumn.Size = new System.Drawing.Size(202, 22);
            this.cmnuTableInserColumn.Text = "cmnuTableInserColumn";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 6);
            // 
            // cmnuTableDelete
            // 
            this.cmnuTableDelete.Image = global::WordHiddenPowers.Properties.Resources.DeleteRedHS;
            this.cmnuTableDelete.Name = "cmnuTableDelete";
            this.cmnuTableDelete.Size = new System.Drawing.Size(167, 22);
            this.cmnuTableDelete.Text = "cmnuTableDelete";
            this.cmnuTableDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // CreateTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 355);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CreateTableDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Макет таблицы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dataGridView;
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
        private System.Windows.Forms.ToolStripMenuItem mnuTableInsertRow;
        private System.Windows.Forms.ToolStripMenuItem mnuTableInsertColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuTableDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuTableInsert;
        private System.Windows.Forms.ToolStripMenuItem cmnuTableInsertRow;
        private System.Windows.Forms.ToolStripMenuItem cmnuTableInserColumn;
        private System.Windows.Forms.ToolStripMenuItem cmnuTableDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuTableDeleteRow;
        private System.Windows.Forms.ToolStripMenuItem mnuTableDeleteColumn;
    }
}