using System;
using ScreenCatcher.Model;

namespace ScreenCatcher.ViewModel
{
    [Serializable]
    public class ScreenSettings : SettingsBase
    {
        public ScreenSettings()
        {
            DefaultFileName = "";
            ScreenCatch = new HotKey();
            ScreenCatchWithConfirmation = new HotKey();
            ScreenCatchCurrentWindow = new HotKey();
            DefaultProgramms = new ProgrammInfo[] { };
            CurrentProgramm = new ProgrammInfo();
        }

        public string DefaultFileName { get; set; }

        public ImageFormat Extension { get; set; }

        public string DefaultPath { get; set; }

        public bool IsStorePath { get; set; }

        public bool UseDate { get; set; }

        public bool UseGuid { get; set; }

        public HotKey ScreenCatch { get; set; }

        public HotKey ScreenCatchWithConfirmation { get; set; }

        public HotKey ScreenCatchCurrentWindow { get; set; }

        public bool RunAs { get; set; }

        public ProgrammInfo[] DefaultProgramms { get; set; }

        public ProgrammInfo CurrentProgramm { get; set; }

        public override object Clone()
        {
            return new ScreenSettings
            {
                DefaultFileName = DefaultFileName,
                Extension = Extension,
                DefaultPath = DefaultPath,
                IsStorePath = IsStorePath,
                UseDate = UseDate,
                UseGuid = UseGuid,
                ScreenCatch = ScreenCatch,
                ScreenCatchWithConfirmation = ScreenCatchWithConfirmation,
                ScreenCatchCurrentWindow = ScreenCatchCurrentWindow,
                RunAs = RunAs,
                DefaultProgramms = DefaultProgramms,
                CurrentProgramm = CurrentProgramm
            };
        }
    }
}