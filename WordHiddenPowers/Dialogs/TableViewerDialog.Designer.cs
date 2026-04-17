namespace WordHiddenPowers.Dialogs
{
    partial class TableViewerDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableViewerDialog));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.topCaptionPanel = new System.Windows.Forms.Panel();
			this.nameLabel = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableEditBox = new WordHiddenPowers.Controls.TableEditBox();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.listView1 = new System.Windows.Forms.ListView();
			this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.valueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.percentColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.topCaptionPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// topCaptionPanel
			// 
			this.topCaptionPanel.BackColor = System.Drawing.SystemColors.Highlight;
			this.topCaptionPanel.Controls.Add(this.nameLabel);
			resources.ApplyResources(this.topCaptionPanel, "topCaptionPanel");
			this.topCaptionPanel.Name = "topCaptionPanel";
			// 
			// nameLabel
			// 
			resources.ApplyResources(this.nameLabel, "nameLabel");
			this.nameLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.nameLabel.Name = "nameLabel";
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableEditBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listView1);
			// 
			// tableEditBox
			// 
			this.tableEditBox.ContextMenuStrip = this.contextMenu;
			resources.ApplyResources(this.tableEditBox, "tableEditBox");
			this.tableEditBox.LastDataSet = null;
			this.tableEditBox.Name = "tableEditBox";
			this.tableEditBox.NowDataSet = null;
			this.tableEditBox.ReadOnly = true;
			this.tableEditBox.Table = null;
			this.tableEditBox.SelectedCell += new System.EventHandler<WordHiddenPowers.Controls.TableEditBox.TableEventArgs>(this.TableEditBox_SelectedCell);
			// 
			// contextMenu
			// 
			this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
			this.contextMenu.Name = "contextMenu";
			resources.ApplyResources(this.contextMenu, "contextMenu");
			// 
			// mnuCopy
			// 
			this.mnuCopy.Name = "mnuCopy";
			resources.ApplyResources(this.mnuCopy, "mnuCopy");
			this.mnuCopy.Click += new System.EventHandler(this.Copy_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.valueColumn,
            this.percentColumn});
			this.listView1.ContextMenuStrip = this.contextMenu;
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
			// 
			// nameColumn
			// 
			resources.ApplyResources(this.nameColumn, "nameColumn");
			// 
			// valueColumn
			// 
			resources.ApplyResources(this.valueColumn, "valueColumn");
			// 
			// percentColumn
			// 
			resources.ApplyResources(this.percentColumn, "percentColumn");
			// 
			// TableViewerDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.topCaptionPanel);
			this.Controls.Add(this.statusStrip1);
			this.Name = "TableViewerDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableEditorDialog_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableEditorDialog_FormClosed);
			this.topCaptionPanel.ResumeLayout(false);
			this.topCaptionPanel.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel topCaptionPanel;
        private System.Windows.Forms.Label nameLabel;
        private WordHiddenPowers.Controls.TableEditBox tableEditBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader valueColumn;
        private System.Windows.Forms.ColumnHeader percentColumn;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuCopy;
	}
}