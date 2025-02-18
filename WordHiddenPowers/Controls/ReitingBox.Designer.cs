namespace WordHiddenPowers.Controls
{
    partial class RatingBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.ratingTrackBar = new System.Windows.Forms.TrackBar();
			this.ratingLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ratingTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// ratingTrackBar
			// 
			this.ratingTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ratingTrackBar.LargeChange = 1;
			this.ratingTrackBar.Location = new System.Drawing.Point(52, 2);
			this.ratingTrackBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ratingTrackBar.Maximum = 5;
			this.ratingTrackBar.Minimum = -5;
			this.ratingTrackBar.Name = "ratingTrackBar";
			this.ratingTrackBar.Size = new System.Drawing.Size(423, 69);
			this.ratingTrackBar.TabIndex = 0;
			this.ratingTrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
			// 
			// ratingLabel
			// 
			this.ratingLabel.AutoSize = true;
			this.ratingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ratingLabel.ForeColor = System.Drawing.SystemColors.Highlight;
			this.ratingLabel.Location = new System.Drawing.Point(0, 9);
			this.ratingLabel.Name = "ratingLabel";
			this.ratingLabel.Size = new System.Drawing.Size(41, 29);
			this.ratingLabel.TabIndex = 1;
			this.ratingLabel.Text = "##";
			// 
			// RatingBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ratingLabel);
			this.Controls.Add(this.ratingTrackBar);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "RatingBox";
			this.Size = new System.Drawing.Size(478, 52);
			this.Resize += new System.EventHandler(this.RatingBox_Resize);
			((System.ComponentModel.ISupportInitialize)(this.ratingTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar ratingTrackBar;
        private System.Windows.Forms.Label ratingLabel;
    }
}
