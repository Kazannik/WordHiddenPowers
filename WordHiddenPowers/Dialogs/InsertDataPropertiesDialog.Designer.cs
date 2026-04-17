namespace WordHiddenPowers.Dialogs
{
	partial class InsertDataPropertiesDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertDataPropertiesDialog));
			this.minRatingNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.maxRatingNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.noteCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.viewHideCheckBox = new System.Windows.Forms.CheckBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.allRatingCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.minRatingNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxRatingNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.noteCountNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// minRatingNumericUpDown
			// 
			resources.ApplyResources(this.minRatingNumericUpDown, "minRatingNumericUpDown");
			this.minRatingNumericUpDown.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.minRatingNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
			this.minRatingNumericUpDown.Name = "minRatingNumericUpDown";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// maxRatingNumericUpDown
			// 
			resources.ApplyResources(this.maxRatingNumericUpDown, "maxRatingNumericUpDown");
			this.maxRatingNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.maxRatingNumericUpDown.Name = "maxRatingNumericUpDown";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// noteCountNumericUpDown
			// 
			resources.ApplyResources(this.noteCountNumericUpDown, "noteCountNumericUpDown");
			this.noteCountNumericUpDown.Name = "noteCountNumericUpDown";
			this.noteCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// viewHideCheckBox
			// 
			resources.ApplyResources(this.viewHideCheckBox, "viewHideCheckBox");
			this.viewHideCheckBox.Name = "viewHideCheckBox";
			this.viewHideCheckBox.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			// 
			// allRatingCheckBox
			// 
			resources.ApplyResources(this.allRatingCheckBox, "allRatingCheckBox");
			this.allRatingCheckBox.Name = "allRatingCheckBox";
			this.allRatingCheckBox.UseVisualStyleBackColor = true;
			this.allRatingCheckBox.CheckedChanged += new System.EventHandler(this.AllRatingCheckBox_CheckedChanged);
			// 
			// InsertDataPropertiesDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.allRatingCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.viewHideCheckBox);
			this.Controls.Add(this.noteCountNumericUpDown);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.maxRatingNumericUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.minRatingNumericUpDown);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "InsertDataPropertiesDialog";
			((System.ComponentModel.ISupportInitialize)(this.minRatingNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxRatingNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.noteCountNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown minRatingNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown maxRatingNumericUpDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown noteCountNumericUpDown;
		private System.Windows.Forms.CheckBox viewHideCheckBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.CheckBox allRatingCheckBox;
	}
}