using System;
using System.Windows;
using System.Windows.Interop;

namespace ScreenCatcher.Common.Extensions
{
    public static class WindowHelper
    {
        public static IntPtr GetWindowHandle(this Window window)
        {
            var helper = new WindowInteropHelper(window);
            return helper.Handle;
        }

        public static HwndSource GetHwndSource(this Window window)
        {
            return HwndSource.FromHwnd(window.GetWindowHandle());
        }
    }
}