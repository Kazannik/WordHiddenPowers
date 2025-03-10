using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Controls.ListControls.CategoriesListControl;
using WordHiddenPowers.Repositories.Categories;
using WordHiddenPowers.Utils;


namespace WordHiddenPowers.Dialogs
{
	public partial class CategoriesEditorDialog : Form
	{
		private readonly Documents.Document document;

		public CategoriesEditorDialog(Documents.Document document)
		{			
			this.document = document;

			this.Icon = WordUtil.GetIconMso("MindMapExportWord", SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);

			InitializeComponent();
			ControlResize();

			listBox.DataSet = this.document.CurrentDataSet;
			listBox.SelectedItemChanged += new EventHandler<ControlLibrary.Controls.ListControls.ItemEventArgs<ListItem>>(ListBox_SelectedItemChanged);
			if (listBox.Items.Count > 0) listBox.SelectedIndex = 0;
		}

		private void ListBox_SelectedItemChanged(object sender, ControlLibrary.Controls.ListControls.ItemEventArgs<ListItem> e)
		{
			captionTextBox.TextChanged -= this.Controls_ValuesChanged;
			descriptionTextBox.TextChanged -= this.Controls_ValuesChanged;
			obligatoryСheckBox.CheckedChanged -= this.Controls_ValuesChanged;
			isDecimalRadioButton.CheckedChanged -= this.Controls_ValuesChanged;
			isTextRadioButton.CheckedChanged -= this.Controls_ValuesChanged;
			beforeTextBox.TextChanged -= this.Controls_ValuesChanged;
			afterTextBox.TextChanged -= this.Controls_ValuesChanged;
			keywordsTextBox.TextChanged -= this.Controls_ValuesChanged;

			if (e.Item != null && e.Item.owner is Category)
			{
				isTextRadioButton.Enabled = false;
				isDecimalRadioButton.Enabled = false;
				keywordsLabel.Enabled = false;
				keywordsTextBox.Enabled = false;

				Category item = e.Item.owner as Category;

				captionTextBox.Text = item.Caption;
				descriptionTextBox.Text = item.Description;
				obligatoryСheckBox.Checked = item.IsObligatory;

				beforeTextBox.Text = item.BeforeText;
				afterTextBox.Text = item.AfterText;

				captionTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				descriptionTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				beforeTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				afterTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
			}
			else if (e.Item != null && e.Item.owner is Subcategory)
			{
				isTextRadioButton.Enabled = true;
				isDecimalRadioButton.Enabled = true;
				keywordsLabel.Enabled = true;
				keywordsTextBox.Enabled = true;

				Subcategory item = e.Item.owner as Subcategory;

				captionTextBox.Text = item.Caption;
				descriptionTextBox.Text = item.Description;
				obligatoryСheckBox.Checked = item.IsObligatory;
				
				isDecimalRadioButton.Checked = item.IsDecimal;
				isTextRadioButton.Checked = item.IsText;
				keywordsTextBox.Text = item.Keywords;

				beforeTextBox.Text = item.BeforeText;
				afterTextBox.Text = item.AfterText;

				captionTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				descriptionTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				obligatoryСheckBox.CheckedChanged += new EventHandler(this.Controls_ValuesChanged);
				isDecimalRadioButton.CheckedChanged += new EventHandler(this.Controls_ValuesChanged);
				isTextRadioButton.CheckedChanged += new EventHandler(this.Controls_ValuesChanged);
				beforeTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				afterTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
				keywordsTextBox.TextChanged += new EventHandler(this.Controls_ValuesChanged);
			}
		}
		
		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = "Текстовый документ (.txt)|*.txt",
				Multiselect = false
			};
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				string content = File.ReadAllText(dialog.FileName, Encoding.GetEncoding(1251));				
				Cursor.Current = Cursors.WaitCursor;
				listBox.BeginUpdate();
				CategoriesUtil.CreateFromText(document.CurrentDataSet, content);
				listBox.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{

		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{

		}

		private void CategoriesEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				foreach (ListItem item in listBox.Items)
				{
					if (item.owner is Category)
					{
						Category category = item.owner as Category;
						document.CurrentDataSet.Write(category);
					}
					else if (item.owner is Subcategory)
					{
						Subcategory subcategory = item.owner as Subcategory;
						document.CurrentDataSet.Write(subcategory);
					}
				}
								
				if (document.CurrentDataSet.HasChanges())
				{
					DialogResult result = MessageBox.Show("Зафиксировать все изменения?", "Категории данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						Cursor.Current = Cursors.WaitCursor;
						listBox.DataSet = null;
						document.CurrentDataSet.Categories.AcceptChanges();
						document.CurrentDataSet.Subcategories.AcceptChanges();
						ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_CURRENT_VARIABLE_NAME, document.CurrentDataSet);
						Cursor.Current = Cursors.Default;
					}
					else if (result == DialogResult.No)
					{
						Cursor.Current = Cursors.WaitCursor;
						listBox.DataSet = null;
						document.CurrentDataSet.Categories.RejectChanges();
						document.CurrentDataSet.Subcategories.RejectChanges();
						Cursor.Current = Cursors.Default;
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}					
				}
			}
		}
		
		private void Controls_ValuesChanged(object sender, EventArgs e)
		{
			ListItem item = (ListItem)listBox.SelectedItem;
			if (item != null && item.owner is Category)
			{
				Category category = item.owner as Category;

				category.Caption = captionTextBox.Text;
				category.Description = descriptionTextBox.Text;
				category.IsObligatory = obligatoryСheckBox.Checked;

				category.BeforeText = beforeTextBox.Text;
				category.AfterText = afterTextBox.Text;

				((ListItemNote)item[0]).Text = captionTextBox.Text;
				((ListItemNote)item[1]).Text = descriptionTextBox.Text;
				int index = listBox.Items.IndexOf(item);
				Rectangle rectangle = listBox.GetItemRectangle(index);
				listBox.Invalidate(rectangle);
			}
			if (item != null && item.owner is Subcategory)
			{				
				Subcategory subcategory = item.owner as Subcategory;

				subcategory.Caption = captionTextBox.Text;
				subcategory.Description = descriptionTextBox.Text;
				subcategory.IsObligatory = obligatoryСheckBox.Checked;

				subcategory.IsDecimal = isDecimalRadioButton.Checked;
				subcategory.IsText = isTextRadioButton.Checked;
				subcategory.Keywords = keywordsTextBox.Text;

				subcategory.BeforeText = beforeTextBox.Text;
				subcategory.AfterText = afterTextBox.Text;

				((ListItemNote)item[0]).Text = captionTextBox.Text;
				((ListItemNote)item[1]).Text = descriptionTextBox.Text;
				int index = listBox.Items.IndexOf(item);
				Rectangle rectangle = listBox.GetItemRectangle(index);
				listBox.Invalidate(rectangle);
			}
		}

		
		private void AddCategory_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedIndices.Count == 0)
			{
				string guid = this.document.CurrentDataSet.Add(Category.Create(
					position: 0,
					caption: "Новая категория", 
					description: "Пояснение", 
					isObligatory: false,
					beforeText: "Текст до...", 
					afterText: "Текст после...")).Guid;
				listBox.SelectedItem = listBox.GetListItem(guid: guid);
			}
			else
			{
				if (((ListItem)listBox.SelectedItem).owner is Category)
				{
					int id = listBox.Items.IndexOf(listBox.SelectedItem);
					int nextId = listBox.CategoryNextIndexOf(id + 1);
					ListItem item = listBox.Items[nextId];
					this.document.CurrentDataSet.Insert(((Category)item.owner).Position, Category.Create(
						guid: ((Category)item.owner).Guid,
						position: 0,
						caption: "Новая категория",
						description: "Пояснение",
						isObligatory: false,
						beforeText: "Текст до...",
						afterText: "Текст после..."));
				}
			}
		}

		
		private void AddSubcategory_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedItem is ListItem item && item.owner is Category category)
			{
				this.document.CurrentDataSet.Add(category, Subcategory.Create(
					category: category,
					position: 0,
					caption: "Новая подкатегория",
					description: "Пояснение",
					isDecimal: false,
					isText: true,
					isObligatory: true,
					beforeText: "Текст до...",
					afterText: "Текст после...",
					keywords: "Keywords"));
			}
		}

		private void RemoveCategory_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedItem is ListItem item && item.owner is Category)
			{
				this.document.CurrentDataSet.Remove(item.owner.Code.Guid);
			}
		}
			

		private void RemoveSubcategory_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedItem is ListItem item && item.owner is Subcategory)
			{
				this.document.CurrentDataSet.Remove(item.owner.Code.Guid);
			}
		}

		private void Clear_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			listBox.BeginUpdate();
			this.document.CurrentDataSet.Subcategories.Clear();
			this.document.CurrentDataSet.Categories.Clear();
			listBox.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void Containers_Resize(object sender, EventArgs e)
		{
			ControlResize();
		}

		private void ControlResize()
		{
			tabControl1.Location = new Point(0, 0);
			tabControl1.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);

			/// Tab Page 1
			captionLabel.Location = new Point(0, 0);
			obligatoryСheckBox.Location = new Point(0, tabPage1.Height - obligatoryСheckBox.Height);
			descriptionLabel.Location = new Point(0, (tabPage1.Height - captionLabel.Height - descriptionLabel.Height - obligatoryСheckBox.Height) / 2);
			captionTextBox.Location = new Point(0, captionLabel.Height);
			captionTextBox.Size = new Size(tabPage1.Width, descriptionLabel.Top - captionTextBox.Top);
			descriptionTextBox.Location = new Point(0, descriptionLabel.Top + descriptionLabel.Height);
			descriptionTextBox.Size = new Size(tabPage1.Width, tabPage1.Height - descriptionTextBox.Top - obligatoryСheckBox.Height);

			/// Tab Page 2
			isTextRadioButton.Location = new Point(12, 12);
			isDecimalRadioButton.Location = new Point(12, isTextRadioButton.Top + isTextRadioButton.Height + 12);
			keywordsLabel.Location = new Point(0, isDecimalRadioButton.Top + isDecimalRadioButton.Height + 12);
			keywordsTextBox.Location = new Point(0, keywordsLabel.Top + keywordsLabel.Height);
			keywordsTextBox.Size = new Size(tabPage2.Width, tabPage2.Height - keywordsTextBox.Top);

			/// Tab Page 3
			beforeLabel.Location = new Point(0, 0);
			afterLabel.Location = new Point(0, (tabPage3.Height - beforeLabel.Height - afterLabel.Height) / 2);
			beforeTextBox.Location = new Point(0, beforeLabel.Height);
			beforeTextBox.Size = new Size(tabPage3.Width, afterLabel.Top - beforeLabel.Height);
			afterTextBox.Location = new Point(0, afterLabel.Top + afterLabel.Height);
			afterTextBox.Size = new Size(tabPage3.Width, tabPage3.Height - afterTextBox.Top);
		}

		
	}
}
