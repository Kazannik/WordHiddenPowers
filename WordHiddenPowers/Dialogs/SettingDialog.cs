// Ignore Spelling: Dialogs uri

using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Services;
using WordHiddenPowers.Utils;

namespace WordHiddenPowers.Dialogs
{
	public partial class SettingDialog : Form
	{
		public IEnumerable<OpenAIModel> LargeLanguageModels { get; private set; }

		public string SelectedLLMName => llmNameComboBox.SelectedItem?.ToString();

		public Uri SelectedUri => new Uri(uriTextBox.Text);

		public string MLNetModelPath { get; private set; }

		private readonly Documents.Document document;

		/// <summary>
		/// public static string MLNetModelPath = Path.Combine(Utils.FileSystem.UserDirectory.FullName, "LbfgsMaximumEntropyMulti_26.04.2025.mlnet");
		/// </summary>
		/// <param name="document"></param>
		public SettingDialog(Documents.Document document, string selectedLLM, Uri uri)
		{
			this.document = document;
			string mlNetModelName = document.MLModelName;

			InitializeComponent();

			DirectoryInfo directory = new DirectoryInfo(FileSystem.UserDirectory.FullName);

			int index = -1;
			foreach (FileInfo file in directory.GetFiles("*.mlnet"))
			{
				int id = mlNetModelNameComboBox.Items.Add(file.Name);

				if (mlNetModelName == file.Name)
				{
					index = id;
				}
			}
			mlNetModelNameComboBox.SelectedIndex = index;			
			uriTextBox.Text = uri.ToString();
			_ = ValidateHostAvailable(selectedLLM);
		}


		private void MlNetModelNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			if (comboBox.SelectedIndex >= 0)
			{
				MLNetModelPath = Path.Combine(FileSystem.UserDirectory.FullName, comboBox.SelectedItem as string);
			}
			else
			{
				MLNetModelPath = string.Empty;
			}
		}

		private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SaveValues();
			}
			else if (e.CloseReason == CloseReason.UserClosing &&
				!string.IsNullOrEmpty(MLNetModelPath) &&
				document.MLModelName != mlNetModelNameComboBox.SelectedItem as string)
			{
				DialogResult result = MessageBox.Show(this, "Сохранить выбор ИИ модели?", "Выбор ИИ модели", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					SaveValues();
				}
				else if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void SaveValues()
		{
			document.MLModelName = mlNetModelNameComboBox.SelectedItem as string;
			document.CommitVariables();
			document.Doc.Saved = false;
		}

		private void ModelNameComboBoxUpdate(string selectedLLMName = "")
		{
			llmNameComboBox.Items.Clear();
			if (LargeLanguageModels is null) return;

			foreach (OpenAIModel item in LargeLanguageModels)
			{
				llmNameComboBox.Items.Add(item.Id);
			}

			if (!string.IsNullOrWhiteSpace(selectedLLMName) &&
				llmNameComboBox.Items.Count > 0)
			{
				llmNameComboBox.SelectedIndex = llmNameComboBox.Items.IndexOf(selectedLLMName);
			}
		}

		private async Task<bool> IsHostAvailable() => await NetService.CheckHostByHttpAsync(new Uri(baseUri: SelectedUri, relativeUri: SelectedUri.AbsolutePath + "/models"));

		private async Task ValidateHostAvailable(string selectedLLMName = "")
		{
			updateLLMArrayButton.Enabled = false;
			uriTextBox.Enabled = false;
			llmNameComboBox.Enabled = false;

			bool isHostAvailable = await IsHostAvailable();

			if (isHostAvailable)
			{
				hostErrorProvider.Clear();
			}
			else
			{
				hostErrorProvider.SetError(uriTextBox, "Выбранный хост не доступен.");
			}
			LargeLanguageModels = await OpenAIService.GetModelsAsync(SelectedUri);

			ModelNameComboBoxUpdate(selectedLLMName: selectedLLMName);

			updateLLMArrayButton.Enabled = true;
			uriTextBox.Enabled = true;
			llmNameComboBox.Enabled = true;
		}

		private void Update_Click(object sender, EventArgs e) => _ = ValidateHostAvailable(SelectedLLMName);

		private void button1_Click(object sender, EventArgs e)
		{
			PromptEditorDialog dialog = new PromptEditorDialog(
				caption: OpenAIService.CaptionButton1,
				systemMessage: OpenAIService.SystemMessageButton1,
				prefixUserMessage: OpenAIService.PrefixUserMessageButton1,
				postfixUserMessage: OpenAIService.PostfixUserMessageButton1
				);
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				OpenAIService.CaptionButton1 = dialog.Caption;
				OpenAIService.SystemMessageButton1 = dialog.SystemMessage;
				OpenAIService.PrefixUserMessageButton1 = dialog.PrefixUserMessage;
				OpenAIService.PostfixUserMessageButton1 = dialog.PostfixUserMessage;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			PromptEditorDialog dialog = new PromptEditorDialog(
				caption: OpenAIService.CaptionButton2,
				systemMessage: OpenAIService.SystemMessageButton2,
				prefixUserMessage: OpenAIService.PrefixUserMessageButton2,
				postfixUserMessage: OpenAIService.PostfixUserMessageButton2
				);
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				OpenAIService.CaptionButton2 = dialog.Caption;
				OpenAIService.SystemMessageButton2 = dialog.SystemMessage;
				OpenAIService.PrefixUserMessageButton2 = dialog.PrefixUserMessage;
				OpenAIService.PostfixUserMessageButton2 = dialog.PostfixUserMessage;
			}
		}
	}
}
