using System;

namespace Silt.CollectionsPool.Debug
{
    public struct DictionaryTrackingInfo
    {
        public Func<int> GetFreeCount;
        public Func<int> GetBusyCount;
        public Type KeyType;
        public Type ValueType;
    }
}