namespace Silt
{
    public class SlotData<T> where T : class, new()
    {
        public SlotData()
        {
            _data = null;
        }
        public void Load(string key)
        {
            _data = SaveUtility.Load<T>(key);
        }
        public void Save(string key)
        {
            SaveUtility.Save(key, _data);
        }
        public T Data => _data;

        private T _data;
    }
}