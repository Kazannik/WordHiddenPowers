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
		private readonly DocumentCollection parent;

		/// <summary>
		/// Основные данные документа.
		/// </summary>
		private RepositoryDataSet currentDataSet;

		/// <summary>
		/// Агрегированные данные из нескольких документов.
		/// </summary>
		private RepositoryDataSet aggregatedDataSet;

		/// <summary>
		/// Сопоставимые агрегированные данные из нескольких документов (например, за прошлый период).
		/// </summary>
		private RepositoryDataSet oldAggregatedDataSet;

		public CustomTaskPane CustomPane { get; }

		public AddInPane Pane => CustomPane.Control as AddInPane;

		private void NotesPane_PropertiesChanged(object sender, EventArgs e)
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

			if (CurrentDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME,
					dataSet: CurrentDataSet);
				Doc.Saved = false;
			}

			if (AggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_AGGREGATED_VARIABLE_NAME,
					dataSet: AggregatedDataSet);
				Doc.Saved = false;
			}

			if (OldAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME,
					dataSet: OldAggregatedDataSet);
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
			get => CurrentDataSet.GetValue(key: Const.Globals.ML_MODEL_VARIABLE_NAME);
			set => CurrentDataSet.SetValue(key: Const.Globals.ML_MODEL_VARIABLE_NAME, value: value);
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

		public RepositoryDataSet AggregatedDataSet
		{
			get
			{
				if (aggregatedDataSet == null)
				{
					aggregatedDataSet = Xml.GetAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						aggregatedDataSet = new RepositoryDataSet();
					}
				}
				return aggregatedDataSet;
			}
		}

		public RepositoryDataSet OldAggregatedDataSet
		{
			get
			{
				if (oldAggregatedDataSet == null)
				{
					oldAggregatedDataSet = Xml.GetOldAggregatedDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						oldAggregatedDataSet = new RepositoryDataSet();
					}
				}
				return oldAggregatedDataSet;
			}
		}

		public Word._Document Doc { get; }

		private Document(DocumentCollection parent, string fileName, Word._Document doc)
		{
			this.parent = parent;

			FileName = fileName;
			ContentHide = false;
			Doc = doc;

			bool oldState = this.parent.PaneVisibleButton.Checked;
			this.parent.PaneVisibleButton.Checked = false;

			CustomPane = Globals.ThisAddIn.CustomTaskPanes.Add(new AddInPane(this), Const.Panes.PANE_TITLE, Doc.Windows[1]);
			CustomPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
			CustomPane.Width = 600;

			CustomPane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
			
			Pane.PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);

			CustomPane.Visible = oldState;
			this.parent.PaneVisibleButton.Checked = oldState;
		}

		private void Pane_VisibleChanged(object sender, EventArgs e)
		{
			CustomTaskPane pane = (CustomTaskPane)sender;
			if (parent != null) parent.PaneVisibleButton.Checked = pane.Visible;
		}

		public static Document Create(DocumentCollection parent, string fileName, Word._Document Doc)
		{
			Document document = new Document(
				parent: parent,
				fileName: fileName,
				doc: Doc);
			return document;
		}

		public void NewData()
		{
			CurrentDataSet.RowsHeaders.Clear();
			CurrentDataSet.ColumnsHeaders.Clear();

			CurrentDataSet.Subcategories.Clear();
			CurrentDataSet.Categories.Clear();

			CurrentDataSet.DecimalPowers.Clear();
			CurrentDataSet.TextPowers.Clear();

			CurrentDataSet.DocumentKeys.Clear();
			CurrentDataSet.WordFiles.Clear();

			CurrentDataSet.Setting.Clear();

			CurrentDataSet.AcceptChanges();

			CommitVariables();
		}

		public void LoadCurrentData(string fileName)
		{
			try
			{
				CurrentDataSet.RowsHeaders.Clear();
				CurrentDataSet.ColumnsHeaders.Clear();

				CurrentDataSet.Subcategories.Clear();
				CurrentDataSet.Categories.Clear();

				CurrentDataSet.DocumentKeys.Clear();
				CurrentDataSet.WordFiles.Clear();

				CurrentDataSet.DecimalPowers.Clear();
				CurrentDataSet.TextPowers.Clear();

				CurrentDataSet.Setting.Clear();

				CurrentDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				CurrentDataSet.AcceptChanges();

				CommitVariables();
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		public void LoadAggregatedData(string fileName)
		{
			try
			{
				AggregatedDataSet.RowsHeaders.Clear();
				AggregatedDataSet.ColumnsHeaders.Clear();

				AggregatedDataSet.Subcategories.Clear();
				AggregatedDataSet.Categories.Clear();

				AggregatedDataSet.DocumentKeys.Clear();
				AggregatedDataSet.WordFiles.Clear();

				AggregatedDataSet.DecimalPowers.Clear();
				AggregatedDataSet.TextPowers.Clear();

				AggregatedDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				AggregatedDataSet.AcceptChanges();

				CommitVariables();
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		public void LoadOldAggregatedData(string fileName)
		{
			try
			{
				OldAggregatedDataSet.RowsHeaders.Clear();
				OldAggregatedDataSet.ColumnsHeaders.Clear();

				OldAggregatedDataSet.Subcategories.Clear();
				OldAggregatedDataSet.Categories.Clear();

				OldAggregatedDataSet.DocumentKeys.Clear();
				OldAggregatedDataSet.WordFiles.Clear();

				OldAggregatedDataSet.DecimalPowers.Clear();
				OldAggregatedDataSet.TextPowers.Clear();

				OldAggregatedDataSet.Setting.Clear();

				OldAggregatedDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				OldAggregatedDataSet.AcceptChanges();

				CommitVariables();
			}
			catch (Exception ex)
			{
				Utils.Dialogs.ShowErrorDialog(ex.Message);
			}
		}

		public void SaveData(string fileName) =>
			Xml.SaveDataSchema(Globals.ThisAddIn.Documents.ActiveDocument.CurrentDataSet, fileName);

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

			if (AggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
			}

			if (OldAggregatedDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME, OldAggregatedDataSet);
			}
		}

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
					Const.Globals.XML_AGGREGATED_VARIABLE_NAME))
					return true;
				return Content.ExistsVariable(Doc.Variables,
					Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME);
			}
			return false;
		}

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
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME);
			}
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
				FileSystem.GetDataSetFromWordFiles(dialog.SelectedPath, ref aggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
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
					FileSystem.GetDataSetFromWordFile(dialog.FileName, ref aggregatedDataSet);
					Content.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
					Doc.Saved = false;
				}
				else
				{
					FileSystem.GetContentFromTextFile(sourceDataSet: CurrentDataSet, fileName: dialog.FileName);
				}
			}
		}

		public void ImportOldDataFromWordDocuments()
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				if (oldAggregatedDataSet == null)
					oldAggregatedDataSet = Xml.GetOldAggregatedDataSet(Doc: Doc, out _);

				FileSystem.GetDataSetFromWordFiles(dialog.SelectedPath, ref oldAggregatedDataSet);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME, OldAggregatedDataSet);
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
					Xml.CopyModel(aggregatedDataSet, oldAggregatedDataSet);
					FileSystem.GetDataSetFromWordFile(dialog.FileName, ref oldAggregatedDataSet);
					Content.CommitVariable(Doc.Variables, Const.Globals.XML_OLD_AGGREGATED_VARIABLE_NAME, OldAggregatedDataSet);
					Doc.Saved = false;
				}
			}
		}

		public void ShowTableViewerDialog()
		{
			TableViewerDialog dialog = new TableViewerDialog(AggregatedDataSet, OldAggregatedDataSet);
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
			CurrentDataSet.TextPowers.Rows.Add(new object[]
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
				dataSet.TextPowers.Rows.Add(new object[]
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
			CurrentDataSet.DecimalPowers.Rows.Add(new object[]
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
				dataSet.DecimalPowers.Rows.Add(new object[]
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
	}
}
