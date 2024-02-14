using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WordHiddenPowers.Controls
{
    [DesignerCategory("code")]
    [ToolboxBitmap(typeof(ComboBox))]
    [ComVisible(false)]
    public abstract class ComboControl <T> : ComboBox where T: ComboControl<T>.IComboBoxItem
    {
        protected StringFormat sfCode;
        protected StringFormat sfCaption;

        #region Initialize

        [DebuggerNonUserCode()]
        public ComboControl(IContainer container) :this()
        {
            if (container != null) { container.Add(this); }
        }

        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                { components.Dispose(); }
            }
            finally
            { base.Dispose(disposing); }
        }

        private IContainer components;

        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new Container();
        }

        public ComboControl():base()
        {
            sfCode = (StringFormat)StringFormat.GenericTypographic.Clone();
            sfCode.Alignment = StringAlignment.Center;
            sfCode.LineAlignment = StringAlignment.Center;
            sfCode.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;

            sfCaption = (StringFormat)StringFormat.GenericTypographic.Clone();
            sfCaption.Alignment = StringAlignment.Near;
            sfCaption.LineAlignment = StringAlignment.Near;
            sfCaption.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
            
            InitializeComponent();

            base.DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            MaxDropDownItems = 20;
            DropDownWidth = 80;
            base.AutoSize = false;
            Width = 80;
            DropDownHeight = 180;
            Items.Clear();
        }

        #endregion

        #region Draw Item

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics graphics = e.Graphics;

            SizeF CodeSize = graphics.MeasureString("FFFFFFF", Font);
            ItemHeight = (int)CodeSize.Height + SystemInformation.BorderSize.Height * 4;
            DropDownHeight = ItemHeight * 8 + SystemInformation.BorderSize.Height * 4;

            Rectangle rectSelection = new Rectangle(e.Bounds.X + 1, e.Bounds.Y, e.Bounds.Width - 3, e.Bounds.Height - 1);
            Rectangle rectCode = new Rectangle(rectSelection.X + 2, rectSelection.Y + 2, (int)CodeSize.Width, rectSelection.Height - 4);
            Rectangle rectText = new Rectangle(rectCode.X + rectCode.Width + 6, rectCode.Y, e.Bounds.Width - rectCode.X - rectCode.Width - 6, rectCode.Height);

            Size TextSize = new Size(rectText.Width - SystemInformation.VerticalScrollBarWidth - 8, rectText.Height);


            Brush backCodeBrush, foreCodeBrush, backCaptionBrush, foreCaptionBrush;
            Pen borderPen;

            if (e.Index == -1)
            {
                backCodeBrush = SystemBrushes.Control;
                foreCodeBrush = SystemBrushes.ControlText;
                foreCaptionBrush = new SolidBrush(ForeColor);
                backCaptionBrush = new SolidBrush(BackColor);
                borderPen = new Pen(ForeColor);

                graphics.FillRectangle(backCaptionBrush, e.Bounds);
                graphics.FillRectangle(backCodeBrush, rectCode);
                graphics.DrawRectangle(borderPen, rectCode);
                graphics.DrawString("", Font, foreCodeBrush, rectCode, sfCode);
                graphics.DrawString("(не выбрано)", Font, foreCaptionBrush, rectText, sfCaption);
                return;
            }

            if (Items.Count <= e.Index) return;
            int itemCode = this[e.Index].Code;
            string itemCodeString = (itemCode + 1).ToString();
            string itemCaptionString = this[e.Index].Text.Trim();
            if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
            {
                backCodeBrush = new SolidBrush(BackColor);
                foreCodeBrush = new SolidBrush(ForeColor);
                foreCaptionBrush = new SolidBrush(ForeColor);
                backCaptionBrush = new SolidBrush(BackColor);
                borderPen = new Pen(ForeColor);
            }
            else
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    backCodeBrush = new LinearGradientBrush(rectCode, Color.FromArgb(249, 249, 249), Color.FromArgb(241, 241, 241), LinearGradientMode.Vertical);
                    foreCodeBrush = new SolidBrush(Color.FromArgb(0, 102, 204));
                    backCaptionBrush = SystemBrushes.Highlight;
                    foreCaptionBrush = SystemBrushes.HighlightText;
                    borderPen = new Pen(Color.FromArgb(0, 102, 204), 1);
                }
                else
                {
                    backCodeBrush = new SolidBrush(BackColor);
                    foreCodeBrush = new SolidBrush(ForeColor);
                    backCaptionBrush = new SolidBrush(BackColor);
                    foreCaptionBrush = new SolidBrush(ForeColor);
                    borderPen = new Pen(ForeColor, 1);
                }
            }
            graphics.FillRectangle(backCaptionBrush, e.Bounds);
            graphics.FillRectangle(backCodeBrush, rectCode);
            graphics.DrawRectangle(borderPen, rectCode);
            graphics.DrawString(itemCodeString, Font, foreCodeBrush, rectCode, sfCode);
            graphics.DrawString(itemCaptionString, Font, foreCaptionBrush, rectText, sfCaption);
        }

        #endregion

        [ReadOnly(true)]
        public T this[int index]
        {
            get
            {
                return (T)Items[index: index];
            }
        }

        public T SelectedContent
        {
            get
            {
                if (SelectedItem != null)
                {
                    return (T)SelectedItem;
                }
                else
                {
                    return default(T);
                }
            }
        }

        [ReadOnly(true)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
        }
                
        protected void Insert(int index, T item)
        {
            Items.Insert(index, item);
        }

        protected void Remove(T value)
        {
            Items.Remove(value);
        }
        
        public int Add(T item)
        {
            foreach (T i in Items)
            {
                if (i.Code == item.Code)
                {
                    throw new ArgumentException("Элемент с кодом [" + item.Code.ToString() + "] ранее добавлен в коллекцию.", "item.Code");
                }
            }
            return Items.Add(item);
        }
       
        public bool Contains(int code)
        {
            foreach (T i in Items)
            {
                if (i.Code == code)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetItem(int code)
        {
            foreach (T i in Items)
            {
                if (i.Code == code)
                {
                    return i;
                }
            }
            return default(T);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            OnSelectedItemChanged(new ComboBoxItemEventArgs(SelectedContent));
            base.OnSelectedIndexChanged(e);
        }

        public interface IComboBoxItem
        {
            int Code { get; }
            string Text { get; }
        }

        public event EventHandler<ComboBoxItemEventArgs> SelectedItemChanged;

        protected virtual void OnSelectedItemChanged(ComboBoxItemEventArgs e)
        {
            SelectedItemChanged?.Invoke(this, e);
        }

        public class ComboBoxItemEventArgs : EventArgs
        {
            public ComboBoxItemEventArgs(T item): base()
            {
                Item = item;
            }

            public T Item { get; }
        }
    }
}
