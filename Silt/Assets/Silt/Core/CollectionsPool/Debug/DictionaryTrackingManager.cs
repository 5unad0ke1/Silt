using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool.Debug
{
    public static class DictionaryTrackingManager
    {
        public static IReadOnlyCollection<DictionaryTrackingInfo> Collections => _collections;
        internal static void Register<TKey, TValue>(Func<int> getFreeCount, Func<int> getBusyCount)
        {
            _collections.Add(new DictionaryTrackingInfo
            {
                GetFreeCount = getFreeCount,
                GetBusyCount = getBusyCount,
                KeyType = typeof(TKey),
                ValueType = typeof(TValue)
            });
        }
        private static readonly List<DictionaryTrackingInfo> _collections = new();
    }
}