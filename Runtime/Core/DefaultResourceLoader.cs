using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class DefaultResourceLoader : IPoolResources
    {

        public void LoadResourceAsync(string assetName, IServiceProvider serviceProvider, Action<IServiceProvider, PoolManagerConfig> onLoadDone)
        {
            ResourceRequest operation = Resources.LoadAsync<PoolManagerConfig>(assetName);
            operation.completed += (asyncOperation) => onLoadDone(serviceProvider, (asyncOperation as ResourceRequest)?.asset as PoolManagerConfig);
        }
    }
}