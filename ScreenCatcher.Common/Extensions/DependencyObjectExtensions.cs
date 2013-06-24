using System.Windows;
using System.Windows.Media;

namespace ScreenCatcher.Common.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static TParent GetVisualParent<TParent>(this DependencyObject child)
            where TParent : Visual
        {
            while ((child != null) && !(child is TParent))
                child = VisualTreeHelper.GetParent(child);
            return child as TParent;
        }
    }
}