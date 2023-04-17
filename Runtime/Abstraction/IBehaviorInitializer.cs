using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IBehaviorInitializer<T>
    {
        string MethodName { get; }

        void Loading(IEnumerable<(T, Type)> initItems);
        void InitializeBehavior(T id, object behavior);
        void InitializeBehaviors(T id, IEnumerable<object> items);
    }
}