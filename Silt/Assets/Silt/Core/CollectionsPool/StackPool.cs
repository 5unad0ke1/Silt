using Silt.Core.CollectionsPool.Debug;
using System;
using System.Collections.Generic;

namespace Silt.Core.CollectionsPool
{
    public static class StackPool<T>
    {
        static StackPool()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            StackTrackingManager.Register<T>(
                () => _free.Count,
                () => _busy.Count);
#endif
        }
        public static Stack<T> Get()
        {
            lock (_lock)
            {
                if (_free.Count == 0)
                {
                    _free.Enqueue(new());
                }

                var stack = _free.Dequeue();

                _busy.Add(stack);

                return stack;
            }
        }
        public static ScopedStack<T> GetScoped()
        {
            return new(Get());
        }
        public static void Free(Stack<T> stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }
            lock (_lock)
            {
                if (!_busy.Contains(stack))
                {
                    throw new ArgumentException("Stack not in busy set.", nameof(stack));
                }

                stack.Clear();

                _busy.Remove(stack);

                _free.Enqueue(stack);
            }
        }
        private static readonly object _lock = new();
        private static readonly Queue<Stack<T>> _free = new();
        private static readonly HashSet<Stack<T>> _busy = new();
    }
}