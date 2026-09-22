// Ignore Spelling: dest

using Microsoft.Office.Tools;
using System;
using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.EventsBus;
using WordHiddenPowers.EventsBus.EventArgs;
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
		private readonly DocumentCollection parent;

		private WordDocumentMode _state = WordDocumentMode.Default;

		private CustomTaskPane customPane;

		/// <summary>
		/// Статус документа.
		/// </summary>
		internal WordDocumentMode State
		{
			get => _state;
			private set
			{
				if (_state != value)
				{
					_state = value;
					GlobalsEventsBus.DoDocumentPropertiesChanged(this);
				}
			}
		}

		internal bool IsTableSchema => currentDataSet != null && currentDataSet.IsTableSchema;

		/// <summary>
		/// Основные данные документа.
		/// </summary>
		private DocumentDataSet currentDataSet;

		/// <summary>
		/// Агрегированные данные из нескольких документов.
		/// </summary>
		private DocumentDataSet nowAggregatedDataSet;

		/// <summary>
		/// Сопоставимые агрегированные данные из нескольких документов (например, за прошлый период).
		/// </summary>
		private DocumentDataSet lastAggregatedDataSet;

		/// <summary>
		/// Хранилище векторов.
		/// </summary>
		private VectorDataSet vectorDataSet;

		/// <summary>
		/// Связанная с документом боковая панель.
		/// </summary>
		public CustomTaskPane CustomPane
		{
			get
			{
				if (customPane == null)
				{
					UserControl pane = new AddInPane(this, Hwnd);
					customPane = Globals.ThisAddIn.CustomTaskPanes.Add(pane, Const.Panes.PANE_TITLE, Doc.Windows[1]);
					customPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
					customPane.Width = 600;
					customPane.VisibleChanged += new EventHandler(CustomPane_VisibleChanged);
					customPane.Visible = GlobalsEventsBus.PaneVisible;

					Pane.PropertiesChanged += new EventHandler<EventArgs>(Pane_PropertiesChanged);
					Pane.NotesControlVisible = _state == WordDocumentMode.Separate;
				}
				return customPane;
			}
		}

		public AddInPane Pane => CustomPane is not null ? CustomPane.Control as AddInPane : null;

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

		public DocumentDataSet CurrentDataSet
		{
			get
			{
				if (currentDataSet == null)
				{
					currentDataSet = Xml.GetCurrentDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						currentDataSet = new DocumentDataSet();
					}
				}
				return currentDataSet;
			}
		}

		public DocumentDataSet NowAggregatedDataSet
		{
			get
			{
				if (nowAggregatedDataSet == null)
				{
					nowAggregatedDataSet = Xml.GetNowAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						nowAggregatedDataSet = new DocumentDataSet();
					}
				}
				return nowAggregatedDataSet;
			}
		}

		public DocumentDataSet LastAggregatedDataSet
		{
			get
			{
				if (lastAggregatedDataSet == null)
				{
					lastAggregatedDataSet = Xml.GetLastAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						lastAggregatedDataSet = new DocumentDataSet();
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

			if (Content.ExistsVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME))
			{
				_state = WordDocumentMode.Combine;
			}
			else if (Content.ExistsVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME))
			{
				_state = WordDocumentMode.Separate;
			}
			else
			{
				_state = WordDocumentMode.Default;
			}
			
			GlobalsEventsBus.DocumentSelectionChange += new EventHandler<WordSelectionEventArgs>(GlobalsEventsBus_DocumentSelectionChange);
			GlobalsEventsBus.PaneStateChanged += new EventHandler<PaneStateEventArgs>(GlobalsEventsBus_PaneStateChanged);
		}

		private void GlobalsEventsBus_PaneStateChanged(object sender, PaneStateEventArgs e)
		{
			Timer delayTimer = new()
			{
				Interval = 10
			};
			delayTimer.Tick += (timerSender, timerArgs) =>
			{
				delayTimer.Stop();
				delayTimer.Dispose();
				if (CustomPane is not null)
				{
					Globals.Ribbons.AddInMainRibbon.paneVisibleButton.Enabled = true;
					CustomPane.Visible = e.IsVisible;
					Globals.Ribbons.AddInMainRibbon.paneVisibleButton.Checked = e.IsVisible;
				}
				else
				{
					Globals.Ribbons.AddInMainRibbon.paneVisibleButton.Enabled = false;
				}
			};
			delayTimer.Start();
		}

		private void CustomPane_VisibleChanged(object sender, EventArgs e)
		{
			if (CustomPane.Visible && !GlobalsEventsBus.PaneVisible)
				GlobalsEventsBus.SetPaneVisibleState(this);
			else if (!CustomPane.Visible && GlobalsEventsBus.PaneVisible)
				GlobalsEventsBus.SetPaneHideState(this);
		}

		private void GlobalsEventsBus_DocumentSelectionChange(object sender, WordSelectionEventArgs e)
		{
			if (e.Selection.Document.Windows[1].Hwnd != Hwnd) return;

			if (e.Selection.Start == e.Selection.End)
			{
				ChatMessageMode = ChatMessageModeEnum.InsertHere;
				previousCharacter = e.Selection.Start == 0 ? default : e.Selection.Document.Range(e.Selection.Start - 1, e.Selection.Start);
				nextCharacter = e.Selection.End == e.Selection.Document.Range().End ? default : e.Selection.Document.Range(e.Selection.End, e.Selection.End + 1);
			}
			else
			{
				bool isPrevious = IsPreviousParagraphIsNull(e.Selection, out previousParagraph);
				bool isBetween = IsCenterParagraphIsNull(e.Selection, out firstRange, out centerParagraph, out lastRange);
				bool isNext = IsNextParagraphIsNull(e.Selection, out nextParagraph);
				messageRange = e.Selection.Range;

				if (!isPrevious && !isBetween && !isNext)
				{
					ChatMessageMode = ChatMessageModeEnum.ReplaceSelection;
					previousCharacter = e.Selection.Start == 0 ? default : e.Selection.Document.Range(e.Selection.Start - 1, e.Selection.Start);
					nextCharacter = e.Selection.End == e.Selection.Document.Range().End ? default : e.Selection.Document.Range(e.Selection.End, e.Selection.End + 1);
				}
				else
				{
					previousCharacter = default;
					nextCharacter = default;

					ChatMessageMode = ChatMessageModeEnum.ReplaceSelection | ChatMessageModeEnum.Insert;
					if (isPrevious) ChatMessageMode |= ChatMessageModeEnum.Previous;
					if (isBetween) ChatMessageMode |= ChatMessageModeEnum.Between;
					if (isNext) ChatMessageMode |= ChatMessageModeEnum.Next;
				}
			}

			GlobalsEventsBus.DoDocumentChatMessageModeChanged(this, ChatMessageMode);
		}

		public static Document Create(DocumentCollection parent, string fileName, Word._Document Doc)
		{
			Document document = new(
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
		/// Загрузить чистую структуру основных данных файла.
		/// </summary>
		/// <param name="fileName">XML файл.</param>
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
		/// Загрузить основные данные из файла.
		/// </summary>
		/// <param name="fileName">XML файл.</param>
		public void LoadCurrentData(string fileName)
		{
			LoadDataSet(CurrentDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, CurrentDataSet);
			State = WordDocumentMode.Separate;
		}

		/// <summary>
		/// Загрузить агрегированные данные текущего периода из файла.
		/// </summary>
		/// <param name="fileName">XML файл.</param>
		public void LoadNowAggregatedData(string fileName)
		{
			LoadDataSet(NowAggregatedDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
			State = WordDocumentMode.Combine;
		}

		/// <summary>
		/// Загрузить агрегированные данные прошлого периода из файла.
		/// </summary>
		/// <param name="fileName">XML файл.</param>
		public void LoadLastAggregatedData(string fileName)
		{
			LoadDataSet(LastAggregatedDataSet, fileName);
			Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
			State = WordDocumentMode.Combine;
		}

		private void LoadDataSet(DocumentDataSet dataSet, string fileName)
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

		private void ClearDataSet(DocumentDataSet dataSet)
		{
			try
			{
				foreach (DataTable table in dataSet.Tables)
				{
					table.Clear();
				}
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		/// <summary>
		/// Загрузить векторную базу даных из файла.
		/// </summary>
		/// <param name="fileName">XML файл.</param>
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
		/// Зафиксировать дополниетльные данные.
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
		/// Проверить наличие дополнительных данных.
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
		/// Удалить дополнительные данные.
		/// </summary>
		public void DeleteVariables()
		{
			ClearDataSet(CurrentDataSet);

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
			FolderBrowserDialog dialog = new();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				FileSystem.GetDataSetFromWordDirectory(dialog.SelectedPath, ref nowAggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public void ImportDataFromWordDocument()
		{
			OpenFileDialog dialog = new()
			{
				Filter = "Документ Word|*.doc;*.docx| Текстовый файл с контекстом заметок|*.*"
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (dialog.FilterIndex == 1)
				{
					FileSystem.GetDataSetFromWordFile(dialog.FileName, ref nowAggregatedDataSet);
					Content.CommitVariable(Doc.Variables, Const.Globals.XML_NOW_AGGREGATED_VARIABLE_NAME, NowAggregatedDataSet);
					Doc.Saved = false;
					State = WordDocumentMode.Combine;
				}
				else
				{
					FileSystem.GetContentFromTextFile(sourceDataSet: CurrentDataSet, fileName: dialog.FileName);
				}
			}
		}

		public void ImportOldDataFromWordDocumentsFolder()
		{
			FolderBrowserDialog dialog = new();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (lastAggregatedDataSet == null)
					lastAggregatedDataSet = Xml.GetLastAggregatedDataSet(Doc: Doc, out _);

				FileSystem.GetDataSetFromWordDirectory(dialog.SelectedPath, ref lastAggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_LAST_AGGREGATED_VARIABLE_NAME, LastAggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public void ImportOldDataFromWordDocument()
		{
			OpenFileDialog dialog = new()
			{
				Filter = "Документ Word|*.doc;*.docx"
			};
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (dialog.FilterIndex == 1)
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
			TableViewerDialog dialog = new(NowAggregatedDataSet, LastAggregatedDataSet);
			Utils.Dialogs.ShowDialog(dialog);
		}

		public void ShowAnalyzerDialog()
		{
			AnalyzerDialog dialog = new(this);
			Utils.Dialogs.Show(dialog);
		}

		public void AddTextNote(Word.Selection selection)
		{
			TextNoteDialog dialog = new(CurrentDataSet, selection);
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
			DecimalNoteDialog dialog = new(CurrentDataSet, selection);
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
			CurrentDataSet.TextNotes.Rows.Add(
			[
				null,
				categoryGuid,
				subcategoryGuid,
				description,
				value,
				rating,
				selectionStart,
				selectionEnd,
				fileId
			]);
			CommitVariables();
		}

		public static void AddTextNote(Word._Document document, int categoryId, int subcategoryId, int rating, int selectionStart, int selectionEnd)
		{
			DocumentDataSet dataSet = Xml.GetCurrentDataSet(document, out bool isCorrect);
			if (isCorrect)
			{
				int fileId = GetFileId(dataSet: dataSet, fileName: document.FullName);
				string categoryGuid = dataSet.Categories[categoryId].key_guid;
				string subcategoryGuid = dataSet.Subcategories[subcategoryId].key_guid;
				Word.Range range = document.Range(selectionStart, selectionEnd);
				dataSet.TextNotes.Rows.Add(
				[
					null,
					categoryGuid,
					subcategoryGuid,
					null,
					range.Text,
					rating,
					selectionStart,
					selectionEnd,
					fileId
				]);
				if (dataSet.HasChanges())
				{
					Content.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}

		public void AddDecimalNote(string categoryGuid, string subcategoryGuid, string description, double value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFileId(FileName);
			CurrentDataSet.DecimalNotes.Rows.Add(
			[
				null,
				categoryGuid,
				subcategoryGuid,
				description,
				value,
				rating,
				selectionStart,
				selectionEnd,
				fileId
			]);
			CommitVariables();
		}

		public static void AddDecimalNote(Word._Document document, int categoryId, int subcategoryId, double value, int rating, int selectionStart, int selectionEnd)
		{
			DocumentDataSet dataSet = Xml.GetCurrentDataSet(document, out bool isCorrect);
			if (isCorrect)
			{
				int fileId = GetFileId(dataSet: dataSet, fileName: document.FullName);
				string categoryGuid = dataSet.Categories[categoryId].key_guid;
				string subcategoryGuid = dataSet.Subcategories[subcategoryId].key_guid;
				dataSet.DecimalNotes.Rows.Add(
				[
					null,
					categoryGuid,
					subcategoryGuid,
					null,
					value,
					rating,
					selectionStart,
					selectionEnd,
					fileId
				]);
				if (dataSet.HasChanges())
				{
					Content.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}

		private int GetFileId(string fileName) => GetFileId(dataSet: CurrentDataSet, fileName: fileName);

		private static int GetFileId(DocumentDataSet dataSet, string fileName)
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
		public void Dispose()
		{
			GlobalsEventsBus.DocumentSelectionChange -= GlobalsEventsBus_DocumentSelectionChange;
			CustomPane?.Dispose();
		}

		/// <summary>
		/// Статус документа (наличие дополнительных данных). 
		/// </summary>
		public enum WordDocumentMode
		{
			/// <summary>
			/// По умолчанию (шаблоны не загружены)
			/// </summary>
			Default,

			/// <summary>
			/// Разделение (анализ одного документа)
			/// </summary>
			Separate,

			/// <summary>
			/// Объединение (коллекция аналитических данных из нескольких документов)
			/// </summary>
			Combine
		}
	}
}
