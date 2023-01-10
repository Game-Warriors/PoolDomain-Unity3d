using GameWarriors.PoolDomain.Abstraction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class BehaviorPool
    {
        private Queue<MonoBehaviour> _queue;
        private MonoBehaviour prefabRef;
        
        public BehaviorPool(int poolCount, MonoBehaviour prefab, Transform parent)
        {
            _queue = new Queue<MonoBehaviour>(poolCount);
            prefabRef = prefab;
            for (int i = 0; i < poolCount; ++i)
            {
                MonoBehaviour behavior = UnityEngine.Object.Instantiate(prefab, parent, true);
                behavior.gameObject.SetActive(true);

                behavior.gameObject.SetActive(false);
                _queue.Enqueue(behavior);
            }
        }

        public void SetupInitializables(IServiceProvider serviceProvider)
        {
            foreach (var item in _queue)
            {
                IInitializable initializable = item as IInitializable;
                initializable?.Initialize(serviceProvider);
            }
        }

        public void AddItem(MonoBehaviour item)
        {
            _queue.Enqueue(item);
        }

        public T GetItem<T>(IServiceProvider serviceProvider, Transform parent) where T : MonoBehaviour
        {
            T item;
            if (_queue.Count > 0)
            {
                item = (T)_queue.Dequeue();
            }
            else
            {
                MonoBehaviour behavior = UnityEngine.Object.Instantiate(prefabRef, parent, true);
                IInitializable initializable = behavior as IInitializable;
                initializable?.Initialize(serviceProvider);
                item = (T)behavior;
            }
            return item;
        }

    }
}
