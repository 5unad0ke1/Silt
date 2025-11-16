using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedList<T> : IDisposable
    {
        internal ScopedList(List<T> list)
        {
            _value = list ?? throw new ArgumentNullException(nameof(list));
        }
        public readonly List<T> Value
        {
            get => _value;
        }
        public void Dispose()
        {
            _value.Free();
        }
        private readonly List<T> _value;
    }
}