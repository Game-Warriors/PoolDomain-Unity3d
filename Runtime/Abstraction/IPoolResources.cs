using GameWarriors.PoolDomain.Data;
using System;

namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IPoolResources
    {
        void LoadResourceAsync(string assetName, Action<PoolManagerConfig> onLoadDone);
    }
}