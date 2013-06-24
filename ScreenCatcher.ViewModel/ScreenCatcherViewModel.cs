using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Size = System.Drawing.Size;

using ScreenCatcher.Model;

namespace ScreenCatcher.ViewModel
{
    public class ScreenCatcherViewModel : ViewModelBase
    {
        private HotkeyRegistrator _hotKeyRegistrator;

        // todo: move actions dictionary here

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
                _hotKeyRegistrator.RegisterGlobalHotkey(
                    CatchScreen,
                    settings.ScreenCatch.Key,
                    settings.ScreenCatch.ModifierKey);

            if (settings.ScreenCatchCurrentWindow.Key != Keys.None)
                _hotKeyRegistrator.RegisterGlobalHotkey(
                    CatchCurrentWindow,
                    settings.ScreenCatchCurrentWindow.Key,
                    settings.ScreenCatchCurrentWindow.ModifierKey);

            if (settings.ScreenCatchWithConfirmation.Key != Keys.None)
                _hotKeyRegistrator.RegisterGlobalHotkey(
                    CatchScreenWithConfirmation,
                    settings.ScreenCatchWithConfirmation.Key,
                    settings.ScreenCatchWithConfirmation.ModifierKey);
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
            return SettingsBase.Load<ScreenSettings>() ?? new ScreenSettings();
        }

        private void CatchScreen(object arg)
        {
            var settings = GetScreenSettings();

            var screenWidth = Convert.ToInt32(SystemParameters.VirtualScreenWidth);
            var screenHeight = Convert.ToInt32(SystemParameters.VirtualScreenHeight);
            var screenLeft = Convert.ToInt32(SystemParameters.VirtualScreenLeft);
            var screenTop = Convert.ToInt32(SystemParameters.VirtualScreenTop);
            using (var screenshot = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);
                screenshot.Save(settings.CreateFileName(), settings.Extension.Parse());
            }
        }

        private void CatchScreenWithConfirmation(object arg)
        {
            var settings = GetScreenSettings();

            var screenWidth = Convert.ToInt32(SystemParameters.VirtualScreenWidth);
            var screenHeight = Convert.ToInt32(SystemParameters.VirtualScreenHeight);
            var screenLeft = Convert.ToInt32(SystemParameters.VirtualScreenLeft);
            var screenTop = Convert.ToInt32(SystemParameters.VirtualScreenTop);

            using (var screenshot = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

                var saveImageDialog = new SaveFileDialog
                {
                    Title = @"Select output file:",
                    Filter = @"JPeg Image|*.jpeg|Bitmap Image|*.bmp|Gif Image|*.gif|Icon Image|*.icon|PNG Image|*.png",
                    FileName = settings.DefaultFileName,
                };
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    var imageFormat = PathParser.GetExtension(saveImageDialog.FileName);
                    screenshot.Save(saveImageDialog.FileName, imageFormat);
                }
            }
        }

        private void CatchCurrentWindow(object arg)
        {
            var settings = GetScreenSettings();
            var bounds = WinAPI.GetActiveWindowBounds();
            const int shift = 0;
            using (var bitmap = new Bitmap(bounds.Width - 2 * shift, bounds.Height - 2 * shift, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(
                        bounds.Left + shift, bounds.Top + shift, 0, 0,
                        new Size(bounds.Size.Width - 2 * shift, bounds.Size.Height - 2 * shift),
                        CopyPixelOperation.SourceCopy);
                }
                bitmap.Save(settings.CreateFileName(), settings.Extension.Parse());
            }
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