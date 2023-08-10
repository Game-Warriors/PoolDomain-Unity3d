using System;
using System.Collections.Generic;

namespace GameWarriors.PoolDomain.Abstraction
{
    /// <summary>
    /// The base abstraction of MonoBehaviour class scripts initializer which use for the initilization pipeline by pool system.
    /// </summary>
    /// <typeparam name="T">Type of key which is unique identifier of the objects</typeparam>
    public interface IBehaviorInitializer<T>
    {
        /// <summary>
        /// The method name which should be used for method injection
        /// </summary>
        string MethodName { get; }

        void Loading(IEnumerable<(T, Type)> initItems);
        void InitializeBehavior(T id, object behavior);
        void InitializeBehaviors(T id, IEnumerable<object> items);
    }
}