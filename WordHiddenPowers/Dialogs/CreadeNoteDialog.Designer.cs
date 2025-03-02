namespace WordHiddenPowers.Dialogs
{
    partial class CreateNoteDialog
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
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(750, 376);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(106, 34);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(638, 376);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(106, 34);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&ОК";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 296);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(844, 64);
            this.descriptionTextBox.TabIndex = 31;
            // 
            // subcategoriesComboBox
            // 
            this.subcategoriesComboBox.Code = "";
            this.subcategoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.subcategoriesComboBox.DropDownHeight = 524;
            this.subcategoriesComboBox.DropDownWidth = 80;
            this.subcategoriesComboBox.FormattingEnabled = true;
            this.subcategoriesComboBox.Id = ((long)(-1));
            this.subcategoriesComboBox.IntegralHeight = false;
            this.subcategoriesComboBox.ItemHeight = 26;
            this.subcategoriesComboBox.Location = new System.Drawing.Point(12, 50);
            this.subcategoriesComboBox.MaxDropDownItems = 20;
            this.subcategoriesComboBox.Name = "subcategoriesComboBox";
            this.subcategoriesComboBox.SelectedItem = null;
            this.subcategoriesComboBox.Size = new System.Drawing.Size(443, 32);
            this.subcategoriesComboBox.TabIndex = 34;
            this.subcategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.SubcategoriesComboBox_SelectedIndexChanged);
            // 
            // ratingBox
            // 
            this.ratingBox.Location = new System.Drawing.Point(12, 368);
            this.ratingBox.Margin = new System.Windows.Forms.Padding(2);
            this.ratingBox.Name = "ratingBox";
            this.ratingBox.Size = new System.Drawing.Size(310, 42);
            this.ratingBox.TabIndex = 32;
            this.ratingBox.Value = 0;
            // 
            // categoriesComboBox
            // 
            this.categoriesComboBox.Code = "";
            this.categoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoriesComboBox.DropDownHeight = 524;
            this.categoriesComboBox.DropDownWidth = 80;
            this.categoriesComboBox.FormattingEnabled = true;
            this.categoriesComboBox.Id = ((long)(-1));
            this.categoriesComboBox.IntegralHeight = false;
            this.categoriesComboBox.ItemHeight = 26;
            this.categoriesComboBox.Location = new System.Drawing.Point(12, 12);
            this.categoriesComboBox.MaxDropDownItems = 20;
            this.categoriesComboBox.Name = "categoriesComboBox";
            this.categoriesComboBox.SelectedItem = null;
            this.categoriesComboBox.Size = new System.Drawing.Size(443, 32);
            this.categoriesComboBox.TabIndex = 33;
            this.categoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesComboBox_SelectedIndexChanged);
            // 
            // CreateNoteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(868, 422);
            this.Controls.Add(this.subcategoriesComboBox);
            this.Controls.Add(this.categoriesComboBox);
            this.Controls.Add(this.ratingBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CreateNoteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Текстовые данные";
            this.Resize += new System.EventHandler(this.Dialog_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.RatingBox ratingBox;
        private Controls.ComboControls.CategoriesComboBox categoriesComboBox;
        private Controls.ComboControls.SubcategoriesComboBox subcategoriesComboBox;
    }
}