using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bender.Controllers
{
    public class ProcessHelper
    {
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);


        public static void SetFocusToExternalApp(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            var process = processes
                .ToList()
                .FirstOrDefault(x => x.MainWindowHandle != IntPtr.Zero);
            if (process == null && processes.Length > 0)
            {
                process = processes[0];
            }
            if (process != null)
            {
                Thread.Sleep(100);
                ShowWindow(process.Handle, ShowWindowEnum.Restore);
                SetForegroundWindow(process.MainWindowHandle);
                SetWindowsPosition(process);
            }
        }

        private static void SetWindowsPosition(Process process)
        {
            IntPtr handle = process.MainWindowHandle;
            if (handle != IntPtr.Zero)
            {
                RECT rct;
                GetWindowRect(handle, out rct);
                var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                int top = ((int)desktopWorkingArea.Bottom - (rct.Bottom - rct.Top)/2);
                SetWindowPos(handle, IntPtr.Zero, 0, top, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
            }
        }
    }
}
