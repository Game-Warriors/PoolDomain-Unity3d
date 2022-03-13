using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    [Serializable]
    public struct ObjectPoolData
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private GameObject _prefab;

        public int PoolCount
        {
            get => _poolCount;
            set => _poolCount = value;
        }

        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }
    }
}