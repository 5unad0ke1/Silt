using System;

namespace Silt.Pause
{
    [Flags]
    public enum PauseReason : byte
    {
        None = 0,
        All = byte.MaxValue,
        UserPaused = 1 << 0,
        SystemMaintenance = 1 << 1,
        NetworkIssues = 1 << 2,
        HighLoad = 1 << 3,
    }
}