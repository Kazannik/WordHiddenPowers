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
            this.minRatingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxRatingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.noteCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.viewHideCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minRatingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRatingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // minRatingNumericUpDown
            // 
            this.minRatingNumericUpDown.Location = new System.Drawing.Point(12, 32);
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
            this.minRatingNumericUpDown.Size = new System.Drawing.Size(120, 26);
            this.minRatingNumericUpDown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Верхний порог негативного рейтинга:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Нижний порог позитивного рейтинга:";
            // 
            // maxRatingNumericUpDown
            // 
            this.maxRatingNumericUpDown.Location = new System.Drawing.Point(12, 104);
            this.maxRatingNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.maxRatingNumericUpDown.Name = "maxRatingNumericUpDown";
            this.maxRatingNumericUpDown.Size = new System.Drawing.Size(120, 26);
            this.maxRatingNumericUpDown.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(286, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Максимальное количество заметок:";
            // 
            // noteCountNumericUpDown
            // 
            this.noteCountNumericUpDown.Location = new System.Drawing.Point(12, 176);
            this.noteCountNumericUpDown.Name = "noteCountNumericUpDown";
            this.noteCountNumericUpDown.Size = new System.Drawing.Size(120, 26);
            this.noteCountNumericUpDown.TabIndex = 5;
            this.noteCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // viewHideCheckBox
            // 
            this.viewHideCheckBox.AutoSize = true;
            this.viewHideCheckBox.Location = new System.Drawing.Point(12, 228);
            this.viewHideCheckBox.Name = "viewHideCheckBox";
            this.viewHideCheckBox.Size = new System.Drawing.Size(319, 24);
            this.viewHideCheckBox.TabIndex = 6;
            this.viewHideCheckBox.Text = "Включать заблокированные заметки";
            this.viewHideCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(240, 269);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 34);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(116, 269);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 34);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "&ОК";
            // 
            // InsertDataPropertiesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(365, 317);
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
            this.Text = "Параметры вставки";
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
	}
}