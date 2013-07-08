using ScreenCatcher.Model;

namespace ScreenCatcher.Core
{
    public interface ICatalogViewerProvider
    {
        ICatalogViewer Create(CatcherSettings settings);
    }
}