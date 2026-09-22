using System;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;

namespace TestControlApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			///authenticationProfileListBox1.Add();
			///

			LLMConnectSettingDialog settingDialog = new LLMConnectSettingDialog();
			settingDialog.ShowDialog();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			//connectionControlBox1.Uri = new Uri("http://localhost:11434/");
		}
	}
}
