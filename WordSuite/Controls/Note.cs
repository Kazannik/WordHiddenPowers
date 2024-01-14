namespace WordSuite.Controls
{
    public class Note: WordHiddenPowers.Repositoryes.Models.Note
    {
        public static Note Create(WordHiddenPowers.Repositoryes.RepositoryDataSet.DecimalPowersRow dataRow, WordHiddenPowers.Repositoryes.Models.Subcategory subcategory)
        {
            return new Note(id: dataRow.id,
                subcategory: subcategory,
                description: dataRow.Description,
                value: dataRow.Value,
                reiting: dataRow.Reiting,
                wordSelectionStart: dataRow.WordSelectionStart,
                wordSelectionEnd: dataRow.WordSelectionEnd,
                dataRow: dataRow);
        }

        public static Note Create(WordHiddenPowers.Repositoryes.RepositoryDataSet.TextPowersRow dataRow, WordHiddenPowers.Repositoryes.Models.Subcategory subcategory)
        {
            return new Note(id: dataRow.id,
                subcategory: subcategory,
                description: dataRow.Description,
                value: dataRow.Value,
                reiting: dataRow.Reiting,
                wordSelectionStart: dataRow.WordSelectionStart,
                wordSelectionEnd: dataRow.WordSelectionEnd,
                dataRow: dataRow);
        }

        private Note(int id, 
            WordHiddenPowers.Repositoryes.Models.Subcategory subcategory,
            string description, 
            double value, 
            int reiting, 
            int wordSelectionStart, 
            int wordSelectionEnd, 
            object dataRow) : base(id: id,
                subcategory: subcategory,
                description: description,
                value: value,
                reiting: reiting,
                wordSelectionStart: wordSelectionStart,
                wordSelectionEnd: wordSelectionEnd,
                dataRow: dataRow)
        { }

        private Note(int id,
            WordHiddenPowers.Repositoryes.Models.Subcategory subcategory,
            string description,
            string value,
            int reiting,
            int wordSelectionStart,
            int wordSelectionEnd,
            object dataRow) : base(id: id,
                subcategory: subcategory,
                description: description,
                value: value,
                reiting: reiting,
                wordSelectionStart: wordSelectionStart,
                wordSelectionEnd: wordSelectionEnd,
                dataRow: dataRow)
        { }

    }
}
