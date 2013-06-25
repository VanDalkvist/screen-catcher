using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Size = System.Drawing.Size;

using ScreenCatcher.Logic;
using ScreenCatcher.Model;
using ModifierKeys = ScreenCatcher.Model.ModifierKeys;

namespace ScreenCatcher.ViewModel
{
    public class ScreenCatcherViewModel : ViewModelBase
    {
        private HotkeyRegistrator _hotKeyRegistrator;

        private ICommand _registerCommand;
        public ICommand RegisterCommand
        {
            get { return _registerCommand ?? (_registerCommand = new RelayCommand(Register)); }
        }

        private void Register(object arg)
        {
            if (!(arg is Window))
                return;

            if (_hotKeyRegistrator == null)
                _hotKeyRegistrator = new HotkeyRegistrator(arg as Window);
            else
                Unregister(arg);

            var settings = GetScreenSettings();
            if (settings.ScreenCatch.Key != Keys.None)
                RegisterKey(CatchScreen, settings.ScreenCatch.Key, settings.ScreenCatch.ModifierKey);

            if (settings.ScreenCatchCurrentWindow.Key != Keys.None)
                RegisterKey(CatchCurrentWindow, settings.ScreenCatchCurrentWindow.Key, settings.ScreenCatchCurrentWindow.ModifierKey);

            if (settings.ScreenCatchWithConfirmation.Key != Keys.None)
                RegisterKey(CatchScreenWithConfirmation, settings.ScreenCatchWithConfirmation.Key, settings.ScreenCatchWithConfirmation.ModifierKey);
        }

        private void RegisterKey(Action<object> catchScreenFunc, Keys key, ModifierKeys modifierKey)
        {
            _hotKeyRegistrator.RegisterGlobalHotkey(catchScreenFunc, key, (System.Windows.Input.ModifierKeys) modifierKey);
        }

        private ICommand _unregisterCommand;
        public ICommand UnregisterCommand
        {
            get { return _unregisterCommand ?? (_unregisterCommand = new RelayCommand(Unregister)); }
        }

        private void Unregister(object arg)
        {
            _hotKeyRegistrator.UnregisterHotkeys();
        }

        public ScreenSettings GetScreenSettings()
        {
            return SettingsBase.Load<ScreenSettings>() ?? GetDefaultSettings();
        }

        private ScreenSettings GetDefaultSettings()
        {
            var settings = new ScreenSettings
            {
                DefaultFileName = DefaultSettings.FileName,
                UseStorePath = false,
                Extension = ImageFormat.Bmp,
                UseSuffix = true,
                CurrentSuffix = Suffix.Date,
                ScreenCatch = new HotKey
                {
                    Key = Keys.PrintScreen,
                    ModifierKey = ModifierKeys.None
                },
                ScreenCatchCurrentWindow = new HotKey
                {
                    Key = Keys.PrintScreen,
                    ModifierKey = ModifierKeys.Control
                },
                ScreenCatchWithConfirmation = new HotKey
                {
                    Key = Keys.PrintScreen,
                    ModifierKey = ModifierKeys.Alt
                },
                CurrentProgramm = Programm.Paint,
                RunAs = false
            };
            settings.Save();
            return settings;
        }

        private void CatchScreen(object arg)
        {
            var settings = GetScreenSettings();

            string fileName;
            using (var screenshot = new Bitmap(VirtualScreen.Width, VirtualScreen.Height, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(VirtualScreen.Left, VirtualScreen.Top, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);
                fileName = settings.CreateFileName();
                var extension = settings.Extension.Parse();
                screenshot.Save(fileName, extension);
            }
            if (settings.RunAs)
                OpenForEdit(settings, fileName);
        }

        private void OpenForEdit(ScreenSettings settings, string fileName)
        {
            //if (settings.CurrentProgramm == Programm.Paint)
            //{
                Process.Start(DefaultSettings.Paint, fileName);
            //}
        }

        private void CatchScreenWithConfirmation(object arg)
        {
            var settings = GetScreenSettings();

            string fileName = string.Empty;
            using (var screenshot = new Bitmap(VirtualScreen.Width, VirtualScreen.Height, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(VirtualScreen.Left, VirtualScreen.Top, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

                var saveImageDialog = new SaveFileDialog
                {
                    Title = @"Select output file:",
                    Filter = @"JPeg Image|*.jpeg|Bitmap Image|*.bmp|Gif Image|*.gif|Icon Image|*.icon|PNG Image|*.png",
                    FileName = settings.DefaultFileName,
                };
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    var imageFormat = PathParser.GetExtension(saveImageDialog.FileName);
                    fileName = saveImageDialog.FileName;
                    screenshot.Save(fileName, imageFormat);
                }
            }
            if (settings.RunAs)
                OpenForEdit(settings, fileName);
        }

        private void CatchCurrentWindow(object arg)
        {
            var settings = GetScreenSettings();
            var bounds = WinAPI.GetActiveWindowBounds();
            const int shift = 0;

            string fileName;
            using (var bitmap = new Bitmap(bounds.Width - 2 * shift, bounds.Height - 2 * shift, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(
                        bounds.Left + shift, bounds.Top + shift, 0, 0,
                        new Size(bounds.Size.Width - 2 * shift, bounds.Size.Height - 2 * shift),
                        CopyPixelOperation.SourceCopy);
                }
                fileName = settings.CreateFileName();
                bitmap.Save(fileName, settings.Extension.Parse());
            }
            if (settings.RunAs)
                OpenForEdit(settings, fileName);
        }

        //private ICommand _openSettingsCommand;
        //public ICommand OpenSettingsCommand
        //{
        //    get { return _openSettingsCommand ?? (_openSettingsCommand = new RelayCommand(OpenSettings)); }
        //}

        //private void OpenSettings(object obj)
        //{
        //    var settings = GetScreenSettings();
        //    var window = new Settings
        //    {
        //        DataContext = new SettingsViewModel(settings)
        //    };
        //    window.ShowDialog();
        //}
    }
}