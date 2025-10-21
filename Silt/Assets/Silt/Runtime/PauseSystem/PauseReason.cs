using System;

namespace Silt
{
    [Flags]
    public enum PauseReason : uint
    {
        None = 0,
        UserPaused = 1 << 0,
        SystemMaintenance = 1 << 1,
        NetworkIssues = 1 << 2,
        HighLoad = 1 << 3,
    }
}