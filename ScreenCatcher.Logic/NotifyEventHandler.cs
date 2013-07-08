using System;

namespace ScreenCatcher.Logic
{
    public delegate void NotifyEventHandler(object sender, NotifyEventHandlerArgs args);

    public class NotifyEventHandlerArgs : EventArgs
    {
        private readonly string _fileName;

        public NotifyEventHandlerArgs(string fileName)
        {
            _fileName = fileName;
        }

        public string FileName
        {
            get { return _fileName; }
        }
    }
}