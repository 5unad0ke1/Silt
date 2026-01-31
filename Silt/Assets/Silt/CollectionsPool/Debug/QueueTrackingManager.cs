using System;
using System.Collections.Generic;

namespace Silt.CollectionsPool.Debug
{
    public static class QueueTrackingManager
    {
        public static IReadOnlyCollection<TrackingInfo> Collections => _collections;
        internal static void Register<T>(Func<int> getFreeCount, Func<int> getBusyCount)
        {
            lock (_lock)
            {
                _collections.Add(new TrackingInfo
                (
                    getFreeCount,
                    getBusyCount,
                    typeof(T)
                ));
            }
        }
        private static readonly object _lock = new();
        private static readonly List<TrackingInfo> _collections = new();
    }
}