using ControlLibrary.Controls.ListControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{

	[ToolboxBitmap(typeof(PictureBox))]
	[ComVisible(false)]
	public partial class PatternsWizardBox : UserControl
	{
		private string text;
		private readonly Regex regexDecimal;
		private bool isCorrect;

		private static readonly Size BUTTON_SIZE = new Size((int)(SystemInformation.MenuHeight * 3.2), (int)(SystemInformation.MenuHeight * 1.2));
		private static readonly Size SMALL_BUTTON_SIZE = new Size(SystemInformation.MenuHeight, SystemInformation.MenuHeight);

		[Category("Action"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<EventArgs> IsCorrectChanged;

		public PatternsWizardBox()
		{
			regexDecimal = new Regex("\\d+([,]\\d+)?", RegexOptions.IgnoreCase & RegexOptions.Multiline);

			InitializeComponent();

			NewKeyword();

			isCorrect = false;
			text = string.Empty;
		}
		
		public bool IsDecimal
		{
			get => decimalResultTextBox.Visible;
			set => decimalResultTextBox.Visible = value;
		}

		public new string Text
		{
			get { return text; }
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

		public IEnumerable<string> GetKeywords()
		{
			return from string item in keywordsListBox.Items select item as string;
		}

		/// <summary>
		/// Получить коллекцию ключевых слов.
		/// </summary>
		/// <param name="keyword">Коллекция ключевых слов.</param>
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
		private void SelectKeyword(string keyword)
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
			}
			else
			{
				patternsListBox.Items.Add(string.Empty);
			}
			patternsListBox.SelectedIndex = 0;
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
			NewKeyword();
		}

		/// <summary>
		/// Удалить ключевое слово.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveKeywordButton_Click(object sender, EventArgs e)
		{
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
		/// Добавить пустую коллекцию паттернов.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddPatternButton_Click(object sender, EventArgs e)
		{
			NewPattern();
		}

		/// <summary>
		/// Удалить коллекцию паттернов.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeletePatternButton_Click(object sender, EventArgs e)
		{
			RemoveSelectedItem(patternsListBox);
			CopyPatternToKeyword();
			removePatternButton.Enabled = patternsListBox.SelectedIndex >= 0;
		}


		private void KeywordsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (keywordsListBox.SelectedIndex >= 0)
			{
				SelectKeyword(keywordsListBox.SelectedItem as string);
			}			
		}

		private void PatternsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			expressionTextBox.TextChanged -= ExpressionTextBox_TextChanged;
			if (patternsListBox.SelectedIndex >= 0)
			{
				expressionTextBox.Text = patternsListBox.Items[patternsListBox.SelectedIndex] as string;
				expressionTextBox.SelectionStart = expressionTextBox.Text.Length;
				expressionTextBox.SelectionLength = 0;
			}			
			expressionTextBox.TextChanged += new EventHandler(ExpressionTextBox_TextChanged);
			SetText();
		}

		private void ExpressionTextBox_TextChanged(object sender, EventArgs e)
		{
			int tmp = patternsListBox.SelectedIndex;
			patternsListBox.BeginUpdate();
			TextBox textBox = sender as TextBox;
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
			if (IsDecimal) patterns = patterns.Union(new string[] { "'\\d+([,]\\d+)?'" });
			keywordsListBox.Items[keywordsListBox.SelectedIndex] = string.Join(";", patterns);
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

			if (IsMatch(text: text, patterns: GetPatterns().ToArray(), index: patternsListBox.Items.Count - 1, out  result))
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
		/// Получить коллекцию паттернов из <see cref="ListBox"/> 
		/// </summary>
		/// <returns></returns>
		private IEnumerable<string> GetPatterns()
		{
			return from string item in patternsListBox.Items select item as string;
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

			if (listBox.Items.Count == 0)
			{
				listBox.Items.Add(string.Empty);
				listBox.SelectedIndex = 0;
			}
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
			removePatternButton.Location = new Point(splitContainer1.Panel1.Width - removePatternButton.Width - 2, splitContainer1.Panel1.Height - removePatternButton.Height);
			addPatternButton.Location = new Point(removePatternButton.Left - addPatternButton.Width - 2, splitContainer1.Panel1.Height - addPatternButton.Height);
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.patternsLabel = new System.Windows.Forms.Label();
			this.keywordsLabel = new System.Windows.Forms.Label();
			this.patternsListBox = new System.Windows.Forms.ListBox();
			this.keywordsListBox = new System.Windows.Forms.ListBox();
			this.addKeywordButton = new System.Windows.Forms.Button();
			this.removeKeywordButton = new System.Windows.Forms.Button();
			this.removePatternButton = new System.Windows.Forms.Button();
			this.addPatternButton = new System.Windows.Forms.Button();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.expressionTextBox = new System.Windows.Forms.TextBox();
			this.expressionLabel = new System.Windows.Forms.Label();
			this.resultTextBox = new System.Windows.Forms.TextBox();
			this.resultLabel = new System.Windows.Forms.Label();
			this.textTextBox = new System.Windows.Forms.TextBox();
			this.textLabel = new System.Windows.Forms.Label();
			this.decimalResultTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.patternsLabel);
			this.splitContainer1.Panel1.Controls.Add(this.patternsListBox);
			this.splitContainer1.Panel1.Controls.Add(this.keywordsLabel);
			this.splitContainer1.Panel1.Controls.Add(this.keywordsListBox);
			this.splitContainer1.Panel1.Controls.Add(this.addKeywordButton);
			this.splitContainer1.Panel1.Controls.Add(this.removeKeywordButton);
			this.splitContainer1.Panel1.Controls.Add(this.decimalResultTextBox);
			this.splitContainer1.Panel1.Controls.Add(this.addPatternButton);
			this.splitContainer1.Panel1.Controls.Add(this.removePatternButton);
			this.splitContainer1.Panel1.Resize += new System.EventHandler(this.SplitContainer1_Panel1_Resize);
			this.splitContainer1.Panel1MinSize = 80;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.textLabel);
			this.splitContainer1.Panel2.Controls.Add(this.textTextBox);
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel2.Resize += new System.EventHandler(this.SplitContainer1_Panel2_Resize);
			this.splitContainer1.Panel2MinSize = 200;
			this.splitContainer1.Size = new System.Drawing.Size(762, 491);
			this.splitContainer1.SplitterDistance = 167;
			this.splitContainer1.TabIndex = 0;
			// 
			// patternsLabel
			// 
			this.patternsLabel.AutoSize = true;
			this.patternsLabel.Location = new System.Drawing.Point(3, 219);
			this.patternsLabel.Name = "patternsLabel";
			this.patternsLabel.Size = new System.Drawing.Size(63, 20);
			this.patternsLabel.TabIndex = 0;
			this.patternsLabel.Text = "Этапы:";
			// 
			// keywordsLabel
			// 
			this.keywordsLabel.AutoSize = true;
			this.keywordsLabel.Location = new System.Drawing.Point(3, 0);
			this.keywordsLabel.Name = "keywordsLabel";
			this.keywordsLabel.Size = new System.Drawing.Size(90, 20);
			this.keywordsLabel.TabIndex = 1;
			this.keywordsLabel.Text = "Паттерны:";
			// 
			// addKeywordButton
			// 
			this.addKeywordButton.Location = new System.Drawing.Point(178, 451);
			this.addKeywordButton.Name = "addKeywordButton";
			this.addKeywordButton.Size = new System.Drawing.Size(33, 33);
			this.addKeywordButton.TabIndex = 6;
			this.addKeywordButton.Text = "+";
			this.addKeywordButton.UseVisualStyleBackColor = true;
			this.addKeywordButton.Click += new System.EventHandler(this.AddKeywordButton_Click);
			// 
			// removeKeywordButton
			// 
			this.removeKeywordButton.Location = new System.Drawing.Point(178, 451);
			this.removeKeywordButton.Name = "removeKeywordButton";
			this.removeKeywordButton.Size = new System.Drawing.Size(33, 33);
			this.removeKeywordButton.TabIndex = 6;
			this.removeKeywordButton.Text = "-";
			this.removeKeywordButton.UseVisualStyleBackColor = true;
			this.removeKeywordButton.Click += new System.EventHandler(this.RemoveKeywordButton_Click);
			// 
			// patternsListBox
			// 
			this.patternsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.patternsListBox.FormattingEnabled = true;
			this.patternsListBox.ItemHeight = 20;
			this.patternsListBox.Location = new System.Drawing.Point(0, 242);
			this.patternsListBox.Name = "patternsListBox";
			this.patternsListBox.Size = new System.Drawing.Size(167, 242);
			this.patternsListBox.TabIndex = 3;
			this.patternsListBox.SelectedIndexChanged += new System.EventHandler(this.PatternsListBox_SelectedIndexChanged);
			// 
			// keywordsListBox
			// 
			this.keywordsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.keywordsListBox.FormattingEnabled = true;
			this.keywordsListBox.ItemHeight = 20;
			this.keywordsListBox.Location = new System.Drawing.Point(0, 40);
			this.keywordsListBox.Name = "keywordsListBox";
			this.keywordsListBox.Size = new System.Drawing.Size(167, 182);
			this.keywordsListBox.TabIndex = 2;
			this.keywordsListBox.SelectedIndexChanged += new System.EventHandler(this.KeywordsListBox_SelectedIndexChanged);
			// 
			// removePatternButton
			// 
			this.removePatternButton.Location = new System.Drawing.Point(178, 451);
			this.removePatternButton.Name = "removePatternButton";
			this.removePatternButton.Size = new System.Drawing.Size(98, 33);
			this.removePatternButton.TabIndex = 6;
			this.removePatternButton.Text = "-";
			this.removePatternButton.UseVisualStyleBackColor = true;
			this.removePatternButton.Click += new System.EventHandler(this.DeletePatternButton_Click);
			// 
			// addPatternButton
			// 
			this.addPatternButton.Location = new System.Drawing.Point(490, 452);
			this.addPatternButton.Name = "addPatternButton";
			this.addPatternButton.Size = new System.Drawing.Size(98, 33);
			this.addPatternButton.TabIndex = 3;
			this.addPatternButton.Text = "+";
			this.addPatternButton.UseVisualStyleBackColor = true;
			this.addPatternButton.Click += new System.EventHandler(this.AddPatternButton_Click);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Location = new System.Drawing.Point(7, 162);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.expressionLabel);
			this.splitContainer2.Panel1.Controls.Add(this.expressionTextBox);
			this.splitContainer2.Panel1.Resize += new System.EventHandler(this.SplitContainer2_Panel1_Resize);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.resultLabel);
			this.splitContainer2.Panel2.Controls.Add(this.resultTextBox);
			this.splitContainer2.Panel2.Resize += new System.EventHandler(this.SplitContainer2_Panel2_Resize);
			this.splitContainer2.Size = new System.Drawing.Size(581, 284);
			this.splitContainer2.SplitterDistance = 40;
			this.splitContainer2.TabIndex = 2;
			// 
			// expressionTextBox
			// 
			this.expressionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.expressionTextBox.Location = new System.Drawing.Point(-2, 23);
			this.expressionTextBox.Multiline = true;
			this.expressionTextBox.Name = "expressionTextBox";
			this.expressionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.expressionTextBox.Size = new System.Drawing.Size(585, 68);
			this.expressionTextBox.TabIndex = 2;
			// 
			// expressionLabel
			// 
			this.expressionLabel.AutoSize = true;
			this.expressionLabel.Location = new System.Drawing.Point(3, 0);
			this.expressionLabel.Name = "expressionLabel";
			this.expressionLabel.Size = new System.Drawing.Size(190, 20);
			this.expressionLabel.TabIndex = 0;
			this.expressionLabel.Text = "Регулярное выражение:";
			// 
			// resultTextBox
			// 
			this.resultTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.resultTextBox.Location = new System.Drawing.Point(-2, 23);
			this.resultTextBox.Multiline = true;
			this.resultTextBox.Name = "resultTextBox";
			this.resultTextBox.ReadOnly = true;
			this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.resultTextBox.Size = new System.Drawing.Size(585, 123);
			this.resultTextBox.TabIndex = 3;
			// 
			// resultLabel
			// 
			this.resultLabel.AutoSize = true;
			this.resultLabel.Location = new System.Drawing.Point(3, 0);
			this.resultLabel.Name = "resultLabel";
			this.resultLabel.Size = new System.Drawing.Size(93, 20);
			this.resultLabel.TabIndex = 1;
			this.resultLabel.Text = "Результат:";
			// 
			// textTextBox
			// 
			this.textTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.textTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textTextBox.Location = new System.Drawing.Point(3, 23);
			this.textTextBox.Multiline = true;
			this.textTextBox.Name = "textTextBox";
			this.textTextBox.ReadOnly = true;
			this.textTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textTextBox.Size = new System.Drawing.Size(585, 120);
			this.textTextBox.TabIndex = 1;
			// 
			// textLabel
			// 
			this.textLabel.AutoSize = true;
			this.textLabel.Location = new System.Drawing.Point(3, 0);
			this.textLabel.Name = "textLabel";
			this.textLabel.Size = new System.Drawing.Size(56, 20);
			this.textLabel.TabIndex = 0;
			this.textLabel.Text = "Текст:";
			// 
			// decimalResultTextBox
			// 
			this.decimalResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.decimalResultTextBox.Location = new System.Drawing.Point(7, 455);
			this.decimalResultTextBox.Name = "decimalResultTextBox";
			this.decimalResultTextBox.ReadOnly = true;
			this.decimalResultTextBox.Size = new System.Drawing.Size(79, 26);
			this.decimalResultTextBox.TextAlign = HorizontalAlignment.Center;
			this.decimalResultTextBox.TabIndex = 7;
			// 
			// PatternsWizardBox
			// 
			this.Name = "PatternsWizardBox";
			this.Load += new System.EventHandler(this.PatternsWizardBox_Load);
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Size = new System.Drawing.Size(762, 491);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);			
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label keywordsLabel;
		private System.Windows.Forms.ListBox keywordsListBox;
		private System.Windows.Forms.Button addKeywordButton;
		private System.Windows.Forms.Button removeKeywordButton;
		private System.Windows.Forms.Label patternsLabel;
		private System.Windows.Forms.ListBox patternsListBox;
		private System.Windows.Forms.Button addPatternButton;
		private System.Windows.Forms.Button removePatternButton;
		private System.Windows.Forms.TextBox decimalResultTextBox;

		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label textLabel;
		private System.Windows.Forms.TextBox textTextBox;
		private System.Windows.Forms.Label expressionLabel;
		private System.Windows.Forms.TextBox expressionTextBox;
		private System.Windows.Forms.Label resultLabel;
		private System.Windows.Forms.TextBox resultTextBox;		
	}
}
