using System;
using System.Windows;

using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    public partial class App
    {
        public App()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            Current.Startup += OnStartup;
            Current.Exit += OnExit;
        }

        private void OnStartup(object sender, StartupEventArgs startupEventArgs)
        {
            if (ApplicationController.IsAlreadyLauched())
            {
                ApplicationController.Clear();
                MessageBox.Show("The application already is running!", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                Environment.Exit(0);
            }
            else
                ContainerConfig.Initialize();
        }

        private void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            ApplicationController.Clear();
        }
    }
}