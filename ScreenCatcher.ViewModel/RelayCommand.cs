using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace ScreenCatcher.ViewModel
{
    internal class RelayCommand : MarkupExtension, ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        internal RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}