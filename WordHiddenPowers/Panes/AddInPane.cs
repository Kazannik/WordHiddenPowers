using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Controls;
using WordHiddenPowers.Documents;


namespace WordHiddenPowers.Panes
{
	[DesignerCategory("UserControl")]
	public class AddInPane : WordHiddenPowersPane
	{
		private readonly IContainer components;

		private TabControl mainTabControl;
		private TabPage llmTabPage;
		private TabPage notesTabPage;
		private Components.LLMControl llmControl;
		private Components.NotesControl notesControl;

		public Components.NotesControl NotesControl => notesControl;

		private bool notesControlVisible;


		public Documents.DocumentCollection.ChartMessageMode MessageMode
		{
			get => llmControl.MessageMode;
			set => llmControl.MessageMode = value;
		}

		public bool NotesControlVisible
		{
			get
			{
				SetNotesControlVisible(notesControlVisible);
				return notesControlVisible;
			}

			set
			{
				notesControlVisible = value;
				SetNotesControlVisible(value);
			}
		}

		private void SetNotesControlVisible(bool visible)
		{
			if (visible)
			{
				if (notesTabPage == null)
					CreateNotesControls();
			}
			else
			{
				if (notesTabPage != null)
					RemoveNotesControls();
			}
		}

		public AddInPane() : base()
		{
			InitializeComponent();
		}

		public AddInPane(Document document, int hwnd) : base(document, hwnd)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			mainTabControl = new TabControl();
			llmTabPage = new TabPage();
			llmControl = new Components.LLMControl(Document);
			mainTabControl.SuspendLayout();
			llmTabPage.SuspendLayout();
			SuspendLayout();
			// 
			// mainTabControl
			// 
			mainTabControl.Alignment = TabAlignment.Bottom;
			mainTabControl.Controls.Add(llmTabPage);
			mainTabControl.Dock = DockStyle.Fill;
			mainTabControl.Location = new Point(0, 0);
			mainTabControl.Name = "tabControl1";
			mainTabControl.SelectedIndex = 0;
			mainTabControl.Size = new Size(366, 427);
			mainTabControl.TabIndex = 1;
			// 
			// tabPage1
			// 
			llmTabPage.Controls.Add(llmControl);
			llmTabPage.Location = new Point(4, 4);
			llmTabPage.Name = "llmTabPage";
			llmTabPage.Padding = new Padding(3);
			llmTabPage.Size = new Size(358, 396);
			llmTabPage.TabIndex = 0;
			llmTabPage.Text = "AI помощник";
			llmTabPage.UseVisualStyleBackColor = true;
			// 
			// llmControl
			// 
			llmControl.Dock = DockStyle.Fill;
			llmControl.Location = new Point(3, 3);
			llmControl.Name = "llmControl";
			llmControl.Size = new Size(352, 390);
			llmControl.TabIndex = 0;
			// 
			// AddInPane
			// 
			AutoScaleDimensions = new SizeF(9F, 18F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(mainTabControl);
			Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
			Margin = new Padding(4, 2, 4, 2);
			Name = "AddInPane";
			Size = new Size(366, 427);
			mainTabControl.ResumeLayout(false);
			llmTabPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void CreateNotesControls()
		{
			notesTabPage = new TabPage();
			notesControl = new Components.NotesControl(Document);
			mainTabControl.Controls.Add(notesTabPage);

			mainTabControl.SuspendLayout();
			notesTabPage.SuspendLayout();
			SuspendLayout();
			// 
			// notesTabPage
			// 
			notesTabPage.Controls.Add(notesControl);
			notesTabPage.Location = new Point(4, 4);
			notesTabPage.Name = "notesTabPage";
			notesTabPage.Padding = new Padding(3);
			notesTabPage.Size = new Size(358, 396);
			notesTabPage.TabIndex = 1;
			notesTabPage.Text = "Разметка документа";
			notesTabPage.UseVisualStyleBackColor = true;
			// 
			// notesControl
			// 
			notesControl.Dock = DockStyle.Fill;
			notesControl.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
			notesControl.Location = new Point(3, 3);
			notesControl.Margin = new Padding(4, 2, 4, 2);
			notesControl.Name = "notesControl";
			notesControl.ShowButtons = false;
			notesControl.Size = new Size(352, 390);
			notesControl.TabIndex = 0;
			notesControl.PropertiesChanged += new EventHandler<EventArgs>(NotesControl_PropertiesChanged);

			mainTabControl.ResumeLayout(false);
			notesTabPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void RemoveNotesControls()
		{
			mainTabControl.SuspendLayout();
			notesTabPage.SuspendLayout();
			SuspendLayout();

			notesTabPage.Controls.Remove(notesControl);
			notesControl.Dispose();
			notesControl = null;

			mainTabControl.Controls.Remove(notesTabPage);
			notesTabPage.Dispose();
			notesTabPage = null;

			mainTabControl.ResumeLayout(false);
			ResumeLayout(false);
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
	}
}