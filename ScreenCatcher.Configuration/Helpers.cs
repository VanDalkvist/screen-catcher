using Microsoft.Practices.Unity;

namespace ScreenCatcher.Configuration
{
    internal static class Helpers
    {
        internal static T Resolve<T>(this IUnityContainer unityContainer)
        {
            return unityContainer.IsRegistered<T>() ? UnityContainerExtensions.Resolve<T>(unityContainer) : default(T);
        }

        internal static IUnityContainer RegisterSelf<T>(this IUnityContainer unityContainer)
        {
            return unityContainer.RegisterType<T, T>();
        }
    }
}