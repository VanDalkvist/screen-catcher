using System;

namespace ScreenCatcher.Logic
{
    internal delegate void CatcherEventHandler(object sender, CatcherEventHandlerArgs args);

    internal class CatcherEventHandlerArgs : EventArgs
    {
        private readonly Int16 _key;

        public CatcherEventHandlerArgs(short key)
        {
            _key = key;
        }

        public short Key
        {
            get { return _key; }
        }
    }
}