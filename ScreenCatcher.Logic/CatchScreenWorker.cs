using System.Drawing;
using System.Drawing.Imaging;

using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class CatchScreenWorker : ICatchScreenWorker
    {
        public void Catch(CatcherSettings settings, out string fileName)
        {
            using (var screenshot = new Bitmap(VirtualScreen.Width, VirtualScreen.Height, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(VirtualScreen.Left, VirtualScreen.Top, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);
                fileName = settings.CreateFileName();
                var extension = settings.Extension.Parse();
                screenshot.Save(fileName, extension);
            }
        }
    }
}