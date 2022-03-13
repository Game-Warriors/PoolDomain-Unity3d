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
        private Dictionary<TV, BehaviorPool> _pool;

        public void Initialize(IServiceProvider serviceProvider, BehaviourPoolData poolData, Transform parent, TV key)
        {
            _pool ??= new Dictionary<TV, BehaviorPool>();
            var behaviorPool = new BehaviorPool(serviceProvider,poolData.PoolCount, poolData.Behavior, parent);
            _pool.Add(key, behaviorPool);
        }


        public TU GetItem<TU, T>(TV type) where TU : MonoBehaviour
        {
            if (_pool.TryGetValue(type, out var queue))
            {
                TU item = queue.GetItem<TU>();
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
