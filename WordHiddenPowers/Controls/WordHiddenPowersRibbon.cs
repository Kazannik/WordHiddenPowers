using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace WordHiddenPowers
{
    public partial class WordHiddenPowersRibbon
    {
        const string dialogFilters = "XML Schema File (.xsd)|*.xsd";

        private void WordHiddenPowersRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void newPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.PowersDataSet.Reset();
        }

        private void openPowersButton_Click(object sender, RibbonControlEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = dialogFilters;
            if (dialog.ShowDialog( Utils.ShowDialogUtil.GetOwner()) == DialogResult.OK)
            {
                try
                {
                    Globals.ThisAddIn.PowersDataSet.ReadXml(dialog.FileName, System.Data.XmlReadMode.IgnoreSchema);
                    Globals.ThisAddIn.PowersDataSet.DecimalPowers.Clear();
                    Globals.ThisAddIn.PowersDataSet.StringPowers.Clear();
                }
                catch (Exception)
                {
                    
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
                    string xml = GetXml(Globals.ThisAddIn.PowersDataSet);
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
            Document doc = Globals.ThisAddIn.Application.ActiveDocument;
            Variable variable = Globals.ThisAddIn.GetVariable(doc.Variables, Globals.ThisAddIn.TableVariableName);
            if (variable != null)
            {
               if ( MessageBox.Show("Удалить дополнительные данные из документа?", "Удаление скрытых данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)== DialogResult.Yes)
                {
                    Globals.ThisAddIn.PowersDataSet.Categories.Clear();
                    Globals.ThisAddIn.PowersDataSet.Subcategories.Clear();
                    Globals.ThisAddIn.PowersDataSet.DecimalPowers.Clear();
                    Globals.ThisAddIn.PowersDataSet.StringPowers.Clear();
                    Globals.ThisAddIn.PowersDataSet.Reset();
                    variable.Delete();
                }
            } else
            {
                MessageBox.Show("Дополнительные данные отсуствуют!", "Удаление скрытых данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
