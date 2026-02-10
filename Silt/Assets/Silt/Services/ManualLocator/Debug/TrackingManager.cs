using System;
using System.Collections.Generic;

namespace Silt.Services.Debug
{
    public static class TrackingManager
    {
        public static IReadOnlyDictionary<Type, int> KeyValuePairs => _injectCounter;
        public static void IncreaseInjectCount<T>()
        {
            if (_injectCounter.ContainsKey(typeof(T)))
            {
                _injectCounter[typeof(T)]++;
            }
            else
            {
                _injectCounter.Add(typeof(T), 1);
            }
        }
        public static void Clear()
        {
            _injectCounter.Clear();
        }

        private static readonly Dictionary<Type, int> _injectCounter = new();
    }
}