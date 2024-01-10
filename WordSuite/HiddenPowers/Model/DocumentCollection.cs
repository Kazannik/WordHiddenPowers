using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHiddenPowers.Data;

namespace WordSuite.HiddenPowers.Model
{
    public class DocumentCollection: List<Document>
    {
        public Table SumTable { get; private set; }


        public new void Add(Document item)
        {
            if (base.Count==0)
            {
                SumTable = new Table(item.Table.Rows.Count, item.Table.ColumnCount);
            }

            for (int r = 0; r < SumTable.Rows.Count; r++)
            {
                for (int c = 0; c < SumTable.ColumnCount; c++)
                {
                    SumTable.Rows[r][c].Value += item.Table.Rows[r][c].Value;
                }
            }
            base.Add(item);
        }
    }
}
