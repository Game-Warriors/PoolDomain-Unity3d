using System;

namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IPoolable
    {
        string PoolName { get; }

        /// <summary>
        /// Call when object pop out and get out from pull
        /// </summary>
        void OnPopOut();

        /// <summary>
        /// call when object push in and go inside pool
        /// </summary>
        void OnPushBack();
    }
}