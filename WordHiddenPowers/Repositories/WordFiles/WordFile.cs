using System;
using System.Collections.Generic;
using System.Data;

namespace WordHiddenPowers.Repositories.WordFiles
{
	public class WordFile : IComparable<WordFile>
	{
		public static WordFile Create(DataRow dataRow)
		{
			return new WordFile(
				id: (int)dataRow["id"],
				fileName: dataRow.IsNull("FileName") ? string.Empty : dataRow["FileName"] as string,
				caption: dataRow.IsNull("Caption") ? string.Empty : dataRow["Caption"] as string,
				description: dataRow.IsNull("fileDescription") ? string.Empty : dataRow["Description"] as string,
				date: (DateTime)dataRow["Date"]);
		}

		public static WordFile Create(RepositoryDataSet.WordFilesRow dataRow)
		{
			return new WordFile(
				id: dataRow.id,
				fileName: dataRow.FileName,
				caption: dataRow.Caption,
				description: dataRow.IsDescriptionNull() ? string.Empty : dataRow.Description,
				date: dataRow.Date);
		}

		public static WordFile Create(string fileName, string caption, string description, DateTime date)
		{
			return new WordFile(
				id: -1,
				fileName: fileName,
				caption: caption,
				description: description,
				date: date);
		}

		public static WordFile Default()
		{
			return new WordFile(
				id: 0,
				fileName: "Не определено",
				caption: "Не определено",
				description: "Значение не определено",
				date: DateTime.Now);
		}

		private WordFile(int id, string fileName, string caption, string description, DateTime date)
		{
			Id = id;
			Filename = fileName;
			Caption = caption;
			Description = description;
			Date = date;
		}

		public int Id { get; }

		public string Filename { get; set; }

		public string Caption { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

		public object[] ToObjectsArray()
		{
			return new object[] {
				Id < 0 ? null: (object) Id,
				Filename,
				Caption,
				Description,
				Date };
		}

		public int CompareTo(WordFile value)
		{
			return Compare(this, value);
		}

		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is WordFile c)
			{
				return c.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public static int Compare(WordFile x, WordFile y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					return x.Filename.CompareTo(y.Filename);
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

		public class WordFileComparer : IComparer<WordFile>
		{
			public int Compare(WordFile x, WordFile y)
			{
				return WordFile.Compare(x, y);
			}
		}
	}
}
