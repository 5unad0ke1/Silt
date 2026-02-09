using System;
using System.Collections.Generic;

namespace Silt.Services.Debug
{
    public static class TrackingManager
    {
        public static IReadOnlyDictionary<Type, int> KeyValuePairs => _injectCounter;
        public static void IncreaseInjectCount<T>()
        {
            _injectCounter[typeof(T)]++;
        }
        public static void Clear()
        {
            _injectCounter.Clear();
        }

        private static readonly Dictionary<Type, int> _injectCounter = new();
    }
}