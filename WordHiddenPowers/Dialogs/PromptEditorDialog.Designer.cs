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
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// captionTextBox
			// 
			resources.ApplyResources(this.captionTextBox, "captionTextBox");
			this.captionTextBox.Name = "captionTextBox";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// systemMessageTextBox
			// 
			resources.ApplyResources(this.systemMessageTextBox, "systemMessageTextBox");
			this.systemMessageTextBox.Name = "systemMessageTextBox";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// prefixUserMessageTextBox
			// 
			resources.ApplyResources(this.prefixUserMessageTextBox, "prefixUserMessageTextBox");
			this.prefixUserMessageTextBox.Name = "prefixUserMessageTextBox";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// postfixUserMessageTextBox
			// 
			resources.ApplyResources(this.postfixUserMessageTextBox, "postfixUserMessageTextBox");
			this.postfixUserMessageTextBox.Name = "postfixUserMessageTextBox";
			// 
			// PromptEditorDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
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
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PromptEditorDialog";
			this.ShowInTaskbar = false;
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