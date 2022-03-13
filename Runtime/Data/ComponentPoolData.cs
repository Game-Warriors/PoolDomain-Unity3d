using System;
using UnityEngine;

namespace GameWarriors.PoolDomain.Data
{
    [Serializable]
    public struct ComponentPoolData
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private Component _component;

        public int PoolCount
        {
            get => _poolCount;
            set => _poolCount = value;
        }

        public Component Component
        {
            get => _component;
            set => _component = value;
        }
    }
}