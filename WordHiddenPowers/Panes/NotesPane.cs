using ControlLibrary.Controls.ListControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordHiddenPowers.Controls.ListControls;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Properties;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Utils;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using ListItem = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItem;
using ListItemNote = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItemNote;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
	[DesignerCategory("UserControl")]
	public class NotesPane : WordHiddenPowersPane
	{
		private IContainer components;
		private SplitContainer notesSplitContainer;
		private Label titleLabel;
		private TextBox descriptionTextBox;
		private Label descriptionLabel;
		private DateTimePicker dateTimePicker;
		private Label dateLabel;
		private NotesListBox noteListBox;
		private ContextMenuStrip noteContextMenu;
		private ToolStripMenuItem mnuNoteEdit;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem mnuNoteRemove;
		private ToolStripMenuItem mnuNoteOpen;
		private ComboBox captionComboBox;

		public NotesPane() : base()
		{
			InitializeComponent();
		}

		public NotesPane(Document document) : base(document)
		{
			InitializeComponent();

			mnuNoteRemove.Image = WordDocument.GetImageMso("Delete", 16, 16);
			noteListBox.DataSet = Document.CurrentDataSet;

			InitializeVariables();

			noteListBox.ItemMouseDown += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_ItemMouseDown);
			noteListBox.ItemMouseClick += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_NoteClick);
			noteListBox.ItemMouseDoubleClick += new EventHandler<ItemMouseEventArgs<ListItem, ListItemNote>>(NoteListBox_NoteDoubleClick);
			noteListBox.ItemDeleted += new EventHandler<EventArgs>(NoteListBox_ItemDeleted);

			Document.CurrentDataSet.DocumentKeys.DocumentKeysRowChanged += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_RowChanged);
			Document.CurrentDataSet.DocumentKeys.DocumentKeysRowDeleted += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_RowChanged);
			Document.CurrentDataSet.DocumentKeys.TableCleared += new DataTableClearEventHandler(DocumentKeys_TableCleared);
		}

		private void NoteListBox_ItemDeleted(object sender, EventArgs e)
		{
			Document.CommitVariables();
		}

		protected override void OnPropertiesChanged(EventArgs e)
		{
			noteListBox.ReadData();
			base.OnPropertiesChanged(e);
		}
		
		private void NoteOpen_Click(object sender, EventArgs e)
		{
			NoteOpen(noteContextMenu.Tag as ListItem);
		}

		private void NoteOpen()
		{
			if (noteListBox.SelectedItem is ListItem)
			{
				ListItem item = noteListBox.SelectedItem as ListItem;
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
			base.OnPropertiesChanged(new EventArgs());
		}

		private void DocumentKeys_TableCleared(object sender, DataTableClearEventArgs e)
		{
			captionComboBox.BeginUpdate();
			captionComboBox.Items.Clear();
			captionComboBox.EndUpdate();
			base.OnPropertiesChanged(new EventArgs());
		}

		public string Caption =>  captionComboBox.Text; 

		public DateTime Date =>  dateTimePicker.Value;

		public string Description => descriptionTextBox.Text;

		public bool ShowButtons
		{
			get => noteListBox.ShowButtons;
			set => noteListBox.ShowButtons = value; 
		}

		private void NotesPane_Resize(object sender, EventArgs e)
		{
			notesSplitContainer.Location = new Point(0, 0);
			notesSplitContainer.Size = this.Size;

			PanelResize();
		}

		private void PanelResize()
		{
			titleLabel.Location = new Point(0, 0);

			captionComboBox.Location = new Point(0, titleLabel.Height);
			captionComboBox.Width = notesSplitContainer.Panel1.Width;

			dateLabel.Location = new Point(0, captionComboBox.Top + captionComboBox.Height + 4);
			dateTimePicker.Location = new Point(dateLabel.Width + 8, captionComboBox.Top + captionComboBox.Height + 4);

			descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);

			descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
			descriptionTextBox.Width = notesSplitContainer.Panel1.Width;
			descriptionTextBox.Height = notesSplitContainer.SplitterDistance - descriptionTextBox.Top;

			noteListBox.Location = new Point(0, 0);
			noteListBox.Width = notesSplitContainer.Panel2.Width;
			noteListBox.Height = notesSplitContainer.Panel2.Height;
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

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.notesSplitContainer = new System.Windows.Forms.SplitContainer();
			this.captionComboBox = new System.Windows.Forms.ComboBox();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.dateLabel = new System.Windows.Forms.Label();
			this.titleLabel = new System.Windows.Forms.Label();
			this.noteListBox = new WordHiddenPowers.Controls.ListControls.NotesListBox();
			this.noteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuNoteOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNoteEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuNoteRemove = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.notesSplitContainer)).BeginInit();
			this.notesSplitContainer.Panel1.SuspendLayout();
			this.notesSplitContainer.Panel2.SuspendLayout();
			this.notesSplitContainer.SuspendLayout();
			this.noteContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// notesSplitContainer
			// 
			this.notesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notesSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.notesSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.notesSplitContainer.Name = "notesSplitContainer";
			this.notesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// notesSplitContainer.Panel1
			// 
			this.notesSplitContainer.Panel1.Controls.Add(this.captionComboBox);
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionTextBox);
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionLabel);
			this.notesSplitContainer.Panel1.Controls.Add(this.dateTimePicker);
			this.notesSplitContainer.Panel1.Controls.Add(this.dateLabel);
			this.notesSplitContainer.Panel1.Controls.Add(this.titleLabel);
			this.notesSplitContainer.Panel1.Resize += new System.EventHandler(this.NotesPane_Resize);
			this.notesSplitContainer.Panel1MinSize = 140;
			// 
			// notesSplitContainer.Panel2
			// 
			this.notesSplitContainer.Panel2.Controls.Add(this.noteListBox);
			this.notesSplitContainer.Panel2.Resize += new System.EventHandler(this.NotesPane_Resize);
			this.notesSplitContainer.Size = new System.Drawing.Size(366, 362);
			this.notesSplitContainer.SplitterDistance = 140;
			this.notesSplitContainer.TabIndex = 2;
			this.notesSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.NotesSplitContainer_SplitterMoved);
			// 
			// captionComboBox
			// 
			this.captionComboBox.FormattingEnabled = true;
			this.captionComboBox.Location = new System.Drawing.Point(120, 6);
			this.captionComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.captionComboBox.Name = "captionComboBox";
			this.captionComboBox.Size = new System.Drawing.Size(185, 30);
			this.captionComboBox.TabIndex = 1;
			this.captionComboBox.SelectedIndexChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			this.captionComboBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Location = new System.Drawing.Point(0, 90);
			this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(366, 59);
			this.descriptionTextBox.TabIndex = 5;
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.AutoSize = true;
			this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.descriptionLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.descriptionLabel.Location = new System.Drawing.Point(9, 90);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(149, 20);
			this.descriptionLabel.TabIndex = 4;
			this.descriptionLabel.Text = "Дополнительно:";
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Location = new System.Drawing.Point(135, 49);
			this.dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(206, 28);
			this.dateTimePicker.TabIndex = 3;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new System.Drawing.Point(9, 54);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(57, 22);
			this.dateLabel.TabIndex = 2;
			this.dateLabel.Text = "Дата:";
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.titleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.titleLabel.Location = new System.Drawing.Point(9, 11);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(104, 20);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "Заголовок:";
			// 
			// noteListBox
			// 
			this.noteListBox.DataSet = null;
			this.noteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noteListBox.Location = new System.Drawing.Point(0, 0);
			this.noteListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.noteListBox.Name = "noteListBox";
			this.noteListBox.ShowButtons = false;
			this.noteListBox.Size = new System.Drawing.Size(366, 218);
			this.noteListBox.TabIndex = 6;
			this.noteListBox.ItemContentChanged += new  EventHandler<ItemEventArgs<ListItem>> (this.NotesPane_PropertiesChanged);
			// 
			// noteContextMenu
			// 
			this.noteContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.noteContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNoteOpen,
            this.mnuNoteEdit,
            this.toolStripMenuItem1,
            this.mnuNoteRemove});
			this.noteContextMenu.Name = "noteContextMenu";
			this.noteContextMenu.Size = new System.Drawing.Size(277, 100);
			// 
			// mnuNoteOpen
			// 
			this.mnuNoteOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.mnuNoteOpen.Name = "mnuNoteOpen";
			this.mnuNoteOpen.Size = new System.Drawing.Size(276, 30);
			this.mnuNoteOpen.Text = "Открыть";
			this.mnuNoteOpen.Click += new System.EventHandler(this.NoteOpen_Click);
			// 
			// mnuNoteEdit
			// 
			this.mnuNoteEdit.Name = "mnuNoteEdit";
			this.mnuNoteEdit.Size = new System.Drawing.Size(276, 30);
			this.mnuNoteEdit.Text = "Редактировать запись...";
			this.mnuNoteEdit.Click += new System.EventHandler(this.NoteEdit_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 6);
			// 
			// mnuNoteRemove
			// 
			this.mnuNoteRemove.Name = "mnuNoteRemove";
			this.mnuNoteRemove.Size = new System.Drawing.Size(276, 30);
			this.mnuNoteRemove.Text = "Удалить запись";
			this.mnuNoteRemove.Click += new System.EventHandler(this.NoteRemove_Click);
			// 
			// NotesPane
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.notesSplitContainer);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.Name = "NotesPane";
			this.Size = new System.Drawing.Size(366, 362);
			this.Load += new System.EventHandler(this.NotesPane_Load);
			this.Resize += new System.EventHandler(this.NotesPane_Resize);
			this.notesSplitContainer.Panel1.ResumeLayout(false);
			this.notesSplitContainer.Panel1.PerformLayout();
			this.notesSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.notesSplitContainer)).EndInit();
			this.notesSplitContainer.ResumeLayout(false);
			this.noteContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		
		private void NotesPane_PropertiesChanged(object sender, EventArgs e)
		{
			base.OnPropertiesChanged(new EventArgs());
		}
		
		protected override void Dispose(bool disposing)
		{
			Settings.Default.NotesPaneSplitterDistance = notesSplitContainer.SplitterDistance;
			Settings.Default.Save();
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
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
