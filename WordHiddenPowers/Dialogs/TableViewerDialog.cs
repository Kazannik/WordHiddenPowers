using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WordHiddenPowers.Controls;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Data;
using Word = Microsoft.Office.Interop.Word;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableViewerDialog : Form
	{
		private readonly RepositoryDataSet nowDataSet;
		private readonly RepositoryDataSet lastDataSet;

		private List<Table> nowDataBase;
		private List<Table> lastDataBase;

		private Table summTable;
		private double maxNowValue;
		private double maxLastValue;

		public TableViewerDialog(RepositoryDataSet nowDataSet) : this(nowDataSet: nowDataSet, lastDataSet: null) { }

		public TableViewerDialog(RepositoryDataSet nowDataSet, RepositoryDataSet lastDataSet)
		{
			this.nowDataSet = nowDataSet;
			this.lastDataSet = lastDataSet;

			InitializeComponent();

			nameLabel.Text = "";

			tableEditBox.NowDataSet = this.nowDataSet;
			tableEditBox.LastDataSet = this.lastDataSet;

			listView1.Columns[1].Width = 120;
			listView1.Columns[2].Width = 100;

			if (tableEditBox.LastDataSet != null &&
				tableEditBox.LastDataSet.IsTables)
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
			if (nowDataSet == null) return;

			nowDataBase = new List<Table>();
			foreach (RepositoryDataSet.DocumentKeysRow row in nowDataSet.DocumentKeys)
			{
				Table table = Table.Create(row.Description2, row.Caption, row.Description1);
				if (!table.IsEmpty) nowDataBase.Add(table);
			}

			lastDataBase = new List<Table>();
			if (lastDataBase != null && lastDataSet.IsTables)
			{
				foreach (RepositoryDataSet.DocumentKeysRow row in lastDataSet.DocumentKeys)
				{
					Table table = Table.Create(row.Description2, row.Caption, row.Description1);
					if (!table.IsEmpty) lastDataBase.Add(table);
				}
			}

			if (nowDataBase.Count > 0)
			{
				summTable = nowDataBase[0].Clone();
				Table lastTable = GetLastTable(nowDataBase[0].Caption);
				if (lastTable != null)
					summTable.AddLastData(lastTable);

				listView1.Items.Clear();
				foreach (Table table in nowDataBase)
				{
					lastTable = GetLastTable(table.Caption);
					if (lastTable != null)
						table.AddLastData(lastTable);

					ListViewItem item = listView1.Items.Add(table.Caption);
					item.SubItems.Add("0");
					item.SubItems.Add("0");
					if (lastTable != null)
					{
						item.SubItems.Add("0");
						item.SubItems.Add("0");
						item.SubItems.Add("0");
						item.SubItems.Add("0");
					}
					item.Tag = table;
					summTable += table;
				}

				nowDataBase.Add(summTable);

				ListViewItem summItem = listView1.Items.Add("Итого:");
				summItem.SubItems.Add("0");
				summItem.SubItems.Add("0");
				if (lastTable != null)
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

				IEnumerable<string> exceptTables = lastDataBase.Select(x => x.Caption).Except(nowDataBase.Select(x => x.Caption));
			}
		}

		private Table GetLastTable(string caption)
		{
			if (lastDataBase.Any(t => t.Caption == caption))
			{
				return lastDataBase.Where(t => t.Caption == caption).FirstOrDefault();
			}
			else
			{
				return default;
			}
		}

		private void TableEditBox_SelectedCell(object sender, Controls.TableEditBox.TableEventArgs e)
		{
			foreach (ListViewItem item in listView1.Items)
			{
				Table table = (Table)item.Tag;

				int columnIndex = e.Cell.ColumnIndex;
				if (summTable.IsLast)
				{
					columnIndex = Math.DivRem(e.Cell.ColumnIndex, 4, out _);
				}

				Cell cell = table.Rows[e.Cell.RowIndex][columnIndex];
				Cell summCell = summTable.Rows[e.Cell.RowIndex][columnIndex];

				item.SubItems[1].Text = cell.NowValue.ToString("### ### ###");
				item.SubItems[2].Text = (((double)cell.NowValue) * 100 / summCell.NowValue).ToString("### ### ##0.00") + " %";

				if (table.IsLast)
				{
					item.SubItems[3].Text = cell.LastValue.ToString("### ### ###");
					item.SubItems[4].Text = (((double)cell.LastValue) * 100 / summCell.LastValue).ToString("### ### ##0.00") + " %";
					item.SubItems[5].Text = cell.Growth;
					item.SubItems[6].Text = cell.GrowthPercent;
				}
				maxNowValue = summCell.NowValue;
				maxLastValue = summCell.LastValue;
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

		private void ClearButton_Click(object sender, EventArgs e) => tableEditBox.ClearValues();

		private void RefreshButton_Click(object sender, EventArgs e) => tableEditBox.RefreshValues();

		private void SaveButton_Click(object sender, EventArgs e) => tableEditBox.CommitValue();

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
			ListViewItemComparer sorter = GetListViewSorter(e.Column, nowDataBase);

			listView1.ListViewItemSorter = sorter;
			listView1.Sort();
		}

		private ListViewItemComparer GetListViewSorter(int columnIndex, List<Table> dataBase)
		{
			ListViewItemComparer sorter = (ListViewItemComparer)listView1.ListViewItemSorter ?? new ListViewItemComparer(dataBase);
			sorter.ColumnIndex = columnIndex;
			sorter.MaxValue = (columnIndex == 1 || columnIndex == 3 || columnIndex == 5) ? maxNowValue : 100;

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
						xDbl = ColumnIndex < lviX.SubItems.Count ? double.Parse(GetText(lviX.SubItems[ColumnIndex].Text)) : 0;
						if (xDbl == MaxValue && SortDirection == SortOrder.Descending)
							xDbl = -1;
						yDbl = ColumnIndex < lviY.SubItems.Count ? double.Parse(GetText(lviY.SubItems[ColumnIndex].Text)) : 0;
						if (yDbl == MaxValue && SortDirection == SortOrder.Descending)
							yDbl = -1;
						result = xDbl.CompareTo(yDbl);
						break;
					case 5:
					case 6:
						xDbl = ColumnIndex < lviX.SubItems.Count ? double.Parse(GetText(lviX.SubItems[ColumnIndex].Text)) : 0;
						if (xDbl == MaxValue && SortDirection == SortOrder.Descending)
							xDbl = -1;
						yDbl = ColumnIndex < lviY.SubItems.Count ? double.Parse(GetText(lviY.SubItems[ColumnIndex].Text)) : 0;
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


			private readonly static Regex replaceRegex = new Regex("(\\s|[%]|[+])", RegexOptions.Compiled & RegexOptions.IgnoreCase & RegexOptions.Multiline);

			private string GetText(string value)
			{
				value = replaceRegex.Replace(value, "");
				return string.IsNullOrWhiteSpace(value) || value == "-" || value == "нечисло" ? "0" : value;
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

		private void Copy_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
			ContextMenuStrip context = menuItem.Owner as ContextMenuStrip;
			if (context.SourceControl is TableEditBox tableBox)
			{
				tableBox.Copy();
			}
			else if (context.SourceControl is ListView)
			{
				Copy();
			}
		}


		public void Copy()
		{
			string[,] array = new string[listView1.Items.Count + 1, listView1.Columns.Count];

			foreach (ColumnHeader column in listView1.Columns)
			{
				array[0, column.Index] = column.Text;
			}

			foreach (ListViewItem row in listView1.Items)
			{
				for (int i = 0; i < row.SubItems.Count; i++)
				{
					array[row.Index + 1, i] = row.SubItems[i].Text;
				}
			}
			Utils.HTMLClipboard.Copy(array, Encoding.Default, 2);
		}
	}
}
