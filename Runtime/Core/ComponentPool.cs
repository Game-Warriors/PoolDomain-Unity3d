using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class ComponentPool
    {
        private Queue<Component> _queue;
        private Component prefabRef;
        private Transform _transformParent;
        
        public ComponentPool(int poolCount, Component prefab, Transform parent=null)
        {
            _transformParent = parent;
            _queue = new Queue<Component>(poolCount);
            prefabRef = prefab;
            for (int i = 0; i < poolCount; ++i)
            {
                var component = Object.Instantiate(prefab, _transformParent, true);
                //IPoolable item = component as IPoolable;
                //if (item != null)
                //    item.Initialize();
                component.gameObject.SetActive(false);
                _queue.Enqueue(component);
            }
        }


        public void AddItem(Component item)
        {
            _queue.Enqueue(item);
        }

        public Component GetItem()
        {
            var item = _queue.Count > 0 ? _queue.Dequeue() : Object.Instantiate(prefabRef, _transformParent, true);
            return item;
        }

        public T GetItem<T>() where T : Component
        {
            T item;
            if (_queue.Count > 0)
            {
                item = (T)_queue.Dequeue();
            }
            else
            {
                item = (T)Object.Instantiate(prefabRef, _transformParent, true);
            }
            return item;
        }

        public Component[] ToArray()
        {
            return _queue.ToArray();
        }
    }
}