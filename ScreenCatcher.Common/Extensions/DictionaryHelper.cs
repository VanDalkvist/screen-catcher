using System.Collections.Generic;

namespace ScreenCatcher.Common.Extensions
{
    public static class DictionaryHelper
    {
        public static void Replace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary.Remove(key);
            dictionary.Add(key, value);
        }
    }
}