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
            this.maketGroup = this.Factory.CreateRibbonGroup();
            this.newPowersButton = this.Factory.CreateRibbonButton();
            this.deletePowersButton = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.openPowersButton = this.Factory.CreateRibbonButton();
            this.savePowersButton = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.editCategoriesButton = this.Factory.CreateRibbonButton();
            this.createTableButton = this.Factory.CreateRibbonButton();
            this.editDocumentKeysButton = this.Factory.CreateRibbonButton();
            this.AnalizerGroup = this.Factory.CreateRibbonGroup();
            this.analizerImportButton = this.Factory.CreateRibbonButton();
            this.analizerTableViewerButton = this.Factory.CreateRibbonButton();
            this.analizerDialogButton = this.Factory.CreateRibbonButton();
            this.fieldsUpdateButton = this.Factory.CreateRibbonButton();
            this.fieldAddButton = this.Factory.CreateRibbonButton();
            this.NotesGroup = this.Factory.CreateRibbonGroup();
            this.AddLastNoteTypeButton = this.Factory.CreateRibbonSplitButton();
            this.AddTextNoteButton = this.Factory.CreateRibbonButton();
            this.AddDecimalNoteButton = this.Factory.CreateRibbonButton();
            this.editTableButton = this.Factory.CreateRibbonButton();
            this.separator3 = this.Factory.CreateRibbonSeparator();
            this.paneVisibleButton = this.Factory.CreateRibbonToggleButton();
            this.WordHiddenPowersTab.SuspendLayout();
            this.maketGroup.SuspendLayout();
            this.AnalizerGroup.SuspendLayout();
            this.NotesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // WordHiddenPowersTab
            // 
            this.WordHiddenPowersTab.Groups.Add(this.maketGroup);
            this.WordHiddenPowersTab.Groups.Add(this.AnalizerGroup);
            this.WordHiddenPowersTab.Groups.Add(this.NotesGroup);
            this.WordHiddenPowersTab.Label = "Дополнительные данные";
            this.WordHiddenPowersTab.Name = "WordHiddenPowersTab";
            // 
            // maketGroup
            // 
            this.maketGroup.Items.Add(this.newPowersButton);
            this.maketGroup.Items.Add(this.deletePowersButton);
            this.maketGroup.Items.Add(this.separator1);
            this.maketGroup.Items.Add(this.openPowersButton);
            this.maketGroup.Items.Add(this.savePowersButton);
            this.maketGroup.Items.Add(this.separator2);
            this.maketGroup.Items.Add(this.editCategoriesButton);
            this.maketGroup.Items.Add(this.createTableButton);
            this.maketGroup.Items.Add(this.editDocumentKeysButton);
            this.maketGroup.Label = "Макет данных";
            this.maketGroup.Name = "maketGroup";
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
            // separator1
            // 
            this.separator1.Name = "separator1";
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
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // editCategoriesButton
            // 
            this.editCategoriesButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.editCategoriesButton.Label = "Редактор категорий...";
            this.editCategoriesButton.Name = "editCategoriesButton";
            this.editCategoriesButton.OfficeImageId = "EditXPath";
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
            // editDocumentKeysButton
            // 
            this.editDocumentKeysButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.editDocumentKeysButton.Label = "Коллекция заголовков";
            this.editDocumentKeysButton.Name = "editDocumentKeysButton";
            this.editDocumentKeysButton.OfficeImageId = "ControlPropertyListControlEditChoices";
            this.editDocumentKeysButton.ShowImage = true;
            this.editDocumentKeysButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editDocumentKeysButton_Click);
            // 
            // AnalizerGroup
            // 
            this.AnalizerGroup.Items.Add(this.analizerImportButton);
            this.AnalizerGroup.Items.Add(this.analizerTableViewerButton);
            this.AnalizerGroup.Items.Add(this.analizerDialogButton);
            this.AnalizerGroup.Items.Add(this.fieldsUpdateButton);
            this.AnalizerGroup.Items.Add(this.fieldAddButton);
            this.AnalizerGroup.Label = "Анализ данных";
            this.AnalizerGroup.Name = "AnalizerGroup";
            // 
            // analizerImportButton
            // 
            this.analizerImportButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.analizerImportButton.Label = "Импортировать данные...";
            this.analizerImportButton.Name = "analizerImportButton";
            this.analizerImportButton.ShowImage = true;
            this.analizerImportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.analizerImportButton_Click);
            // 
            // analizerTableViewerButton
            // 
            this.analizerTableViewerButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.analizerTableViewerButton.Label = "Табличные данные";
            this.analizerTableViewerButton.Name = "analizerTableViewerButton";
            this.analizerTableViewerButton.ShowImage = true;
            this.analizerTableViewerButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.analizerTableViewerButton_Click);
            // 
            // analizerDialogButton
            // 
            this.analizerDialogButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.analizerDialogButton.Label = "Анализ данных...";
            this.analizerDialogButton.Name = "analizerDialogButton";
            this.analizerDialogButton.ShowImage = true;
            this.analizerDialogButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.analizerDialogButton_Click);
            // 
            // fieldsUpdateButton
            // 
            this.fieldsUpdateButton.Label = "Update";
            this.fieldsUpdateButton.Name = "fieldsUpdateButton";
            this.fieldsUpdateButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.fieldsUpdateButton_Click);
            // 
            // fieldAddButton
            // 
            this.fieldAddButton.Label = "Add";
            this.fieldAddButton.Name = "fieldAddButton";
            this.fieldAddButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.fieldAddButton_Click);
            // 
            // NotesGroup
            // 
            this.NotesGroup.Items.Add(this.AddLastNoteTypeButton);
            this.NotesGroup.Items.Add(this.editTableButton);
            this.NotesGroup.Items.Add(this.separator3);
            this.NotesGroup.Items.Add(this.paneVisibleButton);
            this.NotesGroup.Label = "Дополнительные данные";
            this.NotesGroup.Name = "NotesGroup";
            // 
            // AddLastNoteTypeButton
            // 
            this.AddLastNoteTypeButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.AddLastNoteTypeButton.Description = "Создание текстовой заметки";
            this.AddLastNoteTypeButton.Items.Add(this.AddTextNoteButton);
            this.AddLastNoteTypeButton.Items.Add(this.AddDecimalNoteButton);
            this.AddLastNoteTypeButton.Label = "Текстовая заметка";
            this.AddLastNoteTypeButton.Name = "AddLastNoteTypeButton";
            this.AddLastNoteTypeButton.OfficeImageId = "FunctionsTextInsertGallery";
            this.AddLastNoteTypeButton.ScreenTip = "Создать текстовую заметку";
            this.AddLastNoteTypeButton.SuperTip = "Текстовая заметка позволяет сохранить текстовые сведения в массив дополнительных " +
    "данных для их дальнейшего анализа";
            this.AddLastNoteTypeButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddLastNoteTypeButton_Click);
            // 
            // AddTextNoteButton
            // 
            this.AddTextNoteButton.Description = "Создание текстовой заметки";
            this.AddTextNoteButton.Label = "Текстовая заметка";
            this.AddTextNoteButton.Name = "AddTextNoteButton";
            this.AddTextNoteButton.OfficeImageId = "FunctionsTextInsertGallery";
            this.AddTextNoteButton.ScreenTip = "Создать текстовую заметку";
            this.AddTextNoteButton.ShowImage = true;
            this.AddTextNoteButton.SuperTip = "Текстовая заметка позволяет сохранить текстовые сведения в массив дополнительных " +
    "данных для их дальнейшего анализа";
            this.AddTextNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddTextNoteButton_Click);
            // 
            // AddDecimalNoteButton
            // 
            this.AddDecimalNoteButton.Description = "Создание числовой заметки";
            this.AddDecimalNoteButton.Label = "Числовая заметка";
            this.AddDecimalNoteButton.Name = "AddDecimalNoteButton";
            this.AddDecimalNoteButton.OfficeImageId = "FunctionsMathTrigInsertGallery";
            this.AddDecimalNoteButton.ScreenTip = "Создать числовую заметку";
            this.AddDecimalNoteButton.ShowImage = true;
            this.AddDecimalNoteButton.SuperTip = "Числовая заметка позволяет суммировать значения в массиве дополнительных данных д" +
    "ля их дальнейшего анализа";
            this.AddDecimalNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddDecimalNoteButton_Click);
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
            // separator3
            // 
            this.separator3.Name = "separator3";
            // 
            // paneVisibleButton
            // 
            this.paneVisibleButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.paneVisibleButton.Label = "Панель управления";
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
            this.maketGroup.ResumeLayout(false);
            this.maketGroup.PerformLayout();
            this.AnalizerGroup.ResumeLayout(false);
            this.AnalizerGroup.PerformLayout();
            this.NotesGroup.ResumeLayout(false);
            this.NotesGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab WordHiddenPowersTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup maketGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newPowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton openPowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton savePowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton deletePowersButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton paneVisibleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton createTableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editTableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editCategoriesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editDocumentKeysButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AnalizerGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton fieldsUpdateButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton fieldAddButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerImportButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerTableViewerButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerDialogButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup NotesGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton AddLastNoteTypeButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AddTextNoteButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AddDecimalNoteButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator3;
    }

    partial class ThisRibbonCollection
    {
        internal WordHiddenPowersRibbon WordHiddenPowersRibbon
        {
            get { return this.GetRibbon<WordHiddenPowersRibbon>(); }
        }
    }
}
