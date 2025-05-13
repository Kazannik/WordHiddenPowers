namespace WordHiddenPowers
{
    partial class AddInMainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AddInMainRibbon()
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

			aggregatedImportFolderButton.Description = Const.Content.ANALAIZER_IMPORT_FOLDER_DESCRIPTION;
			aggregatedImportFolderButton.Label = Const.Content.ANALAIZER_IMPORT_FOLDER_LABEL;
			aggregatedImportFolderButton.OfficeImageId = Const.Content.ANALAIZER_IMPORT_FOLDER_OFFICE_IMAGE_ID;
			aggregatedImportFolderButton.ScreenTip = Const.Content.ANALAIZER_IMPORT_FOLDER_SCREEN_TIP;
			aggregatedImportFolderButton.SuperTip = Const.Content.ANALAIZER_IMPORT_FOLDER_SUPER_TIP;

			aggregatedImportFileButton.Description = Const.Content.ANALAIZER_IMPORT_FILE_DESCRIPTION;
			aggregatedImportFileButton.Label = Const.Content.ANALAIZER_IMPORT_FILE_LABEL;
			aggregatedImportFileButton.OfficeImageId = Const.Content.ANALAIZER_IMPORT_FILE_OFFICE_IMAGE_ID;
			aggregatedImportFileButton.ScreenTip = Const.Content.ANALAIZER_IMPORT_FILE_SCREEN_TIP;
			aggregatedImportFileButton.SuperTip = Const.Content.ANALAIZER_IMPORT_FILE_SUPER_TIP;

			oldAggregatedImportFolderButton.Description = Const.Content.OLD_ANALAIZER_IMPORT_FOLDER_DESCRIPTION;
			oldAggregatedImportFolderButton.Label = Const.Content.OLD_ANALAIZER_IMPORT_FOLDER_LABEL;
			oldAggregatedImportFolderButton.OfficeImageId = Const.Content.OLD_ANALAIZER_IMPORT_FOLDER_OFFICE_IMAGE_ID;
			oldAggregatedImportFolderButton.ScreenTip = Const.Content.OLD_ANALAIZER_IMPORT_FOLDER_SCREEN_TIP;
			oldAggregatedImportFolderButton.SuperTip = Const.Content.OLD_ANALAIZER_IMPORT_FOLDER_SUPER_TIP;

			oldAggregatedImportFileButton.Description = Const.Content.OLD_ANALAIZER_IMPORT_FILE_DESCRIPTION;
			oldAggregatedImportFileButton.Label = Const.Content.OLD_ANALAIZER_IMPORT_FILE_LABEL;
			oldAggregatedImportFileButton.OfficeImageId = Const.Content.OLD_ANALAIZER_IMPORT_FILE_OFFICE_IMAGE_ID;
			oldAggregatedImportFileButton.ScreenTip = Const.Content.OLD_ANALAIZER_IMPORT_FILE_SCREEN_TIP;
			oldAggregatedImportFileButton.SuperTip = Const.Content.OLD_ANALAIZER_IMPORT_FILE_SUPER_TIP;


			aggregatedTableViewerButton.Description = Const.Content.ANALAIZER_TABLE_DESCRIPTION;
			aggregatedTableViewerButton.Label = Const.Content.ANALAIZER_TABLE_LABEL;
			aggregatedTableViewerButton.OfficeImageId = Const.Content.ANALAIZER_TABLE_OFFICE_IMAGE_ID;
			aggregatedTableViewerButton.ScreenTip = Const.Content.ANALAIZER_TABLE_SCREEN_TIP;
			aggregatedTableViewerButton.SuperTip = Const.Content.ANALAIZER_TABLE_SUPER_TIP;

			aggregatedDialogButton.Description = Const.Content.ANALAIZER_DIALOG_DESCRIPTION;
			aggregatedDialogButton.Label = Const.Content.ANALAIZER_DIALOG_LABEL;
			aggregatedDialogButton.OfficeImageId = Const.Content.ANALAIZER_DIALOG_OFFICE_IMAGE_ID;
			aggregatedDialogButton.ScreenTip = Const.Content.ANALAIZER_DIALOG_SCREEN_TIP;
			aggregatedDialogButton.SuperTip = Const.Content.ANALAIZER_DIALOG_SUPER_TIP;

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

			searchServiceButton.Description = Const.Content.SEARCH_SERVICE_DESCRIPTION;
			searchServiceButton.Label = Const.Content.SEARCH_SERVICE_LABEL;
			searchServiceButton.OfficeImageId = Const.Content.SEARCH_SERVICE_OFFICE_IMAGE_ID;
			searchServiceButton.ScreenTip = Const.Content.SEARCH_SERVICE_SCREEN_TIP;
			searchServiceButton.SuperTip = Const.Content.SEARCH_SERVICE_SUPER_TIP;

			aiServiceButton.Description = Const.Content.AI_SERVICE_DESCRIPTION;
			aiServiceButton.Label = Const.Content.AI_SERVICE_LABEL;
			aiServiceButton.OfficeImageId = Const.Content.AI_SERVICE_OFFICE_IMAGE_ID;
			aiServiceButton.ScreenTip = Const.Content.AI_SERVICE_SCREEN_TIP;
			aiServiceButton.SuperTip = Const.Content.AI_SERVICE_SUPER_TIP;
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
			Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl = this.Factory.CreateRibbonDialogLauncher();

			this.wordHiddenPowersTab = this.Factory.CreateRibbonTab();
			this.maketGroup = this.Factory.CreateRibbonGroup();
			this.newDataButton = this.Factory.CreateRibbonButton();
			this.openDataButton = this.Factory.CreateRibbonButton();
			this.saveDataButton = this.Factory.CreateRibbonButton();
			this.separator1 = this.Factory.CreateRibbonSeparator();
			this.deleteDataButton = this.Factory.CreateRibbonButton();
			this.separator2 = this.Factory.CreateRibbonSeparator();
			this.editCategoriesButton = this.Factory.CreateRibbonButton();
			this.createTableButton = this.Factory.CreateRibbonButton();
			this.editDocumentKeysButton = this.Factory.CreateRibbonButton();
			this.aggregatedGroup = this.Factory.CreateRibbonGroup();
			this.aggregatedImportFolderButton = this.Factory.CreateRibbonSplitButton();
			this.aggregatedImportFileButton = this.Factory.CreateRibbonButton();
			this.separator5 = this.Factory.CreateRibbonSeparator();
			this.oldAggregatedImportFolderButton = this.Factory.CreateRibbonButton();
			this.oldAggregatedImportFileButton = this.Factory.CreateRibbonButton();
			this.aggregatedTableViewerButton = this.Factory.CreateRibbonButton();
			this.aggregatedDialogButton = this.Factory.CreateRibbonButton();
			this.notesGroup = this.Factory.CreateRibbonGroup();
			this.addLastNoteTypeButton = this.Factory.CreateRibbonSplitButton();
			this.addTextNoteButton = this.Factory.CreateRibbonButton();
			this.addDecimalNoteButton = this.Factory.CreateRibbonButton();
			this.separator4 = this.Factory.CreateRibbonSeparator();
			this.searchServiceButton = this.Factory.CreateRibbonButton();
			this.aiServiceButton = this.Factory.CreateRibbonButton();
			this.editTableButton = this.Factory.CreateRibbonButton();
			this.separator3 = this.Factory.CreateRibbonSeparator();
			this.paneVisibleButton = this.Factory.CreateRibbonToggleButton();
			this.wordHiddenPowersTab.SuspendLayout();
			this.maketGroup.SuspendLayout();
			this.aggregatedGroup.SuspendLayout();
			this.notesGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// WordHiddenPowersTab
			// 
			this.wordHiddenPowersTab.Groups.Add(this.maketGroup);
			this.wordHiddenPowersTab.Groups.Add(this.aggregatedGroup);
			this.wordHiddenPowersTab.Groups.Add(this.notesGroup);
			this.wordHiddenPowersTab.Label = "Дополнительные данные";
			this.wordHiddenPowersTab.Name = "WordHiddenPowersTab";
			this.wordHiddenPowersTab.Position = this.Factory.RibbonPosition.AfterOfficeId("TabReferences");
			// 
			// maketGroup
			// 
			this.maketGroup.Items.Add(this.newDataButton);
			this.maketGroup.Items.Add(this.openDataButton);
			this.maketGroup.Items.Add(this.saveDataButton);
			this.maketGroup.Items.Add(this.separator1);
			this.maketGroup.Items.Add(this.deleteDataButton);
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
			this.newDataButton.Label = "";
			this.newDataButton.Name = "newDataButton";
			this.newDataButton.ShowImage = true;
			this.newDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.NewContent_Click);
			// 
			// openDataButton
			// 
			this.openDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.openDataButton.Label = "";
			this.openDataButton.Name = "openDataButton";
			this.openDataButton.ShowImage = true;
			this.openDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OpenContent_Click);
			// 
			// saveDataButton
			// 
			this.saveDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.saveDataButton.Label = "";
			this.saveDataButton.Name = "saveDataButton";
			this.saveDataButton.ShowImage = true;
			this.saveDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SaveContent_Click);
			// 
			// separator1
			// 
			this.separator1.Name = "separator1";
			// 
			// deleteDataButton
			// 
			this.deleteDataButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.deleteDataButton.Label = "";
			this.deleteDataButton.Name = "deleteDataButton";
			this.deleteDataButton.ShowImage = true;
			this.deleteDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DeleteContent_Click);
			// 
			// separator2
			// 
			this.separator2.Name = "separator2";
			// 
			// editCategoriesButton
			// 
			this.editCategoriesButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.editCategoriesButton.Label = "";
			this.editCategoriesButton.Name = "editCategoriesButton";
			this.editCategoriesButton.ShowImage = true;
			this.editCategoriesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.EditCategories_Click);
			// 
			// createTableButton
			// 
			this.createTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.createTableButton.Label = "";
			this.createTableButton.Name = "createTableButton";
			this.createTableButton.ShowImage = true;
			this.createTableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CreateTable_Click);
			// 
			// editDocumentKeysButton
			// 
			this.editDocumentKeysButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.editDocumentKeysButton.Label = "";
			this.editDocumentKeysButton.Name = "editDocumentKeysButton";
			this.editDocumentKeysButton.ShowImage = true;
			this.editDocumentKeysButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.EditDocumentKeys_Click);
			// 
			// aggregatedGroup
			// 
			this.aggregatedGroup.Items.Add(this.aggregatedImportFolderButton);
			this.aggregatedGroup.Items.Add(this.aggregatedTableViewerButton);
			this.aggregatedGroup.Items.Add(this.aggregatedDialogButton);
			this.aggregatedGroup.Label = "Анализ данных";
			this.aggregatedGroup.Name = "aggregatedGroup";
			// 
			// aggregatedImportFoldersButton
			// 
			this.aggregatedImportFolderButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.aggregatedImportFolderButton.Items.Add(this.aggregatedImportFileButton);
			this.aggregatedImportFolderButton.Items.Add(this.separator5);
			this.aggregatedImportFolderButton.Items.Add(this.oldAggregatedImportFolderButton);
			this.aggregatedImportFolderButton.Items.Add(this.oldAggregatedImportFileButton);
			this.aggregatedImportFolderButton.Label = "";
			this.aggregatedImportFolderButton.Name = "aggregatedImportFolderButton";
			this.aggregatedImportFolderButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerImportFolder_Click);
			// 
			// aggregatedImportFileButton
			// 
			this.aggregatedImportFileButton.Label = "";
			this.aggregatedImportFileButton.Name = "aggregatedImportFileButton";
			this.aggregatedImportFileButton.ShowImage = true;
			this.aggregatedImportFileButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerImportFile_Click);
			// 
			// oldAggregatedImportFolderButton
			// 
			this.oldAggregatedImportFolderButton.Label = "";
			this.oldAggregatedImportFolderButton.Name = "oldAggregatedImportFolderButton";
			this.oldAggregatedImportFolderButton.ShowImage = true;
			this.oldAggregatedImportFolderButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerImportOldFolder_Click);
			// 
			// oldAggregatedImportFileButton
			// 
			this.oldAggregatedImportFileButton.Label = "";
			this.oldAggregatedImportFileButton.Name = "oldAggregatedImportFileButton";
			this.oldAggregatedImportFileButton.ShowImage = true;
			this.oldAggregatedImportFileButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerImportOldFile_Click);
			// 
			// aggregatedTableViewerButton
			// 
			this.aggregatedTableViewerButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.aggregatedTableViewerButton.Label = "";
			this.aggregatedTableViewerButton.Name = "aggregatedTableViewerButton";
			this.aggregatedTableViewerButton.ShowImage = true;
			this.aggregatedTableViewerButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerTableViewer_Click);
			// 
			// aggregatedDialogButton
			// 
			this.aggregatedDialogButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.aggregatedDialogButton.Label = "";
			this.aggregatedDialogButton.Name = "aggregatedDialogButton";
			this.aggregatedDialogButton.ShowImage = true;
			this.aggregatedDialogButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AnalizerDialog_Click);
			// 
			// notesGroup
			// 
			this.notesGroup.DialogLauncher = ribbonDialogLauncherImpl;
			this.notesGroup.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.NotesGroup_DialogLauncherClick);
			this.notesGroup.Items.Add(this.addLastNoteTypeButton);
			this.notesGroup.Items.Add(this.editTableButton);
			this.notesGroup.Items.Add(this.separator3);
			this.notesGroup.Items.Add(this.paneVisibleButton);
			this.notesGroup.Label = "Ввод данных";
			this.notesGroup.Name = "notesGroup";
			// 
			// addLastNoteTypeButton
			// 
			this.addLastNoteTypeButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.addLastNoteTypeButton.Items.Add(this.addTextNoteButton);
			this.addLastNoteTypeButton.Items.Add(this.addDecimalNoteButton);
			this.addLastNoteTypeButton.Items.Add(this.separator4);
			this.addLastNoteTypeButton.Items.Add(this.searchServiceButton);
			this.addLastNoteTypeButton.Items.Add(this.aiServiceButton);
			this.addLastNoteTypeButton.Label = "";
			this.addLastNoteTypeButton.Name = "addLastNoteTypeButton";
			this.addLastNoteTypeButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddLastNoteType_Click);
			// 
			// addTextNoteButton
			// 
			this.addTextNoteButton.Label = "";
			this.addTextNoteButton.Name = "addTextNoteButton";
			this.addTextNoteButton.ShowImage = true;
			this.addTextNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddTextNote_Click);
			// 
			// addDecimalNoteButton
			// 
			this.addDecimalNoteButton.Label = "";
			this.addDecimalNoteButton.Name = "addDecimalNoteButton";
			this.addDecimalNoteButton.ShowImage = true;
			this.addDecimalNoteButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddDecimalNote_Click);
			// 
			// separator4
			// 
			this.separator4.Name = "separator4";
			// 
			// searchServiceButton
			// 
			this.searchServiceButton.Label = "";
			this.searchServiceButton.Name = "searchServiceButton";
			this.searchServiceButton.ShowImage = true;
			this.searchServiceButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SearchService_Click);
			// 
			// aiServiceButton
			// 
			this.aiServiceButton.Label = "";
			this.aiServiceButton.Name = "aiServiceButton";
			this.aiServiceButton.ShowImage = true;
			this.aiServiceButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AiService_Click);
			// 
			// editTableButton
			// 
			this.editTableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.editTableButton.Label = "";
			this.editTableButton.Name = "editTableButton";
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
			this.paneVisibleButton.Label = "";
			this.paneVisibleButton.Name = "paneVisibleButton";
			this.paneVisibleButton.ShowImage = true;
			// 
			// AddInMainRibbon
			// 
			this.Name = "AddInMainRibbon";
			this.RibbonType = "Microsoft.Word.Document";
			this.Tabs.Add(this.wordHiddenPowersTab);
			this.wordHiddenPowersTab.ResumeLayout(false);
			this.wordHiddenPowersTab.PerformLayout();
			this.maketGroup.ResumeLayout(false);
			this.maketGroup.PerformLayout();
			this.aggregatedGroup.ResumeLayout(false);
			this.aggregatedGroup.PerformLayout();
			this.notesGroup.ResumeLayout(false);
			this.notesGroup.PerformLayout();
			this.ResumeLayout(false);

		}

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab wordHiddenPowersTab;
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
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator3;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator4;
		internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator5;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton editCategoriesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton editDocumentKeysButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup aggregatedGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton aggregatedImportFolderButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton aggregatedImportFileButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton oldAggregatedImportFolderButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton oldAggregatedImportFileButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton aggregatedTableViewerButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton aggregatedDialogButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup notesGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton addLastNoteTypeButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addTextNoteButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addDecimalNoteButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton searchServiceButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton aiServiceButton;
	}

    partial class ThisRibbonCollection
    {
        internal AddInMainRibbon WordHiddenPowersRibbon
        {
            get { return this.GetRibbon<AddInMainRibbon>(); }
        }
    }
}
