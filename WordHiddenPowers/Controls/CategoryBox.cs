using System;
using System.Drawing;
using System.Windows.Forms;
using WordHiddenPowers.Repository.Categories;

namespace WordHiddenPowers.Controls
{
    public partial class CategoryBox: UserControl
    {
        private object _owner;
		private Size codeSize;
		private Size textSize;

		public CategoryBox()
        {
            InitializeComponent();
        }

        public object Owner
        {
            get => _owner;
            set
            {
				_owner = value;
				if (_owner !=null && _owner is Category category)
				{
					descriptionTextBox.Text = category.Description;
				}
				else if (_owner != null && _owner is Subcategory subcategory)
				{
					descriptionTextBox.Text = subcategory.Description;
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
			if (_owner is Category category)
			{
				e.Graphics.DrawString(category.Text, Font, new SolidBrush(ForeColor), 0, 0);
			}
			else if (_owner is Subcategory subcategory)
			{
				e.Graphics.DrawString(subcategory.Text, Font, new SolidBrush(ForeColor), 0, 0);
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
