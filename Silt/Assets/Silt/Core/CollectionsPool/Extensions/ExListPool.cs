using Silt.Core.CollectionsPool;
using System.Collections.Generic;

namespace Silt
{
    /// <summary>
    /// Extensions for <see cref="ListPool{T}"/>
    /// </summary>
    public static class ExListPool
    {
        public static void Free<T>(this List<T> list)
        {
            ListPool<T>.Free(list);
        }
    }
}