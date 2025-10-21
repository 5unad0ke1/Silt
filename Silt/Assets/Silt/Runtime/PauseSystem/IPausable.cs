using System;

namespace Silt
{
    public interface IPauseable<T> where T : notnull, Enum
    {
        void Pause(T reason);
        void Resume(T reason);
    }
}