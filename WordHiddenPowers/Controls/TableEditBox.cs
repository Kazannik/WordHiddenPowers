using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repository;
using WordHiddenPowers.Repository.Data;
using Font = System.Drawing.Font;

namespace WordHiddenPowers.Controls
{
	public partial class TableEditBox : UserControl
	{
		private RepositoryDataSet _source;
		private RepositoryDataSet _old;
		private Table _table;

		public TableEditBox()
		{
			InitializeComponent();
		}

		public bool ReadOnly { get; set; }

		public bool IsOld { get; private set; }

		public RepositoryDataSet DataSet
		{
			get
			{
				return _source;
			}
			set
			{
				_source = value;
				ReadStructure();
			}
		}

		public RepositoryDataSet OldDataSet
		{
			get
			{
				return _old;
			}
			set
			{
				_old = value;
				ReadStructure();
			}
		}

		public Table Table
		{
			get
			{
				return _table;
			}
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

			IsOld = false;

			if (_source == null) return;
			IsOld = _old != null && _old.IsTables;

			if (_source.ColumnsHeaders.Rows.Count > 0)
			{
				for (int i = 0; i < _source.ColumnsHeaders.Rows.Count; i++)
				{
					string text = _source.ColumnsHeaders.Rows[i]["Header"].ToString();
					int columnIndex = dataGridView.Columns.Add(text, text);
					dataGridView.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
					if (IsOld)
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

					foreach (DataRow item in _source.RowsHeaders.Rows)
					{
						int rowIndex = dataGridView.Rows.Add();
						dataGridView.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString() + ".   " + item["Header"].ToString();
					}

				for (int r = 0; r < _source.RowsHeaders.Rows.Count; r++)
				{
					bool rowBold = bool.Parse(_source.RowsHeaders.Rows[r]["Bold"].ToString());
					int rowColor = int.Parse(_source.RowsHeaders.Rows[r]["Color"].ToString());
					int rowBackColor = int.Parse(_source.RowsHeaders.Rows[r]["BackColor"].ToString());
					for (int c = 0; c < _source.ColumnsHeaders.Rows.Count; c++)
					{
						bool columnBold = bool.Parse(_source.ColumnsHeaders.Rows[c]["Bold"].ToString());
						int columnColor = int.Parse(_source.ColumnsHeaders.Rows[c]["Color"].ToString());
						int columnBackColor = int.Parse(_source.ColumnsHeaders.Rows[c]["BackColor"].ToString());

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
			int step = IsOld ? 4 : 1;
			for (int r = 0; r < dataGridView.RowCount; r++)
			{
				for (int c = 0; c < dataGridView.ColumnCount; c = c + step)
				{
					if (_table.RowCount > r && _table.ColumnCount > c / step)
					{
						dataGridView.Rows[r].Cells[c / step].Value = _table.Rows[r][c / step].Value;
						if (IsOld && _table.IsOld)
						{
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 1]).Text = _table.Rows[r][c / step].OldValue.ToString("### ### ###");
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 2]).Text = _table.Rows[r][c / step].Growth;
							((DataGridViewTextCell)dataGridView.Rows[r].Cells[c / step + 3]).Text = _table.Rows[r][c / step].GrowthPercent;
						}
						else if (IsOld && !_table.IsOld)
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
							_table.Rows[r][c].Value = int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString());
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
								if (_table.Rows[r][c].Value != int.Parse(dataGridView.Rows[r].Cells[c].Value.ToString()))
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

			private int GetTextWidth()
			{
				return TextRenderer.MeasureText(Text, OwningColumn.DefaultCellStyle.Font).Width;
			}
		}
	}
}

