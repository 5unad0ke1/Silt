using System;

namespace Silt.Core.PauseSystem.Debug
{
    public struct TrackingInfo
    {
        internal TrackingInfo(string name, Type flagType, Type type, Func<byte> getFlag)
        {
            Name = name;
            FlagType = flagType;
            Type = type;
            GetFlag = getFlag;
        }

        public readonly string Name;
        public readonly Type FlagType;
        public readonly Type Type;
        public readonly Func<byte> GetFlag;
    }
}