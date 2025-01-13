using ControlLibrary.Controls.ListControls;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using static WordHiddenPowers.Repositoryes.RepositoryDataSet;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls.ListControls
{
	public class CategoriesListBox : ListControl<CategoryListItem, CategoryListItemNote>
	{
		private RepositoryDataSet source;

		public RepositoryDataSet DataSet
		{
			get
			{
				return source;
			}
			set
			{
				if (source != null)
				{
					source.Categories.CategoriesRowChanged -= Categories_RowChanged;
					source.Categories.CategoriesRowDeleted -= Categories_RowChanged;
					source.Categories.TableCleared -= Categories_TableCleared;

					source.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
					source.Subcategories.SubcategoriesRowDeleted -= Subcategories_RowChanged;
					source.Subcategories.TableCleared -= Subcategories_TableCleared;
				}
				source = value;

				ReadData();

				if (source != null)
				{
					source.Categories.CategoriesRowChanged += new CategoriesRowChangeEventHandler(Categories_RowChanged);
					source.Categories.CategoriesRowDeleted += new CategoriesRowChangeEventHandler(Categories_RowChanged);
					source.Categories.TableCleared += new DataTableClearEventHandler(Categories_TableCleared);

					source.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.SubcategoriesRowDeleted += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);
				}
			}
		}


		private void Categories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Categories_RowChanged(object sender, CategoriesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Add(e.Row);
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Remove(e.Row);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Invalidate();
			}
		}

		private void Subcategories_RowChanged(object sender, SubcategoriesRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				Add(e.Row);
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Remove(e.Row);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Invalidate();
			}
		}

		public void ReadData()
		{
			if (DesignMode || source == null) return;
			BeginUpdate();
			Items.Clear();
			if (source.Categories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Categories.Rows)
				{
					Add(row);
				}
			}

			if (source.Subcategories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Subcategories.Rows)
				{
					Add(row);
				}
			}
			EndUpdate();
		}

		private void Add(DataRow dataRow)
		{
			Items.Add(new CategoryListItem(dataRow));
		}

		private void Remove(DataRow dataRow)
		{
			CategoryListItem item = Get(dataRow);
			Items.Remove(item);
		}

		private CategoryListItem Get(DataRow dataRow)
		{
			return (from CategoryListItem item in Items
					where item.DataRow.Equals(dataRow)
					select item).First();
		}

		private bool Exists(DataRow dataRow)
		{
			return (from CategoryListItem item in Items
					where item.DataRow.Equals(dataRow)
					select item).Any();
		}

		public CategoriesListBox() : base() { }
	}

	public class CategoryListItem : ListItem
	{
		internal readonly DataRow DataRow;

		public CategoryListItem() : base(new CategoryListItemNote[] {
			new CaptionCategoryListItemNote(),
			new TextCategoryListItemNote(),
			new BottomCategoryListItemNote()})
		{
			DataRow = null;
		}

		internal CategoryListItem(DataRow dataRow) : base(new CategoryListItemNote[] {
			new CaptionCategoryListItemNote(),
			new TextCategoryListItemNote(),
			new BottomCategoryListItemNote()})
		{
			DataRow = dataRow;
		}
				
		

		public bool IsCategory
		{
			get
			{
				return DataRow.Table.TableName.Equals("Categories");
			}
		}

		

		
	}

	public class CategoryListItemNote : ListItemNote
	{
		public CategoryListItemNote() { }
		
		protected override void OnDraw(DrawItemEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
		{
			throw new System.NotImplementedException();
		}
	}

	public class CaptionCategoryListItemNote : CategoryListItemNote
	{
		protected override void OnDraw(DrawItemEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
		{
			throw new System.NotImplementedException();
		}
	}
	
	public class TextCategoryListItemNote : CategoryListItemNote
	{
		protected override void OnDraw(DrawItemEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
		{
			throw new System.NotImplementedException();
		}
	}

	public class BottomCategoryListItemNote : CategoryListItemNote
	{
		protected override void OnDraw(DrawItemEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		protected override Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
		{
			throw new System.NotImplementedException();
		}
	}
}
