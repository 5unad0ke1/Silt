using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    /// <summary>
    /// Extensions for <see cref="QueuePool{T}"/>
    /// </summary>
    public static class ExQueuePool
    {
        public static void Free<T>(this Queue<T> queue)
        {
            QueuePool<T>.Free(queue);
        }
    }
}