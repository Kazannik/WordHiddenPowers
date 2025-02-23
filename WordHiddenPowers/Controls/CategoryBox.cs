using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordHiddenPowers.Repositories.Categories;

namespace WordHiddenPowers.Controls
{
    public partial class CategoryBox: UserControl
    {
        private object owner;

		private Version code;
		private Size codeSize;
		private Size textSize;

		public CategoryBox()
        {
            InitializeComponent();
        }

        public object Owner
        {
            get => owner;
            set
            {
				owner = value;
				if (owner !=null && owner is Category)
				{
					descriptionTextBox.Text = ((Category)owner).Description;
				}
				else if (owner != null && owner is Subcategory)
				{
					descriptionTextBox.Text = ((Subcategory)owner).Description;
				}
				else
				{
					descriptionTextBox.Text = string.Empty;
				}
					Invalidate();
            }
        }

		protected override void OnPaint(PaintEventArgs e)
		{
			if (owner is Category)
			{
				e.Graphics.DrawString(((Category)owner).Text, Font, new SolidBrush(ForeColor), 0, 0);
			}
			else if (owner is Subcategory)
			{
				e.Graphics.DrawString(((Subcategory)owner).Text, Font, new SolidBrush(ForeColor), 0, 0);
			}
			base.OnPaint(e);
		}

		private Size OnMeasureBound(Graphics graphics, Font font, int itemWidth, int itemHeight)
		{
			Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
			SizeF measure = graphics.MeasureString("XXX.XXX", boldFont, itemWidth - 3, Utils.Drawing.CENTER_STRING_FORMAT);

			codeSize = new Size((int)measure.Width, (int)measure.Height);
			textSize = Utils.Drawing.GetTextSize(graphics, Text, font, itemWidth - (int)measure.Height - (int)measure.Width - 5, Utils.Drawing.CENTER_STRING_FORMAT);

			if (codeSize.Height < textSize.Height)
			{
				return new Size(itemWidth, textSize.Height + 8);
			}
			else
			{
				return new Size(itemWidth, codeSize.Height + 8);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			descriptionTextBox.Location = new Point(0, 40);
			descriptionTextBox.Size = new Size(Width, Height - descriptionTextBox.Top);
		}
	}
}
