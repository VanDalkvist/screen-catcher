using System;
using SystemImageFormat = System.Drawing.Imaging.ImageFormat;

using ScreenCatcher.Common;
using ScreenCatcher.Model;

namespace ScreenCatcher.ViewModel
{
    public static class ImageHelper
    {
        public static SystemImageFormat Parse(this ImageFormat format)
        {
            switch (format)
            {
                case ImageFormat.Bmp:
                    return SystemImageFormat.Bmp;
                case ImageFormat.Gif:
                    return SystemImageFormat.Gif;
                case ImageFormat.Icon:
                    return SystemImageFormat.Icon;
                case ImageFormat.Jpeg:
                    return SystemImageFormat.Jpeg;
                case ImageFormat.Png:
                    return SystemImageFormat.Png;
                default:
                    throw new NotSupportedException(string.Format("Type of image format: {0} is not supported.", format));
            }
        }

        public static string CreateFileName(this ScreenSettings settings)
        {
            var path = String.Empty;
            if (settings.IsStorePath && !String.IsNullOrEmpty(settings.DefaultPath))
                path = settings.DefaultPath + Constants.Delimiter;
            object addon = DateTime.Now.Ticks;
            if (settings.UseDate)
                addon = DateTime.Now.ToString("dd_MM_yyyy_(HH-mm-ss-fff)");
            if (settings.UseGuid)
                addon = Guid.NewGuid();
            return path + settings.DefaultFileName + Constants.NameSeparator + addon + Constants.Dot + settings.Extension.ToString().ToLower();
        }
    }
}