using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
    [DesignerCategory("code")]
    public class WordHiddenPowersPane : UserControl
    {
        protected System.Collections.Generic.IList<Form> dialogs = null;
        protected IContainer components;
                
        public Word.Document Document { get; }

        public RepositoryDataSet PowersDataSet { get; }

        protected WordHiddenPowersPane()
        {
            InitializeComponent();
        }

        public WordHiddenPowersPane(Word.Document Doc)
        {
            dialogs = new System.Collections.Generic.List<Form>();

            Document = Doc;

            PowersDataSet = new RepositoryDataSet();

            InitializeComponent();
        }

        public bool VariablesExists()
        {
            if (Document.Variables.Count > 0)
            {
                Word.Variable title = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.TITLE_VARIABLE_NAME);
                if (title != null)
                {
                    return true;
                }

                Word.Variable date = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.DATE_VARIABLE_NAME);
                if (date != null)
                {
                    return true;
                }

                Word.Variable description = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.DESCRIPTION_VARIABLE_NAME);
                if (description != null)
                {
                    return true;
                }

                Word.Variable categories = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.CATEGORIES_VARIABLE_NAME);
                if (categories != null)
                {
                    return true;
                }

                Word.Variable table = HiddenPowerDocument.GetVariable(Document.Variables, Const.Globals.TABLE_VARIABLE_NAME);
                if (table != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void CommitVariables()
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            PowersDataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
            writer.Close();
            CommitVariables(Const.Globals.CATEGORIES_VARIABLE_NAME, builder.ToString());
        }

        protected void CommitVariables(string name, string value)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable == null)
                Document.Variables.Add(name, value);
            else
                variable.Value = value;
        }

        public void DeleteVariables()
        {
            foreach (DataTable table in PowersDataSet.Tables)
            {
                table.Clear();
            }

            if (Document.Variables.Count > 0)
            {
                DeleteVariable(Const.Globals.TITLE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DATE_VARIABLE_NAME);
                DeleteVariable(Const.Globals.DESCRIPTION_VARIABLE_NAME);
                DeleteVariable(Const.Globals.CATEGORIES_VARIABLE_NAME);
                DeleteVariable(Const.Globals.TABLE_VARIABLE_NAME);
            }
        }

        private void DeleteVariable(string name)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable != null)
                variable.Delete();
        }


        public void ShowDocumentKeysDialog()
        {
            Form dialog = new DocumentKeysDialog(this);
            dialogs.Add(dialog);
            Utils.ShowDialogUtil.ShowDialog(dialog);
            OnPropertiesChanged(new EventArgs());
        }

        public void ShowEditCategoriesDialog()
        {
            Form dialog = new CategoriesEditorDialog(this);
            dialogs.Add(dialog);
            Utils.ShowDialogUtil.ShowDialog(dialog);
            OnPropertiesChanged(new EventArgs());
        }

        public void ShowCreateTableDialog()
        {
            Form dialog = new CreateTableDialog(this);
            dialogs.Add(dialog);
            ShowDialogUtil.ShowDialog(dialog);
            OnPropertiesChanged(new EventArgs());
        }

        public void ShowEditTableDialog()
        {
            Form dialog = new TableEditorDialog(this);
            dialogs.Add(dialog);
            dialog.Show();
            OnPropertiesChanged(new EventArgs());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (dialogs != null)
            {
                foreach (Form form in dialogs)
                {
                    if (form != null)
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                dialogs.Clear();
            }

            base.Dispose(disposing);
        }

        
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            Margin = new Padding(3, 2, 3, 2);
            Name = "WordHiddenPowersPane";
            Size = new Size(325, 322);
            ResumeLayout(false);
        }

        protected event EventHandler<EventArgs> PropertiesChanged;

        protected virtual void OnPropertiesChanged(EventArgs e)
        {
            PropertiesChanged?.Invoke(this, e);
        }
    }
}

