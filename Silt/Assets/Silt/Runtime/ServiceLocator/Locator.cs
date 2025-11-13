using System;
using System.Collections.Generic;

namespace Silt
{
    public sealed class Locator : IDisposable
    {
        public Locator()
        {
            _locators = DictionaryPool<Type, object>.Get();
            _disposables = ListPool<IDisposable>.Get();
        }

        public void Register<T>(T obj) where T : class
        {
            if (_locators.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"A service of type {typeof(T).Name} is already registered.");
            }

            if (obj is IDisposable disposable)
            {
                _disposables.Add(disposable);
            }
            _locators.Add(typeof(T), obj);
        }
        public T Get<T>() where T : class
        {
            if (!_locators.TryGetValue(typeof(T), out var obj))
            {
                throw new InvalidOperationException($"No service of type {typeof(T).Name} is registered.");
            }
            if (obj is not T result)
            {
                throw new InvalidCastException($"Registered service of type {obj.GetType().Name} cannot be cast to {typeof(T).Name}.");
            }
            return result;
        }

        public void Clear()
        {
            Dispose();
        }
        public void Dispose()
        {
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[^(i + 1)]?.Dispose();
            }
            _disposables.Clear();
            _disposables.Free();
            _disposables = null;

            _locators.Clear();

        }

        private Dictionary<Type, object> _locators;
        private List<IDisposable> _disposables;
    }
}