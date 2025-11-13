using System;
using System.Collections.Generic;

namespace Silt
{
    public static class DictionaryPool<TKey, TValue>
    {
        public static Dictionary<TKey, TValue> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Push(new());
                }

                var dictionary = _free.Pop();

                _busy.Add(dictionary);

                return dictionary;
            }
        }
        public static void Free(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            lock (_lock)
            {
                if (!_busy.Contains(dictionary))
                {
                    throw new ArgumentException();
                }

                dictionary.Clear();

                _busy.Remove(dictionary);

                _free.Push(dictionary);
            }
        }

        private static readonly object _lock = new();
        private static readonly Stack<Dictionary<TKey, TValue>> _free = new();
        private static readonly HashSet<Dictionary<TKey, TValue>> _busy = new();
    }
    public static class ExDictionaryPool
    {
        public static void Free<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            DictionaryPool<TKey, TValue>.Free(dictionary);
        }
    }
}