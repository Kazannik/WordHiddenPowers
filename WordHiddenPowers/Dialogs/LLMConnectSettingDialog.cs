// Ignore Spelling: Dialogs uri

using LLMConnectorLibrary;
using LLMConnectorLibrary.Authentication;
using LLMConnectorLibrary.EventArgs;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Controls.ToolBar;
using static WordHiddenPowers.Controls.AuthenticationProfileListControl.AuthenticationProfileListItem;
using static WordHiddenPowers.Controls.ConnectionControlBox;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMConnectSettingDialog : Form
	{
		private readonly string selectedChatModelName;
		private readonly string selectedChatModelProfile;

		private readonly LLMClient client;
		public IEnumerable<IModel> LargeLanguageModels => modelsComboBox.Models;
		public IModel SelectedModel => modelsComboBox.SelectedItem?.Model;
		public IEnumerable<IAuthenticationProfile> AuthenticationProfiles => authenticationProfileListBox;
		public ChatButtonProperties ChatButton_1_Properties { get; private set; }
		public ChatButtonProperties ChatButton_2_Properties { get; private set; }

		public LLMConnectSettingDialog()
		{
			this.selectedChatModelName = string.Empty;
			this.selectedChatModelProfile = string.Empty;

			InitializeComponent();

			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;

			client = new LLMClient();
			client.HostChecked += new EventHandler<CheckHostEventArgs>(LLMClient_HostChecked);
			client.ModelsCollectionCompleted += new EventHandler<ModelsCollectionCompletedEventArgs>(LLMClient_ModelsCollectionCompleted);
		}

		/// <summary>
		/// public static string MLNetModelPath = Path.Combine(Utils.FileSystem.UserDirectory.FullName, "LbfgsMaximumEntropyMulti_26.04.2025.mlnet");
		/// </summary>
		/// <param name="document"></param>
		public LLMConnectSettingDialog(
			IEnumerable<IAuthenticationProfile> profiles,
			string selectedChatModelName,
			string selectedChatModelProfile,
			ChatButtonProperties button_1_properties, ChatButtonProperties button_2_properties) : this()
		{
			this.selectedChatModelName = selectedChatModelName;
			this.selectedChatModelProfile = selectedChatModelProfile;
			authenticationProfileListBox.AddRange(profiles);
			
			ChatButton_1_Properties = button_1_properties;
			ChatButton_2_Properties = button_2_properties;
		}

		private void LLMClient_HostChecked(object sender, CheckHostEventArgs e)
		{
			if (e.IsAvailable)
			{
			}
			else
			{

			}
		}

		private void LLMClient_ModelsCollectionCompleted(object sender, ModelsCollectionCompletedEventArgs e)
		{
			if (e.Models.Count > 0)
			{
				modelsComboBox.BeginInvoke(new Action(() =>
				{
					modelsComboBox.AddRange(e.Models);
					modelsComboBox.Invalidate();
				}));
			}
			else
			{
				ClearModelComboBox();
			}

			modelsComboBox.BeginInvoke(new Action(() =>
			{
				modelsComboBox.SelectModel(selectedChatModelName, selectedChatModelProfile);
				modelsComboBox.Enabled = true;
			}));
		}

		private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{

			}
			else if (e.CloseReason == CloseReason.UserClosing)
			{
				DialogResult result = MessageBox.Show(this, "Сохранить выбор ИИ модели?", "Выбор ИИ модели", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{

				}
				else if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			PromptEditorDialog dialog = new(
				caption: ChatButton_1_Properties.Caption,
				systemMessage: ChatButton_1_Properties.SystemMessage,
				prefixUserMessage: ChatButton_1_Properties.PrefixUserMessage,
				postfixUserMessage: ChatButton_1_Properties.PostfixUserMessage);

			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				ChatButton_1_Properties = new ChatButtonProperties(
					caption: dialog.Caption,
					systemMessage: dialog.SystemMessage,
					prefixUserMessage: dialog.PrefixUserMessage,
					postfixUserMessage: dialog.PostfixUserMessage);
			}
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			PromptEditorDialog dialog = new(
				caption: ChatButton_2_Properties.Caption,
				systemMessage: ChatButton_2_Properties.SystemMessage,
				prefixUserMessage: ChatButton_2_Properties.PrefixUserMessage,
				postfixUserMessage: ChatButton_2_Properties.PostfixUserMessage);

			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				ChatButton_2_Properties = new ChatButtonProperties(
					caption: dialog.Caption,
					systemMessage: dialog.SystemMessage,
					prefixUserMessage: dialog.PrefixUserMessage,
					postfixUserMessage: dialog.PostfixUserMessage);
			}
		}
				
		private void AddButton_Click(object sender, EventArgs e)
		{
			ConnectionTemplatesBrowser dialog = new();
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				authenticationProfileListBox.Add(dialog.AuthenticationProfile);
			}			
		}

		private void AuthenticationProfileListBox_ItemProfileNameChanged(object sender, ItemEventArgs e)
		{
			modelsComboBox.BeginInvoke(new Action(() =>
			{
				modelsComboBox.RefreshRange(e.Item.guid, e.Item.Profile);
			}));
		}

		private void AuthenticationProfileListBox_ItemProfileChanged(object sender, ItemEventArgs e)
		{
		
		}

		private void AuthenticationProfileListBox_ItemStateChanged(object sender, ItemConnectionEventArgs e)
		{

		}

		private void AuthenticationProfileListBox_ItemPingChanged(object sender, ItemConnectionEventArgs e)
		{

		}

		private void AuthenticationProfileListBox_ItemConnecting(object sender, ItemConnectionEventArgs e)
		{
			modelsComboBox.Enabled = false;
		}

		private async void AuthenticationProfileListBox_ItemConnected(object sender, ItemConnectionEventArgs e)
		{
			if (e.State == StateEnum.Connected)
			{
				if (e.Item.IsDouble)
				{
					modelsComboBox.Enabled = true;
					return;
				}
				await Task.Run(() => client.ReadModelsNameAsync(e.Item.Profile, tag: e.Item.guid));
			}
			else if (e.State == (StateEnum.Connected | StateEnum.ERROR))
			{
				try
				{
					modelsComboBox.BeginInvoke(new Action(() =>
					{
						modelsComboBox.RemoveRange(e.Item.guid);
					}));
				}
				catch (Exception) { }
				modelsComboBox.Enabled = true;
			}
		}

		private void ClearModelComboBox()
		{
			IEnumerable<IAuthenticationProfile> removedProfiles = modelsComboBox.Profiles
					.Where(x => !authenticationProfileListBox.Contains(x));

			modelsComboBox.BeginInvoke(new Action(() =>
			{
				modelsComboBox.RemoveRange(removedProfiles);
			}));
		}
	}
}
