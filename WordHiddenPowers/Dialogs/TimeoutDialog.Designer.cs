namespace WordHiddenPowers.Dialogs
{
	partial class TimeoutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeoutDialog));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.pingTimeoutLabel = new System.Windows.Forms.Label();
			this.pingTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.timeoutGroupBox = new System.Windows.Forms.GroupBox();
			this.timeoutBox = new ControlLibrary.Controls.TimeControls.TimeBox();
			((System.ComponentModel.ISupportInitialize)(this.pingTimeoutNumericUpDown)).BeginInit();
			this.timeoutGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cancelButton.Location = new System.Drawing.Point(136, 114);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 27);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.okButton.Location = new System.Drawing.Point(29, 114);
			this.okButton.Margin = new System.Windows.Forms.Padding(4);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(100, 27);
			this.okButton.TabIndex = 8;
			this.okButton.Text = "&ОК";
			// 
			// pingTimeoutLabel
			// 
			this.pingTimeoutLabel.AutoSize = true;
			this.pingTimeoutLabel.Location = new System.Drawing.Point(12, 12);
			this.pingTimeoutLabel.Name = "pingTimeoutLabel";
			this.pingTimeoutLabel.Size = new System.Drawing.Size(133, 16);
			this.pingTimeoutLabel.TabIndex = 10;
			this.pingTimeoutLabel.Text = "Ping Timeout (msec):";
			// 
			// pingTimeoutNumericUpDown
			// 
			this.pingTimeoutNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pingTimeoutNumericUpDown.Location = new System.Drawing.Point(156, 8);
			this.pingTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.pingTimeoutNumericUpDown.Name = "pingTimeoutNumericUpDown";
			this.pingTimeoutNumericUpDown.Size = new System.Drawing.Size(78, 24);
			this.pingTimeoutNumericUpDown.TabIndex = 11;
			// 
			// timeoutGroupBox
			// 
			this.timeoutGroupBox.Controls.Add(this.timeoutBox);
			this.timeoutGroupBox.Location = new System.Drawing.Point(12, 48);
			this.timeoutGroupBox.Name = "timeoutGroupBox";
			this.timeoutGroupBox.Size = new System.Drawing.Size(222, 54);
			this.timeoutGroupBox.TabIndex = 19;
			this.timeoutGroupBox.TabStop = false;
			this.timeoutGroupBox.Text = "Timeout";
			// 
			// timeoutBox
			// 
			this.timeoutBox.BackColor = System.Drawing.SystemColors.Window;
			this.timeoutBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timeoutBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.timeoutBox.ForeColor = System.Drawing.SystemColors.WindowText;
			this.timeoutBox.Location = new System.Drawing.Point(36, 24);
			this.timeoutBox.Margin = new System.Windows.Forms.Padding(0);
			this.timeoutBox.MaxValue = System.TimeSpan.Parse("23:59:59");
			this.timeoutBox.Name = "timeoutBox";
			this.timeoutBox.Size = new System.Drawing.Size(152, 27);
			this.timeoutBox.TabIndex = 0;
			this.timeoutBox.Value = System.TimeSpan.Parse("00:00:00");
			this.timeoutBox.View = ((ControlLibrary.Controls.TimeControls.TimeBox.TimeControlUnitFlags)(((ControlLibrary.Controls.TimeControls.TimeBox.TimeControlUnitFlags.Seconds | ControlLibrary.Controls.TimeControls.TimeBox.TimeControlUnitFlags.Minutes) 
            | ControlLibrary.Controls.TimeControls.TimeBox.TimeControlUnitFlags.Hours)));
			// 
			// TimeoutDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(247, 149);
			this.Controls.Add(this.timeoutGroupBox);
			this.Controls.Add(this.pingTimeoutNumericUpDown);
			this.Controls.Add(this.pingTimeoutLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TimeoutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Timeout";
			((System.ComponentModel.ISupportInitialize)(this.pingTimeoutNumericUpDown)).EndInit();
			this.timeoutGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label pingTimeoutLabel;
		private System.Windows.Forms.NumericUpDown pingTimeoutNumericUpDown;
		private System.Windows.Forms.GroupBox timeoutGroupBox;
		private ControlLibrary.Controls.TimeControls.TimeBox timeoutBox;
	}
}