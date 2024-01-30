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
            this.raitingBox = new WordHiddenPowers.Controls.ReitingBox();
            this.categoriesComboBox = new WordHiddenPowers.Controls.CategoriesComboBox(this.components);
            this.subcategoriesComboBox = new WordHiddenPowers.Controls.SubcategoriesComboBox(this.components);
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(547, 372);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 27);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(437, 372);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 27);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&ОК";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(16, 294);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(631, 67);
            this.descriptionTextBox.TabIndex = 31;
            // 
            // raitingBox
            // 
            this.raitingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.raitingBox.Location = new System.Drawing.Point(16, 366);
            this.raitingBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.raitingBox.Name = "raitingBox";
            this.raitingBox.Size = new System.Drawing.Size(409, 42);
            this.raitingBox.TabIndex = 32;
            this.raitingBox.Value = 0;
            // 
            // categoriesComboBox
            // 
            this.categoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoriesComboBox.DropDownHeight = 164;
            this.categoriesComboBox.DropDownWidth = 80;
            this.categoriesComboBox.FormattingEnabled = true;
            this.categoriesComboBox.IntegralHeight = false;
            this.categoriesComboBox.ItemHeight = 20;
            this.categoriesComboBox.Location = new System.Drawing.Point(16, 9);
            this.categoriesComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.categoriesComboBox.MaxDropDownItems = 20;
            this.categoriesComboBox.Name = "categoriesComboBox";
            this.categoriesComboBox.Size = new System.Drawing.Size(345, 26);
            this.categoriesComboBox.TabIndex = 33;
            // 
            // subcategoriesComboBox
            // 
            this.subcategoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.subcategoriesComboBox.DropDownHeight = 164;
            this.subcategoriesComboBox.DropDownWidth = 80;
            this.subcategoriesComboBox.FormattingEnabled = true;
            this.subcategoriesComboBox.IntegralHeight = false;
            this.subcategoriesComboBox.ItemHeight = 20;
            this.subcategoriesComboBox.Location = new System.Drawing.Point(16, 44);
            this.subcategoriesComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.subcategoriesComboBox.MaxDropDownItems = 20;
            this.subcategoriesComboBox.Name = "subcategoriesComboBox";
            this.subcategoriesComboBox.Size = new System.Drawing.Size(345, 26);
            this.subcategoriesComboBox.TabIndex = 34;
            // 
            // CreateNoteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(661, 414);
            this.Controls.Add(this.subcategoriesComboBox);
            this.Controls.Add(this.categoriesComboBox);
            this.Controls.Add(this.raitingBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CreateNoteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Текстовые данные";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.ReitingBox raitingBox;
        private Controls.CategoriesComboBox categoriesComboBox;
        private Controls.SubcategoriesComboBox subcategoriesComboBox;
    }
}