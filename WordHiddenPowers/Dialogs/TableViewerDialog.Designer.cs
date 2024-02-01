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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableViewerDialog));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.topCaptionPanel = new System.Windows.Forms.Panel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.tableEditBox = new WordHiddenPowers.Controls.TableEditBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.percentColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.topCaptionPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(796, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // topCaptionPanel
            // 
            this.topCaptionPanel.BackColor = System.Drawing.SystemColors.Highlight;
            this.topCaptionPanel.Controls.Add(this.nameLabel);
            this.topCaptionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topCaptionPanel.Location = new System.Drawing.Point(0, 0);
            this.topCaptionPanel.Margin = new System.Windows.Forms.Padding(2);
            this.topCaptionPanel.Name = "topCaptionPanel";
            this.topCaptionPanel.Size = new System.Drawing.Size(796, 26);
            this.topCaptionPanel.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.nameLabel.Location = new System.Drawing.Point(6, 6);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(14, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "#";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearButton,
            this.toolStripSeparator1,
            this.refreshButton,
            this.toolStripSeparator2,
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(796, 31);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearButton.Image = global::WordHiddenPowers.Properties.Resources.TableClear_24;
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(28, 28);
            this.clearButton.Text = "Очистить таблицу";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::WordHiddenPowers.Properties.Resources.Refresh_24;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(28, 28);
            this.refreshButton.Text = "Обновить данные";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Enabled = false;
            this.saveButton.Image = global::WordHiddenPowers.Properties.Resources.Save_24;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(28, 28);
            this.saveButton.Text = "Сохранить данные";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tableEditBox
            // 
            this.tableEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableEditBox.Location = new System.Drawing.Point(0, 0);
            this.tableEditBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableEditBox.Name = "tableEditBox";
            this.tableEditBox.PowersDataSet = null;
            this.tableEditBox.Size = new System.Drawing.Size(397, 341);
            this.tableEditBox.TabIndex = 4;
            this.tableEditBox.Table = null;
            this.tableEditBox.ValueChanged += new System.EventHandler(this.tableEditBox_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 57);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableEditBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(796, 341);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.TabIndex = 5;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.valueColumn,
            this.percentColumn});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(395, 341);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Наименование";
            this.nameColumn.Width = 226;
            // 
            // valueColumn
            // 
            this.valueColumn.Text = "Количество";
            this.valueColumn.Width = 94;
            // 
            // percentColumn
            // 
            this.percentColumn.Text = "%";
            // 
            // TableViewerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 420);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.topCaptionPanel);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TableViewerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Таблица данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableEditorDialog_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableEditorDialog_FormClosed);
            this.topCaptionPanel.ResumeLayout(false);
            this.topCaptionPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private WordHiddenPowers.Controls.TableEditBox tableEditBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader valueColumn;
        private System.Windows.Forms.ColumnHeader percentColumn;
    }
}