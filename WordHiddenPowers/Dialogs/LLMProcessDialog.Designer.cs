namespace WordHiddenPowers.Dialogs
{
	partial class LLMProcessDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LLMProcessDialog));
			this.iconPictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// iconPictureBox
			// 
			resources.ApplyResources(this.iconPictureBox, "iconPictureBox");
			this.iconPictureBox.Image = global::WordHiddenPowers.Properties.Resources.ai_process;
			this.iconPictureBox.Name = "iconPictureBox";
			this.iconPictureBox.TabStop = false;
			this.iconPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseDown);
			this.iconPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseMove);
			this.iconPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseUp);
			// 
			// LLMProcessDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.iconPictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LLMProcessDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dialog_MouseUp);
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox iconPictureBox;
	}
}