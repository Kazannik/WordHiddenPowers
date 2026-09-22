using System.Windows.Forms;

namespace WordHiddenPowers.Dialogs
{
	public partial class TableSettingDialog : Form
	{
		public TableSettingDialog()
		{
			InitializeComponent();
			okButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
			cancelButton.Size = Const.Globals.ACTION_BUTTON_SIZE;
		}

		public int ColumnsCount { get { return (int)columnCountNumericUpDown.Value; } }
		public int RowsCount { get { return (int)rowCountNumericUpDown.Value; } }
	}
}
