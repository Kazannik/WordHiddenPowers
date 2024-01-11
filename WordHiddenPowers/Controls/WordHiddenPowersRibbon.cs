using System;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using System.IO;
using WordHiddenPowers.Dialogs;

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
                Globals.ThisAddIn.ActivePane.PowersDataSet.StringPowers.Clear();

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
                if (dialog.ShowDialog( Utils.ShowDialogUtil.GetOwner()) == DialogResult.OK)
                {
                    try
                    {
                        Globals.ThisAddIn.ActivePane.PowersDataSet.RowsHeaders.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.ColumnsHeaders.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.Categories.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.Subcategories.Clear();

                        Globals.ThisAddIn.ActivePane.PowersDataSet.ReadXml(dialog.FileName, System.Data.XmlReadMode.IgnoreSchema);

                        Globals.ThisAddIn.ActivePane.PowersDataSet.DecimalPowers.Clear();
                        Globals.ThisAddIn.ActivePane.PowersDataSet.StringPowers.Clear();

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
            if (dialog.ShowDialog(Utils.ShowDialogUtil.GetOwner()) == DialogResult.OK)
            {
                try
                {
                    Globals.ThisAddIn.ActivePane.CommitVariables();

                    string xml = GetXml(Globals.ThisAddIn.ActivePane.PowersDataSet);
                    RepositoryDataSet powersDataSet = new RepositoryDataSet();
                    SetXml(powersDataSet, xml);
                    powersDataSet.DecimalPowers.Clear();
                    powersDataSet.StringPowers.Clear();
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
            if (button.Id == Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Id)
            {
                Globals.ThisAddIn.Panes.ActivePane.Visible = Globals.Ribbons.WordHiddenPowersRibbon.paneVisibleButton.Checked;
            }           
        }

        private void createTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            CreateTableDialog dialog = new  CreateTableDialog(Globals.ThisAddIn.ActivePane);
            dialog.ShowDialog();                      
        }

        private void editTableButton_Click(object sender, RibbonControlEventArgs e)
        {
            TableEditorDialog dialog = new TableEditorDialog(Globals.ThisAddIn.ActivePane);
            dialog.Show();
        }
    }
}
