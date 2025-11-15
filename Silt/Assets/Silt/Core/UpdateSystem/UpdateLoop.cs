using System;
using System.Collections.Generic;

namespace Silt.Update
{
    public class UpdateLoop
    {
        public UpdateLoop()
        {
            _updatables = new(128);
            _updatablesSet = new(128);
        }
        public UpdateLoop(int capacity)
        {
            _updatables = new(capacity);
            _updatablesSet = new(capacity);
        }
        public int Count => _updatables.Count;
        public void OnUpdate()
        {
            int count = _updatables.Count;
            for (int i = 0; i < count; i++)
            {
                _updatables[i].OnUpdate();
            }
        }
        public void Register(IUpdatable updatable)
        {
            if (_updatablesSet.Add(updatable))
            {
                _updatables.Add(updatable);
            }
            else
            {
                throw new Exception("Updatable already registered: " + updatable);
            }
        }
        public void Unregister(IUpdatable updatable)
        {
            if (_updatablesSet.Remove(updatable))
            {
                _updatables.Remove(updatable);
            }
            else
            {
                throw new Exception("Updatable already unregistered: " + updatable);
            }
        }
        public void Sort()
        {
            _updatables.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }
        public void Clear()
        {
            _updatables.Clear();
            _updatablesSet.Clear();
        }
        public bool Contains(IUpdatable updatable)
        {
            return _updatablesSet.Contains(updatable);
        }
        public void RemoveNull()
        {

        }

        private readonly List<IUpdatable> _updatables;
        private readonly HashSet<IUpdatable> _updatablesSet;
    }
}