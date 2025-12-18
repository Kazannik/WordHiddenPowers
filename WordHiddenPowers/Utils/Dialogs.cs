// Ignore Spelling: Utils Dialogs

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


#if WORD
namespace WordHiddenPowers.Utils
#else
namespace ProsecutorialSupervision.Utils
#endif
{
	static class Dialogs
	{
		private static List<object> dialogCollection;

		private static void AddDialog(object item)
		{
			if (dialogCollection == null) 
				dialogCollection = new List<object>();
			dialogCollection.Add(item);
		}
		
		/// <summary>
		/// Принудительное закрытие всех модальных окон и высвобождение ресурсов.
		/// </summary>
		public static void CloseAllDialogs()
		{
			if (dialogCollection == null) return;

			foreach (object item in dialogCollection)
			{
				if (item is Form form)
				{
					form.Close();
					form.Dispose();
				}
				else if (item is CommonDialog dialog)
				{
					dialog.Dispose();
				}
			}
		}

		/// <summary>
		/// Отображает форму в виде модального диалогового окна для главного окна активного процесса.
		/// </summary>
		/// <param name="form">Форма, отображаемая в виде модального диалогового окна.</param>
		/// <returns></returns>
		public static DialogResult ShowDialog(Form form)
		{
			NativeWindow ownerWindow = new NativeWindow();
			ownerWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			AddDialog(form);
			DialogResult dialogResult = form.ShowDialog(ownerWindow);
			ownerWindow.ReleaseHandle();
			return dialogResult;
		}

		/// <summary>
		/// Запускает общее диалоговое окно для главного окна активного процесса.
		/// </summary>
		/// <param name="dialog">Запускаемое общее диалоговое окно.</param>
		/// <returns></returns>
		public static DialogResult ShowDialog(CommonDialog dialog)
		{
			NativeWindow ownerWindow = new NativeWindow();
			ownerWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			AddDialog(dialog);
			DialogResult dialogResult = dialog.ShowDialog(ownerWindow);
			ownerWindow.ReleaseHandle();
			return dialogResult;
		}

		/// <summary>
		/// Отображает окно сообщения об ошибке для главного окна активного процесса.
		/// </summary>
		/// <param name="text">Текст, отображаемый в окне сообщения об ошибке.</param>
		/// <returns></returns>
		public static DialogResult ShowErrorDialog(string text)
		{
			NativeWindow ownerWindow = new NativeWindow();
			ownerWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = MessageBox.Show(owner: ownerWindow, text: text, caption: Const.Globals.ADDIN_TITLE, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
			ownerWindow.ReleaseHandle();
			return dialogResult;
		}

		/// <summary>
		/// Отображает окно информационного сообщения для главного окна активного процесса. 
		/// </summary>
		/// <param name="text">Текст, отображаемый в окне информационного сообщения.</param>
		/// <returns></returns>
		public static DialogResult ShowMessageDialog(string text)
		{
			NativeWindow ownerWindow = new NativeWindow();
			ownerWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = MessageBox.Show(owner: ownerWindow, text: text, caption: Const.Globals.ADDIN_TITLE, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			ownerWindow.ReleaseHandle();
			return dialogResult;
		}

		/// <summary>
		/// Показывает форму для главного окна активного процесса.
		/// </summary>
		/// <param name="form">Отображаемая форма.</param>
		public static void Show(Form form)
		{
			NativeWindow ownerWindow = new NativeWindow();
			ownerWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			AddDialog(form);
			form.Show(ownerWindow);
			ownerWindow.ReleaseHandle();
		}		
	}
}
