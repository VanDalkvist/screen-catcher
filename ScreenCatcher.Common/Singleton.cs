using System;

namespace ScreenCatcher.Common
{
    public abstract class Singleton<T>
        where T : class
    {
        private static volatile T _instance;

        private static readonly Object SyncRoot = new Object();

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (SyncRoot)
                    if (_instance == null)
                    {
                        var instance = (T)Activator.CreateInstance(typeof(T), true);
                        _instance = instance;
                    }

                return _instance;
            }
        }
    }
}