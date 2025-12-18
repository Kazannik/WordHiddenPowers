using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Documents;

namespace WordHiddenPowers.Panes
{
	[DesignerCategory("code")]
	public class WordHiddenPowersPane : UserControl
	{
		private IContainer components;

		public readonly Document Document;

		protected WordHiddenPowersPane()
		{
			InitializeComponent();
		}

		public WordHiddenPowersPane(Document document)
		{
			Document = document;

			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new Container();

			SuspendLayout();

			AutoScaleDimensions = new SizeF(8F, 16F);
			AutoScaleMode = AutoScaleMode.Font;
			Margin = new Padding(3, 2, 3, 2);
			Name = "WordHiddenPowersPane";
			Size = new Size(325, 322);
			ResumeLayout(false);
		}

		public event EventHandler<EventArgs> PropertiesChanged;

		protected virtual void OnPropertiesChanged(EventArgs e)
		{
			PropertiesChanged?.Invoke(this, e);
		}
	}
}

