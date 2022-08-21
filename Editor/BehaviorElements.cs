using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using GameWarriors.PoolDomain.Data;

namespace GameWarriors.PoolDomain.Editor
{
    public class BehaviorElements : IPoolElementGroup
    {
        private readonly struct TypeData
        {
            private readonly string[] _subTypeName;
            private readonly uint _typeUniqueId;

            public uint TypeUniqueId => _typeUniqueId;
            public string[] SubTypeName => _subTypeName;

            public TypeData(uint typeUniqueId, string[] subTypeName)
            {
                _typeUniqueId = typeUniqueId;
                _subTypeName = subTypeName;
            }
        }

        private readonly List<BehaviourPoolData> _behaviorPool;

        public int Count => _behaviorPool.Count;

        public string name => "Game Behavior";

        public BehaviorElements(BehaviourPoolData[] poolData)
        {
            if (poolData != null)
            {
                _behaviorPool = new List<BehaviourPoolData>(poolData.Length + 15);
                _behaviorPool.AddRange(poolData);
            }
            else
            {
                _behaviorPool = new List<BehaviourPoolData>(30);
            }
        }

        public void AddNewElement()
        {
            _behaviorPool.Add(new BehaviourPoolData());
        }

        public void DrawElement(int index, int width, int height)
        {
            BehaviourPoolData behaviorData = _behaviorPool[index];
            if (behaviorData.Behavior != null)
                GUILayout.Label($"Key: {behaviorData.Behavior.gameObject.name}");

            behaviorData.PoolCount = EditorGUILayout.IntField("Pool Count", behaviorData.PoolCount, GUILayout.Width(250));
            behaviorData.Behavior = EditorGUILayout.ObjectField("Behavior", behaviorData.Behavior, typeof(MonoBehaviour), false) as MonoBehaviour;

            _behaviorPool[index] = behaviorData;
            GUILayout.Space(5);
            if (GUILayout.Button("Remove"))
            {
                _behaviorPool.RemoveAt(index);
            }
        }

        public void SaveElement<T>(T input)
        {
            var config = input as PoolManagerConfig;
            config.SetBehaviorPoolData(_behaviorPool.Where((item) => item.Behavior != null)
                .ToArray());
        }


    }

}