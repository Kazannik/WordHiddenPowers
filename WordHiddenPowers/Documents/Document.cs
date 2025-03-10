// Ignore Spelling: dest

using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Utils;
using static WordHiddenPowers.Repositories.RepositoryDataSet;
using DataTable = System.Data.DataTable;
using Office = Microsoft.Office.Core;
using Table = WordHiddenPowers.Repositories.Data.Table;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public class Document : IDisposable
	{
		private readonly DocumentCollection parent;

		private RepositoryDataSet currentDataSet;

		private RepositoryDataSet aggregatedDataSet;

		public CustomTaskPane CustomPane { get; }
		
		private void NotesPane_PropertiesChanged(object sender, EventArgs e)
		{
			if (Caption != Pane.Caption)
			{
				Caption = Pane.Caption;
				Doc.Saved = false;
			}
			if (Date != Pane.Date)
			{
				Date = Pane.Date;
				Doc.Saved = false;
			}
			if (Description != Pane.Description)
			{
				Description = Pane.Description;
				Doc.Saved = false;
			}

			if (CurrentDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.XML_CURRENT_VARIABLE_NAME, 
					dataSet: CurrentDataSet);
				Doc.Saved = false;
			}

			if (AggregatedDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_AGGREGATED_VARIABLE_NAME,
					dataSet: AggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public NotesPane Pane => CustomPane.Control as NotesPane;
		
		public string FileName { get; }

		public string Caption
		{
			get
			{
				return ContentUtil.GetCaption(Doc);
			}
			set
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.CAPTION_VARIABLE_NAME, 
					value: value);
			}
		}

		public DateTime Date
		{
			get
			{
				return ContentUtil.GetDate(Doc);
			}
			set
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.DATE_VARIABLE_NAME, 
					value: value.ToShortDateString());
			}
		}

		public string Description
		{
			get
			{
				return ContentUtil.GetDescription(Doc);
			}
			set
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.DESCRIPTION_VARIABLE_NAME,
					value: value);
			}
		}

		public Table Table
		{
			get
			{
				return ContentUtil.GetTable(Doc);
			}
			set
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.TABLE_VARIABLE_NAME,
					value: value.ToString());
			}
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

		public Word._Document Doc { get; }
				
		private Document(DocumentCollection parent, string fileName, Word._Document doc)
		{
			this.parent = parent;

			dialogs = new List<Form>();
			FileName = fileName;
			ContentHide = false;
			Doc = doc;

			bool oldState = this.parent.PaneVisibleButton.Checked;
			this.parent.PaneVisibleButton.Checked = false;

			CustomPane = Globals.ThisAddIn.CustomTaskPanes.Add(new NotesPane(this), Const.Panes.PANE_TITLE, Doc.Windows[1]);
			CustomPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
			CustomPane.Width = 400;

			CustomPane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
			Pane.PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);
			
			CustomPane.Visible = oldState;
			this.parent.PaneVisibleButton.Checked = oldState;
		}
		
		void Pane_VisibleChanged(object sender, EventArgs e)
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

				CurrentDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				CurrentDataSet.AcceptChanges();
				
				CommitVariables();
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
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
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		public void SaveData(string fileName)
		{
			Xml.SaveDataSchema(Globals.ThisAddIn.Documents.ActiveDocument.CurrentDataSet, fileName);
		}

		public void CommitVariables()
		{
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME, Caption);
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

			if (CurrentDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, CurrentDataSet);
			}

			if (AggregatedDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
			}
		}
		
		public bool VariablesExists()
		{
			if (Doc.Variables.Count > 0)
			{
				if (ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.CAPTION_VARIABLE_NAME))
					return true;
				else if (ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.DATE_VARIABLE_NAME))
					return true;
				else if (ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.DESCRIPTION_VARIABLE_NAME))
					return true;
				else if (ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.TABLE_VARIABLE_NAME))
					return true;
				else if (ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.XML_CURRENT_VARIABLE_NAME))
					return true;
				return ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.XML_AGGREGATED_VARIABLE_NAME);
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
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME);
			}
		}

		protected IList<Form> dialogs = null;

		public void ShowDocumentKeysDialog()
		{
			Form dialog = new DocumentKeysDialog(CurrentDataSet);
			dialogs.Add(dialog);
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				CommitVariables();
			}
		}

		public void ShowEditCategoriesDialog()
		{
			Form dialog = new CategoriesEditorDialog(this);
			dialogs.Add(dialog);
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				//Pane.DataSetRefresh();
			}
		}

		public void ShowCreateTableDialog()
		{
			Form dialog = new CreateTableDialog(this);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
			//OnPropertiesChanged(new EventArgs());
		}

		public void ShowEditTableDialog()
		{
			Form dialog = new TableEditorDialog(this);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
			//OnPropertiesChanged(new EventArgs());
		}

		public void ImportDataFromWordDocuments()
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				FileSystemUtil.GetDataSetFromWordFiles(dialog.SelectedPath, ref aggregatedDataSet);
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
				Doc.Saved = false;
			}
		}

		public void ImportDataFromWordDocument()
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = "Документ Word|*.doc,*.docx| Текстовый файл с контекстом заметок|*.*"
			};
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				if (dialog.FilterIndex == 0)
				{
					FileSystemUtil.GetDataSetFromWordFile(dialog.FileName, ref aggregatedDataSet);
					ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_AGGREGATED_VARIABLE_NAME, AggregatedDataSet);
					Doc.Saved = false;
				}
				else
				{
					FileSystemUtil.GetContentFromTextFile(sourceDataSet: CurrentDataSet, fileName: dialog.FileName);
				}
			}
		}

		public void ShowTableViewerDialog()
		{
			TableViewerDialog dialog = new TableViewerDialog(AggregatedDataSet);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
		}

		public void ShowAnalyzerDialog()
		{
			AnalyzerDialog dialog = new AnalyzerDialog(this);
			dialogs.Add(dialog);
			ShowDialogUtil.Show(dialog);
		}

		public void AddTextNote(Word.Selection selection)
		{
			TextNoteDialog dialog = new TextNoteDialog(CurrentDataSet, selection);
			if (dialog.ShowDialog() == DialogResult.OK)
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
			if (dialog.ShowDialog() == DialogResult.OK)
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
			int fileId = GetFile(FileName);
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
				int fileId = GetFile(dataSet: dataSet, fileName: document.FullName);
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
					ContentUtil.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}

		public void AddDecimalNote(string categoryGuid, string subcategoryGuid, string description, double value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFile(FileName);
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
				int fileId = GetFile(dataSet: dataSet, fileName: document.FullName);
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
					ContentUtil.CommitVariable(document.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, dataSet);
				}
			}
		}
				
		public void ShowSearchServiceDialog()
		{
			Form dialog = new SelectCategoriesDialog(this);
			dialogs.Add(dialog);
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				Pane.ShowButtons = true;
				Services.Searcher.Search(
					document: this,
					subcategories: ((SelectCategoriesDialog)dialog).CheckedSubcategories);
				MessageBox.Show("Разметка документа с помощью поисковых функций выполнена!",
							"Разметка документа",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
			}
		}

		public void AiSearchService()
		{
			Pane.ShowButtons = true;
			Services.Сombined.Search(document: this);
			MessageBox.Show("Разметка документа с помощью нейронной сети выполнена!",
						"Разметка документа",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
		}
		
		private int GetFile(string fileName)
		{
			return GetFile(dataSet: CurrentDataSet, fileName: fileName);			
		}

		private static int GetFile(RepositoryDataSet dataSet, string fileName)
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

		public void Dispose()
		{
			if (dialogs != null)
			{
				foreach (Form form in dialogs)
				{
					if (form != null)
					{
						form.Close();
						form.Dispose();
					}
				}
				dialogs.Clear();
			}
			CustomPane?.Dispose();
		}
	}
}
