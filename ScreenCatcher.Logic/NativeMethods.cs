using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenCatcher.Logic
{
    internal static class NativeMethods
    {
        [DllImport("User32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("User32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll")]
        internal static extern Int16 GlobalAddAtom(string name);

        [DllImport("kernel32.dll")]
        internal static extern Int16 GlobalDeleteAtom(Int16 nAtom);
        
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, out Rect rect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        internal const int WmHotkey = 0x0312;

        internal static Rectangle GetActiveWindowBounds()
        {
            var foregroundWindowsHandle = GetForegroundWindow();

            var placement = new WindowPlacement();
            GetWindowPlacement(foregroundWindowsHandle, ref placement);
            if (placement.ShowCmd == WindowPlacement.Maximized)
                return Screen.FromHandle(foregroundWindowsHandle).WorkingArea;

            Rect rect;
            GetWindowRect(foregroundWindowsHandle, out rect);
            var bounds = new Rectangle(
                new Point(rect.Left, rect.Top),
                new Size(rect.Right - rect.Left, rect.Bottom - rect.Top));
            return bounds;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowPlacement
        {
            private readonly uint length;
            private readonly uint flags;
            private readonly uint showCmd;
            private readonly Point ptMinPosition;
            private readonly Point ptMaxPosition;
            private readonly Rect rcNormalPosition;

            public const uint Maximized = 3;
            
            public uint ShowCmd
            {
                get { return showCmd; }
            }
        };

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            private readonly int _left;
            private readonly int _top;
            private readonly int _right;
            private readonly int _bottom;

            public int Left
            {
                get { return _left; }
            }

            public int Top
            {
                get { return _top; }
            }

            public int Right
            {
                get { return _right; }
            }

            public int Bottom
            {
                get { return _bottom; }
            }
        }
    }
}