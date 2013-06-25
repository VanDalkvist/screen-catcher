using System.Windows;

namespace ScreenCatcher.ViewModel
{
    public class VirtualScreen
    {
        public static readonly int Width = (int) SystemParameters.VirtualScreenWidth;
        public static readonly int Height = (int) SystemParameters.VirtualScreenHeight;
        public static readonly int Left = (int) SystemParameters.VirtualScreenLeft;
        public static readonly int Top = (int) SystemParameters.VirtualScreenTop;
    }
}