using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool.Debug
{
    public static class HashSetTrackingManager
    {
        public static IReadOnlyCollection<TrackingInfo> Collections => _collections;
        internal static void Register<T>(Func<int> getFreeCount, Func<int> getBusyCount)
        {
            _collections.Add(new TrackingInfo
            {
                GetFreeCount = getFreeCount,
                GetBusyCount = getBusyCount,
                Type = typeof(T)
            });
        }
        private static readonly List<TrackingInfo> _collections = new();
    }
}