using System;
using System.Collections.Generic;

namespace Silt
{
    public static class ListPool<T>
    {
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
                    throw new ArgumentException();
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

    public static class ExListPool
    {
        public static void Free<T>(this List<T> list)
        {
            ListPool<T>.Free(list);
        }
    }
}