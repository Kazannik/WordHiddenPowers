using System;
using System.Collections.Generic;

namespace WordHiddenPowers.Repository.Data
{
	public class Cell : IComparable<Cell>, IComparable<int>
	{
		internal Cell(Row row)
		{
			Row = row;
			NowValue = 0;
			LastValue = 0;
		}

		public int Index => Row.IndexOf(this);

		public Row Row { get; internal set; }

		public int NowValue { get; set; }

		public int LastValue { get; set; }

		public string Growth => ((NowValue - LastValue) > 0 ? "+" : "") + (NowValue - LastValue).ToString("### ### ###");

		public string GrowthPercent => LastValue != 0 ? ((NowValue - LastValue) > 0 ? "+" : "") + (((double)(NowValue - LastValue)) * 100 / LastValue).ToString("### ### ##0.00") + " %" : "-";

		public int ToInt() => NowValue;

		public static explicit operator int(Cell cell) => cell.NowValue;

		public override bool Equals(object obj)
		{
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{ return false; }
			else
			{
				Cell c = (Cell)obj;
				return NowValue == c.NowValue;
			}
		}

		public override int GetHashCode() => NowValue.GetHashCode();

		public static Cell operator +(Cell a, Cell b)
		{
			a.NowValue += b.NowValue;
			a.LastValue += b.LastValue;
			return a;
		}

		public static Cell operator -(Cell a, Cell b)
		{
			a.NowValue -= b.NowValue;
			a.LastValue -= b.LastValue;
			return a;
		}

		public static Cell operator +(Cell cell, int value)
		{
			cell.NowValue += value;
			return cell;
		}

		public static Cell operator -(Cell cell, int value)
		{
			cell.NowValue -= value;
			return cell;
		}

		public static Cell operator *(Cell cell, int value)
		{
			cell.NowValue *= value;
			return cell;
		}

		public static Cell operator /(Cell cell, int value)
		{
			cell.NowValue /= value;
			return cell;
		}

		public static bool operator ==(Cell x, Cell y) => Compare(x, y) == 0;

		public static bool operator !=(Cell x, Cell y) => Compare(x, y) != 0;

		public static bool operator >(Cell x, Cell y) => Compare(x, y) > 0;

		public static bool operator <(Cell x, Cell y) => Compare(x, y) < 0;

		public static bool operator >=(Cell x, Cell y) => Compare(x, y) >= 0;

		public static bool operator <=(Cell x, Cell y) => Compare(x, y) <= 0;

		public static bool operator ==(Cell cell, int value) => Compare(cell, value) == 0;

		public static bool operator !=(Cell cell, int value) => Compare(cell, value) != 0;

		public static bool operator >(Cell cell, int value) => Compare(cell, value) > 0;

		public static bool operator <(Cell cell, int value) => Compare(cell, value) < 0;

		public static bool operator >=(Cell cell, int value) => Compare(cell, value) >= 0;

		public static bool operator <=(Cell cell, int value) => Compare(cell, value) <= 0;

		public int CompareTo(Cell value) => Compare(this, value);

		public int CompareTo(int value) => Compare(this, value);

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
			if (!Equals(cell, null) && !Equals(value, null))
			{
				try
				{
					return decimal.Compare(cell.NowValue, value);
				}
				catch (Exception)
				{
					return 0;
				}
			}
			else if (!Equals(cell, null) && Equals(value, null))
			{
				return 1;
			}
			else if (Equals(cell, null) && !Equals(value, null))
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}

		public static int Compare(Cell x, Cell y)
		{
			if (!Equals(x, null) && !Equals(y, null))
			{
				try
				{
					return decimal.Compare(x.NowValue, y.NowValue);
				}
				catch (Exception)
				{
					return 0;
				}
			}
			else if (!Equals(x, null) && Equals(y, null))
			{
				return 1;
			}
			else if (Equals(x, null) && !Equals(y, null))
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}

		public class CellComparer : IComparer<Cell>
		{
			public int Compare(Cell x, Cell y) => Cell.Compare(x, y);
		}
	}
}
