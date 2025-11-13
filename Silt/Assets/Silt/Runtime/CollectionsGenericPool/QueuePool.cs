using System;
using System.Collections.Generic;

namespace Silt
{
    public static class QueuePool<T>
    {
        public static Queue<T> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Enqueue(new());
                }

                var queue = _free.Dequeue();

                _busy.Add(queue);

                return queue;
            }
        }

        public static void Free(Queue<T> queue)
        {
            lock (_lock)
            {
                if (!_busy.Contains(queue))
                {
                    throw new ArgumentException();
                }

                queue.Clear();

                _busy.Remove(queue);

                _free.Enqueue(queue);
            }
        }
        private static readonly object _lock = new();
        private static readonly Queue<Queue<T>> _free = new();
        private static readonly HashSet<Queue<T>> _busy = new();
    }

    public static class ExQueuePool
    {
        public static void Free<T>(this Queue<T> queue)
        {
            QueuePool<T>.Free(queue);
        }
    }
}