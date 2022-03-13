using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    [Serializable]
    public struct ClassPoolData
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private string _classType;

        public int PoolCount
        {
            get => _poolCount;
            set => _poolCount = value;
        }

        public string ClassType
        {
            get => _classType;
            set => _classType = value;
        }
    }
}