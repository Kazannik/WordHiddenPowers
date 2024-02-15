using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using WordHiddenPowers.Categories;

namespace WordHiddenPowers.Repositoryes
{
    public class Note: INotifyPropertyChanged, IComparable<Note>
    {
        internal Controls.NoteListBox owner;

        internal Rectangle rectangle;

        internal Controls.NoteListBox.NoteState state = Controls.NoteListBox.NoteState.Passive;

        internal Rectangle removeButton;

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

        internal static Note Create(RepositoryDataSet.DecimalPowersRow dataRow, Subcategory subcategory)
        {
            return new Note(id: dataRow.id,
                subcategory: subcategory,
                description: ! dataRow.IsDescriptionNull() ? dataRow.Description: string.Empty,
                value: dataRow.Value,
                reiting: dataRow.Reiting,
                wordSelectionStart: dataRow.WordSelectionStart,
                wordSelectionEnd: dataRow.WordSelectionEnd,
                dataRow: dataRow,
                fileName: !dataRow.IsfileNameNull() ? dataRow.fileName : string.Empty,
                fileCaption: !dataRow.IsfileCaptionNull() ? dataRow.fileCaption : string.Empty,
                fileDescription: !dataRow.IsfileDescriptionNull() ? dataRow.fileDescription : string.Empty,
                fileDate: !dataRow.IsfileDateNull() ? dataRow.fileDate : DateTime.Today);
        }

        internal static Note Create(RepositoryDataSet.TextPowersRow dataRow, Subcategory subcategory)
        {
            return new Note(id: dataRow.id,
                subcategory: subcategory,
                description: !dataRow.IsDescriptionNull() ? dataRow.Description : string.Empty,
                value: dataRow.Value,
                reiting: dataRow.Reiting,
                wordSelectionStart: dataRow.WordSelectionStart,
                wordSelectionEnd: dataRow.WordSelectionEnd,
                dataRow: dataRow,
                fileName: !dataRow.IsfileNameNull() ? dataRow.fileName: string.Empty,
                fileCaption: !dataRow.IsfileCaptionNull() ? dataRow.fileCaption: string.Empty,
                fileDescription: !dataRow.IsfileDescriptionNull() ? dataRow.fileDescription: string.Empty,
                fileDate: !dataRow.IsfileDateNull() ? dataRow.fileDate: DateTime.Today,
                fileContentHide: !dataRow.IsfileContentHideNull() ? dataRow.fileContentHide: false);
        }

        protected Note(int id, Subcategory subcategory, string description, double value, int reiting, int wordSelectionStart, int wordSelectionEnd, object dataRow, 
            string fileName, string fileCaption, string fileDescription, DateTime fileDate)
        {
            Subcategory = subcategory;
            Id = id;
            Description = description;
            Value = value;
            Reiting = reiting;
            WordSelectionStart = wordSelectionStart;
            WordSelectionEnd = wordSelectionEnd;
            DataRow = dataRow;

            FileCaption = fileCaption;
            FileContentHide = false;
            FileDate = fileDate;
            FileDescription = fileDescription;
            FileName = FileName;
        }

        protected Note(int id, Subcategory subcategory, string description, string value, int reiting, int wordSelectionStart, int wordSelectionEnd, object dataRow,
            string fileName, string fileCaption, string fileDescription, DateTime fileDate, bool fileContentHide)
        {
            Subcategory = subcategory;
            Id = id;
            Description = description;
            Value = value;
            Reiting = Reiting;
            WordSelectionStart = wordSelectionStart;
            WordSelectionEnd = wordSelectionEnd;
            DataRow = dataRow;

            FileCaption = fileCaption;
            FileContentHide = fileContentHide;
            FileDate = fileDate;
            FileDescription = fileDescription;
            FileName = fileName;
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

        public bool FileContentHide { get; set; }

        public object[]  ToObjectsArray()
        {
            return(new object[]{ Id,
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
            if (value is Note)
            {
                Note n = (Note)value;
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
                    return Decimal.Compare(x.WordSelectionStart, y.WordSelectionStart) == 0 ?
                        Decimal.Compare(x.WordSelectionEnd, y.WordSelectionEnd) : 0;
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
    }
}
