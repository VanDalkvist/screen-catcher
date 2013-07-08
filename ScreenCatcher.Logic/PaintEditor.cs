using System.Diagnostics;

using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class PaintEditor : IEditor
    {
        public void Edit(string fileName)
        {
            Process.Start(ProgrammNameProvider.GetEditProgrammName(), fileName);
        }
    }
}