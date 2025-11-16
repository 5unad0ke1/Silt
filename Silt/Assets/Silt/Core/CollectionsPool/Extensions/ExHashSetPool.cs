using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    /// <summary>
    /// Extensions for <see cref="HashSetPool{T}"/>
    /// </summary>
    public static class ExHashSetPool
    {
        public static void Free<T>(this HashSet<T> hashSet)
        {
            HashSetPool<T>.Free(hashSet);
        }
    }
}