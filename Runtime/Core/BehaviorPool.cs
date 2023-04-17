using GameWarriors.PoolDomain.Abstraction;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class BehaviorPool
    {
        private Queue<MonoBehaviour> _queue;
        private MonoBehaviour _prefabRef;

        public IEnumerable<MonoBehaviour> BehaviorItems => _queue;

        public Type ItemType => _prefabRef.GetType();

        public bool CanInject => _prefabRef as IInitializable == null;

        public BehaviorPool(int poolCount, MonoBehaviour prefab, Transform parent)
        {
            _queue = new Queue<MonoBehaviour>(poolCount);
            _prefabRef = prefab;
            for (int i = 0; i < poolCount; ++i)
            {
                MonoBehaviour behavior = UnityEngine.Object.Instantiate(prefab, parent, true);
                behavior.gameObject.SetActive(true);

                behavior.gameObject.SetActive(false);
                _queue.Enqueue(behavior);
            }
        }

        public void AddItem(MonoBehaviour item)
        {
            _queue.Enqueue(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetItem<T>() where T : MonoBehaviour
        {
            T item = null;
            if (_queue.Count > 0)
            {
                item = (T)_queue.Dequeue();
            }
            return item;
        }

        public T CreateItem<T>(Transform parent) where T : MonoBehaviour
        {
            MonoBehaviour behavior = UnityEngine.Object.Instantiate(_prefabRef, parent, true);
            return (T)behavior;
        }
    }
}
