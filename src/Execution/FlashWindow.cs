using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Appalachia.Utility.Execution
{
    public static class FlashWindow
    {
        #region Constants and Static Readonly

        /// <summary>
        ///     Flash both the window caption and taskbar button.
        ///     This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        public const uint FLASHW_ALL = 3;

        /// <summary>
        ///     Flash the window caption.
        /// </summary>
        public const uint FLASHW_CAPTION = 1;

        /// <summary>
        ///     Stop flashing. The system restores the window to its original stae.
        /// </summary>
        public const uint FLASHW_STOP = 0;

        /// <summary>
        ///     Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        public const uint FLASHW_TIMER = 4;

        /// <summary>
        ///     Flash continuously until the window comes to the foreground.
        /// </summary>
        public const uint FLASHW_TIMERNOFG = 12;

        /// <summary>
        ///     Flash the taskbar button.
        /// </summary>
        public const uint FLASHW_TRAY = 2;

        #endregion

        /// <summary>
        ///     A boolean value indicating whether the application is running on Windows 2000 or later.
        /// </summary>
        private static bool Win2000OrLater => Environment.OSVersion.Version.Major >= 5;

        /// <summary>
        ///     Flash the specified Window (form) for the specified number of times
        /// </summary>
        /// <param name="form">The Form (Window) to Flash.</param>
        /// <param name="count">The number of times to Flash.</param>
        /// <returns></returns>
        public static bool Flash(uint count)
        {
            if (Win2000OrLater)
            {
                var handle = Process.GetCurrentProcess().Handle;
                var fi = Create_FLASHWINFO(handle, FLASHW_ALL, count, 0);
                return FlashWindowEx(ref fi);
            }

            return false;
        }

        /// <summary>
        ///     Start Flashing the specified Window (form)
        /// </summary>
        /// <param name="form">The Form (Window) to Flash.</param>
        /// <returns></returns>
        public static bool Start()
        {
            if (Win2000OrLater)
            {
                var handle = Process.GetCurrentProcess().Handle;
                var fi = Create_FLASHWINFO(handle, FLASHW_ALL, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }

            return false;
        }

        /// <summary>
        ///     Stop Flashing the specified Window (form)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool Stop()
        {
            if (Win2000OrLater)
            {
                var handle = Process.GetCurrentProcess().Handle;
                var fi = Create_FLASHWINFO(handle, FLASHW_STOP, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }

            return false;
        }

        private static FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            var fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        #region Nested type: FLASHWINFO

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            #region Fields and Autoproperties

            /// <summary>
            ///     A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            ///     The size of the structure in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            ///     The Flash Status.
            /// </summary>
            public uint dwFlags;

            /// <summary>
            ///     The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;

            /// <summary>
            ///     The number of times to Flash the window.
            /// </summary>
            public uint uCount;

            #endregion
        }

        #endregion
    }
}
