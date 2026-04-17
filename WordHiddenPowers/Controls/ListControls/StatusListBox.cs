using ControlLibrary.Controls.ListControls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Categories;
using static WordHiddenPowers.Controls.ListControls.CategoriesListBox;
using static WordHiddenPowers.Repository.RepositoryDataSet;
using Category = WordHiddenPowers.Repository.Categories.Category;
using Control = WordHiddenPowers.Controls.ListControls.StatusListControl;
using Font = System.Drawing.Font;
using ListItem = WordHiddenPowers.Controls.ListControls.StatusListControl.ListItem;
using Rectangle = System.Drawing.Rectangle;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	[ToolboxBitmap(typeof(CheckedListBox))]
	[ComVisible(false)]
	public class StatusListBox : ListControl<ListItem, Control.ListItemNote>
	{
		private RepositoryDataSet nowSource;
		private RepositoryDataSet lastSource;

		public StatusListBox() : base() { }

		public RepositoryDataSet NowDataSet
		{
			get => nowSource;
			set
			{
				if (nowSource != value)
				{
					if (nowSource != null)
					{
						SuspendEvents();
					}

					nowSource = value;
					ReadData();
				}
			}
		}

		public RepositoryDataSet LastDataSet
		{
			get => lastSource;
			set
			{
				if (lastSource != value)
				{
					if (lastSource != null)
					{
						SuspendEvents();
					}
					lastSource = value;
					ReadData();
				}
			}
		}

		private void Categories_TableCleared(object sender, DataTableClearEventArgs e) => ReadData();

		private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e) => ReadData();

		private void Categories_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			CategoriesRow row = e.Row as CategoriesRow;
			IEnumerable<SubcategoriesRow> subcategories = NowDataSet.Subcategories.GetSubcategoriesRows(row.key_guid);
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
				if (NowDataSet.Categories.Exists(e.Row.category_guid))
				{
					Category category = NowDataSet.GetCategory(e.Row.category_guid);
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
			if (NowDataSet != null)
			{
				NowDataSet.Categories.CategoriesRowChanged -= Categories_RowChanged;
				NowDataSet.Categories.TableCleared -= Categories_TableCleared;
				NowDataSet.Categories.RowDeleting -= Categories_RowDeleting;

				NowDataSet.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
				NowDataSet.Subcategories.TableCleared -= Subcategories_TableCleared;
				NowDataSet.Subcategories.RowDeleting -= Subcategories_RowDeleting;
			}
		}

		private void ResumeEvents()
		{
			if (NowDataSet != null)
			{
				NowDataSet.Categories.CategoriesRowChanged += new CategoriesRowChangeEventHandler(Categories_RowChanged);
				NowDataSet.Categories.TableCleared += new DataTableClearEventHandler(Categories_TableCleared);
				NowDataSet.Categories.RowDeleting += new DataRowChangeEventHandler(Categories_RowDeleting);

				NowDataSet.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
				NowDataSet.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);
				NowDataSet.Subcategories.RowDeleting += new DataRowChangeEventHandler(Subcategories_RowDeleting);
			}
		}

		public void ReadData()
		{
			if (DesignMode || NowDataSet == null) return;

			SuspendEvents();

			BeginUpdate();

			Items.Clear();

			if (NowDataSet.Categories.Rows.Count > 0)
			{
				foreach (DataRow row in NowDataSet.Categories.Rows)
				{
					Category category = Category.Create(row);
					Add(category);
				}
			}

			if (Items.Count > 0 && NowDataSet.Subcategories.Rows.Count > 0)
			{
				foreach (DataRow row in NowDataSet.Subcategories.Rows)
				{
					Category category = NowDataSet.GetCategory(row["category_guid"] as string);
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

		private void Add(ICategoriesListItem item) => Items.Add(new ListItem(item, NowDataSet, LastDataSet));

		private void Insert(int index, ICategoriesListItem item) => Items.Insert(index, new ListItem(item, NowDataSet, LastDataSet));

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

		public bool ExistsListItem(string guid) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == guid)
			.Any();

		public ListItem GetListItem(string guid) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == guid)
			.First();

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

		private bool ExistsListItem(Category category) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == category.Guid)
			.Any();

		private ListItem GetListItem(Category category) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == category.Guid)
			.First();

		private bool ExistsListItem(Subcategory subcategory) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == subcategory.Guid)
			.Any();

		private ListItem GetListItem(Subcategory subcategory) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == subcategory.Guid)
			.First();

		private bool ExistsListItem(CategoriesRow row) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == row.key_guid)
			.Any();

		private ListItem GetListItem(CategoriesRow row) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == row.key_guid)
			.First();

		private bool ExistsListItem(SubcategoriesRow row) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == row.key_guid)
			.Any();

		private ListItem GetListItem(SubcategoriesRow row) => Items.AsEnumerable()
			.Where(x => x.owner.Code.Guid == row.key_guid)
			.First();

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

	namespace StatusListControl
	{
		public class ListItem : ControlLibrary.Controls.ListControls.ListItem
		{
			protected internal ICategoriesListItem owner;
			protected internal RepositoryDataSet nowDataSet;
			protected internal RepositoryDataSet lastDataSet;
			public ListItem() : base()
			{
				owner = default;
			}

			public ListItem(ICategoriesListItem owner, RepositoryDataSet nowDataSet, RepositoryDataSet lastDataSet) : base(
				CreateNotesArray(owner))
			{
				this.nowDataSet = nowDataSet;
				this.lastDataSet = lastDataSet;
				this.owner = owner;
				((SubcategoryTitleListItemNote)notes[0]).owner = this;
				if (notes.Length == 3)
					((StatusListItemNote)notes[2]).owner = this;
			}

			private static ListItemNote[] CreateNotesArray(ICategoriesListItem owner)
			{
				if (owner is Category)
				{
					return new ListItemNote[] {
						new CategoryTitleListItemNote(owner.Code, owner.Caption),
						new DescriptionListItemNote(owner.Description)
					};
				}
				else
				{
					return new ListItemNote[] {
						new SubcategoryTitleListItemNote(owner.Code, owner.Caption),
						new DescriptionListItemNote(owner.Description),
						new StatusListItemNote()
					};
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				e.DrawBackground();
				base.OnDraw(e);
				if (notes.Length > 2 && (!notes[1].Size.IsEmpty || !notes[2].Size.IsEmpty))
				{
					// Рисование линии после титульной части
					Utils.Drawing.DrawLine(e, notes[0]);
				}
				e.DrawFocusRectangle();
			}

			public bool IsCategory => owner is Category;

			public int NowNotesCount => nowDataSet.GetNotesCount(owner.Guid);

			public int NowGroupNotesCount => nowDataSet.GetGroupByFileCaptionNotesCount(owner.Guid);

			public bool IsDecimal => !IsCategory && ((Subcategory)owner).IsDecimal;

			public double NowNotesSum => nowDataSet.GetNotesSum(owner.Guid);

			public bool IsLast => lastDataSet != null;

			public int LastNotesCount => IsLast ? lastDataSet.GetNotesCount(owner.Guid) : 0;

			public int LastGroupNotesCount => IsLast ? lastDataSet.GetGroupByFileCaptionNotesCount(owner.Guid) : 0;

			public double LastNotesSum => IsLast ? lastDataSet.GetNotesSum(owner.Guid) : 0;

			public string Growth => ((NowNotesSum - LastNotesSum) > 0 ? "+" : "") + (NowNotesSum - LastNotesSum).ToString("N0");

			public string GrowthPercent => LastNotesSum != 0 ? ((NowNotesSum - LastNotesSum) > 0 ? "+" : "") + (((double)(NowNotesSum - LastNotesSum)) * 100 / LastNotesSum).ToString("F2") + " %" : "-";

		}

		public class CategoryListItem : ListItem
		{
			public CategoryListItem() : base()
			{
				owner = default;
			}

			public CategoryListItem(Category category, RepositoryDataSet nowDataSet, RepositoryDataSet lastDataSet) : base(owner: category, nowDataSet: nowDataSet, lastDataSet: lastDataSet)
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

			public SubcategoryListItem(Subcategory subcategory, RepositoryDataSet nowDataSet, RepositoryDataSet lastDataSet) : base(owner: subcategory, nowDataSet: nowDataSet, lastDataSet: lastDataSet)
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

			public ListItemNote(string text) => this.text = text;

			public string Text
			{
				get => text;
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
			internal ListItem owner;

			private Version code;
			private Size codeSize;
			private Size textSize;

			public SubcategoryTitleListItemNote(int major, int minor, string guid, string text) : this(code: Version.Create(major: major, minor: minor, guid: guid), text: text) { }

			public SubcategoryTitleListItemNote(Version code, string text) : base(text: text)
			{
				this.code = code;
				codeSize = Size.Empty;
				textSize = Size.Empty;
			}

			public Version Code
			{
				get => code;
				set
				{
					if (code != value)
					{
						code = value;
						DoContentChanged();
					}
				}
			}

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
				Rectangle codeRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, codeSize.Width, codeSize.Height);

				Color backColor = Color.Gray;
				if (owner.IsCategory)
				{
					backColor = Color.Orange;
				}
				else if (owner.NowNotesCount > 0 && owner.IsDecimal)
				{
					backColor = Color.Green;
				}
				else if (owner.NowNotesCount > 0)
				{
					backColor = Color.ForestGreen;
				}

				Utils.Drawing.DrawCode(Code, new DrawItemEventArgs(e.Graphics, boldFont, codeRect, e.Index, e.State, backColor, Color.White));

				if (!textSize.IsEmpty)
				{
					Brush brush = new SolidBrush(e.ForeColor);
					Rectangle textRect = new Rectangle(
						e.Bounds.Width - textSize.Width - 1,
						e.Bounds.Y,
						textSize.Width,
						textSize.Height);
					e.Graphics.DrawString(Text, boldFont, brush, textRect, LEFT_STRING_FORMAT);
				}
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				codeSize = Utils.Drawing.GetCodeSize(graphics, boldFont);
				textSize = GetTextSize(graphics, Text, font, itemWidth - codeSize.Width - 4, CENTER_STRING_FORMAT);

				if (codeSize.Height < textSize.Height)
				{
					return new Size(itemWidth, textSize.Height + 8);
				}
				else
				{
					return new Size(itemWidth, codeSize.Height + 8);
				}
			}
		}

		public class CategoryTitleListItemNote : SubcategoryTitleListItemNote
		{
			public CategoryTitleListItemNote(int major, int minor, string guid, string text) : base(major: major, minor: minor, guid: guid, text: text) { }

			public CategoryTitleListItemNote(Version code, string text) : base(code: code, text: text) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				if (e.Bounds.Width == 0 || e.Bounds.Height == 0) { return; }
				
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
				return new Size(itemWidth, result.Height + 2);				
			}
		}

		public class StatusListItemNote : ListItemNote
		{
			internal ListItem owner;

			public StatusListItemNote() : base(text: string.Empty) { }

			protected override void OnDraw(DrawItemEventArgs e)
			{
				Brush brush = new SolidBrush(e.ForeColor);
				if (owner.IsDecimal && owner.IsLast)
					e.Graphics.DrawString(string.Format("Число записей: {0}({2}), сумма: {1}({3}) / {4}({5})", owner.NowGroupNotesCount, owner.NowNotesSum, owner.LastGroupNotesCount, owner.LastNotesSum, owner.Growth, owner.GrowthPercent), e.Font, brush, e.Bounds, LEFT_STRING_FORMAT);
				else if (owner.IsDecimal)
					e.Graphics.DrawString(string.Format("Число записей: {0}, сумма: {1}", owner.NowGroupNotesCount, owner.NowNotesSum), e.Font, brush, e.Bounds, LEFT_STRING_FORMAT);
				else
					e.Graphics.DrawString(string.Format("Число записей: {0}", owner.NowGroupNotesCount), e.Font, brush, e.Bounds, LEFT_STRING_FORMAT);
				brush.Dispose();
			}

			protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
			{
				Size result = GetTextSize(graphics: graphics, "Число записей: 000 000, сумма: 000 000", font: font, width: itemWidth, LEFT_STRING_FORMAT);
				return new Size(result.Width, result.Height + 10);
			}
		}
	}
}
