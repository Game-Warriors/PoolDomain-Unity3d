using GameWarriors.PoolDomain.Abstraction;

using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class BehaviorPool
    {
        private Queue<MonoBehaviour> _queue;
        private MonoBehaviour prefabRef;
        private System.IServiceProvider _serviceProvider;
        private Transform _transformParent;
        
        public BehaviorPool(System.IServiceProvider serviceProvider, int poolCount, MonoBehaviour prefab, Transform parent)
        {
            _queue = new Queue<MonoBehaviour>(poolCount);
            prefabRef = prefab;
            _transformParent = parent;
            _serviceProvider = serviceProvider;
            for (int i = 0; i < poolCount; ++i)
            {
                MonoBehaviour behavior = Object.Instantiate(prefab, _transformParent, true);
                behavior.gameObject.SetActive(true);
                IPoolable item = behavior as IPoolable;
                item?.Initialize(serviceProvider);
                behavior.gameObject.SetActive(false);
                _queue.Enqueue(behavior);
            }
        }

        public void AddItem(MonoBehaviour item)
        {
            _queue.Enqueue(item);
        }

        public T GetItem<T>() where T : MonoBehaviour
        {
            T item;
            if (_queue.Count > 0)
            {
                item = (T)_queue.Dequeue();
            }
            else
            {
                MonoBehaviour behavior = Object.Instantiate(prefabRef, _transformParent, true);
                IPoolable poolable = behavior as IPoolable;
                poolable?.Initialize(_serviceProvider);
                item = (T)behavior;
            }
            return item;
        }

    }
}
