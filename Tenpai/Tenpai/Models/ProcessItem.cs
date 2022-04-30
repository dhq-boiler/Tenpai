

using Tenpai.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Tenpai.Models
{
    public class ProcessItem
    {
        private List<WindowInfo> _WindowInfos = new List<WindowInfo>();

        public Process Process { get; set; }

        public ProcessItem(Process p)
        {
            this.Process = p;
        }

        internal WindowInfo[] FoundWindows
        {
            get { return _WindowInfos.ToArray(); }
        }

        internal int EnumerateWindows(IntPtr hWnd, int lParam)
        {
            uint procId = 0;
            uint result = NativeMethods.GetWindowThreadProcessId(hWnd, ref procId);
            if (procId == Process.Id)
            {
                GetWindowInfo(hWnd);
                NativeMethods.EnumChildWindows(hWnd, EnumerateChildWindows, IntPtr.Zero);
            }
            return 1;
        }

        internal bool EnumerateChildWindows(IntPtr hWnd, IntPtr lParam)
        {
            GetWindowInfo(hWnd);
            return true;
        }

        private void GetWindowInfo(IntPtr hWnd)
        {
            int textLen = NativeMethods.GetWindowTextLength(hWnd);
            if (textLen > 0)
            {
                StringBuilder sb = new StringBuilder(textLen + 1);
                NativeMethods.GetWindowText(hWnd, sb, sb.Capacity);
                _WindowInfos.Add(new WindowInfo(hWnd, sb.ToString()));
            }
            else
            {
                _WindowInfos.Add(new WindowInfo(hWnd));
            }
        }

        public override string ToString()
        {
            try
            {
                return string.Format($"[{Process.Id}]{Process.ProcessName}.exe - {Process.MainWindowTitle}");
            }
            catch (Win32Exception)
            {
                return string.Format("[{0}]{1} - {2}", Process.Id, "#Win32Exception#", Process.MainWindowTitle);
            }
        }

        public class WindowInfo
        {
            public string WindowTitle { get; set; }
            public IntPtr WindowHandle { get; set; }

            public WindowInfo()
            { }

            public WindowInfo(IntPtr windowHandle)
            {
                this.WindowHandle = windowHandle;
            }

            public WindowInfo(IntPtr windowHandle, string windowTitle)
                : this(windowHandle)
            {
                this.WindowTitle = windowTitle;
            }

            public override string ToString()
            {
                return WindowTitle;
            }
        }
    }
}
