namespace GameWarriors.PoolDomain.Abstraction
{
    /// <summary>
    /// The abstraction which is implemented on the object cause notification by method call when its pop out or push back to pool container.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// The key name of the prefab which used for retrieving.
        /// </summary>
        string PoolName { get; }

        /// <summary>
        /// Call when object pop out and get out from pull
        /// </summary>
        void OnPopOut();

        /// <summary>
        /// Call when object push in and go inside pool
        /// </summary>
        void OnPushBack();
    }
}