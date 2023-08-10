using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// This class provider resource loading for pool system.
    /// </summary>
    public class DefaultPoolResourceLoader : IPoolResources
    {
        public void LoadResourceAsync(Action<PoolManagerConfig> onLoadDone)
        {
            string assetName = PoolManagerConfig.ASSET_NAME;
            ResourceRequest operation = Resources.LoadAsync<PoolManagerConfig>(assetName);
            operation.completed += (asyncOperation) =>
            {
                PoolManagerConfig config = (asyncOperation as ResourceRequest)?.asset as PoolManagerConfig;
                onLoadDone(config);
                Resources.UnloadAsset(config);
            };
        }
    }
}