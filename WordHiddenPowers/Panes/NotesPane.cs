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
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Utils;
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

			InitializeVariables();

			noteListBox.DataSet = Document.DataSet;

			Document.DataSet.DocumentKeys.DocumentKeysRowChanged += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_RowChanged);
		}

		protected override void OnPropertiesChanged(EventArgs e)
		{
			noteListBox.ReadData();
			base.OnPropertiesChanged(e);
		}

		private void NoteOpen_Click(object sender, EventArgs e)
		{
			NoteOpen(noteContextMenu.Tag as NotesListItem);
		}

		private void NoteOpen()
		{
			if (noteListBox.SelectedItem is NotesListItem)
			{
				NotesListItem item = noteListBox.SelectedItem as NotesListItem;
				NoteOpen(item);
			}
		}


		private void NoteOpen(NotesListItem item)
		{			
				Word.Range range = Document.Doc.Range(item.Note.WordSelectionStart, item.Note.WordSelectionEnd);
				range.Select();
		}

		private void NoteEdit_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is NotesListItem)
			{
				NotesListItem item = noteContextMenu.Tag as NotesListItem;
				if (item.Note.IsText)
				{
					TextNoteDialog dialog = new TextNoteDialog(Document.DataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						Document.DataSet.TextPowers.Set(item.Note.Id,
							dialog.Category.Id,
							dialog.Subcategory.Id,
							dialog.Description,
							dialog.Value,
							dialog.Reiting,
							dialog.SelectionStart,
							dialog.SelectionEnd);
						Document.CommitVariables();
					}
				}
				else
				{
					DecimalNoteDialog dialog = new DecimalNoteDialog(Document.DataSet, item.Note);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						Document.DataSet.DecimalPowers.Set(item.Note.Id,
							dialog.Category.Id,
							dialog.Subcategory.Id,
							dialog.Description,
							dialog.Value,
							dialog.Reiting,
							dialog.SelectionStart,
							dialog.SelectionEnd);
						Document.CommitVariables();
					}
				}
			}
		}

		private void NoteRemove_Click(object sender, EventArgs e)
		{
			if (noteContextMenu.Tag is NotesListItem)
			{
				NotesListItem item = noteContextMenu.Tag as NotesListItem;
				if (item.Note.IsText)
					Document.DataSet.TextPowers.Remove(item.Note);
				else
					Document.DataSet.DecimalPowers.Remove(item.Note);
				Document.CommitVariables();
			}
		}

		private void NoteListBox_ApplyButtonNoteClick(object sender, ItemMouseEventArgs<NotesListItem, BottomBarNote> e)
		{
			e.SubItem.IsAppled = true;
		}

		private void NoteListBox_CancelButtonNoteClick(object sender, ItemMouseEventArgs<NotesListItem, BottomBarNote> e)
		{
			if (e.Item.Note.IsText)
				Document.DataSet.TextPowers.Remove(e.Item.Note);
			else
				Document.DataSet.DecimalPowers.Remove(e.Item.Note);
			Document.CommitVariables();
		}

		private void NoteListBox_NoteClick(object sender, ItemMouseEventArgs<NotesListItem, NotesListItemNote> e)
		{
			if (e.Button == MouseButtons.Right)
			{
				noteContextMenu.Show(Cursor.Position);
			}
		}

		private void NoteListBox_ItemMouseDown(object sender, ItemMouseEventArgs<NotesListItem, NotesListItemNote> e)
		{
			if (e.Button == MouseButtons.Right)
			{
				noteContextMenu.Tag = e.Item;
				noteContextMenu.Show(Cursor.Position);
			}
		}

		private void NoteListBox_NoteDoubleClick(object sender, ItemMouseEventArgs<NotesListItem, NotesListItemNote> e)
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
			foreach (DataRow row in Document.DataSet.DocumentKeys.Rows)
			{
				captionComboBox.Items.Add(row["Caption"]);
			}
			captionComboBox.EndUpdate();
			base.OnPropertiesChanged(new EventArgs());
		}

		public string Caption
		{
			get { return captionComboBox.Text; }
		}

		public DateTime Date
		{
			get { return dateTimePicker.Value; }
		}

		public string Description
		{
			get { return descriptionTextBox.Text; }
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
				if (DataSetRefresh())
				{
					captionComboBox.BeginUpdate();
					captionComboBox.Items.Clear();
					foreach (DataRow row in Document.DataSet.DocumentKeys.Rows)
					{
						captionComboBox.Items.Add(row["Caption"]);
					}
					captionComboBox.EndUpdate();
				}

				string caption = Content.GetVariableValue(Document.Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
				if (captionComboBox.Text != caption)
					captionComboBox.Text = caption;

				string strDate = Content.GetVariableValue(Document.Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
				DateTime date = string.IsNullOrWhiteSpace(strDate) ? DateTime.Today : DateTime.Parse(strDate);
				if (dateTimePicker.Value != date)
					dateTimePicker.Value = date;

				string description = Content.GetVariableValue(Document.Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
				if (descriptionTextBox.Text != description)
					descriptionTextBox.Text = description;
			}
		}

		public bool DataSetRefresh()
		{
			Word.Variable content = Content.GetVariable(Document.Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
			if (content != null)
			{
				foreach (DataTable table in Document.DataSet.Tables)
				{
					table.Clear();
				}
				StringReader reader = new StringReader(content.Value);
				Document.DataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
				reader.Close();
				Document.DataSet.AcceptChanges();
				return true;
			}
			else
				return false;
		}


		// add the button to the context menus that you need to support
		//AddButton(applicationObject.CommandBars["Text"]);
		//AddButton(applicationObject.CommandBars["Table Text"]);
		//AddButton(applicationObject.CommandBars["Table Cells"]);


		private void InitializeComponent()
		{
			this.components = new Container();
			this.notesSplitContainer = new SplitContainer();
			this.captionComboBox = new ComboBox();
			this.descriptionTextBox = new TextBox();
			this.descriptionLabel = new Label();
			this.dateTimePicker = new DateTimePicker();
			this.dateLabel = new Label();
			this.titleLabel = new Label();
			this.noteListBox = new NotesListBox();
			this.noteContextMenu = new ContextMenuStrip(this.components);
			this.mnuNoteOpen = new ToolStripMenuItem();
			this.mnuNoteEdit = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.mnuNoteRemove = new ToolStripMenuItem();
			((ISupportInitialize)(this.notesSplitContainer)).BeginInit();
			this.notesSplitContainer.Panel1.SuspendLayout();
			this.notesSplitContainer.Panel2.SuspendLayout();
			this.notesSplitContainer.SuspendLayout();
			this.noteContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// notesSplitContainer
			// 
			this.notesSplitContainer.DataBindings.Add(new Binding("SplitterDistance", Properties.Settings.Default, "NotesPaneSplitterDistance", true, DataSourceUpdateMode.OnPropertyChanged));
			this.notesSplitContainer.Dock = DockStyle.Fill;
			this.notesSplitContainer.Location = new Point(0, 0);
			this.notesSplitContainer.Margin = new Padding(3, 2, 3, 2);
			this.notesSplitContainer.Name = "notesSplitContainer";
			this.notesSplitContainer.Orientation = Orientation.Horizontal;
			// 
			// notesSplitContainer.Panel1
			// 
			this.notesSplitContainer.Panel1.Controls.Add(this.captionComboBox);
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionTextBox);
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionLabel);
			this.notesSplitContainer.Panel1.Controls.Add(this.dateTimePicker);
			this.notesSplitContainer.Panel1.Controls.Add(this.dateLabel);
			this.notesSplitContainer.Panel1.Controls.Add(this.titleLabel);
			this.notesSplitContainer.Panel1.Resize += new EventHandler(this.NotesPane_Resize);
			this.notesSplitContainer.Panel1MinSize = 140;
			// 
			// notesSplitContainer.Panel2
			// 
			this.notesSplitContainer.Panel2.Controls.Add(this.noteListBox);
			this.notesSplitContainer.Panel2.Resize += new EventHandler(this.NotesPane_Resize);
			this.notesSplitContainer.Size = new Size(366, 362);
			this.notesSplitContainer.SplitterDistance = Properties.Settings.Default.NotesPaneSplitterDistance;
			this.notesSplitContainer.TabIndex = 2;
			// 
			// captionComboBox
			// 
			this.captionComboBox.FormattingEnabled = true;
			this.captionComboBox.Location = new Point(120, 6);
			this.captionComboBox.Margin = new Padding(4);
			this.captionComboBox.Name = "captionComboBox";
			this.captionComboBox.Size = new Size(185, 30);
			this.captionComboBox.TabIndex = 1;
			this.captionComboBox.SelectedIndexChanged += new EventHandler(this.NotesPane_PropertiesChanged);
			this.captionComboBox.TextChanged += new EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Location = new Point(0, 90);
			this.descriptionTextBox.Margin = new Padding(3, 2, 3, 2);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new Size(365, 67);
			this.descriptionTextBox.TabIndex = 5;
			this.descriptionTextBox.TextChanged += new EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.AutoSize = true;
			this.descriptionLabel.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			this.descriptionLabel.ForeColor = SystemColors.MenuHighlight;
			this.descriptionLabel.Location = new Point(9, 90);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new Size(149, 20);
			this.descriptionLabel.TabIndex = 4;
			this.descriptionLabel.Text = "Дополнительно:";
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Location = new Point(135, 49);
			this.dateTimePicker.Margin = new Padding(3, 2, 3, 2);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new Size(170, 28);
			this.dateTimePicker.TabIndex = 3;
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new Point(9, 54);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new Size(57, 22);
			this.dateLabel.TabIndex = 2;
			this.dateLabel.Text = "Дата:";
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			this.titleLabel.ForeColor = SystemColors.MenuHighlight;
			this.titleLabel.Location = new Point(9, 11);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new Size(104, 20);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "Заголовок:";
			// 
			// noteListBox
			// 
			this.noteListBox.DataSet = null;
			this.noteListBox.Dock = DockStyle.Fill;
			this.noteListBox.Location = new Point(0, 0);
			this.noteListBox.Margin = new Padding(3, 2, 3, 2);
			this.noteListBox.Name = "noteListBox";
			this.noteListBox.Size = new Size(366, 218);
			this.noteListBox.TabIndex = 6;
			this.noteListBox.ItemMouseDown += new EventHandler<ItemMouseEventArgs<NotesListItem, NotesListItemNote>>(NoteListBox_ItemMouseDown);
			this.noteListBox.ItemMouseClick += new EventHandler<ItemMouseEventArgs<NotesListItem, NotesListItemNote>>(NoteListBox_NoteClick);
			this.noteListBox.ItemMouseDoubleClick += new EventHandler<ItemMouseEventArgs<NotesListItem, NotesListItemNote>>(NoteListBox_NoteDoubleClick);
			this.noteListBox.ItemApplyClick += new EventHandler<ItemMouseEventArgs<NotesListItem, BottomBarNote>>(NoteListBox_ApplyButtonNoteClick);
			this.noteListBox.ItemCancelClick += new EventHandler<ItemMouseEventArgs<NotesListItem, BottomBarNote>>(NoteListBox_CancelButtonNoteClick);
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
			this.noteContextMenu.Size = new System.Drawing.Size(277, 106);
			// 
			// mnuNoteOpen
			// 
			this.mnuNoteOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.mnuNoteOpen.Name = "mnuNoteOpen";
			this.mnuNoteOpen.Size = new System.Drawing.Size(276, 32);
			this.mnuNoteOpen.Text = "Открыть";
			this.mnuNoteOpen.Click += new System.EventHandler(this.NoteOpen_Click);
			// 
			// mnuNoteEdit
			// 
			this.mnuNoteEdit.Name = "mnuNoteEdit";
			this.mnuNoteEdit.Size = new System.Drawing.Size(276, 32);
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
			this.mnuNoteRemove.Size = new System.Drawing.Size(276, 32);
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			Properties.Settings.Default.NotesPaneSplitterDistance = notesSplitContainer.SplitterDistance;
			Properties.Settings.Default.Save();

			base.Dispose(disposing);
		}		
	}
}
