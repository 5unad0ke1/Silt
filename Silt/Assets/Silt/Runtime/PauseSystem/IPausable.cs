using System;

namespace Silt
{
    public interface IPauseable<T>
    {
        void Pause();
        void Resume();
    }
}