using System.Diagnostics;

using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    internal class ExplorerViewer : ICatalogViewer
    {
        public void View(string path)
        {
            Process.Start(ProgrammNameProvider.GetCatalogProgrammName(), "/select," + path);
        }
    }
}