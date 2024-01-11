using System;
using System.Collections.Generic;

namespace WordHiddenPowers.Data
{
    public class Cell: IComparable<Cell>, IComparable<int>
    {
        internal Cell(Row row)
        {
            this.Row = row;
            this.Value = 0;
        }

       public int Index { get { return Row.IndexOf(this); } }

        public Row Row { get; internal set; }

        public int Value { get; set; }
        
               
        public int ToInt()
        {
            return this.Value;
        }
        
        public static explicit operator int(Cell cell)
        {
            return cell.Value;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            { return false; }
            else
            {
                Cell c = (Cell)obj;
                return (this.Value == c.Value);
            }
        }

        public override int GetHashCode()
        {            
            return Value.GetHashCode();
        }

        public static Cell operator +(Cell a, Cell b)
        {
            a.Value += b.Value;
            return a;
        }

        public static Cell operator -(Cell a, Cell b)
        {
            a.Value -= b.Value;
            return a;
        }

        public static Cell operator +(Cell cell, int i)
        {
            cell.Value += i;
            return cell;
        }

        public static Cell operator -(Cell cell, int i)
        {
            cell.Value -= i;            
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

        public static bool operator ==(Cell x, int y)
        {
            return Compare(x, y) == 0;
        }

        public static bool operator !=(Cell x, int y)
        {
            return Compare(x, y) != 0;
        }

        public static bool operator >(Cell x, int y)
        {
            return Compare(x, y) > 0;
        }
        public static bool operator <(Cell x, int y)
        {
            return Compare(x, y) < 0;
        }

        public static bool operator >=(Cell x, int y)
        {
            return Compare(x, y) >= 0;
        }
        public static bool operator <=(Cell x, int y)
        {
            return Compare(x, y) <= 0;
        }

        public int CompareTo(Cell value)
        {
            return Compare(this, value);
        }

        public int CompareTo(int value)
        {
            return Compare(this, value);
        }

        public int CompareTo(Object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (value is Cell)
            {
                Cell c = (Cell)value;
                return c.CompareTo(value);
            }
            throw new ArgumentException();
        }

        public static int Compare(Cell x, int y)
        {
            if (!Equals(x, null) & !Equals(y, null))
            {
                try
                {
                    return Decimal.Compare(x.Value, y);
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

        public static int Compare(Cell x, Cell y)
        {
            if (!Equals(x, null) & !Equals(y, null))
            {
                try
                {
                    return Decimal.Compare(x.Value, y.Value);                    
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
