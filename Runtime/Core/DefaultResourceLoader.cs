using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class DefaultResourceLoader : IPoolResources
    {
        public void LoadResourceAsync(string assetName, Action<PoolManagerConfig> onLoadDone)
        {
            ResourceRequest operation = Resources.LoadAsync<PoolManagerConfig>(assetName);
            operation.completed += (asyncOperation) => onLoadDone((asyncOperation as ResourceRequest)?.asset as PoolManagerConfig);
        }
    }
}