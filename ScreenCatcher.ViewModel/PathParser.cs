using System;
using System.Linq;

using ScreenCatcher.Common;

namespace ScreenCatcher.ViewModel
{
    internal class PathParser
    {
        public static System.Drawing.Imaging.ImageFormat GetExtension(string fullName)
        {
            var paths = fullName.Split('.');
            var extension = paths.Last();
            ImageFormat format;
            Enum.TryParse(extension, true, out format);
            return format.Parse();
        }

        public static ProgrammInfo GetFileInfo(string fullName)
        {
            var index = fullName.LastIndexOf(Constants.Delimiter);
            if (index < 0)
                index = fullName.LastIndexOf(Constants.InverseDelimiter);
            var name = fullName.Substring(index + 1);
            var path = fullName.Substring(0, fullName.Length - (name.Length + 1));
            name = name.Split(Constants.Dot).First();
            return new ProgrammInfo
            {
                Name = name,
                Path = path
            };
        }
    }
}