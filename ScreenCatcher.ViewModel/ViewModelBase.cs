using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Microsoft.Windows.Shell;

namespace ScreenCatcher.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Closed;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        bool? _closeWindowFlag;

        public bool? CloseWindowFlag
        {
            get { return _closeWindowFlag; }
            set
            {
                _closeWindowFlag = value;
                OnPropertyChanged("CloseWindowFlag");
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                if (CloseWindowFlag == null)
                    CloseWindowFlag = result;
                else
                    CloseWindowFlag = !CloseWindowFlag;
            }));
        }

        protected virtual void OnClosed()
        {
            var handler = Closed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(Close)); }
        }

        protected virtual void Close(object obj)
        {
            if (obj == null || !(obj is Window))
                throw new ArgumentNullException("obj");
            SystemCommands.CloseWindow((Window)obj);
        }

        private ICommand _minimizeCommand;
        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand ?? (_minimizeCommand = new RelayCommand(Minimize)); }
        }

        protected virtual void Minimize(object obj)
        {
            if (obj == null || !(obj is Window))
                throw new ArgumentNullException("obj");
            SystemCommands.MinimizeWindow((Window)obj);
        }

        private ICommand _maximizeCommand;
        public ICommand MaximizeCommand
        {
            get { return _maximizeCommand ?? (_maximizeCommand = new RelayCommand(Maximize)); }
        }

        protected virtual void Maximize(object obj)
        {
            var window = ((Window)obj);
            if (window == null)
                throw new ArgumentNullException("obj");

            //var containerBorder = (Border)window.Template.FindName("PART_Container", window);
            switch (window.WindowState)
            {
                case WindowState.Maximized:
                    SystemCommands.RestoreWindow(window);
                    //containerBorder.Padding = new Thickness();
                    break;
                default:
                    SystemCommands.MaximizeWindow(window);
                    //containerBorder.Padding = new Thickness(
                    //    SystemParameters.WorkArea.Left + 7,
                    //    SystemParameters.WorkArea.Top + 7,
                    //    (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 7,
                    //    (SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom) + 5);
                    break;
            }
        }
    }
}