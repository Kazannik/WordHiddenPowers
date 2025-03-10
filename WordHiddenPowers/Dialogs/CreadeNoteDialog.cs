using System;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Repositories.Notes;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class CreateNoteDialog : Form
	{
		private static readonly Size SMALL_BUTTON_SIZE = new Size(SystemInformation.MenuHeight, SystemInformation.MenuHeight);

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

		public CreateNoteDialog()
		{
			InitializeComponent();

			wizardButton.Image = WordUtil.GetImageMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			ControlResize();

			okButton.Enabled = false;
		}

		public CreateNoteDialog(RepositoryDataSet dataSet, Word.Selection selection, bool isText)
		{
			this.dataSet = dataSet;
			
			IsText = isText;			
			SelectionText = selection.Text;
			SelectionStart = selection.Start;
			SelectionEnd = selection.End;
			
			InitializeComponent();

			wizardButton.Image = WordUtil.GetImageMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			ControlResize();
			categoriesComboBox.InitializeSource(this.dataSet, isText);
			okButton.Enabled = false;
		}

		public CreateNoteDialog(RepositoryDataSet dataSet, Note note, bool isText)
		{
			this.dataSet = dataSet;
			IsText = isText;

			SelectionText = note.WordSelectionText;
			SelectionStart = note.WordSelectionStart;
			SelectionEnd = note.WordSelectionEnd;

			InitializeComponent();

			wizardButton.Image = WordUtil.GetImageMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			ControlResize();

			ratingBox.Value = note.Rating;
			descriptionTextBox.Text = note.Description;

			categoriesComboBox.InitializeSource(this.dataSet, isText);
			okButton.Enabled = false;

			categoriesComboBox.SelectedItem = categoriesComboBox.GetItem(note.Category.Position.ToString());
			subcategoriesComboBox.SelectedItem = subcategoriesComboBox.GetItem(note.Subcategory.Position.ToString());
		}

		private void CategoriesComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			subcategoriesComboBox.InitializeSource(dataSet, (Category)categoriesComboBox.SelectedItem, IsText);
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
			ControlResize();
		}

		private void ControlResize()
		{
			categoriesComboBox.Location = new Point(12, 12);
			categoriesComboBox.Width = ClientSize.Width - 24;
			subcategoriesComboBox.Location = new Point(12, categoriesComboBox.Top + categoriesComboBox.Height + 12);
			subcategoriesComboBox.Width = ClientSize.Width - 24;
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
	}
}
