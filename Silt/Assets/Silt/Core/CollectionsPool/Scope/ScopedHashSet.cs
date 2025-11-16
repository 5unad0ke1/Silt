using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedHashSet<T> : IDisposable
    {
        internal ScopedHashSet(HashSet<T> hashSet)
        {
            _value = hashSet ?? throw new ArgumentNullException(nameof(hashSet));
        }
        public readonly HashSet<T> Value
        {
            get => _value;
        }
        public void Dispose()
        {
            _value.Free();
        }
        private readonly HashSet<T> _value;
    }
}