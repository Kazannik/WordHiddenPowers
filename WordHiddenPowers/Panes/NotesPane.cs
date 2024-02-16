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
        private SplitContainer splitContainer1;
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


        private void Panel1_Resize(object sender, EventArgs e)
        {
            SplitterPanel panel = (SplitterPanel)sender;

            titleLabel.Location = new Point(0, 0);
            captionComboBox.Location = new Point(0, titleLabel.Height);
            captionComboBox.Width = panel.Width;
            dateLabel.Location = new Point(0, captionComboBox.Top + captionComboBox.Height + 4);
            dateTimePicker.Location = new Point(dateLabel.Width + 8, captionComboBox.Top + captionComboBox.Height + 4);
            descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);
            descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
            descriptionTextBox.Width = panel.Width;
            descriptionTextBox.Height = splitContainer1.SplitterDistance - descriptionTextBox.Top;
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

                string caption = HiddenPowerDocument.GetVariableValue(Document.Doc.Variables, Const.Globals.CAPTION_VARIABLE_NAME);
                if (captionComboBox.Text != caption)
                    captionComboBox.Text = caption;

                string strDate = HiddenPowerDocument.GetVariableValue(Document.Doc.Variables, Const.Globals.DATE_VARIABLE_NAME);
                DateTime date = string.IsNullOrWhiteSpace(strDate) ? DateTime.Today: DateTime.Parse(strDate);
                if (dateTimePicker.Value != date)
                    dateTimePicker.Value = date; 

                string description = HiddenPowerDocument.GetVariableValue(Document.Doc.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (descriptionTextBox.Text != description)
                    descriptionTextBox.Text = description;
            }
        }

        

                     

        public bool DataSetRefresh()
        {
            Word.Variable content = HiddenPowerDocument.GetVariable(Document.Doc.Variables, Const.Globals.XML_VARIABLE_NAME);
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.noteContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.captionComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.dateLabel);
            this.splitContainer1.Panel1.Controls.Add(this.titleLabel);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.Panel1_Resize);
            this.splitContainer1.Panel1MinSize = 140;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.noteListBox);
            this.splitContainer1.Size = new System.Drawing.Size(244, 262);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // captionComboBox
            // 
            this.captionComboBox.FormattingEnabled = true;
            this.captionComboBox.Location = new System.Drawing.Point(3, 15);
            this.captionComboBox.Name = "captionComboBox";
            this.captionComboBox.Size = new System.Drawing.Size(238, 21);
            this.captionComboBox.TabIndex = 1;
            this.captionComboBox.SelectedIndexChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
            this.captionComboBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(0, 18);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(245, 97);
            this.descriptionTextBox.TabIndex = 5;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(6, 65);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(90, 13);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(42, 39);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(115, 20);
            this.dateTimePicker.TabIndex = 3;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(6, 39);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(36, 13);
            this.dateLabel.TabIndex = 2;
            this.dateLabel.Text = "Дата:";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(42, 6);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(64, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Заголовок:";
            // 
            // noteListBox
            // 
            this.noteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.noteListBox.ItemHeight = 80;
            this.noteListBox.Location = new System.Drawing.Point(0, 0);
            this.noteListBox.Margin = new System.Windows.Forms.Padding(2);
            this.noteListBox.Name = "noteListBox";
            this.noteListBox.DataSet = null;
            this.noteListBox.Size = new System.Drawing.Size(244, 119);
            this.noteListBox.TabIndex = 6;
            this.noteListBox.NoteMouseClick += new System.EventHandler<WordHiddenPowers.Controls.NoteListBox.NoteListMouseEventArgs>(this.noteListBox_NoteClick);
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
            this.noteContextMenu.Size = new System.Drawing.Size(204, 76);
            // 
            // mnuNoteOpen
            // 
            this.mnuNoteOpen.Name = "mnuNoteOpen";
            this.mnuNoteOpen.Size = new System.Drawing.Size(203, 22);
            this.mnuNoteOpen.Text = "Открыть";
            this.mnuNoteOpen.Click += new System.EventHandler(this.NoteOpen_Click);
            // 
            // mnuNoteEdit
            // 
            this.mnuNoteEdit.Name = "mnuNoteEdit";
            this.mnuNoteEdit.Size = new System.Drawing.Size(203, 22);
            this.mnuNoteEdit.Text = "Редактировать запись...";
            this.mnuNoteEdit.Click += new System.EventHandler(this.NoteEdit_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 6);
            // 
            // mnuNoteRemove
            // 
            this.mnuNoteRemove.Name = "mnuNoteRemove";
            this.mnuNoteRemove.Size = new System.Drawing.Size(203, 22);
            this.mnuNoteRemove.Text = "Удалить запись";
            this.mnuNoteRemove.Click += new System.EventHandler(this.NoteRemove_Click);
            // 
            // NotesPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "NotesPane";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
