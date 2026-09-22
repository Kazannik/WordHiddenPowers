namespace WordHiddenPowers.Dialogs
{
	partial class LLMChatOptionsDialog
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
			this.maxOutputTokenCountLabel = new System.Windows.Forms.Label();
			this.maxOutputTokenCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frequencyPenaltyLabel = new System.Windows.Forms.Label();
			this.frequencyPenaltyTrackBar = new System.Windows.Forms.TrackBar();
			this.presencePenaltyTrackBar = new System.Windows.Forms.TrackBar();
			this.temperatureTrackBar = new System.Windows.Forms.TrackBar();
			this.topPTrackBar = new System.Windows.Forms.TrackBar();
			this.presencePenaltyLabel = new System.Windows.Forms.Label();
			this.temperatureLabel = new System.Windows.Forms.Label();
			this.topPLabel = new System.Windows.Forms.Label();
			this.frequencyPenaltyNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.presencePenaltyNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.temperatureNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.topPNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.maxOutputTokenCountNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frequencyPenaltyTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.presencePenaltyTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.temperatureTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.topPTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frequencyPenaltyNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.presencePenaltyNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.temperatureNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.topPNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// maxOutputTokenCountLabel
			// 
			this.maxOutputTokenCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.maxOutputTokenCountLabel.AutoSize = true;
			this.maxOutputTokenCountLabel.Location = new System.Drawing.Point(6, 79);
			this.maxOutputTokenCountLabel.Name = "maxOutputTokenCountLabel";
			this.maxOutputTokenCountLabel.Size = new System.Drawing.Size(152, 16);
			this.maxOutputTokenCountLabel.TabIndex = 2;
			this.maxOutputTokenCountLabel.Text = "Max Output Token Count";
			// 
			// maxOutputTokenCountNumericUpDown
			// 
			this.maxOutputTokenCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.maxOutputTokenCountNumericUpDown.Location = new System.Drawing.Point(168, 76);
			this.maxOutputTokenCountNumericUpDown.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
			this.maxOutputTokenCountNumericUpDown.Name = "maxOutputTokenCountNumericUpDown";
			this.maxOutputTokenCountNumericUpDown.Size = new System.Drawing.Size(114, 22);
			this.maxOutputTokenCountNumericUpDown.TabIndex = 3;
			// 
			// frequencyPenaltyLabel
			// 
			this.frequencyPenaltyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.frequencyPenaltyLabel.AutoSize = true;
			this.frequencyPenaltyLabel.Location = new System.Drawing.Point(6, 139);
			this.frequencyPenaltyLabel.Name = "frequencyPenaltyLabel";
			this.frequencyPenaltyLabel.Size = new System.Drawing.Size(119, 16);
			this.frequencyPenaltyLabel.TabIndex = 4;
			this.frequencyPenaltyLabel.Text = "Frequency Penalty";
			// 
			// frequencyPenaltyTrackBar
			// 
			this.frequencyPenaltyTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.frequencyPenaltyTrackBar.Location = new System.Drawing.Point(234, 136);
			this.frequencyPenaltyTrackBar.Maximum = 20;
			this.frequencyPenaltyTrackBar.Minimum = -20;
			this.frequencyPenaltyTrackBar.Name = "frequencyPenaltyTrackBar";
			this.frequencyPenaltyTrackBar.Size = new System.Drawing.Size(432, 56);
			this.frequencyPenaltyTrackBar.TabIndex = 6;
			this.frequencyPenaltyTrackBar.ValueChanged += new System.EventHandler(this.FrequencyPenalty_ValueChanged);
			// 
			// presencePenaltyTrackBar
			// 
			this.presencePenaltyTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.presencePenaltyTrackBar.Location = new System.Drawing.Point(234, 196);
			this.presencePenaltyTrackBar.Maximum = 20;
			this.presencePenaltyTrackBar.Minimum = -20;
			this.presencePenaltyTrackBar.Name = "presencePenaltyTrackBar";
			this.presencePenaltyTrackBar.Size = new System.Drawing.Size(432, 56);
			this.presencePenaltyTrackBar.TabIndex = 9;
			this.presencePenaltyTrackBar.ValueChanged += new System.EventHandler(this.PresencePenalty_ValueChanged);
			// 
			// temperatureTrackBar
			// 
			this.temperatureTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.temperatureTrackBar.Location = new System.Drawing.Point(234, 256);
			this.temperatureTrackBar.Maximum = 20;
			this.temperatureTrackBar.Name = "temperatureTrackBar";
			this.temperatureTrackBar.Size = new System.Drawing.Size(432, 56);
			this.temperatureTrackBar.TabIndex = 12;
			this.temperatureTrackBar.ValueChanged += new System.EventHandler(this.Temperature_ValueChanged);
			// 
			// topPTrackBar
			// 
			this.topPTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.topPTrackBar.Location = new System.Drawing.Point(234, 316);
			this.topPTrackBar.Name = "topPTrackBar";
			this.topPTrackBar.Size = new System.Drawing.Size(432, 56);
			this.topPTrackBar.TabIndex = 15;
			this.topPTrackBar.ValueChanged += new System.EventHandler(this.TopP_ValueChanged);
			// 
			// presencePenaltyLabel
			// 
			this.presencePenaltyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.presencePenaltyLabel.AutoSize = true;
			this.presencePenaltyLabel.Location = new System.Drawing.Point(6, 199);
			this.presencePenaltyLabel.Name = "presencePenaltyLabel";
			this.presencePenaltyLabel.Size = new System.Drawing.Size(113, 16);
			this.presencePenaltyLabel.TabIndex = 7;
			this.presencePenaltyLabel.Text = "Presence Penalty";
			// 
			// temperatureLabel
			// 
			this.temperatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.temperatureLabel.AutoSize = true;
			this.temperatureLabel.Location = new System.Drawing.Point(6, 259);
			this.temperatureLabel.Name = "temperatureLabel";
			this.temperatureLabel.Size = new System.Drawing.Size(85, 16);
			this.temperatureLabel.TabIndex = 10;
			this.temperatureLabel.Text = "Temperature";
			// 
			// topPLabel
			// 
			this.topPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.topPLabel.AutoSize = true;
			this.topPLabel.Location = new System.Drawing.Point(6, 319);
			this.topPLabel.Name = "topPLabel";
			this.topPLabel.Size = new System.Drawing.Size(44, 16);
			this.topPLabel.TabIndex = 13;
			this.topPLabel.Text = "Top P";
			// 
			// frequencyPenaltyNumericUpDown
			// 
			this.frequencyPenaltyNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.frequencyPenaltyNumericUpDown.DecimalPlaces = 1;
			this.frequencyPenaltyNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.frequencyPenaltyNumericUpDown.Location = new System.Drawing.Point(168, 136);
			this.frequencyPenaltyNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.frequencyPenaltyNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
			this.frequencyPenaltyNumericUpDown.Name = "frequencyPenaltyNumericUpDown";
			this.frequencyPenaltyNumericUpDown.Size = new System.Drawing.Size(60, 22);
			this.frequencyPenaltyNumericUpDown.TabIndex = 5;
			this.frequencyPenaltyNumericUpDown.ValueChanged += new System.EventHandler(this.FrequencyPenalty_ValueChanged);
			// 
			// presencePenaltyNumericUpDown
			// 
			this.presencePenaltyNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.presencePenaltyNumericUpDown.DecimalPlaces = 1;
			this.presencePenaltyNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.presencePenaltyNumericUpDown.Location = new System.Drawing.Point(168, 196);
			this.presencePenaltyNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.presencePenaltyNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
			this.presencePenaltyNumericUpDown.Name = "presencePenaltyNumericUpDown";
			this.presencePenaltyNumericUpDown.Size = new System.Drawing.Size(60, 22);
			this.presencePenaltyNumericUpDown.TabIndex = 8;
			this.presencePenaltyNumericUpDown.ValueChanged += new System.EventHandler(this.PresencePenalty_ValueChanged);
			// 
			// temperatureNumericUpDown
			// 
			this.temperatureNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.temperatureNumericUpDown.DecimalPlaces = 1;
			this.temperatureNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.temperatureNumericUpDown.Location = new System.Drawing.Point(168, 256);
			this.temperatureNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.temperatureNumericUpDown.Name = "temperatureNumericUpDown";
			this.temperatureNumericUpDown.Size = new System.Drawing.Size(60, 22);
			this.temperatureNumericUpDown.TabIndex = 11;
			this.temperatureNumericUpDown.ValueChanged += new System.EventHandler(this.Temperature_ValueChanged);
			// 
			// topPNumericUpDown
			// 
			this.topPNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.topPNumericUpDown.DecimalPlaces = 1;
			this.topPNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.topPNumericUpDown.Location = new System.Drawing.Point(168, 316);
			this.topPNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.topPNumericUpDown.Name = "topPNumericUpDown";
			this.topPNumericUpDown.Size = new System.Drawing.Size(60, 22);
			this.topPNumericUpDown.TabIndex = 14;
			this.topPNumericUpDown.ValueChanged += new System.EventHandler(this.TopP_ValueChanged);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cancelButton.Location = new System.Drawing.Point(564, 383);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(95, 27);
			this.cancelButton.TabIndex = 17;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.okButton.Location = new System.Drawing.Point(463, 383);
			this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(95, 27);
			this.okButton.TabIndex = 16;
			this.okButton.Text = "&ОК";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Options";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(72, 18);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(210, 24);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.Options_SelectedIndexChanged);
			// 
			// LLMChatOptionsDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(671, 424);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.topPNumericUpDown);
			this.Controls.Add(this.temperatureNumericUpDown);
			this.Controls.Add(this.presencePenaltyNumericUpDown);
			this.Controls.Add(this.frequencyPenaltyNumericUpDown);
			this.Controls.Add(this.topPLabel);
			this.Controls.Add(this.temperatureLabel);
			this.Controls.Add(this.presencePenaltyLabel);
			this.Controls.Add(this.topPTrackBar);
			this.Controls.Add(this.temperatureTrackBar);
			this.Controls.Add(this.presencePenaltyTrackBar);
			this.Controls.Add(this.frequencyPenaltyTrackBar);
			this.Controls.Add(this.frequencyPenaltyLabel);
			this.Controls.Add(this.maxOutputTokenCountNumericUpDown);
			this.Controls.Add(this.maxOutputTokenCountLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LLMChatOptionsDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat Options";
			((System.ComponentModel.ISupportInitialize)(this.maxOutputTokenCountNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frequencyPenaltyTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.presencePenaltyTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.temperatureTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.topPTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frequencyPenaltyNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.presencePenaltyNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.temperatureNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.topPNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label maxOutputTokenCountLabel;
		private System.Windows.Forms.NumericUpDown maxOutputTokenCountNumericUpDown;
		private System.Windows.Forms.Label frequencyPenaltyLabel;
		private System.Windows.Forms.TrackBar frequencyPenaltyTrackBar;
		private System.Windows.Forms.TrackBar presencePenaltyTrackBar;
		private System.Windows.Forms.TrackBar temperatureTrackBar;
		private System.Windows.Forms.TrackBar topPTrackBar;
		private System.Windows.Forms.Label presencePenaltyLabel;
		private System.Windows.Forms.Label temperatureLabel;
		private System.Windows.Forms.Label topPLabel;
		private System.Windows.Forms.NumericUpDown frequencyPenaltyNumericUpDown;
		private System.Windows.Forms.NumericUpDown presencePenaltyNumericUpDown;
		private System.Windows.Forms.NumericUpDown temperatureNumericUpDown;
		private System.Windows.Forms.NumericUpDown topPNumericUpDown;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}