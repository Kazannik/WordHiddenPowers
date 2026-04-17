using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Data;
using Font = System.Drawing.Font;

namespace WordHiddenPowers.Controls
{
	public partial class TableEditBox : UserControl
	{
		private RepositoryDataSet _nowDataSet;
		private RepositoryDataSet _lastDataSet;
		private Table _table;

		public TableEditBox()
		{
			InitializeComponent();
		}

		public bool ReadOnly { get; set; }

		public bool IsLast { get; private set; }

		public RepositoryDataSet NowDataSet
		{
			get => _nowDataSet;
			set
			{
				_nowDataSet = value;
				ReadStructure();
			}
		}

		public RepositoryDataSet LastDataSet
		{
			get => _lastDataSet;
			set
			{
				_lastDataSet = value;
				ReadStructure();
			}
		}

		public Table Table
		{
			get => _table;
			set
			{
				_table = value;
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

			IsLast = false;

			if (_nowDataSet == null) return;
			IsLast = _lastDataSet != null && _lastDataSet.IsTables;

			if (_nowDataSet.ColumnsHeaders.Rows.Count > 0)
			{
				for (int i = 0; i < _nowDataSet.ColumnsHeaders.Rows.Count; i++)
				{
					string text = _nowDataSet.ColumnsHeaders.Rows[i]["Header"].ToString();
					int columnIndex = dataGridView.Columns.Add(text, text);
					dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
					if (IsLast)
					{
						columnIndex = dataGridView.Columns.Add(text + "_old", "АППГ");
						dataGridView.Columns[columnIndex].CellTemplate = new DataGridViewTextCell();
						dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
						dataGridView.Columns[columnIndex].ReadOnly = true;

						columnIndex = dataGridView.Columns.Add(text + "_growth", "+/-");
						dataGridView.Columns[columnIndex].CellTemplate = new DataGridViewTextCell();
						dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
						dataGridView.Columns[columnIndex].ReadOnly = true;

						columnIndex = dataGridView.Columns.Add(text + "_percent", "%");
						dataGridView.Columns[columnIndex].CellTemplate = new DataGridViewTextCell();
						dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
						dataGridView.Columns[columnIndex].ReadOnly = true;
					}
				}

				foreach (DataRow item in _nowDataSet.RowsHeaders.Rows)
				{
					int rowIndex = dataGridView.Rows.Add();
					dataGridView.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString() + ".   " + item["Header"].ToString();
				}

				for (int r = 0; r < _nowDataSet.RowsHeaders.Rows.Count; r++)
				{
					bool rowBold = bool.Parse(_nowDataSet.RowsHeaders.Rows[r]["Bold"].ToString());
					int rowColor = int.Parse(_nowDataSet.RowsHeaders.Rows[r]["Color"].ToString());
					int rowBackColor = int.Parse(_nowDataSet.RowsHeaders.Rows[r]["BackColor"].ToString());
					for (int c = 0; c < _nowDataSet.ColumnsHeaders.Rows.Count; c++)
					{
						bool columnBold = bool.Parse(_nowDataSet.ColumnsHeaders.Rows[c]["Bold"].ToString());
						int columnColor = int.Parse(_nowDataSet.ColumnsHeaders.Rows[c]["Color"].ToString());
						int columnBackColor = int.Parse(_nowDataSet.ColumnsHeaders.Rows[c]["BackColor"].ToString());

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
			if (_table == null) return;
			dataGridView.CancelEdit();
			int step = IsLast ? 4 : 1;
			for (int r = 0; r < dataGridView.RowCount; r++)
			{
				for (int c = 0; c < dataGridView.ColumnCount; c = c + step)
				{
					if (_table.RowCount > r && _table.ColumnCount > c / step)
					{
						dataGridView.Rows[r].Cells[c / step].Value = _table.Rows[r][c / step].NowValue;
						if (IsLast && _table.IsLast)
						{
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 1]).Text = _table.Rows[r][c / step].LastValue.ToString("### ### ###");
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 2]).Text = _table.Rows[r][c / step].Growth;
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 3]).Text = _table.Rows[r][c / step].GrowthPercent;
						}
						else if (IsLast && !_table.IsLast)
						{
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 1]).Text = "-";
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 2]).Text = "-";
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 3]).Text = "-";
						}
					}
					else
					{
						dataGridView.Rows[r].Cells[c / step].Value = 0;
					}
				}
			}
			dataGridView.Update();
			dataGridView.Invalidate();
		}

		public void CommitValue()
		{
			dataGridView.EndEdit();
			if (_table == null) return;
			for (int r = 0; r < dataGridView.RowCount; r++)
			{
				for (int c = 0; c < dataGridView.ColumnCount; c++)
				{
					if (dataGridView.Rows[r].Cells[c].Value != null)
					{
						if (_table.RowCount > r && _table.ColumnCount > c)
						{
							_table.Rows[r][c].NowValue = int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString());
						}
					}
				}
			}

			if (IsChanged == true)
			{
				IsChanged = false;
				DoValueChanged();
			}
		}

		public void EndEdit() => dataGridView.EndEdit();

		public bool IsChanged { get; private set; }

		public void Copy()
		{
			string[,] array = new string[dataGridView.Rows.Count + 1, dataGridView.ColumnCount + 1];

			foreach (DataGridViewColumn column in dataGridView.Columns)
			{
				array[0, column.Index + 1] = column.HeaderText;
			}

			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				array[row.Index + 1, 0] = row.HeaderCell.Value.ToString();

				foreach (DataGridViewCell cell in row.Cells)
				{
					if (cell is DataGridViewTextBoxCell textBoxCell && textBoxCell.Value != null)
					{
						array[row.Index + 1, cell.ColumnIndex + 1] = ((int)textBoxCell.Value).ToString("### ### ###");
					}
					else if (cell is DataGridViewTextCell textCell)
					{
						array[row.Index + 1, cell.ColumnIndex + 1] = textCell.Text;
					}
				}
			}
			WordHiddenPowers.Utils.HTMLClipboard.Copy(array, Encoding.Default, 1);
		}

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
				for (int r = 0; r < dataGridView.RowCount; r++)
				{
					for (int c = 0; c < dataGridView.ColumnCount; c++)
					{
						if (dataGridView.Rows[r].Cells[c].Value != null)
						{
							if (_table.RowCount > r && _table.ColumnCount > c)
							{
								if (_table.Rows[r][c].NowValue != int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString()))
								{
									changed = true;
									break;
								}
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

		public void DoValueChanged() => OnValueChanged(new EventArgs());

		protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

		private void DataGridView_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView.SelectedCells.Count > 0)
				DoSelectedCell();
		}

		public event EventHandler<TableEventArgs> SelectedCell;

		public void DoSelectedCell() => OnSelectedCell(new TableEventArgs(dataGridView.SelectedCells[0]));

		protected virtual void OnSelectedCell(TableEventArgs e) => SelectedCell?.Invoke(this, e);

		public class TableEventArgs : EventArgs
		{
			public TableEventArgs(DataGridViewCell cell) : base() => Cell = cell;

			public DataGridViewCell Cell { get; }
		}


		public class DataGridViewTextCell : DataGridViewTextBoxCell
		{
			public string Text { get; set; }

			protected override void Paint(Graphics graphics, Rectangle clipBounds,
				Rectangle cellBounds, int rowIndex,
				DataGridViewElementStates elementState,
				object value, object formattedValue, string errorText,
				DataGridViewCellStyle cellStyle,
				DataGridViewAdvancedBorderStyle advancedBorderStyle,
				DataGridViewPaintParts paintParts)
			{


				base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState,
					value, Text, errorText, cellStyle, advancedBorderStyle,
					DataGridViewPaintParts.All &
					~DataGridViewPaintParts.ContentBackground);

				//var r1 = DataGridView.GetCellDisplayRectangle(OwningColumn.Index, rowIndex, false);

				//var r3 = new Rectangle(r1.Location, new Size(GetTextWidth(), r1.Height));
				//contentBounds.Offset(r1.Location);

				//base.Paint(graphics, clipBounds, contentBounds, rowIndex, elementState,
				//	"value", "Text", errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.All);


			}

			//protected override Rectangle GetContentBounds(Graphics graphics,
			//	DataGridViewCellStyle cellStyle, int rowIndex)
			//{
			//	int textWidth = GetTextWidth();
			//	Rectangle contentBounds = base.GetContentBounds(graphics, cellStyle, rowIndex);
			//	return new Rectangle(contentBounds.Left + textWidth, contentBounds.Top, contentBounds.Width - textWidth, contentBounds.Height);
			//}

			//protected override void OnContentClick(DataGridViewCellEventArgs e)
			//{
			//	base.OnContentClick(e);
			//	DataGridViewColumn owningColumn = OwningColumn;
			//	Rectangle contentBounds = GetContentBounds(e.RowIndex);
			//	Rectangle cellDisplayRectangle = DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

			//	Point position = new Point(cellDisplayRectangle.Left + contentBounds.Left, cellDisplayRectangle.Top + contentBounds.Bottom);
			//	owningColumn.ContextMenuStrip?.Show(DataGridView, position);
			//}

			private int GetTextWidth() => TextRenderer.MeasureText(Text, OwningColumn.DefaultCellStyle.Font).Width;

		}
	}
}

