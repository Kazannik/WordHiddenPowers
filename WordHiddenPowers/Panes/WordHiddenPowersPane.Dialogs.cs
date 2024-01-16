using System.Windows.Forms;
using WordHiddenPowers.Dialogs;

namespace WordHiddenPowers.Panes
{
    partial class WordHiddenPowersPane
    {

        public void ShowDocumentKeysDialog()
        {
            Form dialog = new DocumentKeysDialog(this);
            dialogs.Add(dialog);
            dialog.ShowDialog();
        }

        public void ShowEditCategoriesDialog()
        {
            Form dialog = new CategoriesEditorDialog(this);
            dialogs.Add(dialog);
            dialog.ShowDialog();
        }

        public void ShowCreateTableDialog()
        {
            Form dialog = new CreateTableDialog(this);
            dialogs.Add(dialog);
            dialog.ShowDialog();
        }

        public void ShowEditTableDialog()
        {
            Form dialog = new TableEditorDialog(this);
            dialogs.Add(dialog);
            dialog.Show();
        }



    }
}