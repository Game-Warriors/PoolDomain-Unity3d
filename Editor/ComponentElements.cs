using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using GameWarriors.PoolDomain.Data;

namespace GameWarriors.PoolDomain.Editor
{
    public class ComponentElements : IPoolElementGroup
    {
        private readonly List<ComponentPoolData> _componentPool;


        public int Count => _componentPool.Count;

        public string name => "Game Component";

        public ComponentElements(ComponentPoolData[] poolData)
        {
            if (poolData != null)
            {
                _componentPool = new List<ComponentPoolData>(poolData.Length + 15);
                _componentPool.AddRange(poolData);
            }
            else
            {
                _componentPool = new List<ComponentPoolData>(30);
            }
        }

        public void AddNewElement()
        {
            _componentPool.Add(new ComponentPoolData());
        }

        public void DrawElement(int index, int width, int height)
        {
            ComponentPoolData componentData = _componentPool[index];
            if (componentData.Component != null)
                GUILayout.Label($"Key: {componentData.Component.gameObject.name}");

            componentData.PoolCount = EditorGUILayout.IntField("Pool Count", componentData.PoolCount, GUILayout.Width(250));
            componentData.Component = EditorGUILayout.ObjectField("Component", componentData.Component, typeof(Component), false) as Component;
            _componentPool[index] = componentData;
            GUILayout.Space(5);
            if (GUILayout.Button("Remove"))
            {
                _componentPool.RemoveAt(index);
            }
        }

        public void SaveElement<T>(T input)
        {
            PoolManagerConfig config = input as PoolManagerConfig;
            //List<ComponentPoolData> data = new List<ComponentPoolData>(_componentPool.Count)
            config.SetComponentsPoolData(_componentPool.Where((item) => item.Component != null && item.PoolCount > 0).ToArray());
        }
    }
}