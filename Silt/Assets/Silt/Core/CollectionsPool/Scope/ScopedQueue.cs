using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedQueue<T> : IDisposable
    {
        internal ScopedQueue(Queue<T> queue)
        {
            _value = queue ?? throw new ArgumentNullException(nameof(queue));
        }
        public readonly Queue<T> Value
        {
            get => _value;
        }
        public void Dispose()
        {
            _value.Free();
        }
        private readonly Queue<T> _value;
    }
}