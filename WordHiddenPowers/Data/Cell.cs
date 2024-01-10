using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHiddenPowers.Data
{
    public class Cell
    {
        internal Cell(Row row)
        {
            this.Row = row;
            this.Value = 0;
        }

       public int Index { get { return Row.IndexOf(this); } }

        public Row Row { get; internal set; }

        public int Value { get; set; }

    }
}
