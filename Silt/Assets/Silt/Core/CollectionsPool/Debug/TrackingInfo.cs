using System;

namespace Silt.Core.CollectionsPool.Debug
{
    public struct TrackingInfo
    {
        public Func<int> GetFreeCount;
        public Func<int> GetBusyCount;
        public Type Type;
    }
}