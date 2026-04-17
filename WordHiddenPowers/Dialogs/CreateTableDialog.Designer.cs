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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTableDialog));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
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
			this.mnuTableDeleteColumns = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTableDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonNewTable = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonBold = new System.Windows.Forms.ToolStripButton();
			this.backColorButton = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
			this.fontColorButton = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuInserRowAbove = new System.Windows.Forms.ToolStripButton();
			this.mnuInsertColumnRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDeleteTable = new System.Windows.Forms.ToolStripSplitButton();
			this.mnuDeleteColumns = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
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
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTable});
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
            this.mnuFileSaveAsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.mnuFileExit});
			this.mnuFile.Name = "mnuFile";
			resources.ApplyResources(this.mnuFile, "mnuFile");
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Image = global::WordHiddenPowers.Properties.Resources.CreateTable_24;
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
			this.mnuFileSave.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
			this.mnuFileSave.Name = "mnuFileSave";
			resources.ApplyResources(this.mnuFileSave, "mnuFileSave");
			this.mnuFileSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// mnuFileSaveAsToolStripMenuItem
			// 
			this.mnuFileSaveAsToolStripMenuItem.Name = "mnuFileSaveAsToolStripMenuItem";
			resources.ApplyResources(this.mnuFileSaveAsToolStripMenuItem, "mnuFileSaveAsToolStripMenuItem");
			this.mnuFileSaveAsToolStripMenuItem.Click += new System.EventHandler(this.FileSaveAs_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Image = global::WordHiddenPowers.Properties.Resources.WindowClose_24;
			this.mnuFileExit.Name = "mnuFileExit";
			resources.ApplyResources(this.mnuFileExit, "mnuFileExit");
			// 
			// mnuTable
			// 
			this.mnuTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableAdd,
            this.mnuTableInsert,
            this.toolStripMenuItem2,
            this.mnuTableDelete});
			this.mnuTable.Name = "mnuTable";
			resources.ApplyResources(this.mnuTable, "mnuTable");
			// 
			// mnuTableAdd
			// 
			this.mnuTableAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableAddRow,
            this.mnuTableAddColumn});
			this.mnuTableAdd.Name = "mnuTableAdd";
			resources.ApplyResources(this.mnuTableAdd, "mnuTableAdd");
			// 
			// mnuTableAddRow
			// 
			this.mnuTableAddRow.Image = global::WordHiddenPowers.Properties.Resources.TableInsertRowsAbove_24;
			this.mnuTableAddRow.Name = "mnuTableAddRow";
			resources.ApplyResources(this.mnuTableAddRow, "mnuTableAddRow");
			this.mnuTableAddRow.Click += new System.EventHandler(this.InserRowAbove_Click);
			// 
			// mnuTableAddColumn
			// 
			this.mnuTableAddColumn.Image = global::WordHiddenPowers.Properties.Resources.TableInsertColumnsLeft_24;
			this.mnuTableAddColumn.Name = "mnuTableAddColumn";
			resources.ApplyResources(this.mnuTableAddColumn, "mnuTableAddColumn");
			this.mnuTableAddColumn.Click += new System.EventHandler(this.InsertColumnRight_Click);
			// 
			// mnuTableInsert
			// 
			this.mnuTableInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableInsertRow,
            this.mnuTableInsertColumn});
			this.mnuTableInsert.Name = "mnuTableInsert";
			resources.ApplyResources(this.mnuTableInsert, "mnuTableInsert");
			// 
			// mnuTableInsertRow
			// 
			this.mnuTableInsertRow.Image = global::WordHiddenPowers.Properties.Resources.TableInsertRowsAbove_24;
			this.mnuTableInsertRow.Name = "mnuTableInsertRow";
			resources.ApplyResources(this.mnuTableInsertRow, "mnuTableInsertRow");
			// 
			// mnuTableInsertColumn
			// 
			this.mnuTableInsertColumn.Image = global::WordHiddenPowers.Properties.Resources.TableInsertColumnsLeft_24;
			this.mnuTableInsertColumn.Name = "mnuTableInsertColumn";
			resources.ApplyResources(this.mnuTableInsertColumn, "mnuTableInsertColumn");
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			// 
			// mnuTableDelete
			// 
			this.mnuTableDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTableDeleteColumns,
            this.mnuTableDeleteRows});
			this.mnuTableDelete.Image = global::WordHiddenPowers.Properties.Resources.TableDelete_24;
			this.mnuTableDelete.Name = "mnuTableDelete";
			resources.ApplyResources(this.mnuTableDelete, "mnuTableDelete");
			this.mnuTableDelete.Click += new System.EventHandler(this.Delete_Click);
			// 
			// mnuTableDeleteColumns
			// 
			this.mnuTableDeleteColumns.Image = global::WordHiddenPowers.Properties.Resources.TableDeleteColumns_24;
			this.mnuTableDeleteColumns.Name = "mnuTableDeleteColumns";
			resources.ApplyResources(this.mnuTableDeleteColumns, "mnuTableDeleteColumns");
			this.mnuTableDeleteColumns.Click += new System.EventHandler(this.TableDeleteColumns_Click);
			// 
			// mnuTableDeleteRows
			// 
			this.mnuTableDeleteRows.Image = global::WordHiddenPowers.Properties.Resources.TableDeleteRows_24;
			this.mnuTableDeleteRows.Name = "mnuTableDeleteRows";
			resources.ApplyResources(this.mnuTableDeleteRows, "mnuTableDeleteRows");
			this.mnuTableDeleteRows.Click += new System.EventHandler(this.TableDeleteRows_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewTable,
            this.toolStripSeparator1,
            this.buttonSave,
            this.toolStripSeparator2,
            this.buttonBold,
            this.backColorButton,
            this.fontColorButton,
            this.toolStripSeparator3,
            this.mnuInserRowAbove,
            this.mnuInsertColumnRight,
            this.toolStripSeparator4,
            this.mnuDeleteTable});
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Name = "toolStrip1";
			// 
			// buttonNewTable
			// 
			this.buttonNewTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonNewTable.Image = global::WordHiddenPowers.Properties.Resources.CreateTable_24;
			resources.ApplyResources(this.buttonNewTable, "buttonNewTable");
			this.buttonNewTable.Name = "buttonNewTable";
			this.buttonNewTable.Click += new System.EventHandler(this.FileNew_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// buttonSave
			// 
			this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonSave.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
			resources.ApplyResources(this.buttonSave, "buttonSave");
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Click += new System.EventHandler(this.FileSave_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// buttonBold
			// 
			this.buttonBold.CheckOnClick = true;
			this.buttonBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonBold.Image = global::WordHiddenPowers.Properties.Resources.Bold_24;
			resources.ApplyResources(this.buttonBold, "buttonBold");
			this.buttonBold.Name = "buttonBold";
			this.buttonBold.Click += new System.EventHandler(this.Bold_Click);
			// 
			// backColorButton
			// 
			this.backColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.backColorButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem15});
			this.backColorButton.Image = global::WordHiddenPowers.Properties.Resources.BackColor_24;
			resources.ApplyResources(this.backColorButton, "backColorButton");
			this.backColorButton.Name = "backColorButton";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem5.Image = global::WordHiddenPowers.Properties.Resources.ColorAqua_24;
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem6.Image = global::WordHiddenPowers.Properties.Resources.ColorBlack_24;
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem7.Image = global::WordHiddenPowers.Properties.Resources.ColorBlue_24;
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem8.Image = global::WordHiddenPowers.Properties.Resources.ColorFuchsia_24;
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem9.Image = global::WordHiddenPowers.Properties.Resources.ColorGray_24;
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem10.Image = global::WordHiddenPowers.Properties.Resources.ColorGreen_24;
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem11.Image = global::WordHiddenPowers.Properties.Resources.ColorLime_24;
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem12.Image = global::WordHiddenPowers.Properties.Resources.ColorMaroon_24;
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			resources.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem13.Image = global::WordHiddenPowers.Properties.Resources.ColorNavy_24;
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
			// 
			// toolStripMenuItem14
			// 
			this.toolStripMenuItem14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem14.Image = global::WordHiddenPowers.Properties.Resources.ColorOlive_24;
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			resources.ApplyResources(this.toolStripMenuItem14, "toolStripMenuItem14");
			// 
			// toolStripMenuItem15
			// 
			this.toolStripMenuItem15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem15.Image = global::WordHiddenPowers.Properties.Resources.ColorPalette_24;
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			resources.ApplyResources(this.toolStripMenuItem15, "toolStripMenuItem15");
			// 
			// fontColorButton
			// 
			this.fontColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fontColorButton.Image = global::WordHiddenPowers.Properties.Resources.ColorMenu_24;
			resources.ApplyResources(this.fontColorButton, "fontColorButton");
			this.fontColorButton.Name = "fontColorButton";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// mnuInserRowAbove
			// 
			this.mnuInserRowAbove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mnuInserRowAbove.Image = global::WordHiddenPowers.Properties.Resources.TableInsertRowsAbove_24;
			resources.ApplyResources(this.mnuInserRowAbove, "mnuInserRowAbove");
			this.mnuInserRowAbove.Name = "mnuInserRowAbove";
			this.mnuInserRowAbove.Click += new System.EventHandler(this.InserRowAbove_Click);
			// 
			// mnuInsertColumnRight
			// 
			this.mnuInsertColumnRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mnuInsertColumnRight.Image = global::WordHiddenPowers.Properties.Resources.TableInsertColumnsRight_24;
			resources.ApplyResources(this.mnuInsertColumnRight, "mnuInsertColumnRight");
			this.mnuInsertColumnRight.Name = "mnuInsertColumnRight";
			this.mnuInsertColumnRight.Click += new System.EventHandler(this.InsertColumnRight_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// mnuDeleteTable
			// 
			this.mnuDeleteTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mnuDeleteTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteColumns,
            this.mnuDeleteRows});
			this.mnuDeleteTable.Image = global::WordHiddenPowers.Properties.Resources.TableDelete_24;
			resources.ApplyResources(this.mnuDeleteTable, "mnuDeleteTable");
			this.mnuDeleteTable.Name = "mnuDeleteTable";
			this.mnuDeleteTable.ButtonClick += new System.EventHandler(this.DeleteTable_ButtonClick);
			// 
			// mnuDeleteColumns
			// 
			this.mnuDeleteColumns.Image = global::WordHiddenPowers.Properties.Resources.TableDeleteColumns_24;
			this.mnuDeleteColumns.Name = "mnuDeleteColumns";
			resources.ApplyResources(this.mnuDeleteColumns, "mnuDeleteColumns");
			this.mnuDeleteColumns.Click += new System.EventHandler(this.TableDeleteColumns_Click);
			// 
			// mnuDeleteRows
			// 
			this.mnuDeleteRows.Image = global::WordHiddenPowers.Properties.Resources.TableDeleteRows_24;
			this.mnuDeleteRows.Name = "mnuDeleteRows";
			resources.ApplyResources(this.mnuDeleteRows, "mnuDeleteRows");
			this.mnuDeleteRows.Click += new System.EventHandler(this.TableDeleteRows_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.ColumnHeadersVisible = false;
			resources.ApplyResources(this.dataGridView, "dataGridView");
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowTemplate.Height = 24;
			this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTableInsert,
            this.toolStripMenuItem3,
            this.cmnuTableDelete});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			// 
			// cmnuTableInsert
			// 
			this.cmnuTableInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTableInsertRow,
            this.cmnuTableInserColumn});
			this.cmnuTableInsert.Name = "cmnuTableInsert";
			resources.ApplyResources(this.cmnuTableInsert, "cmnuTableInsert");
			// 
			// cmnuTableInsertRow
			// 
			this.cmnuTableInsertRow.Image = global::WordHiddenPowers.Properties.Resources.TableInsertRowsAbove_16;
			this.cmnuTableInsertRow.Name = "cmnuTableInsertRow";
			resources.ApplyResources(this.cmnuTableInsertRow, "cmnuTableInsertRow");
			// 
			// cmnuTableInserColumn
			// 
			this.cmnuTableInserColumn.Image = global::WordHiddenPowers.Properties.Resources.TableInsertColumnsLeft_16;
			this.cmnuTableInserColumn.Name = "cmnuTableInserColumn";
			resources.ApplyResources(this.cmnuTableInserColumn, "cmnuTableInserColumn");
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			// 
			// cmnuTableDelete
			// 
			this.cmnuTableDelete.Image = global::WordHiddenPowers.Properties.Resources.TableDelete_16;
			this.cmnuTableDelete.Name = "cmnuTableDelete";
			resources.ApplyResources(this.cmnuTableDelete, "cmnuTableDelete");
			this.cmnuTableDelete.Click += new System.EventHandler(this.Delete_Click);
			// 
			// CreateTableDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreateTableDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
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
        private System.Windows.Forms.ToolStripMenuItem mnuTableDeleteRows;
        private System.Windows.Forms.ToolStripMenuItem mnuTableDeleteColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuInserRowAbove;
        private System.Windows.Forms.ToolStripButton mnuInsertColumnRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton mnuDeleteTable;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteRows;
        private System.Windows.Forms.ToolStripButton buttonBold;
        private System.Windows.Forms.ToolStripSplitButton fontColorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSplitButton backColorButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    }
}