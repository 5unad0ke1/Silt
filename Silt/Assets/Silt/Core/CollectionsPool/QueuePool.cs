using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
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
        public static ScopedQueue<T> GetScoped()
        {
            return new(Get());
        }

        public static void Free(Queue<T> queue)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }
            lock (_lock)
            {
                if (!_busy.Contains(queue))
                {
                    throw new ArgumentException("Queue not in busy set.", nameof(queue));
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
}