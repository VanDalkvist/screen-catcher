using System;

namespace ScreenCatcher.Common
{
    public abstract class Singleton<T>
        where T : class
    {
        protected static volatile T _instance;

        protected static readonly Object SyncRoot = new Object();

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (SyncRoot)
                    if (_instance == null)
                    {
                        return GetInstance();
                    }

                return _instance;
            }
        }

        protected static T GetInstance()
        {
            var type = typeof(T);
            if (type.IsAbstract || type.IsInterface)
                return null;

            return Activator.CreateInstance(type, true) as T;
        }
    }
}