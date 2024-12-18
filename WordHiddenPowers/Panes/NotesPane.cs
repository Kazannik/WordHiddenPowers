using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
        private Controls.NoteListBox noteListBox;
        private ContextMenuStrip noteContextMenu;
        private ToolStripMenuItem mnuNoteEdit;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuNoteRemove;
        private ToolStripMenuItem mnuNoteOpen;
        private ComboBox captionComboBox;

        public NotesPane(): base()
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
            NoteOpen();
        }

        private void NoteOpen()
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                Word.Range range = Document.Doc.Range(note.WordSelectionStart, note.WordSelectionEnd);
                range.Select();
            }
        }

        private void NoteEdit_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                if (note.IsText)
                {
                    TextNoteDialog dialog = new TextNoteDialog(Document.DataSet, note);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Document.DataSet.TextPowers.Set(note.Id,
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
                    DecimalNoteDialog dialog = new DecimalNoteDialog(Document.DataSet, note);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Document.DataSet.DecimalPowers.Set(note.Id,
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
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                if (note.IsText)
                    Document.DataSet.TextPowers.Remove(note);
                else
                    Document.DataSet.DecimalPowers.Remove(note);
            }
        }

        private void noteListBox_NoteClick(object sender, Controls.NoteListBox.NoteListMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                noteContextMenu.Show(Cursor.Position);
            }
        }

        private void noteListBox_NoteMouseDoubleClick(object sender, Controls.NoteListBox.NoteListMouseEventArgs e)
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
                DateTime date = string.IsNullOrWhiteSpace(strDate) ? DateTime.Today: DateTime.Parse(strDate);
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
            this.components = new System.ComponentModel.Container();
            this.notesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.captionComboBox = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.noteListBox = new WordHiddenPowers.Controls.NoteListBox();
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
            this.notesSplitContainer.SplitterDistance = 193;
            this.notesSplitContainer.TabIndex = 2;
            // 
            // captionComboBox
            // 
            this.captionComboBox.FormattingEnabled = true;
            this.captionComboBox.Location = new System.Drawing.Point(4, 20);
            this.captionComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.captionComboBox.Name = "captionComboBox";
            this.captionComboBox.Size = new System.Drawing.Size(355, 26);
            this.captionComboBox.TabIndex = 1;
            this.captionComboBox.SelectedIndexChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
            this.captionComboBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(0, 25);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(365, 132);
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
            this.descriptionLabel.Size = new System.Drawing.Size(131, 17);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(63, 54);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(170, 24);
            this.dateTimePicker.TabIndex = 3;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(9, 54);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(47, 18);
            this.dateLabel.TabIndex = 2;
            this.dateLabel.Text = "Дата:";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.titleLabel.Location = new System.Drawing.Point(63, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(90, 17);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Заголовок:";
            // 
            // noteListBox
            // 
            this.noteListBox.DataSet = null;
            this.noteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.noteListBox.ItemHeight = 80;
            this.noteListBox.Location = new System.Drawing.Point(0, 0);
            this.noteListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.noteListBox.Name = "noteListBox";
            this.noteListBox.Size = new System.Drawing.Size(366, 165);
            this.noteListBox.TabIndex = 6;
            this.noteListBox.NoteMouseClick += new System.EventHandler<WordHiddenPowers.Controls.NoteListBox.NoteListMouseEventArgs>(this.noteListBox_NoteClick);
            this.noteListBox.NoteMouseDoubleClick += new System.EventHandler<WordHiddenPowers.Controls.NoteListBox.NoteListMouseEventArgs>(this.noteListBox_NoteMouseDoubleClick);
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
            this.noteContextMenu.Size = new System.Drawing.Size(242, 82);
            // 
            // mnuNoteOpen
            // 
            this.mnuNoteOpen.Name = "mnuNoteOpen";
            this.mnuNoteOpen.Size = new System.Drawing.Size(241, 24);
            this.mnuNoteOpen.Text = "Открыть";
            this.mnuNoteOpen.Click += new System.EventHandler(this.NoteOpen_Click);
            // 
            // mnuNoteEdit
            // 
            this.mnuNoteEdit.Name = "mnuNoteEdit";
            this.mnuNoteEdit.Size = new System.Drawing.Size(241, 24);
            this.mnuNoteEdit.Text = "Редактировать запись...";
            this.mnuNoteEdit.Click += new System.EventHandler(this.NoteEdit_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
            // 
            // mnuNoteRemove
            // 
            this.mnuNoteRemove.Name = "mnuNoteRemove";
            this.mnuNoteRemove.Size = new System.Drawing.Size(241, 24);
            this.mnuNoteRemove.Text = "Удалить запись";
            this.mnuNoteRemove.Click += new System.EventHandler(this.NoteRemove_Click);
            // 
            // NotesPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
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
            
            base.Dispose(disposing);
        }

      
    }
}
