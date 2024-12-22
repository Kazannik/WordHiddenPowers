using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;
using Point = System.Drawing.Point;
using WordHiddenPowers.Repositoryes.Notes;
using static WordHiddenPowers.Repositoryes.Notes.Note;

namespace WordHiddenPowers.Controls
{
	internal class NoteListBox : Control
	{
		//private ColorThemeList theme;
		private Rectangle periodRectagle;
		private Rectangle todateRectagle;
		private Rectangle todateTextRectagle;

		private readonly DateTimeFormatInfo FormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
		private readonly StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center };
		private int month_label_width = 0;
		private int month_label_height = 0;
		private const int month_label_border = 2;

		private NoteCollection notes;
		private Note HoveringNote;
		private Note LeftClickedNote;
		private Note RightClickedNote;
		private Note selectedNote;

		#region Constructor

		public NoteListBox() : base()
		{
			InitializeComponent();
			InitializeNotes();
		}

		public NoteListBox(string text) : base(text: text)
		{
			InitializeComponent();
			InitializeNotes();
		}

		public NoteListBox(Control parent, string text) : base(parent: parent, text: text)
		{
			InitializeComponent();
			InitializeNotes();
		}

		public NoteListBox(string text, int left, int top, int width, int height) : base(text: text, left: left, top: top, width: width, height: height)
		{
			InitializeComponent();
			InitializeNotes();
		}

		public NoteListBox(Control parent, string text, int left, int top, int width, int height) : base(parent: parent, text: text, left: left, top: top, width: width, height: height)
		{
			InitializeComponent();
			InitializeNotes();
		}

		private void InitializeNotes()
		{
			BackColor = SystemColors.Window;
			ForeColor = SystemColors.WindowText;

			notes = new NoteCollection(this);
			Graphics graphics = CreateGraphics();
			for (int i = 1; i <= 12; i++)
			{
				string monthname = FormatInfo.GetMonthName(i);
				SizeF labelsize = graphics.MeasureString(monthname, Font);
				if (month_label_height < labelsize.Height) month_label_height = (int)labelsize.Height;
				if (month_label_width < labelsize.Width) month_label_width = (int)labelsize.Width;
				notes.Add(new PeriodBoxButton(i, monthname));
			}
			month_label_height += month_label_border * 2;
			month_label_width += month_label_border * 2;

			if (month_label_width > 0)
			{
				Width = month_label_border + (month_label_width + month_label_border) * 3;
				Height = month_label_border + (month_label_height + month_label_border) * 6;
			}

			periodRectagle = new Rectangle(month_label_height + month_label_border, month_label_border, Width - month_label_height * 2 - month_label_border * 4, month_label_height);
			todateRectagle = new Rectangle(month_label_height + month_label_border, Height - month_label_height - month_label_border, (int)(month_label_height * 1.5), month_label_height);

			notes.Add(new PeriodBoxButton(0, ""));
			notes.Add(new PeriodBoxButton(0, ""));
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// NoteListBox
			// 
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NoteListBox_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NoteListBox_MouseDoubleClick);
			this.MouseLeave += new System.EventHandler(this.NoteListBox_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NoteListBox_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NoteListBox_MouseUp);
			this.Resize += new System.EventHandler(this.NoteListBox_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		#endregion

		private void NoteListBox_Resize(object sender, EventArgs e)
		{
			if (month_label_width > 0)
			{
				Width = month_label_border + (month_label_width + month_label_border) * 3;
				Height = month_label_border + (month_label_height + month_label_border) * 6;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;

			int startY = month_label_height + month_label_border * 2;
			int startX = month_label_border;
			int i = 0;
			for (int y = 0; y <= 3; y++)
			{
				for (int x = 0; x <= 2; x++)
				{
					Rectangle rec = new Rectangle(startX + (month_label_border + month_label_width) * x, startY + (month_label_border + month_label_height) * y, month_label_width, month_label_height);
					notes[i].Rectangle = rec;
					PaintNote(notes[i], graphics);
					i++;
				}
			}

			graphics.DrawString(period.ToShortDateString(), Font, new SolidBrush(ForeColor), periodRectagle, sf);

			Color BorderColor = GetBorderColor(NoteState.Selected);
			Pen BorderColorPen = new Pen(BorderColor, 1);
			graphics.DrawRectangle(BorderColorPen, todateRectagle);
			BorderColorPen.Dispose();
		}

		private void PaintNote(Note note, Graphics graphics)
		{
			Color ForeColor;
			if (note.Equals(HoveringNote))
			{
				if (LeftClickedNote == null)
				{
					if (note.Equals(SelectedItem))
					{
						FillNote(note, graphics, NoteState.Selected | NoteState.Hovering);
						ForeColor = GetForeColor(NoteState.Selected | NoteState.Hovering);
					}
					else
					{
						FillNote(note, graphics, NoteState.Hovering);
						ForeColor = GetForeColor(NoteState.Hovering);
					}
				}
				else
				{
					FillNote(note, graphics, NoteState.Selected | NoteState.Hovering);
					ForeColor = GetForeColor(NoteState.Selected | NoteState.Hovering);
				}
			}
			else
			{
				if (note.Equals(SelectedItem))
				{
					FillNote(note, graphics, NoteState.Selected);
					ForeColor = GetForeColor(NoteState.Selected);
				}
				else
				{
					FillNote(note, graphics, NoteState.Passive);
					ForeColor = GetForeColor(NoteState.Passive);
				}
			}

			if (note.ForeColor != ForeColor)
			{
				note.ForeColor = ForeColor;
				Brush ForeColorBrush = new SolidBrush(ForeColor);
				graphics.DrawString(note.Text, Font, ForeColorBrush, note.Rectangle, sf);
				ForeColorBrush.Dispose();
			}
		}

		private void FillNote(Note note, Graphics graphics, NoteState buttonState)
		{
			Color BackColor = GetNoteColor(buttonState, 0);
			if (note.BackColor != BackColor)
			{
				note.BackColor = BackColor;
				Brush BackColorBrush = new LinearGradientBrush(note.Rectangle, BackColor, GetNoteColor(buttonState, 1), LinearGradientMode.Vertical);
				graphics.FillRectangle(BackColorBrush, note.Rectangle);
				BackColorBrush.Dispose();
			}

			if (buttonState == NoteState.Passive) return;

			Color BorderColor = GetBorderColor(buttonState);
			if (note.BorderColor != BorderColor)
			{
				note.BorderColor = BorderColor;
				Pen BorderColorPen = new Pen(BorderColor, 1);
				graphics.DrawRectangle(BorderColorPen, note.Rectangle);
				BorderColorPen.Dispose();
			}
		}

		private Color GetNoteColor(NoteState buttonState, int colorIndex)
		{
			switch (buttonState)
			{
				case NoteState.Hovering | NoteState.Selected:
					if (colorIndex == 0) return Color.FromArgb(235, 244, 253);
					if (colorIndex == 1) return Color.FromArgb(194, 220, 253);
					break;
				case NoteState.Hovering:
					if (colorIndex == 0) return Color.FromArgb(253, 254, 255);
					if (colorIndex == 1) return Color.FromArgb(243, 248, 255);
					break;
				case NoteState.Selected:
					if (colorIndex == 0) return Color.FromArgb(249, 249, 249);
					if (colorIndex == 1) return Color.FromArgb(241, 241, 241);
					break;
				case NoteState.Passive:
					if (colorIndex == 0) return BackColor;
					if (colorIndex == 1) return BackColor;
					break;
				default:
					if (colorIndex == 0) return BackColor;
					if (colorIndex == 1) return BackColor;
					break;
			}
			return BackColor;
		}

		private Color GetBorderColor(NoteState buttonState)
		{
			switch (buttonState)
			{
				case NoteState.Hovering | NoteState.Selected:
					return Color.FromArgb(0, 102, 204);
				case NoteState.Hovering:
					return Color.FromArgb(185, 215, 252);
				case NoteState.Selected:
					return Color.FromArgb(0, 102, 204);
				case NoteState.Passive:
					return BackColor;
				default:
					return BackColor;
			}
		}

		private Color GetForeColor(NoteState buttonState)
		{
			switch (buttonState)
			{
				case NoteState.Hovering | NoteState.Selected:
					return Color.FromArgb(0, 102, 204);
				case NoteState.Hovering:
					return Color.FromArgb(0, 102, 204);
				case NoteState.Selected:
					return Color.FromArgb(0, 102, 204);
				case NoteState.Passive:
					return ForeColor;
				default:
					return ForeColor;
			}
		}

		private void NoteListBox_MouseClick(object sender, MouseEventArgs e)
		{
			RightClickedNote = null;
			Note note = notes[e.X, e.Y];
			if (note != null)
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						notes.SelectedItem = note;
						Invalidate();
						break;
					case MouseButtons.Right:
						RightClickedNote = note;
						Invalidate();
						break;
				}
				OnNoteMouseClick(new NoteMouseEventArgs(note, e));
			}
		}

		private void NoteListBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			RightClickedNote = null;
			Note note = notes[e.X, e.Y];
			if (note != null)
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						notes.SelectedItem = note;
						Invalidate();
						break;
					case MouseButtons.Right:
						RightClickedNote = note;
						Invalidate();
						break;
				}
				OnNoteMouseDoubleClick(new NoteMouseEventArgs(note, e));
			}			
		}

		private void NoteListBox_MouseLeave(object sender, EventArgs e)
		{
			if (RightClickedNote == null)
			{
				if (HoveringNote != null)
				{
					Rectangle rec = HoveringNote.Rectangle;
					HoveringNote = null;
					Invalidate(rec);
				}
			}
		}

		private void NoteListBox_MouseMove(object sender, MouseEventArgs e)
		{
			Note focusNote = notes[e.X, e.Y];
			if (focusNote != null)
			{
				Cursor = Cursors.Hand;
				if (HoveringNote != focusNote)
				{
					HoveringNote = focusNote;
					Invalidate(HoveringNote.Rectangle);
				}
			}
			else
			{
				if (HoveringNote != null)
				{
					Rectangle rec = HoveringNote.Rectangle;
					HoveringNote = null;
					Invalidate(rec);
				}
				this.Cursor = Cursors.Default;
			}
		}

		private void NoteListBox_MouseUp(object sender, MouseEventArgs e)
		{
			LeftClickedNote = null;
		}

		#region SelectedNote

		public Note SelectedItem
		{
			get
			{
				return notes.SelectedItem;
			}
			set
			{
				if (!Equals(notes.SelectedItem, value))
				{
					notes.SelectedItem = value;
					DoSelectedItemChanged();
				}
			}
		}

		public event EventHandler<NoteEventArgs> SelectedItemChanged;

		private void DoSelectedItemChanged()
		{
			Invalidate();
			OnSelectedItemChanged(new NoteEventArgs(SelectedItem));
		}

		protected virtual void OnSelectedItemChanged(NoteEventArgs e)
		{
			SelectedItemChanged?.Invoke(this, e);
		}

		#endregion

		#region Click
				
		public event EventHandler<NoteMouseEventArgs> NoteMouseClick;
		public event EventHandler<NoteMouseEventArgs> NoteMouseDoubleClick;
		public event EventHandler<NoteMouseEventArgs> NoteMouseDown;
		public event EventHandler<NoteMouseEventArgs> NoteMouseUp;

		protected virtual void OnNoteMouseClick(NoteMouseEventArgs e)
		{
			NoteMouseClick?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseDoubleClick(NoteMouseEventArgs e)
		{
			NoteMouseDoubleClick?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseDown(NoteMouseEventArgs e)
		{
			NoteMouseDown?.Invoke(this, e);
		}

		protected virtual void OnNoteMouseUp(NoteMouseEventArgs e)
		{
			NoteMouseUp?.Invoke(this, e);
		}


		#endregion

		internal class NoteCollection : CollectionBase
		{
			private readonly NoteListBox owner;

			public NoteCollection(NoteListBox owner) : base()
			{
				this.owner = owner;
			}

			public Note this[int index]
			{
				get { return (Note)List[index]; }
			}
						
			public Note this[int x, int y]
			{
				get
				{
					foreach (Note item in List)
					{
						if (item.Rectangle != null)
						{
							if (item.Rectangle.Contains(new Point(x, y)))
							{
								return item;
							}
						}
					}
					return null;
				}
			}

			public Note SelectedItem
			{
				get
				{
					return (from Note note in List where note.Selected select note).FirstOrDefault();
				}
				set
				{
					foreach (Note item in List)
					{
						item.Selected = item.Equals(value);						
					}				
				}
			}
			public int Add(Note item)
			{
				item.owner = owner;
				return List.Add(item);
			}

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
				"CA1062:ValidateArgumentsOfPublicMethods")]
			public void AddRange(NoteCollection items)
			{
				foreach (Note item in items)
				{
					Add(item);
				}
			}

			public int IndexOf(Note item)
			{
				return List.IndexOf(item);
			}

			public void Insert(int index, Note value)
			{
				List.Insert(index, value);
			}

			public void Remove(Note value)
			{
				List.Remove(value);
			}

			public bool Contains(Note value)
			{
				return List.Contains(value);
			}

			protected override void OnValidate(object value)
			{
				if (!typeof(Note).IsAssignableFrom(value.GetType()))
				{
					throw new ArgumentException("value не является типом Note.", "value");
				}
			}
		}

		internal enum NoteState : int
		{
			Passive = 0,
			Hovering = 1,
			Selected = 2
		}
				
	}
}
