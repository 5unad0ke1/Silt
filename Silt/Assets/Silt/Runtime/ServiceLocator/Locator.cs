using System;
using System.Collections.Generic;

namespace Silt
{
    public sealed class Locator : IDisposable
    {
        public Locator() { }

        public void Register<T>(T obj) where T : class
        {
            if (_locators.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException();
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
                throw new InvalidOperationException();
            }
            if (obj is not T result)
            {
                throw new Exception();
            }
            return result;
        }

        public void Clear()
        {
            _locators.Clear();
            _disposables.Clear();
        }
        public void Dispose()
        {
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[^(i + 1)]?.Dispose();
            }
            Clear();
        }

        private readonly Dictionary<Type, object> _locators = new();
        private readonly List<IDisposable> _disposables = new();
    }
}