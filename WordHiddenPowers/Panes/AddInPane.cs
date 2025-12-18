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
using WordHiddenPowers.Repository;
using WordHiddenPowers.Utils;
using Content = WordHiddenPowers.Utils.WordDocuments.Content;
using ListItem = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItem;
using ListItemNote = WordHiddenPowers.Controls.ListControls.NotesListControl.ListItemNote;
using Word = Microsoft.Office.Interop.Word;


namespace WordHiddenPowers.Panes
{
    [DesignerCategory("UserControl")]
    public class AddInPane : WordHiddenPowersPane
    {
        private IContainer components;
		private TabControl tabControl1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private Components.NotesControl notesControl1;
		private Components.LLMControl llmControl1;
		
		public Components.NotesControl NotesControl => this.notesControl1;

        public AddInPane() : base()
        {
            InitializeComponent();
        }

        public AddInPane(Document document) : base(document)
        {
            InitializeComponent();
        }
     
        
		        
        private void InitializeComponent()
        {
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.llmControl1 = new WordHiddenPowers.Panes.Components.LLMControl(Document);
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.notesControl1 = new WordHiddenPowers.Panes.Components.NotesControl(Document);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(366, 427);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.llmControl1);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(358, 396);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "AI помощник";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// llmControl1
			// 
			this.llmControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.llmControl1.Location = new System.Drawing.Point(3, 3);
			this.llmControl1.Name = "llmControl1";
			this.llmControl1.Size = new System.Drawing.Size(352, 390);
			this.llmControl1.TabIndex = 0;
			this.llmControl1.Load += new System.EventHandler(this.llmControl1_Load);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.notesControl1);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(358, 396);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Разметка документа";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// notesControl1
			// 
			this.notesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.notesControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.notesControl1.Location = new System.Drawing.Point(3, 3);
			this.notesControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.notesControl1.Name = "notesControl1";
			this.notesControl1.ShowButtons = false;
			this.notesControl1.Size = new System.Drawing.Size(352, 390);
			this.notesControl1.TabIndex = 0;
			// 
			// AddInPane
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.Name = "AddInPane";
			this.Size = new System.Drawing.Size(366, 427);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
        }
				
		protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		private void NotesControl_PropertiesChanged(object sender, EventArgs e)
		{
			OnPropertiesChanged(e);
		}

		private void llmControl1_Load(object sender, EventArgs e)
		{

		}
	}
}