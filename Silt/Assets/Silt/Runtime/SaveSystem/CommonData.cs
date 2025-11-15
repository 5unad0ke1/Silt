namespace Silt.Runtime
{
    public class CommonData<T> where T : class, new()
    {
        public CommonData()
        {
            Load();
        }
        public void Load()
        {
            _data = SaveUtility.Load<T>(KEY);
        }
        public void Save()
        {
            SaveUtility.Save(KEY, _data);
        }

        public T Data => _data;

        private T _data;

        private const string KEY = nameof(T);
    }
}