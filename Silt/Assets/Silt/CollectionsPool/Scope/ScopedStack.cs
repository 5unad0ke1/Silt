using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    public readonly struct ScopedStack<T> : IDisposable
    {
        internal ScopedStack(Stack<T> stack)
        {
            _value = stack ?? throw new ArgumentNullException(nameof(stack));
        }
        public readonly Stack<T> Value
        {
            get => _value;
        }
        public void Dispose()
        {
            _value.Free();
        }
        private readonly Stack<T> _value;
    }
}