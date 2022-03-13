using GameWarriors.PoolDomain.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameWarriors.PoolDomain.Editor
{
    public class ClassElements : IPoolElementGroup
    {
        private readonly List<ClassPoolData> _classPool;

        public int Count => _classPool.Count;

        public string name => "Class";

        public ClassElements(ClassPoolData[] poolData)
        {
            if (poolData != null)
            {
                _classPool = new List<ClassPoolData>(poolData.Length + 15);
                _classPool.AddRange(poolData);
            }
            else
                _classPool = new List<ClassPoolData>(30);
        }


        public void AddNewElement()
        {
            _classPool.Add(new ClassPoolData());
        }


        public void DrawElement(int index, int width, int height)
        {
            ClassPoolData behaviorData = _classPool[index];
            behaviorData.PoolCount = EditorGUILayout.IntField("Pool Count", behaviorData.PoolCount, GUILayout.Width(250));

            //SerializedProperty
            //behaviorData.ClassType = EditorGUILayout.ObjectField("h",tmp,typeof(object)).GetType();
            if (GUILayout.Button("Remove"))
            {
                _classPool.RemoveAt(index);
            }
        }

        public void SaveElement<T>(T input)
        {
            throw new NotImplementedException();
        }
    }

}