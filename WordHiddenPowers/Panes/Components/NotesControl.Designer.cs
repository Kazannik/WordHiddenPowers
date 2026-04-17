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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotesControl));
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
			resources.ApplyResources(this.notesSplitContainer, "notesSplitContainer");
			this.notesSplitContainer.Name = "notesSplitContainer";
			// 
			// notesSplitContainer.Panel1
			// 
			this.notesSplitContainer.Panel1.Controls.Add(this.descriptionTextBox);
			this.notesSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.NotesSplitContainer_SplitterMoved);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// captionComboBox
			// 
			resources.ApplyResources(this.captionComboBox, "captionComboBox");
			this.captionComboBox.FormattingEnabled = true;
			this.captionComboBox.Name = "captionComboBox";
			this.captionComboBox.SelectedIndexChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			this.captionComboBox.TextChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// descriptionLabel
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.descriptionLabel, 2);
			resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
			this.descriptionLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.descriptionLabel.Name = "descriptionLabel";
			// 
			// dateTimePicker
			// 
			resources.ApplyResources(this.dateTimePicker, "dateTimePicker");
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.NotesPane_PropertiesChanged);
			// 
			// dateLabel
			// 
			resources.ApplyResources(this.dateLabel, "dateLabel");
			this.dateLabel.Name = "dateLabel";
			// 
			// titleLabel
			// 
			resources.ApplyResources(this.titleLabel, "titleLabel");
			this.titleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.titleLabel.Name = "titleLabel";
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
			resources.ApplyResources(this.noteContextMenu, "noteContextMenu");
			// 
			// mnuNoteOpen
			// 
			resources.ApplyResources(this.mnuNoteOpen, "mnuNoteOpen");
			this.mnuNoteOpen.Name = "mnuNoteOpen";
			this.mnuNoteOpen.Click += new System.EventHandler(this.NoteOpen_Click);
			// 
			// mnuNoteEdit
			// 
			this.mnuNoteEdit.Name = "mnuNoteEdit";
			resources.ApplyResources(this.mnuNoteEdit, "mnuNoteEdit");
			this.mnuNoteEdit.Click += new System.EventHandler(this.NoteEdit_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			// 
			// mnuNoteRemove
			// 
			this.mnuNoteRemove.Name = "mnuNoteRemove";
			resources.ApplyResources(this.mnuNoteRemove, "mnuNoteRemove");
			this.mnuNoteRemove.Click += new System.EventHandler(this.NoteRemove_Click);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.captionComboBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.notesSplitContainer, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.titleLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dateTimePicker, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.dateLabel, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// NotesControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "NotesControl";
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
