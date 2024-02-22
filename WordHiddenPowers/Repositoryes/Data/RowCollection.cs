using System.Collections.Generic;

namespace WordHiddenPowers.Repositoryes.Data
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
