using Silt.CollectionsPool.Debug;
using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public static class DictionaryPool<TKey, TValue>
    {
        static DictionaryPool()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DictionaryTrackingManager.Register<TKey, TValue>(
                () =>
                {
                    lock (_lock)
                    {
                        return _free.Count;
                    }
                },
                () =>
                {
                    lock (_lock)
                    {
                        return _busy.Count;
                    }
                });
#endif
        }
        public static Dictionary<TKey, TValue> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Enqueue(new());
                }

                var dictionary = _free.Dequeue();

                _busy.Add(dictionary);

                return dictionary;
            }
        }
        public static ScopedDictionary<TKey, TValue> GetScoped()
        {
            return new(Get());
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
                    throw new ArgumentException("Dictionary not in busy set.", nameof(dictionary));
                }

                dictionary.Clear();

                _busy.Remove(dictionary);

                _free.Enqueue(dictionary);
            }
        }

        private static readonly object _lock = new();
        private static readonly Queue<Dictionary<TKey, TValue>> _free = new();
        private static readonly HashSet<Dictionary<TKey, TValue>> _busy = new();
    }
}