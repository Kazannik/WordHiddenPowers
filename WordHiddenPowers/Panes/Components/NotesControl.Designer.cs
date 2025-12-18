using ControlLibrary.Controls.ListControls;
using System;

namespace WordHiddenPowers.Panes.Components
{
	partial class NotesControl
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
			this.notesSplitContainer = new System.Windows.Forms.SplitContainer();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.captionComboBox = new System.Windows.Forms.ComboBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.dateLabel = new System.Windows.Forms.Label();
			this.titleLabel = new System.Windows.Forms.Label();
			this.noteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuNoteOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNoteEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuNoteRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.notesSplitContainer)).BeginInit();
			this.notesSplitContainer.Panel1.SuspendLayout();
			this.notesSplitContainer.SuspendLayout();
			this.noteContextMenu.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notesSplitContainer
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.notesSplitContainer, 2);
			this.notesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notesSplitContainer.Location = new System.Drawing.Point(3, 84);
			this.notesSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.notesSplitContainer.Name = "notesSplitContainer";
			this.notesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// notesSplitContainer.Panel1
			// 
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionTextBox);
			this.notesSplitContainer.Panel1MinSize = 60;
			this.notesSplitContainer.Size = new System.Drawing.Size(360, 276);
			this.notesSplitContainer.SplitterDistance = 81;
			this.notesSplitContainer.TabIndex = 2;
			this.notesSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.NotesSplitContainer_SplitterMoved);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptionTextBox.Location = new System.Drawing.Point(0, 0);
			this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(360, 81);
			this.descriptionTextBox.TabIndex = 5;
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// captionComboBox
			// 
			this.captionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.captionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.captionComboBox.FormattingEnabled = true;
			this.captionComboBox.Location = new System.Drawing.Point(134, 4);
			this.captionComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.captionComboBox.Name = "captionComboBox";
			this.captionComboBox.Size = new System.Drawing.Size(228, 26);
			this.captionComboBox.TabIndex = 1;
			this.captionComboBox.SelectedIndexChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			this.captionComboBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionLabel
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.descriptionLabel, 2);
			this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.descriptionLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.descriptionLabel.Location = new System.Drawing.Point(3, 56);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(360, 26);
			this.descriptionLabel.TabIndex = 4;
			this.descriptionLabel.Text = "Дополнительно:";
			this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimePicker.Location = new System.Drawing.Point(133, 32);
			this.dateTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(230, 24);
			this.dateTimePicker.TabIndex = 3;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// dateLabel
			// 
			this.dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateLabel.Location = new System.Drawing.Point(3, 30);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(124, 26);
			this.dateLabel.TabIndex = 2;
			this.dateLabel.Text = "Дата:";
			this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// titleLabel
			// 
			this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.titleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.titleLabel.Location = new System.Drawing.Point(3, 0);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(124, 30);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "Заголовок:";
			this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.mnuNoteOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.captionComboBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.notesSplitContainer, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.titleLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dateTimePicker, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.dateLabel, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(366, 362);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// NotesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.Name = "NotesControl";
			this.Size = new System.Drawing.Size(366, 362);
			this.Load += new System.EventHandler(this.NotesPane_Load);
			this.notesSplitContainer.Panel1.ResumeLayout(false);
			this.notesSplitContainer.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.notesSplitContainer)).EndInit();
			this.notesSplitContainer.ResumeLayout(false);
			this.noteContextMenu.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
