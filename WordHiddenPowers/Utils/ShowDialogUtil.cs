using System.Diagnostics;
using System.Windows.Forms;

namespace WordHiddenPowers.Utils
{
	public static class ShowDialogUtil
	{
		public static DialogResult ShowDialog(Form form)
		{
			NativeWindow mainWindow = new NativeWindow();
			mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = form.ShowDialog(mainWindow);
			mainWindow.ReleaseHandle();
			return dialogResult;
		}

		public static DialogResult ShowDialog(CommonDialog dialog)
		{
			NativeWindow mainWindow = new NativeWindow();
			mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = dialog.ShowDialog(mainWindow);
			mainWindow.ReleaseHandle();
			return dialogResult;
		}

		public static DialogResult ShowErrorDialog(string text)
		{
			NativeWindow mainWindow = new NativeWindow();
			mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = MessageBox.Show(owner: mainWindow, text: text, caption: Const.Globals.ADDIN_TITLE, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
			mainWindow.ReleaseHandle();
			return dialogResult;
		}

		public static DialogResult ShowMessageDialog(string text)
		{
			NativeWindow mainWindow = new NativeWindow();
			mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			DialogResult dialogResult = MessageBox.Show(owner: mainWindow, text: text, caption: Const.Globals.ADDIN_TITLE, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			mainWindow.ReleaseHandle();
			return dialogResult;
		}

		public static void Show(Form form)
		{
			NativeWindow mainWindow = new NativeWindow();
			mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
			form.Show(mainWindow);
			mainWindow.ReleaseHandle();
		}		
	}
}
