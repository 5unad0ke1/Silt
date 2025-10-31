using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Silt.Systems
{
    public sealed class PauseLight<T> where T : unmanaged, Enum
    {
        public PauseLight()
        {
            string message = "PauseLight only supports enums with an underlying type of byte.";

            int enumSize = PauseUtility.GetEnumSize<T>();
            if (enumSize > sizeof(byte))
            {
                throw new InvalidOperationException($"{message}\nActual size of generic type '{typeof(T).Name}' is {enumSize} bytes.");
            }
        }

        public T CurrentEnum => (T)Enum.ToObject(typeof(T), _reasonBits);
        public uint CurrentBits => _reasonBits;
        public bool IsPaused()
            => _reasonBits != 0;
        public bool IsPaused(T filter)
            => (_reasonBits & PauseUtility.ToBits(filter)) != 0;
        public void AddReason(T values)
        {
            byte previousBits = _reasonBits;
            byte currentBits = (byte)(previousBits | PauseUtility.ToBits(values));
            _reasonBits = currentBits;

            UpdatePauseState(_pauseables, previousBits, currentBits);
        }
        public void RemoveReason(T values)
        {
            byte previousBits = _reasonBits;
            byte currentBits = (byte)(previousBits & ~PauseUtility.ToBits(values));
            _reasonBits = currentBits;

            UpdatePauseState(_pauseables, previousBits, currentBits);
        }
        public void Register(IPauseable pauseable, T filter)
        {
            _pauseables[pauseable] = PauseUtility.ToBits(filter);
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
        public override string ToString()
             => $"{typeof(T).Name}: {_reasonBits:X2} ({CurrentEnum})";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UpdatePauseState(Dictionary<IPauseable, byte> data, in byte previousBits, in byte currentBits)
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

        private byte _reasonBits = 0;
        private readonly Dictionary<IPauseable, byte> _pauseables = new();
    }
}