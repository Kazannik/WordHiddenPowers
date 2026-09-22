using ControlLibrary.Structures;
using System;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Categories;
using WordHiddenPowers.Repository.Notes;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class NoteDialog : Form
	{
		private static readonly Size SMALL_BUTTON_SIZE = new Size((int)(SystemInformation.MenuHeight * 1.2), (int)(SystemInformation.MenuHeight * 1.2));

		private readonly DocumentDataSet dataSet;

		public string SelectionText { get; }

		public int SelectionStart { get; }

		public int SelectionEnd { get; }

		public int Rating => ratingControl.Rating.Value;

		public Category Category => categoriesComboBox.SelectedItem;

		public Subcategory Subcategory => subcategoriesComboBox.SelectedItem;

		public string Description => descriptionTextBox.Text;

		public bool IsText { get; }

		public NoteDialog()
		{
			InitializeComponent();
			Visible = false;
			okButton.Enabled = false;
		}

		public NoteDialog(DocumentDataSet dataSet, string selectionText, int selectionStart, int selectionEnd, bool isText) : this()
		{
			IsText = isText;
			this.dataSet = dataSet;

			categoriesComboBox.InitializeSource(this.dataSet, IsText);

			SelectionText = selectionText;
			SelectionStart = selectionStart;
			SelectionEnd = selectionEnd;
		}


		public NoteDialog(DocumentDataSet dataSet, Word.Selection selection, bool isText)
			: this(dataSet: dataSet, selectionText: selection.Text, selectionStart: selection.Start, selectionEnd: selection.End, isText: isText)
		{ }

		public NoteDialog(DocumentDataSet dataSet, Note note, bool isText)
			: this(dataSet: dataSet, selectionText: note.WordSelectionText, selectionStart: note.WordSelectionStart, selectionEnd: note.WordSelectionEnd, isText: isText)
		{
			ratingControl.Rating = (Rating)note.Rating;
			descriptionTextBox.Text = note.Description;

			categoriesComboBox.SelectedItem = categoriesComboBox.GetItem(note.Category.Position.ToString());
			subcategoriesComboBox.SelectedItem = subcategoriesComboBox.GetItem(note.Subcategory.Position.ToString());
		}

		private void CategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			subcategoriesComboBox.InitializeSource(dataSet, categoriesComboBox.SelectedItem, IsText);
			if (subcategoriesComboBox.Items.Count == 1)
				subcategoriesComboBox.SelectedIndex = 0;

			okButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
		}

		private void SubcategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			okButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
			wizardButton.Enabled = categoriesComboBox.SelectedIndex >= 0 && subcategoriesComboBox.SelectedIndex >= 0;
		}

		private void WizardButton_Click(object sender, EventArgs e)
		{
			PatternsWizardDialog dialog = new PatternsWizardDialog(subcategory: Subcategory, text: SelectionText);
			if (Utils.Dialogs.ShowDialog(dialog) == DialogResult.OK)
			{
				Subcategory.Keywords = string.Join(Environment.NewLine, dialog.Keywords);
			}
		}

		private void Dialog_Resize(object sender, EventArgs e)
		{
			ControlsResize();
		}

		protected virtual void ControlsResize()
		{
			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			wizardButton.Size = SMALL_BUTTON_SIZE;
		}

		protected int ControlTop => subcategoriesComboBox.Top + subcategoriesComboBox.Height + 12;
		protected int ControlHeight => descriptionTextBox.Top - ControlTop - 12;
		protected int MinHeight => ControlTop + descriptionTextBox.Height + 200;

		private void CreateNoteDialog_Load(object sender, EventArgs e)
		{
			ControlsResize();
			//wizardButton.Image = WordUtil.GetImageMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);
			Visible = true;
		}

		private void RatingControl1_RatingChanged(object sender, ControlLibrary.Controls.RatingControls.RatingEventArgs e)
		{
			if (ratingControl.Rating.Value < 0)
				ratingControl.StarsColor1 = Const.Globals.COLOR_NEGATIVE_STAR_ICON;
			if (ratingControl.Rating.Value > 0)
				ratingControl.StarsColor1 = Const.Globals.COLOR_STAR_ICON;
		}
	}
}
