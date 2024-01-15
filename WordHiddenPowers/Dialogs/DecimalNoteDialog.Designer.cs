namespace WordHiddenPowers.Dialogs
{
    partial class DecimalNoteDialog
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
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.categoriesComboBox1 = new WordHiddenPowers.Controls.CategoriesComboBox();
            this.numericTextBox1 = new WordHiddenPowers.Controls.NumericTextBox();
            this.raitingBox = new WordHiddenPowers.Controls.ReitingBox();
            this.subcategoriesComboBox1 = new WordHiddenPowers.Controls.SubcategoriesComboBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(410, 140);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 22);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(328, 140);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 22);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&ОК";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 76);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(472, 55);
            this.descriptionTextBox.TabIndex = 31;
            // 
            // categoriesComboBox1
            // 
            this.categoriesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoriesComboBox1.DropDownHeight = 140;
            this.categoriesComboBox1.DropDownWidth = 80;
            this.categoriesComboBox1.FormattingEnabled = true;
            this.categoriesComboBox1.IntegralHeight = false;
            this.categoriesComboBox1.ItemHeight = 17;
            this.categoriesComboBox1.Location = new System.Drawing.Point(12, 12);
            this.categoriesComboBox1.MaxDropDownItems = 20;
            this.categoriesComboBox1.Name = "categoriesComboBox1";
            this.categoriesComboBox1.Size = new System.Drawing.Size(260, 23);
            this.categoriesComboBox1.TabIndex = 34;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.numericTextBox1.Location = new System.Drawing.Point(278, 21);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(206, 29);
            this.numericTextBox1.TabIndex = 33;
            this.numericTextBox1.Text = "0";
            this.numericTextBox1.Value = 0D;
            this.numericTextBox1.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // raitingBox
            // 
            this.raitingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.raitingBox.Location = new System.Drawing.Point(12, 135);
            this.raitingBox.Margin = new System.Windows.Forms.Padding(2);
            this.raitingBox.Name = "raitingBox";
            this.raitingBox.Size = new System.Drawing.Size(307, 42);
            this.raitingBox.TabIndex = 32;
            this.raitingBox.Value = 0;
            // 
            // subcategoriesComboBox1
            // 
            this.subcategoriesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.subcategoriesComboBox1.DropDownHeight = 140;
            this.subcategoriesComboBox1.DropDownWidth = 80;
            this.subcategoriesComboBox1.FormattingEnabled = true;
            this.subcategoriesComboBox1.IntegralHeight = false;
            this.subcategoriesComboBox1.ItemHeight = 17;
            this.subcategoriesComboBox1.Location = new System.Drawing.Point(13, 42);
            this.subcategoriesComboBox1.MaxDropDownItems = 20;
            this.subcategoriesComboBox1.Name = "subcategoriesComboBox1";
            this.subcategoriesComboBox1.Size = new System.Drawing.Size(259, 23);
            this.subcategoriesComboBox1.TabIndex = 35;
            // 
            // DecimalNoteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(496, 173);
            this.Controls.Add(this.subcategoriesComboBox1);
            this.Controls.Add(this.categoriesComboBox1);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.raitingBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DecimalNoteDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Числовые данные";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.ReitingBox raitingBox;
        private Controls.NumericTextBox numericTextBox1;
        private Controls.CategoriesComboBox categoriesComboBox1;
        private Controls.SubcategoriesComboBox subcategoriesComboBox1;
    }
}