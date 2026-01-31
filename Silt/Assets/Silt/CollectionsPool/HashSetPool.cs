using Silt.CollectionsPool.Debug;
using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public static class HashSetPool<T>
    {
        static HashSetPool()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            HashSetTrackingManager.Register<T>(
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
        public static HashSet<T> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Enqueue(new());
                }

                var hashSet = _free.Dequeue();

                _busy.Add(hashSet);

                return hashSet;
            }
        }
        public static ScopedHashSet<T> GetScoped()
        {
            return new(Get());
        }

        public static void Free(HashSet<T> hashSet)
        {
            lock (_lock)
            {
                if (hashSet is null)
                {
                    throw new ArgumentNullException(nameof(hashSet));
                }
                if (!_busy.Contains(hashSet))
                {
                    throw new ArgumentException("HashSet not in busy set.", nameof(hashSet));
                }

                hashSet.Clear();

                _busy.Remove(hashSet);

                _free.Enqueue(hashSet);
            }
        }
        private static readonly object _lock = new();
        private static readonly Queue<HashSet<T>> _free = new();
        private static readonly HashSet<HashSet<T>> _busy = new();
    }
}