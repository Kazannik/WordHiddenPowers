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
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.newPowersButton = this.Factory.CreateRibbonButton();
            this.deletePowersButton = this.Factory.CreateRibbonButton();
            this.openPowersButton = this.Factory.CreateRibbonButton();
            this.savePowersButton = this.Factory.CreateRibbonButton();
            this.editDocumentKeysButton = this.Factory.CreateRibbonButton();
            this.editCategoriesButton = this.Factory.CreateRibbonButton();
            this.createTableButton = this.Factory.CreateRibbonButton();
            this.editTableButton = this.Factory.CreateRibbonButton();
            this.paneVisibleButton = this.Factory.CreateRibbonToggleButton();
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
            this.group1.Items.Add(this.separator1);
            this.group1.Items.Add(this.openPowersButton);
            this.group1.Items.Add(this.savePowersButton);
            this.group1.Items.Add(this.separator2);
            this.group1.Items.Add(this.editDocumentKeysButton);
            this.group1.Items.Add(this.editCategoriesButton);
            this.group1.Items.Add(this.createTableButton);
            this.group1.Label = "Макет данных";
            this.group1.Name = "group1";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.editTableButton);
            this.group2.Items.Add(this.paneVisibleButton);
            this.group2.Label = "Панель управления";
            this.group2.Name = "group2";
            // 
            // newPowersButton
            // 
            this.newPowersButton.Label = "Создать макет данных";
            this.newPowersButton.Name = "newPowersButton";
            this.newPowersButton.OfficeImageId = "CreateSubsite";
            this.newPowersButton.ShowImage = true;
            this.newPowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.newPowersButton_Click);
            // 
            // deletePowersButton
            // 
            this.deletePowersButton.Label = "Удалить данные";
            this.deletePowersButton.Name = "deletePowersButton";
            this.deletePowersButton.OfficeImageId = "Delete";
            this.deletePowersButton.ShowImage = true;
            this.deletePowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.deletePowersButton_Click);
            // 
            // openPowersButton
            // 
            this.openPowersButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.openPowersButton.Description = "Открыть макет данных в котором сохранены настройки ";
            this.openPowersButton.Label = "Открыть макет данных...";
            this.openPowersButton.Name = "openPowersButton";
            this.openPowersButton.OfficeImageId = "OpenSubsite";
            this.openPowersButton.ScreenTip = "Открыть макет данных";
            this.openPowersButton.ShowImage = true;
            this.openPowersButton.SuperTip = "В макете данных сохранены все настройки, необходимые для анализа документов";
            this.openPowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.openPowersButton_Click);
            // 
            // savePowersButton
            // 
            this.savePowersButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.savePowersButton.Label = "Сохранить макет данных...";
            this.savePowersButton.Name = "savePowersButton";
            this.savePowersButton.OfficeImageId = "FileSaveAs";
            this.savePowersButton.ShowImage = true;
            this.savePowersButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.savePowersButton_Click);
            // 
            // editDocumentKeysButton
            // 
            this.editDocumentKeysButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.editDocumentKeysButton.Label = "Коллекция заголовков";
            this.editDocumentKeysButton.Name = "editDocumentKeysButton";
            this.editDocumentKeysButton.ShowImage = true;
            this.editDocumentKeysButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editDocumentKeysButton_Click);
            // 
            // editCategoriesButton
            // 
            this.editCategoriesButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.editCategoriesButton.Label = "Редактор категорий...";
            this.editCategoriesButton.Name = "editCategoriesButton";
            this.editCategoriesButton.OfficeImageId = "GroupContentTypeEdit";
            this.editCategoriesButton.ShowImage = true;
            this.editCategoriesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editCategoriesButton_Click);
            // 
            // createTableButton
            // 
            this.createTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.createTableButton.Label = "Макет таблицы...";
            this.createTableButton.Name = "createTableButton";
            this.createTableButton.OfficeImageId = "GroupCreateTableSql";
            this.createTableButton.ShowImage = true;
            this.createTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.createTableButton_Click);
            // 
            // editTableButton
            // 
            this.editTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.editTableButton.Label = "Таблица данных...";
            this.editTableButton.Name = "editTableButton";
            this.editTableButton.OfficeImageId = "TableStyleModify";
            this.editTableButton.ShowImage = true;
            this.editTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editTableButton_Click);
            // 
            // paneVisibleButton
            // 
            this.paneVisibleButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.paneVisibleButton.Label = "Дополнительные данные";
            this.paneVisibleButton.Name = "paneVisibleButton";
            this.paneVisibleButton.OfficeImageId = "MenuToDoBar";
            this.paneVisibleButton.ShowImage = true;
            this.paneVisibleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.paneVisibleButton_Click);
            // 
            // WordHiddenPowersRibbon
            // 
            this.Name = "WordHiddenPowersRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.WordHiddenPowersTab);
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
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editCategoriesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editDocumentKeysButton;
    }

    partial class ThisRibbonCollection
    {
        internal WordHiddenPowersRibbon WordHiddenPowersRibbon
        {
            get { return this.GetRibbon<WordHiddenPowersRibbon>(); }
        }
    }
}
