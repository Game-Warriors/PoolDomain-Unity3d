using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    [System.Serializable]
    public struct PoolData
    {
        private const uint UNIQUE_ID_SHIFT_COUNT = 10;
        private const uint SUBTYPE_ID_SHIFT_FILTER = 21;

        private const uint POOL_COUNT_FILTER = 1023;
        private const uint UNIQUE_ID_FILTER = 2043 << 10;
        private const uint SUBTYPE_ID_FILTER = (uint)2043 << 21;

        [SerializeField] private uint _behviourData;

        /// <summary>
        /// Max Amount = 1023
        /// </summary>
        public uint PoolCount
        {
            get => _behviourData & POOL_COUNT_FILTER;
            set => _behviourData = (value & POOL_COUNT_FILTER) | (_behviourData & ~POOL_COUNT_FILTER);
        }

        /// <summary>
        /// Max Amount = 2043
        /// </summary>
        public uint TypeUniqueId
        {
            get => (_behviourData & UNIQUE_ID_FILTER) >> 10;
            set => _behviourData = ((value << 10) & UNIQUE_ID_FILTER) | (_behviourData & ~UNIQUE_ID_FILTER);
        }

        /// <summary>
        /// Max Amount = 2043
        /// </summary>
        public uint SubTypeId
        {
            get => (_behviourData & SUBTYPE_ID_FILTER) >> 21;
            set => _behviourData = ((value << 21) & SUBTYPE_ID_FILTER) | (_behviourData & ~SUBTYPE_ID_FILTER);
        }

    }
}
