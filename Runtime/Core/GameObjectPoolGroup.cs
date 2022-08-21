using GameWarriors.PoolDomain.Data;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class GameObjectPoolGroup<T>
    {
        private Dictionary<T, GameObjectPool> _pool;

        public void Initialize(ObjectPoolData objectPoolData, Transform gameObjectParent, T key)
        {
            _pool ??= new Dictionary<T, GameObjectPool>();
            GameObjectPool pool = new GameObjectPool(objectPoolData.PoolCount, objectPoolData.Prefab, gameObjectParent);
            _pool.Add(key, pool);
        }


        public GameObject GetItem(T type)
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem();
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