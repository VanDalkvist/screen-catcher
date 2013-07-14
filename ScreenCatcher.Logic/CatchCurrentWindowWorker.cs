using System.Drawing;
using System.Drawing.Imaging;

using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class CatchCurrentWindowWorker : ICatchScreenWorker
    {
        public void Catch(CatcherSettings settings, out string fileName)
        {
            var bounds = NativeMethods.GetActiveWindowBounds();
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
                fileName = settings.CreateFileName();
                bitmap.Save(fileName, settings.Extension.Parse());
            }
        }
    }
}