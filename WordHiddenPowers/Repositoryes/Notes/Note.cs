using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes.Categories;
using Category = WordHiddenPowers.Repositoryes.Categories.Category;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;

namespace WordHiddenPowers.Repositoryes.Notes
{
	public class Note : INotifyPropertyChanged, IComparable<Note>
	{
		internal Controls.NoteListBox owner;
		internal Controls.NoteListBox.NoteState state = Controls.NoteListBox.NoteState.Passive;

		internal Rectangle removeButton;

		public Rectangle Rectangle { get; private set; }

		public bool Selected
		{
			get
			{
				return owner.SelectedItem.Equals(this);
			}
			set
			{
				owner.SelectedItem = this;
			}
		}

		internal static Note Create(RepositoryDataSet.DecimalPowersRow dataRow, RepositoryDataSet.WordFilesRow fileRow, Subcategory subcategory)
		{
			return new Note(
				id: dataRow.id,
				subcategory: subcategory,
				description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
				dblValue: dataRow.Value,
				reiting: dataRow.Reiting,
				wordSelectionStart: dataRow.WordSelectionStart,
				wordSelectionEnd: dataRow.WordSelectionEnd,
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
				reiting: dataRow.Reiting,
				wordSelectionStart: dataRow.WordSelectionStart,
				wordSelectionEnd: dataRow.WordSelectionEnd,
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
			int reiting, 
			int wordSelectionStart, 
			int wordSelectionEnd, 
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
			Reiting = reiting;
			WordSelectionStart = wordSelectionStart;
			WordSelectionEnd = wordSelectionEnd;
			DataRow = dataRow;

			FileName = FileName;
			FileCaption = fileCaption;
			FileDescription = fileDescription;
			FileDate = fileDate;
		}

		protected Note(
			int id, 
			Subcategory subcategory, 
			string description, 
			string strValue, 
			int reiting, 
			int wordSelectionStart, 
			int wordSelectionEnd, 
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
			Reiting = Reiting;
			WordSelectionStart = wordSelectionStart;
			WordSelectionEnd = wordSelectionEnd;
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

		public int Reiting { get; internal set; }

		public int WordSelectionStart { get; }

		public int WordSelectionEnd { get; }

		public bool IsText
		{
			get { return Value.GetType() != typeof(double); }
		}

		public object DataRow { get; }

		public string FileName { get; }

		public string FileCaption { get; }

		public string FileDescription { get; }

		public DateTime FileDate { get; }

		public object[] ToObjectsArray()
		{
			return (new object[]{
				Id,
				Subcategory.Category.Id,
				Subcategory.Id,
				Description,
				Value,
				Reiting,
				WordSelectionStart,
				WordSelectionEnd });
		}

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
					return decimal.Compare(x.WordSelectionStart, y.WordSelectionStart) == 0 ?
						decimal.Compare(x.WordSelectionEnd, y.WordSelectionEnd) : 0;
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

		public class NoteComparer : IComparer<Note>
		{
			public int Compare(Note x, Note y)
			{
				return Note.Compare(x, y);
			}
		}

		#endregion

		#region DrawItem

		public void DrawItem(DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				e.DrawBackground();

				Rectangle = e.Bounds;
				const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.Top;

				SizeF captionSize, subcategorySize, textSize;
				captionSize = e.Graphics.MeasureString(Category.Caption, e.Font, e.Bounds.Width);
				subcategorySize = e.Graphics.MeasureString(Subcategory.Caption, e.Font, e.Bounds.Width);
				textSize = e.Graphics.MeasureString(Value as string, e.Font, e.Bounds.Width);


				Rectangle captionRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, (int)captionSize.Height);
				TextRenderer.DrawText(e.Graphics, Category.Caption, e.Font, captionRectangle, Color.RosyBrown, flags | TextFormatFlags.WordBreak);

				Rectangle subcategoryRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + (int)captionSize.Height, e.Bounds.Width, (int)subcategorySize.Height);
				TextRenderer.DrawText(e.Graphics, Subcategory.Caption, e.Font, subcategoryRectangle, Color.YellowGreen, flags | TextFormatFlags.WordBreak);

				Rectangle textRectangle = new Rectangle(e.Bounds.X, e.Bounds.Y + (int)captionSize.Height + (int)subcategorySize.Height, e.Bounds.Width, (int)textSize.Height);
				TextRenderer.DrawText(e.Graphics, Value as string, e.Font, textRectangle, e.ForeColor, flags | TextFormatFlags.WordBreak);
				
				e.DrawFocusRectangle();
			}
		}

		public Size GetSizeItem(Graphics graphics, Font font, Size size)
		{
			SizeF captionSize, subcategorySize, textSize;
			captionSize = graphics.MeasureString(Category.Caption, font, size);
			subcategorySize = graphics.MeasureString(Subcategory.Caption, font, size);
			textSize = graphics.MeasureString(Value as string, font, size);

			Size result = new Size(size.Width, (int)captionSize.Height + (int)subcategorySize.Height + (int)textSize.Height)
			{
				Height = Math.Max(Math.Min(size.Height, 247), font.Height + 8)
			};
			return result;
		}

		#endregion

		public class NoteEventArgs : EventArgs
		{
			public NoteEventArgs(Note args)
			{
				Note = args;
			}

			public Note Note { get; }
		}

		public class NoteMouseEventArgs : MouseEventArgs
		{
			public NoteMouseEventArgs(Note note, MouseEventArgs arg) : base(arg.Button, arg.Clicks, arg.X, arg.Y, arg.Delta)
			{
				Note = note;
			}

			public Note Note { get; }
		}
	}
}
