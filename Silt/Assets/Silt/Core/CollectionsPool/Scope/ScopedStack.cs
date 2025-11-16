using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedStack<T> : IDisposable
    {
        internal ScopedStack(Stack<T> stack)
        {
            Stack = stack;
        }
        public void Dispose()
        {
            Stack?.Free();
        }
        public readonly Stack<T> Stack;

        public static implicit operator Stack<T>(ScopedStack<T> scopedStack)
            => scopedStack.Stack;
    }
}