using System;
using System.Diagnostics;
using System.Windows.Input;
using ScreenCatcher.Common;

namespace ScreenCatcher.ViewModel
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly string _fileName;

        public NotificationsViewModel(string fileName)
        {
            _fileName = fileName;
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(Open)); }
        }

        private void Open(object obj)
        {
            //var settings = SettingsBase.Load<ScreenSettings>();
            //if (settings.CurrentProgramm == Programm.Paint)
            //{
            Process.Start(DefaultSettings.Paint, FileName);
            //}
        }

        private ICommand _openDirectoryCommand;
        public ICommand OpenDirectoryCommand
        {
            get { return _openDirectoryCommand ?? (_openDirectoryCommand = new RelayCommand(OpenDirectory)); }
        }

        private void OpenDirectory(object obj)
        {
            Process.Start(DefaultSettings.Explorer, Environment.CurrentDirectory);
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string FullFileName
        {
            get { return Environment.CurrentDirectory + Constants.Delimiter + _fileName; }
        }
    }
}