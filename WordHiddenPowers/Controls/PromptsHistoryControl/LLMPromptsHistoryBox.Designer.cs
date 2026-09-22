namespace WordHiddenPowers.Controls.PromptsHistoryControl
{
	partial class LLMPromptsHistoryBox
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
			this.toolStripBar = new System.Windows.Forms.ToolStrip();
			this.nextPromptButton = new System.Windows.Forms.ToolStripButton();
			this.previousPromptButton = new System.Windows.Forms.ToolStripButton();
			this.favoriteButton = new System.Windows.Forms.ToolStripButton();
			this.deleteButton = new System.Windows.Forms.ToolStripButton();
			this.clearButton = new System.Windows.Forms.ToolStripButton();
			this.systemMessageEditButton = new System.Windows.Forms.ToolStripButton();
			this.chatOptionsButton = new System.Windows.Forms.ToolStripButton();
			this.modelLabel = new System.Windows.Forms.Label();
			this.modelsComboBox = new WordHiddenPowers.Controls.ComboControls.ModelsComboBox(this.components);
			this.stateLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripBar
			// 
			this.toolStripBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripBar.ImageScalingSize = new System.Drawing.Size(46, 46);
			this.toolStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextPromptButton,
            this.previousPromptButton,
            this.favoriteButton,
            this.deleteButton,
            this.clearButton,
            this.systemMessageEditButton,
            this.chatOptionsButton,
            this.stateLabel});
			this.toolStripBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStripBar.Location = new System.Drawing.Point(0, 0);
			this.toolStripBar.Name = "toolStripBar";
			this.toolStripBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripBar.Size = new System.Drawing.Size(600, 47);
			this.toolStripBar.TabIndex = 0;
			this.toolStripBar.Text = "Стандартная панель";
			// 
			// nextPromptButton
			// 
			this.nextPromptButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextPromptButton.Image = global::WordHiddenPowers.Properties.Resources.HistoryNext2;
			this.nextPromptButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.nextPromptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.nextPromptButton.Name = "nextPromptButton";
			this.nextPromptButton.Padding = new System.Windows.Forms.Padding(4);
			this.nextPromptButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.nextPromptButton.Size = new System.Drawing.Size(44, 44);
			this.nextPromptButton.Text = "Следующий промпт";
			this.nextPromptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.nextPromptButton.ToolTipText = "Перейти к следующему промпту";
			this.nextPromptButton.Click += new System.EventHandler(this.NextPromptButton_Click);
			// 
			// previousPromptButton
			// 
			this.previousPromptButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.previousPromptButton.Image = global::WordHiddenPowers.Properties.Resources.HistoryPrevious;
			this.previousPromptButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.previousPromptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.previousPromptButton.Name = "previousPromptButton";
			this.previousPromptButton.Padding = new System.Windows.Forms.Padding(4);
			this.previousPromptButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.previousPromptButton.Size = new System.Drawing.Size(44, 44);
			this.previousPromptButton.Text = "Предыдущий промпт";
			this.previousPromptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.previousPromptButton.ToolTipText = "Перейти к предыдущему промпту";
			this.previousPromptButton.Click += new System.EventHandler(this.PreviousPromptButton_Click);
			// 
			// favoriteButton
			// 
			this.favoriteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.favoriteButton.Image = global::WordHiddenPowers.Properties.Resources.Favorite;
			this.favoriteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.favoriteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.favoriteButton.Name = "favoriteButton";
			this.favoriteButton.Padding = new System.Windows.Forms.Padding(4);
			this.favoriteButton.Size = new System.Drawing.Size(44, 44);
			this.favoriteButton.Text = "Избранное";
			this.favoriteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.favoriteButton.Click += new System.EventHandler(this.FavoriteButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.deleteButton.Image = global::WordHiddenPowers.Properties.Resources.DocumentClear32;
			this.deleteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Padding = new System.Windows.Forms.Padding(4);
			this.deleteButton.Size = new System.Drawing.Size(44, 44);
			this.deleteButton.Text = "Удалить";
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// clearButton
			// 
			this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearButton.Image = global::WordHiddenPowers.Properties.Resources.HistoryClear;
			this.clearButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearButton.Name = "clearButton";
			this.clearButton.Padding = new System.Windows.Forms.Padding(4);
			this.clearButton.Size = new System.Drawing.Size(44, 44);
			this.clearButton.Text = "Очистить...";
			this.clearButton.ToolTipText = "Очистить историю промптов";
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// systemMessageEditButton
			// 
			this.systemMessageEditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.systemMessageEditButton.Image = global::WordHiddenPowers.Properties.Resources.SystemMessage;
			this.systemMessageEditButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.systemMessageEditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.systemMessageEditButton.Name = "systemMessageEditButton";
			this.systemMessageEditButton.Padding = new System.Windows.Forms.Padding(4);
			this.systemMessageEditButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.systemMessageEditButton.Size = new System.Drawing.Size(44, 44);
			this.systemMessageEditButton.Text = "Системный промпт";
			this.systemMessageEditButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.systemMessageEditButton.ToolTipText = "Редактировать системный промпт";
			this.systemMessageEditButton.Click += new System.EventHandler(this.SystemMessageEditButton_Click);
			// 
			// chatOptionsButton
			// 
			this.chatOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.chatOptionsButton.Image = global::WordHiddenPowers.Properties.Resources.ChatOptions;
			this.chatOptionsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.chatOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.chatOptionsButton.Name = "chatOptionsButton";
			this.chatOptionsButton.Padding = new System.Windows.Forms.Padding(4);
			this.chatOptionsButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chatOptionsButton.Size = new System.Drawing.Size(44, 44);
			this.chatOptionsButton.Text = "Параметры промпта";
			this.chatOptionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.chatOptionsButton.ToolTipText = "Настроить параметры промпта";
			this.chatOptionsButton.Click += new System.EventHandler(this.ChatOptionsButton_Click);
			// 
			// modelLabel
			// 
			this.modelLabel.AutoSize = true;
			this.modelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.modelLabel.Location = new System.Drawing.Point(0, 48);
			this.modelLabel.Name = "modelLabel";
			this.modelLabel.Size = new System.Drawing.Size(80, 20);
			this.modelLabel.TabIndex = 2;
			this.modelLabel.Text = "Модель:";
			// 
			// modelsComboBox
			// 
			this.modelsComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.modelsComboBox.DropDownHeight = 504;
			this.modelsComboBox.DropDownWidth = 180;
			this.modelsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.modelsComboBox.FormattingEnabled = true;
			this.modelsComboBox.Guid = "";
			this.modelsComboBox.Id = ((long)(-1));
			this.modelsComboBox.IntegralHeight = false;
			this.modelsComboBox.ItemHeight = 25;
			this.modelsComboBox.Location = new System.Drawing.Point(144, 48);
			this.modelsComboBox.MaxDropDownItems = 20;
			this.modelsComboBox.Name = "modelsComboBox";
			this.modelsComboBox.Prefix = "";
			this.modelsComboBox.PrefixUnique = false;
			this.modelsComboBox.SelectedItem = null;
			this.modelsComboBox.Size = new System.Drawing.Size(444, 31);
			this.modelsComboBox.TabIndex = 1;
			this.modelsComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelsComboBox_SelectedIndexChanged);
			// 
			// stateLabel
			// 
			this.stateLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stateLabel.Margin = new System.Windows.Forms.Padding(0, 8, 4, 2);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(24, 28);
			this.stateLabel.Text = "#";
			// 
			// LLMPromptsHistoryBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.modelLabel);
			this.Controls.Add(this.modelsComboBox);
			this.Controls.Add(this.toolStripBar);
			this.Name = "LLMPromptsHistoryBox";
			this.Size = new System.Drawing.Size(600, 83);
			this.toolStripBar.ResumeLayout(false);
			this.toolStripBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripBar;
		private System.Windows.Forms.ToolStripButton previousPromptButton;
		private System.Windows.Forms.ToolStripButton nextPromptButton;
		private System.Windows.Forms.ToolStripButton systemMessageEditButton;
		private ComboControls.ModelsComboBox modelsComboBox;
		private System.Windows.Forms.Label modelLabel;
		private System.Windows.Forms.ToolStripButton chatOptionsButton;
		private System.Windows.Forms.ToolStripButton favoriteButton;
		private System.Windows.Forms.ToolStripButton clearButton;
		private System.Windows.Forms.ToolStripButton deleteButton;
		private System.Windows.Forms.ToolStripLabel stateLabel;
	}
}
