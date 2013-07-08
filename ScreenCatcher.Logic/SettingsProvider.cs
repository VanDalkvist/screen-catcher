using System.Windows.Forms;

using ScreenCatcher.Common;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    public class SettingsProvider
    {
        public static CatcherSettings GetCatcherSettings()
        {
            return SettingsBase.Load<CatcherSettings>() ?? GetDefaultSettings();
        }

        private static CatcherSettings GetDefaultSettings()
        {
            var settings = new CatcherSettings
            {
                DefaultFileName = Constants.FileName,
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
    }
}