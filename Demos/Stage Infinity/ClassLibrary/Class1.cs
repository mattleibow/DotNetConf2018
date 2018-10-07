using Microsoft.Win32;

namespace ClassLibrary
{
    public static class Class1
    {
        public static string Message = "Hello from .NET Framework!";

        public static bool IsNotepadStatusBarVisible()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Notepad"))
            {
                return (int)key?.GetValue("StatusBar") == 1;
            }
        }
    }
}
