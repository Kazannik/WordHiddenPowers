using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WordHiddenPowers.Repository.Categories;
using WordHiddenPowers.Utils;

namespace WordHiddenPowers.Dialogs
{
    public partial class PatternsWizardDialog: Form
    {
        public Subcategory Subcategory { get; }

        public IEnumerable<string> Keywords => patternsWizardBox1.GetKeywords();

        public PatternsWizardDialog(Subcategory subcategory, string text)
        {
            Subcategory = subcategory;
            InitializeComponent();
			
            Icon = WordDocument.GetIconMso("GanttChartWizard", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			okButton.Enabled = false;
			patternsWizardBox1.Text = text;
            patternsWizardBox1.IsDecimal = subcategory.IsDecimal;
            patternsWizardBox1.SetKeywords(subcategory.Keywords);
        }

		private void PatternsWizardBox_IsCorrectChanged(object sender, EventArgs e)
		{
            okButton.Enabled = patternsWizardBox1.IsCorrect;
		}
	}
}
