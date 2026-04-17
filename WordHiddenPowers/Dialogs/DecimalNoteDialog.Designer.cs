using ControlLibrary.Controls.TextControl;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecimalNoteDialog));
			this.numericTextBox1 = new ControlLibrary.Controls.TextControl.NumericTextBox(this.components);
			this.SuspendLayout();
			// 
			// numericTextBox1
			// 
			resources.ApplyResources(this.numericTextBox1, "numericTextBox1");
			this.numericTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.numericTextBox1.Name = "numericTextBox1";
			this.numericTextBox1.Value = 0D;
			this.numericTextBox1.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
			// 
			// DecimalNoteDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericTextBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DecimalNoteDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Controls.SetChildIndex(this.numericTextBox1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private NumericTextBox numericTextBox1;
    }
}