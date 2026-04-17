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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableSettingDialog));
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
			resources.ApplyResources(this.columnCountLabel, "columnCountLabel");
			this.columnCountLabel.Name = "columnCountLabel";
			// 
			// rowCountLabel
			// 
			resources.ApplyResources(this.rowCountLabel, "rowCountLabel");
			this.rowCountLabel.Name = "rowCountLabel";
			// 
			// columnCountNumericUpDown
			// 
			resources.ApplyResources(this.columnCountNumericUpDown, "columnCountNumericUpDown");
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
			this.columnCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// rowCountNumericUpDown
			// 
			resources.ApplyResources(this.rowCountNumericUpDown, "rowCountNumericUpDown");
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
			resources.ApplyResources(this.tableSizeGroupBox, "tableSizeGroupBox");
			this.tableSizeGroupBox.Name = "tableSizeGroupBox";
			this.tableSizeGroupBox.TabStop = false;
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			// 
			// TableSettingDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.tableSizeGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TableSettingDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
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