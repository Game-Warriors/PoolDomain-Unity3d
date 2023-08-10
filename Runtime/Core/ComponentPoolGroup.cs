using GameWarriors.PoolDomain.Data;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// The pool container which is specify for the component derived classes and directly pool by unity3d component class reference.
    /// </summary>
    /// <typeparam name="TV">Type of key which is unique identifier of the objects</typeparam>
    public class ComponentPoolGroup<TV>
    {
        private readonly Dictionary<TV, ComponentPool> _pool;
        private readonly Transform _parent;

        public ComponentPoolGroup(Transform parent)
        {
            _parent = parent;
            _pool = new Dictionary<TV, ComponentPool>();
        }

        public void SetupGroup(ComponentPoolData poolData, TV key)
        {
            ComponentPool componentPool = new ComponentPool(poolData.PoolCount, poolData.Component, _parent);
            _pool.Add(key, componentPool);
        }

        public Component GetItem(TV type)
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem(_parent);
            else
                Debug.LogError($"There is no {type} Component queue type in poolManager");
            return null;
        }

        public T GetItem<T>(TV type) where T : Component
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem<T>(_parent);
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