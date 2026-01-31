using System.Collections.Generic;

namespace Silt.CollectionsPool
{
    /// <summary>
    /// Extensions for <see cref="DictionaryPool{TKey, TValue}"/>
    /// </summary>
    public static class ExDictionaryPool
    {
        public static void Free<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            DictionaryPool<TKey, TValue>.Free(dictionary);
        }
    }
}