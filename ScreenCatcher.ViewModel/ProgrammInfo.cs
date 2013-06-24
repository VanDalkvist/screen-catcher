using System;
using System.ComponentModel;

namespace ScreenCatcher.ViewModel
{
    [Serializable]
    public class ProgrammInfo : INotifyPropertyChanged
    {
        private string _name;
        private string _path;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                if (_path == value)
                    return;

                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}