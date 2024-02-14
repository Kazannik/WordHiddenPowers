using System;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using System.IO;
using WordHiddenPowers.Panes;

namespace WordHiddenPowers
{
    public partial class WordHiddenPowersRibbon
    {
        const string dialogFilters = "XML Schema File (.xsd)|*.xsd";

        
        private void newPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.ActivePane !=null)
            {
                Globals.ThisAddIn.ActivePane.PowersDataSet.RowsHeaders.Clear();
                Globals.ThisAddIn.ActivePane.PowersDataSet.ColumnsHeaders.Clear();
                Globals.ThisAddIn.ActivePane.PowersDataSet.Categories.Clear();
                Globals.ThisAddIn.ActivePane.PowersDataSet.Subcategories.Clear();
                Globals.ThisAddIn.ActivePane.PowersDataSet.DecimalPowers.Clear();
                Globals.ThisAddIn.ActivePane.PowersDataSet.TextPowers.Clear();

                Globals.ThisAddIn.ActivePane.CommitVariables();
            }
        }

        private void openPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.ActivePane != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = dialogFilters;
                if (Utils.ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
                {
                    try
                    {
                        Globals.ThisAddIn.ActivePane.PowersDataSet.RowsHeaders.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.ColumnsHeaders.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.Categories.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.Subcategories.Clear();

                        Globals.ThisAddIn.ActivePane.PowersDataSet.ReadXml(dialog.FileName, System.Data.XmlReadMode.IgnoreSchema);

                        Globals.ThisAddIn.ActivePane.PowersDataSet.DecimalPowers.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.TextPowers.Clear();

                        Globals.ThisAddIn.ActivePane.CommitVariables();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void savePowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = dialogFilters;
            if (Utils.ShowDialogUtil.ShowDialogObj(dialog) == DialogResult.OK)
            {
                try
                {
                    Globals.ThisAddIn.ActivePane.CommitVariables();

                    string xml = GetXml(Globals.ThisAddIn.ActivePane.PowersDataSet);
                    RepositoryDataSet powersDataSet = new RepositoryDataSet();
                    SetXml(powersDataSet, xml);
                    powersDataSet.DecimalPowers.Clear();
                    powersDataSet.TextPowers.Clear();
                    powersDataSet.WriteXml(dialog.FileName, System.Data.XmlWriteMode.WriteSchema);
                }
                catch (Exception)
                {

                }
            }
        }


        private string GetXml(RepositoryDataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            dataSet.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
            writer.Close();
            return builder.ToString();
        }

        private void SetXml(RepositoryDataSet dataSet, string xml)
        {
            StringReader reader = new StringReader(xml);
            dataSet.ReadXml(reader, System.Data.XmlReadMode.IgnoreSchema);
            reader.Close();
        }

        private void deletePowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.ActivePane != null)
            {
                if (Globals.ThisAddIn.ActivePane.VariablesExists())
                {
                    if ( MessageBox.Show("Удалить дополнительные данные из документа?", "Удаление скрытых данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)== DialogResult.Yes)
                    {
                        Globals.ThisAddIn.ActivePane.DeleteVariables();
                    }
                } else
                {
                    MessageBox.Show("Дополнительные данные отсуствуют!", "Удаление скрытых данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void paneVisibleButton_Click(object sender, RibbonControlEventArgs e)
        {
            RibbonToggleButton button = (RibbonToggleButton)sender;
            if (button.Id == Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Id &&
                Globals.ThisAddIn.Panes.Count > 0)
            {
                Globals.ThisAddIn.Panes.ActivePane.Visible = Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked;
            }           
        }

        private void createTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowCreateTableDialog();                           
        }

        private void editTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowEditTableDialog();
        }

        private void editCategoriesButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowEditCategoriesDialog();
        }

        private void editDocumentKeysButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowDocumentKeysDialog();
        }

        private void fieldAddButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            WordHiddenPowers.Utils.WordUtil.AddField(pane.Document, Globals.ThisAddIn.Application.Selection , 1);
        }

        private void analizerImportButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ImportDataFromWordDocuments();
        }

        private void analizerTableViewerButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowTableViewerDialog();
        }

        private void analizerDialogButton_Click(object sender, RibbonControlEventArgs e)
        {
            WordHiddenPowersPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as WordHiddenPowersPane;
            pane.ShowAnalizerDialog();
        }


        private enum NoteType
        {
            Text = 0,
            Decimal = 1
        }

        private NoteType lastNoteType = NoteType.Text;

        private void AddTextNoteButton_Click(object sender, RibbonControlEventArgs e)
        {
            NotesPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as NotesPane;
            if (pane !=null)
                pane.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);

            lastNoteType = NoteType.Text;
            AddLastNoteTypeButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
            AddLastNoteTypeButton.Label = Const.Content.TEXT_NOTE_LABEL;
            AddLastNoteTypeButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
            AddLastNoteTypeButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
            AddLastNoteTypeButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;
        }

        private void AddDecimalNoteButton_Click(object sender, RibbonControlEventArgs e)
        {
            NotesPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as NotesPane;
            if (pane != null)
                pane.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);

            lastNoteType = NoteType.Decimal;
            AddLastNoteTypeButton.Description = Const.Content.DECIMAL_NOTE_DESCRIPTION;
            AddLastNoteTypeButton.Label = Const.Content.DECIMAL_NOTE_LABEL;
            AddLastNoteTypeButton.OfficeImageId = Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID;
            AddLastNoteTypeButton.ScreenTip = Const.Content.DECIMAL_NOTE_SCREEN_TIP;
            AddLastNoteTypeButton.SuperTip = Const.Content.DECIMAL_NOTE_SUPER_TIP;
        }

        private void AddLastNoteTypeButton_Click(object sender, RibbonControlEventArgs e)
        {
            NotesPane pane = Globals.ThisAddIn.Panes.ActivePane.Control as NotesPane;
            if (pane != null)
            {
                if (lastNoteType == NoteType.Text)
                    pane.AddTextNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
                else
                    pane.AddDecimalNote(Globals.ThisAddIn.Application.ActiveWindow.Selection);
            }
        }
    }
}
