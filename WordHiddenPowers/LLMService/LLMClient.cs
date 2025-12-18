using Microsoft.Extensions.AI;
using Microsoft.Office.Interop.Word;
using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace WordHiddenPowers.LLMService
{
	internal class LLMClient
	{
		public event EventHandler<ChatCompletedEventArgs> ChatCompleted;
		public event EventHandler<ChatCanceledEventArgs> ChatCanceled;
		public event EventHandler<ChatProgressEventArgs> ChatProgress;

		protected virtual void OnChatCompleted(ChatCompletedEventArgs e)
		{
			ChatCompleted?.Invoke(this, e);
		}

		protected virtual void OnChatCanceled(ChatCanceledEventArgs e)
		{
			ChatCanceled?.Invoke(this, e);
		}

		protected virtual void OnChatProgress(ChatProgressEventArgs e)
		{
			ChatProgress?.Invoke(this, e);
		}

		public event EventHandler<EmbedCompletedEventArgs> EmbedCompleted;
		public event EventHandler<EmbedCanceledEventArgs> EmbedCanceled;
		public event EventHandler<EmbedProgressEventArgs> EmbedProgress;

		protected virtual void OnEmbedCompleted(EmbedCompletedEventArgs e)
		{
			EmbedCompleted?.Invoke(this, e);
		}

		protected virtual void OnEmbedCanceled(EmbedCanceledEventArgs e)
		{
			EmbedCanceled?.Invoke(this, e);
		}

		protected virtual void OnEmbedProgress(EmbedProgressEventArgs e)
		{
			EmbedProgress?.Invoke(this, e);
		}


		private readonly BackgroundWorker llmWorker;

		public string Url { get; private set; }

		public IEnumerable<OpenAIModel> Models { get; private set; }

		public string SelectedModel { get; private set; }

		public LLMClient(string url = "http://localhost:11434")//public AIClient(string url = "http://10.197.24.37:8000")			
		{
			llmWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			llmWorker.DoWork += new DoWorkEventHandler(LLM_DoWork);
			llmWorker.ProgressChanged += new ProgressChangedEventHandler(LLM_ProgressChanged);
			llmWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LLM_Completed);

			Url = url;
			//OllamaApiClient ollamaClient = new(Url);
			//_ = InitializeClientAsync(ollamaClient);
		}

		//private async Task InitializeClientAsync(OllamaApiClient ollamaClient)
		//{
		//	try
		//	{
		//		Models = await ollamaClient.ListLocalModelsAsync();
		//		if (Models.Any())
		//		{
		//			SelectedModel = Models.First().Name;
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		return;
		//	}
		//}

		//private async Task<string> SendAsync(string model, string systemMessage, string userMessage)
		//{
		//	//try
		//	//{
		//	//	ollamaClient.SelectedModel = model;
		//	//	Chat chat = new(ollamaClient, systemMessage);
		//	//	var answerBuilder = new StringBuilder();
		//	//	await foreach (string answerToken in chat.SendAsync(userMessage))
		//	//	{
		//	//		if (answerToken is null)
		//	//			continue;
		//	//		answerBuilder.Append(answerToken);
		//	//		await ollamaClient.IsRunningAsync();
		//	//	}
		//	//	return answerBuilder.ToString();
		//	//}

		//	////try
		//	////{
		//	////	ollamaClient.SelectedModel = model;
		//	////	Chat chat = new(ollamaClient, systemPrompt);
		//	////	string result = "";

		//	////	await foreach (string answerToken in chat.SendAsync(prompt))
		//	////		result += answerToken;
		//	////	return result;
		//	////}
		//	//catch (Exception ex)
		//	//{
		//	//	return ex.Message + " ИЛИ Ошибка в модуле AI: Превышен установленный временной интервал для подготовки информации.";
		//	//}
		//}

		//private async Task<IEnumerable<Embedding<float>>> EmbedAsync(string model, IEnumerable<string> values)
		//{
		//	ollamaClient.SelectedModel = model;
		//	IEmbeddingGenerator<string, Embedding<float>> generator = ollamaClient;
		//	return await generator.GenerateAsync(values);
		//}

		public void Send(string model, string userMessage)
		{
			Send(model: model, systemMessage: string.Empty, userMessage: userMessage, tag: null);
		}

		public void Send(string model, string systemMessage, string userMessage)
		{
			Send(model: model, systemMessage: systemMessage, userMessage: userMessage, tag: null);
		}

		public void Send(string model, string systemMessage, string userMessage, object tag)
		{
			if (llmWorker.IsBusy != true)
			{
				llmWorker.RunWorkerAsync(new object[] { model, systemMessage, userMessage, tag });
			}
		}

		public void Embed(string model, IEnumerable<string> values)
		{
			Embed(model: model, values: values, tag: null);
		}

		public void Embed(string model, IEnumerable<string> values, object tag)
		{
			if (llmWorker.IsBusy != true)
			{
				llmWorker.RunWorkerAsync(new object[] { model, values, tag });
			}
		}

		private void LLM_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			object[] arguments = (object[])e.Argument;

			//_ = InitializeClientAsync(ollamaClient);

			if (arguments.Length == 4)
			{
				Task<bool> isAvailable = Services.NetService.CheckHostByHttpAsync(new Uri(baseUri: Services.OpenAIService.Uri, relativeUri: Services.OpenAIService.Uri.AbsolutePath + "/models"));
				isAvailable.Wait();
				
				if (isAvailable.Result)
				{
					try
					{
						Task<string> message = Services.OpenAIService.SendMessageAsync(
							model: arguments[0] as string,
							systemMessage: arguments[1] as string,
							userMessage: arguments[2] as string);
						
						message.Wait();
						e.Result = new object[] { arguments[0], arguments[1], arguments[2], arguments[3], message };
					}
					catch (Exception ex)
					{
						e.Result = new object[] { arguments[0], arguments[1], arguments[2], arguments[3], ex};
					}
				}
				else
				{
					e.Result = new object[] { arguments[0], arguments[1], arguments[2], arguments[3], new Exception(string.Format("Проверьте доступ к провайдеру по адресу: [{0}]", Services.OpenAIService.Uri)) };
				}
				
				//while (result.Status == TaskStatus.Running)
				//{
				//	worker.ReportProgress(percentProgress: 0, userState: arguments);
				//	System.Threading.Thread.Sleep(50);
				//	if (worker.CancellationPending == true)
				//	{
				//		e.Cancel = true;
				//		break;
				//	}
				//}
			}
			else if (arguments.Length == 3)
			{
				//var result = EmbedAsync(ollamaClient: ollamaClient,
				//	model: arguments[0] as string,
				//	values: arguments[1] as IEnumerable<string>);

				//while (result.Status == TaskStatus.Running)
				//{
				//	worker.ReportProgress(percentProgress: 0, userState: arguments);
				//	System.Threading.Thread.Sleep(50);
				//	if (worker.CancellationPending == true)
				//	{
				//		e.Cancel = true;
				//		break;
				//	}
				//}
				//e.Result = new object[] { arguments[0], arguments[1], arguments[2], result };
			}
		}

		private void LLM_Completed(object sender, RunWorkerCompletedEventArgs e)
		{
			object[] arguments = (object[])e.Result;

			if (arguments.Length == 5)
			{
				if (e.Cancelled == true)
				{
					OnChatProgress(new ChatCanceledEventArgs(
						model: arguments[0] as string,
						systemMessage: arguments[1] as string,
						userMessage: arguments[2] as string,
						tag: arguments[3]));
				}
				else if (e.Error != null)
				{
					OnChatProgress(new ChatCanceledEventArgs(
						model: arguments[0] as string,
						systemMessage: arguments[1] as string,
						userMessage: arguments[2] as string,
						tag: arguments[3],
						cancel: false,
						error: true,
						exceptionMessage: arguments[4] as string));
				}
				else
				{
					Task<string> message = (Task<string>)arguments[4];
					OnChatCompleted(new ChatCompletedEventArgs(
						model: arguments[0] as string,
						systemMessage: arguments[1] as string,
						userMessage: arguments[2] as string,
						tag: arguments[3],
						message: message.Result));
				}
			}
			else if (arguments.Length == 4)
			{
				if (e.Cancelled == true)
				{
					OnEmbedProgress(new EmbedCanceledEventArgs(
						model: arguments[0] as string,
						values: arguments[1] as IEnumerable<string>,
						tag: arguments[2]));
				}
				else if (e.Error != null)
				{
					OnEmbedProgress(new EmbedCanceledEventArgs(
						model: arguments[0] as string,
						values: arguments[1] as IEnumerable<string>,
						tag: arguments[2],
						cancel: false,
						error: true,
						exceptionMessage: arguments[3] as string));
				}
				else
				{
					Task<IEnumerable<Embedding<float>>> embedding = (Task<IEnumerable<Embedding<float>>>)arguments[3];
					OnEmbedCompleted(new EmbedCompletedEventArgs(
						model: arguments[0] as string,
						values: arguments[1] as IEnumerable<string>,
						tag: arguments[2],
						embedding: embedding.Result));
				}
			}
		}

		private void LLM_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			object[] arguments = (object[])e.UserState;
			if (arguments.Length == 4)
			{
				OnChatProgress(new ChatProgressEventArgs(model: arguments[0] as string,
					systemMessage: arguments[1] as string,
					userMessage: arguments[2] as string,
					tag: arguments[3]));
			}
			else if (arguments.Length == 3)
			{
				OnEmbedProgress(new EmbedProgressEventArgs(
					model: arguments[0] as string,
					values: arguments[1] as IEnumerable<string>,
					tag: arguments[2]));
			}
		}
				
		public class ChatProgressEventArgs : EventArgs
		{
			public string Model { get; }
			public string SystemMessage { get; }
			public string UserMessage { get; }
			public object Tag { get; }

			internal ChatProgressEventArgs(string model, string systemMessage, string userMessage, object tag) : base()
			{
				Model = model;
				SystemMessage = systemMessage;
				UserMessage = userMessage;
				Tag = tag;
			}
		}

		public class ChatCompletedEventArgs : ChatProgressEventArgs
		{
			public string Message { get; }

			internal ChatCompletedEventArgs(string model, string systemMessage, string userMessage, object tag, string message) :
				base(model: model, systemMessage: systemMessage, userMessage: userMessage, tag: tag)
			{
				Message = message;
			}
		}

		public class ChatCanceledEventArgs : ChatProgressEventArgs
		{
			public bool Cancel { get; }
			public bool Error { get; }
			public string ExceptionMessage { get; }

			internal ChatCanceledEventArgs(string model, string systemMessage, string userMessage, object tag) :
				this(model: model, systemMessage: systemMessage, userMessage: userMessage, tag: tag, cancel: true, error: false, exceptionMessage: string.Empty)
			{ }

			internal ChatCanceledEventArgs(string model, string systemMessage, string userMessage, object tag, bool cancel, bool error, string exceptionMessage) :
				base(model: model, systemMessage: systemMessage, userMessage: userMessage, tag: tag)
			{
				Cancel = cancel;
				Error = error;
				ExceptionMessage = exceptionMessage;
			}
		}

		public class EmbedProgressEventArgs : EventArgs
		{
			public string Model { get; }
			public IEnumerable<string> Values { get; }
			public object Tag { get; }

			internal EmbedProgressEventArgs(string model, IEnumerable<string> values, object tag) : base()
			{
				Model = model;
				Values = values;
				Tag = tag;
			}
		}

		public class EmbedCompletedEventArgs : EmbedProgressEventArgs
		{
			public IEnumerable<Embedding<float>> Embedding { get; }

			internal EmbedCompletedEventArgs(string model, IEnumerable<string> values, object tag, IEnumerable<Embedding<float>> embedding) :
				base(model: model, values: values, tag: tag)
			{
				Embedding = embedding;
			}
		}

		public class EmbedCanceledEventArgs : EmbedProgressEventArgs
		{
			public bool Cancel { get; }
			public bool Error { get; }
			public string ExceptionMessage { get; }

			internal EmbedCanceledEventArgs(string model, IEnumerable<string> values, object tag) :
				this(model: model, values: values, tag: tag, cancel: true, error: false, exceptionMessage: string.Empty)
			{ }

			internal EmbedCanceledEventArgs(string model, IEnumerable<string> values, object tag, bool cancel, bool error, string exceptionMessage) :
				base(model: model, values: values, tag: tag)
			{
				Cancel = cancel;
				Error = error;
				ExceptionMessage = exceptionMessage;
			}
		}
	}
}