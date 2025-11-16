using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public readonly struct ScopedList<T> : IDisposable
    {
        internal ScopedList(List<T> list)
        {
            List = list;
        }
        public void Dispose()
        {
            List?.Free();
        }
        public readonly List<T> List;

        public static implicit operator List<T>(ScopedList<T> scopedStack)
            => scopedStack.List;
    }
}