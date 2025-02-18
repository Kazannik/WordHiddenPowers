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
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(675, 402);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(112, 34);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(555, 402);
			this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(112, 34);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "&ОК";
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox1.DataSet = null;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(12, 12);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(655, 369);
			this.checkedListBox1.TabIndex = 0;
			this.checkedListBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CheckedListBox1_ItemContentChanged);
			// 
			// checkButton
			// 
			this.checkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkButton.Location = new System.Drawing.Point(675, 12);
			this.checkButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkButton.Name = "checkButton";
			this.checkButton.Size = new System.Drawing.Size(112, 34);
			this.checkButton.TabIndex = 1;
			this.checkButton.Text = "Отметить";
			this.checkButton.Click += new System.EventHandler(this.CheckButton_Click);
			// 
			// unckeckButton
			// 
			this.unckeckButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.unckeckButton.Location = new System.Drawing.Point(675, 56);
			this.unckeckButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.unckeckButton.Name = "unckeckButton";
			this.unckeckButton.Size = new System.Drawing.Size(112, 34);
			this.unckeckButton.TabIndex = 2;
			this.unckeckButton.Text = "Снять";
			this.unckeckButton.Click += new System.EventHandler(this.UnckeckButton_Click);
			// 
			// inverseButton
			// 
			this.inverseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.inverseButton.Location = new System.Drawing.Point(675, 100);
			this.inverseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.inverseButton.Name = "inverseButton";
			this.inverseButton.Size = new System.Drawing.Size(112, 34);
			this.inverseButton.TabIndex = 3;
			this.inverseButton.Text = "Изменить";
			this.inverseButton.Click += new System.EventHandler(this.InverseButton_Click);
			// 
			// SelectCategoriesDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.inverseButton);
			this.Controls.Add(this.unckeckButton);
			this.Controls.Add(this.checkButton);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Name = "SelectCategoriesDialog";
			this.Text = "SelectCategoriesDialog";
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