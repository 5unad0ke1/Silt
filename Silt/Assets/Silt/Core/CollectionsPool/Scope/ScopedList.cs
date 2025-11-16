using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public struct ScopedList<T> : IDisposable
    {
        internal ScopedList(List<T> list)
        {
            _isDisposed = false;
            _value = list ?? throw new ArgumentNullException(nameof(list));
        }
        public readonly List<T> Value
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ScopedList<T>));
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
        private List<T> _value;
        private bool _isDisposed;
    }
}