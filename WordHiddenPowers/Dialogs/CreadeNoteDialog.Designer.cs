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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.subcategoriesComboBox = new WordHiddenPowers.Controls.SubcategoriesComboBox(this.components);
			this.raitingBox = new WordHiddenPowers.Controls.ReitingBox();
			this.categoriesComboBox = new WordHiddenPowers.Controls.CategoriesComboBox(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(778, 385);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(87, 25);
			this.cancelButton.TabIndex = 28;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(682, 385);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(87, 25);
			this.okButton.TabIndex = 27;
			this.okButton.Text = "&ОК";
			// 
			// descriptionTextBox
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.descriptionTextBox, 3);
			this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptionTextBox.Location = new System.Drawing.Point(3, 310);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.descriptionTextBox.Size = new System.Drawing.Size(862, 63);
			this.descriptionTextBox.TabIndex = 31;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
			this.tableLayoutPanel1.Controls.Add(this.subcategoriesComboBox, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.raitingBox, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.okButton, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.cancelButton, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.categoriesComboBox, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(868, 422);
			this.tableLayoutPanel1.TabIndex = 35;
			// 
			// subcategoriesComboBox
			// 
			this.subcategoriesComboBox.Code = "";
			this.subcategoriesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.subcategoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.subcategoriesComboBox.DropDownHeight = 524;
			this.subcategoriesComboBox.DropDownWidth = 80;
			this.subcategoriesComboBox.FormattingEnabled = true;
			this.subcategoriesComboBox.Id = ((long)(-1));
			this.subcategoriesComboBox.IntegralHeight = false;
			this.subcategoriesComboBox.ItemHeight = 26;
			this.subcategoriesComboBox.Location = new System.Drawing.Point(3, 33);
			this.subcategoriesComboBox.MaxDropDownItems = 20;
			this.subcategoriesComboBox.Name = "subcategoriesComboBox";
			this.subcategoriesComboBox.SelectedItem = null;
			this.subcategoriesComboBox.Size = new System.Drawing.Size(670, 32);
			this.subcategoriesComboBox.TabIndex = 34;
			this.subcategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.SubcategoriesComboBox_SelectedIndexChanged);
			// 
			// raitingBox
			// 
			this.raitingBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.raitingBox.Location = new System.Drawing.Point(2, 378);
			this.raitingBox.Margin = new System.Windows.Forms.Padding(2);
			this.raitingBox.Name = "raitingBox";
			this.raitingBox.Size = new System.Drawing.Size(1008, 42);
			this.raitingBox.TabIndex = 32;
			this.raitingBox.Value = 0;
			// 
			// categoriesComboBox
			// 
			this.categoriesComboBox.Code = "";
			this.categoriesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.categoriesComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.categoriesComboBox.DropDownHeight = 524;
			this.categoriesComboBox.DropDownWidth = 80;
			this.categoriesComboBox.FormattingEnabled = true;
			this.categoriesComboBox.Id = ((long)(-1));
			this.categoriesComboBox.IntegralHeight = false;
			this.categoriesComboBox.ItemHeight = 26;
			this.categoriesComboBox.Location = new System.Drawing.Point(3, 3);
			this.categoriesComboBox.MaxDropDownItems = 20;
			this.categoriesComboBox.Name = "categoriesComboBox";
			this.categoriesComboBox.SelectedItem = null;
			this.categoriesComboBox.Size = new System.Drawing.Size(670, 32);
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
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "CreateNoteDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Текстовые данные";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private Controls.ReitingBox raitingBox;
        private Controls.CategoriesComboBox categoriesComboBox;
        private Controls.SubcategoriesComboBox subcategoriesComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}