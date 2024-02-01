using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Models;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
    [DesignerCategory("UserControl")]
    class NotesPane : WordHiddenPowersPane
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
        private ComboBox titleComboBox;

        public NotesPane(): base()
        {
            PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);
            InitializeComponent();
        }

        public NotesPane(Word.Document Doc) : base(Doc)
        {
            PropertiesChanged += new EventHandler<EventArgs>(NotesPane_PropertiesChanged);

            InitializeComponent();
            
            InitializeVariables();

            PowersDataSet.DocumentKeys.DocumentKeysRowChanged += new RepositoryDataSet.DocumentKeysRowChangeEventHandler(DocumentKeys_DocumentKeysRowChanged);

            noteListBox.PowersDataSet = PowersDataSet;
        }

        private void NotesPane_PropertiesChanged(object sender, EventArgs e)
        {
            noteListBox.ReadData();
        }

        private void NoteOpen_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                Word.Range range = Document.Range(note.WordSelectionStart, note.WordSelectionEnd);
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
                    TextNoteDialog dialog = new TextNoteDialog(PowersDataSet, note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.TextPowers.Set(note.Id,
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
                    DecimalNoteDialog dialog = new DecimalNoteDialog(PowersDataSet, note);
                    dialogs.Add(dialog);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PowersDataSet.DecimalPowers.Set(note.Id,
                            dialog.Category.Id,
                            dialog.Subcategory.Id,
                            dialog.Description,
                            dialog.Value,
                            dialog.Reiting,
                            dialog.SelectionStart,
                            dialog.SelectionEnd);
                    }
                }
                base.CommitVariables();
            }
        }

        private void NoteRemove_Click(object sender, EventArgs e)
        {
            Note note = noteListBox.SelectedItem as Note;
            if (note != null)
            {
                if (note.IsText)
                    PowersDataSet.TextPowers.Remove(note);
                else
                    PowersDataSet.DecimalPowers.Remove(note);

                CommitVariables();
            }
        }

        private void noteListBox_NoteClick(object sender, Controls.NoteListBox.NoteListMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                noteContextMenu.Show(Cursor.Position);
            }
        }

        


        private void DocumentKeys_DocumentKeysRowChanged(object sender, RepositoryDataSet.DocumentKeysRowChangeEvent e)
        {
            titleComboBox.BeginUpdate();
            titleComboBox.Items.Clear();
            foreach (DataRow row in PowersDataSet.DocumentKeys.Rows)
            {
                titleComboBox.Items.Add(row["Caption"]);
            }
            titleComboBox.EndUpdate();
        }

        public string Title
        {
            get { return titleComboBox.Text; }
        }

        public DateTime Date
        {
            get { return dateTimePicker.Value; }
        }

        public string Description
        {
            get { return descriptionTextBox.Text; }
        }


        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            SplitterPanel panel = (SplitterPanel)sender;

            titleLabel.Location = new Point(0, 0);
            titleComboBox.Location = new Point(0, titleLabel.Height);
            titleComboBox.Width = panel.Width;
            dateLabel.Location = new Point(0, titleComboBox.Top + titleComboBox.Height + 4);
            dateTimePicker.Location = new Point(dateLabel.Width + 8, titleComboBox.Top + titleComboBox.Height + 4);
            descriptionLabel.Location = new Point(0, dateLabel.Top + dateLabel.Height + 4);
            descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
            descriptionTextBox.Width = panel.Width;
            descriptionTextBox.Height = splitContainer1.SplitterDistance - descriptionTextBox.Top;
        }

        public void InitializeVariables()
        {
            if (Document.Variables.Count > 0)
            {
                DataSetRefresh();

                titleComboBox.Text = GetVariable(Const.Globals.TITLE_VARIABLE_NAME);
                string date = GetVariable(Const.Globals.DATE_VARIABLE_NAME);
                dateTimePicker.Value = string.IsNullOrWhiteSpace(date) ? DateTime.Today: DateTime.Parse(date);
                descriptionTextBox.Text = GetVariable(Const.Globals.DESCRIPTION_VARIABLE_NAME);
            }
        }

        private string GetVariable(string name)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable != null)
                return variable.Value;
            else
                return string.Empty;
        }

        public new void DeleteVariables()
        {
            titleComboBox.Items.Clear();
            titleComboBox.Text = string.Empty;
            dateTimePicker.Text = string.Empty;
            descriptionTextBox.Text = string.Empty;

            base.DeleteVariables();
        }

        

       

        public void DataSetRefresh()
        {
            Word.Variable categories = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
            if (categories != null)
            {
                StringReader reader = new StringReader(categories.Value);

                foreach (DataTable table in PowersDataSet.Tables)
                {
                    table.Clear();
                }

                PowersDataSet.ReadXml(reader, XmlReadMode.IgnoreSchema);
                reader.Close();
            }
        }


        public void AddTextNote(Word.Selection selection)
        {
            TextNoteDialog dialog = new TextNoteDialog(PowersDataSet, selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.TextPowers.Rows.Add(new object[]
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                base.CommitVariables();
            }
        }

        public void AddDecimalNote(Word.Selection selection)
        {
            DecimalNoteDialog dialog = new DecimalNoteDialog(PowersDataSet, selection);
            dialogs.Add(dialog);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PowersDataSet.DecimalPowers.Rows.Add(new object[]
                { null, dialog.Category.Id, dialog.Subcategory.Id, dialog.Description, dialog.Value, dialog.Reiting, dialog.SelectionStart, dialog.SelectionEnd });

                base.CommitVariables();
            }
        }



        // add the button to the context menus that you need to support
        //AddButton(applicationObject.CommandBars["Text"]);
        //AddButton(applicationObject.CommandBars["Table Text"]);
        //AddButton(applicationObject.CommandBars["Table Cells"]);




        private new void CommitVariables()
        {
            CommitVariables(Const.Globals.TITLE_VARIABLE_NAME, titleComboBox.Text);
            CommitVariables(Const.Globals.DATE_VARIABLE_NAME, dateTimePicker.Value.ToShortDateString());
            CommitVariables(Const.Globals.DESCRIPTION_VARIABLE_NAME, descriptionTextBox.Text);

            base.CommitVariables();
        }       
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.titleComboBox = new System.Windows.Forms.ComboBox();
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
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.titleComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.dateLabel);
            this.splitContainer1.Panel1.Controls.Add(this.titleLabel);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            this.splitContainer1.Panel1MinSize = 140;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.noteListBox);
            this.splitContainer1.Size = new System.Drawing.Size(325, 322);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 2;
            // 
            // titleComboBox
            // 
            this.titleComboBox.FormattingEnabled = true;
            this.titleComboBox.Location = new System.Drawing.Point(4, 18);
            this.titleComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.titleComboBox.Name = "titleComboBox";
            this.titleComboBox.Size = new System.Drawing.Size(316, 24);
            this.titleComboBox.TabIndex = 1;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(0, 22);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(325, 118);
            this.descriptionTextBox.TabIndex = 5;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(8, 80);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(117, 17);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(56, 48);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(152, 22);
            this.dateTimePicker.TabIndex = 3;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(8, 48);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(46, 17);
            this.dateLabel.TabIndex = 2;
            this.dateLabel.Text = "Дата:";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(56, 7);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(80, 17);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Заголовок:";
            // 
            // noteListBox
            // 
            this.noteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.noteListBox.ItemHeight = 40;
            this.noteListBox.Location = new System.Drawing.Point(0, 0);
            this.noteListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.noteListBox.Name = "noteListBox";
            this.noteListBox.PowersDataSet = null;
            this.noteListBox.Size = new System.Drawing.Size(325, 178);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

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

            base.Dispose(disposing);
        }

    }
}
