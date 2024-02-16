using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

namespace WordHiddenPowers
{
    public partial class WordHiddenPowersRibbon
    {
        const string dialogFilters = "XML Schema File (.xsd)|*.xsd";

        
        private void newPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Documents.ActiveDocument !=null)
            {
                Globals.ThisAddIn.Documents.ActiveDocument.NewData();            }
        }

        private void openPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Documents.ActiveDocument != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = dialogFilters;
                if (Utils.ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
                {
                    Globals.ThisAddIn.Documents.ActiveDocument.LoadData(dialog.FileName);                    
                }
            }
        }

        private void savePowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = dialogFilters;
            if (Utils.ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
            {
                Globals.ThisAddIn.Documents.ActiveDocument.CommitVariables();
                Globals.ThisAddIn.Documents.ActiveDocument.SaveData(dialog.FileName);               
            }
        }
                

        private void deletePowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Documents.ActiveDocument != null)
            {
                if (Globals.ThisAddIn.Documents.ActiveDocument.VariablesExists())
                {
                    if ( MessageBox.Show("Удалить дополнительные данные из документа?", "Удаление скрытых данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)== DialogResult.Yes)
                    {
                        Globals.ThisAddIn.Documents.ActiveDocument.DeleteVariables();
                    }
                } else
                {
                    MessageBox.Show("Дополнительные данные отсуствуют!", "Удаление скрытых данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void paneVisibleButton_Click(object sender, RibbonControlEventArgs e)
        {
            //RibbonToggleButton button = (RibbonToggleButton)sender;
            //if (button.Id == Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Id &&
            //    Globals.ThisAddIn.Panes.Count > 0)
            //{
            //    Globals.ThisAddIn.Panes.ActivePane.Visible = Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked;
            //}           
        }

        private void createTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowCreateTableDialog();                           
        }

        private void editTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowEditTableDialog();
        }

        private void editCategoriesButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowEditCategoriesDialog();
        }

        private void editDocumentKeysButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowDocumentKeysDialog();
        }

        private void fieldAddButton_Click(object sender, RibbonControlEventArgs e)
        {
            //WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            //WordHiddenPowers.Utils.WordUtil.AddField(pane.Document, Globals.ThisAddIn.Application.Selection , 1);
        }

        private void fieldsUpdateButton_Click(object sender, RibbonControlEventArgs e)
        {
            //WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            //WordHiddenPowers.Utils.WordUtil.UpdateField(pane.Document, Globals.ThisAddIn.Application.Selection, 1);
        }

        private void analizerImportButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ImportDataFromWordDocuments();
        }

        private void analizerTableViewerButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowTableViewerDialog();
        }

        private void analizerDialogButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.ShowAnalizerDialog();
        }


        private enum NoteType
        {
            Text = 0,
            Decimal = 1
        }

        private NoteType lastNoteType = NoteType.Text;

        private void AddTextNoteButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);

            lastNoteType = NoteType.Text;
            AddLastNoteTypeButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
            AddLastNoteTypeButton.Label = Const.Content.TEXT_NOTE_LABEL;
            AddLastNoteTypeButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
            AddLastNoteTypeButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
            AddLastNoteTypeButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;
        }

        private void AddDecimalNoteButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Documents.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);

            lastNoteType = NoteType.Decimal;
            AddLastNoteTypeButton.Description = Const.Content.DECIMAL_NOTE_DESCRIPTION;
            AddLastNoteTypeButton.Label = Const.Content.DECIMAL_NOTE_LABEL;
            AddLastNoteTypeButton.OfficeImageId = Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID;
            AddLastNoteTypeButton.ScreenTip = Const.Content.DECIMAL_NOTE_SCREEN_TIP;
            AddLastNoteTypeButton.SuperTip = Const.Content.DECIMAL_NOTE_SUPER_TIP;
        }

        private void AddLastNoteTypeButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (lastNoteType == NoteType.Text)
                Globals.ThisAddIn.Documents.ActiveDocument.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
            else
                Globals.ThisAddIn.Documents.ActiveDocument.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
        }        
    }
}
