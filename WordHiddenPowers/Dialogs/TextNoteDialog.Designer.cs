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
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // valueTextBox
            // 
            this.valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.valueTextBox.Location = new System.Drawing.Point(5, 69);
            this.valueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.valueTextBox.Multiline = true;
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.valueTextBox.Size = new System.Drawing.Size(859, 235);
            this.valueTextBox.TabIndex = 29;
            // 
            // TextNoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(868, 422);
            this.Controls.Add(this.valueTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TextNoteDialog";
            this.Controls.SetChildIndex(this.valueTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox valueTextBox;
    }
}