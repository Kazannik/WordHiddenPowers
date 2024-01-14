using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
    public partial class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            InitializeComponent();
            base.Multiline = false;
        }

        public NumericTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            base.Multiline = false;
            base.MaxLength = 15;
            base.ScrollBars = ScrollBars.None;
        }

        [Browsable(false), ReadOnly(true)]
        public new bool Multiline
        {
            get { return base.Multiline; }
        }

        [Browsable(false), ReadOnly(true)]
        public new int MaxLength
        {
            get { return base.MaxLength; }
        }

        [Browsable(false), ReadOnly(true)]
        public new ScrollBars ScrollBars
        {
            get { return base.ScrollBars; }
        }

        bool IsNumeric(string s)
        {
            double output;
            return double.TryParse(s, out output);
        }

        public double Value
        {
            get
            {
                double output;
                if(double.TryParse(base.Text, out output))
                {
                    return output;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                base.Text = value.ToString();
            }
        }
                         
        protected override void OnKeyPress(KeyPressEventArgs e)
        {           
            base.OnKeyPress(e);
            Regex regex = new Regex("\\d+([,]\\d{1,}){0,1}");
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = !regex.IsMatch(this.Text + e.KeyChar.ToString());
            }
            else if (this.Text.IndexOf(',') < 0 && e.KeyChar == ',')
            {
                e.Handled = false;
            }
            else if (!Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        string oldText = string.Empty;

        protected override void OnTextChanged(EventArgs e)
        {            
            Regex regex = new Regex("\\d+([,]\\d{1,}){0,1}");
            if (regex.IsMatch(this.Text))
            {
                oldText = Text;
                base.OnTextChanged(e);                               
            }
            else if (Text.Length == 0 || Text == ",")
            {
                oldText = Text;
                base.OnTextChanged(e);
            }
            else
            {
                Text = oldText;
            }
        }
    }
}
