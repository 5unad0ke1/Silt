using System;
using UnityEngine.Profiling;

namespace Silt.Utility
{
    public readonly struct ProfilerScope : IDisposable
    {
        public ProfilerScope(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("ProfilerScope name cannot be null or whitespace.", nameof(name));
            Profiler.BeginSample(name);
        }
        void IDisposable.Dispose()
        {
            Profiler.EndSample();
        }
    }
}