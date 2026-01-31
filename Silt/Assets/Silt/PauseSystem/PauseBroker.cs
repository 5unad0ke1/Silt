using Silt.CollectionsPool;
using Silt.PauseSystem.Debug;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Silt.PauseSystem
{
    public sealed class PauseBroker<T> : IDisposable where T : unmanaged, Enum
    {
        public PauseBroker(string name = "default")
        {
            int enumSize = PauseUtility.GetEnumSize<T>();
            if (enumSize > BYTE_SIZE)
            {
                throw new InvalidOperationException($"PauseBroker only supports enums with an underlying type of byte.\nActual size of generic type '{typeof(T).Name}' is {enumSize} bytes.");
            }
            _pausables = DictionaryPool<int, HashSet<IPauseable>>.Get();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            PauseSystemTracking.Register(this, typeof(T), typeof(PauseBroker<>), name, () => _reasonBits);
#endif
        }
        public bool IsPaused()
            => _reasonBits != 0;

        public bool IsPaused(T filter)
                => (_reasonBits & PauseUtility.ToBits(filter)) != 0;
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
                    hashSet = HashSetPool<IPauseable>.Get();
                    hashSet.Add(pauseable);
                    _pausables[i] = hashSet;
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
                if (IsAllZeroBits(r, i))
                    return;
                if (!IsTrueWithBit(r, i))
                    continue;
                if (_pausables.TryGetValue(i, out var hashSet))
                {
                    hashSet.Remove(pauseable);
                }
                else
                {
                    throw new InvalidOperationException($"Pauseable not found in filter bit {i}. Ensure Register was called before Unregister.");
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
            _reasonBits = 0;
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;

            foreach (var item in _pausables)
            {
                item.Value?.Free();
            }
            _pausables.Free();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            PauseSystemTracking.Unregister(this);
#endif
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