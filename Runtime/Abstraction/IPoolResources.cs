using GameWarriors.PoolDomain.Data;
using System;

namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IPoolResources
    {
        void LoadResourceAsync(string assetName, IServiceProvider serviceProvider, Action<IServiceProvider, PoolManagerConfig> onLoadDone);
    }
}