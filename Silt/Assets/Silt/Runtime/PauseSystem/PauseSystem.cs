using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Silt
{
    public sealed class PauseSystem<T> where T : unmanaged, Enum
    {
        public T CurrentEnum => (T)Enum.ToObject(typeof(T), _reasonBits);
        public ulong CurrentBits => _reasonBits;
        public bool IsPaused()
            => _reasonBits != 0;
        public bool IsPaused(T filter)
            => (_reasonBits & ToBits(filter)) != 0;
        public void AddReason(T values)
        {
            ulong previousBits = _reasonBits;
            ulong currentBits = previousBits | ToBits(values);
            _reasonBits = currentBits;

            UpdatePauseState(_pauseables, previousBits, currentBits);
        }
        public void RemoveReason(T values)
        {
            ulong previousBits = _reasonBits;
            ulong currentBits = previousBits & ~ToBits(values);
            _reasonBits = currentBits;

            UpdatePauseState(_pauseables, previousBits, currentBits);
        }
        public void Register(IPauseable pauseable, T filter)
        {
            _pauseables[pauseable] = ToBits(filter);
        }
        public void Unregister(IPauseable pauseable)
        {
            _pauseables.Remove(pauseable);
        }
        public void Clear()
        {
            _pauseables.Clear();
            _reasonBits = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UpdatePauseState(Dictionary<IPauseable, ulong> data, in ulong previousBits, in ulong currentBits)
        {
            bool wasPaused;
            bool isPaused;

            foreach (var item in data)
            {
                wasPaused = previousBits != 0 && (item.Value & previousBits) != 0;
                isPaused = currentBits != 0 && (item.Value & currentBits) != 0;

                if (wasPaused && !isPaused)
                    item.Key.Resume();
                else if (!wasPaused && isPaused)
                    item.Key.Pause();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ToBits(T value) => Convert.ToUInt64(value);

        private ulong _reasonBits = 0;
        private readonly Dictionary<IPauseable, ulong> _pauseables = new();
    }
}