namespace WordHiddenPowers.Dialogs
{
	partial class LLMChatDialog
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
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// messageTextBox
			// 
			this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.messageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.messageTextBox.Location = new System.Drawing.Point(5, 73);
			this.messageTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.Size = new System.Drawing.Size(517, 30);
			this.messageTextBox.TabIndex = 0;
			// 
			// sendButton
			// 
			this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.sendButton.Location = new System.Drawing.Point(530, 73);
			this.sendButton.Margin = new System.Windows.Forms.Padding(4);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(222, 30);
			this.sendButton.TabIndex = 1;
			this.sendButton.Text = "Отправить";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.BackColor = System.Drawing.Color.Coral;
			this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.closeButton.Location = new System.Drawing.Point(709, 3);
			this.closeButton.Margin = new System.Windows.Forms.Padding(4);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(43, 31);
			this.closeButton.TabIndex = 2;
			this.closeButton.UseVisualStyleBackColor = false;
			this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// AiChatDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(758, 107);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.messageTextBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AiChatDialog";
			this.Opacity = 0.8D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Copilot";
			this.TopMost = true;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Button closeButton;
	}
}