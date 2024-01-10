using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHiddenPowers.Data
{
    public class Row : IList<Cell>
    {

        List<Cell> List;

        internal RowCollection parent;

        internal Row(RowCollection parent, int CellCount)
        {
            List = new List<Cell>();
            for (int i = 0; i < CellCount; i++)
            {
                List.Add(new Cell(this));
            }
        }
        
        public int Index { get { return parent.IndexOf(this); } }


        public Cell this[int index]
        {
            get
            {
                return List[index];
            }

            set
            {
                List[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Cell item)
        {
            item.Row = this;
            List.Add(item);
        }

        public void Clear()
        {
            List.Clear();
        }

        public bool Contains(Cell item)
        {
            return List.Contains(item);
        }

        public void CopyTo(Cell[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public int IndexOf(Cell item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, Cell item)
        {
            item.Row = this;
            List.Insert(index, item);
        }

        public bool Remove(Cell item)
        {
            return List.Remove(item);
        }

        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}
