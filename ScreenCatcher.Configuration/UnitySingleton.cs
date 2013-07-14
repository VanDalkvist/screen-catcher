using ScreenCatcher.Common;

namespace ScreenCatcher.Configuration
{
    public abstract class UnitySingleton<T> : Singleton<T>
        where T : class
    {
        public new static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (SyncRoot)
                    if (_instance == null)
                    {
                        var instance = Bootstrapper.Container.Resolve<T>();
                        _instance = instance ?? GetInstance();
                    }

                return _instance;
            }
        }
    }
}