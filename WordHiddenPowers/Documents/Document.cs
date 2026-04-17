// Ignore Spelling: dest

using Microsoft.Office.Tools;
using System;
using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Utils;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using DataTable = System.Data.DataTable;
using Office = Microsoft.Office.Core;
using Table = WordHiddenPowers.Repository.Data.Table;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public partial class Document : IDisposable
	{
		private WordDocumentMode state = WordDocumentMode.Default;

		internal WordDocumentMode State
		{
			get => state;
			private set
			{
				state = value;
				Pane.NotesControlVisible = state == WordDocumentMode.Separate;
			}
		}

		internal bool IsTableSchema => currentDataSet != null && currentDataSet.IsTableSchema;

		private readonly DocumentCollection parent;

		/// <summary>
		/// Основные данные документа.
		/// </summary>
		private RepositoryDataSet currentDataSet;

		/// <summary>
		/// Агрегированные данные из нескольких документов.
		/// </summary>
		private RepositoryDataSet nowAggregatedDataSet;

		/// <summary>
		/// Сопоставимые агрегированные данные из нескольких документов (например, за прошлый период).
		/// </summary>
		private RepositoryDataSet lastAggregatedDataSet;

		/// <summary>
		/// Хранилище векторов.
		/// </summary>
		private VectorDataSet vectorDataSet;

		/// <summary>
		/// Связанная с документом боковая панель.
		/// </summary>
		public CustomTaskPane CustomPane { get; }

		public AddInPane Pane => CustomPane.Control as AddInPane;

		public int Hwnd { get; }

		private void Pane_PropertiesChanged(object sender, EventArgs e)
		{
			if (Pane.NotesControlVisible)
			{
				if (Caption != Pane.NotesControl.Caption)
				{
					Caption = Pane.NotesControl.Caption;
					Doc.Saved = false;
				}

				if (Date != Pane.NotesControl.Date)
				{
					Date = Pane.NotesControl.Date;
					Doc.Saved = false;
				}

				if (Description != Pane.NotesControl.Description)
				{
					Description = Pane.NotesControl.Description;
					Doc.Saved = false;
				}
			}

			if (CurrentDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME,
					dataSet: CurrentDataSet);
				Doc.Saved = false;
			}

			if (NowAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME,
					dataSet: NowAggregatedDataSet);
				Doc.Saved = false;
			}

			if (LastAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME,
					dataSet: LastAggregatedDataSet);
				Doc.Saved = false;
			}

			if (VectorDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_VECTOR_VARIABLE_NAME,
					dataSet: VectorDataSet);
				Doc.Saved = false;
			}
		}

		public string FileName { get; }

		public string Caption
		{
			get => Content.GetCaption(Doc);
			set
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.CAPTION_VARIABLE_NAME,
					value: value);
			}
		}

		public DateTime Date
		{
			get => Content.GetDate(Doc);
			set
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.DATE_VARIABLE_NAME,
					value: value.ToShortDateString());
			}
		}

		public string Description
		{
			get => Content.GetDescription(Doc);
			set
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.DESCRIPTION_VARIABLE_NAME,
					value: value);
			}
		}

		public Table Table
		{
			get => Content.GetTable(Doc);
			set
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.TABLE_VARIABLE_NAME,
					value: value.ToString());
			}
		}

		public string MLModelName
		{
			get => CurrentDataSet.MLModelName;
			set => CurrentDataSet.MLModelName = value;
		}

		public string EmbedLLModelName
		{
			get => CurrentDataSet.EmbedLLModelName;
			set => CurrentDataSet.EmbedLLModelName = value;
		}

		public bool ContentHide { get; set; }

		public RepositoryDataSet CurrentDataSet
		{
			get
			{
				if (currentDataSet == null)
				{
					currentDataSet = Xml.GetCurrentDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						currentDataSet = new RepositoryDataSet();
					}
				}
				return currentDataSet;
			}
		}

		public RepositoryDataSet NowAggregatedDataSet
		{
			get
			{
				if (nowAggregatedDataSet == null)
				{
					nowAggregatedDataSet = Xml.GetNowAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						nowAggregatedDataSet = new RepositoryDataSet();
					}
				}
				return nowAggregatedDataSet;
			}
		}

		public RepositoryDataSet LastAggregatedDataSet
		{
			get
			{
				if (lastAggregatedDataSet == null)
				{
					lastAggregatedDataSet = Xml.GetLastAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						lastAggregatedDataSet = new RepositoryDataSet();
					}
				}
				return lastAggregatedDataSet;
			}
		}

		public VectorDataSet VectorDataSet
		{
			get
			{
				if (vectorDataSet == null)
				{
					vectorDataSet = Xml.GetVectorDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						vectorDataSet = new VectorDataSet();
					}
				}
				return vectorDataSet;
			}
		}

		public Word._Document Doc { get; }

		private Document(DocumentCollection parent, string fileName, Word._Document doc)
		{
			this.parent = parent;

			FileName = fileName;
			ContentHide = false;
			Doc = doc;
			Hwnd = Doc.Windows[1].Hwnd;


			bool oldState = this.parent.paneVisibleButton.Checked;

			this.parent.paneVisibleButton.Checked = false;

			CustomPane = Globals.ThisAddIn.CustomTaskPanes.Add(new AddInPane(this, Hwnd), Const.Panes.PANE_TITLE, Doc.Windows[1]);
			CustomPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
			CustomPane.Width = 600;
			CustomPane.Visible = oldState;
			CustomPane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
			Pane.PropertiesChanged += new EventHandler<EventArgs>(Pane_PropertiesChanged);

			if (Content.ExistsVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME))
			{
				State = WordDocumentMode.Combine;
			}
			else if (Content.ExistsVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME))
			{
				State = WordDocumentMode.Separate;
			}
			else
			{
				State = WordDocumentMode.Default;
			}
			this.parent.paneVisibleButton.Checked = oldState;
		}

		private void Pane_VisibleChanged(object sender, EventArgs e)
		{
			CustomTaskPane pane = (CustomTaskPane)sender;
			if (parent != null) parent.paneVisibleButton.Checked = pane.Visible;
		}

		public static Document Create(DocumentCollection parent, string fileName, Word._Document Doc)
		{
			Document document = new Document(
				parent: parent,
				fileName: fileName,
				doc: Doc);
			return document;
		}


		/// <summary>
		/// Создать чистую структуру данных.
		/// </summary>
		public void NewData()
		{
			if (Doc.Variables.Count > 0)
			{
				Content.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_VECTOR_VARIABLE_NAME);
			}
			ClearDataSet(CurrentDataSet);
			CurrentDataSet.AcceptChanges();
			CommitVariables();
			State = WordDocumentMode.Separate;
		}

		/// <summary>
		/// Загрузить чистую структуру основных данных.
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadClearData(string fileName)
		{
			if (Doc.Variables.Count > 0)
			{
				Content.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME);
			}

			LoadDataSet(CurrentDataSet, fileName);

			CurrentDataSet.DecimalNotes.Clear();
			CurrentDataSet.TextNotes.Clear();
			CurrentDataSet.WordFiles.Clear();
			CurrentDataSet.DecimalTable.Clear();

			CurrentDataSet.AcceptChanges();
			CommitVariables();
			State = WordDocumentMode.Separate;
		}

		/// <summary>
		/// Загрузить основные данные.
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadCurrentData(string fileName)
		{
			LoadDataSet(CurrentDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, CurrentDataSet);
			State = WordDocumentMode.Separate;
		}

		/// <summary>
		/// Загрузить агрегированные данные текущего периода.
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadNowAggregatedData(string fileName)
		{
			LoadDataSet(NowAggregatedDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
			State = WordDocumentMode.Combine;
		}

		/// <summary>
		/// Загрузить агрегированные данные прошлого периода.
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadLastAggregatedData(string fileName)
		{
			LoadDataSet(LastAggregatedDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
			State = WordDocumentMode.Combine;
		}

		private void LoadDataSet(RepositoryDataSet dataSet, string fileName)
		{
			try
			{
				ClearDataSet(dataSet);
				dataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);
				dataSet.AcceptChanges();
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		private void ClearDataSet(RepositoryDataSet dataSet)
		{
			try
			{
				dataSet.RowsHeaders.Clear();
				dataSet.ColumnsHeaders.Clear();

				dataSet.Subcategories.Clear();
				dataSet.Categories.Clear();

				dataSet.DocumentKeys.Clear();
				dataSet.WordFiles.Clear();
				dataSet.DecimalTable.Clear();

				dataSet.DecimalNotes.Clear();
				dataSet.TextNotes.Clear();

				dataSet.Setting.Clear();
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		/// <summary>
		/// Загрузить векторную базу дааных.
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadVectorData(string fileName)
		{
			try
			{
				VectorDataSet.ParagraphVectorStore.Clear();

				VectorDataSet.WordFiles.Clear();

				VectorDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				VectorDataSet.AcceptChanges();

				Content.CommitVariable(Doc.Variables, Const.Globals.XML_VECTOR_VARIABLE_NAME, VectorDataSet);
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		public void SaveSchema(string fileName) =>
			Xml.SaveSchema(Globals.ThisAddIn.Documents.ActiveDocument.CurrentDataSet, fileName);

		public void SaveClearData(string fileName) =>
			Xml.SaveClearData(Globals.ThisAddIn.Documents.ActiveDocument.CurrentDataSet, fileName);

		public void SaveCurrentData(string fileName) =>
			Xml.SaveData(Globals.ThisAddIn.Documents.ActiveDocument.CurrentDataSet, fileName);

		public void SaveNowAggregatedData(string fileName) =>
			Xml.SaveData(Globals.ThisAddIn.Documents.ActiveDocument.NowAggregatedDataSet, fileName);

		public void SaveLastAggregatedData(string fileName) =>
			Xml.SaveData(Globals.ThisAddIn.Documents.ActiveDocument.LastAggregatedDataSet, fileName);

		public void SaveVectorData(string fileName) =>
			Xml.SaveVectorData(Globals.ThisAddIn.Documents.ActiveDocument.VectorDataSet, fileName);

		/// <summary>
		/// Зафиксировать данные.
		/// </summary>
		public void CommitVariables()
		{
			Content.CommitVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME, Caption);
			Content.CommitVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
			Content.CommitVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
			Content.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

			if (CurrentDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, CurrentDataSet);
			}

			if (NowAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
			}

			if (LastAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
			}

			if (VectorDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_VECTOR_VARIABLE_NAME, VectorDataSet);
			}
		}

		/// <summary>
		/// Проверить наличие данных.
		/// </summary>
		/// <returns></returns>
		public bool VariablesExists()
		{
			if (Doc.Variables.Count > 0)
			{
				if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.CAPTION_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.DATE_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.DESCRIPTION_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.TABLE_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.XML_CURRENT_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME))
					return true;
				else if (Content.ExistsVariable(Doc.Variables,
					Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME))
					return true;
				return Content.ExistsVariable(Doc.Variables,
					Const.Globals.XML_VECTOR_VARIABLE_NAME);
			}
			return false;
		}

		/// <summary>
		/// Удалить данные.
		/// </summary>
		public void DeleteVariables()
		{
			foreach (DataTable table in CurrentDataSet.Tables)
			{
				table.Clear();
			}

			if (Doc.Variables.Count > 0)
			{
				Content.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_VECTOR_VARIABLE_NAME);
			}
			State = WordDocumentMode.Default;
		}

		public void ShowDocumentKeysDialog()
		{
			Form dialog = new DocumentKeysDialog(CurrentDataSet);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				CommitVariables();
			}
		}

		public void ShowEditCategoriesDialog()
		{
			Form dialog = new CategoriesEditorDialog(this);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				//Pane.DataSetRefresh();
			}
		}

		public void ShowCreateTableDialog()
		{
			Form dialog = new CreateTableDialog(this);
			Utils.Dialogs.ShowDialog(dialog);
			//OnPropertiesChanged(new EventArgs());
		}

		public void ShowEditTableDialog()
		{
			Form dialog = new TableEditorDialog(this);
			Utils.Dialogs.ShowDialog(dialog);
			//OnPropertiesChanged(new EventArgs());
		}

		public void ImportDataFromWordDocuments()
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				FileSystem.GetDataSetFromWordFiles(dialog.SelectedPath, ref nowAggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public void ImportDataFromWordDocument()
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = "Документ Word|*.doc;*.docx| Текстовый файл с контекстом заметок|*.*"
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (dialog.FilterIndex == 0)
				{
					FileSystem.GetDataSetFromWordFile(dialog.FileName, ref nowAggregatedDataSet);
					Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
					Doc.Saved = false;
				}
				else
				{
					FileSystem.GetContentFromTextFile(sourceDataSet: CurrentDataSet, fileName: dialog.FileName);
				}
			}
		}

		public void ImportOldDataFromWordDocumentsFolder()
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (lastAggregatedDataSet == null)
					lastAggregatedDataSet = Xml.GetLastAggregatedDataSet(Doc: Doc, out _);

				FileSystem.GetDataSetFromWordFiles(dialog.SelectedPath, ref lastAggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public void ImportOldDataFromWordDocument()
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = "Документ Word|*.doc;*.docx"
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (dialog.FilterIndex == 0)
				{
					Xml.CopyModel(nowAggregatedDataSet, lastAggregatedDataSet);
					FileSystem.GetDataSetFromWordFile(dialog.FileName, ref lastAggregatedDataSet);
					Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
					Doc.Saved = false;
				}
			}
		}

		public void ShowTableViewerDialog()
		{
			TableViewerDialog dialog = new TableViewerDialog(NowAggregatedDataSet, LastAggregatedDataSet);
			Utils.Dialogs.ShowDialog(dialog);
		}

		public void ShowAnalyzerDialog()
		{
			AnalyzerDialog dialog = new AnalyzerDialog(this);
			Utils.Dialogs.Show(dialog);
		}

		public void AddTextNote(Word.Selection selection)
		{
			TextNoteDialog dialog = new TextNoteDialog(CurrentDataSet, selection);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				AddTextNote(
					categoryGuid: dialog.Category.Guid,
					subcategoryGuid: dialog.Subcategory.Guid,
					description: dialog.Description,
					value: dialog.Value as string,
					rating: dialog.Rating,
					selectionStart: dialog.SelectionStart,
					selectionEnd: dialog.SelectionEnd
					);
			}
		}

		public void AddDecimalNote(Word.Selection selection)
		{
			DecimalNoteDialog dialog = new DecimalNoteDialog(CurrentDataSet, selection);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				AddDecimalNote(
					categoryGuid: dialog.Category.Guid,
					subcategoryGuid: dialog.Subcategory.Guid,
					description: dialog.Description,
					value: (double)dialog.Value,
					rating: dialog.Rating,
					selectionStart: dialog.SelectionStart,
					selectionEnd: dialog.SelectionEnd
					);
			}
		}

		public void AddTextNote(string categoryGuid, string subcategoryGuid, string description, string value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFileId(FileName);
			CurrentDataSet.TextNotes.Rows.Add(new object[]
			{
				null,
				categoryGuid,
				subcategoryGuid,
				description,
				value,
				rating,
				selectionStart,
				selectionEnd,
				fileId
			});
			CommitVariables();
		}

		public static void AddTextNote(Word._Document document, int categoryId, int subcategoryId, int rating, int selectionStart, int selectionEnd)
		{
			RepositoryDataSet dataSet = Xml.GetCurrentDataSet(document, out bool isCorrect);
			if (isCorrect)
			{
				int fileId = GetFileId(dataSet: dataSet, fileName: document.FullName);
				string categoryGuid = dataSet.Categories[categoryId].key_guid;
				string subcategoryGuid = dataSet.Subcategories[subcategoryId].key_guid;
				Word.Range range = document.Range(selectionStart, selectionEnd);
				dataSet.TextNotes.Rows.Add(new object[]
				{
					null,
					categoryGuid,
					subcategoryGuid,
					null,
					range.Text,
					rating,
					selectionStart,
					selectionEnd,
					fileId
				});
				if (dataSet.HasChanges())
				{
					Content.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}

		public void AddDecimalNote(string categoryGuid, string subcategoryGuid, string description, double value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFileId(FileName);
			CurrentDataSet.DecimalNotes.Rows.Add(new object[]
			{
				null,
				categoryGuid,
				subcategoryGuid,
				description,
				value,
				rating,
				selectionStart,
				selectionEnd,
				fileId
			});
			CommitVariables();
		}

		public static void AddDecimalNote(Word._Document document, int categoryId, int subcategoryId, double value, int rating, int selectionStart, int selectionEnd)
		{
			RepositoryDataSet dataSet = Xml.GetCurrentDataSet(document, out bool isCorrect);
			if (isCorrect)
			{
				int fileId = GetFileId(dataSet: dataSet, fileName: document.FullName);
				string categoryGuid = dataSet.Categories[categoryId].key_guid;
				string subcategoryGuid = dataSet.Subcategories[subcategoryId].key_guid;
				dataSet.DecimalNotes.Rows.Add(new object[]
				{
					null,
					categoryGuid,
					subcategoryGuid,
					null,
					value,
					rating,
					selectionStart,
					selectionEnd,
					fileId
				});
				if (dataSet.HasChanges())
				{
					Content.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}

		public void ShowSearchServiceDialog()
		{
			Form dialog = new SelectCategoriesDialog(this);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				Pane.NotesControl.ShowButtons = true;
				Services.Searcher.Search(
					document: this,
					subcategories: ((SelectCategoriesDialog)dialog).CheckedSubcategories);

				MessageBox.Show("Разметка документа с помощью поисковых функций выполнена!",
							"Разметка документа",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				Pane.NotesControl.ShowButtons = false;
			}
		}

		public void MLSearchService()
		{
			Pane.NotesControl.ShowButtons = true;
			Services.MLService.Search(document: this, Const.Globals.LEVEL_PASSAGE);
			MessageBox.Show("Разметка документа с помощью нейронной сети выполнена!",
						"Разметка документа",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
			Pane.NotesControl.ShowButtons = false;
		}

		public void LLMSearchService()
		{
			Pane.NotesControl.ShowButtons = true;
			Services.MLService.Search(document: this, Const.Globals.LEVEL_PASSAGE);
			MessageBox.Show("Разметка документа с помощью нейронной сети выполнена!",
						"Разметка документа",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
			Pane.NotesControl.ShowButtons = false;
		}

		private int GetFileId(string fileName) => GetFileId(dataSet: CurrentDataSet, fileName: fileName);

		private static int GetFileId(RepositoryDataSet dataSet, string fileName)
		{
			if (dataSet.WordFiles.Exists(fileName: fileName))
			{
				return dataSet.WordFiles.Get(fileName: fileName).Id;
			}
			else
			{
				return dataSet.WordFiles.Add(fileName: fileName, caption: string.Empty, description: string.Empty, date: DateTime.Now).Id;
			}
		}

		/// <summary>
		/// Освобождает ресурсы, занятые панелью управления.
		/// </summary>
		public void Dispose() => CustomPane?.Dispose();

		public enum WordDocumentMode
		{
			/// <summary>
			/// По умолчанию (шаблоны проверки не загружены)
			/// </summary>
			Default,
			/// <summary>
			/// Разделение (Анализ одного документа)
			/// </summary>
			Separate,
			/// <summary>
			/// Объединение (Объединение аналитических данных с нескольких документов)
			/// </summary>
			Combine
		}

	}
}
