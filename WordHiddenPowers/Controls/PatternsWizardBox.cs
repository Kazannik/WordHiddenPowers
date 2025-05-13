using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#if DEVELOP
namespace PatternsWizardBoxDeveloper.Controls
#else
namespace WordHiddenPowers.Controls
#endif
{

	[ToolboxBitmap(typeof(PictureBox))]
	[ComVisible(false)]
	public partial class PatternsWizardBox : UserControl
	{
		private static readonly Size BUTTON_SIZE = new Size((int)(SystemInformation.MenuHeight * 3.2), (int)(SystemInformation.MenuHeight * 1.2));
		private static readonly Size SMALL_BUTTON_SIZE = new Size(SystemInformation.MenuHeight, SystemInformation.MenuHeight);
		private static readonly Regex regexDecimal = new Regex("\\d+([,]\\d+)?", RegexOptions.IgnoreCase & RegexOptions.Multiline);
		
		private string text;
		private bool isCorrect;


		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> IsCorrectChanged;

		public PatternsWizardBox()
		{
			InitializeComponent();

			expressionTextBox.GotFocus += new EventHandler(Controls_GotFocus);
			keywordsListBox.GotFocus += new EventHandler(Controls_GotFocus);
			patternsListBox.GotFocus += new EventHandler(Controls_GotFocus);

			isCorrect = false;
			IsDecimal = false;
			text = string.Empty;
		}

		private void Controls_GotFocus(object sender, EventArgs e)
		{
			if (sender == expressionTextBox)
			{
				expressionTextBox.TextChanged += new EventHandler(ExpressionTextBox_TextChanged);
				keywordsListBox.SelectedIndexChanged -= KeywordsListBox_SelectedIndexChanged;
				patternsListBox.SelectedIndexChanged -= PatternsListBox_SelectedIndexChanged;
			}
			else if (sender == keywordsListBox)
			{
				keywordsListBox.SelectedIndexChanged += new EventHandler(KeywordsListBox_SelectedIndexChanged);
				patternsListBox.SelectedIndexChanged -= PatternsListBox_SelectedIndexChanged;
				expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			}
			else
			{
				patternsListBox.SelectedIndexChanged += new EventHandler(PatternsListBox_SelectedIndexChanged);
				keywordsListBox.SelectedIndexChanged -= KeywordsListBox_SelectedIndexChanged;
				expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			}
		}

		public bool IsDecimal
		{
			get => decimalResultTextBox.Visible;
			set => decimalResultTextBox.Visible = value;
		}

		public new string Text
		{
			get => text;			
			set
			{
				if (text != value)
				{
					text = value;
					SetText();
				}
			}
		}

		public bool IsCorrect
		{
			get => isCorrect;
			private set
			{
				if (isCorrect != value)
				{
					isCorrect = value;
					OnIsCorrectChanged(new EventArgs());
				}
			}
		}

		protected virtual void OnIsCorrectChanged(EventArgs e)
		{
			IsCorrectChanged?.Invoke(this, e);
		}
		private void SetText()
		{
			if (patternsListBox.SelectedIndex == 0)
			{
				textTextBox.Text = text;
			}
			else
			{
				IsMatch(text: text, patterns: GetPatterns().ToArray(), index: patternsListBox.SelectedIndex - 1, out string tmp);
				textTextBox.Text = tmp;
			}

			if (IsMatch(text: text, patterns: GetPatterns().ToArray(), index: patternsListBox.SelectedIndex, out string result))
			{
				if (IsDecimal)
				{
					addKeywordButton.Enabled = regexDecimal.IsMatch(result);
					addPatternButton.Enabled = regexDecimal.IsMatch(result);
					decimalResultTextBox.Text = regexDecimal.Match(result).Value;
				}
				else
				{
					decimalResultTextBox.Text = string.Empty;
					addKeywordButton.Enabled = true;
					addPatternButton.Enabled = true;
				}
			}
			else
			{
				decimalResultTextBox.Text = string.Empty;
				addKeywordButton.Enabled = false;
				addPatternButton.Enabled = false;
			}
			resultTextBox.Text = result;

			if (IsMatch(text: text, patterns: GetPatterns().ToArray(), index: patternsListBox.Items.Count - 1, out result))
			{
				if (IsDecimal)
				{
					IsCorrect = regexDecimal.IsMatch(result);
				}
				else
				{
					IsCorrect = true;
				}
			}
			else
			{
				IsCorrect = false;
			}
		}

		/// <summary>
		/// Получить коллекцию ключевых слов.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetKeywords()
		{
			return from string item in keywordsListBox.Items select item;
		}

		/// <summary>
		/// Задать построчную коллекцию ключевых слов.
		/// </summary>
		/// <param name="keyword">Построчная коллекция ключевых слов.</param>
		public void SetKeywords(string keyword)
		{
			keywordsListBox.BeginUpdate();
			keywordsListBox.Items.Clear();
			string[] keywords = keyword.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			
			if (keywords.Any())
			{
				foreach (string item in keywords)
				{
					keywordsListBox.Items.Add(item);
				}
			}
			else
			{
				keywordsListBox.Items.Add(string.Empty);
			}
			keywordsListBox.SelectedIndex = 0;
		}

		/// <summary>
		/// Выбрать ключевое слово.
		/// </summary>
		/// <param name="keyword">Ключевое слово.</param>
		private void SetKeyword(string keyword)
		{
			patternsListBox.BeginUpdate();
			patternsListBox.Items.Clear();
			IEnumerable<string> patterns = Services.Searcher.GetPatterns(keyword);
			if (patterns.Any())
			{
				foreach (string item in patterns)
				{
					patternsListBox.Items.Add(item);
				}
				patternsListBox.SelectedIndex = 0;
				expressionTextBox.Text = patternsListBox.Items[patternsListBox.SelectedIndex] as string;
			}
			resultTextBox.Text = string.Empty;
			patternsListBox.EndUpdate();
		}

		/// <summary>
		/// Создать пустое ключевое слово.
		/// </summary>
		private void NewKeyword()
		{
			keywordsListBox.Items.Add(string.Empty);
			keywordsListBox.SelectedIndex = keywordsListBox.Items.Count - 1;

			patternsListBox.Items.Add(string.Empty);
			patternsListBox.SelectedIndex = 0;
		}

		/// <summary>
		/// Добавить пустое ключевое слово.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddKeywordButton_Click(object sender, EventArgs e)
		{
			expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			patternsListBox.SelectedIndexChanged -= PatternsListBox_SelectedIndexChanged;
			keywordsListBox.SelectedIndexChanged += new EventHandler(KeywordsListBox_SelectedIndexChanged);
			NewKeyword();
		}

		/// <summary>
		/// Удалить ключевое слово.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveKeywordButton_Click(object sender, EventArgs e)
		{
			expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			patternsListBox.SelectedIndexChanged -= PatternsListBox_SelectedIndexChanged;
			keywordsListBox.SelectedIndexChanged += new EventHandler(KeywordsListBox_SelectedIndexChanged);
			RemoveSelectedItem(keywordsListBox);			
		}

		/// <summary>
		/// Создать пустой паттерн.
		/// </summary>
		private void NewPattern()
		{
			if (patternsListBox.SelectedIndex + 1 == patternsListBox.Items.Count)
			{
				patternsListBox.Items.Add(string.Empty);
			}
			patternsListBox.SelectedIndex += 1;
			SetText();
		}

		/// <summary>
		/// Добавить пустой паттерн.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddPatternButton_Click(object sender, EventArgs e)
		{
			expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			keywordsListBox.SelectedIndexChanged -= KeywordsListBox_SelectedIndexChanged;
			patternsListBox.SelectedIndexChanged += new EventHandler(PatternsListBox_SelectedIndexChanged);
			NewPattern();
		}

		/// <summary>
		/// Удалить выделенный паттерн.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeletePatternButton_Click(object sender, EventArgs e)
		{
			expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			keywordsListBox.SelectedIndexChanged -= KeywordsListBox_SelectedIndexChanged;
			patternsListBox.SelectedIndexChanged += new EventHandler(PatternsListBox_SelectedIndexChanged);
			RemoveSelectedItem(patternsListBox);
			CopyPatternToKeyword();
			removePatternButton.Enabled = patternsListBox.SelectedIndex >= 0;
		}


		private void KeywordsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (keywordsListBox.SelectedIndex >= 0)
			{
				SetKeyword(keywordsListBox.SelectedItem as string);
			}			
		}

		private void PatternsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (patternsListBox.SelectedIndex >= 0)
			{
				expressionTextBox.Text = patternsListBox.Items[patternsListBox.SelectedIndex] as string;
			}
			SetText();
		}

		private void ExpressionTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			int tmp = patternsListBox.SelectedIndex;
			patternsListBox.BeginUpdate();
			if (patternsListBox.SelectedIndex >= 0)
				patternsListBox.Items[patternsListBox.SelectedIndex] = textBox.Text;
			SetText();
			CopyPatternToKeyword();
			patternsListBox.SelectedIndex = tmp;
			patternsListBox.EndUpdate();
		}
		
		/// <summary>
		/// Копировать паттерны в коллекцию ключевых слов.
		/// </summary>
		private void CopyPatternToKeyword()
		{
			IEnumerable<string> patterns = GetPatterns().Select(s => "'" + s + "'");
			// if (IsDecimal) patterns = patterns.Union(new string[] { "'\\d+([,]\\d+)?'" });
			keywordsListBox.BeginUpdate();
			
			if (patterns.Any() && keywordsListBox.Items.Count == 0)
			{
				keywordsListBox.Items.Add(string.Empty);
				keywordsListBox.SelectedIndex = 0;
			}
			
			if (keywordsListBox.Items.Count > 0) 
				keywordsListBox.Items[keywordsListBox.SelectedIndex] = string.Join(";", patterns);
			
			keywordsListBox.EndUpdate();
		}


		/// <summary>
		/// Получить коллекцию паттернов из <see cref="ListBox"/> 
		/// </summary>
		/// <returns></returns>
		private IEnumerable<string> GetPatterns()
		{
			return from string item in patternsListBox.Items select item;
		}

		/// <summary>
		/// Проверка на последовательное соответствие коллекции регулярных выражений. 
		/// </summary>
		/// <param name="text">Текст</param>
		/// <param name="patterns">Коллекция регулярных выражений.</param>
		/// <param name="index">Индекс последнего из проверяемых регулярных выражений начиная с 0.</param>
		/// <param name="result">Результатирующий текст, который равен выборке или в случае ошибки сообщению об ошибке.</param>
		/// <returns>Результат проверки на соответствие регулярному выражению.</returns>
		private static bool IsMatch(string text, string[] patterns, int index, out string result)
		{
			result = text;
			for (int i = 0; i <= index; i++)
			{
				if (!IsMatch(result, pattern: patterns[i], out result))
				{
					return false;
				}
			}
			return !string.IsNullOrEmpty(result);
		}

		/// <summary>
		/// Проверка на соответствие регулярному выражению.
		/// </summary>
		/// <param name="text">Текст</param>
		/// <param name="pattern">Регулярное выражение.</param>
		/// <param name="result">Результатирующий текст, который равен выборке или в случае ошибки сообщению об ошибке.</param>
		/// <returns>Результат проверки на соответствие регулярному выражению.</returns>
		private static bool IsMatch(string text, string pattern, out string result)
		{
			try
			{
				Regex regex = new Regex(pattern, RegexOptions.IgnoreCase & RegexOptions.Multiline);
				result = regex.Match(text).Value;
				return regex.IsMatch(text) && !string.IsNullOrEmpty(result);
			}
			catch (Exception ex)
			{
				result = ex.Message;
				return false;
			}
		}
				
		/// <summary>
		/// Удалить выделенную позицию.
		/// </summary>
		/// <param name="listBox"></param>
		private static void RemoveSelectedItem(ListBox listBox)
		{
			listBox.BeginUpdate();
			int index = listBox.SelectedIndex;
			listBox.Items.RemoveAt(index);

			if (index > 0)
				listBox.SelectedIndex = index - 1;
			else if (listBox.Items.Count > 0)
				listBox.SelectedIndex = index;

			//if (listBox.Items.Count == 0)
			//{
			//	listBox.Items.Add(string.Empty);
			//	listBox.SelectedIndex = 0;
			//}
			listBox.EndUpdate();
		}
	}

	partial class PatternsWizardBox
	{

		#region Resize Controls

		private void PatternsWizardBox_Load(object sender, EventArgs e)
		{
			splitContainer1.SplitterDistance += 1;
		}

		private void SplitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			addKeywordButton.Size = SMALL_BUTTON_SIZE;
			removeKeywordButton.Size = SMALL_BUTTON_SIZE;
			addPatternButton.Size = SMALL_BUTTON_SIZE;
			removePatternButton.Size = SMALL_BUTTON_SIZE;
			decimalResultTextBox.Size = BUTTON_SIZE;

			keywordsLabel.Location = new Point(0, 0);
			keywordsListBox.Location = new Point(0, keywordsLabel.Top + keywordsLabel.Height);
			keywordsListBox.Size = new Size(splitContainer1.Panel1.Width, SystemInformation.MenuHeight * 4);
			
			removeKeywordButton.Location = new Point(splitContainer1.Panel1.Width - removeKeywordButton.Width - 2, keywordsListBox.Top + keywordsListBox.Height + 2);
			addKeywordButton.Location = new Point(removeKeywordButton.Left - addKeywordButton.Width - 2, keywordsListBox.Top + keywordsListBox.Height + 2);

			patternsLabel.Location = new Point(0, addKeywordButton.Top + addKeywordButton.Height + 2);
			patternsListBox.Location = new Point(0, patternsLabel.Top + patternsLabel.Height);
			
			removePatternButton.Location = new Point(splitContainer1.Panel1.Width - removePatternButton.Width - 2, splitContainer1.Panel1.Height - removePatternButton.Height - 2);
			addPatternButton.Location = new Point(removePatternButton.Left - addPatternButton.Width - 2, splitContainer1.Panel1.Height - addPatternButton.Height - 2);
			
			decimalResultTextBox.Location = new Point(0, addPatternButton.Top + 2);
			patternsListBox.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height - patternsListBox.Top - addPatternButton.Height - 2);

			splitContainer1.Panel1MinSize = SystemInformation.MenuHeight * 6;
		}

		private void SplitContainer1_Panel2_Resize(object sender, EventArgs e)
		{
			textLabel.Location = new Point(0, 0);
			textTextBox.Location = new Point(0, textLabel.Top + textLabel.Height);
			textTextBox.Size = new Size(splitContainer1.Panel2.Width, SystemInformation.MenuHeight * 4);

			splitContainer2.Location = new Point(0, textTextBox.Top + textTextBox.Height);
			splitContainer2.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - textLabel.Height - textTextBox.Height);

			splitContainer2.Panel1MinSize = SystemInformation.MenuHeight * 3;
		}

		private void SplitContainer2_Panel1_Resize(object sender, EventArgs e)
		{
			expressionLabel.Location = new Point(0, 0);
			expressionTextBox.Location = new Point(0, expressionLabel.Height);
			expressionTextBox.Size = new Size(splitContainer2.Panel1.Width, splitContainer2.Panel1.Height - expressionLabel.Height);
		}

		private void SplitContainer2_Panel2_Resize(object sender, EventArgs e)
		{
			resultLabel.Location = new Point(0, 0);
			resultTextBox.Location = new Point(0, resultLabel.Height);
			resultTextBox.Size = new Size(splitContainer2.Panel2.Width, splitContainer2.Panel2.Height - resultLabel.Height);
		}

		#endregion

		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private readonly IContainer components = null;

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
			splitContainer1 = new SplitContainer();
			patternsLabel = new Label();
			keywordsLabel = new Label();
			patternsListBox = new ListBox();
			keywordsListBox = new ListBox();
			addKeywordButton = new Button();
			removeKeywordButton = new Button();
			removePatternButton = new Button();
			addPatternButton = new Button();
			splitContainer2 = new SplitContainer();
			expressionTextBox = new TextBox();
			expressionLabel = new Label();
			resultTextBox = new TextBox();
			resultLabel = new Label();
			textTextBox = new TextBox();
			textLabel = new Label();
			decimalResultTextBox = new TextBox();
			(splitContainer1 as ISupportInitialize).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			(splitContainer2 as ISupportInitialize).BeginInit();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(patternsLabel);
			splitContainer1.Panel1.Controls.Add(patternsListBox);
			splitContainer1.Panel1.Controls.Add(keywordsLabel);
			splitContainer1.Panel1.Controls.Add(keywordsListBox);
			splitContainer1.Panel1.Controls.Add(addKeywordButton);
			splitContainer1.Panel1.Controls.Add(removeKeywordButton);
			splitContainer1.Panel1.Controls.Add(decimalResultTextBox);
			splitContainer1.Panel1.Controls.Add(addPatternButton);
			splitContainer1.Panel1.Controls.Add(removePatternButton);
			splitContainer1.Panel1.Resize += new EventHandler(SplitContainer1_Panel1_Resize);
			splitContainer1.Panel1MinSize = 80;
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(textLabel);
			splitContainer1.Panel2.Controls.Add(textTextBox);
			splitContainer1.Panel2.Controls.Add(splitContainer2);
			splitContainer1.Panel2.Resize += new EventHandler(SplitContainer1_Panel2_Resize);
			splitContainer1.Panel2MinSize = 200;
			splitContainer1.Size = new Size(762, 491);
			splitContainer1.SplitterDistance = 167;
			splitContainer1.TabIndex = 0;
			// 
			// patternsLabel
			// 
			patternsLabel.AutoSize = true;
			patternsLabel.Location = new Point(3, 219);
			patternsLabel.Name = "patternsLabel";
			patternsLabel.Size = new Size(63, 20);
			patternsLabel.TabIndex = 0;
			patternsLabel.Text = "Этапы:";
			// 
			// keywordsLabel
			// 
			keywordsLabel.AutoSize = true;
			keywordsLabel.Location = new Point(3, 0);
			keywordsLabel.Name = "keywordsLabel";
			keywordsLabel.Size = new Size(90, 20);
			keywordsLabel.TabIndex = 1;
			keywordsLabel.Text = "Паттерны:";
			// 
			// addKeywordButton
			// 
			addKeywordButton.Location = new Point(178, 451);
			addKeywordButton.Name = "addKeywordButton";
			addKeywordButton.Size = new Size(33, 33);
			addKeywordButton.TabIndex = 6;
			addKeywordButton.Text = "+";
			addKeywordButton.UseVisualStyleBackColor = true;
			addKeywordButton.Click += new EventHandler(AddKeywordButton_Click);
			// 
			// removeKeywordButton
			// 
			removeKeywordButton.Location = new Point(178, 451);
			removeKeywordButton.Name = "removeKeywordButton";
			removeKeywordButton.Size = new Size(33, 33);
			removeKeywordButton.TabIndex = 6;
			removeKeywordButton.Text = "-";
			removeKeywordButton.UseVisualStyleBackColor = true;
			removeKeywordButton.Click += new EventHandler(RemoveKeywordButton_Click);
			// 
			// patternsListBox
			// 
			patternsListBox.BorderStyle = BorderStyle.FixedSingle;
			patternsListBox.FormattingEnabled = true;
			patternsListBox.ItemHeight = 20;
			patternsListBox.Location = new Point(0, 242);
			patternsListBox.Name = "patternsListBox";
			patternsListBox.Size = new Size(167, 242);
			patternsListBox.TabIndex = 3;
			patternsListBox.SelectedIndexChanged += new EventHandler(PatternsListBox_SelectedIndexChanged);
			// 
			// keywordsListBox
			// 
			keywordsListBox.BorderStyle = BorderStyle.FixedSingle;
			keywordsListBox.FormattingEnabled = true;
			keywordsListBox.ItemHeight = 20;
			keywordsListBox.Location = new Point(0, 40);
			keywordsListBox.Name = "keywordsListBox";
			keywordsListBox.Size = new Size(167, 182);
			keywordsListBox.TabIndex = 2;
			keywordsListBox.SelectedIndexChanged += new EventHandler(KeywordsListBox_SelectedIndexChanged);
			// 
			// removePatternButton
			// 
			removePatternButton.Location = new Point(178, 451);
			removePatternButton.Name = "removePatternButton";
			removePatternButton.Size = new Size(98, 33);
			removePatternButton.TabIndex = 6;
			removePatternButton.Text = "-";
			removePatternButton.UseVisualStyleBackColor = true;
			removePatternButton.Click += new EventHandler(DeletePatternButton_Click);
			// 
			// addPatternButton
			// 
			addPatternButton.Location = new Point(490, 452);
			addPatternButton.Name = "addPatternButton";
			addPatternButton.Size = new Size(98, 33);
			addPatternButton.TabIndex = 3;
			addPatternButton.Text = "+";
			addPatternButton.UseVisualStyleBackColor = true;
			addPatternButton.Click += new EventHandler(AddPatternButton_Click);
			// 
			// splitContainer2
			// 
			splitContainer2.Location = new Point(7, 162);
			splitContainer2.Name = "splitContainer2";
			splitContainer2.Orientation = Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			splitContainer2.Panel1.Controls.Add(expressionLabel);
			splitContainer2.Panel1.Controls.Add(expressionTextBox);
			splitContainer2.Panel1.Resize += new EventHandler(SplitContainer2_Panel1_Resize);
			// 
			// splitContainer2.Panel2
			// 
			splitContainer2.Panel2.Controls.Add(resultLabel);
			splitContainer2.Panel2.Controls.Add(resultTextBox);
			splitContainer2.Panel2.Resize += new EventHandler(SplitContainer2_Panel2_Resize);
			splitContainer2.Size = new Size(581, 284);
			splitContainer2.SplitterDistance = 40;
			splitContainer2.TabIndex = 2;
			// 
			// expressionTextBox
			// 
			expressionTextBox.BorderStyle = BorderStyle.FixedSingle;
			expressionTextBox.Location = new Point(-2, 23);
			expressionTextBox.Multiline = true;
			expressionTextBox.Name = "expressionTextBox";
			expressionTextBox.ScrollBars = ScrollBars.Vertical;
			expressionTextBox.Size = new Size(585, 68);
			expressionTextBox.TabIndex = 2;
			// 
			// expressionLabel
			// 
			expressionLabel.AutoSize = true;
			expressionLabel.Location = new Point(3, 0);
			expressionLabel.Name = "expressionLabel";
			expressionLabel.Size = new Size(190, 20);
			expressionLabel.TabIndex = 0;
			expressionLabel.Text = "Регулярное выражение:";
			// 
			// resultTextBox
			// 
			resultTextBox.BackColor = SystemColors.Window;
			resultTextBox.BorderStyle = BorderStyle.FixedSingle;
			resultTextBox.Location = new Point(-2, 23);
			resultTextBox.Multiline = true;
			resultTextBox.Name = "resultTextBox";
			resultTextBox.ReadOnly = true;
			resultTextBox.ScrollBars = ScrollBars.Vertical;
			resultTextBox.Size = new Size(585, 123);
			resultTextBox.TabIndex = 3;
			// 
			// resultLabel
			// 
			resultLabel.AutoSize = true;
			resultLabel.Location = new Point(3, 0);
			resultLabel.Name = "resultLabel";
			resultLabel.Size = new Size(93, 20);
			resultLabel.TabIndex = 1;
			resultLabel.Text = "Результат:";
			// 
			// textTextBox
			// 
			textTextBox.BackColor = SystemColors.Window;
			textTextBox.BorderStyle = BorderStyle.FixedSingle;
			textTextBox.Location = new Point(3, 23);
			textTextBox.Multiline = true;
			textTextBox.Name = "textTextBox";
			textTextBox.ReadOnly = true;
			textTextBox.ScrollBars = ScrollBars.Vertical;
			textTextBox.Size = new Size(585, 120);
			textTextBox.TabIndex = 1;
			// 
			// textLabel
			// 
			textLabel.AutoSize = true;
			textLabel.Location = new Point(3, 0);
			textLabel.Name = "textLabel";
			textLabel.Size = new Size(56, 20);
			textLabel.TabIndex = 0;
			textLabel.Text = "Текст:";
			// 
			// decimalResultTextBox
			// 
			decimalResultTextBox.BorderStyle = BorderStyle.FixedSingle;
			decimalResultTextBox.Location = new Point(7, 455);
			decimalResultTextBox.Name = "decimalResultTextBox";
			decimalResultTextBox.ReadOnly = true;
			decimalResultTextBox.Size = new Size(79, 26);
			decimalResultTextBox.TextAlign = HorizontalAlignment.Center;
			decimalResultTextBox.TabIndex = 7;
			// 
			// PatternsWizardBox
			// 
			Name = "PatternsWizardBox";
			Load += new EventHandler(PatternsWizardBox_Load);
			AutoScaleDimensions = new SizeF(9F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(splitContainer1);
			Size = new Size(762, 491);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.Panel2.PerformLayout();
			(splitContainer1 as ISupportInitialize).EndInit();
			splitContainer1.ResumeLayout(false);
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel1.PerformLayout();
			splitContainer2.Panel2.ResumeLayout(false);
			splitContainer2.Panel2.PerformLayout();
			(splitContainer2 as ISupportInitialize).EndInit();
			splitContainer2.ResumeLayout(false);			
			ResumeLayout(false);
		}

		#endregion

		private SplitContainer splitContainer1;
		private Label keywordsLabel;
		private ListBox keywordsListBox;
		private	Button addKeywordButton;
		private Button removeKeywordButton;
		private Label patternsLabel;
		private	ListBox patternsListBox;
		private Button addPatternButton;
		private	Button removePatternButton;
		private TextBox decimalResultTextBox;

		private SplitContainer splitContainer2;
		private Label textLabel;
		private TextBox textTextBox;
		private Label expressionLabel;
		private TextBox expressionTextBox;
		private Label resultLabel;
		private TextBox resultTextBox;
	}
}
