using System;

namespace WordHiddenPowers.Repository.Data
{
	public class Table
	{
		public Table(int rowCount, int columnCount)
		{
			this.IsLast = false;
			this.ColumnCount = columnCount;
			Rows = new RowCollection();
			for (int i = 0; i < rowCount; i++)
			{
				Rows.Add(new Row(Rows, columnCount));
			}
		}

		public string Caption { get; private set; }

		public string FileName { get; private set; }

		public int ColumnCount { get; }

		public int RowCount => Rows.Count;

		public RowCollection Rows { get; }

		public bool IsEmpty => RowCount == 0 || ColumnCount == 0;

		public bool IsLast { get; private set; }

		public void Clear()
		{
			Caption = string.Empty;
			FileName = string.Empty;

			for (int r = 0; r < RowCount; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					Rows[r][c].NowValue = 0;
					Rows[r][c].LastValue = 0;
				}
			}
		}

		public Table Clone() => new Table(Rows.Count, ColumnCount);

		public new string ToString()
		{
			string result = string.Empty;
			for (int r = 0; r < Rows.Count; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					result += Rows[r][c].NowValue.ToString("0") + ';';
				}
				result += Environment.NewLine;
			}
			return result;
		}

		public new string ToStringLast()
		{
			string result = string.Empty;
			for (int r = 0; r < Rows.Count; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					result += Rows[r][c].LastValue.ToString("0") + ';';
				}
				result += Environment.NewLine;
			}
			return result;
		}

		public static Table Create(string text) => Create(text: text, caption: string.Empty, fileName: string.Empty);

		/// <summary>
		/// Создать таблицу из текстовой матрицы 
		/// </summary>
		/// <param name="text">Текстовые данные</param>
		/// <param name="caption">Наименование таблицы</param>
		/// <param name="fileName">Имя файла.</param>
		/// <returns></returns>
		public static Table Create(string text, string caption, string fileName)
		{
			if (string.IsNullOrWhiteSpace(text)) return new Table(0, 0);

			string[] rows = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			string[] cells = rows[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			Table table = new Table(rows.Length, cells.Length);

			for (int r = 0; r < table.RowCount; r++)
			{
				cells = rows[r].Split(';');
				for (int c = 0; c < table.ColumnCount; c++)
				{
					table.Rows[r][c].NowValue = int.Parse(cells[c]);
				}
			}
			table.Caption = caption;
			table.FileName = fileName;
			return table;
		}

		public void AddLastData(string text)
		{
			string[] rows = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			for (int r = 0; r < RowCount; r++)
			{
				string[] cells = rows[r].Split(';');
				for (int c = 0; c < ColumnCount; c++)
				{
					Rows[r][c].LastValue = int.Parse(cells[c]);
				}
			}
			IsLast = true;
		}

		public void AddLastData(Table table)
		{
			for (int r = 0; r < RowCount; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					if (r < table.RowCount && c < table.ColumnCount)
						Rows[r][c].LastValue = table.Rows[r][c].NowValue;
				}
			}
			IsLast = true;
		}

		public static Table operator +(Table a, Table b)
		{
			if (b.IsLast) a.IsLast = true;
			for (int r = 0; r < a.RowCount; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].NowValue = a.Rows[r][c].NowValue + GetValue(b, r, c);
					a.Rows[r][c].LastValue = a.Rows[r][c].LastValue + GetOldValue(b, r, c);
				}
			}
			return a;
		}

		public static Table operator -(Table a, Table b)
		{
			for (int r = 0; r < a.RowCount; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].NowValue = a.Rows[r][c].NowValue - GetValue(b, r, c);
					a.Rows[r][c].LastValue = a.Rows[r][c].LastValue - GetOldValue(b, r, c);
				}
			}
			return a;
		}

		public static Table operator *(Table a, int b)
		{
			for (int r = 0; r < a.RowCount; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].NowValue = a.Rows[r][c].NowValue * b;
				}
			}
			return a;
		}

		public static Table operator /(Table a, int b)
		{
			for (int r = 0; r < a.RowCount; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].NowValue = a.Rows[r][c].NowValue / b;
				}
			}
			return a;
		}

		private static int GetValue(Table table, int row, int column)
		{
			if (table.RowCount > row && table.ColumnCount > column)
			{
				return table.Rows[row][column].NowValue;
			}
			else return 0;
		}

		private static int GetOldValue(Table table, int row, int column)
		{
			if (table.RowCount > row && table.ColumnCount > column)
			{
				return table.Rows[row][column].LastValue;
			}
			else return 0;
		}
	}
}
