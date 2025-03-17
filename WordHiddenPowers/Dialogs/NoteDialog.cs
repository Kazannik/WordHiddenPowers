using System;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class NoteDialog : Form
	{
		private static readonly Size BUTTON_SIZE = new Size((int)(SystemInformation.MenuHeight * 3.2), (int)(SystemInformation.MenuHeight * 1.2));
		private static readonly Size SMALL_BUTTON_SIZE = new Size((int)(SystemInformation.MenuHeight * 1.2), (int)(SystemInformation.MenuHeight * 1.2));

		private readonly RepositoryDataSet dataSet;
				
		public string SelectionText { get; }

		public int SelectionStart { get; }

		public int SelectionEnd { get; }

		public int Rating
		{
			get
			{
				return ratingBox.Value;
			}
		}

		public Category Category
		{
			get
			{
				return categoriesComboBox.SelectedItem;
			}
		}

		public Subcategory Subcategory
		{
			get
			{
				return subcategoriesComboBox.SelectedItem;
			}
		}

		public string Description
		{
			get
			{
				return descriptionTextBox.Text;
			}
		}

		public bool IsText { get; }

		public NoteDialog()
		{
			InitializeComponent();
			okButton.Enabled = false;
		}

		public NoteDialog(RepositoryDataSet dataSet, Word.Selection selection, bool isText)
		{
			this.dataSet = dataSet;
			InitializeComponent();

			IsText = isText;			
			categoriesComboBox.InitializeSource(this.dataSet, IsText);
			
			SelectionText = selection.Text;
			SelectionStart = selection.Start;
			SelectionEnd = selection.End;	

			okButton.Enabled = false;
		}

		public NoteDialog(RepositoryDataSet dataSet, Note note, bool isText)
		{
			this.dataSet = dataSet;
			InitializeComponent();
			
			IsText = isText;
			categoriesComboBox.InitializeSource(this.dataSet, IsText);
			
			SelectionText = note.WordSelectionText;
			SelectionStart = note.WordSelectionStart;
			SelectionEnd = note.WordSelectionEnd;

			ratingBox.Value = note.Rating;
			descriptionTextBox.Text = note.Description;

			okButton.Enabled = false;

			categoriesComboBox.SelectedItem = categoriesComboBox.GetItem(note.Category.Position.ToString());
			subcategoriesComboBox.SelectedItem = subcategoriesComboBox.GetItem(note.Subcategory.Position.ToString());
		}

		private void CategoriesComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			subcategoriesComboBox.InitializeSource(dataSet, categoriesComboBox.SelectedItem, IsText);
			okButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
		}

		private void SubcategoriesComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			okButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
			wizardButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
		}

		private void WizardButton_Click(object sender, System.EventArgs e)
		{
			PatternsWizardDialog dialog = new PatternsWizardDialog(subcategory: Subcategory, text: SelectionText);			
			if (Utils.ShowDialogUtil.ShowDialog(dialog) == DialogResult.OK)
			{
				Subcategory.Keywords = string.Join(Environment.NewLine, dialog.Keywords);			
			}
		}

		private void Dialog_Resize(object sender, System.EventArgs e)
		{
			ControlsResize();
		}

		protected virtual void ControlsResize()
		{
			okButton.Size = BUTTON_SIZE;
			cancelButton.Size = BUTTON_SIZE;
			
			categoriesComboBox.Location = new Point(12, 12);
			categoriesComboBox.Size = new Size(ClientSize.Width - 24, BUTTON_SIZE.Height);
			categoriesComboBox.BringToFront();

			subcategoriesComboBox.Location = new Point(12, categoriesComboBox.Top + categoriesComboBox.Height + 12);
			subcategoriesComboBox.Size = new Size(ClientSize.Width - 24, BUTTON_SIZE.Height);
			subcategoriesComboBox.BringToFront();

			cancelButton.Location = new Point(ClientSize.Width - cancelButton.Width - 12, ClientSize.Height - cancelButton.Height - 12);
			okButton.Location = new Point(cancelButton.Left - okButton.Width - 12, cancelButton.Top);
			wizardButton.Location = new Point(12, cancelButton.Top);
			wizardButton.Size = SMALL_BUTTON_SIZE;

			ratingBox.Location = new Point(12 + wizardButton.Left + wizardButton.Width, cancelButton.Top);
			descriptionTextBox.Location = new Point(12, ClientSize.Height / 2);
			descriptionTextBox.Size = new Size(ClientSize.Width - 24, ClientSize.Height - descriptionTextBox.Top - cancelButton.Height - 24);
		}
				
		protected int ControlTop { get => subcategoriesComboBox.Top + subcategoriesComboBox.Height + 12; }
		protected int ControlHeight { get => descriptionTextBox.Top - ControlTop - 12; }
		protected int MinHeight { get => ControlTop + descriptionTextBox.Height + 200; }

		private void CreateNoteDialog_Load(object sender, EventArgs e)
		{
			ControlsResize();
			//wizardButton.Image = WordUtil.GetImageMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);
		}
	}
}
