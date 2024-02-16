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
            this.components = new System.ComponentModel.Container();
            this.numericTextBox1 = new WordHiddenPowers.Controls.NumericTextBox(this.components);
            this.SuspendLayout();
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.numericTextBox1.Location = new System.Drawing.Point(943, 18);
            this.numericTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(158, 34);
            this.numericTextBox1.TabIndex = 33;
            this.numericTextBox1.Text = "0";
            this.numericTextBox1.Value = 0D;
            this.numericTextBox1.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // DecimalNoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 222);
            this.Controls.Add(this.numericTextBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DecimalNoteDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Числовые данные";
            this.Controls.SetChildIndex(this.numericTextBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.NumericTextBox numericTextBox1;
    }
}