namespace WordHiddenPowers.Dialogs
{
    partial class NoteDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteDialog));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.wizardButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.categoriesComboBox = new WordHiddenPowers.Controls.ComboControls.CategoriesComboBox(this.components);
			this.subcategoriesComboBox = new WordHiddenPowers.Controls.ComboControls.SubcategoriesComboBox(this.components);
			this.ratingControl1 = new ControlLibrary.Controls.RatingControls.RatingControl(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			// 
			// descriptionTextBox
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.descriptionTextBox, 5);
			resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
			this.descriptionTextBox.Name = "descriptionTextBox";
			// 
			// wizardButton
			// 
			resources.ApplyResources(this.wizardButton, "wizardButton");
			this.wizardButton.Image = global::WordHiddenPowers.Properties.Resources.ColorMenu_24;
			this.wizardButton.Name = "wizardButton";
			this.wizardButton.Click += new System.EventHandler(this.WizardButton_Click);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.wizardButton, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.categoriesComboBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.subcategoriesComboBox, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cancelButton, 4, 4);
			this.tableLayoutPanel1.Controls.Add(this.okButton, 3, 4);
			this.tableLayoutPanel1.Controls.Add(this.ratingControl1, 1, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// categoriesComboBox
			// 
			this.categoriesComboBox.Code = "";
			this.tableLayoutPanel1.SetColumnSpan(this.categoriesComboBox, 5);
			resources.ApplyResources(this.categoriesComboBox, "categoriesComboBox");
			this.categoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.categoriesComboBox.DropDownHeight = 444;
			this.categoriesComboBox.DropDownWidth = 80;
			this.categoriesComboBox.FormattingEnabled = true;
			this.categoriesComboBox.Guid = "";
			this.categoriesComboBox.Id = ((long)(-1));
			this.categoriesComboBox.Name = "categoriesComboBox";
			this.categoriesComboBox.SelectedItem = null;
			this.categoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesComboBox_SelectedIndexChanged);
			// 
			// subcategoriesComboBox
			// 
			this.subcategoriesComboBox.Code = "";
			this.tableLayoutPanel1.SetColumnSpan(this.subcategoriesComboBox, 5);
			resources.ApplyResources(this.subcategoriesComboBox, "subcategoriesComboBox");
			this.subcategoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.subcategoriesComboBox.DropDownHeight = 444;
			this.subcategoriesComboBox.DropDownWidth = 80;
			this.subcategoriesComboBox.FormattingEnabled = true;
			this.subcategoriesComboBox.Guid = "";
			this.subcategoriesComboBox.Id = ((long)(-1));
			this.subcategoriesComboBox.Name = "subcategoriesComboBox";
			this.subcategoriesComboBox.SelectedItem = null;
			this.subcategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.SubcategoriesComboBox_SelectedIndexChanged);
			// 
			// ratingControl1
			// 
			resources.ApplyResources(this.ratingControl1, "ratingControl1");
			this.ratingControl1.Name = "ratingControl1";
			this.ratingControl1.StarsColor1 = System.Drawing.SystemColors.ControlText;
			this.ratingControl1.RatingChanged += new System.EventHandler<ControlLibrary.Controls.RatingControls.RatingEventArgs>(this.ratingControl1_RatingChanged);
			// 
			// NoteDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "NoteDialog";
			this.Load += new System.EventHandler(this.CreateNoteDialog_Load);
			this.Resize += new System.EventHandler(this.Dialog_Resize);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.ComboControls.CategoriesComboBox categoriesComboBox;
        private Controls.ComboControls.SubcategoriesComboBox subcategoriesComboBox;
		private System.Windows.Forms.Button wizardButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ControlLibrary.Controls.RatingControls.RatingControl ratingControl1;
	}
}