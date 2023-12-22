using System.Diagnostics;
using System.Windows.Forms;

namespace WordHiddenPowers.Utils
{
    public static class ShowDialogUtil
    {
        public static IWin32Window GetOwner()
        {
            NativeWindow mainWindow = new NativeWindow();
            mainWindow.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            return mainWindow;            
        }        
    }
}
