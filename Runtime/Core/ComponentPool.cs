using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class ComponentPool
    {
        private Queue<Component> _queue;
        private Component prefabRef;

        public ComponentPool(int poolCount, Component prefab, Transform parent)
        {
            _queue = new Queue<Component>(poolCount);
            prefabRef = prefab;
            for (int i = 0; i < poolCount; ++i)
            {
                var component = Object.Instantiate(prefab, parent, true);
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

        public Component GetItem(Transform parent)
        {
            var item = _queue.Count > 0 ? _queue.Dequeue() : Object.Instantiate(prefabRef, parent, true);
            return item;
        }

        public T GetItem<T>(Transform parent) where T : Component
        {
            T item;
            if (_queue.Count > 0)
            {
                item = (T)_queue.Dequeue();
            }
            else
            {
                item = (T)Object.Instantiate(prefabRef, parent, true);
            }
            return item;
        }

        public Component[] ToArray()
        {
            return _queue.ToArray();
        }
    }
}