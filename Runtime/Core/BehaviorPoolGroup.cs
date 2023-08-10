using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// The pool container which is specify for the “MonoBehaviour” derived classes and directly pool by script class reference.
    /// </summary>
    /// <typeparam name="TV">Type of key which is unique identifier of the objects</typeparam>
    public class BehaviorPoolGroup<TV>
    {
        private readonly Dictionary<TV, BehaviorPool> _pool;
        private readonly Transform _parent;
        public int ItemCount => _pool.Count;

        public IDictionary<TV, BehaviorPool> Items => _pool;

        public IEnumerable<(TV, Type)> ItemsType
        {
            get
            {
                foreach (KeyValuePair<TV, BehaviorPool> item in Items)
                {
                    BehaviorPool pool = item.Value;
                    if (pool.CanInject)
                        yield return new(item.Key, pool.ItemType);
                    else
                        yield return new(item.Key, null);
                }
            }

        }

        public BehaviorPoolGroup(Transform parent)
        {
            _pool ??= new Dictionary<TV, BehaviorPool>();
            _parent = parent;
        }

        public void SetupGroup(BehaviourPoolData poolData, TV key)
        {
            var behaviorPool = new BehaviorPool(poolData.PoolCount, poolData.Behavior, _parent);
            _pool.Add(key, behaviorPool);
        }

        public IEnumerable<MonoBehaviour> GetBehaviorItems(TV key)
        {
            if (_pool.TryGetValue(key, out var pool))
                return pool.BehaviorItems;
            return null;
        }

        public TU GetItem<TU, T>(TV key, IBehaviorInitializer<TV> initializer) where TU : MonoBehaviour
        {
            if (_pool.TryGetValue(key, out var queue))
            {
                TU item = queue.GetItem<TU>();
                if (item == null)
                {
                    item = queue.CreateItem<TU>(_parent);
                    initializer.InitializeBehavior(key, item);
                }
                IPoolable poolable = item as IPoolable;
                poolable?.OnPopOut();
                return item;
            }
            else
                Debug.LogError($"There is no {key} behavior queue type in poolManager");
            return null;
        }

        public void AddItem<TU>(TV type, TU item) where TU : MonoBehaviour
        {
            if (_pool.TryGetValue(type, out var queue))
            {
                IPoolable poolable = item as IPoolable;
                poolable?.OnPushBack();
                queue.AddItem(item);
            }
            else
                Debug.LogError($"There is no {type} behavior queue type in poolManager");
        }
    }
}
