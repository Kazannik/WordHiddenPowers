using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WordHiddenPowers.Repositories;
using WordHiddenPowers.Repositories.Data;
using static Tensorboard.TensorShapeProto.Types;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableViewerDialog : Form
	{
		private readonly RepositoryDataSet dataSet;
		private readonly RepositoryDataSet oldDataSet;

		private List<Table> dataBase;
		private List<Table> oldDataBase;

		private Table summTable;
		private double maxValue;
		private double maxOldValue;

		public TableViewerDialog(RepositoryDataSet dataSet) : this(dataSet: dataSet, oldDataSet: null) { }
		
		public TableViewerDialog(RepositoryDataSet dataSet, RepositoryDataSet oldDataSet)
		{
			this.dataSet = dataSet;
			this.oldDataSet = oldDataSet;

			InitializeComponent();

			nameLabel.Text = "";

			tableEditBox.DataSet = this.dataSet;
			tableEditBox.OldDataSet = this.oldDataSet;

			listView1.Columns[1].Width = 120;
			listView1.Columns[2].Width = 100;

			if (tableEditBox.OldDataSet != null &&
				tableEditBox.OldDataSet.IsTables)
			{
				listView1.Columns.Add("АППГ").Width = 120;
				listView1.Columns.Add("%").Width = 100;
				listView1.Columns.Add("+/-").Width = 100;
				listView1.Columns.Add("%").Width = 120;
			}

			for (int i = 1; i < listView1.Columns.Count; i++)
			{
				listView1.Columns[i].TextAlign = HorizontalAlignment.Right;
			}

			InitializeDatabase();
		}


		private void InitializeDatabase()
		{
			if (dataSet == null) return;

			dataBase = new List<Table>();
			foreach (RepositoryDataSet.DocumentKeysRow row in dataSet.DocumentKeys)
			{
				Table table = Table.Create(row.Description2, row.Caption, row.Description1);
				if (!table.IsEmpty) dataBase.Add(table);
			}
			
			oldDataBase = new List<Table>();
			if (oldDataBase !=null && oldDataSet.IsTables)
			{				
				foreach (RepositoryDataSet.DocumentKeysRow row in oldDataSet.DocumentKeys)
				{
					Table table = Table.Create(row.Description2, row.Caption, row.Description1);
					if (!table.IsEmpty) oldDataBase.Add(table);
				}
			}

			if (dataBase.Count > 0)
			{
				summTable = dataBase[0].Clone();
				Table oldTable = GetOldTable(dataBase[0].Caption);
				if (oldTable != null) 
					summTable.AddOldData(oldTable);

				listView1.Items.Clear();
				foreach (Table table in dataBase)
				{
					oldTable = GetOldTable(table.Caption);
					if (oldTable != null)
						table.AddOldData(oldTable);

					ListViewItem item = listView1.Items.Add(table.Caption);
					item.SubItems.Add("0");
					item.SubItems.Add("0");
					if (oldTable != null)
					{
						item.SubItems.Add("0");
						item.SubItems.Add("0");
						item.SubItems.Add("0");
						item.SubItems.Add("0");
					}
					item.Tag = table;
					summTable += table;
				}

				dataBase.Add(summTable);

				ListViewItem summItem = listView1.Items.Add("Итого:");
				summItem.SubItems.Add("0");
				summItem.SubItems.Add("0");
				if (oldTable != null)
				{
					summItem.SubItems.Add("0");
					summItem.SubItems.Add("0");
					summItem.SubItems.Add("0");
					summItem.SubItems.Add("0");
				}
				summItem.Tag = summTable;
				summItem.Font = new System.Drawing.Font(summItem.Font, System.Drawing.FontStyle.Bold);
				summItem.Selected = true;

				listView1.Refresh();
			}
		}

		private Table GetOldTable(string caption)
		{
			if (oldDataBase.Any(t => t.Caption == caption))
			{
				return oldDataBase.Where(t=> t.Caption == caption).FirstOrDefault();
			}
			else
			{
				return null;
			}
		}

		private void TableEditBox_SelectedCell(object sender, Controls.TableEditBox.TableEventArgs e)
		{
			foreach (ListViewItem item in listView1.Items)
			{
				Table table = (Table)item.Tag;

				int columnIndex = e.Cell.ColumnIndex;
				if (summTable.IsOld)
				{
					columnIndex = Math.DivRem(e.Cell.ColumnIndex, 4, out _);
				}	

				Cell cell = table.Rows[e.Cell.RowIndex][columnIndex];
				Cell summCell = summTable.Rows[e.Cell.RowIndex][columnIndex];

				item.SubItems[1].Text = cell.Value.ToString("### ### ###");
				item.SubItems[2].Text = (((double)cell.Value) * 100 / summCell.Value).ToString("### ### ##0.00");
				
				if (table.IsOld)
				{
					item.SubItems[3].Text = cell.OldValue.ToString("### ### ###");
					item.SubItems[4].Text = (((double)cell.OldValue) * 100 / summCell.OldValue).ToString("### ### ##0.00");
					item.SubItems[5].Text = cell.Growth;
					item.SubItems[6].Text = cell.GrowthPercent;
				}
				maxValue = summCell.Value;
				maxOldValue = summCell.OldValue;
			}
		}

		private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				Table table = (Table)listView1.SelectedItems[0].Tag;
				tableEditBox.Table = table;
				nameLabel.Text = listView1.SelectedItems[0].Text;
			}
		}

		private void TableEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
		{

		}

		private Word.Variable GetVariable(Word.Variables array, string variableName)
		{
			for (int i = 1; i <= array.Count; i++)
			{
				if (array[i].Name == variableName)
				{
					return array[i];
				}
			}
			return null;
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			tableEditBox.ClearValues();
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			tableEditBox.RefreshValues();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			tableEditBox.CommitValue();
		}

		private void TableEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (tableEditBox.IsChanged)
				{
					DialogResult result = MessageBox.Show("Зафиксировать табличные данные?", "Табличные данные", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						tableEditBox.CommitValue();
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListViewItemComparer sorter = GetListViewSorter(e.Column, dataBase);

			listView1.ListViewItemSorter = sorter;
			listView1.Sort();
		}

		private ListViewItemComparer GetListViewSorter(int columnIndex, List<Table> dataBase)
		{
			ListViewItemComparer sorter = (ListViewItemComparer)listView1.ListViewItemSorter ?? new ListViewItemComparer(dataBase);
			sorter.ColumnIndex = columnIndex;
			sorter.MaxValue = (columnIndex == 1 || columnIndex == 3 || columnIndex == 5) ? maxValue : 100;

			if (sorter.SortDirection == SortOrder.Ascending)
			{
				sorter.SortDirection = SortOrder.Descending;
			}
			else
			{
				sorter.SortDirection = SortOrder.Ascending;
			}
			return sorter;
		}

		public class ListViewItemComparer : IComparer
		{
			private readonly List<Table> dataBase;

			public double MaxValue { get; set; }

			public int ColumnIndex { get; set; }

			public SortOrder SortDirection { get; set; }

			public ListViewItemComparer(List<Table> dataBase)
			{
				this.dataBase = dataBase;
				SortDirection = SortOrder.None;
			}

			public int Compare(object x, object y)
			{
				ListViewItem lviX = x as ListViewItem;
				ListViewItem lviY = y as ListViewItem;
				int xInt, yInt;
				double xDbl, yDbl;
				int result;				

				switch (ColumnIndex)
				{
					case 0:
						xInt = GetIndex(lviX.SubItems[ColumnIndex].Text, SortDirection);
						yInt = GetIndex(lviY.SubItems[ColumnIndex].Text, SortDirection);
						result = xInt.CompareTo(yInt);
						break;
					case 1:
					case 2:
					case 3:
					case 4:
						xDbl = ColumnIndex < lviX.SubItems.Count ? double.Parse(lviX.SubItems[ColumnIndex].Text) : 0;
						if (xDbl == MaxValue && SortDirection == SortOrder.Descending)
							xDbl = -1;
						yDbl = ColumnIndex < lviY.SubItems.Count ? double.Parse(lviY.SubItems[ColumnIndex].Text) : 0;
						if (yDbl == MaxValue && SortDirection == SortOrder.Descending)
							yDbl = -1;
						result = xDbl.CompareTo(yDbl);
						break;
					case 5:
					case 6:
						xDbl = (ColumnIndex < lviX.SubItems.Count &&
							!string.IsNullOrWhiteSpace(lviX.SubItems[ColumnIndex].Text)) ? double.Parse(lviX.SubItems[ColumnIndex].Text.Replace("%", "").Replace(" ", "").Replace("+","")) : 0;
						if (xDbl == MaxValue && SortDirection == SortOrder.Descending)
							xDbl = -1;
						yDbl = (ColumnIndex < lviY.SubItems.Count &&
							!string.IsNullOrWhiteSpace(lviY.SubItems[ColumnIndex].Text)) ? double.Parse(lviY.SubItems[ColumnIndex].Text.Replace("%","").Replace(" ","").Replace("+", "")) : 0;
						if (yDbl == MaxValue && SortDirection == SortOrder.Descending)
							yDbl = -1;
						result = xDbl.CompareTo(yDbl);
						break;

					default:
						result = string.Compare(
							lviX.SubItems[ColumnIndex].Text,
							lviY.SubItems[ColumnIndex].Text,
							false);

						break;
				}

				if (SortDirection == SortOrder.Descending)
				{
					return -result;
				}
				else
				{
					return result;
				}
			}

			private int GetIndex(string Caption, SortOrder sortOrder)
			{
				for (int i = 0; i < dataBase.Count; i++)
				{
					if (dataBase[i].Caption == Caption)
						return i;
				}
				if (sortOrder == SortOrder.Ascending ||
					sortOrder == SortOrder.None)
				{
					return dataBase.Count - 1;
				}
				else
				{
					return -1;
				}
			}
		}
	}
}
