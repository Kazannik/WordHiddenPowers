using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Panes;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Data;
using WordHiddenPowers.Utils;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Documents
{
	public class Document : IDisposable
	{
		private readonly DocumentCollection parent;

		private RepositoryDataSet dataSet;

		private CustomTaskPane pane;

		private RepositoryDataSet importDataSet;

		public CustomTaskPane CustomPane
		{
			get
			{
				if (pane == null)
				{
					pane = Globals.ThisAddIn.CustomTaskPanes.Add(new NotesPane(this), Const.Panes.PANE_TITLE);
					pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
					pane.Width = 400;
					pane.Visible = false;
					pane.VisibleChanged += new EventHandler(Pane_VisibleChanged);
					NotesPane note = pane.Control as NotesPane;
					note.PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);
				}
				return pane;
			}
		}

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
				Content.CommitVariable(
					array: Doc.Variables, 
					variableName: Const.Globals.XML_VARIABLE_NAME, 
					dataSet: DataSet);
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
				return Content.GetCaption(Doc);
			}
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
			get
			{
				return Content.GetDate(Doc);
			}
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
			get
			{
				return Content.GetDescription(Doc);
			}
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
			get
			{
				return Content.GetTable(Doc);
			}
			set
			{
				Content.CommitVariable(
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
				if (dataSet == null)
				{
					dataSet = Xml.GetDataSet(Doc: Doc);
				}
				return dataSet;
			}
		}

		public RepositoryDataSet ImportDataSet
		{
			get
			{
				if (importDataSet == null)
				{
					importDataSet = Xml.GetDataSet(
						Doc: Doc, 
						variableName: Const.Globals.XML_IMPORT_VARIABLE_NAME);
				}
				return importDataSet;
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
		}

		void Pane_VisibleChanged(object sender, EventArgs e)
		{
			CustomTaskPane pane = (CustomTaskPane)sender;
			parent.paneVisibleButton.Checked = pane.Visible;
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
			DataSet.Categories.Clear();
			DataSet.Subcategories.Clear();
			DataSet.DecimalPowers.Clear();
			DataSet.TextPowers.Clear();
			DataSet.WordFiles.Clear();

			CommitVariables();
		}

		public void LoadData(string fileName)
		{
			try
			{
				DataSet.RowsHeaders.Clear();
				DataSet.ColumnsHeaders.Clear();
				DataSet.Categories.Clear();
				DataSet.Subcategories.Clear();
				DataSet.DocumentKeys.Clear();

				DataSet.ReadXml(fileName, XmlReadMode.IgnoreSchema);

				DataSet.DecimalPowers.Clear();
				DataSet.TextPowers.Clear();

				CommitVariables();
			}
			catch (Exception ex)
			{
				ShowDialogUtil.ShowErrorDialog(ex.Message);
			}
		}

		public void SaveData(string fileName)
		{
			Xml.SaveData(Globals.ThisAddIn.Documents.ActiveDocument.DataSet, fileName);
		}

		public void CommitVariables()
		{
			Content.CommitVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME, Caption);
			Content.CommitVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME, Date.ToShortDateString());
			Content.CommitVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME, Description);
			Content.CommitVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME, Table.ToString());

			if (DataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME, DataSet);
			}

			if (ImportDataSet.HasChanges())
			{
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME, ImportDataSet);
			}
		}

		public bool VariablesExists()
		{
			if (Doc.Variables.Count > 0)
			{
				Word.Variable caption = Content.GetVariable(Doc.Variables,
					Const.Globals.CAPTION_VARIABLE_NAME);
				if (caption != null)
				{
					return true;
				}

				Word.Variable date = Content.GetVariable(Doc.Variables,
					Const.Globals.DATE_VARIABLE_NAME);
				if (date != null)
				{
					return true;
				}

				Word.Variable description = Content.GetVariable(Doc.Variables,
					Const.Globals.DESCRIPTION_VARIABLE_NAME);
				if (description != null)
				{
					return true;
				}

				Word.Variable table = Content.GetVariable(Doc.Variables,
					Const.Globals.TABLE_VARIABLE_NAME);
				if (table != null)
				{
					return true;
				}

				Word.Variable content = Content.GetVariable(Doc.Variables,
					Const.Globals.XML_VARIABLE_NAME);
				if (content != null)
				{
					return true;
				}

				Word.Variable import = Content.GetVariable(Doc.Variables,
					Const.Globals.XML_IMPORT_VARIABLE_NAME);
				if (import != null)
				{
					return true;
				}
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
				Content.DeleteVariable(Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.TABLE_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
				Content.DeleteVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME);
			}
		}


		protected IList<Form> dialogs = null;

		public void ShowDocumentKeysDialog()
		{
			Form dialog = new DocumentKeysDialog(DataSet);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
			//OnPropertiesChanged(new EventArgs());
		}

		public void ShowEditCategoriesDialog()
		{
			Form dialog = new CategoriesEditorDialog(this);
			dialogs.Add(dialog);
			if (ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				Pane.DataSetRefresh();
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
			if (ShowDialogUtil.ShowDialogO(dialog) == DialogResult.OK)
			{
				importDataSet = FileSystem.ImportFiles(dialog.SelectedPath);
				Content.CommitVariable(Doc.Variables, Const.Globals.XML_IMPORT_VARIABLE_NAME, ImportDataSet);
				Doc.Saved = false;
			}
		}

		public void ShowTableViewerDialog()
		{
			TableViewerDialog dialog = new TableViewerDialog(ImportDataSet);
			dialogs.Add(dialog);
			ShowDialogUtil.ShowDialog(dialog);
		}

		public void ShowAnalizerDialog()
		{
			AnalyzerDialog dialog = new AnalyzerDialog(ImportDataSet);
			dialogs.Add(dialog);
			ShowDialogUtil.Show(dialog);
		}

		public void AddTextNote(Word.Selection selection)
		{
			TextNoteDialog dialog = new TextNoteDialog(DataSet, selection);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				int fileId = GetFile(FileName);

				DataSet.TextPowers.Rows.Add(new object[]
				{ null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd, fileId });

				CommitVariables();
			}
		}

		public void AddDecimalNote(Word.Selection selection)
		{
			DecimalNoteDialog dialog = new DecimalNoteDialog(DataSet, selection);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				int fileId = GetFile(FileName);

				DataSet.DecimalPowers.Rows.Add(new object[]
				{ null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd, fileId });

				CommitVariables();
			}
		}


		private int GetFile(string fileName)
		{
			if (DataSet.WordFiles.Exists(fileName: fileName))
			{
				return DataSet.WordFiles.Get(fileName: fileName).Id;
			}
			else
			{
				return DataSet.WordFiles.Add(fileName: fileName, caption: string.Empty, description: string.Empty, date: DateTime.Now).Id;
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
			if (pane != null)
			{
				pane.Dispose();
				pane = null;
			}
		}
	}
}
