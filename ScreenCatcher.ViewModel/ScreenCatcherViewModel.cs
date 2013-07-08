using System;
using System.Windows;
using System.Windows.Input;

using ScreenCatcher.Logic;

namespace ScreenCatcher.ViewModel
{
    public class ScreenCatcherViewModel : ViewModelBase
    {
        private Logic.ScreenCatcher _screenCatcher;

        private Window _registeredWindow;

        internal ScreenCatcherViewModel()
        {
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new RelayCommand(Load)); }
        }

        private void Load(object arg)
        {
            var window = arg as Window;
            if (window == null)
                return;

            _registeredWindow = window;
            _screenCatcher = new Logic.ScreenCatcher(window);
            
            Subscribe();
            
            SetOptions(window);
        }

        private void Subscribe()
        {
            _screenCatcher.Notifying += ScreenCatcherOnNotifying;
        }

        private void ScreenCatcherOnNotifying(object sender, NotifyEventHandlerArgs args)
        {
            WindowProperties.SetHadCaughtNotificationFileName(_registeredWindow, args.FileName);
        }

        private void SetOptions(Window window)
        {
            window.Hide();
        }

        private ICommand _unloadCommand;
        public ICommand UnloadCommand
        {
            get { return _unloadCommand ?? (_unloadCommand = new RelayCommand(Unload)); }
        }

        private void Unload(object arg)
        {
            _screenCatcher.Unload();
        }

        protected override void Minimize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var window = obj as Window;
            if (window == null)
                throw new ArgumentException();

            window.Hide();
        }
    }
}