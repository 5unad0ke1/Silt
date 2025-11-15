using System;
using System.Collections.Generic;

namespace Silt.Core
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
            if (_isDisposed)
                return;

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
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(Locator));

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
            if (_isDisposed)
                return;

            _locators.Clear();

            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[^(i + 1)]?.Dispose();
            }
            _disposables.Clear();
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (_disposables is not null)
            {
                for (int i = 0; i < _disposables.Count; i++)
                {
                    _disposables[^(i + 1)]?.Dispose();
                }

                _disposables.Free();
                _disposables = null;
            }

            if (_locators is not null)
            {
                _locators.Free();
                _locators = null;
            }
        }
        private bool _isDisposed = false;
        private Dictionary<Type, object> _locators;
        private List<IDisposable> _disposables;
    }
}