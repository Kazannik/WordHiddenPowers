namespace WordHiddenPowers.Dialogs
{
	partial class LLMConnectSettingDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LLMConnectSettingDialog));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.llmGroupBox = new System.Windows.Forms.GroupBox();
			this.modelsComboBox = new WordHiddenPowers.Controls.ComboControls.ModelsComboBox(this.components);
			this.addButton = new System.Windows.Forms.Button();
			this.authenticationProfileListBox = new WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListBox(this.components);
			this.llmGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// llmGroupBox
			// 
			resources.ApplyResources(this.llmGroupBox, "llmGroupBox");
			this.llmGroupBox.Controls.Add(this.modelsComboBox);
			this.llmGroupBox.Controls.Add(this.addButton);
			this.llmGroupBox.Controls.Add(this.authenticationProfileListBox);
			this.llmGroupBox.Controls.Add(this.label2);
			this.llmGroupBox.Name = "llmGroupBox";
			this.llmGroupBox.TabStop = false;
			// 
			// modelsComboBox
			// 
			resources.ApplyResources(this.modelsComboBox, "modelsComboBox");
			this.modelsComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.modelsComboBox.DropDownHeight = 404;
			this.modelsComboBox.DropDownWidth = 180;
			this.modelsComboBox.FormattingEnabled = true;
			this.modelsComboBox.Guid = "";
			this.modelsComboBox.Id = ((long)(-1));
			this.modelsComboBox.Name = "modelsComboBox";
			this.modelsComboBox.Prefix = "";
			this.modelsComboBox.PrefixUnique = false;
			this.modelsComboBox.SelectedItem = null;
			// 
			// addButton
			// 
			resources.ApplyResources(this.addButton, "addButton");
			this.addButton.Name = "addButton";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// authenticationProfileListBox
			// 
			resources.ApplyResources(this.authenticationProfileListBox, "authenticationProfileListBox");
			this.authenticationProfileListBox.BackColor = System.Drawing.SystemColors.Window;
			this.authenticationProfileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.authenticationProfileListBox.Name = "authenticationProfileListBox";
			this.authenticationProfileListBox.ItemProfileNameChanged += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemEventArgs>(this.AuthenticationProfileListBox_ItemProfileNameChanged);
			this.authenticationProfileListBox.ItemProfileChanged += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemEventArgs>(this.AuthenticationProfileListBox_ItemProfileChanged);
			this.authenticationProfileListBox.ItemStateChanged += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemConnectionEventArgs>(this.AuthenticationProfileListBox_ItemStateChanged);
			this.authenticationProfileListBox.ItemPingChanged += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemConnectionEventArgs>(this.AuthenticationProfileListBox_ItemPingChanged);
			this.authenticationProfileListBox.ItemConnecting += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemConnectionEventArgs>(this.AuthenticationProfileListBox_ItemConnecting);
			this.authenticationProfileListBox.ItemConnected += new System.EventHandler<WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem.ItemConnectionEventArgs>(this.AuthenticationProfileListBox_ItemConnected);
			// 
			// LLMConnectSettingDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			resources.ApplyResources(this, "$this");
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.llmGroupBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LLMConnectSettingDialog";
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
			this.llmGroupBox.ResumeLayout(false);
			this.llmGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox llmGroupBox;
		private Controls.AuthenticationProfileListControl.AuthenticationProfileListBox authenticationProfileListBox;
		private System.Windows.Forms.Button addButton;
		private Controls.ComboControls.ModelsComboBox modelsComboBox;
	}
}