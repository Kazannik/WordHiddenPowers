namespace WordHiddenPowers.Controls.SendMessagesControl
{
	partial class LLMSendMessageBox
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
			this.toolStripBar = new System.Windows.Forms.ToolStrip();
			this.insertHereButton = new System.Windows.Forms.ToolStripButton();
			this.replaceSelectionButton = new System.Windows.Forms.ToolStripButton();
			this.insertNextButton = new System.Windows.Forms.ToolStripButton();
			this.insertPreviousButton = new System.Windows.Forms.ToolStripButton();
			this.insertBetweenButton = new System.Windows.Forms.ToolStripButton();
			this.repeatButton = new System.Windows.Forms.ToolStripButton();
			this.redoButton = new System.Windows.Forms.ToolStripButton();
			this.undoButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripBar
			// 
			this.toolStripBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStripBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripBar.ImageScalingSize = new System.Drawing.Size(46, 46);
			this.toolStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertHereButton,
            this.replaceSelectionButton,
            this.insertNextButton,
            this.insertPreviousButton,
            this.insertBetweenButton,
            this.repeatButton,
            this.redoButton,
            this.undoButton});
			this.toolStripBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStripBar.Location = new System.Drawing.Point(0, 0);
			this.toolStripBar.Name = "toolStripBar";
			this.toolStripBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripBar.Size = new System.Drawing.Size(491, 47);
			this.toolStripBar.Stretch = true;
			this.toolStripBar.TabIndex = 5;
			this.toolStripBar.Text = "Стандартная панель";
			// 
			// insertHereButton
			// 
			this.insertHereButton.Image = global::WordHiddenPowers.Properties.Resources.ChartType01;
			this.insertHereButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.insertHereButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.insertHereButton.Name = "insertHereButton";
			this.insertHereButton.Padding = new System.Windows.Forms.Padding(4);
			this.insertHereButton.Size = new System.Drawing.Size(44, 44);
			this.insertHereButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.insertHereButton.ToolTipText = "Вставить ответ в выбранную позицию";
			this.insertHereButton.Click += new System.EventHandler(this.InsertHereButton_Click);
			// 
			// replaceSelectionButton
			// 
			this.replaceSelectionButton.Image = global::WordHiddenPowers.Properties.Resources.ChartType02;
			this.replaceSelectionButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.replaceSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.replaceSelectionButton.Name = "replaceSelectionButton";
			this.replaceSelectionButton.Padding = new System.Windows.Forms.Padding(4);
			this.replaceSelectionButton.Size = new System.Drawing.Size(44, 44);
			this.replaceSelectionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.replaceSelectionButton.ToolTipText = "Заменить выделенный текст на ответ";
			this.replaceSelectionButton.Click += new System.EventHandler(this.ReplaceSelectionButton_Click);
			// 
			// insertNextButton
			// 
			this.insertNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.insertNextButton.Image = global::WordHiddenPowers.Properties.Resources.ChartType03;
			this.insertNextButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.insertNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.insertNextButton.Name = "insertNextButton";
			this.insertNextButton.Padding = new System.Windows.Forms.Padding(4);
			this.insertNextButton.Size = new System.Drawing.Size(44, 44);
			this.insertNextButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.insertNextButton.ToolTipText = "Вставить ответ ниже выделенного текста";
			this.insertNextButton.Click += new System.EventHandler(this.InsertNextButton_Click);
			// 
			// insertPreviousButton
			// 
			this.insertPreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.insertPreviousButton.Image = global::WordHiddenPowers.Properties.Resources.ChartType04;
			this.insertPreviousButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.insertPreviousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.insertPreviousButton.Name = "insertPreviousButton";
			this.insertPreviousButton.Padding = new System.Windows.Forms.Padding(4);
			this.insertPreviousButton.Size = new System.Drawing.Size(44, 44);
			this.insertPreviousButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.insertPreviousButton.ToolTipText = "Вставить ответ выше выделенного текста";
			this.insertPreviousButton.Click += new System.EventHandler(this.InsertPreviousButton_Click);
			// 
			// insertBetweenButton
			// 
			this.insertBetweenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.insertBetweenButton.Image = global::WordHiddenPowers.Properties.Resources.ChartType05;
			this.insertBetweenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.insertBetweenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.insertBetweenButton.Name = "insertBetweenButton";
			this.insertBetweenButton.Padding = new System.Windows.Forms.Padding(4);
			this.insertBetweenButton.Size = new System.Drawing.Size(44, 44);
			this.insertBetweenButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.insertBetweenButton.ToolTipText = "Вставить ответ в середину выделенного текста";
			this.insertBetweenButton.Click += new System.EventHandler(this.InsertCenterButton_Click);
			// 
			// repeatButton
			// 
			this.repeatButton.AutoSize = false;
			this.repeatButton.Image = global::WordHiddenPowers.Properties.Resources.Repeat;
			this.repeatButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.repeatButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.repeatButton.Name = "repeatButton";
			this.repeatButton.Padding = new System.Windows.Forms.Padding(4);
			this.repeatButton.Size = new System.Drawing.Size(128, 44);
			this.repeatButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.repeatButton.Click += new System.EventHandler(this.RepeatButton_Click);
			// 
			// redoButton
			// 
			this.redoButton.Image = global::WordHiddenPowers.Properties.Resources.Redo;
			this.redoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoButton.Name = "redoButton";
			this.redoButton.Padding = new System.Windows.Forms.Padding(4);
			this.redoButton.Size = new System.Drawing.Size(44, 44);
			this.redoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
			// 
			// undoButton
			// 
			this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoButton.Image = global::WordHiddenPowers.Properties.Resources.Undo;
			this.undoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoButton.Name = "undoButton";
			this.undoButton.Padding = new System.Windows.Forms.Padding(4);
			this.undoButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.undoButton.Size = new System.Drawing.Size(44, 44);
			this.undoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
			// 
			// LLMSendMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.toolStripBar);
			this.Name = "LLMSendMessageBox";
			this.Size = new System.Drawing.Size(491, 49);
			this.toolStripBar.ResumeLayout(false);
			this.toolStripBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripBar;
		private System.Windows.Forms.ToolStripButton insertHereButton;
		private System.Windows.Forms.ToolStripButton replaceSelectionButton;
		private System.Windows.Forms.ToolStripButton insertNextButton;
		private System.Windows.Forms.ToolStripButton insertPreviousButton;
		private System.Windows.Forms.ToolStripButton insertBetweenButton;
		private System.Windows.Forms.ToolStripButton repeatButton;
		private System.Windows.Forms.ToolStripButton undoButton;
		private System.Windows.Forms.ToolStripButton redoButton;
	}
}
