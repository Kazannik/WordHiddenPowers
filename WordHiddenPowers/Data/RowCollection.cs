using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHiddenPowers.Data
{
    public class RowCollection : List<Row>
    {
        public new void Add(Row item)
        {
            item.parent = this;
            base.Add(item);
        }

        public new void Insert(int index, Row item)
        {
            item.parent = this;
            base.Insert(index, item);
        }       
    }
}
