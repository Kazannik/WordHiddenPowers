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

            addLastNoteTypeButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
            addLastNoteTypeButton.Label = Const.Content.TEXT_NOTE_LABEL;
            addLastNoteTypeButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
            addLastNoteTypeButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
            addLastNoteTypeButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;

            addTextNoteButton.Description = Const.Content.TEXT_NOTE_DESCRIPTION;
            addTextNoteButton.Label = Const.Content.TEXT_NOTE_LABEL;
            addTextNoteButton.OfficeImageId = Const.Content.TEXT_NOTE_OFFICE_IMAGE_ID;
            addTextNoteButton.ScreenTip = Const.Content.TEXT_NOTE_SCREEN_TIP;
            addTextNoteButton.SuperTip = Const.Content.TEXT_NOTE_SUPER_TIP;

            addDecimalNoteButton.Description = Const.Content.DECIMAL_NOTE_DESCRIPTION;
            addDecimalNoteButton.Label = Const.Content.DECIMAL_NOTE_LABEL;
            addDecimalNoteButton.OfficeImageId = Const.Content.DECIMAL_NOTE_OFFICE_IMAGE_ID;
            addDecimalNoteButton.ScreenTip = Const.Content.DECIMAL_NOTE_SCREEN_TIP;
            addDecimalNoteButton.SuperTip = Const.Content.DECIMAL_NOTE_SUPER_TIP;

            newDataButton.Description = Const.Content.NEW_DATA_DESCRIPTION;
            newDataButton.Label = Const.Content.NEW_DATA_LABEL;
            newDataButton.OfficeImageId = Const.Content.NEW_DATA_OFFICE_IMAGE_ID;
            newDataButton.ScreenTip = Const.Content.NEW_DATA_SCREEN_TIP;
            newDataButton.SuperTip = Const.Content.NEW_DATA_SUPER_TIP;

            deleteDataButton.Description = Const.Content.DELETE_DATA_DESCRIPTION;
            deleteDataButton.Label = Const.Content.DELETE_DATA_LABEL;
            deleteDataButton.OfficeImageId = Const.Content.DELETE_DATA_OFFICE_IMAGE_ID;
            deleteDataButton.ScreenTip = Const.Content.DELETE_DATA_SCREEN_TIP;
            deleteDataButton.SuperTip = Const.Content.DELETE_DATA_SUPER_TIP;

            openDataButton.Description = Const.Content.OPEN_DATA_DESCRIPTION;
            openDataButton.Label = Const.Content.OPEN_DATA_LABEL;
            openDataButton.OfficeImageId = Const.Content.OPEN_DATA_OFFICE_IMAGE_ID;
            openDataButton.ScreenTip = Const.Content.OPEN_DATA_SCREEN_TIP;
            openDataButton.SuperTip = Const.Content.OPEN_DATA_SUPER_TIP;

            saveDataButton.Description = Const.Content.SAVE_DATA_DESCRIPTION;
            saveDataButton.Label = Const.Content.SAVE_DATA_LABEL;
            saveDataButton.OfficeImageId = Const.Content.SAVE_DATA_OFFICE_IMAGE_ID;
            saveDataButton.ScreenTip = Const.Content.SAVE_DATA_SCREEN_TIP;
            saveDataButton.SuperTip = Const.Content.SAVE_DATA_SUPER_TIP;

            editCategoriesButton.Description = Const.Content.EDIT_CATEGORIES_DESCRIPTION;
            editCategoriesButton.Label = Const.Content.EDIT_CATEGORIES_LABEL;
            editCategoriesButton.OfficeImageId = Const.Content.EDIT_CATEGORIES_OFFICE_IMAGE_ID;
            editCategoriesButton.ScreenTip = Const.Content.EDIT_CATEGORIES_SCREEN_TIP;
            editCategoriesButton.SuperTip = Const.Content.EDIT_CATEGORIES_SUPER_TIP;

            createTableButton.Description = Const.Content.CREATE_TABLE_DESCRIPTION;
            createTableButton.Label = Const.Content.CREATE_TABLE_LABEL;
            createTableButton.OfficeImageId = Const.Content.CREATE_TABLE_OFFICE_IMAGE_ID;
            createTableButton.ScreenTip = Const.Content.CREATE_TABLE_SCREEN_TIP;
            createTableButton.SuperTip = Const.Content.CREATE_TABLE_SUPER_TIP;

            editDocumentKeysButton.Description = Const.Content.EDIT_DOCUMENT_KEYS_DESCRIPTION;
            editDocumentKeysButton.Label = Const.Content.EDIT_DOCUMENT_KEYS_LABEL;
            editDocumentKeysButton.OfficeImageId = Const.Content.EDIT_DOCUMENT_KEYS_OFFICE_IMAGE_ID;
            editDocumentKeysButton.ScreenTip = Const.Content.EDIT_DOCUMENT_KEYS_SCREEN_TIP;
            editDocumentKeysButton.SuperTip = Const.Content.EDIT_DOCUMENT_KEYS_SUPER_TIP;

            analizerImportButton.Description = Const.Content.ANALAIZER_IMPORT_DESCRIPTION;
            analizerImportButton.Label = Const.Content.ANALAIZER_IMPORT_LABEL;
            analizerImportButton.OfficeImageId = Const.Content.ANALAIZER_IMPORT_OFFICE_IMAGE_ID;
            analizerImportButton.ScreenTip = Const.Content.ANALAIZER_IMPORT_SCREEN_TIP;
            analizerImportButton.SuperTip = Const.Content.ANALAIZER_IMPORT_SUPER_TIP;

            analizerTableViewerButton.Description = Const.Content.ANALAIZER_TABLE_DESCRIPTION;
            analizerTableViewerButton.Label = Const.Content.ANALAIZER_TABLE_LABEL;
            analizerTableViewerButton.OfficeImageId = Const.Content.ANALAIZER_TABLE_OFFICE_IMAGE_ID;
            analizerTableViewerButton.ScreenTip = Const.Content.ANALAIZER_TABLE_SCREEN_TIP;
            analizerTableViewerButton.SuperTip = Const.Content.ANALAIZER_TABLE_SUPER_TIP;

            analizerDialogButton.Description = Const.Content.ANALAIZER_DIALOG_DESCRIPTION;
            analizerDialogButton.Label = Const.Content.ANALAIZER_DIALOG_LABEL;
            analizerDialogButton.OfficeImageId = Const.Content.ANALAIZER_DIALOG_OFFICE_IMAGE_ID;
            analizerDialogButton.ScreenTip = Const.Content.ANALAIZER_DIALOG_SCREEN_TIP;
            analizerDialogButton.SuperTip = Const.Content.ANALAIZER_DIALOG_SUPER_TIP;

            editTableButton.Description = Const.Content.EDIT_TABLE_DESCRIPTION;
            editTableButton.Label = Const.Content.EDIT_TABLE_LABEL;
            editTableButton.OfficeImageId = Const.Content.EDIT_TABLE_OFFICE_IMAGE_ID;
            editTableButton.ScreenTip = Const.Content.EDIT_TABLE_SCREEN_TIP;
            editTableButton.SuperTip = Const.Content.EDIT_TABLE_SUPER_TIP;

            paneVisibleButton.Description = Const.Content.PANE_VISIBLE_DESCRIPTION;
            paneVisibleButton.Label = Const.Content.PANE_VISIBLE_LABEL;
            paneVisibleButton.OfficeImageId = Const.Content.PANE_VISIBLE_OFFICE_IMAGE_ID;
            paneVisibleButton.ScreenTip = Const.Content.PANE_VISIBLE_SCREEN_TIP;
            paneVisibleButton.SuperTip = Const.Content.PANE_VISIBLE_SUPER_TIP;
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
			this.newDataButton = this.Factory.CreateRibbonButton();
			this.deleteDataButton = this.Factory.CreateRibbonButton();
			this.separator1 = this.Factory.CreateRibbonSeparator();
			this.openDataButton = this.Factory.CreateRibbonButton();
			this.saveDataButton = this.Factory.CreateRibbonButton();
			this.separator2 = this.Factory.CreateRibbonSeparator();
			this.editCategoriesButton = this.Factory.CreateRibbonButton();
			this.createTableButton = this.Factory.CreateRibbonButton();
			this.editDocumentKeysButton = this.Factory.CreateRibbonButton();
			this.AnalizerGroup = this.Factory.CreateRibbonGroup();
			this.analizerImportButton = this.Factory.CreateRibbonButton();
			this.analizerTableViewerButton = this.Factory.CreateRibbonButton();
			this.analizerDialogButton = this.Factory.CreateRibbonButton();
			this.NotesGroup = this.Factory.CreateRibbonGroup();
			this.addLastNoteTypeButton = this.Factory.CreateRibbonSplitButton();
			this.addTextNoteButton = this.Factory.CreateRibbonButton();
			this.addDecimalNoteButton = this.Factory.CreateRibbonButton();
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
			this.WordHiddenPowersTab.Position = this.Factory.RibbonPosition.AfterOfficeId("TabReferences");
			// 
			// maketGroup
			// 
			this.maketGroup.Items.Add(this.newDataButton);
			this.maketGroup.Items.Add(this.deleteDataButton);
			this.maketGroup.Items.Add(this.separator1);
			this.maketGroup.Items.Add(this.openDataButton);
			this.maketGroup.Items.Add(this.saveDataButton);
			this.maketGroup.Items.Add(this.separator2);
			this.maketGroup.Items.Add(this.editCategoriesButton);
			this.maketGroup.Items.Add(this.createTableButton);
			this.maketGroup.Items.Add(this.editDocumentKeysButton);
			this.maketGroup.Label = "Макет данных";
			this.maketGroup.Name = "maketGroup";
			// 
			// newDataButton
			// 
			this.newDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.newDataButton.Label = "Создать макет данных";
			this.newDataButton.Name = "newDataButton";
			this.newDataButton.OfficeImageId = "CreateSubsite";
			this.newDataButton.ShowImage = true;
			this.newDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.NewPowers_Click);
			// 
			// deleteDataButton
			// 
			this.deleteDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.deleteDataButton.Label = "Удалить данные";
			this.deleteDataButton.Name = "deleteDataButton";
			this.deleteDataButton.OfficeImageId = "Delete";
			this.deleteDataButton.ShowImage = true;
			this.deleteDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DeletePowers_Click);
			// 
			// separator1
			// 
			this.separator1.Name = "separator1";
			// 
			// openDataButton
			// 
			this.openDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.openDataButton.Description = "Открыть макет данных в котором сохранены настройки ";
			this.openDataButton.Label = "Открыть макет данных...";
			this.openDataButton.Name = "openDataButton";
			this.openDataButton.OfficeImageId = "OpenSubsite";
			this.openDataButton.ScreenTip = "Открыть макет данных";
			this.openDataButton.ShowImage = true;
			this.openDataButton.SuperTip = "В макете данных сохранены все настройки, необходимые для анализа документов";
			this.openDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OpenPowers_Click);
			// 
			// saveDataButton
			// 
			this.saveDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.saveDataButton.Label = "Сохранить макет данных...";
			this.saveDataButton.Name = "saveDataButton";
			this.saveDataButton.OfficeImageId = "FileSaveAs";
			this.saveDataButton.ShowImage = true;
			this.saveDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SavePowers_Click);
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
			this.editCategoriesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.EditCategories_Click);
			// 
			// createTableButton
			// 
			this.createTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.createTableButton.Label = "Макет таблицы...";
			this.createTableButton.Name = "createTableButton";
			this.createTableButton.OfficeImageId = "GroupCreateTableSql";
			this.createTableButton.ShowImage = true;
			this.createTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CreateTable_Click);
			// 
			// editDocumentKeysButton
			// 
			this.editDocumentKeysButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.editDocumentKeysButton.Label = "Коллекция заголовков";
			this.editDocumentKeysButton.Name = "editDocumentKeysButton";
			this.editDocumentKeysButton.OfficeImageId = "ControlPropertyListControlEditChoices";
			this.editDocumentKeysButton.ShowImage = true;
			this.editDocumentKeysButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.EditDocumentKeys_Click);
			// 
			// AnalizerGroup
			// 
			this.AnalizerGroup.Items.Add(this.analizerImportButton);
			this.AnalizerGroup.Items.Add(this.analizerTableViewerButton);
			this.AnalizerGroup.Items.Add(this.analizerDialogButton);
			this.AnalizerGroup.Label = "Анализ данных";
			this.AnalizerGroup.Name = "AnalizerGroup";
			// 
			// analizerImportButton
			// 
			this.analizerImportButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.analizerImportButton.Label = "Импортировать данные...";
			this.analizerImportButton.Name = "analizerImportButton";
			this.analizerImportButton.ShowImage = true;
			this.analizerImportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerImport_Click);
			// 
			// analizerTableViewerButton
			// 
			this.analizerTableViewerButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.analizerTableViewerButton.Label = "Табличные данные";
			this.analizerTableViewerButton.Name = "analizerTableViewerButton";
			this.analizerTableViewerButton.ShowImage = true;
			this.analizerTableViewerButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerTableViewer_Click);
			// 
			// analizerDialogButton
			// 
			this.analizerDialogButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.analizerDialogButton.Label = "Анализ данных...";
			this.analizerDialogButton.Name = "analizerDialogButton";
			this.analizerDialogButton.ShowImage = true;
			this.analizerDialogButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerDialog_Click);
			// 
			// NotesGroup
			// 
			this.NotesGroup.Items.Add(this.addLastNoteTypeButton);
			this.NotesGroup.Items.Add(this.editTableButton);
			this.NotesGroup.Items.Add(this.separator3);
			this.NotesGroup.Items.Add(this.paneVisibleButton);
			this.NotesGroup.Label = "Дополнительные данные";
			this.NotesGroup.Name = "NotesGroup";
			// 
			// addLastNoteTypeButton
			// 
			this.addLastNoteTypeButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.addLastNoteTypeButton.Items.Add(this.addTextNoteButton);
			this.addLastNoteTypeButton.Items.Add(this.addDecimalNoteButton);
			this.addLastNoteTypeButton.Label = "Текстовая заметка";
			this.addLastNoteTypeButton.Name = "addLastNoteTypeButton";
			this.addLastNoteTypeButton.OfficeImageId = "FunctionsTextInsertGallery";
			this.addLastNoteTypeButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddLastNoteType_Click);
			// 
			// addTextNoteButton
			// 
			this.addTextNoteButton.Description = "Создание текстовой заметки";
			this.addTextNoteButton.Label = "Текстовая заметка";
			this.addTextNoteButton.Name = "addTextNoteButton";
			this.addTextNoteButton.OfficeImageId = "FunctionsTextInsertGallery";
			this.addTextNoteButton.ScreenTip = "Создать текстовую заметку";
			this.addTextNoteButton.ShowImage = true;
			this.addTextNoteButton.SuperTip = "Текстовая заметка позволяет сохранить текстовые сведения в массив дополнительных " +
    "данных для их дальнейшего анализа";
			this.addTextNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddTextNote_Click);
			// 
			// addDecimalNoteButton
			// 
			this.addDecimalNoteButton.Description = "Создание числовой заметки";
			this.addDecimalNoteButton.Label = "Числовая заметка";
			this.addDecimalNoteButton.Name = "addDecimalNoteButton";
			this.addDecimalNoteButton.OfficeImageId = "FunctionsMathTrigInsertGallery";
			this.addDecimalNoteButton.ScreenTip = "Создать числовую заметку";
			this.addDecimalNoteButton.ShowImage = true;
			this.addDecimalNoteButton.SuperTip = "Числовая заметка позволяет суммировать значения в массиве дополнительных данных д" +
    "ля их дальнейшего анализа";
			this.addDecimalNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddDecimalNote_Click);
			// 
			// editTableButton
			// 
			this.editTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.editTableButton.Label = "Таблица данных...";
			this.editTableButton.Name = "editTableButton";
			this.editTableButton.OfficeImageId = "TableStyleModify";
			this.editTableButton.ShowImage = true;
			this.editTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.EditTable_Click);
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newDataButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton openDataButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton saveDataButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton deleteDataButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton paneVisibleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton createTableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editTableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editCategoriesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editDocumentKeysButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AnalizerGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerImportButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerTableViewerButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton analizerDialogButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup NotesGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton addLastNoteTypeButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addTextNoteButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addDecimalNoteButton;
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
