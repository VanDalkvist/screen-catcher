using System.Configuration;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ScreenCatcher.Configuration
{
    internal static class Bootstrapper
    {
        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get { return _container ?? (_container = BuildUnityContainer()); }
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            if (ConfigurationManager.GetSection("unity") != null)
                container.LoadConfiguration();

            return container;
        }
    }
}