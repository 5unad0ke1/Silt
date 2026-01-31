using Silt.CollectionsPool.Debug;
using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public static class ListPool<T>
    {
        static ListPool()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            ListTrackingManager.Register<T>(
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
        public static List<T> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Enqueue(new());
                }

                var array = _free.Dequeue();

                _busy.Add(array);

                return array;
            }
        }
        public static ScopedList<T> GetScoped()
        {
            return new(Get());
        }

        public static void Free(List<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            lock (_lock)
            {
                if (!_busy.Contains(list))
                {
                    throw new ArgumentException("List not in busy set.", nameof(list));
                }

                list.Clear();

                _busy.Remove(list);

                _free.Enqueue(list);
            }
        }
        private static readonly object _lock = new();
        private static readonly Queue<List<T>> _free = new();
        private static readonly HashSet<List<T>> _busy = new();
    }
}