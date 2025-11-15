using System;
using System.Collections.Generic;

namespace Silt.ObjPool
{
    public static class ClassPool<T> where T : class, new()
    {
        static ClassPool()
        {
            _lock = new();
            _free = new();
            _busy = new();
        }
        public static T New()
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

        public static void Free(T list)
        {
            lock (_lock)
            {
                if (!_busy.Contains(list))
                {
                    throw new ArgumentException("The list to free is not in use by the pool.", nameof(list));
                }

                _busy.Remove(list);

                _free.Enqueue(list);
            }
        }

        private readonly static object _lock;
        private readonly static Queue<T> _free;
        private readonly static HashSet<T> _busy;
    }
}