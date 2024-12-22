using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repositoryes;
using WordHiddenPowers.Repositoryes.Data;

namespace WordHiddenPowers.Controls
{
	public partial class TableEditBox : UserControl
	{
		private RepositoryDataSet source;
		private Table table;

		public TableEditBox()
		{
			InitializeComponent();
		}

		public bool ReadOnly { get; set; }

		public RepositoryDataSet DataSet
		{
			get
			{
				return source;
			}
			set
			{
				source = value;
				ReadStructure();
			}
		}

		public Table Table
		{
			get
			{
				return table;
			}
			set
			{
				table = value;
				RefreshValues();
			}
		}

		private void ReadStructure()
		{
			try
			{
				dataGridView.Rows.Clear();
				dataGridView.Columns.Clear();
			}
			catch (Exception)
			{
				return;
			}

			if (source == null) return;
			if (source.ColumnsHeaders.Rows.Count > 0)
			{
				for (int i = 0; i < source.ColumnsHeaders.Rows.Count; i++)
				{
					string text = source.ColumnsHeaders.Rows[i]["Header"].ToString();
					int columnIndex = dataGridView.Columns.Add(text, text);
					dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
				}

				foreach (DataRow item in source.RowsHeaders.Rows)
				{
					int rowIndex = dataGridView.Rows.Add();
					dataGridView.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString() + ".   " + item["Header"].ToString();
				}

				for (int r = 0; r < source.RowsHeaders.Rows.Count; r++)
				{
					bool rowBold = bool.Parse(source.RowsHeaders.Rows[r]["Bold"].ToString());
					int rowColor = int.Parse(source.RowsHeaders.Rows[r]["Color"].ToString());
					int rowBackColor = int.Parse(source.RowsHeaders.Rows[r]["BackColor"].ToString());
					for (int c = 0; c < source.ColumnsHeaders.Rows.Count; c++)
					{
						bool columnBold = bool.Parse(source.ColumnsHeaders.Rows[c]["Bold"].ToString());
						int columnColor = int.Parse(source.ColumnsHeaders.Rows[c]["Color"].ToString());
						int columnBackColor = int.Parse(source.ColumnsHeaders.Rows[c]["BackColor"].ToString());

						dataGridView.Rows[r].Cells[c].ReadOnly = ReadOnly;

						// -16777216

						if (rowBold || columnBold)
						{
							if (dataGridView.Rows[r].Cells[c].Style.Font.Style != FontStyle.Bold)
								dataGridView.Rows[r].Cells[c].Style.Font = new Font(dataGridView.Rows[r].Cells[c].Style.Font, FontStyle.Bold);
							else
								dataGridView.Rows[r].Cells[c].Style.Font = new Font(dataGridView.Rows[r].Cells[c].Style.Font, FontStyle.Regular);
						}
					}
				}
			}
		}

		public void ClearValues()
		{
			dataGridView.CancelEdit();
			for (int r = 0; r < dataGridView.RowCount; r++)
			{
				for (int c = 0; c < dataGridView.ColumnCount; c++)
				{
					dataGridView.Rows[r].Cells[c].Value = 0;
				}
			}
		}

		public void RefreshValues()
		{
			dataGridView.CancelEdit();
			if (table == null) return;
			for (int r = 0; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.ColumnCount; c++)
				{
					dataGridView.Rows[r].Cells[c].Value = table.Rows[r][c].Value;
				}
			}
		}

		public void CommitValue()
		{
			dataGridView.CancelEdit();
			if (table == null) return;
			for (int r = 0; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.ColumnCount; c++)
				{
					if (dataGridView.Rows[r].Cells[c].Value != null)
					{
						table.Rows[r][c].Value = int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString());
					}
				}
			}

			if (IsChanged == true)
			{
				IsChanged = false;
				DoValueChanged();
			}
		}

		public bool IsChanged { get; private set; }

		private void Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
			{
				bool correct = int.TryParse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out _);
				if (!correct)
				{
					dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
				}

				bool changed = false;
				for (int r = 0; r < table.Rows.Count; r++)
				{
					for (int c = 0; c < table.ColumnCount; c++)
					{
						if (dataGridView.Rows[r].Cells[c].Value != null)
						{
							if (table.Rows[r][c].Value != int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString()))
							{
								changed = true;
								break;
							}
						}
					}
				}

				if (IsChanged != changed)
				{
					IsChanged = changed;
					DoValueChanged();
				}
			}
		}

		public event EventHandler ValueChanged;

		public void DoValueChanged()
		{
			OnValueChanged(new EventArgs());
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
			ValueChanged?.Invoke(this, e);
		}

		private void DataGridView_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView.SelectedCells.Count > 0)
				DoSelectedCell();
		}

		public event EventHandler<TableEventArgs> SelectedCell;

		public void DoSelectedCell()
		{
			OnSelectedCell(new TableEventArgs(dataGridView.SelectedCells[0]));
		}

		protected virtual void OnSelectedCell(TableEventArgs e)
		{
			SelectedCell?.Invoke(this, e);
		}
		public class TableEventArgs : EventArgs
		{
			public TableEventArgs(DataGridViewCell cell) : base()
			{
				Cell = cell;
			}

			public DataGridViewCell Cell { get; }
		}
	}
}

