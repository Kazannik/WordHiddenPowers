namespace WordHiddenPowers.Controls
{
	partial class LLMConnectionControlBox
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
			this.connectionBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.serverAddressLabel = new System.Windows.Forms.Label();
			this.serverAddressTextBox = new System.Windows.Forms.TextBox();
			this.textChangedTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// connectionBackgroundWorker
			// 
			this.connectionBackgroundWorker.WorkerReportsProgress = true;
			this.connectionBackgroundWorker.WorkerSupportsCancellation = true;
			this.connectionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConnectionBackgroundWorker_DoWork);
			this.connectionBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ConnectionBackgroundWorker_ProgressChanged);
			this.connectionBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConnectionBackgroundWorker_RunWorkerCompleted);
			// 
			// serverAddressLabel
			// 
			this.serverAddressLabel.AutoSize = true;
			this.serverAddressLabel.Location = new System.Drawing.Point(0, 0);
			this.serverAddressLabel.Name = "serverAddressLabel";
			this.serverAddressLabel.Size = new System.Drawing.Size(104, 16);
			this.serverAddressLabel.TabIndex = 0;
			this.serverAddressLabel.Text = "Server Address:";
			this.serverAddressLabel.Resize += new System.EventHandler(this.AddressLabel_Resize);
			// 
			// serverAddressTextBox
			// 
			this.serverAddressTextBox.Location = new System.Drawing.Point(120, 0);
			this.serverAddressTextBox.Name = "serverAddressTextBox";
			this.serverAddressTextBox.Size = new System.Drawing.Size(528, 22);
			this.serverAddressTextBox.TabIndex = 1;
			this.serverAddressTextBox.TextChanged += new System.EventHandler(this.ServerAddressTextBox_TextChanged);
			// 
			// textChangedTimer
			// 
			this.textChangedTimer.Tick += new System.EventHandler(this.TextChangedTimer_Tick);
			// 
			// LLMConnectionControlBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.serverAddressTextBox);
			this.Controls.Add(this.serverAddressLabel);
			this.Name = "LLMConnectionControlBox";
			this.Size = new System.Drawing.Size(645, 72);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.ComponentModel.BackgroundWorker connectionBackgroundWorker;
		private System.Windows.Forms.Label serverAddressLabel;
		private System.Windows.Forms.TextBox serverAddressTextBox;
		private System.Windows.Forms.Timer textChangedTimer;
	}
}
