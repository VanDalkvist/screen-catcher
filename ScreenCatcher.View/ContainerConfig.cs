using ScreenCatcher.Configuration;
using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View
{
    static class ContainerConfig
    {
        public static void Initialize()
        {
            Configurator.Startup();
            Configurator.RegisterSelf<ScreenCatcherViewModel>();
            Configurator.RegisterSelf<SettingsViewModel>();
        }
    }
}