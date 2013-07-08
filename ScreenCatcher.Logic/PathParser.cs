using System;
using System.Linq;

using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class PathParser
    {
        internal static System.Drawing.Imaging.ImageFormat GetExtension(string fullName)
        {
            var paths = fullName.Split('.');
            var extension = paths.Last();
            ImageFormat format;
            Enum.TryParse(extension, true, out format);
            return format.Parse();
        }
    }
}