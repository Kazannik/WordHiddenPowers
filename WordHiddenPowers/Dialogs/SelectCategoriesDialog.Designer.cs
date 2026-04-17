namespace WordHiddenPowers.Dialogs
{
	partial class SelectCategoriesDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCategoriesDialog));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.checkedListBox1 = new WordHiddenPowers.Controls.ListControls.CategoriesCheckedListBox();
			this.checkButton = new System.Windows.Forms.Button();
			this.unckeckButton = new System.Windows.Forms.Button();
			this.inverseButton = new System.Windows.Forms.Button();
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
			// checkedListBox1
			// 
			resources.ApplyResources(this.checkedListBox1, "checkedListBox1");
			this.checkedListBox1.DataSet = null;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CheckedListBox1_ItemContentChanged);
			// 
			// checkButton
			// 
			resources.ApplyResources(this.checkButton, "checkButton");
			this.checkButton.Name = "checkButton";
			this.checkButton.Click += new System.EventHandler(this.CheckButton_Click);
			// 
			// unckeckButton
			// 
			resources.ApplyResources(this.unckeckButton, "unckeckButton");
			this.unckeckButton.Name = "unckeckButton";
			this.unckeckButton.Click += new System.EventHandler(this.UnckeckButton_Click);
			// 
			// inverseButton
			// 
			resources.ApplyResources(this.inverseButton, "inverseButton");
			this.inverseButton.Name = "inverseButton";
			this.inverseButton.Click += new System.EventHandler(this.InverseButton_Click);
			// 
			// SelectCategoriesDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.inverseButton);
			this.Controls.Add(this.unckeckButton);
			this.Controls.Add(this.checkButton);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Name = "SelectCategoriesDialog";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private WordHiddenPowers.Controls.ListControls.CategoriesCheckedListBox checkedListBox1;
		private System.Windows.Forms.Button checkButton;
		private System.Windows.Forms.Button unckeckButton;
		private System.Windows.Forms.Button inverseButton;
	}
}