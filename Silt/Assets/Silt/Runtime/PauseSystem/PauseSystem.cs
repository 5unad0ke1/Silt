using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Silt
{
    public sealed class PauseSystem<T> where T : struct, Enum
    {
        public PauseSystem()
        {
            _pausables = new();
        }
        public bool IsPaused()
        {
            return _reasonBits != 0;
        }
        public bool IsPaused(T filter)
        {
            return (_reasonBits & ToBits(filter)) != 0;
        }
        public void AddReason(T value)
        {
            var result = _reasonBits | ToBits(value);
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
            ulong valueBits = ToBits(value);
            ulong result = _reasonBits & ~valueBits;
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
            ulong result = ToBits(filter);

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
            ulong r = ToBits(filter);
            for (int i = 0; i < ULONG_SIZE; i++)
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
            foreach (var item in _pausables)
            {
                item.Value?.Clear();
            }
            _pausables.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ForEachActiveReason(ulong reasons, Action<int> action)
        {
            if(action is null)
                throw new ArgumentNullException(nameof(action));
            for (int i = 0; i < ULONG_SIZE; i++)
            {
                if (IsAllZeroBits(reasons, i))
                    return;
                if (!IsTrueWithBit(reasons, i))
                    continue;
                action(i);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsAllZeroBits(ulong data, int index)
            => (data >> index) == 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsTrueWithBit(ulong data, int index)
            => (data & (1ul << index)) != 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ToBits(T value) => Convert.ToUInt64(value);

        private const int ULONG_SIZE = sizeof(ulong) * 8;
        private ulong _reasonBits = 0;
        private readonly Dictionary<int, HashSet<IPauseable>> _pausables;
    }
}