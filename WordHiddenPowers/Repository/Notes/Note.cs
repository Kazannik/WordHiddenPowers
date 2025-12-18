using System;
using System.Collections.Generic;
using System.ComponentModel;
using WordHiddenPowers.Repository.Categories;
using Category = WordHiddenPowers.Repository.Categories.Category;

namespace WordHiddenPowers.Repository.Notes
{
	public class Note : INotifyPropertyChanged, IComparable<Note>
	{
		internal static Note Create(RepositoryDataSet.DecimalPowersRow dataRow, RepositoryDataSet.WordFilesRow fileRow, Subcategory subcategory, string wordSelectionText = "")
		{
			return new Note(
				id: dataRow.id,
				subcategory: subcategory,
				description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
				dblValue: dataRow.Value,
				rating: dataRow.Rating,
				wordSelectionText: wordSelectionText,
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

		internal static Note Create(RepositoryDataSet.TextPowersRow dataRow, RepositoryDataSet.WordFilesRow fileRow, Subcategory subcategory, string wordSelectionText = "")
		{
			return new Note(
				id: dataRow.id,
				subcategory: subcategory,
				description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
				strValue: dataRow.Value,
				rating: dataRow.Rating,
				wordSelectionText: wordSelectionText,
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
			string wordSelectionText,
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
			WordSelectionText = wordSelectionText;
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
			string wordSelectionText,
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
			WordSelectionText = wordSelectionText;
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

		public string WordSelectionText { get; private set; }

		public int WordSelectionStart { get; }

		public int WordSelectionEnd { get; }

		public bool Hide { get; internal set; }

		public bool IsText => Value.GetType() != typeof(double);

		public object DataRow { get; }

		public string FileName { get; }

		public string FileCaption { get; }

		public string FileDescription { get; }

		public DateTime FileDate { get; }

		public void SetWordSelectionText(string text) => WordSelectionText = text;

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName) => 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		#endregion

		#region IComparable<Note> Member

		public int CompareTo(Note value) => Compare(this, value);

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
			if (!Equals(x, null) && !Equals(y, null))
			{
				try
				{
					int result = decimal.Compare(x.WordSelectionStart, y.WordSelectionStart) == 0 ?
						decimal.Compare(x.WordSelectionEnd, y.WordSelectionEnd) : 0;
					if (result == 0) result = string.Compare(x.Value.ToString(), y.Value.ToString());
					return result;
				}
				catch (Exception)
				{
					return 0;
				}
			}
			else if (!Equals(x, null) && Equals(y, null))
				return 1;
			else if (Equals(x, null) && !Equals(y, null))
				return -1;
			else
				return 0;
		}

		public class NoteComparer : IComparer<Note>, IEqualityComparer<Note>
		{
			public int Compare(Note x, Note y) => Note.Compare(x, y);

			public bool Equals(Note x, Note y) => GetHashCode(x) == GetHashCode(y);

			public int GetHashCode(Note obj) => 
				unchecked((87 * obj.Value.GetHashCode()) ^ obj.WordSelectionStart.GetHashCode() ^ obj.WordSelectionEnd.GetHashCode());
		}

		#endregion
	}
}
