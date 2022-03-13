using GameWarriors.PoolDomain.Data;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class ComponentPoolGroup<TV>
    {
        private Dictionary<TV, ComponentPool> _pool;

        public ComponentPoolGroup()
        {
            _pool = new Dictionary<TV, ComponentPool>();
        }

        public void Initialize(ComponentPoolData poolData, Transform parent, TV key)
        {
            ComponentPool componentPool = new ComponentPool(poolData.PoolCount, poolData.Component, parent);
            _pool.Add(key, componentPool);
        }

        public Component GetItem(TV type)
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem();
            else
                Debug.LogError($"There is no {type} Component queue type in poolManager");
            return null;
        }

        public T GetItem<T>(TV type) where T : Component
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem<T>();
            else
                Debug.LogError($"There is no {type} Component queue type in poolManager");
            return null;
        }

        public void AddItem(TV type, Component item)
        {
            if (_pool.TryGetValue(type, out var queue))
                queue.AddItem(item);
            else
                Debug.LogError($"There is no {type} Component queue type in poolManager");
        }
    }
}