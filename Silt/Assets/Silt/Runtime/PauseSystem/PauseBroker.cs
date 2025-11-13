using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Silt.Systems
{
    public sealed class PauseBroker<T> : IDisposable where T : unmanaged, Enum
    {
        public PauseBroker()
        {
            string message = "PauseBroker only supports enums with an underlying type of byte.";
            int enumSize = PauseUtility.GetEnumSize<T>();
            if (enumSize > sizeof(byte))
            {
                throw new InvalidOperationException($"{message}\nActual size of generic type '{typeof(T).Name}' is {enumSize} bytes.");
            }

            _pausables = DictionaryPool<int, HashSet<IPauseable>>.Get();
        }
        public bool IsPaused()
        {
            return _reasonBits != 0;
        }
        public bool IsPaused(T filter)
        {
            return (_reasonBits & PauseUtility.ToBits(filter)) != 0;
        }
        public void AddReason(T value)
        {
            if (_isDisposed)
                return;

            var result = (byte)(_reasonBits | PauseUtility.ToBits(value));
            _reasonBits = result;

            ForEachActiveReason(result, i =>
            {
                if (!_pausables.TryGetValue(i, out var hashSet))
                    return;
                foreach (var item in hashSet)
                {
                    item.Pause();
                }
            });
        }
        public void RemoveReason(T value)
        {
            if (_isDisposed)
                return;

            byte valueBits = PauseUtility.ToBits(value);
            byte result = (byte)(_reasonBits & ~valueBits);
            _reasonBits = result;

            ForEachActiveReason(valueBits, i =>
            {
                if (!_pausables.TryGetValue(i, out var hashset))
                    return;
                foreach (var item in hashset)
                {
                    item.Resume();
                }
            });
        }
        public void Register(IPauseable pauseable, T filter)
        {
            if (_isDisposed)
                return;

            byte result = PauseUtility.ToBits(filter);

            ForEachActiveReason(result, i =>
            {
                if (_pausables.TryGetValue(i, out var hashSet))
                {
                    hashSet.Add(pauseable);
                }
                else
                {
                    _pausables[i] = new(1) { pauseable };
                }
            });
        }
        public void Unregister(IPauseable pauseable, T filter)
        {
            if (_isDisposed)
                return;

            byte r = PauseUtility.ToBits(filter);
            for (int i = 0; i < BYTE_SIZE; i++)
            {
                if (!IsTrueWithBit(r, i))
                    return;
                if (_pausables.TryGetValue(i, out var hashSet))
                {
                    hashSet.Remove(pauseable);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Clear()
        {
            if (_isDisposed)
                return;

            foreach (var item in _pausables)
            {
                item.Value?.Clear();
            }
            _pausables.Clear();
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;

            Clear();
            _pausables.Free();
            _isDisposed = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ForEachActiveReason(byte reasons, Action<int> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            for (int i = 0; i < BYTE_SIZE; i++)
            {
                if (IsAllZeroBits(reasons, i))
                    return;
                if (!IsTrueWithBit(reasons, i))
                    continue;
                action(i);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsAllZeroBits(byte data, int index)
            => (data >> index) == 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsTrueWithBit(byte data, int index)
            => (data & (1 << index)) != 0;

        private bool _isDisposed = false;
        private const int BYTE_SIZE = sizeof(byte) * 8;
        private byte _reasonBits = 0;
        private Dictionary<int, HashSet<IPauseable>> _pausables;
    }
}