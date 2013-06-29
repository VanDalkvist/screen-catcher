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
using ImageFormat = ScreenCatcher.Model.ImageFormat;
using ModifierKeys = ScreenCatcher.Model.ModifierKeys;

namespace ScreenCatcher.ViewModel
{
    public class ScreenCatcherViewModel : ViewModelBase
    {
        private HotkeyRegistrator _hotKeyRegistrator;
        private Window _registeredWindow;

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

            if (_hotKeyRegistrator == null)
                _hotKeyRegistrator = new HotkeyRegistrator(window);
            else
                Unregister(arg);

            Register();

            SetOptions(window);
        }

        private void Register()
        {
            var settings = GetScreenSettings();
            if (settings.ScreenCatch.Key != Keys.None)
                RegisterKey(CatchScreen, settings.ScreenCatch.Key, settings.ScreenCatch.ModifierKey);

            if (settings.ScreenCatchCurrentWindow.Key != Keys.None)
                RegisterKey(CatchCurrentWindow, settings.ScreenCatchCurrentWindow.Key,
                            settings.ScreenCatchCurrentWindow.ModifierKey);

            if (settings.ScreenCatchWithConfirmation.Key != Keys.None)
                RegisterKey(CatchScreenWithConfirmation, settings.ScreenCatchWithConfirmation.Key,
                            settings.ScreenCatchWithConfirmation.ModifierKey);
        }

        private void SetOptions(Window window)
        {
            window.Hide();
        }

        private void RegisterKey(Action<object> catchScreenFunc, Keys key, ModifierKeys modifierKey)
        {
            _hotKeyRegistrator.RegisterGlobalHotkey(catchScreenFunc, key, (System.Windows.Input.ModifierKeys)modifierKey);
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
                UseNotification = true,
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

            if (settings.UseNotification)
                Notify(fileName);
        }

        private void Notify(string fileName)
        {
            WindowProperties.SetHadCaughtNotificationFileName(_registeredWindow, fileName);
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
                    Filter = @"JPeg Image|*.jpeg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png",
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
            var bounds = HotkeyRegistrator.GetActiveWindowBounds();
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

            if (settings.UseNotification)
                Notify(fileName);
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