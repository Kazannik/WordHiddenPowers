using System;
using System.Collections.Generic;


namespace WordHiddenPowers.Repository.Data
{
	public class TableCollection : List<(Table current, Table old)>
	{
		public TableCollection() { }

		private class Values
		{
			public int LocationId { get; set; }

			public DateTime Period { get; set; }

			public int RowId { get; set; }

			public int ColumnId { get; set; }

		}
	}
}
