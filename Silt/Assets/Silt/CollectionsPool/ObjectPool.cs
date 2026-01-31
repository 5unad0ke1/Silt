using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public sealed class ObjectPool<T> : IDisposable where T : class
    {
        public ObjectPool(Func<T> func)
        {
            _generator = func ?? throw new ArgumentNullException(nameof(func));

            _lock = new();
            _free = QueuePool<T>.Get();
            _busy = HashSetPool<T>.Get();
        }
        public T Get()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ObjectPool<T>));

                if (_free.Count == 0)
                {
                    _free.Enqueue(_generator());
                }

                var item = _free.Dequeue();

                _busy.Add(item);

                return item;
            }
        }
        public void Free(T value)
        {
            lock (_lock)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ObjectPool<T>));

                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!_busy.Contains(value))
                    throw new ArgumentException("Object not in busy set.", nameof(value));

                _busy.Remove(value);
                _free.Enqueue(value);
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;

            QueuePool<T>.Free(_free);
            HashSetPool<T>.Free(_busy);
            _free = null;
            _busy = null;
        }

        private bool _isDisposed = false;
        private readonly object _lock;
        private readonly Func<T> _generator;
        private Queue<T> _free;
        private HashSet<T> _busy;
    }
}