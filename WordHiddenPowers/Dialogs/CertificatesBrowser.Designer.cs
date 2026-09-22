namespace WordHiddenPowers.Dialogs
{
	partial class CertificatesBrowser
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificatesBrowser));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.browserListView = new System.Windows.Forms.ListView();
			this.columnSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cancelButton.Location = new System.Drawing.Point(417, 312);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 27);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.okButton.Location = new System.Drawing.Point(312, 312);
			this.okButton.Margin = new System.Windows.Forms.Padding(4);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(100, 27);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "&ОК";
			// 
			// browserListView
			// 
			this.browserListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.browserListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSubject,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
			this.browserListView.FullRowSelect = true;
			this.browserListView.HideSelection = false;
			this.browserListView.LargeImageList = this.imageList1;
			this.browserListView.Location = new System.Drawing.Point(12, 12);
			this.browserListView.MultiSelect = false;
			this.browserListView.Name = "browserListView";
			this.browserListView.Size = new System.Drawing.Size(504, 288);
			this.browserListView.SmallImageList = this.imageList1;
			this.browserListView.TabIndex = 0;
			this.browserListView.UseCompatibleStateImageBehavior = false;
			this.browserListView.View = System.Windows.Forms.View.Details;
			this.browserListView.SelectedIndexChanged += new System.EventHandler(this.BrowserListView_SelectedIndexChanged);
			// 
			// columnSubject
			// 
			this.columnSubject.Text = "Кому выдан";
			this.columnSubject.Width = 160;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Кем выдан";
			this.columnHeader3.Width = 160;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Срок действия";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Имя";
			this.columnHeader6.Width = 200;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Certificate");
			this.imageList1.Images.SetKeyName(1, "CertificateWithKey");
			// 
			// CertificatesBrowser
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(525, 355);
			this.Controls.Add(this.browserListView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CertificatesBrowser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CertificatesBrowser";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ListView browserListView;
		private System.Windows.Forms.ColumnHeader columnSubject;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ImageList imageList1;
	}
}