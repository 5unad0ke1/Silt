using System;
using System.Runtime.CompilerServices;

namespace Silt
{
    internal static class InterfaceUtility
    {
        public static bool TrySetCreatedDate<T>(T data) where T : class
        {
            if (data is ICreatedAt createdAt)
            {
                SetCreatedDate(createdAt);
                return true;
            }
            return false;
        }
        public static bool TryUpdateDate<T>(T data) where T : class
        {
            if (data is IUpdateAt updateAt)
            {
                SetUpdateDate(updateAt);
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCreatedDate(ICreatedAt data)
        {
            data.CreatedAtUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetUpdateDate(IUpdateAt data)
        {
            data.UpdatedAtUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}