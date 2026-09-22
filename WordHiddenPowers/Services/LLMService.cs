// Ignore Spelling: uri OPENAI

using WordHiddenPowers.Controls.ToolBar;
using WordHiddenPowers.Dialogs;
using Word = Microsoft.Office.Interop.Word;

#pragma warning disable OPENAI001 // Тип предназначен только для оценки и может быть изменен или удален в будущих обновлениях. Чтобы продолжить, скройте эту диагностику.

namespace WordHiddenPowers.Services
{
	/// <summary>
	/// Разметка текста с помощью больших языковых моделей.
	/// </summary>
	static class LLMService
	{

		public static void ShowSettingDialog()
		{
			LLMConnectSettingDialog dialog = new(
				profiles: Globals.ThisAddIn.ProfilesSetting,
				selectedChatModelName: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName,
				selectedChatModelProfile: Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint,
				button_1_properties: new ChatButtonProperties(
					caption: Globals.ThisAddIn.GlobalsSetting.CaptionButton1,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1,
					prefixUserMessage: Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton1,
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton1),
				button_2_properties: new ChatButtonProperties(
					caption: Globals.ThisAddIn.GlobalsSetting.CaptionButton2,
					systemMessage: Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2,
					prefixUserMessage: Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton2,
					postfixUserMessage: Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton2));

			if (Utils.Dialogs.ShowDialog(dialog) == System.Windows.Forms.DialogResult.OK)
			{
				Globals.ThisAddIn.GlobalsSetting.CaptionButton1 = dialog.ChatButton_1_Properties.Caption;
				Globals.ThisAddIn.GlobalsSetting.SystemMessageButton1 = dialog.ChatButton_1_Properties.SystemMessage;
				Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton1 = dialog.ChatButton_1_Properties.PrefixUserMessage;
				Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton1 = dialog.ChatButton_1_Properties.PostfixUserMessage;

				Globals.ThisAddIn.GlobalsSetting.CaptionButton2 = dialog.ChatButton_2_Properties.Caption;
				Globals.ThisAddIn.GlobalsSetting.SystemMessageButton2 = dialog.ChatButton_2_Properties.SystemMessage;
				Globals.ThisAddIn.GlobalsSetting.PrefixUserMessageButton2 = dialog.ChatButton_2_Properties.PrefixUserMessage;
				Globals.ThisAddIn.GlobalsSetting.PostfixUserMessageButton2 = dialog.ChatButton_2_Properties.PostfixUserMessage;

				Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelName = dialog.SelectedModel?.Id;
				Globals.ThisAddIn.GlobalsSetting.DefaultChatLLModelProfileEndpoint = dialog.SelectedModel?.Profile.ClientOptions.Endpoint.OriginalString;

				Globals.ThisAddIn.ProfilesSetting.Clear();
				Globals.ThisAddIn.ProfilesSetting.AddRange(dialog.AuthenticationProfiles);
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
