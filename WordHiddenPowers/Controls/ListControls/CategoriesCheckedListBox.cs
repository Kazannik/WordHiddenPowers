using ControlLibrary.Controls.ListControls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Categories;
using static WordHiddenPowers.Controls.ListControls.CategoriesListBox;
using static WordHiddenPowers.Repositories.RepositoryDataSet;
using Category = WordHiddenPowers.Repositories.Categories.Category;
using Control = WordHiddenPowers.Controls.ListControls.CategoriesCheckedListControl;
using Font = System.Drawing.Font;
using ListItem = WordHiddenPowers.Controls.ListControls.CategoriesCheckedListControl.ListItem;
using Rectangle = System.Drawing.Rectangle;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(CheckedListBox))]
	[ComVisible(false)]
	public class CategoriesCheckedListBox : ListControl<ListItem, Control.ListItemNote>
	{
		private RepositoryDataSet source;

		public CategoriesCheckedListBox() : base() { }

		public RepositoryDataSet DataSet
		{
			get
			{
				return source;
			}
			set
			{
				if (source != value)
				{
					if (source != null)
					{
						SuspendEvents();
					}

					source = value;
					ReadData();
				}
			}
		}

		public bool IsAnyChecked
		{
			get
			{
				return Items.Any(i => i.IsChecked);
			}
		}

		public IEnumerable<Subcategory> CheckedSubcategories
		{
			get
			{
				return Items.Where(i => !i.IsCategory && i.IsChecked).Select(i => (Subcategory)i.owner);
			}
		}

		protected override void OnItemMouseClick(ItemMouseEventArgs<ListItem, Control.ListItemNote> e)
		{
			if (e.Button == MouseButtons.Left && !e.Item.IsCategory)
			{
				e.Item.IsChecked = !e.Item.IsChecked;
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == 32 && SelectedItem != null && !((ListItem)SelectedItem).IsCategory)
			{
				((ListItem)SelectedItem).IsChecked = !((ListItem)SelectedItem).IsChecked;
			}
			base.OnKeyPress(e);
		}

		private void Categories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Categories_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			CategoriesRow row = e.Row as CategoriesRow;
			IEnumerable<SubcategoriesRow> subcategories = DataSet.Subcategories.GetSubcategoriesRows(row.key_guid);
			if (subcategories.Any())
			{
				foreach (SubcategoriesRow refRow in subcategories)
				{
					refRow.Delete();
				}
			}
			if (ExistsListItem(e.Row))
			{
				ListItem item = GetListItem(e.Row);
				Items.Remove(item);
			}
		}

		private void Subcategories_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			if (ExistsListItem(e.Row))
			{
				ListItem item = GetListItem(e.Row);
				Items.Remove(item);
			}
		}

		private void Categories_RowChanged(object sender, CategoriesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Add(Category.Create(e.Row));
			}
			else if (e.Action == DataRowAction.Change)
			{
				if (ExistsListItem(e.Row))
				{
					ListItem item = GetListItem(e.Row);
					((Control.ListItemNote)item[0]).Text = e.Row.Caption;
					((Control.ListItemNote)item[1]).Text = e.Row.Description;
					int index = Items.IndexOf(item);
					Rectangle rectangle = GetItemRectangle(index);
					Invalidate(rectangle);
				}
			}
		}

		private void Subcategories_RowChanged(object sender, SubcategoriesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				if (DataSet.Categories.Exists(e.Row.category_guid))
				{
					Category category = DataSet.GetCategory(e.Row.category_guid);
					Add(Subcategory.Create(category, e.Row));
				}
			}
			else if (e.Action == DataRowAction.Change)
			{
				if (ExistsListItem(e.Row))
				{
					ListItem item = GetListItem(e.Row);
					((Control.ListItemNote)item[0]).Text = e.Row.Caption;
					((Control.ListItemNote)item[1]).Text = e.Row.Description;
					int index = Items.IndexOf(item);
					Rectangle rectangle = GetItemRectangle(index);
					Invalidate(rectangle);
				}
			}
		}

		private void SuspendEvents()
		{
			if (DataSet != null)
			{
				DataSet.Categories.CategoriesRowChanged -= Categories_RowChanged;
				DataSet.Categories.TableCleared -= Categories_TableCleared;
				DataSet.Categories.RowDeleting -= Categories_RowDeleting;

				DataSet.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
				DataSet.Subcategories.TableCleared -= Subcategories_TableCleared;
				DataSet.Subcategories.RowDeleting -= Subcategories_RowDeleting;
			}
		}

		private void ResumeEvents()
		{
			if (DataSet != null)
			{
				DataSet.Categories.CategoriesRowChanged += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				DataSet.Categories.TableCleared += new DataTableClearEventHandler(Categories_TableCleared);
				DataSet.Categories.RowDeleting += new DataRowChangeEventHandler(Categories_RowDeleting);

				DataSet.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				DataSet.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);
				DataSet.Subcategories.RowDeleting += new DataRowChangeEventHandler(Subcategories_RowDeleting);
			}
		}

		public void ReadData()
		{
			if (DesignMode || DataSet == null) return;

			SuspendEvents();

			BeginUpdate();

			Items.Clear();

			if (DataSet.Categories.Rows.Count > 0)
			{
				foreach (DataRow row in DataSet.Categories.Rows)
				{
					Category category = Category.Create(row);
					Add(category);
				}
			}

			if (Items.Count > 0 && DataSet.Subcategories.Rows.Count > 0)
			{
				foreach (DataRow row in DataSet.Subcategories.Rows)
				{
					Category category = DataSet.GetCategory(row["category_guid"] as string);
					Subcategory subcategory = Subcategory.Create(category: category, dataRow: row);
					ListItem item = GetListItem(category);
					int index = GetLastItemIndex(item);
					Insert(index, subcategory);
				}
			}
			EndUpdate();
			ResumeEvents();
		}

		private int GetLastItemIndex(ListItem categoryItem)
		{
			int index = Items.IndexOf(categoryItem);

			ICategoriesListItem category = categoryItem.owner;
			do
			{
				index++;
			}
			while (index < Items.Count && Items[index].owner.Code.Major == category.Code.Major);

			return index;
		}

		private void Add(ICategoriesListItem item)
		{
			Items.Add(new ListItem(item));
		}

		private void Insert(int index, ICategoriesListItem item)
		{
			Items.Insert(index, new ListItem(item));
		}

		private void Remove(Category category)
		{
			if (ExistsListItem(category))
			{
				ListItem item = GetListItem(category);
				Items.Remove(item);
			}
		}

		private void Remove(Subcategory subcategory)
		{
			if (ExistsListItem(subcategory))
			{
				ListItem item = GetListItem(subcategory);
				Items.Remove(item);
			}
		}

		public bool ExistsListItem(string guid)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == guid).Any();
		}

		public ListItem GetListItem(string guid)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == guid).First();
		}

		public int CategoryNextIndexOf(int index)
		{
			for (int i = index; i < Items.Count; i++)
			{
				ListItem item = Items[i];
				if (item.owner is Category)
					return i;
			}
			return -1;
		}

		private bool ExistsListItem(Category category)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == category.Guid).Any();
		}

		private ListItem GetListItem(Category category)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == category.Guid).First();
		}

		private bool ExistsListItem(Subcategory subcategory)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == subcategory.Guid).Any();
		}

		private ListItem GetListItem(Subcategory subcategory)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == subcategory.Guid).First();
		}

		private bool ExistsListItem(CategoriesRow row)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == row.key_guid).Any();
		}

		private ListItem GetListItem(CategoriesRow row)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == row.key_guid).First();
		}

		private bool ExistsListItem(SubcategoriesRow row)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == row.key_guid).Any();
		}

		private ListItem GetListItem(SubcategoriesRow row)
		{
			return Items.AsEnumerable()
				.Where(x => x.owner.Code.Guid == row.key_guid).First();
		}

		private bool ExistsListItem(DataRow row)
		{
			if (row.Table is CategoriesDataTable)
			{
				return ExistsListItem(row as CategoriesRow);
			}
			else
			{
				return ExistsListItem(row as SubcategoriesRow);
			}
		}

		private ListItem GetListItem(DataRow row)
		{
			if (row.Table is CategoriesDataTable)
			{
				return GetListItem(row as CategoriesRow);
			}
			else
			{
				return GetListItem(row as SubcategoriesRow);
			}
		}
	}

	namespace CategoriesCheckedListControl
	{
		public class ListItem : ControlLibrary.Controls.ListControls.ListItem
		{
			protected internal ICategoriesListItem owner;

			public ListItem() : base()
			{
				owner = default;
			}

			public ListItem(ICategoriesListItem owner) : base(
				new ListItemNote[] {
				(owner is Category)
					? new CategoryTitleListItemNote(owner.Code, owner.Caption)
					: new SubcategoryTitleListItemNote(owner.Code, owner.Caption),
				new DescriptionListItemNote(owner.Description)})
			{
				this.owner = owner;
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				DrawItemEventArgs arg = new DrawItemEventArgs(
					graphics: e.Graphics,
					font: e.Font,
					rect: e.Bounds,
					index: e.Index,
					state: DrawItemState.None,
					foreColor: SystemColors.WindowText,
					backColor: SystemColors.Window);
				arg.DrawBackground();
				base.OnDraw(arg);
				if (notes.Length > 2 && (!notes[1].Size.IsEmpty || !notes[2].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Pen linePen = arg.State == (arg.State | DrawItemState.Selected) ? new Pen(arg.ForeColor) : SystemPens.InactiveCaption;
					arg.Graphics.DrawLine(linePen,
					arg.Bounds.X + 7, notes[0].Size.Height - 2,
					arg.Bounds.X + e.Bounds.Width - 15, notes[0].Size.Height - 2);
				}
				arg.DrawFocusRectangle();
			}

			public bool IsCategory => owner is Category;

			public bool IsChecked
			{
				get => ((SubcategoryTitleListItemNote)notes[0]).IsChecked;
				set => ((SubcategoryTitleListItemNote)notes[0]).IsChecked = value;
			}
		}

		public class CategoryListItem : ListItem
		{
			public CategoryListItem() : base()
			{
				owner = default;
			}

			public CategoryListItem(Category category) : base(owner: category)
			{
				owner = category;
			}

			public Category Category => (Category)owner;
		}

		public class SubcategoryListItem : ListItem
		{
			public SubcategoryListItem() : base()
			{
				owner = default;
			}

			public SubcategoryListItem(Subcategory subcategory) : base(owner: subcategory)
			{
				owner = subcategory;
			}

			public Subcategory Category => (Subcategory)owner;
		}

		public abstract class ListItemNote : ControlLibrary.Controls.ListControls.ListItemNote
		{
			protected static readonly StringFormat CENTER_STRING_FORMAT = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			protected static readonly StringFormat LEFT_STRING_FORMAT = new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Near
			};

			private string text;

			public ListItemNote(string text)
			{
				this.text = text;
			}

			public string Text
			{
				get
				{
					return text;
				}
				set
				{
					if (text != value)
					{
						text = value;
						DoContentChanged();
					}
				}
			}
		}

		public class SubcategoryTitleListItemNote : ListItemNote
		{
			private Version code;
			protected Size checkBoxSize;
			private Size codeSize;
			private Size textSize;

			private bool isChecked;
			public Rectangle CheckButton { get; private set; }

			public SubcategoryTitleListItemNote(int major, int minor, string guid, string text) : this(code: Version.Create(major: major, minor: minor, guid: guid), text: text) { }

			public SubcategoryTitleListItemNote(Version code, string text) : base(text: text)
			{
				this.code = code;
				checkBoxSize = Size.Empty;
				codeSize = Size.Empty;
				textSize = Size.Empty;
				CheckButton = Rectangle.Empty;
			}

			public Version Code
			{
				get
				{
					return code;
				}
				set
				{
					if (code != value)
					{
						code = value;
						DoContentChanged();
					}
				}
			}

			public virtual bool IsChecked
			{
				get => isChecked;
				set
				{
					if (isChecked != value)
					{
						isChecked = value;
						DoContentChanged();
					}
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

				if (!checkBoxSize.IsEmpty)
				{
					Rectangle checkBoxRectangle = new Rectangle(
					e.Bounds.X + 2,
					e.Bounds.Y + 2,
					checkBoxSize.Width,
					checkBoxSize.Height);
					DrawCheckBox(new DrawItemEventArgs(e.Graphics, boldFont, checkBoxRectangle, e.Index, e.State, e.ForeColor, e.BackColor));
				}

				Rectangle codeRectangle = new Rectangle(
					e.Bounds.X + checkBoxSize.Width + 3,
					e.Bounds.Y + 2,
					codeSize.Width,
					codeSize.Height);
				Utils.Drawing.DrawCode(Code, new DrawItemEventArgs(e.Graphics, boldFont, codeRectangle, e.Index, e.State, e.ForeColor, e.BackColor));

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle rectangle = new Rectangle(
						e.Bounds.Width - textSize.Width - 1,
						e.Bounds.Y,
						textSize.Width,
						textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, rectangle, LEFT_STRING_FORMAT);
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				codeSize = Utils.Drawing.GetCodeSize(graphics, boldFont);
				checkBoxSize = new Size(codeSize.Height, codeSize.Height);
				textSize = GetTextSize(graphics, Text, font, itemWidth - codeSize.Width - checkBoxSize.Width - 5, CENTER_STRING_FORMAT);

				if (codeSize.Height < textSize.Height)
				{
					return new Size(itemWidth, textSize.Height + 8);
				}
				else
				{
					return new Size(itemWidth, codeSize.Height + 8);
				}
			}

			private void DrawCheckBox(DrawItemEventArgs e)
			{
				CheckButton = new Rectangle(
					e.Bounds.X + 1,
					e.Bounds.Y + 2,
					e.Bounds.Width - 3,
					e.Bounds.Height - 3);

				if (isChecked)
				{
					ControlLibrary.Utils.Drawing.DrawCheckedIcon(e.Graphics, Const.Globals.COLOR_OK_ICON, e.BackColor, CheckButton);
				}
				else
				{
					ControlLibrary.Utils.Drawing.DrawUncheckedIcon(e.Graphics, Const.Globals.COLOR_OK_ICON, e.BackColor, CheckButton);
				}
			}			
		}

		public class CategoryTitleListItemNote : SubcategoryTitleListItemNote
		{
			public CategoryTitleListItemNote(int major, int minor, string guid, string text) : base(major: major, minor: minor, guid: guid, text: text) { }

			public CategoryTitleListItemNote(Version code, string text) : base(code: code, text: text) { }

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Size measureSize = base.OnMeasureBound(graphics, font, itemWidth, itemHeight);
				checkBoxSize = Size.Empty;
				return measureSize;
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				LinearGradientBrush brush;
				if (e.State == (e.State | DrawItemState.Selected))
				{
					brush = new LinearGradientBrush(e.Bounds, Const.Globals.COLOR_1_SELECTED_BACK, Const.Globals.COLOR_2_SELECTED_BACK, LinearGradientMode.ForwardDiagonal);
				}
				else
				{
					brush = new LinearGradientBrush(e.Bounds, Const.Globals.COLOR_1_BACK, Const.Globals.COLOR_2_BACK, LinearGradientMode.ForwardDiagonal);
				}
				e.Graphics.FillRectangle(brush, e.Bounds);

				base.OnDraw(e);
			}

			public override bool IsChecked
			{
				get => false;
				set { }
			}
		}

		public class DescriptionListItemNote : ListItemNote
		{
			public DescriptionListItemNote(string text) : base(text: text) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Brush brush = new SolidBrush(e.ForeColor);
				e.Graphics.DrawString(Text, e.Font, brush, e.Bounds, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Size result = GetTextSize(graphics: graphics, Text, font: font, width: itemWidth, LEFT_STRING_FORMAT);
				return new Size(result.Width, result.Height + 18);
			}
		}
	}
}
