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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableEditBox = new WordHiddenPowers.Controls.TableEditBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.percentColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.topCaptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1061, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // topCaptionPanel
            // 
            this.topCaptionPanel.BackColor = System.Drawing.SystemColors.Highlight;
            this.topCaptionPanel.Controls.Add(this.nameLabel);
            this.topCaptionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topCaptionPanel.Location = new System.Drawing.Point(0, 0);
            this.topCaptionPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.topCaptionPanel.Name = "topCaptionPanel";
            this.topCaptionPanel.Size = new System.Drawing.Size(1061, 32);
            this.topCaptionPanel.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.nameLabel.Location = new System.Drawing.Point(8, 7);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(16, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "#";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableEditBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(1061, 463);
            this.splitContainer1.SplitterDistance = 529;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // tableEditBox
            // 
            this.tableEditBox.DataSet = null;
            this.tableEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableEditBox.Location = new System.Drawing.Point(0, 0);
            this.tableEditBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableEditBox.Name = "tableEditBox";
            this.tableEditBox.ReadOnly = true;
            this.tableEditBox.Size = new System.Drawing.Size(529, 463);
            this.tableEditBox.TabIndex = 4;
            this.tableEditBox.Table = null;
            this.tableEditBox.SelectedCell += new System.EventHandler<WordHiddenPowers.Controls.TableEditBox.TableEventArgs>(this.tableEditBox_SelectedCell);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.valueColumn,
            this.percentColumn});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(527, 463);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 517);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.topCaptionPanel);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TableViewerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Таблица данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableEditorDialog_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableEditorDialog_FormClosed);
            this.topCaptionPanel.ResumeLayout(false);
            this.topCaptionPanel.PerformLayout();
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
        private WordHiddenPowers.Controls.TableEditBox tableEditBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader valueColumn;
        private System.Windows.Forms.ColumnHeader percentColumn;
    }
}