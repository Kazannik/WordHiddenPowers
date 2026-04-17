namespace WordHiddenPowers.Controls
{
	partial class SendMessageButtonsBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMessageButtonsBox));
			this.insertButton = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.replaceButton = new System.Windows.Forms.Button();
			this.insertNextButton = new System.Windows.Forms.Button();
			this.insertPreviousButton = new System.Windows.Forms.Button();
			this.insertCenterButton = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// insertButton
			// 
			this.insertButton.ImageIndex = 0;
			this.insertButton.ImageList = this.imageList1;
			this.insertButton.Location = new System.Drawing.Point(324, 0);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(120, 48);
			this.insertButton.TabIndex = 0;
			this.insertButton.Text = "Отправить";
			this.insertButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.toolTip1.SetToolTip(this.insertButton, "Вставить ответ в выбранную позицию");
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.InsertButton_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "ChartType01");
			this.imageList1.Images.SetKeyName(1, "ChartType02");
			this.imageList1.Images.SetKeyName(2, "ChartType03");
			this.imageList1.Images.SetKeyName(3, "ChartType04");
			this.imageList1.Images.SetKeyName(4, "ChartType05");
			// 
			// replaceButton
			// 
			this.replaceButton.ImageIndex = 1;
			this.replaceButton.ImageList = this.imageList1;
			this.replaceButton.Location = new System.Drawing.Point(204, 0);
			this.replaceButton.Name = "replaceButton";
			this.replaceButton.Size = new System.Drawing.Size(120, 48);
			this.replaceButton.TabIndex = 1;
			this.replaceButton.Text = "Отправить";
			this.replaceButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.toolTip1.SetToolTip(this.replaceButton, "Заменить выделенный текст на ответ");
			this.replaceButton.UseVisualStyleBackColor = true;
			this.replaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// insertNextButton
			// 
			this.insertNextButton.ImageIndex = 2;
			this.insertNextButton.ImageList = this.imageList1;
			this.insertNextButton.Location = new System.Drawing.Point(156, 0);
			this.insertNextButton.Name = "insertNextButton";
			this.insertNextButton.Size = new System.Drawing.Size(48, 48);
			this.insertNextButton.TabIndex = 2;
			this.toolTip1.SetToolTip(this.insertNextButton, "Вставить ответ ниже выделенного текста");
			this.insertNextButton.UseVisualStyleBackColor = true;
			this.insertNextButton.Click += new System.EventHandler(this.InsertNextButton_Click);
			// 
			// insertPreviousButton
			// 
			this.insertPreviousButton.ImageIndex = 3;
			this.insertPreviousButton.ImageList = this.imageList1;
			this.insertPreviousButton.Location = new System.Drawing.Point(108, 0);
			this.insertPreviousButton.Name = "insertPreviousButton";
			this.insertPreviousButton.Size = new System.Drawing.Size(48, 48);
			this.insertPreviousButton.TabIndex = 3;
			this.toolTip1.SetToolTip(this.insertPreviousButton, "Вставить ответ выше выделенного текста");
			this.insertPreviousButton.UseVisualStyleBackColor = true;
			this.insertPreviousButton.Click += new System.EventHandler(this.InsertPreviousButton_Click);
			// 
			// insertCenterButton
			// 
			this.insertCenterButton.ImageIndex = 4;
			this.insertCenterButton.ImageList = this.imageList1;
			this.insertCenterButton.Location = new System.Drawing.Point(60, 0);
			this.insertCenterButton.Name = "insertCenterButton";
			this.insertCenterButton.Size = new System.Drawing.Size(48, 48);
			this.insertCenterButton.TabIndex = 4;
			this.toolTip1.SetToolTip(this.insertCenterButton, "Вставить ответ в середину выделенного текста");
			this.insertCenterButton.UseVisualStyleBackColor = true;
			this.insertCenterButton.Click += new System.EventHandler(this.InsertCenterButton_Click);
			// 
			// SendMessageButtonsBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.insertCenterButton);
			this.Controls.Add(this.insertPreviousButton);
			this.Controls.Add(this.insertNextButton);
			this.Controls.Add(this.replaceButton);
			this.Controls.Add(this.insertButton);
			this.Name = "SendMessageButtonsBox";
			this.Size = new System.Drawing.Size(442, 52);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button replaceButton;
		private System.Windows.Forms.Button insertNextButton;
		private System.Windows.Forms.Button insertPreviousButton;
		private System.Windows.Forms.Button insertCenterButton;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
