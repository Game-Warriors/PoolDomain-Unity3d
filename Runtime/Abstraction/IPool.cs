using UnityEngine;

namespace GameWarriors.PoolDomain.Abstraction
{
    /// <summary>
    /// The main abstraction which provide the object pooling system main features
    /// </summary>
    public interface IPool
    {
        /// <summary>
        /// By calling this method all added prefabs in editor which has capability of initialize pipeline, if the pipeline not triggered before.
        /// </summary>
        void SetupBehaviorInitialization();

        /// <summary>
        /// Getting the component instance from pool component container by unique name key.
        /// </summary>
        /// <typeparam name="T">Main type of componenet</typeparam>
        /// <param name="name">The unique name of the component prefab</param>
        /// <returns>Return the active instance if exist by input name, otherwise return null and log error</returns>
        T GetGameComponent<T>(string name) where T : Component;

        /// <summary>
        /// Getting the gameobject instance from gameobject container by unique name key.
        /// </summary>
        /// <param name="name">The unique name of the gameobject prefab</param>
        /// <returns>Return the active instance if exist by input name, otherwise return null and log error</returns>
        GameObject GetGameObject(string name);

        /// <summary>
        /// Getting the MonoBehaviour instance from behaviour container by unique name key.
        /// </summary>
        /// <typeparam name="T">Main type of the MonoBehaviour class</typeparam>
        /// <param name="name">The unique name of the behaviour prefab</param>
        /// <returns>Return the active instance if exist by the name, otherwise return null and log error</returns>
        T GetGameBehavior<T>(string name) where T : MonoBehaviour;

        /// <summary>
        /// Adding and deactivate the instance to gameobject container by unique name key. log error if specific object pool does not exists.
        /// </summary>
        /// <param name="item">The instance of the game object</param>
        /// <param name="name">The unique name of the gameobject prefab</param>
        void AddGameObject(GameObject item, string name);

        /// <summary>
        /// Adding and deactivate the instance to behaviour container by unique name key. log error if specific object pool does not exists.
        /// </summary>
        /// <param name="item">the instance of the class</param>
        /// <param name="name">The unique name of the behaviour prefab</param>
        void AddGameBehavior(MonoBehaviour item, string name);

        /// <summary>
        /// Adding and deactivate the IPoolable instance to behaviour container by unique name key. log error if specific object pool does not exists.
        /// </summary>
        /// <param name="poolable">The MonoBehaviour unity script which implement IPoolable interface</param>
        void AddGameBehavior(IPoolable poolable);

        /// <summary>
        /// Adding and deactivate the instance to component container by unique name key. log error if specific object pool does not exists.
        /// </summary>
        /// <param name="item">The instance of the component</param>
        /// <param name="name">The unique name of the component prefab</param>
        void AddGameComponent(Component item, string name);
    }
}
