// Ignore Spelling: uri OPENAI

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;



#pragma warning disable OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.

namespace WordHiddenPowers.Services
{

	static class OpenAIService
	{
		public static string MainSystemMessage { get; set; } = "Твоя аудитория профессиональные юристы. При подготовке ответа не фантазируй и не добавляй свои комментарии.";
		public static string CaptionButton1 { get; set; } = "Кнопка 1";
		public static string SystemMessageButton1 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton1 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton1 { get; set; } = string.Empty;

		public static string CaptionButton2 { get; set; } = "Кнопка 2";
		public static string SystemMessageButton2 { get; set; } = "Твоя аудитория юристы. Давай краткие понятные ответы только по существу вопроса";
		public static string PrefixUserMessageButton2 { get; set; } = string.Empty;
		public static string PostfixUserMessageButton2 { get; set; } = string.Empty;


		public const string OPENAI_API_KEY = "OPENAI_API_KEY";

		public static Uri Uri { get; set; } = new Uri(baseUri: new Uri("http://localhost:11434"), relativeUri: "/v1");

		public static string LLMName { get; set; } = "mistral:latest";

		public static void ShowSettingDialog(Documents.Document document)
		{
			SettingDialog dialog = new SettingDialog(document: document, selectedLLM: LLMName, uri: Uri);
			if (Utils.Dialogs.ShowDialog(dialog) == System.Windows.Forms.DialogResult.OK)
			{
				Uri = dialog.SelectedUri;
				LLMName = dialog.SelectedLLMName;
			}
		}

		public static IEnumerable<string> GetModelsName() => GetModelsName(uri: Uri);

		public static IEnumerable<string> GetModelsName(Uri uri)
		{
			try
			{
				return LLMConnectorLibrary.LLMOpenAI.GetModelsName(uri);
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowMessageDialog(ex.InnerException.Message);
				return new string[] { };
			}			
		}		
	}
}

#pragma warning restore OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.
