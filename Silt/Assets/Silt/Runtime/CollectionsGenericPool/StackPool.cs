using System;
using System.Collections.Generic;

namespace Silt
{
    public static class StackPool<T>
    {
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

        public static void Free(Stack<T> stack)
        {
            lock (_lock)
            {
                if (!_busy.Contains(stack))
                {
                    throw new ArgumentException();
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

    public static class ExStackPool
    {
        public static void Free<T>(this Stack<T> stack)
        {
            StackPool<T>.Free(stack);
        }
    }
}