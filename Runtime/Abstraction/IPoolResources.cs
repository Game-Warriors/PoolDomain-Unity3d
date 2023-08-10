using GameWarriors.PoolDomain.Data;
using System;

namespace GameWarriors.PoolDomain.Abstraction
{
    /// <summary>
    /// The abstraction which using by pool system to load require data.
    /// </summary>
    public interface IPoolResources
    {
        /// <summary>
        /// Loading pool data asynchronously and trigger the callback when its done.
        /// </summary>
        /// <param name="onLoadDone"></param>
        void LoadResourceAsync(Action<PoolManagerConfig> onLoadDone);
    }
}