using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedHashSet<T> : IDisposable
    {
        internal ScopedHashSet(HashSet<T> hashSet)
        {
            HashSet = hashSet;
        }
        public void Dispose()
        {
            HashSet?.Free();
        }
        public readonly HashSet<T> HashSet;

        public static implicit operator HashSet<T>(ScopedHashSet<T> scopedStack)
            => scopedStack.HashSet;
    }
}