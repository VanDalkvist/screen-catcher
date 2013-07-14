using System;
using System.Windows;

using Microsoft.Windows.Shell;

using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Host
    {
        public Host() : this(UnitySingleton<ScreenCatcherViewModel>.Instance) { }

        public Host(ViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OpenSettings(object sender, EventArgs args)
        {
            new Settings().ShowDialog();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}