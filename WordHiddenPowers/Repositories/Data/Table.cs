using System;

namespace WordHiddenPowers.Repositories.Data
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

		public string Caption { get; private set; }

		public string FileName { get; private set; }

		public int ColumnCount { get; }

		public RowCollection Rows { get; }

		public bool IsEmpty
		{
			get { return Rows.Count == 0 || ColumnCount == 0; }
		}

		public void Clear()
		{
			Caption = string.Empty;
			FileName = string.Empty;

			for (int r = 0; r < Rows.Count; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					Rows[r][c].Value = 0;
				}
			}
		}

		public Table Clone()
		{
			return new Table(Rows.Count, ColumnCount);
		}

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
			return Create(text: text, caption: string.Empty, fileName: string.Empty);
		}

		public static Table Create(string text, string caption, string fileName)
		{
			if (string.IsNullOrWhiteSpace(text)) return new Table(0, 0);

			string[] rows = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			string[] cells = rows[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			Table table = new Table(rows.Length, cells.Length);

			for (int r = 0; r < table.Rows.Count; r++)
			{
				cells = rows[r].Split(';');
				for (int c = 0; c < table.ColumnCount; c++)
				{
					table.Rows[r][c].Value = int.Parse(cells[c]);
				}
			}
			table.Caption = caption;
			table.FileName = fileName;
			return table;
		}

		public static Table operator +(Table a, Table b)
		{
			for (int r = 0; r < a.Rows.Count; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].Value = a.Rows[r][c].Value + b.Rows[r][c].Value;
				}
			}
			return a;
		}

		public static Table operator -(Table a, Table b)
		{
			for (int r = 0; r < a.Rows.Count; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].Value = a.Rows[r][c].Value - b.Rows[r][c].Value;
				}
			}
			return a;
		}

		public static Table operator *(Table a, int b)
		{
			for (int r = 0; r < a.Rows.Count; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].Value = a.Rows[r][c].Value * b;
				}
			}
			return a;
		}

		public static Table operator /(Table a, int b)
		{
			for (int r = 0; r < a.Rows.Count; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].Value = a.Rows[r][c].Value / b;
				}
			}
			return a;
		}
	}
}
