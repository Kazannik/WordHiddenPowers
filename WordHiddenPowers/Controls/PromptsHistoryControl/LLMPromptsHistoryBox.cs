using LLMConnectorLibrary;
using LLMConnectorLibrary.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using DialogsUtils = WordHiddenPowers.Utils.Dialogs;


namespace WordHiddenPowers.Controls.PromptsHistoryControl
{
	public partial class LLMPromptsHistoryBox : UserControl
	{
		private IChatOptions chatOptions;
		private string systemMessage;
		private string modelName;
		private string profileEndpoint;


		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ChatOptionsChanged;
		protected virtual void OnChatOptionsChanged(EventArgs e) => ChatOptionsChanged?.Invoke(this, e);
		private void ChatOptionsButton_Click(object sender, EventArgs e)
		{
			LLMChatOptionsDialog dialog = new(ChatOptions);
			if (DialogsUtils.ShowDialog(dialog) == DialogResult.OK)
			{
				ChatOptions = dialog.ChatOptions;
			}
		}


		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> SystemMessageChanged;
		protected virtual void OnSystemMessageChanged(EventArgs e) => SystemMessageChanged?.Invoke(this, e);
		private void SystemMessageEditButton_Click(object sender, EventArgs e)
		{
			TextEditorDialog dialog = new(SystemMessage);
			if (DialogsUtils.ShowDialog(dialog) == DialogResult.OK)
			{
				SystemMessage = dialog.Text;
			}
		}


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickClear;
		protected virtual void OnClickClear(EventArgs e) => ClickClear?.Invoke(this, e);
		private void ClearButton_Click(object sender, EventArgs e) => OnClickClear(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickDelete;
		protected virtual void OnClickDelete(EventArgs e) => ClickDelete?.Invoke(this, e);
		private void DeleteButton_Click(object sender, EventArgs e) => OnClickDelete(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickFavorite;
		protected virtual void OnClickFavorite(EventArgs e) => ClickFavorite?.Invoke(this, e);
		private void FavoriteButton_Click(object sender, EventArgs e) => OnClickFavorite(e);
		

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickPreviousPrompt;
		protected virtual void OnClickPreviousPrompt(EventArgs e) => ClickPreviousPrompt?.Invoke(this, e);
		private void PreviousPromptButton_Click(object sender, EventArgs e) => OnClickPreviousPrompt(e);


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> ClickNextPrompt;
		protected virtual void OnClickNextPrompt(EventArgs e) => ClickNextPrompt?.Invoke(this, e);
		private void NextPromptButton_Click(object sender, EventArgs e) => OnClickNextPrompt(e);


		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> SelectedModelChanged;
		protected virtual void OnSelectedModelChanged(EventArgs e) => SelectedModelChanged?.Invoke(this, e);
		private void ModelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedModelChanged(new EventArgs());
		} 

		public LLMPromptsHistoryBox()
		{
			InitializeComponent();

			toolStripBar.Height = 44;
			toolStripBar.Width = Width;
						
			ResizeComponent();
		}

		public void InitializeModelsSource(ModelsCollection models) => modelsComboBox.InitializeSource(models);
				
		protected override void OnResize(EventArgs e)
		{
			ResizeComponent();
			base.OnResize(e);
		}

		private void ResizeComponent()
		{
			int offset = (modelsComboBox.Height - modelLabel.Height) / 2;
			toolStripBar.Height = 44;
			toolStripBar.Width = Width;

			int h = toolStripBar.Height + modelsComboBox.Height + SystemInformation.BorderSize.Height * 4;
			if (Height != h) Height = h;
			modelLabel.Location = new Point(2, Height - modelLabel.Height - offset + 1);
			modelsComboBox.Location = new Point(modelLabel.Width + 6, Height - modelsComboBox.Height);
			modelsComboBox.Width = Width - modelLabel.Width - 8;
		}

		public IChatOptions ChatOptions
		{
			get => chatOptions;
			set
			{
				if (value != chatOptions) 
				{
					chatOptions = value;
					OnChatOptionsChanged(new EventArgs());
				}
			}
		}

		public string SystemMessage
		{
			get => systemMessage;
			set
			{
				if (value != systemMessage)
				{
					systemMessage = value;
					OnSystemMessageChanged(new EventArgs());
				}
			}
		}

		public IModel SelectedModel
		{
			get => GetModel();
			set
			{
				if (value is not null)
				{
					if (value.Id != modelName ||
						value.Profile.ClientOptions.Endpoint.OriginalString != profileEndpoint)
					{
						modelName = value.Id;
						profileEndpoint = value.Profile.ClientOptions.Endpoint.OriginalString;
						SelectModel(model: modelName, profileEndpoint: profileEndpoint);
					}
				}
				else
				{
					modelName = string.Empty;
					profileEndpoint = string.Empty;
					SelectModel(model: modelName, profileEndpoint: profileEndpoint);
				}
			}
		}

		private IModel GetModel()
		{
			if (InvokeRequired)
			{
				return (IModel)Invoke(new Func<IModel>(GetModel));
			}
			else
			{
				return modelsComboBox.SelectedItem?.Model;
			}
		}
		
		public void SelectModel(string model, string profileEndpoint)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => SelectModel(model: model, profileEndpoint: profileEndpoint)));
			}
			else
			{
				modelsComboBox.SelectModel(modelName: model, modelProfileEndpoint: profileEndpoint);
			}
		}

		public Image DeleteButtonImage
		{
			get => GetDeleteButtonImage();
			set => SetDeleteButtonImage(value);
		}

		private Image GetDeleteButtonImage()
		{
			if (InvokeRequired)
				return (Image)Invoke(new Func<Image>(GetDeleteButtonImage));
			else
				return deleteButton.Image;
		}
		
		private void SetDeleteButtonImage(Image value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetDeleteButtonImage(value)));
			else
				deleteButton.Image = value;
		}

		public string DeleteButtonToolTipText
		{
			get => GetDeleteButtonToolTipText();
			set => SetDeleteButtonToolTipText(value);
		}

		private string GetDeleteButtonToolTipText()
		{
			if (InvokeRequired)
				return (string)Invoke(new Func<string>(GetDeleteButtonToolTipText));
			else
				return deleteButton.ToolTipText;
		}

		private void SetDeleteButtonToolTipText(string value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetDeleteButtonToolTipText(value)));
			else
				deleteButton.ToolTipText = value;
		}

		public string StateText
		{
			get => GetStateText();
			set => SetStateText(value);
		}

		private string GetStateText()
		{
			if (InvokeRequired)
				return (string)Invoke(new Func<string>(GetStateText));
			else
				return stateLabel.Text;
		}

		private void SetStateText(string value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetStateText(value)));
			else
				stateLabel.Text = value;
		}

		public bool Favorite
		{
			get => GetFavoriteChecked();
			set => SetFavoriteChecked(value);
		}

		private bool GetFavoriteChecked()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetFavoriteChecked));
			else
				return favoriteButton.Checked;
		}
		
		private void SetFavoriteChecked(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetFavoriteChecked(value)));
			else
				favoriteButton.Checked = value;
		}

		#region Enabled Controls

		public bool ClearButtonEnabled
		{
			get => GetClearButtonEnabled();
			set => SetClearButtonEnabled(value);
		}

		private bool GetClearButtonEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetClearButtonEnabled));
			else
				return clearButton.Enabled;
		}

		private void SetClearButtonEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetClearButtonEnabled(value)));
			else
				clearButton.Enabled = value;
		}

		public bool DeleteButtonEnabled
		{
			get => GetDeleteButtonEnabled();
			set => SetDeleteButtonEnabled(value);
		}

		private bool GetDeleteButtonEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetDeleteButtonEnabled));
			else
				return deleteButton.Enabled;
		}

		private void SetDeleteButtonEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetDeleteButtonEnabled(value)));
			else
				deleteButton.Enabled = value;
		}

		public bool FavoriteButtonEnabled
		{
			get => GetFavoriteButtonEnabled();
			set => SetFavoriteButtonEnabled(value);
		}

		private bool GetFavoriteButtonEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetFavoriteButtonEnabled));
			else
				return favoriteButton.Enabled;
		}

		private void SetFavoriteButtonEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetFavoriteButtonEnabled(value)));
			else
				favoriteButton.Enabled = value;
		}

		public bool PreviousButtonEnabled
		{
			get => GetPreviousButtonEnabled();
			set => SetPreviousButtonEnabled(value);
		}

		private bool GetPreviousButtonEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetPreviousButtonEnabled));
			else
				return previousPromptButton.Enabled;
		}

		private void SetPreviousButtonEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetPreviousButtonEnabled(value)));
			else
				previousPromptButton.Enabled = value;
		}
		
		public bool NextButtonEnabled
		{
			get => GetNextButtonEnabled();
			set => SetNextButtonEnabled(value);
		}

		private bool GetNextButtonEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetNextButtonEnabled));
			else
				return nextPromptButton.Enabled;
		}

		private void SetNextButtonEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetNextButtonEnabled(value)));
			else
				nextPromptButton.Enabled = value;
		}

		public bool ModelsComboBoxEnabled
		{
			get => GetModelsComboBoxEnabled();
			set => SetModelsComboBoxEnabled(value);
		}

		private bool GetModelsComboBoxEnabled()
		{
			if (InvokeRequired)
				return (bool)Invoke(new Func<bool>(GetModelsComboBoxEnabled));
			else
				return modelsComboBox.Enabled;
		}

		private void SetModelsComboBoxEnabled(bool value)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetModelsComboBoxEnabled(value)));
			else
			 modelsComboBox.Enabled = value;
		}

		#endregion

		public void SetChatOptions(
				int maxOutputTokenCount,
				float frequencyPenalty,
				float presencePenalty,
				float temperature,
				float topP)
		{
			chatOptions = LLMConnectorLibrary.ChatOptions.Create(
				maxOutputTokenCount: maxOutputTokenCount,
				frequencyPenalty: frequencyPenalty,
				presencePenalty: presencePenalty,
				temperature: temperature,
				topP: topP);
		}


		public void SetSystemMessage(string message)
		{
			systemMessage = message;
		}
	}
}
