using System;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

using ScreenCatcher.Common;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal static class ImageHelper
    {
        internal static ImageFormat Parse(this Model.ImageFormat format)
        {
            switch (format)
            {
                case Model.ImageFormat.Bmp:
                    return ImageFormat.Bmp;
                case Model.ImageFormat.Gif:
                    return ImageFormat.Gif;
                case Model.ImageFormat.Jpeg:
                    return ImageFormat.Jpeg;
                case Model.ImageFormat.Png:
                    return ImageFormat.Png;
                default:
                    throw new NotSupportedException(string.Format("Type of image format: {0} is not supported.", format));
            }
        }

        internal static string CreateFileName(this CatcherSettings settings)
        {
            var path = String.Empty;
            if (settings.UseStorePath && !String.IsNullOrEmpty(settings.DefaultPath))
                path = settings.DefaultPath + Constants.Delimiter;
            var addon = string.Empty;
            if (settings.UseSuffix)
            {
                addon += Constants.NameSeparator;
                switch (settings.CurrentSuffix)
                {
                    case Suffix.Date:
                        addon += DateTime.Now.ToString("dd_MM_yyyy_(HH-mm-ss-fff)");
                        break;
                    case Suffix.Guid:
                        addon += Guid.NewGuid().ToString();
                        break;
                }
            }
            return path + settings.DefaultFileName + addon + Constants.Dot + settings.Extension.ToString().ToLower();
        }
    }
}