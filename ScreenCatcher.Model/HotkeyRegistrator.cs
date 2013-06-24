using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

using ScreenCatcher.Common.Extensions;

namespace ScreenCatcher.Model
{
    public class HotkeyRegistrator
    {
        private readonly IntPtr _windowHandle;

        private readonly IDictionary<Int16, Action<object>> _globalActions = new Dictionary<short, Action<object>>();

        public HotkeyRegistrator(Window window)
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
                if (_globalActions.ContainsKey(atom))
                    _globalActions[atom](lparam);
            }
            return IntPtr.Zero;
        }

        public bool RegisterGlobalHotkey(Action<object> action, Keys commonKey, params ModifierKeys[] keys)
        {
            var mod = keys.Cast<uint>().Aggregate((current, modKey) => current | modKey);
            short atom = WinAPI.GlobalAddAtom("ScreenCatcher" + (_globalActions.Count + 1));
            bool status = WinAPI.RegisterHotKey(_windowHandle, atom, mod, (uint)commonKey);

            if (status)
                _globalActions.Add(atom, action);

            return status;
        }

        public void UnregisterHotkeys()
        {
            foreach (var atom in _globalActions.Keys)
            {
                WinAPI.UnregisterHotKey(_windowHandle, atom);
                WinAPI.GlobalDeleteAtom(atom);
            }
        }
    }
}