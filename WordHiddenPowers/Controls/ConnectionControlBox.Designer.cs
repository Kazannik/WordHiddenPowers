namespace WordHiddenPowers.Controls
{
	partial class ConnectionControlBox
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
			this.components = new System.ComponentModel.Container();
			this.addressLabel = new System.Windows.Forms.Label();
			this.addressTextBox = new System.Windows.Forms.TextBox();
			this.addressChangedTimer = new System.Windows.Forms.Timer(this.components);
			this.moreButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// addressLabel
			// 
			this.addressLabel.AutoSize = true;
			this.addressLabel.Location = new System.Drawing.Point(0, 0);
			this.addressLabel.Margin = new System.Windows.Forms.Padding(0);
			this.addressLabel.Name = "addressLabel";
			this.addressLabel.Size = new System.Drawing.Size(104, 16);
			this.addressLabel.TabIndex = 0;
			this.addressLabel.Text = "Server Address:";
			// 
			// addressTextBox
			// 
			this.addressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.addressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.addressTextBox.Location = new System.Drawing.Point(108, 0);
			this.addressTextBox.Name = "addressTextBox";
			this.addressTextBox.Size = new System.Drawing.Size(534, 22);
			this.addressTextBox.TabIndex = 1;
			this.addressTextBox.TextChanged += new System.EventHandler(this.AddressTextBox_TextChanged);
			this.addressTextBox.Enter += new System.EventHandler(this.Component_Enter);
			// 
			// addressChangedTimer
			// 
			this.addressChangedTimer.Tick += new System.EventHandler(this.AddressChangedTimer_Tick);
			// 
			// moreButton
			// 
			this.moreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moreButton.Image = global::WordHiddenPowers.Properties.Resources.More_16;
			this.moreButton.Location = new System.Drawing.Point(624, 30);
			this.moreButton.Name = "moreButton";
			this.moreButton.Size = new System.Drawing.Size(18, 18);
			this.moreButton.TabIndex = 2;
			this.moreButton.UseVisualStyleBackColor = true;
			this.moreButton.Click += new System.EventHandler(this.MoreButton_Click);
			// 
			// ConnectionControlBox
			// 
			this.Controls.Add(this.addressLabel);
			this.Controls.Add(this.addressTextBox);
			this.Controls.Add(this.moreButton);
			this.Size = new System.Drawing.Size(645, 51);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		
		#endregion

		private System.Windows.Forms.Label addressLabel;
		private System.Windows.Forms.TextBox addressTextBox;
		private System.Windows.Forms.Timer addressChangedTimer;
		private System.Windows.Forms.Button moreButton;
	}
}
