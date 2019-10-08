/*
    The MIT License (MIT)

    Copyright (c) 2019 reblGreen Software Ltd. (https://reblgreen.com/)

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using System;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

namespace DimWin
{
    public class Helpers
    {
        //array of valid brightness values in percent
        public static int Brightness
        {
            get
            {
                var scope = new ManagementScope("root\\WMI");
                var query = new SelectQuery("WmiMonitorBrightness");

                using (var mos = new ManagementObjectSearcher(scope, query))
                {
                    try
                    {
                        using (var moc = mos.Get())
                        {
                            foreach (ManagementObject o in moc)
                            {
                                return (byte)o.GetPropertyValue("CurrentBrightness");
                            }
                        }
                    }
                    catch { }
                }

                return -1;
            }
            set
            {
                var scope = new ManagementScope("root\\WMI");
                var query = new SelectQuery("WmiMonitorBrightnessMethods");

                using (var mos = new ManagementObjectSearcher(scope, query))
                {
                    try
                    {
                        using (var moc = mos.Get())
                        {
                            foreach (ManagementObject o in moc)
                            {
                                o.InvokeMethod("WmiSetBrightness", new object[] { uint.MaxValue, value });
                                return;
                            }
                        }
                    }
                    catch { }
                }
            }
        }
        
        public static void SetGhost(IntPtr hwnd)
        {
            //Const LWA_ALPHA = &H2&
            var flags = Native.GetWindowLong(hwnd, (int)Native.GetWindowLongFlags.GWL_EXSTYLE) | (int)Native.ExStyleFlags.WS_EX_LAYERED | (int)Native.ExStyleFlags.WS_EX_TRANSPARENT;
            Native.SetWindowLong(hwnd, (int)Native.GetWindowLongFlags.GWL_EXSTYLE, (IntPtr)flags);
        }

        public static void ResetGhost(IntPtr hwnd)
        {
            var flags = Native.GetWindowLong(hwnd, (int)Native.GetWindowLongFlags.GWL_EXSTYLE) |~ (int)Native.ExStyleFlags.WS_EX_LAYERED |~ (int)Native.ExStyleFlags.WS_EX_TRANSPARENT;
            Native.SetWindowLong(hwnd, (int)Native.GetWindowLongFlags.GWL_EXSTYLE, (IntPtr)flags);
        }

        public static bool IsWin64
        {
            get
            {
                return (IntPtr.Size == 8);
            }
        }

        public static bool IsXPOrHigher()
        {
            OperatingSystem OS = Environment.OSVersion;
            return (OS.Platform == PlatformID.Win32NT) && ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor >= 1)));
        }

        public static bool IsVistaOrHigher()
        {
            OperatingSystem OS = Environment.OSVersion;
            return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
        }

        public static void RemoveFromAeroPeek(IntPtr Handle)
        {
            try
            {
                int value = (int)Native.DWMNCRENDERINGPOLICY.DWMNCRP_ENABLED;
                Native.DwmSetWindowAttribute(Handle, Native.DWMWINDOWATTRIBUTE.DWMWA_EXCLUDED_FROM_PEEK, ref value, Marshal.SizeOf(value));
            }
            catch { }
        }

        public static void SetHandleToDesktop(IntPtr handle)
        {
            IntPtr dh = IntPtr.Zero;
            dh = Native.FindWindow("ProgMan", null);

            dh = Native.FindWindowEx(dh, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            dh = Native.FindWindowEx(dh, IntPtr.Zero, "SysListView32", null);

            Native.SetParent(handle, dh);
        }

        public static bool IsDesktopWindowActive()
        {
            IntPtr activeWindow = Native.GetActiveWindow();

            IntPtr dh = IntPtr.Zero;
            dh = Native.FindWindow("ProgMan", null);

            if (dh != IntPtr.Zero)
            {
                if (activeWindow == dh)
                    return true;

                dh = Native.FindWindowEx(dh, IntPtr.Zero, "SHELLDLL_DefVIew", null);
                if (activeWindow == dh)
                    return true;

                dh = Native.FindWindowEx(dh, IntPtr.Zero, "SysListView32", null);
                if (activeWindow == dh)
                    return true;
            }

            if (Native.GetDesktopWindow() == activeWindow)
                return true;

            return false;
        }

        public static void OpenLink(string url)
        {
            System.Diagnostics.Process.Start(url);
            //Native.ShellExecute(0, "open", url, "0", "0", 1);
        }

        // Return short path format of a file name
        public static string ToShortPathName(string path)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = Native.GetShortPathName(path, s, iSize);
            return s.ToString();
        }
    }   
}