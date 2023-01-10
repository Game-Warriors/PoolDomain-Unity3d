using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class GameObjectPool
    {
        private readonly Queue<GameObject> _queue;
        private readonly GameObject _objectToPool;

        public GameObjectPool(int poolCount, GameObject prefab, Transform parent = null)
        {
            _objectToPool = prefab;
            _queue = new Queue<GameObject>(poolCount);
            for (int i = 0; i < poolCount; ++i)
            {
                GameObject tmp = Object.Instantiate(_objectToPool, parent, true);
                tmp.SetActive(false);
                _queue.Enqueue(tmp);
            }
        }

        public void AddItem(GameObject item)
        {
            item.SetActive(false);
            _queue.Enqueue(item);
        }

        public GameObject GetItem(Transform parent)
        {
            GameObject item = _queue.Count > 0 ? _queue.Dequeue() : Object.Instantiate(_objectToPool, parent, true);
            item.SetActive(true);
            return item;
        }

        public GameObject[] ToArray()
        {
            return _queue.ToArray();
        }
    }
}
