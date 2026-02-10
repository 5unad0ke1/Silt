namespace Silt.Services
{
    public static class GlobalLocator
    {
        static GlobalLocator() { }
        public static void Register<T>(T obj) where T : class
        {
            _locator.Register(obj);
        }
        public static T Get<T>() where T : class
        {
            return _locator.Get<T>();
        }
        public static void Clear()
        {
            _locator.Clear();
        }
        private static readonly Locator _locator = new();
    }
}