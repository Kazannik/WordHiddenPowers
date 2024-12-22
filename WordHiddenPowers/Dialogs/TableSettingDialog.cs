using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableSettingDialog : Form
	{
		public TableSettingDialog()
		{
			InitializeComponent();
		}

		public int ColumnsCount { get { return (int)columnCountNumericUpDown.Value; } }
		public int RowsCount { get { return (int)rowCountNumericUpDown.Value; } }
	}
}
