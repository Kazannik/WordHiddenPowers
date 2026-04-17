// Ignore Spelling: Dialogs uri

using LLMConnectorLibrary;
using LLMConnectorLibrary.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Services;
using WordHiddenPowers.Utils;
using static LLMConnectorLibrary.LLMOpenAI;
using static WordHiddenPowers.Controls.LLMConnectionControlBox;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMConnectSettingDialog : Form
	{
		public IEnumerable<string> LargeLanguageModels { get; private set; }

		public string SelectedLLMName => llmNameComboBox.SelectedItem?.ToString();

		public string SelectedEmbeddingLLMName => embeddingLlmNameComboBox.SelectedItem?.ToString();

		public Uri Uri => llmConnectionControlBox.Uri;

		public TimeSpan Timeout => new TimeSpan(hours: 0, minutes: (int)minutesNumericUpDown.Value, seconds: (int)secondsNumericUpDown.Value);

		public string MLNetModelPath { get; private set; }

		private readonly Documents.Document document;

		private readonly LLMClient llmClient;

		private readonly string llmName;
		private readonly string embeddingLlmName;

		/// <summary>
		/// public static string MLNetModelPath = Path.Combine(Utils.FileSystem.UserDirectory.FullName, "LbfgsMaximumEntropyMulti_26.04.2025.mlnet");
		/// </summary>
		/// <param name="document"></param>
		public LLMConnectSettingDialog(Documents.Document document, string selectedLLM, string selectedEmbeddingLlmName, Uri uri, TimeSpan timeout)
		{
			this.document = document;
			string mlNetModelName = document.MLModelName;
			llmName = selectedLLM;
			embeddingLlmName = selectedEmbeddingLlmName;

			InitializeComponent();

			minutesNumericUpDown.Value = timeout.Minutes;
			secondsNumericUpDown.Value = timeout.Seconds;

			llmConnectionControlBox.StateChanged += new EventHandler<ConnectionEventArgs>(LlmConnectionControlBox_ConnectedState);
			llmConnectionControlBox.PingTimeout = (int)Timeout.TotalSeconds;
			llmConnectionControlBox.ConnectingTimeout = (int)Timeout.TotalSeconds;
			llmConnectionControlBox.Address = uri.ToString();

			llmClient = new LLMClient(uri: uri, timeout: timeout);
			llmClient.HostChecked += new EventHandler<CheckHostEventArgs>(LlmClient_HostChecked);
			llmClient.ModelsCollectionCompleted += new EventHandler<ModelsCollectionCompletedEventArgs>(LlmClient_ModelsCollectionCompleted);
			
			llmConnectionControlBox.ConnectionCheck();

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
		}

		private void LlmConnectionControlBox_ConnectedState(object sender, ConnectionEventArgs e)
		{
			minutesNumericUpDown.Enabled =
				secondsNumericUpDown.Enabled =
				llmNameComboBox.Enabled =
				embeddingLlmNameComboBox.Enabled = 
				testModelButton.Enabled =
				updateLLMArrayButton.Enabled = 
				e.State.HasFlag(StateEnum.Checked) || e.State.HasFlag(StateEnum.ERROR) || e.State.HasFlag(StateEnum.Connected);
			
			if (e.State == (StateEnum.Connected | StateEnum.OK))
			{
				llmNameComboBox.Items.Clear();
				embeddingLlmNameComboBox.Items.Clear();

				llmNameComboBox.Enabled =
				embeddingLlmNameComboBox.Enabled = false;
				llmClient.ReadModelsName(uri: Uri, timeout: Timeout);
			}	
		}

		private void LlmClient_ModelsCollectionCompleted(object sender, ModelsCollectionCompletedEventArgs e)
		{
			LargeLanguageModels = e.Models;
			
			ModelNameComboBoxUpdate(selectedLLMName: string.IsNullOrEmpty(SelectedLLMName) ? llmName : SelectedLLMName,
				selectedEmbeddingLLMName: string.IsNullOrEmpty(SelectedEmbeddingLLMName) ? embeddingLlmName : SelectedEmbeddingLLMName);
			
			llmNameComboBox.Enabled = llmNameComboBox.Items.Count > 0;
			embeddingLlmNameComboBox.Enabled = embeddingLlmNameComboBox.Items.Count > 0;
		}

		private void LlmClient_HostChecked(object sender, CheckHostEventArgs e)
		{
			if (e.IsAvailable)
			{
			}
			else
			{
				LargeLanguageModels = new string[] { };
				llmNameComboBox.Items.Clear();
			}
			llmClient.ReadModelsName(uri: e.Uri, timeout: e.Timeout);
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

		private void ModelNameComboBoxUpdate(string selectedLLMName = "", string selectedEmbeddingLLMName = "")
		{
			llmNameComboBox.Items.Clear();
			embeddingLlmNameComboBox.Items.Clear();

			if (LargeLanguageModels is null) return;

			foreach (string item in LargeLanguageModels.OrderBy(x => x))
			{
				llmNameComboBox.Items.Add(item);
				embeddingLlmNameComboBox.Items.Add(item);
			}

			if (!string.IsNullOrWhiteSpace(selectedLLMName) &&
				llmNameComboBox.Items.Count > 0)
			{
				llmNameComboBox.SelectedIndex = llmNameComboBox.Items.IndexOf(selectedLLMName);
			}

			if (!string.IsNullOrWhiteSpace(selectedEmbeddingLLMName) &&
				embeddingLlmNameComboBox.Items.Count > 0)
			{
				embeddingLlmNameComboBox.SelectedIndex = embeddingLlmNameComboBox.Items.IndexOf(selectedEmbeddingLLMName);
			}
		}
				
		private void Update_Click(object sender, EventArgs e) => llmConnectionControlBox.ConnectionCheck();

		private void Button1_Click(object sender, EventArgs e)
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

		private void Button2_Click(object sender, EventArgs e)
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

		private void TestModelButton_Click(object sender, EventArgs e)
		{
			testModelButton.Enabled = false;
			string selectedEmbeddingLLMName = SelectedEmbeddingLLMName;

			IEnumerable<string> embeddingLargeLanguageModels = TestEmbedding(
								uri: Uri,
								timeout: Timeout,
								models: LargeLanguageModels);

			embeddingLlmNameComboBox.Items.Clear();

			foreach (string item in embeddingLargeLanguageModels)
			{
				embeddingLlmNameComboBox.Items.Add(item);
			}

			if (!string.IsNullOrWhiteSpace(selectedEmbeddingLLMName) &&
				embeddingLlmNameComboBox.Items.Count > 0)
			{
				embeddingLlmNameComboBox.SelectedIndex = embeddingLlmNameComboBox.Items.IndexOf(selectedEmbeddingLLMName);
			}
			testModelButton.Enabled = true;
		}
	}
}
