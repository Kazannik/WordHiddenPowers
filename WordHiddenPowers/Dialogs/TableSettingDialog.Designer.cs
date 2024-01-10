namespace WordHiddenPowers.Dialogs
{
    partial class TableSettingDialog
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
            this.columnCountLabel = new System.Windows.Forms.Label();
            this.rowCountLabel = new System.Windows.Forms.Label();
            this.columnCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rowCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.columnCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowCountNumericUpDown)).BeginInit();
            this.tableSizeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnCountLabel
            // 
            this.columnCountLabel.AutoSize = true;
            this.columnCountLabel.Location = new System.Drawing.Point(6, 21);
            this.columnCountLabel.Name = "columnCountLabel";
            this.columnCountLabel.Size = new System.Drawing.Size(92, 13);
            this.columnCountLabel.TabIndex = 0;
            this.columnCountLabel.Text = "Число столбцов:";
            // 
            // rowCountLabel
            // 
            this.rowCountLabel.AutoSize = true;
            this.rowCountLabel.Location = new System.Drawing.Point(6, 47);
            this.rowCountLabel.Name = "rowCountLabel";
            this.rowCountLabel.Size = new System.Drawing.Size(74, 13);
            this.rowCountLabel.TabIndex = 1;
            this.rowCountLabel.Text = "Число строк:";
            // 
            // columnCountNumericUpDown
            // 
            this.columnCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.columnCountNumericUpDown.Location = new System.Drawing.Point(123, 19);
            this.columnCountNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.columnCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.columnCountNumericUpDown.Name = "columnCountNumericUpDown";
            this.columnCountNumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.columnCountNumericUpDown.TabIndex = 2;
            this.columnCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // rowCountNumericUpDown
            // 
            this.rowCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rowCountNumericUpDown.Location = new System.Drawing.Point(123, 45);
            this.rowCountNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.rowCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowCountNumericUpDown.Name = "rowCountNumericUpDown";
            this.rowCountNumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.rowCountNumericUpDown.TabIndex = 3;
            this.rowCountNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tableSizeGroupBox
            // 
            this.tableSizeGroupBox.Controls.Add(this.columnCountLabel);
            this.tableSizeGroupBox.Controls.Add(this.rowCountLabel);
            this.tableSizeGroupBox.Controls.Add(this.rowCountNumericUpDown);
            this.tableSizeGroupBox.Controls.Add(this.columnCountNumericUpDown);
            this.tableSizeGroupBox.Location = new System.Drawing.Point(12, 12);
            this.tableSizeGroupBox.Name = "tableSizeGroupBox";
            this.tableSizeGroupBox.Size = new System.Drawing.Size(190, 82);
            this.tableSizeGroupBox.TabIndex = 4;
            this.tableSizeGroupBox.TabStop = false;
            this.tableSizeGroupBox.Text = "Размеры таблицы";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(46, 112);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 22);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "&ОК";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(127, 112);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 22);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.Text = "Отмена";
            // 
            // TableSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 146);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tableSizeGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TableSettingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Создание макета";
            ((System.ComponentModel.ISupportInitialize)(this.columnCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowCountNumericUpDown)).EndInit();
            this.tableSizeGroupBox.ResumeLayout(false);
            this.tableSizeGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label columnCountLabel;
        private System.Windows.Forms.Label rowCountLabel;
        private System.Windows.Forms.NumericUpDown columnCountNumericUpDown;
        private System.Windows.Forms.NumericUpDown rowCountNumericUpDown;
        private System.Windows.Forms.GroupBox tableSizeGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}