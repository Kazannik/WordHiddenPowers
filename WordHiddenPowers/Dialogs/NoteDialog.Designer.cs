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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.subcategoriesComboBox = new WordHiddenPowers.Controls.ComboControls.SubcategoriesComboBox(this.components);
            this.ratingBox = new WordHiddenPowers.Controls.RatingBox();
            this.categoriesComboBox = new WordHiddenPowers.Controls.ComboControls.CategoriesComboBox(this.components);
            this.wizardButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelButton.Location = new System.Drawing.Point(468, 388);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(108, 31);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.okButton.Location = new System.Drawing.Point(354, 388);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(108, 31);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&ОК";
            // 
            // descriptionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.descriptionTextBox, 4);
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 236);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(573, 146);
            this.descriptionTextBox.TabIndex = 31;
            // 
            // subcategoriesComboBox
            // 
            this.subcategoriesComboBox.Code = "";
            this.tableLayoutPanel1.SetColumnSpan(this.subcategoriesComboBox, 4);
            this.subcategoriesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subcategoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.subcategoriesComboBox.DropDownHeight = 444;
            this.subcategoriesComboBox.DropDownWidth = 80;
            this.subcategoriesComboBox.FormattingEnabled = true;
            this.subcategoriesComboBox.Guid = "";
            this.subcategoriesComboBox.Id = ((long)(-1));
            this.subcategoriesComboBox.IntegralHeight = false;
            this.subcategoriesComboBox.ItemHeight = 22;
            this.subcategoriesComboBox.Location = new System.Drawing.Point(3, 35);
            this.subcategoriesComboBox.MaxDropDownItems = 20;
            this.subcategoriesComboBox.Name = "subcategoriesComboBox";
            this.subcategoriesComboBox.SelectedItem = null;
            this.subcategoriesComboBox.Size = new System.Drawing.Size(573, 28);
            this.subcategoriesComboBox.TabIndex = 34;
            this.subcategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.SubcategoriesComboBox_SelectedIndexChanged);
            // 
            // ratingBox
            // 
            this.ratingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ratingBox.Location = new System.Drawing.Point(52, 387);
            this.ratingBox.Margin = new System.Windows.Forms.Padding(2);
            this.ratingBox.Name = "ratingBox";
            this.ratingBox.Size = new System.Drawing.Size(297, 42);
            this.ratingBox.TabIndex = 32;
            this.ratingBox.Value = 0;
            // 
            // categoriesComboBox
            // 
            this.categoriesComboBox.Code = "";
            this.tableLayoutPanel1.SetColumnSpan(this.categoriesComboBox, 4);
            this.categoriesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoriesComboBox.DropDownHeight = 444;
            this.categoriesComboBox.DropDownWidth = 80;
            this.categoriesComboBox.FormattingEnabled = true;
            this.categoriesComboBox.Guid = "";
            this.categoriesComboBox.Id = ((long)(-1));
            this.categoriesComboBox.IntegralHeight = false;
            this.categoriesComboBox.ItemHeight = 22;
            this.categoriesComboBox.Location = new System.Drawing.Point(3, 3);
            this.categoriesComboBox.MaxDropDownItems = 20;
            this.categoriesComboBox.Name = "categoriesComboBox";
            this.categoriesComboBox.SelectedItem = null;
            this.categoriesComboBox.Size = new System.Drawing.Size(573, 28);
            this.categoriesComboBox.TabIndex = 33;
            this.categoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesComboBox_SelectedIndexChanged);
            // 
            // wizardButton
            // 
            this.wizardButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardButton.Enabled = false;
            this.wizardButton.Image = global::WordHiddenPowers.Properties.Resources.ColorMenu_24;
            this.wizardButton.Location = new System.Drawing.Point(3, 388);
            this.wizardButton.Name = "wizardButton";
            this.wizardButton.Size = new System.Drawing.Size(44, 31);
            this.wizardButton.TabIndex = 35;
            this.wizardButton.Click += new System.EventHandler(this.WizardButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.okButton, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.ratingBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.wizardButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.categoriesComboBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.subcategoriesComboBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.36842F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 422);
            this.tableLayoutPanel1.TabIndex = 36;
            // 
            // NoteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(579, 422);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "NoteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Данные";
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
        private Controls.RatingBox ratingBox;
        private Controls.ComboControls.CategoriesComboBox categoriesComboBox;
        private Controls.ComboControls.SubcategoriesComboBox subcategoriesComboBox;
		private System.Windows.Forms.Button wizardButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}