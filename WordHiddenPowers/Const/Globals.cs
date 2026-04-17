// Ignore Spelling: ADDIN TSV DIC APP Globals

using System.Drawing;

namespace WordHiddenPowers.Const
{
	public static class Globals
	{
		public const string APP_FOLDER_NAME = "Microsoft Word MLModel";

		/// <summary>
		/// Минимальный порог для оценки параграфа как удовлетворяющего требованиям. 
		/// </summary>
		public const float LEVEL_PASSAGE = 0.8f;

		public const string ADDIN_TITLE = "AI помощник и дополнительные данные";

		private const string VARIABLES_NAME = "HiddenPower";

		public const string DIALOG_XML_FILTER =
			"XML Schema File|*.xsd|" +
			"Файл структуры анализа|*.xml|" +
			"Файл разметки документа|*.docxml|" +
			"Файл текущих агрегированных данных|*.datexml|" +
			"Файл прошлых агрегированных данных|*.datexml|" +
			"Файл векторного хранилища документа|*.vecxml";
		public const string DIALOG_TSV_FILTER = "Текстовый формат с разделителями|*.tsv";
		public const string DIALOG_TXT_FILTER = "Текстовый формат|*.txt";
		public const string DIALOG_DIC_FILTER = "Словарь использованных слов|*.dic";

		public const string CAPTION_VARIABLE_NAME = "Title" + VARIABLES_NAME;
		public const string DESCRIPTION_VARIABLE_NAME = "Description" + VARIABLES_NAME;
		public const string DATE_VARIABLE_NAME = "Date" + VARIABLES_NAME;
		public const string XML_CURRENT_VARIABLE_NAME = "Xml" + VARIABLES_NAME;
		public const string TABLE_VARIABLE_NAME = "Table" + VARIABLES_NAME;
		public const string ML_MODEL_VARIABLE_NAME = "MLModel" + VARIABLES_NAME;
		public const string EMBED_LLMODEL_VARIABLE_NAME = "EmbedLLModel" + VARIABLES_NAME;


		///// <summary>
		///// Имя переменной для хранения уникального идентификатора документа в формате GUID
		///// </summary>
		//public const string DOC_ID_VARIABLE_NAME = "C25F7F16-ADEF-42F8-A07E-608E2DEA873F";

		/// <summary>
		/// Имя переменной для хранения результатов импорта данных.
		/// </summary>
		public const string XML_NOW_AGGREGATED_VARIABLE_NAME = "78B817AB-4941-4CD9-9830-E39D15BBE28B";

		/// <summary>
		/// Имя переменной для хранения результатов импорта данных за прошлый период.
		/// </summary>
		public const string XML_LAST_AGGREGATED_VARIABLE_NAME = "6B0F57EE-BE9F-4C83-A660-2FCDBB1E6B34";

		/// <summary>
		/// Имя переменной для хранения векторной базы данных.
		/// </summary>
		public const string XML_VECTOR_VARIABLE_NAME = "7C02D1A8-153A-427A-B6F3-19607C60335D";


		#region COLORS

		public static readonly Color COLOR_STAR_ICON = Color.Red;
		public static readonly Color COLOR_NEGATIVE_STAR_ICON = SystemColors.Highlight;

		public static readonly Color COLOR_OK_ICON = Color.Green;
		public static readonly Color COLOR_CANCEL_ICON = Color.Red;

		public static readonly Color COLOR_1_SELECTED_BACK = Color.LightGoldenrodYellow;
		public static readonly Color COLOR_2_SELECTED_BACK = Color.OrangeRed;

		public static readonly Color COLOR_1_BACK = Color.Orange;
		public static readonly Color COLOR_2_BACK = Color.LightPink;

		#endregion
	}
}
