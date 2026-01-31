using UnityEngine;

namespace Silt
{
    internal static class SaveUtility
    {
        public static T Load<T>(string key) where T : class, new()
        {
            if (!FileUtility.TryRead(key, out var strData))
                return ConstructData<T>();

            if (string.IsNullOrEmpty(strData))
            {
                return ConstructData<T>();
            }
            try
            {
                var data = JsonUtility.FromJson<T>(strData);
                return data;
            }
            catch
            {
                return ConstructData<T>();
            }
        }
        public static void Save<T>(string key, T data) where T : class, new()
        {
            if(data == null)
                return;

            InterfaceUtility.TryUpdateDate(data);

            var strData = JsonUtility.ToJson(data);
            FileUtility.Write(key, strData);
        }
        private static T ConstructData<T>() where T : class, new()
        {
            var result = new T();

            InterfaceUtility.TrySetCreatedDate(result);
            InterfaceUtility.TryUpdateDate(result);

            return result;
        }
    }
}