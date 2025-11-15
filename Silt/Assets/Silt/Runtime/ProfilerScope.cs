using System;
using UnityEngine.Profiling;

namespace Silt
{
    public readonly struct ProfilerScope : IDisposable
    {
        public ProfilerScope(string name)
        {
            Profiler.BeginSample(name);
        }
        void IDisposable.Dispose()
        {
            Profiler.EndSample();
        }
    }
}