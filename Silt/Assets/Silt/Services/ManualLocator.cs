

using Silt.Services;
using System;
using System.Collections.Generic;

public static class ManualLocator
{
    public static bool TryRegister<T>(T obj) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var type = typeof(T);

        if (_locators.ContainsKey(type))
        {
            return false;
        }

        if (obj is IDisposable disposable)
        {
            if (!_disposable.Add(disposable))
            {
                throw new InvalidOperationException($"Same IDisposable object already registered: type = {disposable.GetType().FullName}");
            }
        }

        _locators[type] = obj;
        return true;
    }
    public static void Clear()
    {
        foreach (var item in _disposable)
        {
            item.Dispose();
        }
        _disposable.Clear();
        _locators.Clear();
    }
    public static bool TryInject<T0>(IInjectable<T0> injectable)
    {
        if (!TryGetValue(out T0 t0))
            return false;

        injectable.Inject(t0);
        return true;
    }
    public static bool TryInject<T0, T1>(IInjectable<T0, T1> injectable)
    {
        if (!TryGetValue(out T0 t0))
            return false;
        if (!TryGetValue(out T1 t1))
            return false;

        injectable.Inject(t0, t1);
        return true;
    }
    public static bool TryInject<T0, T1, T2>(IInjectable<T0, T1, T2> injectable)
    {
        if (!TryGetValue(out T0 t0))
            return false;
        if (!TryGetValue(out T1 t1))
            return false;
        if (!TryGetValue(out T2 t2))
            return false;

        injectable.Inject(t0, t1, t2);
        return true;
    }
    public static bool TryInject<T0, T1, T2, T3>(IInjectable<T0, T1, T2, T3> injectable)
    {
        if (!TryGetValue(out T0 t0))
            return false;
        if (!TryGetValue(out T1 t1))
            return false;
        if (!TryGetValue(out T2 t2))
            return false;
        if (!TryGetValue(out T3 t3))
            return false;

        injectable.Inject(t0, t1, t2, t3);
        return true;
    }

    private static bool TryGetValue<T>(out T value)
    {
        value = default;
        if (!_locators.TryGetValue(typeof(T), out var result))
            return false;
        if (result is T instance)
        {
            value = instance;
        }
        else
        {
            throw new InvalidCastException($"Registered service of type {result.GetType().Name} cannot be cast to {typeof(T).Name}.");
        }
        return true;
    }

    private static readonly Dictionary<Type, object> _locators = new();
    private static readonly HashSet<IDisposable> _disposable = new();
}
