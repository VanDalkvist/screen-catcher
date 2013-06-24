using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenCatcher.Model
{
    public static class WinAPI
    {
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll")]
        public static extern Int16 GlobalAddAtom(string name);

        [DllImport("kernel32.dll")]
        public static extern Int16 GlobalDeleteAtom(Int16 nAtom);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, out Rect rect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WhKeyboardLL = 13;
        private const int WmKeydown = 0x0100;
        public const int WmHotkey = 0x0312;
        //private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static readonly IntPtr _hookID = IntPtr.Zero;

        // запуск отлова
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            {
                using (var curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WhKeyboardLL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        // отлов и, при необходимости, обработка хоткея
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WmKeydown)
            {
                var vkCode = (Keys)Marshal.ReadInt32(lParam);
                switch (vkCode)
                {
                    case Keys.MediaNextTrack:
                        {
                            break;
                        }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public static Rectangle GetActiveWindowBounds()
        {
            var foregroundWindowsHandle = GetForegroundWindow();

            var placement = new WindowPlacement();
            GetWindowPlacement(foregroundWindowsHandle, ref placement);
            if (placement.showCmd == WindowPlacement.Maximized)
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
            public uint length;
            public uint flags;
            public uint showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rect rcNormalPosition;

            public const uint Maximized = 3;
        };

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            private readonly int _left;
            private readonly int _top;
            private readonly int _right;
            private readonly int _bottom;

            private Rect(int left, int top, int right, int bottom)
            {
                _left = left;
                _top = top;
                _right = right;
                _bottom = bottom;
            }

            private Rect(Rectangle rectangle) : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom) { }

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

            private int Height
            {
                get { return _bottom - _top; }
            }

            private int Width
            {
                get { return _right - _left; }
            }

            public static implicit operator Rectangle(Rect rectangle)
            {
                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
            }

            public static implicit operator Rect(Rectangle rectangle)
            {
                return new Rect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public static bool operator ==(Rect rectangle1, Rect rectangle2)
            {
                return rectangle1.Equals(rectangle2);
            }

            public static bool operator !=(Rect rectangle1, Rect rectangle2)
            {
                return !rectangle1.Equals(rectangle2);
            }

            public override string ToString()
            {
                return "{ Left: " + _left + "; " + "Top: " + _top + "; Right: " + _right + "; Bottom: " + _bottom + " }";
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            private bool Equals(Rect rectangle)
            {
                return rectangle.Left == _left && rectangle.Top == _top && rectangle.Right == _right && rectangle.Bottom == _bottom;
            }

            public override bool Equals(object Object)
            {
                if (Object is Rect)
                    return Equals((Rect) Object);

                if (Object is Rectangle)
                    return Equals(new Rect((Rectangle) Object));

                return false;
            }
        }
    }
}