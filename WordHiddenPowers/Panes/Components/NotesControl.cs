using ControlLibrary.Controls.ListControls;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WordHiddenPowers.Controls.ListControls;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Properties;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Utils;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using ListItem = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItem;
using ListItemNote = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItemNote;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes.Components
{
	public partial class NotesControl : UserControl
	{
		public readonly Document Document;

		public event EventHandler<EventArgs> PropertiesChanged;

		protected virtual void OnPropertiesChanged(EventArgs e)
		{
			notesListBox.ReadData();
			PropertiesChanged?.Invoke(this, e);
		}

		public NotesControl()
		{
			InitializeComponent();
		}

		private SplitContainer notesSplitContainer;
		private Label titleLabel;
		private TextBox descriptionTextBox;
		private Label descriptionLabel;
		private DateTimePicker dateTimePicker;
		private Label dateLabel;
		private NotesListBox notesListBox;
		private ContextMenuStrip noteContextMenu;
		private ToolStripMenuItem mnuNoteEdit;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem mnuNoteRemove;
		private ToolStripMenuItem mnuNoteOpen;
		private ComboBox captionComboBox;
		private TableLayoutPanel tableLayoutPanel1;

		public NotesControl(Document document)
		{
			Document = document;
			
			InitializeComponent();

			notesListBox = new NotesListBox();
			notesListBox.DataSet = Document.CurrentDataSet;
			notesListBox.Dock = DockStyle.Fill;
			notesListBox.FormattingEnabled = true;
			notesListBox.Location = new System.Drawing.Point(0, 0);
			notesListBox.Name = "notesListBox";
			notesListBox.ShowButtons = false;
			notesListBox.Size = new System.Drawing.Size(360, 191);
			notesListBox.TabIndex = 0;
			notesSplitContainer.Panel2.Controls.Add(notesListBox);

			mnuNoteRemove.Image = WordDocument.GetImageMso("Delete", 16, 16);


			InitializeVariables();

			notesListBox.ItemMouseDown += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_ItemMouseDown);
			notesListBox.ItemMouseClick += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_NoteClick);
			notesListBox.ItemMouseDoubleClick += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_NoteDoubleClick);
			notesListBox.ItemDeleted += new EventHandler<EventArgs>(NoteListBox_ItemDeleted);
			notesListBox.ItemContentChanged += new EventHandler<ItemEventArgs<ListItem>>(NotesListBox_ItemContentChanged);

			Document.CurrentDataSet.DocumentKeys.DocumentKeysRowChanged += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_RowChanged);
			Document.CurrentDataSet.DocumentKeys.DocumentKeysRowDeleted += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_RowChanged);
			Document.CurrentDataSet.DocumentKeys.TableCleared += new DataTableClearEventHandler(DocumentKeys_TableCleared);
		}

		private void NotesListBox_ItemContentChanged(object sender, ItemEventArgs<ListItem> e)
		{
			OnPropertiesChanged(new EventArgs()); 
		}

		private void NoteListBox_ItemDeleted(object sender, EventArgs e)
		{
			Document.CommitVariables();
		}
		
		private void NoteOpen_Click(object sender, EventArgs e)
		{
			NoteOpen(noteContextMenu.Tag as ListItem);
		}

		private void NoteOpen()
		{
			if (notesListBox.SelectedItem is ListItem)
			{
				ListItem item = notesListBox.SelectedItem as ListItem;
				NoteOpen(item);
			}
		}

		private void SetSelectedText(ListItem item)
		{
			Word.Range range = Document.Doc.Range(item.Note.WordSelectionStart, item.Note.WordSelectionEnd);
			item.Note.SetWordSelectionText(range.Text);
		}

		private void NoteOpen(ListItem item)
		{
			Word.Range range = Document.Doc.Range(item.Note.WordSelectionStart, item.Note.WordSelectionEnd);
			range.Select();
		}

		private void NoteEdit_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				SetSelectedText(item);
				if (item.Note.IsText)
				{
					TextNoteDialog dialog = new TextNoteDialog(Document.CurrentDataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						Document.CurrentDataSet.TextPowers.Set(item.Note.Id,
							dialog.Category.Guid,
							dialog.Subcategory.Guid,
							dialog.Description,
							dialog.Value as string,
							dialog.Rating,
							dialog.SelectionStart,
							dialog.SelectionEnd,
							false);
						if (item.Note.Subcategory.Keywords != dialog.Subcategory.Keywords)
						{
							Document.CurrentDataSet.Write(dialog.Subcategory);
						}
						Document.CommitVariables();
					}
				}
				else
				{
					DecimalNoteDialog dialog = new DecimalNoteDialog(Document.CurrentDataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						Document.CurrentDataSet.DecimalPowers.Set(
							item.Note.Id,
							dialog.Category.Guid,
							dialog.Subcategory.Guid,
							dialog.Description,
							(double)dialog.Value,
							dialog.Rating,
							dialog.SelectionStart,
							dialog.SelectionEnd,
							false);
						Document.CommitVariables();
					}
				}
			}
		}

		private void NoteRemove_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is ListItem)
			{
				ListItem item = noteContextMenu.Tag as ListItem;
				Document.CurrentDataSet.Remove(item.Note);
				Document.CommitVariables();
			}
		}

		private void NoteListBox_NoteClick(object sender, ItemMouseEventArgs<ListItem, ListItemNote> e)
		{
			if (e.Button == MouseButtons.Right)
			{
				noteContextMenu.Show(Cursor.Position);
			}
		}

		private void NoteListBox_ItemMouseDown(object sender, ItemMouseEventArgs<ListItem, ListItemNote> e)
		{
			if (e.Button == MouseButtons.Right)
			{
				noteContextMenu.Tag = e.Item;
				noteContextMenu.Show(Cursor.Position);
			}
		}

		private void NoteListBox_NoteDoubleClick(object sender, ItemMouseEventArgs<ListItem, ListItemNote> e)
		{
			if (e.Button == MouseButtons.Left)
			{
				NoteOpen();
			}
		}

		private void DocumentKeys_RowChanged(object sender, RepositoryDataSet.DocumentKeysRowChangeEvent e)
		{
			captionComboBox.BeginUpdate();
			captionComboBox.Items.Clear();
			foreach (DataRow row in Document.CurrentDataSet.DocumentKeys.Rows)
			{
				captionComboBox.Items.Add(row["Caption"]);
			}
			captionComboBox.EndUpdate();
			OnPropertiesChanged(new EventArgs());
		}

		private void DocumentKeys_TableCleared(object sender, DataTableClearEventArgs e)
		{
			captionComboBox.BeginUpdate();
			captionComboBox.Items.Clear();
			captionComboBox.EndUpdate();
			OnPropertiesChanged(new EventArgs());
		}

		public string Caption => captionComboBox.Text;

		public DateTime Date => dateTimePicker.Value;

		public string Description => descriptionTextBox.Text;

		public bool ShowButtons
		{
			get => notesListBox.ShowButtons;
			set => notesListBox.ShowButtons = value;
		}
		
		public void InitializeVariables()
		{
			if (Document.Doc.Variables.Count > 0)
			{
				captionComboBox.BeginUpdate();
				captionComboBox.Items.Clear();
				foreach (DataRow row in Document.CurrentDataSet.DocumentKeys.Rows)
				{
					captionComboBox.Items.Add(row["Caption"]);
				}
				captionComboBox.EndUpdate();

				string caption = Content.GetVariableValueOrDefault(Document.Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				if (captionComboBox.Text != caption)
					captionComboBox.Text = caption;

				string strDate = Content.GetVariableValueOrDefault(Document.Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				DateTime date = string.IsNullOrWhiteSpace(strDate) ? DateTime.Today : DateTime.Parse(strDate);
				if (dateTimePicker.Value != date)
					dateTimePicker.Value = date;

				string description = Content.GetVariableValueOrDefault(Document.Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				if (descriptionTextBox.Text != description)
					descriptionTextBox.Text = description;
			}
		}

		public bool CurrentDataSetRefresh()
		{
			Word.Variable content = Content.GetVariable(Document.Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME);
			if (content != null)
			{
				foreach (DataTable table in Document.CurrentDataSet.Tables)
				{
					table.Clear();
				}
				StringReader reader = new StringReader(content.Value);
				Document.CurrentDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
				reader.Close();
				Document.CurrentDataSet.AcceptChanges();
				return true;
			}
			else
				return false;
		}
		
		private void NotesPane_PropertiesChanged(object sender, EventArgs e)
		{
			OnPropertiesChanged(new EventArgs());
		}
		
		private void NotesSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Settings.Default.NotesPaneSplitterDistance = notesSplitContainer.SplitterDistance;
			Settings.Default.Save();
			Settings.Default.Upgrade();
		}

		private void NotesPane_Load(object sender, EventArgs e)
		{
			Settings.Default.Upgrade();
			try
			{
				notesSplitContainer.SplitterDistance = Settings.Default.NotesPaneSplitterDistance;
			}
			catch (Exception) { }
		}
	}
}