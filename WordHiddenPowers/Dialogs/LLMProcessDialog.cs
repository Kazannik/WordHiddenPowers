// Ignore Spelling: uri Dialogs

using LLMConnectorLibrary;
using LLMConnectorLibrary.Authentication;
using LLMConnectorLibrary.EventArgs;
using LLMConnectorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.Repository;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	delegate void MessageDelegate(LLMClient client, IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag);
	delegate void EmbedStoreDelegate(LLMClient client, IModel model, IEnumerable<(int key, string description)> store, object tag);
	delegate void EmbedDataSetDelegate(LLMClient client, IModel model, DocumentDataSet repositoryDataSet, VectorDataSet vectorDataSet, object tag);

	public delegate Word.Range InsertTextDelegate(Word.Range range, string prefix, string text, string postfix);

	public partial class LLMProcessDialog : Form
	{
		private readonly LLMClient client;
		private bool hostIsAvailable;

		private readonly MessageDelegate sendMessageDelegate;
		private readonly EmbedStoreDelegate embedStoreDelegate;
		private readonly EmbedDataSetDelegate embedDataSetDelegate;

		private readonly IModel model;
		private readonly IChatOptions options;
		private readonly string systemMessage;
		private readonly IEnumerable<string> userMessages;
		private readonly IEnumerable<(int key, string description)> store;

		private readonly DocumentDataSet repositoryDataSet;
		private readonly VectorDataSet vectorDataSet;

		private readonly object tag;

		public LLMProcessDialog(IAuthenticationProfile profile)
		{
			hostIsAvailable = false;

			InitializeComponent();

			client = new LLMClient();
			client.HostChecked += new EventHandler<CheckHostEventArgs>(Client_HostChecked);
			client.ModelsCollectionCompleted += new EventHandler<ModelsCollectionCompletedEventArgs>(Client_ModelsCollectionCompleted);

			client.ChatProgress += new EventHandler<ChatProgressEventArgs>(Client_ChatProgress);
			client.ChatCompleted += new EventHandler<ChatCompletedEventArgs>(Client_ChatCompleted);
			client.ChatCanceled += new EventHandler<ChatCanceledEventArgs>(Client_ChatCanceled);

			client.EmbedProgress += new EventHandler<EmbedProgressEventArgs>(Client_EmbedProgress);
			client.EmbedCompleted += new EventHandler<EmbedCompletedEventArgs>(Client_EmbedCompleted);
			client.EmbedCanceled += new EventHandler<EmbedCanceledEventArgs>(Client_EmbedCanceled);

			InitializeClient(profile);
		}

		private async void InitializeClient(IAuthenticationProfile profile)
		{
			await client.CheckHostAsync(profile: profile);
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
		public LLMProcessDialog(IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag)
			: this(profile: model.Profile)
		{
			this.model = model;
			this.options = options;
			this.systemMessage = systemMessage;
			this.userMessages = userMessages;
			this.tag = tag;
			sendMessageDelegate = OnSendMessage;
		}

		public readonly struct Arguments(InsertTextDelegate insertTextFunction, Word.Range range, string prefix, string postfix)
		{
			public InsertTextDelegate InsertTextFunction { get; } = insertTextFunction;
			public Word.Range Range { get; } = range;
			public string Prefix { get; } = prefix;
			public string Postfix { get; } = postfix;
		}

		public LLMProcessDialog(IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, Arguments arg)
			: this(profile: model?.Profile)
		{
			this.model = model;
			this.options = options;
			this.systemMessage = systemMessage;
			this.userMessages = userMessages;
			tag = arg;
			sendMessageDelegate = OnSendMessage;
		}

		public LLMProcessDialog(IModel model, string input, object tag) :
			this(model: model, store: [(1, input)], tag: tag)
		{ }

		public LLMProcessDialog(IModel model, IEnumerable<(int key, string description)> store, object tag)
			: this(profile: model.Profile)
		{
			this.model = model;
			this.store = store;
			this.tag = tag;
			embedStoreDelegate = OnEmbed;
		}

		public LLMProcessDialog(IModel model, DocumentDataSet repositoryDataSet, VectorDataSet vectorDataSet, object tag)
			: this(profile: model.Profile)
		{
			this.model = model;
			this.repositoryDataSet = repositoryDataSet;
			this.vectorDataSet = vectorDataSet;
			this.tag = tag;
			embedDataSetDelegate = OnEmbedDataSet;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			SizeF size = graphics.MeasureString(model?.Id, Font);
			graphics.DrawString(model?.Id, Font, new SolidBrush(ForeColor), Height + 10, (Height - size.Height) / 2);
			ControlLibrary.Utils.Drawing.DrawRoundedRectangle(graphics, SystemPens.ControlDark, new Rectangle(0, 0, Width - 1, Height - 1), 7);
			base.OnPaint(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			GraphicsPath path = ControlLibrary.Utils.Drawing.CreateRoundedPath(new Rectangle(0, 0, Width, Height), 5);
			this.Region = new Region(path);
			base.OnLoad(e);
		}

		public bool HostIsAvailable => hostIsAvailable;

		private static async void OnSendMessage(LLMClient client, IModel model, IChatOptions options, string systemMessage, IEnumerable<string> userMessages, object tag)
		{
			if (tag is Arguments arg)
				GlobalsEventsBus.DoSendChatMessage(arg.Range, systemMessage, userMessages);

			await client.SendAsync(model: model, options: options, systemMessage: systemMessage, userMessages: userMessages, tag: tag);
		}

		private static async void OnEmbed(LLMClient client, IModel model, string input, object tag)
		{
			await client.EmbedAsync(model: model, input: input, tag: tag);
		}

		private static async void OnEmbed(LLMClient client, IModel model, IEnumerable<(int key, string description)> store, object tag)
		{
			await client.EmbedAsync(model: model, store: store, tag: tag);
		}

		private static async void OnEmbedDataSet(LLMClient client, IModel model, DocumentDataSet sourceDataSet, VectorDataSet vectorDataSet, object tag)
		{
			IEnumerable<(int key, string description)> store =
				sourceDataSet.GetTextNotes()
				.Select((note, key) => (key: key + 1, description: note.Value as string));

			await client.EmbedAsync(model: model, store: store, tag: tag);
		}

		private async void Client_HostChecked(object sender, CheckHostEventArgs e)
		{
			hostIsAvailable = e.IsAvailable;
			if (hostIsAvailable)
				await client.ReadModelsNameAsync(e.Profile);
			else
			{
				Utils.Dialogs.ShowErrorDialog(string.Format("Проверьте доступ к провайдеру: {0}.\nКажется этот хост недоступен!", e.Profile.ClientOptions.Endpoint.AbsoluteUri));
				DialogClose();
			}
		}

		private void Client_ModelsCollectionCompleted(object sender, ModelsCollectionCompletedEventArgs e)
		{
			sendMessageDelegate?.Invoke(client, model, options, systemMessage, userMessages, tag);
			embedStoreDelegate?.Invoke(client, model, store, tag);
			embedDataSetDelegate?.Invoke(client, model, repositoryDataSet, vectorDataSet, tag);
		}

		private void Client_ChatProgress(object sender, ChatProgressEventArgs e) { }

		private void Client_ChatCompleted(object sender, ChatCompletedEventArgs e)
		{
			if (e.Tag is Arguments args)
			{
				GlobalsEventsBus.DoReceiveChatMessage(args.Range, e.SystemMessage, e.UserMessages, e.Message, e.Model, e.Options);
				args.InsertTextFunction?.Invoke(args.Range, args.Prefix, e.Message, args.Postfix);
			}
			else if (e.Tag is Word.Range range)
			{
				GlobalsEventsBus.DoReceiveChatMessage(range, e.SystemMessage, e.UserMessages, e.Message, e.Model, e.Options);
				range.InsertAfter(e.Message);
			}
			DialogClose();
		}

		private void Client_ChatCanceled(object sender, ChatCanceledEventArgs e)
		{
			try
			{
				DialogClose();
			}
			catch (Exception) { }
			finally
			{
				Utils.Dialogs.ShowErrorDialog(e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message);
			}
		}

		private void Client_EmbedProgress(object sender, EmbedProgressEventArgs e) { }

		private void Client_EmbedCompleted(object sender, EmbedCompletedEventArgs e)
		{
			string result = string.Empty;
			foreach ((int key, string description, ReadOnlyMemory<float> vector) in e.Embedding)
			{
				result += string.Join(";", vector.ToArray());
			}
			DialogClose();

			Utils.Dialogs.ShowMessageDialog(result);
		}

		private void Client_EmbedCanceled(object sender, EmbedCanceledEventArgs e) => DialogClose();

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

		/// <summary>
		/// Закрывает форму.
		/// </summary>
		private void DialogClose()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(DialogClose));
			}
			else
			{
				this.Close();
			}
		}
	}
}
