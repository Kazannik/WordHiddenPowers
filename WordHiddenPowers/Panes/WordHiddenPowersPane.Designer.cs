using System.Windows.Forms;

namespace WordHiddenPowers.Panes
{
    partial class WordHiddenPowersPane
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
                    if (form !=null)
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                dialogs.Clear();
            }          

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.titleComboBox = new System.Windows.Forms.ComboBox();
            this.documentKeysBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryDataSet = new WordHiddenPowers.Repositoryes.RepositoryDataSet();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.noteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNoteOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNoteEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNoteRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.documentKeysBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.noteListBox = new WordHiddenPowers.Controls.NoteListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentKeysBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryDataSet)).BeginInit();
            this.noteContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentKeysBindingSource1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.titleComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.descriptionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.dateLabel);
            this.splitContainer1.Panel1.Controls.Add(this.titleLabel);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.noteListBox);
            this.splitContainer1.Size = new System.Drawing.Size(244, 262);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // titleComboBox
            // 
            this.titleComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "ghh",
            "juiu"});
            this.titleComboBox.FormattingEnabled = true;
            this.titleComboBox.Location = new System.Drawing.Point(3, 15);
            this.titleComboBox.Name = "titleComboBox";
            this.titleComboBox.Size = new System.Drawing.Size(238, 21);
            this.titleComboBox.TabIndex = 7;
            // 
            // documentKeysBindingSource
            // 
            this.documentKeysBindingSource.DataMember = "DocumentKeys";
            this.documentKeysBindingSource.DataSource = this.repositoryDataSet;
            // 
            // repositoryDataSet
            // 
            this.repositoryDataSet.DataSetName = "RepositoryDataSet";
            this.repositoryDataSet.Locale = new System.Globalization.CultureInfo("ru");
            this.repositoryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(6, 84);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(115, 40);
            this.descriptionTextBox.TabIndex = 6;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(6, 65);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(90, 13);
            this.descriptionLabel.TabIndex = 5;
            this.descriptionLabel.Text = "Дополнительно:";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(42, 39);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(115, 20);
            this.dateTimePicker.TabIndex = 4;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(6, 39);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(36, 13);
            this.dateLabel.TabIndex = 3;
            this.dateLabel.Text = "Дата:";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(42, 6);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(64, 13);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Заголовок:";
            // 
            // noteContextMenu
            // 
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
            // documentKeysBindingSource1
            // 
            this.documentKeysBindingSource1.DataMember = "DocumentKeys";
            this.documentKeysBindingSource1.DataSource = this.repositoryDataSet;
            // 
            // noteListBox
            // 
            this.noteListBox.ContextMenuStrip = this.noteContextMenu;
            this.noteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.noteListBox.ItemHeight = 40;
            this.noteListBox.Location = new System.Drawing.Point(0, 0);
            this.noteListBox.Margin = new System.Windows.Forms.Padding(2);
            this.noteListBox.Name = "noteListBox";
            this.noteListBox.PowersDataSet = null;
            this.noteListBox.Size = new System.Drawing.Size(244, 136);
            this.noteListBox.TabIndex = 0;
            // 
            // WordHiddenPowersPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WordHiddenPowersPane";
            this.Size = new System.Drawing.Size(244, 262);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentKeysBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryDataSet)).EndInit();
            this.noteContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentKeysBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label dateLabel;
        private Controls.NoteListBox noteListBox;
        private System.Windows.Forms.ContextMenuStrip noteContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuNoteEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuNoteRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuNoteOpen;
        private ComboBox titleComboBox;
        private BindingSource documentKeysBindingSource;
        private Repositoryes.RepositoryDataSet repositoryDataSet;
        private BindingSource documentKeysBindingSource1;
    }
}
