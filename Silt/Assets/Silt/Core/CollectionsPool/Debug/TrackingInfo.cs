using System;

namespace Silt.Core.CollectionsPool.Debug
{
    public readonly struct TrackingInfo
    {
        internal TrackingInfo(
            Func<int> getFreeCount,
            Func<int> getBusyCount,
            Type type)
        {
            GetFreeCount = getFreeCount;
            GetBusyCount = getBusyCount;
            Type = type;
        }

        public readonly Func<int> GetFreeCount;
        public readonly Func<int> GetBusyCount;
        public readonly Type Type;
    }
}