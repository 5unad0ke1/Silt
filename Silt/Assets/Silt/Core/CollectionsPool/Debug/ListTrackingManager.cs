using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool.Debug
{
    public static class ListTrackingManager
    {
        public static IReadOnlyCollection<TrackingInfo> Collections => _collections;
        internal static void Register<T>(Func<int> getFreeCount, Func<int> getBusyCount)
        {
            _collections.Add(new TrackingInfo(
                getFreeCount,
                getBusyCount,
                typeof(T)
            ));
        }
        private static readonly List<TrackingInfo> _collections = new();
    }
}