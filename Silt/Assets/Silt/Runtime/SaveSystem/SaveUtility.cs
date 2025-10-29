using System.Runtime.CompilerServices;
using UnityEngine;

namespace Silt
{
    internal static class SaveUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Load<T>(string key) where T : class, new()
        {
            var strData = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(strData))
            {
                return new();
            }

            try
            {
                return JsonUtility.FromJson<T>(strData);
            }
            catch
            {
                return new();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Save<T>(string key, T data)
        {
            var strData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, strData);
            PlayerPrefs.Save();
        }
    }
}