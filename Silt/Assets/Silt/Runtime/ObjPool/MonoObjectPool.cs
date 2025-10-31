using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class MonoObjectPool<T> where T : Behaviour
    {
        private GameObject _prefab;

        private Transform _parent;

        private Stack<T> _stack = new(64);

        public MonoObjectPool(GameObject prefabObj, Transform parent)
        {
            _prefab = prefabObj;
            _parent = parent;
        }
        public T Pop()
        {
            if (_stack.Count <= 0)
            {
                AddObjMore();
            }
            return _stack.Pop();
        }
        public void Push(T value)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_stack.Contains(value))
            {
                Debug.LogWarning("重複があります");
                return;
            }
#endif 
            value.gameObject.SetActive(false);
            value.transform.position = Vector3.zero;
            _stack.Push(value);
        }
        private void AddObjMore()
        {
            var obj = Object.Instantiate(_prefab, _parent);
            obj.SetActive(false);
            var component = obj.GetComponent<T>();
            _stack.Push(component);

            if (component is IMonoPool<T>)
            {
                (component as IMonoPool<T>).SetMonoPoolManager(this);
            }
        }
    }
}