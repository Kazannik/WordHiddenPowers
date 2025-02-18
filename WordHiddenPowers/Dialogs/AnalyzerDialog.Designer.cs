namespace WordHiddenPowers.Dialogs
{
    partial class AnalyzerDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyzerDialog));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileInsertToWord = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.insertToDocumentButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.analyzerListBox = new WordHiddenPowers.Controls.ListControls.AnalyzerListBox();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(901, 36);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileInsertToWord});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(69, 32);
			this.mnuFile.Text = "Файл";
			// 
			// mnuFileInsertToWord
			// 
			this.mnuFileInsertToWord.Name = "mnuFileInsertToWord";
			this.mnuFileInsertToWord.Size = new System.Drawing.Size(368, 34);
			this.mnuFileInsertToWord.Text = "Вставить в открытый документ";
			this.mnuFileInsertToWord.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToDocumentButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 36);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(901, 38);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// insertToDocumentButton
			// 
			this.insertToDocumentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.insertToDocumentButton.Image = ((System.Drawing.Image)(resources.GetObject("insertToDocumentButton.Image")));
			this.insertToDocumentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.insertToDocumentButton.Name = "insertToDocumentButton";
			this.insertToDocumentButton.Size = new System.Drawing.Size(34, 33);
			this.insertToDocumentButton.Text = "Вставить в открытый документ";
			this.insertToDocumentButton.Click += new System.EventHandler(this.InsertToDocumentButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 560);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip1.Size = new System.Drawing.Size(901, 32);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(179, 25);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 74);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.analyzerListBox);
			this.splitContainer1.Size = new System.Drawing.Size(901, 486);
			this.splitContainer1.SplitterDistance = 615;
			this.splitContainer1.TabIndex = 3;
			// 
			// analyzerListBox
			// 
			this.analyzerListBox.DataSet = null;
			this.analyzerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.analyzerListBox.FormattingEnabled = true;
			this.analyzerListBox.IntegralHeight = false;
			this.analyzerListBox.Location = new System.Drawing.Point(0, 0);
			this.analyzerListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.analyzerListBox.Name = "analyzerListBox";
			this.analyzerListBox.Size = new System.Drawing.Size(615, 486);
			this.analyzerListBox.TabIndex = 0;
			// 
			// AnalyzerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(901, 592);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AnalyzerDialog";
			this.Text = "AnalyzerDialog";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton insertToDocumentButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private WordHiddenPowers.Controls.ListControls.AnalyzerListBox analyzerListBox;
        private System.Windows.Forms.ToolStripMenuItem mnuFileInsertToWord;
    }
}