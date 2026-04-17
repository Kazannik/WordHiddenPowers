// Ignore Spelling: uri OPENAI

using System;
using WordHiddenPowers.Dialogs;
using static LLMConnectorLibrary.LLMOpenAI;
using Word = Microsoft.Office.Interop.Word;

#pragma warning disable OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.

namespace WordHiddenPowers.Services
{
	/// <summary>
	/// Разметка текста с помощью больших языковых моделей.
	/// </summary>
	static class OpenAIService
	{
		public static string SystemMessage { get; set; } = "Твоя аудитория профессиональные юристы. При подготовке ответа не фантазируй и не добавляй свои комментарии.";

		public static string CaptionButton1 { get; set; } = "Кнопка 1";
		public static string SystemMessageButton1 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton1 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton1 { get; set; } = string.Empty;

		public static string CaptionButton2 { get; set; } = "Кнопка 2";
		public static string SystemMessageButton2 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton2 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton2 { get; set; } = string.Empty;

		public static Uri Uri { get; set; } = new Uri("http://localhost:11434");

		public static TimeSpan Timeout { get; set; } = new TimeSpan(0, 10, 0);

		public static string LLMName { get; set; } = "mistral:latest";

		public static string EmbeddingLLMName { get; set; } = "mistral:latest";

		public static ChatOptions Options { get; set; } = ChatOptions.Empty;

		public static void ShowSettingDialog(Documents.Document document)
		{
			LLMConnectSettingDialog dialog = new LLMConnectSettingDialog(document: document, selectedLLM: LLMName, selectedEmbeddingLlmName: EmbeddingLLMName, uri: Uri, timeout: Timeout);
			if (Utils.Dialogs.ShowDialog(dialog) == System.Windows.Forms.DialogResult.OK)
			{
				Uri = dialog.Uri;
				Timeout = dialog.Timeout;
				LLMName = dialog.SelectedLLMName;
				EmbeddingLLMName = dialog.SelectedEmbeddingLLMName;
			}
		}

		public static void EmbeddingNotesCollection(Documents.Document document)
		{
			foreach (var row in document.CurrentDataSet.DecimalNotes)
			{
				Word.Range range = document.Doc.Range(row.WordSelectionStart, row.WordSelectionEnd);

				///item.Note.SetWordSelectionText(range.Text);

				//row.
			}

		}


		#region Поиск векторов для категорий




		#endregion


	}
}

#pragma warning restore OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.
