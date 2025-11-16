using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedQueue<T> : IDisposable
    {
        internal ScopedQueue(Queue<T> queue)
        {
            Queue = queue;
        }
        public void Dispose()
        {
            Queue?.Free();
        }
        public readonly Queue<T> Queue;

        public static implicit operator Queue<T>(ScopedQueue<T> scopedStack)
            => scopedStack.Queue;
    }
}