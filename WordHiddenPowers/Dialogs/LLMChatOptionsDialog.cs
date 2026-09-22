using LLMConnectorLibrary;
using System;
using System.Windows.Forms;
using Options = LLMConnectorLibrary.ChatOptions;

namespace WordHiddenPowers.Dialogs
{
	public partial class LLMChatOptionsDialog : Form
	{
		private IChatOptions defaultOptions = Options.Empty;
		private IChatOptions brainstorOptions = Options.Brainstorming;

		public LLMChatOptionsDialog()
		{
			InitializeComponent();

			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;

			comboBox1.Items.Add("Другие настойки");
			comboBox1.Items.Add(Options.Basic);
			comboBox1.Items.Add(Options.CodingAndMathematics);
			comboBox1.Items.Add(Options.AnalyticsAndFacts);
			comboBox1.Items.Add(Options.BusinessCorrespondence);
			comboBox1.Items.Add(Options.StandardChat);
			comboBox1.Items.Add(Options.CopywritingAndBlogging);
			comboBox1.Items.Add("Мозговой штурм");
		}

		public LLMChatOptionsDialog(IChatOptions options) : this()
		{
			SetOptions(options);
		}

		public IChatOptions ChatOptions => Options.Create(
			(int)maxOutputTokenCountNumericUpDown.Value,
			(float)frequencyPenaltyNumericUpDown.Value,
			(float)presencePenaltyNumericUpDown.Value,
			(float)temperatureNumericUpDown.Value,
			(float)topPNumericUpDown.Value);


		private void SetOptions(IChatOptions options)
		{
			maxOutputTokenCountNumericUpDown.Value = options.MaxOutputTokenCount != null ? (decimal)options.MaxOutputTokenCount : 0;
			frequencyPenaltyNumericUpDown.Value = options.FrequencyPenalty != null ? (decimal)options.FrequencyPenalty : 0;
			presencePenaltyNumericUpDown.Value = options.PresencePenalty != null ? (decimal)options.PresencePenalty : 0;
			temperatureNumericUpDown.Value = options.Temperature != null ? (decimal)options.Temperature : 0;
			topPNumericUpDown.Value = options.TopP != null ? (decimal)options.TopP : 0;

			if (options.FrequencyPenalty == Options.Basic.FrequencyPenalty &&
				options.PresencePenalty == Options.Basic.PresencePenalty &&
				options.Temperature == Options.Basic.Temperature &&
				options.TopP == Options.Basic.TopP)
			{
				comboBox1.SelectedIndex = 1;
			}
			else if (options.FrequencyPenalty == Options.CodingAndMathematics.FrequencyPenalty &&
				options.PresencePenalty == Options.CodingAndMathematics.PresencePenalty &&
				options.Temperature == Options.CodingAndMathematics.Temperature &&
				options.TopP == Options.CodingAndMathematics.TopP)
			{
				comboBox1.SelectedIndex = 2;
			}
			else if (options.FrequencyPenalty == Options.AnalyticsAndFacts.FrequencyPenalty &&
				options.PresencePenalty == Options.AnalyticsAndFacts.PresencePenalty &&
				options.Temperature == Options.AnalyticsAndFacts.Temperature &&
				options.TopP == Options.AnalyticsAndFacts.TopP)
			{
				comboBox1.SelectedIndex = 3;
			}
			else if (options.FrequencyPenalty == Options.BusinessCorrespondence.FrequencyPenalty &&
				options.PresencePenalty == Options.BusinessCorrespondence.PresencePenalty &&
				options.Temperature == Options.BusinessCorrespondence.Temperature &&
				options.TopP == Options.BusinessCorrespondence.TopP)
			{
				comboBox1.SelectedIndex = 4;
			}
			else if (options.FrequencyPenalty == Options.StandardChat.FrequencyPenalty &&
				options.PresencePenalty == Options.StandardChat.PresencePenalty &&
				options.Temperature == Options.StandardChat.Temperature &&
				options.TopP == Options.StandardChat.TopP)
			{
				comboBox1.SelectedIndex = 5;
			}
			else if (options.FrequencyPenalty == Options.CopywritingAndBlogging.FrequencyPenalty &&
				options.PresencePenalty == Options.CopywritingAndBlogging.PresencePenalty &&
				options.Temperature == Options.CopywritingAndBlogging.Temperature &&
				options.TopP == Options.CopywritingAndBlogging.TopP)
			{
				comboBox1.SelectedIndex = 6;
			}
			else if (options.FrequencyPenalty == Options.Brainstorming.FrequencyPenalty &&
				options.PresencePenalty == Options.Brainstorming.PresencePenalty &&
				options.Temperature >= 1.2f && options.Temperature <= 1.5f &&
				options.TopP == Options.Brainstorming.TopP)
			{
				brainstorOptions = Options.GetBrainstorming(temperature: options.Temperature.GetValueOrDefault());
				comboBox1.SelectedIndex = 7;
			}
			else
			{
				defaultOptions = options;
				comboBox1.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Максимальное количество токенов в ответе..
		/// Значение по умолчанию 4096.
		/// </summary>
		public int MaxOutputTokenCount => (int)maxOutputTokenCountNumericUpDown.Value;

		/// <summary>
		/// Frequency penalty ограничивает токены в зависимости от того, как часто они встречаются в тексте на данный момент.
		/// Если вы присутствует чрезмерное использование одних и тех же слов в сгенерированном результате, возможно,
		/// следует увеличить значение этого параметра.
		/// Значения от -2 до 2. Значение по умолчанию: 0.
		/// </summary>
		public float FrequencyPenalty => (float)frequencyPenaltyNumericUpDown.Value;

		/// <summary>
		/// Presence penalty ограничивает токены на основании того, появляются ли они в сгенерированном тексте до сих пор,
		/// независимо от того, как часто они встречаются.
		/// Значения от -2.0 до 2.0. Значение по умолчанию: 0.
		/// </summary>
		public float PresencePenalty => (float)presencePenaltyNumericUpDown.Value;

		/// <summary>
		/// Temperature контролирует случайность и креативность генерируемого текста. Низкие значения делают модель более
		/// детерминированной и ориентированной на наиболее вероятные ответы. Это подходит для задач, требующих точности
		/// и согласованности, например, для ответов на фактические вопросы. Высокие значения вносят креативность и 
		/// разнообразие, позволяя модели исследовать менее вероятные варианты. Это полезно для творческого письма, 
		/// мозгового штурма, создания стихов.
		/// Диапазон температур обычно составляет от 0.0 до 2.0. Значение по умолчанию: 0.
		/// </summary>
		public float Temperature => (float)temperatureNumericUpDown.Value;

		/// <summary>
		/// Top-P (nucleus sampling) — метод сэмплирования, который управляет уровнем случайности и креативности при 
		/// выборе следующего токена в генерируемой последовательности. Высокое значение p (близкое к 1) включает больше токенов
		/// с меньшими вероятностями. Результат становится более случайным и разнообразным, но может иногда терять связность
		/// или релевантность. Низкое значение p(например, 0,5 или 0,7) включает меньше самых вероятных токенов. Результат
		/// более предсказуемый, сфокусированный, но может быть менее интересным и склонным к повторениям.
		/// Диапазон от 0.0 до 1.0. Значение по умолчанию: 0.
		/// </summary>
		public float TopP => (float)topPNumericUpDown.Value;
		
		private void FrequencyPenalty_ValueChanged(object sender, EventArgs e)
		{
			if (sender is NumericUpDown numericUpDown)
			{
				if (frequencyPenaltyTrackBar.Value != (int)(numericUpDown.Value * 10))
					frequencyPenaltyTrackBar.Value = (int)(numericUpDown.Value * 10);
			}
			else if (sender is TrackBar trackBar)
			{
				if (frequencyPenaltyNumericUpDown.Value != (decimal)trackBar.Value / 10)
					frequencyPenaltyNumericUpDown.Value = (decimal)trackBar.Value / 10;
			}
			SetOptions(ChatOptions);
		}

		private void PresencePenalty_ValueChanged(object sender, EventArgs e)
		{
			if (sender is NumericUpDown numericUpDown)
			{
				if (presencePenaltyTrackBar.Value != (int)(numericUpDown.Value * 10))
					presencePenaltyTrackBar.Value = (int)(numericUpDown.Value * 10);
			}
			else if (sender is TrackBar trackBar)
			{
				if (presencePenaltyNumericUpDown.Value != (decimal)trackBar.Value / 10)
					presencePenaltyNumericUpDown.Value = (decimal)trackBar.Value / 10;
			}
			SetOptions(ChatOptions);
		}

		private void Temperature_ValueChanged(object sender, EventArgs e)
		{
			if (sender is NumericUpDown numericUpDown)
			{
				if (temperatureTrackBar.Value != (int)(numericUpDown.Value * 10))
					temperatureTrackBar.Value = (int)(numericUpDown.Value * 10);
			}
			else if (sender is TrackBar trackBar)
			{
				if (temperatureNumericUpDown.Value != (decimal)trackBar.Value / 10)
					temperatureNumericUpDown.Value = (decimal)trackBar.Value / 10;
			}
			SetOptions(ChatOptions);
		}

		private void TopP_ValueChanged(object sender, EventArgs e)
		{
			if (sender is NumericUpDown numericUpDown)
			{
				if (topPTrackBar.Value != (int)(numericUpDown.Value * 10))
					topPTrackBar.Value = (int)(numericUpDown.Value * 10);
			}
			else if (sender is TrackBar trackBar)
			{
				if (topPNumericUpDown.Value != (decimal)trackBar.Value / 10)
					topPNumericUpDown.Value = (decimal)trackBar.Value / 10;
			}
			SetOptions(ChatOptions);
		}

		private void Options_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			if (comboBox.SelectedIndex == 0)
			{
				SetOptions(defaultOptions);
			}
			else if (comboBox.SelectedIndex == 7)
			{
				SetOptions(brainstorOptions);
			}
			else if (comboBox.SelectedItem != null)
			{
				Options options = (Options)comboBox.SelectedItem;
				SetOptions(options);
			}			
		}
	}
}
