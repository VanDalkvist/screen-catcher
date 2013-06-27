using System.Windows;
using System.Windows.Input;

namespace ScreenCatcher.ViewModel
{
    public static class StandardCommands
    {
        public static readonly DependencyProperty LoadCommandProperty =
           DependencyProperty.RegisterAttached("LoadCommand", typeof(ICommand), typeof(StandardCommands), new PropertyMetadata(LoadCommandChanged));

        private static void LoadCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.Loaded += (sender, args) => ExecuteCommand(sender, e);
        }

        private static void ExecuteCommand(object sender, DependencyPropertyChangedEventArgs e)
        {
            var command = e.NewValue as ICommand;
            if (command != null)
                command.Execute(sender);
        }

        public static ICommand GetLoadCommand(Window source)
        {
            return (ICommand)source.GetValue(LoadCommandProperty);
        }

        public static void SetLoadCommand(Window source, ICommand value)
        {
            source.SetValue(LoadCommandProperty, value);
        }

        public static readonly DependencyProperty UnloadCommandProperty =
           DependencyProperty.RegisterAttached("UnloadCommand", typeof(ICommand), typeof(StandardCommands), new PropertyMetadata(UnloadCommandChanged));

        private static void UnloadCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.Closing += (sender, args) => ExecuteCommand(sender, e);
        }

        public static ICommand GetUnloadCommand(Window source)
        {
            return (ICommand)source.GetValue(UnloadCommandProperty);
        }

        public static void SetUnloadCommand(Window source, ICommand value)
        {
            source.SetValue(UnloadCommandProperty, value);
        }

        public static readonly DependencyProperty MinimizeCommandProperty =
           DependencyProperty.RegisterAttached("MinimizeCommand", typeof(ICommand), typeof(StandardCommands), new PropertyMetadata(MinimizeCommandChanged));

        private static void MinimizeCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.StateChanged += (sender, args) => ExecuteCommand(sender, e);
        }

        public static ICommand GetMinimizeCommand(Window source)
        {
            return (ICommand)source.GetValue(UnloadCommandProperty);
        }

        public static void SetMinimizeCommand(Window source, ICommand value)
        {
            source.SetValue(UnloadCommandProperty, value);
        }
    }
}