using System;
using System.Collections.Generic;

namespace WordHiddenPowers.Repositories.Data
{
	public class Cell : IComparable<Cell>, IComparable<int>
	{
		internal Cell(Row row)
		{
			Row = row;
			Value = 0;
			OldValue = 0;
		}

		public int Index { get { return Row.IndexOf(this); } }

		public Row Row { get; internal set; }

		public int Value { get; set; }

		public int OldValue { get; set; }

		public string Growth => ((Value - OldValue) > 0 ? "+":"") + (Value - OldValue).ToString("### ### ###");

		public string GrowthPercent => OldValue != 0 ? ((Value - OldValue) > 0 ? "+" : "") +(((double)(Value - OldValue)) * 100 / OldValue).ToString("### ### ##0.00") + " %" : "-";

		public int ToInt()
		{
			return Value;
		}

		public static explicit operator int(Cell cell)
		{
			return cell.Value;
		}

		public override bool Equals(object obj)
		{
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{ return false; }
			else
			{
				Cell c = (Cell)obj;
				return Value == c.Value;
			}
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static Cell operator +(Cell a, Cell b)
		{
			a.Value += b.Value;
			a.OldValue += b.OldValue;
			return a;
		}

		public static Cell operator -(Cell a, Cell b)
		{
			a.Value -= b.Value;
			a.OldValue -= b.OldValue;
			return a;
		}

		public static Cell operator +(Cell cell, int value)
		{
			cell.Value += value;
			return cell;
		}

		public static Cell operator -(Cell cell, int value)
		{
			cell.Value -= value;
			return cell;
		}

		public static bool operator ==(Cell x, Cell y)
		{
			return Compare(x, y) == 0;
		}

		public static bool operator !=(Cell x, Cell y)
		{
			return Compare(x, y) != 0;
		}

		public static bool operator >(Cell x, Cell y)
		{
			return Compare(x, y) > 0;
		}
		public static bool operator <(Cell x, Cell y)
		{
			return Compare(x, y) < 0;
		}

		public static bool operator >=(Cell x, Cell y)
		{
			return Compare(x, y) >= 0;
		}
		public static bool operator <=(Cell x, Cell y)
		{
			return Compare(x, y) <= 0;
		}

		public static bool operator ==(Cell cell, int value)
		{
			return Compare(cell, value) == 0;
		}

		public static bool operator !=(Cell cell, int value)
		{
			return Compare(cell, value) != 0;
		}

		public static bool operator >(Cell cell, int value)
		{
			return Compare(cell, value) > 0;
		}
		public static bool operator <(Cell cell, int value)
		{
			return Compare(cell, value) < 0;
		}

		public static bool operator >=(Cell cell, int value)
		{
			return Compare(cell, value) >= 0;
		}
		public static bool operator <=(Cell cell, int value)
		{
			return Compare(cell, value) <= 0;
		}

		public int CompareTo(Cell value)
		{
			return Compare(this, value);
		}

		public int CompareTo(int value)
		{
			return Compare(this, value);
		}

		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Cell c)
			{
				return c.CompareTo(value);
			}
			throw new ArgumentException();
		}

		public static int Compare(Cell cell, int value)
		{
			if (!Equals(cell, null) & !Equals(value, null))
			{
				try
				{
					return decimal.Compare(cell.Value, value);
				}
				catch (Exception)
				{ return 0; }
			}
			else if (!Equals(cell, null) & Equals(value, null))
			{ return 1; }
			else if (Equals(cell, null) & !Equals(value, null))
			{ return -1; }
			else { return 0; }
		}

		public static int Compare(Cell x, Cell y)
		{
			if (!Equals(x, null) & !Equals(y, null))
			{
				try
				{
					return decimal.Compare(x.Value, y.Value);
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

		public class CellComparer : IComparer<Cell>
		{
			public int Compare(Cell x, Cell y)
			{
				return Cell.Compare(x, y);
			}
		}
	}
}
