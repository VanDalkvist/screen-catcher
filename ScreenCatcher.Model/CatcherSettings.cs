using System;

namespace ScreenCatcher.Model
{
    [Serializable]
    public class CatcherSettings : SettingsBase
    {
        public CatcherSettings()
        {
            ScreenCatch = new HotKey();
            ScreenCatchWithConfirmation = new HotKey();
            ScreenCatchCurrentWindow = new HotKey();
        }

        public string DefaultFileName { get; set; }

        public ImageFormat Extension { get; set; }

        public string DefaultPath { get; set; }

        public bool UseStorePath { get; set; }

        public bool UseSuffix { get; set; }

        public Suffix CurrentSuffix { get; set; }

        public HotKey ScreenCatch { get; set; }

        public HotKey ScreenCatchWithConfirmation { get; set; }

        public HotKey ScreenCatchCurrentWindow { get; set; }

        public bool RunAs { get; set; }
        
        public Programm CurrentProgramm { get; set; }

        public bool UseNotification { get; set; }

        public override object Clone()
        {
            return new CatcherSettings
            {
                DefaultFileName = DefaultFileName,
                Extension = Extension,
                DefaultPath = DefaultPath,
                UseStorePath = UseStorePath,
                UseSuffix = UseSuffix,
                CurrentSuffix = CurrentSuffix,
                ScreenCatch = ScreenCatch,
                ScreenCatchWithConfirmation = ScreenCatchWithConfirmation,
                ScreenCatchCurrentWindow = ScreenCatchCurrentWindow,
                RunAs = RunAs,
                CurrentProgramm = CurrentProgramm,
                UseNotification = UseNotification
            };
        }
    }
}