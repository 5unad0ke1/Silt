using System;
using System.Collections.Generic;

namespace Silt.Core.PauseSystem.Debug
{
    public static class PauseSystemTracking
    {
        public static ICollection<KeyValuePair<object, TrackingInfo>> Infos => _infos;

        internal static void Register(
            object obj,
            Type flagType,
            Type type,
            string name,
            Func<byte> getFlag)
        {
            _infos[obj] = new TrackingInfo(name, flagType, type, getFlag);
        }
        internal static void Unregister(object obj)
        {
            _infos.Remove(obj);
        }

        private readonly static Dictionary<object, TrackingInfo> _infos = new();
    }
}