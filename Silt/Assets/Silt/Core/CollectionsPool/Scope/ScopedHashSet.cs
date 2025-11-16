using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public struct ScopedHashSet<T> : IDisposable
    {
        internal ScopedHashSet(HashSet<T> hashSet)
        {
            _isDisposed = false;
            _value = hashSet ?? throw new ArgumentNullException(nameof(hashSet));
        }
        public readonly HashSet<T> Value
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ScopedHashSet<T>));
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
        private HashSet<T> _value;
        private bool _isDisposed;
    }
}