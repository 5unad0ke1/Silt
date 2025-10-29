using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Silt
{
    internal static class FileUtility
    {
        public static void Write(string name, string contents)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Directory.CreateDirectory(SAVE_PATH);
            File.WriteAllText(ToFullPath(name), contents);
            Debug.Log($"{SAVE_PATH}‚É•Û‘¶");
        }
        public static bool TryRead(string name, out string result)
        {
            result = null;

            if (string.IsNullOrEmpty(name))
                throw new Exception();

            var fullPath = ToFullPath(name);
            if (!File.Exists(fullPath))
                return false;

            result = File.ReadAllText(fullPath);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ToFullPath(string name)
            => Path.Combine(SAVE_PATH, name);
        private static readonly string SAVE_PATH = Application.persistentDataPath;
    }
}