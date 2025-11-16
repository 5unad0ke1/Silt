using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public struct ScopedStack<T> : IDisposable
    {
        internal ScopedStack(Stack<T> stack)
        {
            _isDisposed = false;
            _value = stack ?? throw new ArgumentNullException(nameof(stack));
        }
        public readonly Stack<T> Value
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(ScopedStack<T>));
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
        private Stack<T> _value;
        private bool _isDisposed;
    }
}