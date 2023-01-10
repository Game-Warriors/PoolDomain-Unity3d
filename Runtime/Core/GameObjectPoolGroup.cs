using GameWarriors.PoolDomain.Data;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class GameObjectPoolGroup<T>
    {
        private readonly Dictionary<T, GameObjectPool> _pool;
        private readonly Transform _parent;

        public GameObjectPoolGroup(Transform parent)
        {
            _pool ??= new Dictionary<T, GameObjectPool>();
            _parent = parent;
        }

        public void SetupGroup(ObjectPoolData objectPoolData, T key)
        {
            GameObjectPool pool = new GameObjectPool(objectPoolData.PoolCount, objectPoolData.Prefab, _parent);
            _pool.Add(key, pool);
        }

        public GameObject GetItem(T type)
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem(_parent);
            else
                Debug.LogError($"There is no {type} gameObject queue type in poolManager");
            return null;
        }

        public void AddItem(T type, GameObject item)
        {
            if (_pool.TryGetValue(type, out var queue))
                queue.AddItem(item);
            else
                Debug.LogError($"There is no {type} gameObject queue type in poolManager");
        }
    }
}