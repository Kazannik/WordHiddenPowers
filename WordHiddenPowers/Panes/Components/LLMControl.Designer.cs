namespace WordHiddenPowers.Panes.Components
{
	partial class LLMControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LLMControl));
			this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.userMessageTextBox = new System.Windows.Forms.TextBox();
			this.sendMessageButtonsBar = new WordHiddenPowers.Controls.SendMessagesControl.LLMSendMessageBox();
			this.promptsHistoryBox = new WordHiddenPowers.Controls.PromptsHistoryControl.LLMPromptsHistoryBox();
			this.mainTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTableLayoutPanel
			// 
			resources.ApplyResources(this.mainTableLayoutPanel, "mainTableLayoutPanel");
			this.mainTableLayoutPanel.Controls.Add(this.userMessageTextBox, 0, 1);
			this.mainTableLayoutPanel.Controls.Add(this.sendMessageButtonsBar, 0, 2);
			this.mainTableLayoutPanel.Controls.Add(this.promptsHistoryBox, 0, 0);
			this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
			// 
			// userMessageTextBox
			// 
			resources.ApplyResources(this.userMessageTextBox, "userMessageTextBox");
			this.userMessageTextBox.Name = "userMessageTextBox";
			this.userMessageTextBox.Click += new System.EventHandler(this.UserMessageTextBox_TextChanged);
			this.userMessageTextBox.TextChanged += new System.EventHandler(this.UserMessageTextBox_TextChanged);
			// 
			// sendMessageButtonsBar
			// 
			this.sendMessageButtonsBar.AccessUserMessage = false;
			this.sendMessageButtonsBar.BackColor = System.Drawing.SystemColors.Window;
			this.sendMessageButtonsBar.ChatMessageMode = WordHiddenPowers.Documents.Document.ChatMessageModeEnum.Nothing;
			resources.ApplyResources(this.sendMessageButtonsBar, "sendMessageButtonsBar");
			this.sendMessageButtonsBar.Document = null;
			this.sendMessageButtonsBar.Name = "sendMessageButtonsBar";
			this.sendMessageButtonsBar.ClickInsertHereMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickInsertHereMessage);
			this.sendMessageButtonsBar.ClickReplaceSelectionMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickReplaceSelectionMessage);
			this.sendMessageButtonsBar.ClickInsertNextMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickInsertNextMessage);
			this.sendMessageButtonsBar.ClickInsertPreviousMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickInsertPreviousMessage);
			this.sendMessageButtonsBar.ClickInsertCenterMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickInsertBetweenMessage);
			this.sendMessageButtonsBar.ClickRepeatMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickRepeatMessage);
			this.sendMessageButtonsBar.ClickRedoMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickRedoMessage);
			this.sendMessageButtonsBar.ClickUndoMessage += new System.EventHandler<System.EventArgs>(this.SendMessageButtonsBar_ClickUndoMessage);
			// 
			// promptsHistoryBox
			// 
			this.promptsHistoryBox.ChatOptions = null;
			this.promptsHistoryBox.ClearButtonEnabled = true;
			this.promptsHistoryBox.DeleteButtonEnabled = true;
			this.promptsHistoryBox.DeleteButtonImage = ((System.Drawing.Image)(resources.GetObject("promptsHistoryBox.DeleteButtonImage")));
			this.promptsHistoryBox.DeleteButtonToolTipText = "Удалить";
			resources.ApplyResources(this.promptsHistoryBox, "promptsHistoryBox");
			this.promptsHistoryBox.Favorite = false;
			this.promptsHistoryBox.FavoriteButtonEnabled = true;
			this.promptsHistoryBox.ModelsComboBoxEnabled = true;
			this.promptsHistoryBox.Name = "promptsHistoryBox";
			this.promptsHistoryBox.NextButtonEnabled = true;
			this.promptsHistoryBox.PreviousButtonEnabled = true;
			this.promptsHistoryBox.SelectedModel = null;
			this.promptsHistoryBox.StateText = "#";
			this.promptsHistoryBox.SystemMessage = null;
			this.promptsHistoryBox.ChatOptionsChanged += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ChatOptionsChanged);
			this.promptsHistoryBox.SystemMessageChanged += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_SystemMessageChanged);
			this.promptsHistoryBox.ClickClear += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ClickClear);
			this.promptsHistoryBox.ClickDelete += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ClickDelete);
			this.promptsHistoryBox.ClickFavorite += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ClickFavorite);
			this.promptsHistoryBox.ClickPreviousPrompt += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ClickPreviousPrompt);
			this.promptsHistoryBox.ClickNextPrompt += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_ClickNextPrompt);
			this.promptsHistoryBox.SelectedModelChanged += new System.EventHandler<System.EventArgs>(this.PromptsHistoryBox_SelectedModelChanged);
			// 
			// LLMControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.mainTableLayoutPanel);
			this.Name = "LLMControl";
			resources.ApplyResources(this, "$this");
			this.mainTableLayoutPanel.ResumeLayout(false);
			this.mainTableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}
		
		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
		private System.Windows.Forms.TextBox userMessageTextBox;
		private Controls.SendMessagesControl.LLMSendMessageBox sendMessageButtonsBar;
		private Controls.PromptsHistoryControl.LLMPromptsHistoryBox promptsHistoryBox;
	}
}
