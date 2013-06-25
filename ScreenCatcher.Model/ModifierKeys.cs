using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Markup;

namespace ScreenCatcher.Model
{
    [TypeConverter(typeof(ModifierKeysConverter))]
    [ValueSerializer(typeof(ModifierKeysValueSerializer))]
    [Flags]
    public enum ModifierKeys
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
    }
}