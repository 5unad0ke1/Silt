using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedDictionary<TKey, TValue> : IDisposable
    {
        internal ScopedDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Dictionary = dictionary;
        }
        public void Dispose()
        {
            Dictionary?.Free();
        }
        public readonly Dictionary<TKey, TValue> Dictionary;

        public static implicit operator Dictionary<TKey, TValue>(ScopedDictionary<TKey, TValue> scopedStack)
            => scopedStack.Dictionary;
    }
}