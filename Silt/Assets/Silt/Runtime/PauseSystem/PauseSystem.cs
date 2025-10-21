using System;
using System.Collections.Generic;

namespace Silt
{
    public sealed class PauseSystem<T> where T : notnull, Enum
    {
        public PauseSystem()
        {
            _reasons = default;
            _pauseables = new HashSet<IPauseable<T>>();
        }
        public bool IsPaused()
        {
            return !_reasons.Equals(default(T));
        }
        public bool IsPaused(T filter)
        {
            return _reasons.HasFlag(filter);
        }
        public void AddReason(T value)
        {
            if (_locked)
                return;
            _locked = true;

            ulong v = Convert.ToUInt64(value);
            ulong r = Convert.ToUInt64(_reasons);
            _reasons = (T)Enum.ToObject(typeof(T), r | v);

            foreach (var pauseable in _pauseables)
            {
                pauseable.Pause(_reasons);
            }
            _locked = false;
        }
        public void RemoveReason(T value)
        {
            if (_locked)
                return;
            _locked = true;

            ulong v = Convert.ToUInt64(value);
            ulong r = Convert.ToUInt64(_reasons);
            _reasons = (T)Enum.ToObject(typeof(T), r & ~v);

            foreach (var pauseable in _pauseables)
            {
                pauseable.Resume(_reasons);
            }
            _locked = false;
        }
        public void Register(IPauseable<T> pauseable)
        {
            _pauseables.Add(pauseable);
        }
        public void Unregister(IPauseable<T> pauseable)
        {
            _pauseables.Remove(pauseable);
        }

        public void Clear()
        {
            _pauseables.Clear();
            _reasons = default;
        }

        private bool _locked;
        private T _reasons;
        private readonly HashSet<IPauseable<T>> _pauseables;

    }
}