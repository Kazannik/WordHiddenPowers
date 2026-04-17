namespace WordHiddenPowers.Dialogs
{
    partial class TableEditorDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableEditorDialog));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.topCaptionPanel = new System.Windows.Forms.Panel();
			this.nameLabel = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.clearButton = new System.Windows.Forms.ToolStripButton();
			this.deleteButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveButton = new System.Windows.Forms.ToolStripButton();
			this.tableEditBox = new WordHiddenPowers.Controls.TableEditBox();
			this.topCaptionPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
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
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearButton,
            this.deleteButton,
            this.toolStripSeparator1,
            this.refreshButton,
            this.toolStripSeparator2,
            this.saveButton});
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Name = "toolStrip1";
			// 
			// clearButton
			// 
			this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearButton.Image = global::WordHiddenPowers.Properties.Resources.TableClear_24;
			resources.ApplyResources(this.clearButton, "clearButton");
			this.clearButton.Name = "clearButton";
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.deleteButton.Image = global::WordHiddenPowers.Properties.Resources.TableDelete_24;
			resources.ApplyResources(this.deleteButton, "deleteButton");
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// refreshButton
			// 
			this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshButton.Image = global::WordHiddenPowers.Properties.Resources.Refresh_24;
			resources.ApplyResources(this.refreshButton, "refreshButton");
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// saveButton
			// 
			this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.saveButton, "saveButton");
			this.saveButton.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
			this.saveButton.Name = "saveButton";
			this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// tableEditBox
			// 
			resources.ApplyResources(this.tableEditBox, "tableEditBox");
			this.tableEditBox.LastDataSet = null;
			this.tableEditBox.Name = "tableEditBox";
			this.tableEditBox.NowDataSet = null;
			this.tableEditBox.ReadOnly = false;
			this.tableEditBox.Table = null;
			this.tableEditBox.ValueChanged += new System.EventHandler(this.TableEditBox_ValueChanged);
			// 
			// TableEditorDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableEditBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.topCaptionPanel);
			this.Controls.Add(this.statusStrip1);
			this.Name = "TableEditorDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableEditorDialog_FormClosing);
			this.topCaptionPanel.ResumeLayout(false);
			this.topCaptionPanel.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel topCaptionPanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.TableEditBox tableEditBox;
        private System.Windows.Forms.ToolStripButton deleteButton;
    }
}