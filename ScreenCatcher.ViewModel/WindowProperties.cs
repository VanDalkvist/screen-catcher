using System.Windows;

using ScreenCatcher.Common.Extensions;

namespace ScreenCatcher.ViewModel
{
    public static class WindowProperties
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(WindowProperties), new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.Close();
        }

        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        public static readonly DependencyProperty MaximizeVisibilityProperty =
            DependencyProperty.RegisterAttached("MaximizeVisibility", typeof(Visibility), typeof(WindowProperties));

        public static void SetMaximizeVisibility(Window target, Visibility value)
        {
            target.SetValue(MaximizeVisibilityProperty, value);
        }

        public static Visibility GetMaximizeVisibility(Window target)
        {
            return (Visibility)target.GetValue(MaximizeVisibilityProperty);
        }

        public static readonly DependencyProperty MinimizeVisibilityProperty =
            DependencyProperty.RegisterAttached("MinimizeVisibility", typeof(Visibility), typeof(WindowProperties));

        public static void SetMinimizeVisibility(Window target, Visibility value)
        {
            target.SetValue(MinimizeVisibilityProperty, value);
        }

        public static Visibility GetMinimizeVisibility(Window target)
        {
            return (Visibility)target.GetValue(MinimizeVisibilityProperty);
        }

        public static readonly DependencyProperty CanDragMoveProperty =
            DependencyProperty.RegisterAttached("CanDragMove", typeof(bool), typeof(WindowProperties), new PropertyMetadata(CanDragMoveChanged));

        public static void SetCanDragMove(UIElement target, bool value)
        {
            target.SetValue(CanDragMoveProperty, value);
        }

        public static bool GetCanDragMove(UIElement target)
        {
            return (bool)target.GetValue(CanDragMoveProperty);
        }

        private static void CanDragMoveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!((bool)e.NewValue))
                return;

            var uiElement = d as UIElement;
            if (uiElement != null)
                uiElement.MouseLeftButtonDown += WindowPropertiesMouseLeftButtonDown;
        }

        private static void WindowPropertiesMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null)
                return;

            var window = frameworkElement.GetVisualParent<Window>();
            if (window == null)
                return;

            window.DragMove();
        }

        public static readonly DependencyProperty HadCaughtNotificationFileNameProperty =
            DependencyProperty.RegisterAttached("HadCaughtNotificationFileName", typeof(string), typeof(WindowProperties));

        public static void SetHadCaughtNotificationFileName(Window target, string value)
        {
            target.SetValue(HadCaughtNotificationFileNameProperty, value);
        }

        public static string GetHadCaughtNotificationFileName(Window target)
        {
            return (string)target.GetValue(HadCaughtNotificationFileNameProperty);
        }
    }
}