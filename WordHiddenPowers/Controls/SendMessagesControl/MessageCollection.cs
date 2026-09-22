using System;
using System.Collections;
using System.ComponentModel;

namespace WordHiddenPowers.Controls.SendMessagesControl
{
	/// <summary>
	/// Коллекция ответов моделей.
	/// </summary>
	public class MessageCollection : CollectionBase
	{
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler StatusChanged;
		public void DoStatusChanged() => StatusChanged?.Invoke(this, new EventArgs());

		private int position = 0;
		public void Add(string message)
		{
			List.Add(message);
			position = List.Count - 1;
			SetStatus(position);
		}

		public void Clear()
		{
			List.Clear();
			position = -1;
			SetStatus(position);
		}

		/// <summary>
		/// Повторить.
		/// </summary>
		public bool IsRepeat { get; private set; } = false;

		/// <summary>
		/// Отменить.
		/// </summary>
		public bool IsUndo { get; private set; } = false;

		/// <summary>
		/// Отменить.
		/// </summary>
		public bool IsRedo { get; private set; } = false;

		public string GetUndo()
		{
			if (position > 0)
			{
				position--;
				return SetStatus(position);
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		public string GetRedo()
		{
			if (position < List.Count)
			{
				position++;
				return SetStatus(position);
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		private string SetStatus(int index)
		{
			bool newRepeat = index >= 0 &&
				index == List.Count - 1;
			bool newUno = index > 0;
			bool newRego = index >= 0 &&
				index < List.Count - 1;
			if (newRepeat != IsRepeat ||
				newUno != IsUndo ||
				newRego != IsRedo)
			{
				IsRepeat = newRepeat;
				IsUndo = newUno;
				IsRedo = newRego;
				DoStatusChanged();
			}
			return index < 0 ? string.Empty : List[index] as string;
		}

		public string Current => List[position] as string;

		public void CurrentDelete()
		{
			List.RemoveAt(position);
		}		
	}
}
