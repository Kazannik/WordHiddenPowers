namespace WordHiddenPowers.Controls
{
    partial class ReitingBox
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
            this.reitingTrackBar = new System.Windows.Forms.TrackBar();
            this.ratingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.reitingTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // reitingTrackBar
            // 
            this.reitingTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reitingTrackBar.Location = new System.Drawing.Point(32, 0);
            this.reitingTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reitingTrackBar.Name = "reitingTrackBar";
            this.reitingTrackBar.Size = new System.Drawing.Size(282, 45);
            this.reitingTrackBar.TabIndex = 0;
            this.reitingTrackBar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ratingLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ratingLabel.Location = new System.Drawing.Point(0, 6);
            this.ratingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(29, 20);
            this.ratingLabel.TabIndex = 1;
            this.ratingLabel.Text = "##";
            // 
            // ReitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ratingLabel);
            this.Controls.Add(this.reitingTrackBar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ReitingBox";
            this.Size = new System.Drawing.Size(319, 34);
            this.Resize += new System.EventHandler(this.ReitingBox_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.reitingTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar reitingTrackBar;
        private System.Windows.Forms.Label ratingLabel;
    }
}
