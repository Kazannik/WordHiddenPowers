using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHiddenPowers.Data
{
    public class Row : List<Cell>
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
    }
}
