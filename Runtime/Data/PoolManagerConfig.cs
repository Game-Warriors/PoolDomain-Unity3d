using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    public class PoolManagerConfig : ScriptableObject
    {
        public const string ASSET_NAME = "PoolConfig";
        public const string ASSET_PATH = "Assets/AssetData/Resources/PoolConfig.asset";

        [SerializeField] private ObjectPoolData[] _objectPoolData;
        [SerializeField] private ComponentPoolData[] _componentPoolData;
        [SerializeField] private BehaviourPoolData[] _behaviorPoolData;
        [SerializeField] private ClassPoolData[] _classPoolData;

        public void SetGameObjectPoolData(ObjectPoolData[] objectPoolData)
        {
            _objectPoolData = objectPoolData;
        }

        public void SetComponentsPoolData(ComponentPoolData[] componentPoolData)
        {
            _componentPoolData = componentPoolData;
        }

        public void SetBehaviorPoolData(BehaviourPoolData[] behaviourPoolData)
        {
            _behaviorPoolData = behaviourPoolData;
        }

        public ObjectPoolData[] ObjectPool => _objectPoolData;

        public ComponentPoolData[] ComponentPool => _componentPoolData;

        public BehaviourPoolData[] BehaviorPool => _behaviorPoolData;

        public ClassPoolData[] ClassPool => _classPoolData;
    }

}