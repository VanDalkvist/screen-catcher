using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace ScreenCatcher.Model
{
    [Serializable]
    public class HotKey
    {
        public Keys Key { get; set; }
        public ModifierKeys ModifierKey { get; set; }
    }
}