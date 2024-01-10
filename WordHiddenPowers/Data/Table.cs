using System;

namespace WordHiddenPowers.Data
{
    public class Table
    {
        public Table(int RowCount, int ColumnCount)
        {
            this.ColumnCount = ColumnCount;
            Rows = new RowCollection();
            for (int i = 0; i < RowCount; i++)
            {
                Rows.Add(new Row(Rows, ColumnCount));
            }
        }

        public int ColumnCount { get; }

        public RowCollection Rows { get; }
        
        public new string ToString()
        {
            string result = string.Empty;
            for (int r = 0; r < Rows.Count; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    result += Rows[r][c].Value.ToString("0") + ';';
                }
                result += Environment.NewLine;
            }
            return result;
        }
        
        public static Table Create(string text)
        {
            string[] rows = text.Split( new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] cells = rows[0].Split( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Table table = new Table(rows.Length, cells.Length);

            for (int r = 0; r < table.Rows.Count; r++)
            {
                cells = rows[r].Split(';');
                for (int c = 0; c < table.ColumnCount; c++)
                {
                    table.Rows[r][c].Value = int.Parse(cells[c]);
                }
            }
            return table;
        }
    }
}
