using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using WordHiddenPowers.Documents;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Panes
{
    [DesignerCategory("code")]
    public class WordHiddenPowersPane : UserControl
    {
        private IContainer components;
                
        public Word._Document Document { get; }

        public RepositoryDataSet PowersDataSet { get; }

        protected WordHiddenPowersPane()
        {
            InitializeComponent();
        }

        public WordHiddenPowersPane(Word._Document Doc)
        {
            

            Document = Doc;

            PowersDataSet = new RepositoryDataSet();

            InitializeComponent();
        }

      
        public void CommitVariables()
        {
            if (PowersDataSet.HasChanges())
            {
                StringBuilder builder = new StringBuilder();
                StringWriter writer = new StringWriter(builder);
                PowersDataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
                writer.Close();
                CommitVariables(Const.Globals.XML_VARIABLE_NAME, builder.ToString());
            }
        }

        protected void CommitVariables(string name, string value)
        {
            Word.Variable variable = HiddenPowerDocument.GetVariable(Document.Variables, name);
            if (variable == null && !string.IsNullOrWhiteSpace(value))
                Document.Variables.Add(name, value);
            else if (variable != null && variable.Value != value)
                variable.Value = value;
        }

        

       




        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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

