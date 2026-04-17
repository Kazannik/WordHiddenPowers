namespace WordHiddenPowers.Dialogs
{
	partial class LLMConnectSettingDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LLMConnectSettingDialog));
			this.mlNetModelNameComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.llmNameComboBox = new System.Windows.Forms.ComboBox();
			this.updateLLMArrayButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.llmGroupBox = new System.Windows.Forms.GroupBox();
			this.llmConnectionControlBox = new WordHiddenPowers.Controls.LLMConnectionControlBox();
			this.testModelButton = new System.Windows.Forms.Button();
			this.timeoutGroupBox = new System.Windows.Forms.GroupBox();
			this.secondsLabel = new System.Windows.Forms.Label();
			this.secondsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.minutesNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.minutesLabel = new System.Windows.Forms.Label();
			this.embeddingLlmNameComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.llmGroupBox.SuspendLayout();
			this.timeoutGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.secondsNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.minutesNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// mlNetModelNameComboBox
			// 
			resources.ApplyResources(this.mlNetModelNameComboBox, "mlNetModelNameComboBox");
			this.mlNetModelNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mlNetModelNameComboBox.FormattingEnabled = true;
			this.mlNetModelNameComboBox.Name = "mlNetModelNameComboBox";
			this.mlNetModelNameComboBox.SelectedIndexChanged += new System.EventHandler(this.MlNetModelNameComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
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
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// llmNameComboBox
			// 
			resources.ApplyResources(this.llmNameComboBox, "llmNameComboBox");
			this.llmNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.llmNameComboBox.FormattingEnabled = true;
			this.llmNameComboBox.Name = "llmNameComboBox";
			// 
			// updateLLMArrayButton
			// 
			resources.ApplyResources(this.updateLLMArrayButton, "updateLLMArrayButton");
			this.updateLLMArrayButton.Name = "updateLLMArrayButton";
			this.updateLLMArrayButton.UseVisualStyleBackColor = true;
			this.updateLLMArrayButton.Click += new System.EventHandler(this.Update_Click);
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// llmGroupBox
			// 
			resources.ApplyResources(this.llmGroupBox, "llmGroupBox");
			this.llmGroupBox.Controls.Add(this.llmConnectionControlBox);
			this.llmGroupBox.Controls.Add(this.testModelButton);
			this.llmGroupBox.Controls.Add(this.timeoutGroupBox);
			this.llmGroupBox.Controls.Add(this.embeddingLlmNameComboBox);
			this.llmGroupBox.Controls.Add(this.label4);
			this.llmGroupBox.Controls.Add(this.updateLLMArrayButton);
			this.llmGroupBox.Controls.Add(this.llmNameComboBox);
			this.llmGroupBox.Controls.Add(this.label2);
			this.llmGroupBox.Name = "llmGroupBox";
			this.llmGroupBox.TabStop = false;
			// 
			// llmConnectionControlBox
			// 
			this.llmConnectionControlBox.Address = "";
			resources.ApplyResources(this.llmConnectionControlBox, "llmConnectionControlBox");
			this.llmConnectionControlBox.ConnectingTimeout = 100;
			this.llmConnectionControlBox.Name = "llmConnectionControlBox";
			this.llmConnectionControlBox.PingTimeout = 100;
			// 
			// testModelButton
			// 
			resources.ApplyResources(this.testModelButton, "testModelButton");
			this.testModelButton.Name = "testModelButton";
			this.testModelButton.UseVisualStyleBackColor = true;
			this.testModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
			// 
			// timeoutGroupBox
			// 
			this.timeoutGroupBox.Controls.Add(this.secondsLabel);
			this.timeoutGroupBox.Controls.Add(this.secondsNumericUpDown);
			this.timeoutGroupBox.Controls.Add(this.minutesNumericUpDown);
			this.timeoutGroupBox.Controls.Add(this.minutesLabel);
			resources.ApplyResources(this.timeoutGroupBox, "timeoutGroupBox");
			this.timeoutGroupBox.Name = "timeoutGroupBox";
			this.timeoutGroupBox.TabStop = false;
			// 
			// secondsLabel
			// 
			resources.ApplyResources(this.secondsLabel, "secondsLabel");
			this.secondsLabel.Name = "secondsLabel";
			// 
			// secondsNumericUpDown
			// 
			resources.ApplyResources(this.secondsNumericUpDown, "secondsNumericUpDown");
			this.secondsNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.secondsNumericUpDown.Name = "secondsNumericUpDown";
			// 
			// minutesNumericUpDown
			// 
			resources.ApplyResources(this.minutesNumericUpDown, "minutesNumericUpDown");
			this.minutesNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.minutesNumericUpDown.Name = "minutesNumericUpDown";
			// 
			// minutesLabel
			// 
			resources.ApplyResources(this.minutesLabel, "minutesLabel");
			this.minutesLabel.Name = "minutesLabel";
			// 
			// embeddingLlmNameComboBox
			// 
			resources.ApplyResources(this.embeddingLlmNameComboBox, "embeddingLlmNameComboBox");
			this.embeddingLlmNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.embeddingLlmNameComboBox.FormattingEnabled = true;
			this.embeddingLlmNameComboBox.Name = "embeddingLlmNameComboBox";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// LLMConnectSettingDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.llmGroupBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mlNetModelNameComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LLMConnectSettingDialog";
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
			this.llmGroupBox.ResumeLayout(false);
			this.llmGroupBox.PerformLayout();
			this.timeoutGroupBox.ResumeLayout(false);
			this.timeoutGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.secondsNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.minutesNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox mlNetModelNameComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox llmNameComboBox;
		private System.Windows.Forms.Button updateLLMArrayButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox llmGroupBox;
		private System.Windows.Forms.ComboBox embeddingLlmNameComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox timeoutGroupBox;
		private System.Windows.Forms.Label secondsLabel;
		private System.Windows.Forms.NumericUpDown secondsNumericUpDown;
		private System.Windows.Forms.NumericUpDown minutesNumericUpDown;
		private System.Windows.Forms.Label minutesLabel;
		private System.Windows.Forms.Button testModelButton;
		private Controls.LLMConnectionControlBox llmConnectionControlBox;
	}
}