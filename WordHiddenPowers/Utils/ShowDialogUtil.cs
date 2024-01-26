using System.Diagnostics;
using System.Windows.Forms;

namespace WordHiddenPowers.Utils
{
    public static class ShowDialogUtil
    {
        public static DialogResult ShowDialog(Form dialog)
        {
            NativeWindow mainWindow = new NativeWindow();
            mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            DialogResult dialogResult = dialog.ShowDialog(mainWindow);
            mainWindow.ReleaseHandle();
            return dialogResult;
        }

        public static DialogResult ShowDialogObj(dynamic dialog)
        {
            NativeWindow mainWindow = new NativeWindow();
            mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            DialogResult dialogResult = dialog.ShowDialog(mainWindow);
            mainWindow.ReleaseHandle();
            return dialogResult;
        }
    }
}
