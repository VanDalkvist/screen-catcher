using System;
using System.Windows.Forms;

namespace ScreenCatcher.Model
{
    [Serializable]
    public class HotKey
    {
        public Keys Key { get; set; }
        public ModifierKeys ModifierKey { get; set; }
    }
}