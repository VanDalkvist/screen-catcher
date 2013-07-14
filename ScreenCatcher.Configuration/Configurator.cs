using Microsoft.Practices.Unity;

using ScreenCatcher.Core;
using ScreenCatcher.Logic;

namespace ScreenCatcher.Configuration
{
    public static class Configurator
    {
        public static void Startup()
        {
            Bootstrapper.Container.RegisterInstance(SettingsProvider.GetCatcherSettings());
            Bootstrapper.Container.RegisterType<ICatalogViewerProvider, DefaultCatalogViewerProvider>();
            Bootstrapper.Container.RegisterType<IEditorProvider, DefaultEditorProvider>();
        }

        public static void RegisterSelf<T>()
        {
            Bootstrapper.Container.RegisterSelf<T>();
        }
    }
}