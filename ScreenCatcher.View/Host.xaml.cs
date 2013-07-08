using System;
using System.Windows;

using Microsoft.Windows.Shell;

using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Host
    {
        public Host()
        {
            InitializeComponent();
            var viewModelProvider = new MainViewModelProvider();
            DataContext = viewModelProvider.Create();
        }

        private void OpenSettings(object sender, EventArgs args)
        {
            var viewModel = DataContext as ViewModelBase;
            if (viewModel == null)
                return;

            var settingsProvider = new SettingsViewModelProvider(viewModel);
            var window = new Settings
            {
                DataContext = settingsProvider.Create()
            };
            window.ShowDialog();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}