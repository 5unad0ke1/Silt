using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    /// <summary>
    /// Extensions for <see cref="StackPool{T}"/>
    /// </summary>
    public static class ExStackPool
    {
        public static void Free<T>(this Stack<T> stack)
        {
            StackPool<T>.Free(stack);
        }
    }
}