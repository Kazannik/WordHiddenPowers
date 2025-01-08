using System.Collections.Generic;
using System.Data;
using System.Linq;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Categories;
using WordHiddenPowers.Repositoryes.Notes;
using static WordHiddenPowers.Repositoryes.RepositoryDataSet;
using Category = WordHiddenPowers.Repositoryes.Categories.Category;
using Version = ControlLibrary.Structures.Version;

namespace WordHiddenPowers.Controls
{
	public class NoteListBox : ControlLibrary.Controls.ListControls.ListControl<NoteListItem>
	{
		private RepositoryDataSet source;

		internal IDictionary<int, Subcategory> subcategories;

		private IDictionary<int, WordFilesRow> files;

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
					source.DecimalPowers.DecimalPowersRowChanged -= DecimalPowers_RowChanged;
					source.TextPowers.TextPowersRowChanged -= TextPowers_RowChanged;
					source.DecimalPowers.DecimalPowersRowDeleted -= DecimalPowers_RowChanged;
					source.TextPowers.TextPowersRowDeleted -= TextPowers_RowChanged;
					source.DecimalPowers.TableCleared -= TablesPowers_TableCleared;
					source.TextPowers.TableCleared -= TablesPowers_TableCleared;

					source.Subcategories.SubcategoriesRowChanged -= Subcategories_RowChanged;
					source.Subcategories.SubcategoriesRowDeleted -= Subcategories_RowChanged;
					source.Subcategories.TableCleared -= Subcategories_TableCleared;

					source.WordFiles.WordFilesRowChanged -= WordFiles_RowChanged;
					source.WordFiles.WordFilesRowDeleted -= WordFiles_RowChanged;
					source.WordFiles.TableCleared -= WordFiles_TableCleared;
				}
				source = value;

				Items.Clear();

				ReadData();

				if (source != null)
				{
					source.DecimalPowers.DecimalPowersRowChanged += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
					source.TextPowers.TextPowersRowChanged += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
					source.DecimalPowers.DecimalPowersRowDeleted += new DecimalPowersRowChangeEventHandler(DecimalPowers_RowChanged);
					source.TextPowers.TextPowersRowDeleted += new TextPowersRowChangeEventHandler(TextPowers_RowChanged);
					source.DecimalPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);
					source.TextPowers.TableCleared += new DataTableClearEventHandler(TablesPowers_TableCleared);

					source.Subcategories.SubcategoriesRowChanged += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.SubcategoriesRowDeleted += new SubcategoriesRowChangeEventHandler(Subcategories_RowChanged);
					source.Subcategories.TableCleared += new DataTableClearEventHandler(Subcategories_TableCleared);

					source.WordFiles.WordFilesRowChanged += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
					source.WordFiles.WordFilesRowDeleted += new WordFilesRowChangeEventHandler(WordFiles_RowChanged);
					source.WordFiles.TableCleared += new DataTableClearEventHandler(WordFiles_TableCleared);
				}
			}
		}

		private void WordFiles_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void WordFiles_RowChanged(object sender, WordFilesRowChangeEvent e)
		{
			ReadData();
		}

		private void Subcategories_TableCleared(object sender, DataTableClearEventArgs e)
		{
			ReadData();
		}

		private void Subcategories_RowChanged(object sender, SubcategoriesRowChangeEvent e)
		{
			ReadData();
		}

		private void DecimalPowers_RowChanged(object sender, DecimalPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) RemoveNote(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Reiting = e.Row.Reiting;
				note.Value = e.Row.Value;
				Category category = source.Categories.Get(e.Row.category_id);
				note.Subcategory = source.Subcategories.Get(category, e.Row.subcategory_id);
				//if (note.Rectangle != null)
				//	Invalidate(note.Rectangle);
			}
		}

		private void TextPowers_RowChanged(object sender, TextPowersRowChangeEvent e)
		{
			if (e.Action == DataRowAction.Add)
			{
				AddNote(Note.Create(e.Row, files[e.Row.file_id], subcategories[e.Row.subcategory_id]));
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Note note = GetNote(e.Row);
				if (note != null) RemoveNote(note);
			}
			else if (e.Action == DataRowAction.Change)
			{
				Note note = GetNote(e.Row);
				note.Description = e.Row.Description;
				note.Reiting = e.Row.Reiting;
				note.Value = e.Row.Value;
				//if (note.Rectangle != null)
				//	Invalidate(note.Rectangle);
			}
		}

		private void TablesPowers_TableCleared(object sender, DataTableClearEventArgs e)
		{
			Items.Clear();
		}

		public void ReadData()
		{
			if (DesignMode || source == null) return;

			BeginUpdate();
			IDictionary<int, Category> categories = new Dictionary<int, Category>();
			if (source.Categories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Categories.Rows)
				{
					Category category = Category.Create(row);
					categories.Add(category.Id, category);
				}
			}
			else
			{
				Category category = Category.Default();
				categories.Add(category.Id, category);
			}

			subcategories = new Dictionary<int, Subcategory>();
			if (source.Subcategories.Rows.Count > 0)
			{
				foreach (DataRow row in source.Subcategories.Rows)
				{
					Subcategory subcategory = Subcategory.Create(categories[(int)row["category_id"]], row);
					subcategories.Add(subcategory.Id, subcategory);
				}
			}
			else
			{
				foreach (Category category in categories.Values)
				{
					Subcategory subcategory = Subcategory.Default(category: category);
					subcategories.Add(subcategory.Id, subcategory);
				}
			}

			files = new Dictionary<int, WordFilesRow>();
			if (source.WordFiles.Rows.Count > 0)
			{
				foreach (WordFilesRow row in source.WordFiles.Rows)
				{
					files.Add(row.id, row);
				}
			}

			if (source.TextPowers.Rows.Count > 0 ||
				source.DecimalPowers.Rows.Count > 0)
			{
				foreach (Note note in source.GetNotes())
				{
					AddNote(note);	 
				}
			}
			EndUpdate();
		}

		private void AddNote(Note note)
		{
			Items.Add(new NoteListItem(note));
		}

		private void RemoveNote(Note note)
		{
			Items.Remove(GetNoteListItem(note.DataRow as DataRow));			
		}

		private Note GetNote(DataRow dataRow)
		{
			return (from NoteListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item.Note).First();
		}

		private NoteListItem GetNoteListItem(DataRow dataRow)
		{
			return (from NoteListItem item in Items
					where item.Note.DataRow.Equals(dataRow)
					select item).First();
		}		

		public NoteListBox(): base() { }			
	}

	public class NoteListItem : ControlLibrary.Controls.ListControls.ListItem
	{
		public NoteListItem() : base() 
		{
			Note = default;
		}

		public NoteListItem(Note note) : base()
		{
			Note = note;
		}	

		public Note Note { get; }

		public override Version Version
		{
			get
			{
				return Version.Create(Note.Category.Id, Note.Subcategory.Id);
			}
		}

		public override string Caption
		{
			get
			{
				return Note.Subcategory.Caption.Trim();
			}			
		}

		public override string Text
		{
			get
			{
				return Note.Value.ToString().Trim();
			}			
		}

		public override string Description
		{
			get
			{
				return Note.Description.Trim();
			}
		}
	}
}
