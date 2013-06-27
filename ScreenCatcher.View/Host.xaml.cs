using System;
using System.Windows;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class Host
    {
        public Host()
        {
            InitializeComponent();
            DataContext = new ScreenCatcherViewModel();
        }

        private void OpenSettings(object sender, EventArgs args)
        {
            var viewModel = DataContext as ScreenCatcherViewModel;
            if (viewModel == null)
                return;

            var settings = viewModel.GetScreenSettings();
            var window = new Settings
            {
                DataContext = new SettingsViewModel(settings)
            };
            window.ShowDialog();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}