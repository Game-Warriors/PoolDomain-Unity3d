using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    [System.Serializable]
    public struct BehaviourPoolData
    {
        //private const uint UNIQUE_ID_SHIFT_COUNT = 10;
        //private const uint SUBTYPE_ID_SHIFT_FILTER = 21;

        //private const uint POOL_COUNT_FILTER = 1023;
        //private const uint UNIQUE_ID_FILTER = 2043 << 10;
        //private const uint SUBTYPE_ID_FILTER = (uint)2043 << 21;

        [SerializeField] private int _poolCount;
        [SerializeField] private MonoBehaviour _behavior;

        /// <summary>
        /// Max Amount = 1023
        /// </summary>
        public int PoolCount
        {
            get => _poolCount;
            set => _poolCount = value;
            //get { return _behaviourData & POOL_COUNT_FILTER; }
            //set { _behaviourData = (value & POOL_COUNT_FILTER) | (_behaviourData & ~POOL_COUNT_FILTER); }
        }

        // Max Amount = 2043
        //public uint TypeUniqueId
        //{
        //get { return (_behaviourData & UNIQUE_ID_FILTER) >> 10; }
        //set { _behaviourData = ((value << 10) & UNIQUE_ID_FILTER) | (_behaviourData & ~UNIQUE_ID_FILTER); }
        //}

        /// <summary>
        /// Max Amount = 2043
        /// </summary>
        //public uint SubTypeId
        //{
        //get { return (_behaviourData & SUBTYPE_ID_FILTER) >> 21; }
        //set { _behaviourData = ((value << 21) & SUBTYPE_ID_FILTER) | (_behaviourData & ~SUBTYPE_ID_FILTER); }
        //}

        public MonoBehaviour Behavior
        {
            get => _behavior;
            set => _behavior = value;
        }

        public BehaviourPoolData(int poolCount, MonoBehaviour behavior)
        {
            _poolCount = 0;
            //_behaviourData = 0;
            _behavior = behavior;
            PoolCount = poolCount;
            //TypeUniqueId = uniqueId;
            //SubTypeId = subtypeId;
        }

        //private static void DebugTest()
        //{
        //    var T = new BehaviourPoolData(10, 2, 3, null);
        //    Debug.LogFormat("count:{0} , unique ID:{1} , sub type id:{2}", T.PoolCount, T.TypeUniqueId, T.SubTypeId);
        //}
    }
}