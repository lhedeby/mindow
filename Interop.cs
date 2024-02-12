using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Interop
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    public static nint GetCurrentWindowHwnd()
    {
        return GetForegroundWindow();
    }

    public static Window[] GetCurrentWindows()
    {
        return Process
            .GetProcesses()
            .Where(process => !string.IsNullOrEmpty(process.MainWindowTitle))
            .Select(ProcessToWindow)
            .ToArray();
    }

    public static void SetWindowPosition(Window window)
    {
        MoveWindow(
            window.MainWindowHandle,
            window.Rect.X,
            window.Rect.Y,
            window.Rect.XEnd - window.Rect.X,
            window.Rect.YEnd - window.Rect.Y,
            true
        );
    }

    public static Window ProcessToWindow(Process process)
    {
        var rect = new Rect();
        var hWnd = process.MainWindowHandle;
        GetWindowRect(hWnd, ref rect);
        return new Window
        {
            MainWindowTitle = process.MainWindowTitle,
            MainWindowHandle = process.MainWindowHandle,
            Rect = rect
        };
    }
}

