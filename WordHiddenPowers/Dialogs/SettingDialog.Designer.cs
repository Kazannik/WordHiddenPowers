namespace WordHiddenPowers.Dialogs
{
	partial class SettingDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDialog));
            this.mlNetModelNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.llmNameComboBox = new System.Windows.Forms.ComboBox();
            this.uriTextBox = new System.Windows.Forms.TextBox();
            this.updateLLMArrayButton = new System.Windows.Forms.Button();
            this.hostErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hostErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mlNetModelNameComboBox
            // 
            this.mlNetModelNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mlNetModelNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mlNetModelNameComboBox.FormattingEnabled = true;
            this.mlNetModelNameComboBox.Location = new System.Drawing.Point(11, 44);
            this.mlNetModelNameComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mlNetModelNameComboBox.Name = "mlNetModelNameComboBox";
            this.mlNetModelNameComboBox.Size = new System.Drawing.Size(457, 24);
            this.mlNetModelNameComboBox.TabIndex = 0;
            this.mlNetModelNameComboBox.SelectedIndexChanged += new System.EventHandler(this.MlNetModelNameComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выбрать модель:";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(373, 276);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 27);
            this.cancelButton.TabIndex = 30;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(273, 276);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(95, 27);
            this.okButton.TabIndex = 29;
            this.okButton.Text = "&ОК";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Выбрать модель LLM:";
            // 
            // llmNameComboBox
            // 
            this.llmNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.llmNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.llmNameComboBox.FormattingEnabled = true;
            this.llmNameComboBox.Location = new System.Drawing.Point(12, 191);
            this.llmNameComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.llmNameComboBox.Name = "llmNameComboBox";
            this.llmNameComboBox.Size = new System.Drawing.Size(457, 24);
            this.llmNameComboBox.TabIndex = 31;
            // 
            // uriTextBox
            // 
            this.uriTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uriTextBox.Location = new System.Drawing.Point(11, 121);
            this.uriTextBox.Name = "uriTextBox";
            this.uriTextBox.Size = new System.Drawing.Size(328, 22);
            this.uriTextBox.TabIndex = 34;
            // 
            // updateLLMArrayButton
            // 
            this.updateLLMArrayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateLLMArrayButton.Location = new System.Drawing.Point(370, 119);
            this.updateLLMArrayButton.Name = "updateLLMArrayButton";
            this.updateLLMArrayButton.Size = new System.Drawing.Size(95, 27);
            this.updateLLMArrayButton.TabIndex = 35;
            this.updateLLMArrayButton.Text = "Обновить";
            this.updateLLMArrayButton.UseVisualStyleBackColor = true;
            this.updateLLMArrayButton.Click += new System.EventHandler(this.Update_Click);
            // 
            // hostErrorProvider
            // 
            this.hostErrorProvider.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Указать адрес хоста LLM:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(11, 229);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 27);
            this.button1.TabIndex = 37;
            this.button1.Text = "Кнопка 1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(112, 229);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 27);
            this.button2.TabIndex = 38;
            this.button2.Text = "Кнопка 2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SettingDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(477, 311);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.updateLLMArrayButton);
            this.Controls.Add(this.uriTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llmNameComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mlNetModelNameComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.hostErrorProvider)).EndInit();
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
		private System.Windows.Forms.TextBox uriTextBox;
		private System.Windows.Forms.Button updateLLMArrayButton;
		private System.Windows.Forms.ErrorProvider hostErrorProvider;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}