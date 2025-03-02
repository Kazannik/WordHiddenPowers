// Ignore Spelling: dest

using Microsoft.Office.Interop.Word;
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

		private RepositoryDataSet bufferDataSet;

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

			if (DataSet.HasChanges())
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.XML_VARIABLE_NAME, 
					dataSet: DataSet);
				Doc.Saved = false;
			}

			if (ImportDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(
					array: Doc.Variables,
					variableName: Const.Globals.XML_IMPORT_VARIABLE_NAME,
					dataSet: ImportDataSet);
				Doc.Saved = false;
			}
		}

		public NotesPane Pane
		{
			get
			{
				return CustomPane.Control as NotesPane;
			}
		}

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

		public RepositoryDataSet DataSet
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

		public RepositoryDataSet ImportDataSet
		{
			get
			{
				if (bufferDataSet == null)
				{
					bufferDataSet = Xml.GetAnalyzerDataSet(Doc: Doc, out bool isCorrect);
					if (!isCorrect)
					{
						bufferDataSet = new RepositoryDataSet();
					}
				}
				return bufferDataSet;
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

			bool oldState = this.parent.paneVisibleButton.Checked;
			this.parent.paneVisibleButton.Checked = false;

			CustomPane = Globals.ThisAddIn.CustomTaskPanes.Add(new NotesPane(this), Const.Panes.PANE_TITLE, Doc.ActiveWindow);
			CustomPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
			CustomPane.Width = 400;
			
			CustomPane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
			Pane.PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);
			
			CustomPane.Visible = oldState;
			this.parent.paneVisibleButton.Checked = oldState;
		}

		void Pane_VisibleChanged(object sender, EventArgs e)
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
				
		public void NewData()
		{
			DataSet.RowsHeaders.Clear();
			DataSet.ColumnsHeaders.Clear();

			DataSet.Subcategories.Clear();
			DataSet.Categories.Clear();

			DataSet.DecimalPowers.Clear();
			DataSet.TextPowers.Clear();

			DataSet.DocumentKeys.Clear();
			DataSet.WordFiles.Clear();

			DataSet.AcceptChanges();

			CommitVariables();
		}

		public void LoadData(string fileName)
		{
			try
			{
				DataSet.RowsHeaders.Clear();
				DataSet.ColumnsHeaders.Clear();

				DataSet.Subcategories.Clear();
				DataSet.Categories.Clear();
				
				DataSet.DocumentKeys.Clear();
				DataSet.WordFiles.Clear();

				DataSet.DecimalPowers.Clear();
				DataSet.TextPowers.Clear();

				DataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				DataSet.AcceptChanges();
				
				CommitVariables();
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		public void LoadAnalyzerData(string fileName)
		{
			try
			{
				ImportDataSet.RowsHeaders.Clear();
				ImportDataSet.ColumnsHeaders.Clear();

				ImportDataSet.Subcategories.Clear();
				ImportDataSet.Categories.Clear();

				ImportDataSet.DocumentKeys.Clear();
				ImportDataSet.WordFiles.Clear();

				ImportDataSet.DecimalPowers.Clear();
				ImportDataSet.TextPowers.Clear();

				ImportDataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				ImportDataSet.AcceptChanges();

				CommitVariables();
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		public void SaveData(string fileName)
		{
			Xml.SaveDataSchema(Globals.ThisAddIn.Documents.ActiveDocument.DataSet, fileName);
		}

		public void CommitVariables()
		{
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME, Caption);
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
			ContentUtil.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

			if (DataSet.HasChanges())
			{
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME, DataSet);
			}

			if (ImportDataSet.HasChanges())
			{
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME, ImportDataSet);
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
					Const.Globals.XML_VARIABLE_NAME))
					return true;
				return ContentUtil.ExistsVariable(Doc.Variables,
					Const.Globals.XML_IMPORT_VARIABLE_NAME);
			}
			return false;
		}

		public void DeleteVariables()
		{
			foreach (DataTable table in DataSet.Tables)
			{
				table.Clear();
			}

			if (Doc.Variables.Count > 0)
			{
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
				ContentUtil.DeleteVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME);
			}
		}

		protected IList<Form> dialogs = null;

		public void ShowDocumentKeysDialog()
		{
			Form dialog = new DocumentKeysDialog(DataSet);
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
				FileSystemUtil.GetDataSetFromWordFiles(dialog.SelectedPath, ref bufferDataSet);
				ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME, ImportDataSet);
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
					FileSystemUtil.GetDataSetFromWordFile(dialog.FileName, ref bufferDataSet);
					ContentUtil.CommitVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME, ImportDataSet);
					Doc.Saved = false;
				}
				else
				{
					FileSystemUtil.GetContentFromTextFile(sourceDataSet: DataSet, fileName: dialog.FileName);
				}
			}
		}

		public void ShowTableViewerDialog()
		{
			TableViewerDialog dialog = new TableViewerDialog(ImportDataSet);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
		}

		public void ShowAnalyzerDialog()
		{
			AnalyzerDialog dialog = new AnalyzerDialog(this);
			dialogs.Add(dialog);
			ShowDialogUtil.Show(dialog);
		}

		public void AddTextNote(Selection selection)
		{
			TextNoteDialog dialog = new TextNoteDialog(DataSet, selection);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				AddTextNote(
					categoryGuid: dialog.Category.Guid,
					subcategoryGuid: dialog.Subcategory.Guid,
					description: dialog.Description,
					value: dialog.Value,
					rating: dialog.Rating,
					selectionStart: dialog.SelectionStart,
					selectionEnd: dialog.SelectionEnd
					);
			}
		}

		public void AddDecimalNote(Selection selection)
		{
			DecimalNoteDialog dialog = new DecimalNoteDialog(DataSet, selection);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				AddDecimalNote(
					categoryGuid: dialog.Category.Guid,
					subcategoryGuid: dialog.Subcategory.Guid,
					description: dialog.Description,
					value: dialog.Value,
					rating: dialog.Rating,
					selectionStart: dialog.SelectionStart,
					selectionEnd: dialog.SelectionEnd
					);
			}
		}

		private void AddTextNote(string categoryGuid, string subcategoryGuid, string description, string value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFile(FileName);
			DataSet.TextPowers.Rows.Add(new object[]
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
				Range range = document.Range(selectionStart, selectionEnd);
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
					ContentUtil.CommitVariable(document.Variables, Const.Globals.XML_VARIABLE_NAME, dataSet);
				}
			}
		}

		private void AddDecimalNote(string categoryGuid, string subcategoryGuid, string description, double value, int rating, int selectionStart, int selectionEnd)
		{
			int fileId = GetFile(FileName);
			DataSet.DecimalPowers.Rows.Add(new object[]
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
					ContentUtil.CommitVariable(document.Variables, Const.Globals.XML_VARIABLE_NAME, dataSet);
				}
			}
		}

		public static void CopyModel(RepositoryDataSet sourceDataSet, Word._Document destDocument)
		{
			RepositoryDataSet destDataSet = new RepositoryDataSet();
			Xml.CopyData(sourceDataSet, destDataSet);
			destDataSet.DecimalPowers.Clear();
			destDataSet.TextPowers.Clear();
			destDataSet.DocumentKeys.Clear();
			destDataSet.WordFiles.Clear();
			destDataSet.AcceptChanges();
			ContentUtil.CommitVariable(destDocument.Variables, Const.Globals.XML_VARIABLE_NAME, destDataSet);
		}

		public void ShowSearchServiceDialog()
		{
			Form dialog = new SelectCategoriesDialog(this);
			dialogs.Add(dialog);
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				SearchService(((SelectCategoriesDialog)dialog).CheckedSubcategories);
				MessageBox.Show("Разметка документа с помощью поисковых функций выполнена!",
							"Разметка документа",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
			}
		}

		public void SearchService(IEnumerable<Subcategory> items)
		{
			foreach (Subcategory item in items)
			{
				if (!string.IsNullOrEmpty(item.Keywords))
				{
					string[] keywords = item.Keywords.Split(separator: new string[] { Environment.NewLine }, options: StringSplitOptions.RemoveEmptyEntries);
					foreach (string keyword in keywords)
					{
						SearchParagraphs( categoryGuid: item.Category.Guid, subcategoryGuid: item.Guid, keyword: keyword, isText: item.IsText);
					}
				}
			}
		}

		private void SearchParagraphs(string categoryGuid, string subcategoryGuid, string keyword, bool isText)
		{
			Pane.ShowButtons = true;
			string[] patterns = GetPatters(keyword);			
			
			if (patterns.Length > 0)
			{
				foreach (Paragraph paragraph in Doc.Content.Paragraphs)
				{
					if (IsCompliance(paragraph: paragraph, pattern: patterns[0]))
					{
						if (isText)
						{
							if (patterns.Length == 1)
							{
								AddTextNote(
									categoryGuid: categoryGuid,
									subcategoryGuid: subcategoryGuid,
									description: "Добавлено с помощью поисковых алгоритмов",
									value: paragraph.Range.Text,
									rating: 0,
									selectionStart: paragraph.Range.Start,
									selectionEnd: paragraph.Range.End
									);
							}
							else
							{
								AddTextParagraph(
									categoryGuid: categoryGuid,
									subcategoryGuid: subcategoryGuid,
									paragraph: paragraph,
									patterns: patterns
									);
							}
						}
						else
						{
							AddDecimalParagraph(
									categoryGuid: categoryGuid,
									subcategoryGuid: subcategoryGuid,
									paragraph: paragraph,
									patterns: patterns
									);
						}
					}
				}
			}
		}

		private static string[] GetPatters(string keyword)
		{
			string[] patterns = keyword.Split(new string[] { "';'" }, StringSplitOptions.RemoveEmptyEntries);
			if (patterns.Length > 0)
			{
				if (patterns[0].Length >0 
					&& patterns[0][0] == 39
					&& patterns[patterns.Length - 1][patterns[patterns.Length - 1].Length-1] == 39)
				{
					patterns[0] = patterns[0].Substring(1);
					patterns[patterns.Length - 1] = patterns[patterns.Length - 1].Substring(0, patterns[patterns.Length - 1].Length-1);
				}
			}
			return patterns;
		}

		private static bool IsCompliance(Paragraph paragraph, string pattern) 
		{
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(paragraph.Range.Text);			
		}

		private void AddTextParagraph(string categoryGuid, string subcategoryGuid, Paragraph paragraph, string[] patterns)
		{
			string content = paragraph.Range.Text;
			int selectionStart = paragraph.Range.Start;
			for (int i = 1; i < patterns.Length; i++)
			{
				Regex regex = new Regex(patterns[i], RegexOptions.IgnoreCase);
				if (regex.IsMatch(content))
				{
					Match match = regex.Match(content);
					content = match.Value;
					selectionStart += match.Index;
				}
				else
				{
					return;
				}
			}
			AddTextNote(
				categoryGuid: categoryGuid, 
				subcategoryGuid: subcategoryGuid,
				description: null,
				value: content,
				rating: 0,
				selectionStart: selectionStart,
				selectionEnd: selectionStart + content.Length);
		}

		private void AddDecimalParagraph(string categoryGuid, string subcategoryGuid, Paragraph paragraph, string[] patterns)
		{
			string content = paragraph.Range.Text;
			for (int i = 1; i < patterns.Length - 1; i++)
			{
				Regex regex = new Regex(patterns[i], RegexOptions.IgnoreCase);
				if (regex.IsMatch(content))
				{
					Match match = regex.Match(content);
					content = match.Value;
				}
				else
				{
					return;
				}
			}
			if (double.TryParse(content, out double result))
				{
				AddDecimalNote(
				categoryGuid: categoryGuid,
				subcategoryGuid: subcategoryGuid,
				description: null,
				value: result,
				rating: 0,
				selectionStart: paragraph.Range.Start,
				selectionEnd: paragraph.Range.End
				);
			}
		}

		public void AiService(IEnumerable<SubcategoriesRow> rows)
		{
			Pane.ShowButtons = true;
			foreach (Paragraph paragraph in Doc.Content.Paragraphs)
			{
				IOrderedEnumerable<KeyValuePair<string, float>> result = Utils.MLModelUtil.PredictAll(paragraph.Range.Text);
				IEnumerable<Subcategory> subcategories = GetSubcategories(result);
				if (subcategories != null)
				{
					foreach (Subcategory subcategory in subcategories)
					{
						AddTextNote(
							categoryGuid: subcategory.Category.Guid,
							subcategoryGuid: subcategory.Guid,
							string.Format("Добавлено с помощью ИИ ({0} %)", (100 * (float)(subcategory.Tag)).ToString("0.000")),
							value: paragraph.Range.Text,
							rating: 0,
							selectionStart: paragraph.Range.Start,
							selectionEnd: paragraph.Range.End);
					}
				}							
			}
		}

		private IEnumerable<Subcategory> GetSubcategories(IOrderedEnumerable<KeyValuePair<string, float>> result)
		{
			if (result == null) return null;
			return result.OrderByDescending(p => p.Value)
				.Where(p => p.Value > Const.Globals.LEVEL_PASSAGE)
				.Select(p => DataSet.GetSubcategory(guid: p.Key, tag: p.Value));			
		}

		private int GetFile(string fileName)
		{
			return GetFile(dataSet: DataSet, fileName: fileName);			
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
