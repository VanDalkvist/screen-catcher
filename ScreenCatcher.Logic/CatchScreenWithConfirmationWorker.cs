using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class CatchScreenWithConfirmationWorker : ICatchScreenWorker
    {
        public void Catch(CatcherSettings settings, out string fileName)
        {
            fileName = string.Empty;
            using (var screenshot = new Bitmap(VirtualScreen.Width, VirtualScreen.Height, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(VirtualScreen.Left, VirtualScreen.Top, 0, 0, screenshot.Size,
                                        CopyPixelOperation.SourceCopy);

                var saveImageDialog = new SaveFileDialog
                {
                    Title = @"Select output file:",
                    Filter = @"JPeg Image|*.jpeg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png",
                    FileName = settings.DefaultFileName,
                };
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    var imageFormat = PathParser.GetExtension(saveImageDialog.FileName);
                    fileName = saveImageDialog.FileName;
                    screenshot.Save(fileName, imageFormat);
                }
            }
        }
    }
}