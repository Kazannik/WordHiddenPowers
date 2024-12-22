using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Data;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableViewerDialog : Form
	{
		private readonly RepositoryDataSet dataSet;

		private List<Table> dataBase;

		private Table summTable;
		private double maxValue;

		public TableViewerDialog(RepositoryDataSet dataSet)
		{
			this.dataSet = dataSet;

			InitializeComponent();

			nameLabel.Text = "";

			tableEditBox.DataSet = this.dataSet;

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

			if (dataBase.Count > 0)
			{
				summTable = dataBase[0].Clone();
				listView1.Items.Clear();
				foreach (Table table in dataBase)
				{
					ListViewItem item = listView1.Items.Add(table.Caption);
					item.SubItems.Add("0");
					item.SubItems.Add("0");
					item.Tag = table;

					summTable += table;
				}

				dataBase.Add(summTable);

				ListViewItem summItem = listView1.Items.Add("Итого:");
				summItem.SubItems.Add("0");
				summItem.SubItems.Add("0");
				summItem.Tag = summTable;
				summItem.Font = new System.Drawing.Font(summItem.Font, System.Drawing.FontStyle.Bold);
				summItem.Selected = true;

				listView1.Refresh();
			}
		}

		private void TableEditBox_SelectedCell(object sender, Controls.TableEditBox.TableEventArgs e)
		{
			foreach (ListViewItem item in listView1.Items)
			{
				Table table = (Table)item.Tag;
				item.SubItems[1].Text = table.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value.ToString();
				item.SubItems[2].Text = (((double)table.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value) * 100 / ((double)summTable.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value)).ToString("0.00");
				maxValue = summTable.Rows[e.Cell.RowIndex][e.Cell.ColumnIndex].Value;
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
			sorter.MaxValue = columnIndex == 1 ? maxValue : 100;

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

				int result;

				if (lviX == null && lviY == null)
				{
					result = 0;
				}
				else if (lviX == null)
				{
					result = -1;
				}

				else if (lviY == null)
				{
					result = 1;
				}

				switch (ColumnIndex)
				{
					case 0:
						int xInt = GetIndex(lviX.SubItems[ColumnIndex].Text, SortDirection);
						int yInt = GetIndex(lviY.SubItems[ColumnIndex].Text, SortDirection);
						result = xInt.CompareTo(yInt);
						break;
					case 1:
					case 2:
						double xDbl = double.Parse(lviX.SubItems[ColumnIndex].Text);
						if (xDbl == MaxValue && SortDirection == SortOrder.Descending)
							xDbl = -1;
						double yDbl = double.Parse(lviY.SubItems[ColumnIndex].Text);
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
