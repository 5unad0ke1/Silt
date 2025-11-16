using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedDictionary<TKey, TValue> : IDisposable
    {
        internal ScopedDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _isDisposed = false;
            _value = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }
        public readonly Dictionary<TKey, TValue> Value
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ScopedDictionary<TKey, TValue>));
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

        private bool _isDisposed;
        private Dictionary<TKey, TValue> _value;
    }
}