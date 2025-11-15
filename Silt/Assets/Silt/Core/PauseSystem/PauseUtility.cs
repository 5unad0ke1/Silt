using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Silt.Core
{
    internal static class PauseUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToBits<T>(T value) where T : unmanaged, Enum
            => Convert.ToByte(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetEnumSize<T>() where T : unmanaged, Enum
        {
            Type underlying = Enum.GetUnderlyingType(typeof(T));
            return Marshal.SizeOf(underlying);
        }
    }
}