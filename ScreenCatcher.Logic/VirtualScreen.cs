using System.Windows;

namespace ScreenCatcher.Logic
{
    internal class VirtualScreen
    {
        internal static readonly int Width = (int)SystemParameters.VirtualScreenWidth;
        internal static readonly int Height = (int)SystemParameters.VirtualScreenHeight;
        internal static readonly int Left = (int)SystemParameters.VirtualScreenLeft;
        internal static readonly int Top = (int)SystemParameters.VirtualScreenTop;
    }
}