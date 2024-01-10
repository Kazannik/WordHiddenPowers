namespace WordHiddenPowers
{
    partial class WordHiddenPowersRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public WordHiddenPowersRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.WordHiddenPowersTab = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.newPowersButton = this.Factory.CreateRibbonButton();
            this.deletePowersButton = this.Factory.CreateRibbonButton();
            this.openPowersButton = this.Factory.CreateRibbonButton();
            this.savePowersButton = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.paneVisibleButton = this.Factory.CreateRibbonToggleButton();
            this.createTableButton = this.Factory.CreateRibbonButton();
            this.editTableButton = this.Factory.CreateRibbonButton();
            this.WordHiddenPowersTab.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WordHiddenPowersTab
            // 
            this.WordHiddenPowersTab.Groups.Add(this.group1);
            this.WordHiddenPowersTab.Groups.Add(this.group2);
            this.WordHiddenPowersTab.Label = "Дополнительные данные";
            this.WordHiddenPowersTab.Name = "WordHiddenPowersTab";
            // 
            // group1
            // 
            this.group1.Items.Add(this.newPowersButton);
            this.group1.Items.Add(this.deletePowersButton);
            this.group1.Items.Add(this.openPowersButton);
            this.group1.Items.Add(this.savePowersButton);
            this.group1.Items.Add(this.createTableButton);
            this.group1.Label = "Набор данных";
            this.group1.Name = "group1";
            // 
            // newPowersButton
            // 
            this.newPowersButton.Label = "Создать макет данных";
            this.newPowersButton.Name = "newPowersButton";
            this.newPowersButton.ShowImage = true;
            this.newPowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.newPowersButton_Click);
            // 
            // deletePowersButton
            // 
            this.deletePowersButton.Label = "Удалить данные";
            this.deletePowersButton.Name = "deletePowersButton";
            this.deletePowersButton.ShowImage = true;
            this.deletePowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.deletePowersButton_Click);
            // 
            // openPowersButton
            // 
            this.openPowersButton.Label = "Открыть макет данных...";
            this.openPowersButton.Name = "openPowersButton";
            this.openPowersButton.ShowImage = true;
            this.openPowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.openPowersButton_Click);
            // 
            // savePowersButton
            // 
            this.savePowersButton.Label = "Сохранить макет данных...";
            this.savePowersButton.Name = "savePowersButton";
            this.savePowersButton.ShowImage = true;
            this.savePowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.savePowersButton_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.paneVisibleButton);
            this.group2.Items.Add(this.editTableButton);
            this.group2.Label = "Панель управления";
            this.group2.Name = "group2";
            // 
            // paneVisibleButton
            // 
            this.paneVisibleButton.Label = "Дополнительные данные";
            this.paneVisibleButton.Name = "paneVisibleButton";
            this.paneVisibleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.paneVisibleButton_Click);
            // 
            // createTableButton
            // 
            this.createTableButton.Label = "Макет таблицы...";
            this.createTableButton.Name = "createTableButton";
            this.createTableButton.ShowImage = true;
            this.createTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.createTableButton_Click);
            // 
            // editTableButton
            // 
            this.editTableButton.Label = "Таблица данных...";
            this.editTableButton.Name = "editTableButton";
            this.editTableButton.ShowImage = true;
            this.editTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editTableButton_Click);
            // 
            // WordHiddenPowersRibbon
            // 
            this.Name = "WordHiddenPowersRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.WordHiddenPowersTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.WordHiddenPowersRibbon_Load);
            this.WordHiddenPowersTab.ResumeLayout(false);
            this.WordHiddenPowersTab.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab WordHiddenPowersTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newPowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton openPowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton savePowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton deletePowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton paneVisibleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton createTableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editTableButton;
    }

    partial class ThisRibbonCollection
    {
        internal WordHiddenPowersRibbon WordHiddenPowersRibbon
        {
            get { return this.GetRibbon<WordHiddenPowersRibbon>(); }
        }
    }
}
