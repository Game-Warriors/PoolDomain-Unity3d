using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TV">Item Key</typeparam>
    /// <typeparam name="U">Item Type</typeparam>
    public class BehaviorPoolGroup<TV>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<TV, BehaviorPool> _pool;
        private readonly Transform _parent;
        private bool _isInit;

        public BehaviorPoolGroup(IServiceProvider serviceProvider, Transform parent)
        {
            _pool ??= new Dictionary<TV, BehaviorPool>();
            _serviceProvider = serviceProvider;
            _parent = parent;
        }

        public void SetupGroup(BehaviourPoolData poolData, TV key)
        {
            var behaviorPool = new BehaviorPool(poolData.PoolCount, poolData.Behavior, _parent);
            _pool.Add(key, behaviorPool);
        }

        public void InitializeBehaviors()
        {
            if (!_isInit)
            {
                foreach (BehaviorPool item in _pool.Values)
                {
                    item.SetupInitializables(_serviceProvider);
                }
                _isInit = true;
            }
        }

        public TU GetItem<TU, T>(TV type) where TU : MonoBehaviour
        {
            if (!_isInit)
                InitializeBehaviors();

            if (_pool.TryGetValue(type, out var queue))
            {
                TU item = queue.GetItem<TU>(_serviceProvider, _parent);
                IPoolable poolable = item as IPoolable;
                poolable?.OnPopOut();
                return item;
            }
            else
                Debug.LogError($"There is no {type} behavior queue type in poolManager");
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
