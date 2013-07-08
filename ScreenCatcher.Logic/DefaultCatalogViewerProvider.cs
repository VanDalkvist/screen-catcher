using ScreenCatcher.Core;
using ScreenCatcher.Model;

namespace ScreenCatcher.Logic
{
    public class DefaultCatalogViewerProvider : ICatalogViewerProvider
    {
        public ICatalogViewer Create(CatcherSettings settings)
        {
            return new ExplorerViewer();
        }
    }
}