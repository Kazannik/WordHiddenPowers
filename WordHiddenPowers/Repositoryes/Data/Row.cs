using System;
using System.Collections.Generic;

namespace WordHiddenPowers.Repositoryes.Data
{
	public class Row : List<Cell>, IComparable<Row>
	{
		internal RowCollection parent;

		internal Row(RowCollection parent, int CellCount)
		{
			this.parent = parent;
			for (int i = 0; i < CellCount; i++)
			{
				base.Add(new Cell(this));
			}
		}

		public int Index
		{
			get
			{
				return parent.IndexOf(this);
			}
		}

		public new void Add(Cell item)
		{
			item.Row = this;
			base.Add(item);
		}

		public new void Insert(int index, Cell item)
		{
			item.Row = this;
			base.Insert(index, item);
		}

		public override bool Equals(object obj)
		{
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{ return false; }
			else
			{
				Row r = obj as Row;
				if (Count != r.Count)
				{
					return false;
				}
				else
				{
					for (int c = 0; c < Count; c++)
					{
						if (this[c].Value != r[c].Value)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static Row operator +(Row a, Row b)
		{
			for (int c = 0; c < a.Count; c++)
			{
				a[c].Value += b[c].Value;
			}
			return a;
		}

		public static Row operator -(Row a, Row b)
		{
			for (int c = 0; c < a.Count; c++)
			{
				a[c].Value -= b[c].Value;
			}
			return a;
		}

		public static Row operator *(Row row, int value)
		{
			for (int c = 0; c < row.Count; c++)
			{
				row[c].Value *= value;
			}
			return row;
		}

		public static Row operator /(Row row, int value)
		{
			for (int c = 0; c < row.Count; c++)
			{
				row[c].Value /= value;
			}
			return row;
		}

		public static bool operator ==(Row x, Row y)
		{
			return Compare(x, y) == 0;
		}

		public static bool operator !=(Row x, Row y)
		{
			return Compare(x, y) != 0;
		}

		public static bool operator >(Row x, Row y)
		{
			return Compare(x, y) > 0;
		}
		public static bool operator <(Row x, Row y)
		{
			return Compare(x, y) < 0;
		}

		public static bool operator >=(Row x, Row y)
		{
			return Compare(x, y) >= 0;
		}
		public static bool operator <=(Row x, Row y)
		{
			return Compare(x, y) <= 0;
		}

		public int CompareTo(Row value)
		{
			return Compare(this, value);
		}

		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Row r)
			{
				return r.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public static int Compare(Row x, Row y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					for (int c = 0; c < x.Count; c++)
					{
						int compare = x.CompareTo(y);
						if (compare != 0) return compare;
					}
					return 0;
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

		public class RowComparer : IComparer<Row>
		{
			public int Compare(Row x, Row y)
			{
				return Row.Compare(x, y);
			}
		}
	}
}
