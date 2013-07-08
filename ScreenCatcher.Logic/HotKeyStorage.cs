using System;
using System.Collections;
using System.Collections.Generic;

using ScreenCatcher.Core;

namespace ScreenCatcher.Logic
{
    internal class HotKeyStorage : IEnumerable<Int16>
    {
        private readonly IDictionary<Int16, ICatchScreenWorker> _actions = new Dictionary<short, ICatchScreenWorker>();

        public void Add(Int16 key, ICatchScreenWorker worker)
        {
            _actions.Add(key, worker);
        }

        public IEnumerator<short> GetEnumerator()
        {
            return _actions.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ICatchScreenWorker this[Int16 key]
        {
            get { return _actions[key]; }
        }

        public void Clear()
        {
            _actions.Clear();
        }
    }
}