using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IPool
    {
        void SetupBehaviorInitialization();
        T GetGameComponent<T>(string name) where T : Component;
        GameObject GetGameObject(string name);
        T GetGameBehavior<T>(string key) where T : MonoBehaviour;
        void AddGameObject(GameObject item, string name);
        void AddGameBehavior(MonoBehaviour item, string key);
        void AddGameBehavior(IPoolable poolable);
        void AddGameComponent(Component item, string key);
    }
}
