namespace WordHiddenPowers.Dialogs
{
    partial class TextNoteDialog
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
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.raitingBox = new WordHiddenPowers.Controls.ReitingBox();
            this.categoriesComboBox1 = new WordHiddenPowers.Controls.CategoriesComboBox(this.components);
            this.subcategoriesComboBox1 = new WordHiddenPowers.Controls.SubcategoriesComboBox(this.components);
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(410, 302);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 22);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Отмена";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(328, 302);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 22);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&ОК";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.valueTextBox.Location = new System.Drawing.Point(12, 65);
            this.valueTextBox.Multiline = true;
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.valueTextBox.Size = new System.Drawing.Size(472, 171);
            this.valueTextBox.TabIndex = 29;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 239);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(472, 55);
            this.descriptionTextBox.TabIndex = 31;
            // 
            // raitingBox
            // 
            this.raitingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.raitingBox.Location = new System.Drawing.Point(12, 297);
            this.raitingBox.Margin = new System.Windows.Forms.Padding(2);
            this.raitingBox.Name = "raitingBox";
            this.raitingBox.Size = new System.Drawing.Size(307, 42);
            this.raitingBox.TabIndex = 32;
            this.raitingBox.Value = 0;
            // 
            // categoriesComboBox1
            // 
            this.categoriesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoriesComboBox1.DropDownHeight = 140;
            this.categoriesComboBox1.DropDownWidth = 80;
            this.categoriesComboBox1.FormattingEnabled = true;
            this.categoriesComboBox1.IntegralHeight = false;
            this.categoriesComboBox1.ItemHeight = 17;
            this.categoriesComboBox1.Location = new System.Drawing.Point(12, 7);
            this.categoriesComboBox1.MaxDropDownItems = 20;
            this.categoriesComboBox1.Name = "categoriesComboBox1";
            this.categoriesComboBox1.Size = new System.Drawing.Size(260, 23);
            this.categoriesComboBox1.TabIndex = 33;
            // 
            // subcategoriesComboBox1
            // 
            this.subcategoriesComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.subcategoriesComboBox1.DropDownHeight = 140;
            this.subcategoriesComboBox1.DropDownWidth = 80;
            this.subcategoriesComboBox1.FormattingEnabled = true;
            this.subcategoriesComboBox1.IntegralHeight = false;
            this.subcategoriesComboBox1.ItemHeight = 17;
            this.subcategoriesComboBox1.Location = new System.Drawing.Point(12, 36);
            this.subcategoriesComboBox1.MaxDropDownItems = 20;
            this.subcategoriesComboBox1.Name = "subcategoriesComboBox1";
            this.subcategoriesComboBox1.Size = new System.Drawing.Size(260, 23);
            this.subcategoriesComboBox1.TabIndex = 34;
            // 
            // TextNoteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(496, 336);
            this.Controls.Add(this.subcategoriesComboBox1);
            this.Controls.Add(this.categoriesComboBox1);
            this.Controls.Add(this.raitingBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TextNoteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Текстовые данные";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.ReitingBox raitingBox;
        private Controls.CategoriesComboBox categoriesComboBox1;
        private Controls.SubcategoriesComboBox subcategoriesComboBox1;
    }
}