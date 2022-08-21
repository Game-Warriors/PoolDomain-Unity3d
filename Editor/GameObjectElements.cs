using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using GameWarriors.PoolDomain.Data;

namespace GameWarriors.PoolDomain.Editor
{
    public class GameObjectElements : IPoolElementGroup
    {
        private readonly List<ObjectPoolData> _gameObjectsPool;
        public int Count => _gameObjectsPool.Count;

        public string name => "Game Object";

        public GameObjectElements(ObjectPoolData[] poolData)
        {
            if (poolData != null)
            {
                _gameObjectsPool = new List<ObjectPoolData>(poolData.Length + 15);
                _gameObjectsPool.AddRange(poolData);
            }
            else
                _gameObjectsPool = new List<ObjectPoolData>(30);
        }

        public void AddNewElement()
        {
            _gameObjectsPool.Add(new ObjectPoolData());
        }

        public void DrawElement(int index, int fieldWidth, int fieldHeight)
        {
            ObjectPoolData poolData = _gameObjectsPool[index];
            if (poolData.Prefab != null)
                GUILayout.Label($"Key: {poolData.Prefab.gameObject.name}");

            poolData.PoolCount = EditorGUILayout.IntField("Pool Count", poolData.PoolCount, GUILayout.Width(250));
            poolData.Prefab = EditorGUILayout.ObjectField("GameObject", poolData.Prefab, typeof(GameObject), false) as GameObject;
            _gameObjectsPool[index] = poolData;
            GUILayout.Space(10);
            if (GUILayout.Button("Remove"))
            {
                _gameObjectsPool.RemoveAt(index);
            }
        }

        public void SaveElement<T>(T input)
        {
            PoolManagerConfig config = input as PoolManagerConfig;
            config.SetGameObjectPoolData(_gameObjectsPool.Where((item) => item.Prefab != null).ToArray());
        }
    }
}