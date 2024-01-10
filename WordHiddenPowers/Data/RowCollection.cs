using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHiddenPowers.Data
{
    public class RowCollection : IList<Row>
    {
        List<Row> List;

        public RowCollection()
        {
            List = new List<Row>();
        }

        public Row this[int index]
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

        public void Add(Row item)
        {
            item.parent = this;
            List.Add(item);
        }

        public void Clear()
        {
            List.Clear();
        }

        public bool Contains(Row item)
        {
            return List.Contains(item);
        }

        public void CopyTo(Row[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Row> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public int IndexOf(Row item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, Row item)
        {
            item.parent = this;
            List.Insert(index, item);
        }

        public bool Remove(Row item)
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
