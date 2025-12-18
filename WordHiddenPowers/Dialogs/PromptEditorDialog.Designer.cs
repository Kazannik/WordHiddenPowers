namespace WordHiddenPowers.Dialogs
{
	partial class PromptEditorDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptEditorDialog));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.captionTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.systemMessageTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.prefixUserMessageTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.postfixUserMessageTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(493, 373);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(106, 34);
			this.cancelButton.TabIndex = 30;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(381, 373);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(106, 34);
			this.okButton.TabIndex = 29;
			this.okButton.Text = "&ОК";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 16);
			this.label1.TabIndex = 31;
			this.label1.Text = "Заголовок кнопки";
			// 
			// captionTextBox
			// 
			this.captionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.captionTextBox.Location = new System.Drawing.Point(12, 28);
			this.captionTextBox.Name = "captionTextBox";
			this.captionTextBox.Size = new System.Drawing.Size(587, 22);
			this.captionTextBox.TabIndex = 32;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Системный промпт:";
			// 
			// systemMessageTextBox
			// 
			this.systemMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.systemMessageTextBox.Location = new System.Drawing.Point(12, 84);
			this.systemMessageTextBox.Multiline = true;
			this.systemMessageTextBox.Name = "systemMessageTextBox";
			this.systemMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.systemMessageTextBox.Size = new System.Drawing.Size(587, 124);
			this.systemMessageTextBox.TabIndex = 34;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 211);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(368, 16);
			this.label4.TabIndex = 38;
			this.label4.Text = "Пользовательский промпт - префикс (не обязательно):";
			// 
			// prefixUserMessageTextBox
			// 
			this.prefixUserMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.prefixUserMessageTextBox.Location = new System.Drawing.Point(12, 232);
			this.prefixUserMessageTextBox.Multiline = true;
			this.prefixUserMessageTextBox.Name = "prefixUserMessageTextBox";
			this.prefixUserMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.prefixUserMessageTextBox.Size = new System.Drawing.Size(587, 48);
			this.prefixUserMessageTextBox.TabIndex = 37;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 283);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(374, 16);
			this.label5.TabIndex = 40;
			this.label5.Text = "Пользовательский промпт - постфикс (не обязательно):";
			// 
			// postfixUserMessageTextBox
			// 
			this.postfixUserMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.postfixUserMessageTextBox.Location = new System.Drawing.Point(12, 304);
			this.postfixUserMessageTextBox.Multiline = true;
			this.postfixUserMessageTextBox.Name = "postfixUserMessageTextBox";
			this.postfixUserMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.postfixUserMessageTextBox.Size = new System.Drawing.Size(587, 48);
			this.postfixUserMessageTextBox.TabIndex = 39;
			// 
			// PromptEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(611, 419);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.postfixUserMessageTextBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.prefixUserMessageTextBox);
			this.Controls.Add(this.systemMessageTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.captionTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PromptEditorDialog";
			this.ShowInTaskbar = false;
			this.Text = "Редактор промпта";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox captionTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox systemMessageTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox prefixUserMessageTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox postfixUserMessageTextBox;
	}
}