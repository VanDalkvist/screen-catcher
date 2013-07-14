using System;
using System.Globalization;
using System.Threading;

namespace ScreenCatcher.ViewModel
{
    public static class ApplicationController
    {
        private static Mutex _mutex;
        private static bool _mutexCreated;
        private static readonly string _mutexName = String.Format(CultureInfo.InvariantCulture, UniqueIdentifier);

        public static bool IsAlreadyLauched()
        {
            _mutex = new Mutex(true, _mutexName, out _mutexCreated);

            return !_mutexCreated;
        }

        public static void Clear()
        {
            if (_mutex == null)
                return;

            if (_mutexCreated)
                _mutex.ReleaseMutex();

            _mutex.Close();
            _mutex = null;
        }

        private const string UniqueIdentifier = "1D2C470B-A2A5-4362-B23E-A3008BCDA551";
    }
}