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

			listBox.DataSet = this.document.DataSet;
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
				CategoriesUtil.CreateFromText(document.DataSet, content);
				listBox.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
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
						document.DataSet.Categories.Write(category);
					}
					else if (item.owner is Subcategory)
					{
						Subcategory subcategory = item.owner as Subcategory;
						document.DataSet.Subcategories.Write(subcategory);
					}
				}
								
				if (document.DataSet.HasChanges())
				{
					DialogResult result = MessageBox.Show("Зафиксировать все изменения?", "Категории данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						Cursor.Current = Cursors.WaitCursor;
						listBox.DataSet = null;
						document.DataSet.Categories.AcceptChanges();
						document.DataSet.Subcategories.AcceptChanges();
						ContentUtil.CommitVariable(document.Doc.Variables, Const.Globals.XML_VARIABLE_NAME, document.DataSet);
						Cursor.Current = Cursors.Default;
					}
					else if (result == DialogResult.No)
					{
						Cursor.Current = Cursors.WaitCursor;
						listBox.DataSet = null;
						document.DataSet.Categories.RejectChanges();
						document.DataSet.Subcategories.RejectChanges();
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
				string guid = this.document.DataSet.Categories.Add(Category.Create(
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
					this.document.DataSet.Insert(((Category)item.owner).Position, Category.Create(
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
				this.document.DataSet.Subcategories.Add(category, Subcategory.Create(
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
				this.document.DataSet.Categories.Remove(item.owner.Code.Guid);
			}
		}
			

		private void RemoveSubcategory_Click(object sender, EventArgs e)
		{
			if (listBox.SelectedItem is ListItem item && item.owner is Subcategory)
			{
				this.document.DataSet.Subcategories.Remove(item.owner.Code.Guid);
			}
		}

		private void Clear_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			listBox.BeginUpdate();
			this.document.DataSet.Subcategories.Clear();
			this.document.DataSet.Categories.Clear();
			listBox.EndUpdate();
			Cursor.Current = Cursors.Default;
		}		
	}
}
