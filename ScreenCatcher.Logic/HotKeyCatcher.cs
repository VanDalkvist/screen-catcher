using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

using ScreenCatcher.Common.Extensions;

namespace ScreenCatcher.Logic
{
    internal class HotKeyCatcher
    {
        private readonly IntPtr _windowHandle;

        public HotKeyCatcher(Window window)
        {
            _windowHandle = window.GetWindowHandle();
            var source = HwndSource.FromHwnd(_windowHandle);
            if (source != null)
                source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == WinAPI.WmHotkey)
            {
                short atom = Int16.Parse(wparam.ToString());
                OnCaptured(atom);
            }
            return IntPtr.Zero;
        }

        public event CatcherEventHandler Captured;

        private void OnCaptured(CatcherEventHandlerArgs args)
        {
            var handler = Captured;
            if (handler != null)
                handler(this, args);
        }

        private void OnCaptured(Int16 key)
        {
            OnCaptured(new CatcherEventHandlerArgs(key));
        }

        public short RegisterGlobalHotkey(Keys buttonKey, params ModifierKeys[] modifierKeys)
        {
            var modifierKey = modifierKeys.Cast<uint>().Aggregate((current, modKey) => current | modKey);
            short atom = WinAPI.GlobalAddAtom("ScreenCatcher" + Guid.NewGuid());
            WinAPI.RegisterHotKey(_windowHandle, atom, modifierKey, (uint)buttonKey);
            return atom;
        }

        public void UnregisterHotkeys(HotKeyStorage storage)
        {
            foreach (var atom in storage)
            {
                WinAPI.UnregisterHotKey(_windowHandle, atom);
                WinAPI.GlobalDeleteAtom(atom);
            }
        }
    }
}