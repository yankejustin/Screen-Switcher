using System;
using System.Runtime.InteropServices;

namespace prjScreenSwitcher.Utilities
{
    public static class Win32
    {
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(IntPtr WindowHandle, int flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Win32Types.RECT lpRect);
    }
}