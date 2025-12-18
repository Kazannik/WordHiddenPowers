using DocumentsGrinder.MsWord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentsGrinder
{
	public partial class AppForm : Form
	{
		public AppForm()
		{
			InitializeComponent();
		}

		private void ConvertToTextButton_Click(object sender, EventArgs e)
		{
			Converter converter = new Converter();
			converter.ToText("E:\\Kazannik.m.V6\\source\\repos\\Kazannik\\WordHiddenPowers\\DocumentsGrinder\\bin\\Debug\\Документ Microsoft Word.docx");
		}
	}
}
