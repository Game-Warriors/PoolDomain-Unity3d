using GameWarriors.PoolDomain.Abstraction;
using GameWarriors.PoolDomain.Data;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace GameWarriors.PoolDomain.Core
{
    public class PoolSystem : IPool
    {
        private readonly IBehaviorInitializer<string> _initializer;

        private GameObjectPoolGroup<string> _gameObjectPool;
        private BehaviorPoolGroup<string> _behaviorPool;
        private ComponentPoolGroup<string> _componentPoolString;

        private bool IsBehaviorInit { get; set; }

        [UnityEngine.Scripting.Preserve]
        public PoolSystem(IBehaviorInitializer<string> initializer, IPoolResources poolResources)
        {
            poolResources ??= new DefaultResourceLoader();
            _initializer = initializer;
            poolResources.LoadResourceAsync(PoolManagerConfig.ASSET_NAME, LoadDone);
        }

        public IEnumerator WaitForLoadingCoroutine()
        {
            yield return new WaitUntil(() => _behaviorPool != null);
            _initializer.Loading(_behaviorPool.ItemsType);
        }

        public async Task WaitForLoading()
        {
            while (_behaviorPool == null)
            {
                await Task.Delay(100);
            }
            await Task.Run(() =>
            {
                _initializer.Loading(_behaviorPool.ItemsType);
            });
        }

        public GameObject GetGameObject(string name)
        {
            var tmp = _gameObjectPool.GetItem(name);
            tmp.SetActive(true);
            return tmp;
        }

        public T GetGameBehavior<T>(string key) where T : MonoBehaviour
        {
            if (!IsBehaviorInit)
            {
                SetupBehaviorInitialization();
            }

            T tmp = _behaviorPool.GetItem<T, uint>(key, _initializer);
            tmp.gameObject.SetActive(true);
            return tmp;
        }

        public T GetGameComponent<T>(string name) where T : Component
        {
            var tmp = (T)_componentPoolString.GetItem(name);
            tmp.gameObject.SetActive(true);
            return tmp;
        }

        public void AddGameObject(GameObject item, string name)
        {
            item.SetActive(false);
            _gameObjectPool.AddItem(name, item);
        }

        public void AddGameBehavior(MonoBehaviour item, string key)
        {
            item.gameObject.SetActive(false);
            _behaviorPool.AddItem(key, item);
        }

        public void AddGameBehavior(IPoolable poolable)
        {
            MonoBehaviour item = (MonoBehaviour)poolable;
            item.gameObject.SetActive(false);
            _behaviorPool.AddItem(poolable.PoolName, item);
        }

        public void AddGameComponent(Component item, string key)
        {
            item.gameObject.SetActive(false);
            _componentPoolString.AddItem(key, item);
        }

        public void SetupBehaviorInitialization()
        {
            if (!IsBehaviorInit)
            {
                foreach (var item in _behaviorPool.Items)
                {
                    _initializer.InitializeBehaviors(item.Key, item.Value.BehaviorItems);
                }
                IsBehaviorInit = true;
            }
        }

        private void LoadDone(PoolManagerConfig config)
        {
            int length = config.ComponentPool.Length;
            Transform componentParent = new GameObject("ComponentPool").transform;
            _componentPoolString = new ComponentPoolGroup<string>(componentParent);

            for (int i = 0; i < length; ++i)
            {
                Component component = config.ComponentPool[i].Component;
                _componentPoolString.SetupGroup(config.ComponentPool[i], component.gameObject.name);
            }

            Transform gameObjectParent = new GameObject("GameObjectPool").transform;
            _gameObjectPool = new GameObjectPoolGroup<string>(gameObjectParent);
            length = config.ObjectPool.Length;
            for (int i = 0; i < length; ++i)
            {
                _gameObjectPool.SetupGroup(config.ObjectPool[i], string.Intern(config.ObjectPool[i].Prefab.name));
            }

            Transform behaviorParent = new GameObject("BehaviorPool").transform;
            _behaviorPool = new BehaviorPoolGroup<string>(behaviorParent);
            length = config.BehaviorPool.Length;
            for (int i = 0; i < length; ++i)
            {
                string key = config.BehaviorPool[i].Behavior.gameObject.name;
                _behaviorPool.SetupGroup(config.BehaviorPool[i], key);
            }
        }
    }
}