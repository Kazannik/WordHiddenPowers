// Ignore Spelling: uri Dialogs

using LLMConnectorLibrary;
using LLMConnectorLibrary.EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using static LLMConnectorLibrary.LLMOpenAI;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	delegate void MessageDelegate(LLMClient client, string model, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag);
	delegate void EmbedStoreDelegate(LLMClient client, string model, IEnumerable<(int key, string description)> store, object tag);
	delegate void EmbedDataSetDelegate(LLMClient client, string model, RepositoryDataSet repositoryDataSet, VectorDataSet vectorDataSet, object tag);

	public delegate void InsertTextDelegate(Word.Range range, string prefix, string text, string postfix);

	public partial class LLMProcessDialog : Form
	{
		private readonly LLMClient _client;
		private bool _hostIsAvailable;
		private IEnumerable<string> _modelsName;

		private readonly MessageDelegate sendMessageDelegate;
		private readonly EmbedStoreDelegate embedStoreDelegate;
		private readonly EmbedDataSetDelegate embedDataSetDelegate;

		private readonly string _model;
		private readonly ChatOptions _options;
		private readonly string _systemMessage;
		private readonly IEnumerable<string> _userMessages;
		private readonly IEnumerable<(int key, string description)> _store;

		private readonly RepositoryDataSet _repositoryDataSet;
		private readonly VectorDataSet _vectorDataSet;

		private readonly object _tag;

		public LLMProcessDialog(Uri uri, TimeSpan timeout)
		{
			_hostIsAvailable = false;
			_modelsName = new string[] { };

			InitializeComponent();

			_client = new LLMClient(uri: uri, timeout: timeout);

			_client.HostChecked += new EventHandler<CheckHostEventArgs>(Client_HostChecked);
			_client.ModelsCollectionCompleted += new EventHandler<ModelsCollectionCompletedEventArgs>(Client_ModelsCollectionCompleted);
			_client.CheckHost();

			_client.ChatProgress += new EventHandler<ChatProgressEventArgs>(Client_ChatProgress);
			_client.ChatCompleted += new EventHandler<ChatCompletedEventArgs>(Client_ChatCompleted);
			_client.ChatCanceled += new EventHandler<ChatCanceledEventArgs>(Client_ChatCanceled);

			_client.EmbedProgress += new EventHandler<EmbedProgressEventArgs>(Client_EmbedProgress);
			_client.EmbedCompleted += new EventHandler<EmbedCompletedEventArgs>(Client_EmbedCompleted);
			_client.EmbedCanceled += new EventHandler<EmbedCanceledEventArgs>(Client_EmbedCanceled);
		}

		/// <summary>
		/// Вызов модели в режиме чата.
		/// </summary>
		/// <param name="uri">API</param>
		/// <param name="model">Модель</param>
		/// <param name="options">Настройки работы модели</param>
		/// <param name="systemMessage">Системный промпт</param>
		/// <param name="userMessages">Пользовательские промпты</param>
		/// <param name="tag">Определяемые пользователем данные</param>
		public LLMProcessDialog(Uri uri, TimeSpan timeout, string model, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag) : this(uri: uri, timeout: timeout)
		{
			_model = model;
			_options = options;
			_systemMessage = systemMessage;
			_userMessages = userMessages;
			_tag = tag;
			sendMessageDelegate = OnSendMessage;
		}

		public readonly struct Arguments
		{
			public Arguments (InsertTextDelegate insertTextFunction, Word.Range range, string prefix, string postfix)
			{
				InsertTextFunction = insertTextFunction;
				Range = range;
				Prefix = prefix;
				Postfix = postfix;
			}

			public InsertTextDelegate InsertTextFunction { get; }
			public Word.Range Range { get; }
			public string Prefix { get; }
			public string Postfix { get; }
		}

		public LLMProcessDialog(Uri uri, TimeSpan timeout, string model, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, Arguments arg) : this(uri: uri, timeout: timeout)
		{
			_model = model;
			_options = options;
			_systemMessage = systemMessage;
			_userMessages = userMessages;
			_tag = arg;
			sendMessageDelegate = OnSendMessage;
		}

		public LLMProcessDialog(Uri uri, TimeSpan timeout, string model, string input, object tag) :
			this(uri: uri, timeout: timeout, model: model, store: new (int key, string description)[] { (1, input) }, tag: tag) { }

		public LLMProcessDialog(Uri uri, TimeSpan timeout, string model, IEnumerable<(int key, string description)> store, object tag) : this(uri: uri, timeout: timeout)
		{
			_model = model;
			_store = store;
			_tag = tag;
			embedStoreDelegate = OnEmbed;
		}

		public LLMProcessDialog(Uri uri, TimeSpan timeout, string model, RepositoryDataSet repositoryDataSet, VectorDataSet vectorDataSet, object tag) : this(uri: uri, timeout: timeout)
		{
			_model = model;
			_repositoryDataSet = repositoryDataSet;
			_vectorDataSet = vectorDataSet;
			_tag = tag;
			embedDataSetDelegate = OnEmbedDataSet;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			SizeF size = graphics.MeasureString(_model, Font);
			graphics.DrawString(_model, Font, new SolidBrush(ForeColor), Height + 10, (Height - size.Height) / 2);
			graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(e.ClipRectangle.Location, new Size(e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)));
			base.OnPaint(e);
		}

		public bool HostIsAvailable => _hostIsAvailable;

		public IEnumerable<string> ModelNames => _modelsName;

		private static void OnSendMessage(LLMClient client, string model, ChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			client.Send(model: model, options: options, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
		}

		private static void OnEmbed(LLMClient client, string model, string input, object tag)
		{
			client.Embed(model: model, input: input, tag: tag);
		}

		private static void OnEmbed(LLMClient client, string model, IEnumerable<(int key, string description)> store, object tag)
		{
			client.Embed(model: model, store: store, tag: tag);
		}

		private static void OnEmbedDataSet(LLMClient client, string model, RepositoryDataSet sourceDataSet, VectorDataSet vectorDataSet, object tag)
		{
			IEnumerable<(int key, string description)> store =
				sourceDataSet.GetTextNotes()
				.Select((note, key) => (key: key + 1, description: note.Value as string));

			client.Embed(model: model, store: store, tag: tag);
		}

		private void Client_HostChecked(object sender, CheckHostEventArgs e)
		{
			_hostIsAvailable = e.IsAvailable;
			if (_hostIsAvailable)
				_client.ReadModelsName();
			else
			{
				Utils.Dialogs.ShowErrorDialog(string.Format("Проверьте доступ к провайдеру: {0}.\nКажется этот хост не доступен!", e.Uri.ToString()));
				Close();
			}
		}

		private void Client_ModelsCollectionCompleted(object sender, ModelsCollectionCompletedEventArgs e)
		{
			_modelsName = e.Models;

			sendMessageDelegate?.Invoke(_client, _model, _options, _systemMessage, _userMessages, _tag);
			embedStoreDelegate?.Invoke(_client, _model, _store, _tag);
			embedDataSetDelegate?.Invoke(_client, _model, _repositoryDataSet, _vectorDataSet, _tag);
		}

		private void Client_ChatProgress(object sender, ChatProgressEventArgs e) { }

		private void Client_ChatCompleted(object sender, ChatCompletedEventArgs e)
		{
			if (e.Tag is Arguments args)
			{
				args.InsertTextFunction?.Invoke(args.Range, args.Prefix, e.Message, args.Postfix);
			}
			else if (e.Tag is Word.Range range)
			{
				range.InsertAfter(e.Message);
			}
			Close();
		}

		private void Client_ChatCanceled(object sender, ChatCanceledEventArgs e)
		{
			Utils.Dialogs.ShowErrorDialog(e.Exception.InnerException.Message);
			Close();
		}

		private void Client_EmbedProgress(object sender, EmbedProgressEventArgs e) { }

		private void Client_EmbedCompleted(object sender, EmbedCompletedEventArgs e)
		{
			string result = string.Empty;
			foreach ((int key, string description, ReadOnlyMemory<float> vector) in e.Embedding)
			{
				result += string.Join(";", vector.ToArray());
			}
			Close();

			Utils.Dialogs.ShowMessageDialog(result);
		}

		private void Client_EmbedCanceled(object sender, EmbedCanceledEventArgs e) => Close();

		private void button1_Click(object sender, EventArgs e)
		{
			_client.Cancel();
			Close();
		}

		#region Mouse

		private bool isDragging = false;
		private Point lastCursorPosition;

		private void Dialog_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = true;
				lastCursorPosition = e.Location;
				Opacity = 0.4;
			}
		}

		private void Dialog_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDragging)
			{
				Left += e.X - lastCursorPosition.X;
				Top += e.Y - lastCursorPosition.Y;
			}
		}

		private void Dialog_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isDragging = false;
				Opacity = 1;
			}
		}

		#endregion

	}
}
