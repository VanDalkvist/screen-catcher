using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

using ScreenCatcher.Model;

namespace ScreenCatcher.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _defaultFileName;
        private ImageFormat _extension;
        private string _defaultPath;
        private bool _isStorePath;
        private bool _useDate;
        private bool _useGuid;
        private Keys _screenCatchKey;
        private ModifierKeys _screenCatchModifierKey;
        private Keys _screenCatchWithConfirmationKey;
        private ModifierKeys _screenCatchWithConfirmationModifierKey;
        private Keys _screenCatchCurrentWindowKey;
        private ModifierKeys _screenCatchCurrentWindowModifierKey;
        private bool _runAs;
        private Programm _currentProgramm;

        public SettingsViewModel(ScreenSettings settings)
        {
            Initialize(settings);
            Sibscribe();
        }

        private void Initialize(ScreenSettings settings)
        {
            DefaultFileName = settings.DefaultFileName;
            Extension = settings.Extension;
            DefaultPath = settings.DefaultPath;
            IsStorePath = settings.IsStorePath;
            UseDate = settings.UseDate;
            UseGuid = settings.UseGuid;
            ScreenCatchKey = settings.ScreenCatch.Key;
            ScreenCatchModifierKey = settings.ScreenCatch.ModifierKey;
            ScreenCatchWithConfirmationKey = settings.ScreenCatchWithConfirmation.Key;
            ScreenCatchWithConfirmationModifierKey = settings.ScreenCatchWithConfirmation.ModifierKey;
            ScreenCatchCurrentWindowKey = settings.ScreenCatchCurrentWindow.Key;
            ScreenCatchCurrentWindowModifierKey = settings.ScreenCatchCurrentWindow.ModifierKey;
            RunAs = settings.RunAs;
            CurrentProgramm = settings.CurrentProgramm;
        }

        private void Sibscribe()
        {
            Closed += OnClosed;
        }

        public string DefaultFileName
        {
            get { return _defaultFileName; }
            set
            {
                if (_defaultFileName == value)
                    return;

                _defaultFileName = value;
                OnPropertyChanged("DefaultFileName");
            }
        }

        public ImageFormat Extension
        {
            get { return _extension; }
            set
            {
                if (_extension == value)
                    return;

                _extension = value;
                OnPropertyChanged("Extension");
            }
        }

        public string DefaultPath
        {
            get { return _defaultPath; }
            set
            {
                if (_defaultPath == value)
                    return;

                _defaultPath = value;
                OnPropertyChanged("DefaultPath");
            }
        }

        public bool IsStorePath
        {
            get { return _isStorePath; }
            set
            {
                if (_isStorePath == value)
                    return;

                _isStorePath = value;
                OnPropertyChanged("IsStorePath");
            }
        }

        public bool UseDate
        {
            get { return _useDate; }
            set
            {
                if (_useDate == value)
                    return;

                _useDate = value;
                OnPropertyChanged("UseDate");
            }
        }

        public bool UseGuid
        {
            get { return _useGuid; }
            set
            {
                if (_useGuid == value)
                    return;

                _useGuid = value;
                OnPropertyChanged("UseGuid");
            }
        }

        public Keys Key
        {
            get { return _screenCatchKey; }
            set
            {
                if (_screenCatchKey == value)
                    return;

                _screenCatchKey = value;
                OnPropertyChanged("Key");
            }
        }

        public Keys ScreenCatchKey
        {
            get { return _screenCatchKey; }
            set
            {
                if (_screenCatchKey == value)
                    return;

                _screenCatchKey = value;
                OnPropertyChanged("ScreenCatchKey");
            }
        }

        public ModifierKeys ScreenCatchModifierKey
        {
            get { return _screenCatchModifierKey; }
            set
            {
                if (_screenCatchModifierKey == value)
                    return;

                _screenCatchModifierKey = value;
                OnPropertyChanged("ScreenCatchModifierKey");
            }
        }

        public Keys ScreenCatchWithConfirmationKey
        {
            get { return _screenCatchWithConfirmationKey; }
            set
            {
                if (_screenCatchWithConfirmationKey == value)
                    return;

                _screenCatchWithConfirmationKey = value;
                OnPropertyChanged("ScreenCatchWithConfirmationKey");
            }
        }

        public ModifierKeys ScreenCatchWithConfirmationModifierKey
        {
            get { return _screenCatchWithConfirmationModifierKey; }
            set
            {
                if (_screenCatchWithConfirmationModifierKey == value)
                    return;

                _screenCatchWithConfirmationModifierKey = value;
                OnPropertyChanged("ScreenCatchWithConfirmationModifierKey");
            }
        }

        public Keys ScreenCatchCurrentWindowKey
        {
            get { return _screenCatchCurrentWindowKey; }
            set
            {
                if (_screenCatchCurrentWindowKey == value)
                    return;

                _screenCatchCurrentWindowKey = value;
                OnPropertyChanged("ScreenCatchCurrentWindowKey");
            }
        }

        public ModifierKeys ScreenCatchCurrentWindowModifierKey
        {
            get { return _screenCatchCurrentWindowModifierKey; }
            set
            {
                if (_screenCatchCurrentWindowModifierKey == value)
                    return;

                _screenCatchCurrentWindowModifierKey = value;
                OnPropertyChanged("ScreenCatchCurrentWindowModifierKey");
            }
        }

        public bool RunAs
        {
            get { return _runAs; }
            set
            {
                if (_runAs == value)
                    return;

                _runAs = value;
                OnPropertyChanged("RunAs");
            }
        }

        public bool InPaint
        {
            get { return _currentProgramm == Programm.Paint; }
            set
            {
                if (_currentProgramm == Programm.Paint)
                    return;

                _currentProgramm = Programm.Paint;
                OnPropertyChanged("InPaint");
                OnPropertyChanged("InSelf");
            }
        }

        public bool InSelf
        {
            get { return _currentProgramm == Programm.Self; }
            set
            {
                if (_currentProgramm == Programm.Self)
                    return;

                _currentProgramm = Programm.Self;
                OnPropertyChanged("InSelf");
                OnPropertyChanged("InPaint");
            }
        }

        public Programm CurrentProgramm
        {
            get { return _currentProgramm; }
            set
            {
                if (_currentProgramm == value)
                    return;

                _currentProgramm = value;
                OnPropertyChanged("CurrentProgramm");
            }
        }

        public IEnumerable<ImageFormat> Extensions
        {
            get { return Enum.GetValues(typeof(ImageFormat)).Cast<ImageFormat>(); }
        }

        public IEnumerable<ModifierKeys> ModifierKeys
        {
            get { return Enum.GetValues(typeof(ModifierKeys)).Cast<ModifierKeys>(); }
        }

        public IEnumerable<Keys> Keys
        {
            get { return Enum.GetValues(typeof(Keys)).Cast<Keys>(); }
        }

        private ICommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get { return _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(SaveSettings)); }
        }

        private void SaveSettings(object obj)
        {
            var settings = new ScreenSettings
            {
                DefaultFileName = _defaultFileName,
                Extension = _extension,
                DefaultPath = _defaultPath,
                IsStorePath = _isStorePath,
                UseDate = _useDate,
                UseGuid = _useGuid,
                ScreenCatch = new HotKey
                {
                    Key = _screenCatchKey,
                    ModifierKey = _screenCatchModifierKey
                },
                ScreenCatchWithConfirmation = new HotKey
                {
                    Key = _screenCatchWithConfirmationKey,
                    ModifierKey = _screenCatchWithConfirmationModifierKey
                },
                ScreenCatchCurrentWindow = new HotKey
                {
                    Key = _screenCatchCurrentWindowKey,
                    ModifierKey = _screenCatchCurrentWindowModifierKey
                },
                CurrentProgramm = _currentProgramm,
                RunAs = _runAs
            };
            settings.Save();

            OnClosed();
        }

        private ICommand _setDefaultPathCommand;
        public ICommand SetDefaultPathCommand
        {
            get { return _setDefaultPathCommand ?? (_setDefaultPathCommand = new RelayCommand(SetDefaultPath)); }
        }

        private void SetDefaultPath(object obj)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = _defaultPath
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = dialog.SelectedPath;
                DefaultPath = selectedPath;
            }
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            CloseWindow();
        }

        protected override void Close(object obj)
        {
            OnClosed();
        }
    }
}