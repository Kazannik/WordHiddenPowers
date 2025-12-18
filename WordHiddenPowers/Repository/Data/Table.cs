using System;
using System.Globalization;

namespace WordHiddenPowers.Repository.Data
{
	public class Table
	{
		public Table(int RowCount, int ColumnCount)
		{
			this.IsOld = false;
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

		public int RowCount => Rows.Count;

		public RowCollection Rows { get; }

		public bool IsEmpty => RowCount == 0 || ColumnCount == 0;

		public bool IsOld {  get; private set; }

		public void Clear()
		{
			Caption = string.Empty;
			FileName = string.Empty;

			for (int r = 0; r < RowCount; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					Rows[r][c].Value = 0;
					Rows[r][c].OldValue = 0;
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
					result += Rows[r][c].Value.ToString("0") + ';';
				}
				result += Environment.NewLine;
			}
			return result;
		}

		public new string ToStringOld()
		{
			string result = string.Empty;
			for (int r = 0; r < Rows.Count; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					result += Rows[r][c].OldValue.ToString("0") + ';';
				}
				result += Environment.NewLine;
			}
			return result;
		}

		protected void OnValidate(object value)
		{
			if (!typeof(Table).IsAssignableFrom(value.GetType()))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Не удалось привести тип Value: {0} к поддерживаемому типу: {1}.", value.GetType().ToString(), typeof(Table).ToString()));
			}
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
					table.Rows[r][c].Value = int.Parse(cells[c]);
				}
			}
			table.Caption = caption;
			table.FileName = fileName;
			return table;
		}

		public void AddOldData(string text)
		{
			string[] rows = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			for (int r = 0; r < RowCount; r++)
			{
				string[] cells = rows[r].Split(';');
				for (int c = 0; c < ColumnCount; c++)
				{
					Rows[r][c].OldValue = int.Parse(cells[c]);
				}
			}
			IsOld= true;
		}

		public void AddOldData(Table table)
		{
			for (int r = 0; r < RowCount; r++)
			{
				for (int c = 0; c < ColumnCount; c++)
				{
					if (r < table.RowCount && c < table.ColumnCount)
						Rows[r][c].OldValue = table.Rows[r][c].Value;
				}
			}
			IsOld = true;
		}

		public static Table operator +(Table a, Table b)
		{
			if (b.IsOld) a.IsOld = true;
			for (int r = 0; r < a.RowCount; r++)
			{
				for (int c = 0; c < a.ColumnCount; c++)
				{
					a.Rows[r][c].Value = a.Rows[r][c].Value + GetValue(b, r, c);
					a.Rows[r][c].OldValue = a.Rows[r][c].OldValue + GetOldValue(b, r, c);
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
					a.Rows[r][c].Value = a.Rows[r][c].Value - GetValue(b, r, c);
					a.Rows[r][c].OldValue = a.Rows[r][c].OldValue - GetOldValue(b, r, c);
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
					a.Rows[r][c].Value = a.Rows[r][c].Value * b;
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
					a.Rows[r][c].Value = a.Rows[r][c].Value / b;
				}
			}
			return a;
		}
	
		private static int GetValue(Table table, int row, int column)
		{
			if (table.RowCount > row && table.ColumnCount > column)
			{
				return table.Rows[row][column].Value;
			}
			else return 0;
		}

		private static int GetOldValue(Table table, int row, int column)
		{
			if (table.RowCount > row && table.ColumnCount > column)
			{
				return table.Rows[row][column].OldValue;
			}
			else return 0;
		}
	}
}
