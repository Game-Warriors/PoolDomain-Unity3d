using UnityEngine;
using UnityEditor;
using GameWarriors.PoolDomain.Data;
using System.IO;

namespace GameWarriors.PoolDomain.Editor
{
    public class PoolManagerEditor : EditorWindow
    {
        private const int ELEMENT_WIDTH = 300;
        private const int ELEMENT_HEIGHT = 100;
        private int _tapIndex = 0;
        private string[] _tapContents;
        private IPoolElementGroup[] _poolElements;
        private int _drawCount;
        private PoolManagerConfig _poolConfig;
        private Vector2 scrollPosition;
        private IPoolElementGroup CurrentElement => _poolElements[_tapIndex];

        [MenuItem("Tools/Pool Configuration")]
        private static void OnPoolManagerEditor()
        {
            if (!Directory.Exists("Assets/AssetData/Resources"))
                Directory.CreateDirectory("Assets/AssetData/Resources");

            PoolManagerEditor editor = CreateInstance<PoolManagerEditor>();
            editor.Initialize();
            editor.Show();
        }

        private void Initialize()
        {
            _poolElements = new IPoolElementGroup[3];
            _poolConfig = AssetDatabase.LoadAssetAtPath<PoolManagerConfig>(PoolManagerConfig.ASSET_PATH);
            if (_poolConfig == null)
            {
                _poolConfig = CreateInstance<PoolManagerConfig>();
                AssetDatabase.CreateAsset(_poolConfig, PoolManagerConfig.ASSET_PATH);
            }

            _poolElements[0] = new GameObjectElements(_poolConfig.ObjectPool);
            _poolElements[1] = new ComponentElements(_poolConfig.ComponentPool);
            _poolElements[2] = new BehaviorElements(_poolConfig.BehaviorPool);
            _tapContents = new string[] { _poolElements[0].name + " Pool", _poolElements[1].name + " Pool", _poolElements[2].name + " Pool" };
        }
        void OnGUI()
        {
            DrawElementView();
        }

        private void DrawElementView()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            _tapIndex = GUILayout.Toolbar(_tapIndex, _tapContents);
            if (_poolElements != null)
            {
                _drawCount = CurrentElement.Count;
                int horizontalCount = (int)(position.width / (ELEMENT_WIDTH + 15));
                horizontalCount = Mathf.Max(1, horizontalCount);
                int verticalCount = CurrentElement.Count / horizontalCount;

                ++verticalCount;
                for (int i = 0; i < verticalCount; ++i)
                {
                    DrawHorizontalElement((i * horizontalCount) +1, CurrentElement.name, horizontalCount);
                }
                // GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }
            else
            {
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                Close();
            }

        }

        private void DrawHorizontalElement(int startVerticalCount, string elementName, int elementCount)
        {
            int count = Mathf.Min(_drawCount, elementCount);

            GUILayout.BeginHorizontal();
            for (int i = 0; i < count; ++i)
            {
                GUILayout.BeginVertical($"{elementName} : {startVerticalCount + i}", GUI.skin.box, GUILayout.Width(ELEMENT_WIDTH));
                GUILayout.Space(30);
                CurrentElement.DrawElement(CurrentElement.Count - _drawCount, ELEMENT_WIDTH, ELEMENT_HEIGHT);
                GUILayout.EndVertical();
                GUILayout.Space(10);
                --_drawCount;
            }
            if (count < elementCount)
                DrawAddButton();
            GUILayout.EndHorizontal();
            DrawSaveButton();
        }

        private void DrawAddButton()
        {
            if (GUILayout.Button("+", GUILayout.Width(40), GUILayout.Height(40)))
            {
                CurrentElement.AddNewElement();
            }
        }

        private void DrawSaveButton()
        {
            if (GUI.Button(new Rect(position.width - 105, 5, 100, 20), "Save"))
            {
                int length = _poolElements.Length;
                for (int i = 0; i < length; i++)
                {
                    _poolElements[i].SaveElement(_poolConfig);
                    EditorUtility.SetDirty(_poolConfig);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}