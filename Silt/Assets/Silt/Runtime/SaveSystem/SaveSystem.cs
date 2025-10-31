namespace Silt
{
    public static class SaveSystem<CommonData, SlotData> where CommonData : class, new() where SlotData : class, new()
    {
        static SaveSystem() 
        {
            Common = new();
            Slot = new();
        }

        public static readonly CommonData<CommonData> Common;
        public static readonly SlotData<SlotData> Slot;
    }
}