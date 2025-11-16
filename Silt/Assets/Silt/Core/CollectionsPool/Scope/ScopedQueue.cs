using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public struct ScopedQueue<T> : IDisposable
    {
        internal ScopedQueue(Queue<T> queue)
        {
            _isDisposed = false;
            _value = queue ?? throw new ArgumentNullException(nameof(queue));
        }
        public readonly Queue<T> Value
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ScopedQueue<T>));
                return _value;
            }
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;

            _value.Free();
            _value = null;
        }
        private Queue<T> _value;
        private bool _isDisposed;
    }
}