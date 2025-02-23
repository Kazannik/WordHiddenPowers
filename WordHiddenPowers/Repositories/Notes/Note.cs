using System;
using System.Collections.Generic;
using System.ComponentModel;
using WordHiddenPowers.Repositories.Categories;
using Category = WordHiddenPowers.Repositories.Categories.Category;

namespace WordHiddenPowers.Repositories.Notes
{
	public class Note : INotifyPropertyChanged, IComparable<Note>
	{
		internal static Note Create(RepositoryDataSet.DecimalPowersRow dataRow, RepositoryDataSet.WordFilesRow fileRow, Subcategory subcategory)
		{
			return new Note(
				id: dataRow.id,
				subcategory: subcategory,
				description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
				dblValue: dataRow.Value,
				rating: dataRow.Rating,
				wordSelectionStart: dataRow.WordSelectionStart,
				wordSelectionEnd: dataRow.WordSelectionEnd,
				hide: dataRow.Hide,
				dataRow: dataRow,
				fileName: fileRow.FileName,
				fileCaption: fileRow.Caption,
				fileDescription: !fileRow.IsDescriptionNull() ? fileRow.Description : string.Empty,
				fileDate: fileRow.Date
				);
		}

		internal static Note Create(RepositoryDataSet.TextPowersRow dataRow, RepositoryDataSet.WordFilesRow fileRow, Subcategory subcategory)
		{
			return new Note(
				id: dataRow.id,
				subcategory: subcategory,
				description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
				strValue: dataRow.Value,
				rating: dataRow.Rating,
				wordSelectionStart: dataRow.WordSelectionStart,
				wordSelectionEnd: dataRow.WordSelectionEnd,
				hide: dataRow.Hide,
				dataRow: dataRow,
				fileName: fileRow.FileName,
				fileCaption: fileRow.Caption,
				fileDescription: !fileRow.IsDescriptionNull() ? fileRow.Description : string.Empty,
				fileDate: fileRow.Date
				);
		}

		protected Note(
			int id, 
			Subcategory subcategory, 
			string description, 
			double dblValue, 
			int rating, 
			int wordSelectionStart, 
			int wordSelectionEnd, 
			bool hide,
			object dataRow,
			string fileName, 
			string fileCaption, 
			string fileDescription, 
			DateTime fileDate)
		{
			Subcategory = subcategory;
			Id = id;
			Description = description;
			Value = dblValue;
			Rating = rating;
			WordSelectionStart = wordSelectionStart;
			WordSelectionEnd = wordSelectionEnd;
			Hide = hide;
			DataRow = dataRow;

			FileName = fileName;
			FileCaption = fileCaption;
			FileDescription = fileDescription;
			FileDate = fileDate;
		}

		protected Note(
			int id, 
			Subcategory subcategory, 
			string description, 
			string strValue, 
			int rating, 
			int wordSelectionStart, 
			int wordSelectionEnd,
			bool hide,
			object dataRow,
			string fileName, 
			string fileCaption, 
			string fileDescription, 
			DateTime fileDate)
		{
			Subcategory = subcategory;
			Id = id;
			Description = description;
			Value = strValue;
			Rating = rating;
			WordSelectionStart = wordSelectionStart;
			WordSelectionEnd = wordSelectionEnd;
			Hide = hide;
			DataRow = dataRow;

			FileName = fileName;
			FileCaption = fileCaption;
			FileDescription = fileDescription;
			FileDate = fileDate;
		}

		public Category Category { get { return Subcategory.Category; } }

		public Subcategory Subcategory { get; internal set; }

		public int Id { get; }

		public string Description { get; internal set; }

		public object Value { get; internal set; }

		public int Rating { get; internal set; }

		public int WordSelectionStart { get; }

		public int WordSelectionEnd { get; }

		public bool Hide { get; internal set; }

		public bool IsText
		{
			get { return Value.GetType() != typeof(double); }
		}

		public object DataRow { get; }

		public string FileName { get; }

		public string FileCaption { get; }

		public string FileDescription { get; }

		public DateTime FileDate { get; }
			
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region IComparable<Note> Member

		public int CompareTo(Note value)
		{
			return Compare(this, value);
		}

		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Note n)
			{
				return n.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public static int Compare(Note x, Note y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					int result = decimal.Compare(x.WordSelectionStart, y.WordSelectionStart) == 0 ?
						decimal.Compare(x.WordSelectionEnd, y.WordSelectionEnd) : 0;
					if (result == 0) result = string.Compare(x.Value.ToString(), y.Value.ToString());
					return result; 
				}
				catch (Exception)
				{ return 0; }
			}
			else if (!Equals(x, null) & Equals(y, null))
			{ return 1; }
			else if (Equals(x, null) & !Equals(y, null))
			{ return -1; }
			else { return 0; }
		}

		public class NoteComparer : IComparer<Note>, IEqualityComparer<Note>
		{
			public int Compare(Note x, Note y)
			{
				return Note.Compare(x, y);
			}

			public bool Equals(Note x, Note y)
			{
				return GetHashCode(x) == GetHashCode(y);
			}

			public int GetHashCode(Note obj)
			{
				return unchecked((87 * obj.Value.GetHashCode()) ^ obj.WordSelectionStart.GetHashCode() ^ obj.WordSelectionEnd.GetHashCode());
			}
		}

		#endregion			
	}
}
