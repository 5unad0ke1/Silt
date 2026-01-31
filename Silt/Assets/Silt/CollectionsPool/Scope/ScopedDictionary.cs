using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public readonly struct ScopedDictionary<TKey, TValue> : IDisposable
    {
        internal ScopedDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _value = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }
        public readonly Dictionary<TKey, TValue> Value
        {
            get => _value;
        }
        public void Dispose()
        {
            _value.Free();
        }
        private readonly Dictionary<TKey, TValue> _value;
    }
}